using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using clawSoft.clawPDF.Assistants;
using clawSoft.clawPDF.Core.Ghostscript;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.Helper.Logging;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.Startup;
using clawSoft.clawPDF.Threading;
using clawSoft.clawPDF.Utilities.Communication;
using NLog;
using Application = System.Windows.Forms.Application;

namespace clawSoft.clawPDF
{
    public partial class App
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            InitializeComponent();

            Application.EnableVisualStyles();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var globalMutex = new GlobalMutex("clawPDF-137a7751-1070-4db4-a407-83c1625762c7");
            globalMutex.Acquire();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Thread.CurrentThread.Name = "ProgramThread";

            try
            {
                LoggingHelper.InitFileLogger("clawPDF", LoggingLevel.Error);

                RunApplication(e.Args);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "There was an error while starting clawPDF");
            }
            finally
            {
                globalMutex.Release();
                Logger.Debug("Ending clawPDF");
                Shutdown();
            }
        }

        private void RunApplication(string[] commandLineArguments)
        {
            CheckSpoolerRunning();

            // Must be done before the other checks to initialize the translator
            SettingsHelper.Init();

            // Check translations and Ghostscript. Exit if there is a problem
            CheckInstallation();

            var appStartFactory = new AppStartFactory();
            var appStart = appStartFactory.CreateApplicationStart(commandLineArguments);

            // PrintFile needs to be started before initializing the JonbInfoQueue
            if (appStart is PrintFileStart)
            {
                appStart.Run();
                return;
            }

            Logger.Debug("Starting clawPDF");

            if (commandLineArguments.Length > 0)
                Logger.Info("Command Line parameters: \r\n" + string.Join(" ", commandLineArguments));

            if (!InitializeJobInfoQueue())
                return;

            // Start the application
            appStart.Run();

            Logger.Debug("Waiting for all threads to finish");
            ThreadManager.Instance.WaitForThreadsAndShutdown(this);
        }

        private void CheckSpoolerRunning()
        {
            var spoolerController = new ServiceController("spooler");
            if (spoolerController.Status != ServiceControllerStatus.Running)
            {
                Logger.Error("Spooler service is not running. Exiting...");
                var message =
                    "The Windows spooler service is not running. Please start the spooler first.\r\n\r\nProgram exiting now.";
                const string caption = @"clawPDF";
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private bool InitializeJobInfoQueue()
        {
            JobInfoQueue.Init();

            if (!JobInfoQueue.Instance.SpoolFolderIsAccessible())
            {
                var repairSpoolFolderAssistant = new RepairSpoolFolderAssistant();
                return repairSpoolFolderAssistant.TryRepairSpoolPath();
            }

            return true;
        }

        private void CheckInstallation()
        {
            if (TranslationHelper.Instance.TranslationPath == null)
            {
                MessageBox.Show(@"Could not find any translation. Please reinstall clawPDF.",
                    @"Translations missing");
                Shutdown(1);
            }

            // Verfiy that Ghostscript is installed and exit if not
            //EnsureGhoscriptIsInstalled();

            // Verify that clawPDF printers are installed
            EnsurePrinterIsInstalled();
        }

        private void EnsureGhoscriptIsInstalled()
        {
            if (!HasGhostscriptInstance())
            {
                Logger.Debug("No valid Ghostscript version found. Exiting...");
                var message = TranslationHelper.Instance.TranslatorInstance.GetTranslation("ConversionWorkflow",
                    "NoSupportedGSFound",
                    "Can't find a supported Ghostscript installation.\r\n\r\nProgram exiting now.");
                const string caption = @"clawPDF";
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
                Environment.Exit(1);
            }
        }

        private void EnsurePrinterIsInstalled()
        {
            var repairPrinterAssistant = new RepairPrinterAssistant();

            if (repairPrinterAssistant.IsRepairRequired())
            {
                var printers =
                    SettingsHelper.Settings.ApplicationSettings.PrinterMappings.Select(mapping => mapping.PrinterName);
                if (!repairPrinterAssistant.TryRepairPrinter(printers)) Environment.Exit(1);
            }
        }

        private bool HasGhostscriptInstance()
        {
            var gsVersion = new GhostscriptDiscovery().GetBestGhostscriptInstance();

            return gsVersion != null;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            Logger.Fatal(ex, "Uncaught exception, IsTerminating: {0}", e.IsTerminating);
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error(e.Exception, "Uncaught exception in WPF thread");

            e.Handled = true;
            Current.Shutdown(1);
        }
    }
}