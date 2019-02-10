using System.Diagnostics;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Utilities;
using NLog;

namespace clawSoft.clawPDF.Core.Actions
{
    /// <summary>
    ///     DefaultViewerAction opens the output files in the default viewer
    /// </summary>
    public class DefaultViewerAction : IAction
    {
        private const int ActionId = 10;
        protected static Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Creates a new default viewer action.
        /// </summary>
        /// <param name="firstFileOnly">
        ///     If true, only the first output file will be opened. If false, all files will be openend
        ///     indidually
        /// </param>
        public DefaultViewerAction(bool firstFileOnly)
        {
            FirstFileOnly = firstFileOnly;
        }

        /// <summary>
        ///     Gets whether only the first output file is opened. If false, all files will be opened
        /// </summary>
        public bool FirstFileOnly { get; }

        /// <summary>
        ///     Open all files in the default viewer
        /// </summary>
        /// <param name="job">Job information</param>
        /// <returns>An ActionResult to determine the success and a list of errors</returns>
        public ActionResult ProcessJob(IJob job)
        {
            Logger.Debug("Launched Viewer-Action");

            var isPdfFile = job.Profile.OutputFormat == OutputFormat.Pdf ||
                            job.Profile.OutputFormat == OutputFormat.PdfA1B ||
                            job.Profile.OutputFormat == OutputFormat.PdfA2B ||
                            job.Profile.OutputFormat == OutputFormat.PdfX;

            if (!isPdfFile)
                return OpenJobOutput(job);

            if (!FileUtil.Instance.FileAssocHasOpen(".pdf")) Logger.Error("No program associated with pdf.");

            return OpenJobOutput(job);
        }

        private ActionResult OpenJobOutput(IJob job)
        {
            Logger.Trace("Open file(s) with default programm");
            try
            {
                if (FirstFileOnly)
                {
                    Process.Start(job.OutputFiles[0]);
                    Logger.Trace("Openend (only first) file: " + job.OutputFiles[0]);
                }
                else
                {
                    foreach (var file in job.OutputFiles)
                    {
                        Process.Start(file);
                        Logger.Trace("Openend file: " + file);
                    }
                }
            }
            catch
            {
                Logger.Error("File could not be opened.");
                return new ActionResult(ActionId, 101);
            }

            return new ActionResult();
        }
    }
}