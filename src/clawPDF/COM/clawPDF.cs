using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Threading;
using clawSoft.clawPDF.Utilities.Communication;

namespace clawSoft.clawPDF.COM
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("4400958B-7C3F-435A-B402-840DDE68A2CA")]
    public interface IClawPdf
    {
        Printers GetclawPdfPrinters { get; }
        bool IsInstanceRunning { get; }

        void PrintFile(string path);

        void AddFileToQueue(string path);

        void PrintFileSwitchingPrinters(string path, bool allowDefaultPrinterSwitch);
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("496229AE-E777-44EA-873D-4BC5AE0B3DC4")]
    [ProgId("clawPDF.clawPDFObj")]
    public class ClawPdfObj : IClawPdf
    {
        /// <summary>
        ///     Returns a new PrinterDevices object
        /// </summary>
        public Printers GetclawPdfPrinters => new Printers();

        /// <summary>
        ///     Checks if clawPDF is running
        /// </summary>
        public bool IsInstanceRunning => PipeServer.SessionServerInstanceRunning(ThreadManager.PipeName);

        /// <summary>
        ///     Prints a file to the clawPDF printer without switching the default printer.
        /// </summary>
        /// <param name="path">Path of the file to be printed</param>
        public void PrintFile(string path)
        {
            PrintFileSwitchingPrinters(path, false);
        }

        /// <summary>
        ///     Prints a file to the clawPDF printer, where the user of the COM Interface can decide,
        ///     if the default printer should be temporally changed or not. In case of not, the file
        ///     will not be printed.
        /// </summary>
        /// <param name="path">The path to the file that should be printed</param>
        /// <param name="allowDefaultPrinterSwitch">
        ///     If true, the default printer will be temporally changed,
        ///     otherwise not and the file will not be printed.
        /// </param>
        public void PrintFileSwitchingPrinters(string path, bool allowDefaultPrinterSwitch)
        {
            PathCheck(path);

            var printFileHelper = new PrintFileHelperCom();

            printFileHelper.AddFile(path);
            printFileHelper.AllowDefaultPrinterSwitch = allowDefaultPrinterSwitch;
            printFileHelper.PrintAll();
        }

        /// <summary>
        ///     Inserts a .ps or .pdf file directly into the queue.
        /// </summary>
        /// <param name="path">The path to the .ps or .pdf file</param>
        public void AddFileToQueue(string path)
        {
            PathCheck(path);

            var fileExtension = Path.GetExtension(path) ?? string.Empty;
            var queueInstance = JobInfoQueue.Instance;
            var legalFileTypes = new List<string> { ".ps", ".pdf" };

            if (!legalFileTypes.Contains(fileExtension.ToLower()))
                throw new COMException("Only .ps and .pdf files can be directly added to the queue.");

            var spoolFolder = queueInstance.SpoolFolder;

            if (!queueInstance.SpoolFolderIsAccessible())
                throw new COMException("Accessing the spool folder failed.");

            var infFile = PsFileHelper.TransformToInfFile(path, spoolFolder);

            queueInstance.Add(new JobInfo(infFile));
        }

        private void PathCheck(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new COMException("The specified path must not be empty or uninitiliazed.");

            if (!File.Exists(path))
                throw new COMException(
                    "File with such a path doesn't exist. Please check if the specified path is correct.");
        }
    }
}