using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.Registry;
using NLog;
using pdfforge.DataStorage.Storage;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Startup
{
    internal class AppStartFactory
    {
        private readonly ApplicationSettings _appSettings;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public Action<clawPDFSettings> UpdateSettings { get; set; }

        public AppStartFactory() : this(SettingsHelper.Settings.ApplicationSettings)
        {
        }

        internal AppStartFactory(ApplicationSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public IAppStart CreateApplicationStart(string[] commandLineArgs)
        {
            return CreateApplicationStart(commandLineArgs, new FileWrap());
        }

        public IAppStart CreateApplicationStart(string[] commandLineArgs, IFile fileWrap)
        {
            if (DragAndDropHelper.IsDragAndDrop(commandLineArgs))
            {
                _logger.Debug("Detected only filenames as parameters: Assuming Drag & Drop");
                var validFiles = DragAndDropHelper.RemoveInvalidFiles(commandLineArgs, fileWrap);
                if (validFiles.Count > 0)
                    return new DragAndDropStart(commandLineArgs);
            }

            var commandLineParser = new CommandLineParser(commandLineArgs);

            if (commandLineParser.HasArgument("Config"))
            {
                Console.WriteLine("Load settings");
                var settings = SettingsHelper.CreateEmptySettings();
                var ini = new IniStorage(Encoding.UTF8);
                var configFile = FindConfigParameter(commandLineParser);
                settings.LoadDataStart(ini, configFile, SettingsHelper.UpgradeSettings);
                SettingsHelper.ApplySettings(settings);
                SettingsHelper.SaveSettings();
                return null;
            }

            if (commandLineParser.HasArgument("PrintFile"))
            {
                var settings = SettingsHelper.Settings;
                var primaryPrinter = settings.ApplicationSettings.PrimaryPrinter;
                var printerDefaultProfileGuid = "";
                var lastProfile = settings.ApplicationSettings.LastUsedProfileGuid;

                var printFile = FindPrintFile(commandLineParser);
                var printerName = FindPrinterName(commandLineParser);
                var profileName = FindProfileParameter(commandLineParser);

                foreach (var printer in settings.ApplicationSettings.PrinterMappings)
                {
                    if (printer.PrinterName == primaryPrinter)
                    {
                        printerDefaultProfileGuid = printer.ProfileGuid;
                        printer.ProfileGuid = "";
                    }
                }

                settings.ApplicationSettings.LastUsedProfileGuid = profileName;

                RegistryUtility.WriteRegistryValue(@"Software\clawSoft\clawPDF\Batch", "DefaultProfileGuid", lastProfile ?? "");
                RegistryUtility.WriteRegistryValue(@"Software\clawSoft\clawPDF\Batch", "PrinterDefaultProfileGuid", printerDefaultProfileGuid ?? "");

                SettingsHelper.ApplySettings(settings);
                SettingsHelper.SaveSettings();

                return new PrintFileStart(printFile, printerName);
            }

            if (ShouldCallInitialize(commandLineParser)) return new InitializeSettingsStart();

            var appStart = DetermineAppStart(commandLineParser);

            if (commandLineParser.HasArgument("ManagePrintJobs")) appStart.StartManagePrintJobs = true;

            return appStart;
        }

        private static bool ShouldCallInitialize(CommandLineParser commandLineParser)
        {
            if (!commandLineParser.HasArgument("InitializeSettings"))
                return false;

            var excludingArguments = new[] { "ManagePrintJobs", "InfoDataFile", "PsFile", "PdfFile", "Config" };

            return excludingArguments.All(argument => !commandLineParser.HasArgument(argument));
        }

        private MaybePipedStart DetermineAppStart(CommandLineParser commandLineParser)
        {
            // let's see if we have a new JobInfo passed as command line argument
            var newJob = FindJobInfoFile(commandLineParser);
            if (newJob != null)
            {
                return new NewPrintJobStart(newJob);
            }

            // or a PSFile?
            newJob = FindPsFile(commandLineParser);
            if (newJob != null)
            {
                var printerName = FindPrinterName(commandLineParser);
                return new NewPsJobStart(newJob, printerName);
            }

            // or a PdfFile?
            newJob = FindPdfFile(commandLineParser);
            if (newJob != null)
            {
                var printerName = FindPrinterName(commandLineParser);
                return new NewPsJobStart(newJob, printerName);
            }

            // ...nope!? We have a MainWindowStart
            return new MainWindowStart();
        }

        private string FindPrintFile(CommandLineParser commandLineParser)
        {
            return commandLineParser.GetArgument("PrintFile");
        }

        private string FindJobInfoFile(CommandLineParser commandLineParser)
        {
            string infFile;

            if (!commandLineParser.HasArgument("InfoDataFile"))
                return null;

            _logger.Info("Launched clawPDF with InfoDataFile parameter.");
            infFile = commandLineParser.GetArgument("InfoDataFile");

            _logger.Debug("Recevied \"" + infFile + "\" as command line parameter.");

            return infFile;
        }

        private string FindPsFile(CommandLineParser commandlineParser)
        {
            string psFile;

            if (!commandlineParser.HasArgument("PsFile"))
                return null;

            _logger.Info("Launched clawPDF with PsFile parameter.");
            psFile = commandlineParser.GetArgument("PsFile");

            _logger.Debug("Recevied \"" + psFile + "\" as command line parameter.");

            return psFile;
        }

        private string FindPdfFile(CommandLineParser commandlineParser)
        {
            string pdfFile;

            if (!commandlineParser.HasArgument("PdfFile"))
                return null;

            _logger.Info("Launched clawPDF with PdfFile parameter.");
            pdfFile = commandlineParser.GetArgument("PdfFile");

            _logger.Debug("Recevied \"" + pdfFile + "\" as command line parameter.");

            return pdfFile;
        }

        private string FindPrinterName(CommandLineParser commandLineParser)
        {
            if (commandLineParser.HasArgument("PrinterName"))
                return commandLineParser.GetArgument("PrinterName");

            return _appSettings.PrimaryPrinter;
        }

        private string FindProfileParameter(CommandLineParser commandLineParser)
        {
            if (commandLineParser.HasArgument("Profile"))
                return commandLineParser.GetArgument("Profile");

            return "";
        }

        private string FindConfigParameter(CommandLineParser commandLineParser)
        {
            if (commandLineParser.HasArgument("Config"))
                return commandLineParser.GetArgument("Config");

            return "";
        }
    }
}