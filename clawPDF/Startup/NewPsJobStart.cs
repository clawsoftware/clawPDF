using System.Runtime.InteropServices;
using clawSoft.clawPDF.Shared.Helper;
using NLog;

namespace clawSoft.clawPDF.Startup
{
    internal class NewPsJobStart : MaybePipedStart
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private string _newInfFile;

        public NewPsJobStart(string newPsFile, string printerName = "clawPDF")
        {
            NewPsFile = newPsFile;
            PrinterName = printerName;
        }

        public string NewPsFile { get; }
        public string PrinterName { get; }

        private string NewInfFile
        {
            get
            {
                if (!string.IsNullOrEmpty(_newInfFile))
                    return _newInfFile;

                var spool = JobInfoQueue.Instance.SpoolFolder;
                _newInfFile = PsFileHelper.TransformToInfFile(NewPsFile, spool, PrinterName);

                if (string.IsNullOrEmpty(_newInfFile))
                    _newInfFile = "";

                return _newInfFile;
            }
        }

        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.U4)]
        public static extern int WTSGetActiveConsoleSessionId();

        internal override string ComposePipeMessage()
        {
            return "NewJob|" + NewInfFile;
        }

        internal override bool StartApplication()
        {
            if (string.IsNullOrEmpty(NewInfFile))
                return false;

            _logger.Debug("Adding new job.");
            JobInfoQueue.Instance.Add(NewInfFile);

            return true;
        }
    }
}