using System;
using System.Text;
using System.Threading;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Ghostscript;
using clawSoft.clawPDF.Core.Ghostscript.OutputDevices;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.PDFProcessing;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Core.Jobs
{
    /// <summary>
    ///     The GhostscriptJob class processes a Job with Ghostscript.
    /// </summary>
    public class GhostscriptJob : AbstractJob
    {
        private readonly GhostScript _ghostScript;
        private readonly StringBuilder _ghostscriptOutput = new StringBuilder();

        /// <summary>
        ///     Create a new GhostscriptJob instance
        /// </summary>
        /// <param name="jobInfo">JobInfo of the job to convert</param>
        /// <param name="profile">Profile that determines the conversion process</param>
        /// <param name="tempFolder">
        ///     TempFolderProvider that gives the full Temp path, i.e. C:\Users\Admin\Local
        ///     Settings\Temp\clawPDF
        /// </param>
        /// <param name="jobTranslations">Translations needed for the job</param>
        public GhostscriptJob(IJobInfo jobInfo, ConversionProfile profile, ITempFolderProvider tempFolder,
            JobTranslations jobTranslations)
            : this(jobInfo, profile, jobTranslations, tempFolder, new FileWrap(), new DirectoryWrap())
        {
        }

        /// <summary>
        ///     Create a new GhostscriptJob instance
        /// </summary>
        /// <param name="jobInfo">JobInfo of the job to convert</param>
        /// <param name="profile">Profile that determines the conversion process</param>
        /// <param name="tempFolder">
        ///     TempFolderProvider that gives the full Temp path, i.e. C:\Users\Admin\Local
        ///     Settings\Temp\clawPDF
        /// </param>
        /// <param name="jobTranslations">Translations needed for the job</param>
        /// <param name="fileWrap">Only for testing</param>
        /// <param name="directoryWrap">Only for testing</param>
        public GhostscriptJob(IJobInfo jobInfo, ConversionProfile profile, JobTranslations jobTranslations,
            ITempFolderProvider tempFolder, IFile fileWrap, IDirectory directoryWrap)
            : base(jobInfo, profile, jobTranslations, fileWrap, directoryWrap)
        {
            var gsVersion = new GhostscriptDiscovery().GetBestGhostscriptInstance();
            if (gsVersion == null)
            {
                Logger.Error("No valid Ghostscript version found.");
                throw new InvalidOperationException("No valid Ghostscript version found.");
            }

            Logger.Debug("Ghostscript Version: " + gsVersion.Version + " loaded from " + gsVersion.DllPath);
            _ghostScript = new GhostScript(gsVersion);

            JobTempFolder = PathSafe.Combine(tempFolder.TempFolder,
                "Job_" + PathSafe.GetFileNameWithoutExtension(PathSafe.GetRandomFileName()));
            JobTempOutputFolder = PathSafe.Combine(JobTempFolder, "tempoutput");
            DirectoryWrap.CreateDirectory(JobTempFolder);
            DirectoryWrap.CreateDirectory(JobTempOutputFolder);

            // Shorten the temp folder for GS compatibility
            JobTempFolder = JobTempFolder;
        }

        private string GhostscriptOutput => _ghostscriptOutput.ToString();

        /// <summary>
        ///     Apply all Actions according to the configuration
        /// </summary>
        protected virtual void SetUpActions()
        {
            // it does not work yet...
            /*if (Profile.Stamping.Enable)
            {
                StampPdfAction stamp = new StampPdfAction(Profile.Stamping.StampText, Profile.Stamping.FontName);
                JobActions.Add(stamp);
            }*/

            if (Profile.Scripting.Enabled)
            {
                AddAction(new ScriptAction());
                Logger.Trace("Script-Action added");
            }

            if (Profile.OpenViewer)
            {
                var defaultViewerAction = new DefaultViewerAction(true);
                AddAction(defaultViewerAction);

                Logger.Trace("Viewer-Action added");
            }

            if (Profile.Printing.Enabled)
            {
                AddAction(new PrintingAction(_ghostScript));
                Logger.Trace("Print-Action added");
            }

            if (Profile.EmailSmtp.Enabled)
            {
                var smtpMailAction = new SmtpMailAction(JobTranslations.EmailSignature);
                smtpMailAction.QueryRetypeSmtpPassword += OnRetypeSmtpPassword;
                AddAction(smtpMailAction);
                Logger.Trace("SMTP-Mail-Action added");
            }

            if (Profile.EmailClient.Enabled)
            {
                var eMailClientAction = new EMailClientAction(JobTranslations.EmailSignature);
                AddAction(eMailClientAction);
                Logger.Trace("EMail-Client-Action added");
            }

            if (Profile.Ftp.Enabled)
            {
                var ftpAction = new FtpAction();
                AddAction(ftpAction);
                Logger.Trace("Ftp-Action added");
            }

            /*if (Profile.AttachMe.Enable)
            {
                var attachMeAction = new AttachMeAction();
                AddAction(attachMeAction);
                Logger.Trace("Attach.Me added");
            }*/
        }

        /// <summary>
        ///     Run the Job
        /// </summary>
        protected override JobState RunJobWork()
        {
            try
            {
                if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
                    Thread.CurrentThread.Name = "JobWorker";
            }
            catch (InvalidOperationException)
            {
            }

            try
            {
                _ghostScript.Output += Ghostscript_Output;
                OnJobCompleted += (sender, args) => _ghostScript.Output -= Ghostscript_Output;

                OutputFiles.Clear();

                Logger.Trace("Setting up actions");
                SetUpActions();

                Logger.Debug("Starting Ghostscript Job");

                OutputDevice device;
                switch (Profile.OutputFormat)
                {
                    case OutputFormat.PdfA1B:
                    case OutputFormat.PdfA2B:
                    case OutputFormat.PdfX:
                    case OutputFormat.Pdf:
                        device = new PdfDevice(this);
                        break;

                    case OutputFormat.Png:
                        device = new PngDevice(this);
                        break;

                    case OutputFormat.Jpeg:
                        device = new JpegDevice(this);
                        break;

                    case OutputFormat.Tif:
                        device = new TiffDevice(this);
                        break;

                    case OutputFormat.Txt:
                        device = new TextDevice(this);
                        break;

                    default:
                        throw new Exception("Illegal OutputFormat specified");
                }

                Logger.Trace("Output format is: {0}", Profile.OutputFormat.ToString());

                _ghostScript.Output += Ghostscript_Logging;
                var success = _ghostScript.Run(device, JobTempFolder);
                _ghostScript.Output -= Ghostscript_Logging;

                Logger.Trace("Finished Ghostscript execution");

                if (!success)
                {
                    var errorMessage = ExtractGhostscriptErrors(GhostscriptOutput);
                    Logger.Error("Ghostscript execution failed: " + errorMessage);
                    ErrorMessage = errorMessage;

                    JobState = JobState.Failed;
                    return JobState;
                }

                ProcessOutput();

                Logger.Trace("Moving output files to final location");
                MoveOutputFiles();

                Logger.Trace("Finished Ghostscript Job");
                JobState = JobState.Succeeded;
                return JobState;
            }
            catch (ProcessingException)
            {
                JobState = JobState.Failed;
                throw;
            }
            catch (DeviceException)
            {
                JobState = JobState.Failed;
                throw;
            }
            catch (Exception ex)
            {
                JobState = JobState.Failed;
                Logger.Error("There was an error while converting the Job {0}: {1}", JobInfo.InfFile, ex);
                throw;
            }
        }

        public static string ExtractGhostscriptErrors(string ghostscriptOutput)
        {
            var lines = ghostscriptOutput.Split('\n');

            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                if (line.StartsWith("GPL Ghostscript"))
                    continue;

                if (line.StartsWith("Copyright (C)"))
                    continue;

                if (line.StartsWith("This software comes with NO WARRANTY"))
                    continue;

                if (line.StartsWith("Loading"))
                    continue;

                if (line.StartsWith("%%"))
                    continue;

                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Process Ghostscript output to detect the progress
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event Arguments</param>
        private void Ghostscript_Output(object sender, OutputEventArgs e)
        {
            var output = e.Output;

            const string pageMarker = "[Page: ";
            if (output.Contains("[LastPage]"))
            {
                ReportProgress(100);
            }
            else if (output.Contains(pageMarker))
            {
                var start = output.LastIndexOf(pageMarker, StringComparison.Ordinal);
                var end = output.IndexOf("]", start, StringComparison.Ordinal);
                if (start >= 0 && end > start)
                {
                    start += pageMarker.Length;
                    var page = output.Substring(start, end - start);
                    int pageNumber;

                    if (int.TryParse(page, out pageNumber))
                        if (pageNumber <= NumberOfPages)
                            ReportProgress(pageNumber * 100 / NumberOfPages);
                }
            }
        }

        /// <summary>
        ///     Process Ghostscript output to provide logging
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event Arguments</param>
        private void Ghostscript_Logging(object sender, OutputEventArgs e)
        {
            _ghostscriptOutput.Append(e.Output);

            Logger.Debug(e.Output.TrimEnd('\r', '\n'));
        }

        public override event EventHandler<QueryPasswordEventArgs> OnRetypeSmtpPassword;

        private void ProcessOutput()
        {
            if (!PDFProcessor.ProcessingRequired(Profile))
                return;

            PDFProcessor.ProcessPDF(TempOutputFiles[0], Profile, Passwords);
        }
    }
}