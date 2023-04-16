using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using clawSoft.clawPDF.SetupHelper.Helper;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.SetupHelper.Driver
{
    internal class Actions
    {
        public static bool CheckIfPrinterNotInstalled()
        {
            bool resultCode;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.IsclawPDFPrinterInstalled())
                    resultCode = true;
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool AddPrinter(string name)
        {
            bool resultCode;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.AddCustomclawPDFPrinter(name))
                {
                    resultCode = true;
                    Spooler.stop();
                    Spooler.start();
                }
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool RemovePrinter(string name)
        {
            bool resultCode;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.DeleteCustomclawPDFPrinter(name))
                    resultCode = true;
                else
                    resultCode = false;
            }
            finally
            { }
            return resultCode;
        }

        public static bool IsRepairRequired()
        {
            var printerHelper = new PrinterHelper();
            return !printerHelper.GetclawPDFPrinters().Any();
        }

        public static void WaitForPrintSpooler()
        {
            ServiceController printSpooler = new ServiceController("Spooler");

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (printSpooler.Status != ServiceControllerStatus.Running && stopwatch.ElapsedMilliseconds < 120000)
            {
                printSpooler.Refresh();
                Thread.Sleep(3000);
            }

            stopwatch.Stop();

            if (printSpooler.Status != ServiceControllerStatus.Running)
            {
            }
        }

        public static bool InstallclawPDFPrinter()
        {
            bool printerInstalled;
            string clawmonpath;
            Utilities.OsHelper osHelper = new Utilities.OsHelper();
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (Environment.Is64BitOperatingSystem && !osHelper.IsArm64())
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\x64\";
                }
                else if (!Environment.Is64BitOperatingSystem && !osHelper.IsArm64())
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\x86\";
                }
                else if (osHelper.IsArm64())
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\arm64\";
                }
                else
                {
                    clawmonpath = Path.GetDirectoryName(Application.ExecutablePath) + @"\clawmon\x64\";
                }

                if (installer.InstallclawPDFPrinter(clawmonpath, "clawPDF.exe"))
                    printerInstalled = true;
                else
                    printerInstalled = false;
            }
            finally
            { }
            return printerInstalled;
        }

        public static bool UninstallclawPDFPrinter()
        {
            bool printerUninstalled;
            clawPDFInstaller installer = new clawPDFInstaller();
            try
            {
                if (installer.UninstallclawPDFPrinter())
                    printerUninstalled = true;
                else
                    printerUninstalled = false;
            }
            finally
            { }
            return printerUninstalled;
        }
    }
}