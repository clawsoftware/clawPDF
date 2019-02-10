using System;
using System.Threading;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Helper;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Exceptions;
using clawSoft.clawPDF.PDFProcessing;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Threading;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.Threading;
using clawSoft.clawPDF.Views;
using NLog;

namespace clawSoft.clawPDF.Workflow
{
    /// <summary>
    ///     Defines the different stats the workflow can be in
    /// </summary>
    internal enum WorkflowStep
    {
        Init,
        SelectTarget,
        SetSecurityPasswords,
        SetSignaturePassword,
        SetSmtpPassword,
        SetFtpPassword,
        Convert,
        AbortedByUser,
        Finished
    }

    /// <summary>
    ///     The ConversionWorkflow class handles all user interaction that is required to convert a PostScript file.
    /// </summary>
    internal abstract class ConversionWorkflow
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected bool Cancel;
        protected DirectoryHelper DirectoryHelper;

        /// <summary>
        ///     Settings for the conversion process
        /// </summary>
        public clawPDFSettings Settings { get; set; }

        /// <summary>
        ///     JobInfo of the current job
        /// </summary>
        public IJobInfo JobInfo { get; protected set; }

        /// <summary>
        ///     The job that is created during the workflow
        /// </summary>
        public IJob Job { get; protected set; }

        /// <summary>
        ///     The step the workflow currently is in
        /// </summary>
        public WorkflowStep WorkflowStep { get; protected set; }

        /// <summary>
        ///     Query and set the location where the files will be created. This may include showing a Dialog to the user, if
        ///     appropriate.
        ///     The implementation must set Job.JobInfo.OutputFilenameTemplate and Job.JobInfo.OutputFormat
        /// </summary>
        protected abstract void QueryTargetFile();

        /// <summary>
        ///     Query and set passwords for PDF encryption. This may include showing a Dialog to the user, if appropriate.
        ///     The implementation must set Job.PdfPasswords.OwnerPassword and Job.PdfPasswords.UserPassword
        /// </summary>
        protected abstract bool QueryEncryptionPasswords();

        /// <summary>
        ///     Query and set passwords for the PDF signature. This may include showing a Dialog to the user, if appropriate.
        ///     The implementation must set Job.PdfPasswords.SignaturePassword
        /// </summary>
        protected abstract bool QuerySignaturePassword();

        /// <summary>
        ///     Get the delegate that handles the requests to query the mail password during the conversion
        /// </summary>
        /// <returns>A QueryMailPassword delegate that handles everything</returns>
        protected abstract bool QueryEmailSmtpPassword();

        /// <summary>
        ///     Get the delegate that handles the requests to query the mail password during the conversion
        /// </summary>
        /// <returns>A QueryMailPassword delegate that handles everything</returns>
        protected abstract bool QueryFtpPassword();

        /// <summary>
        ///     If the Job failed, this method is called to notify the user about the error
        /// </summary>
        protected abstract void NotifyUserAboutFailedJob();

        public event EventHandler JobFinished;

        /// <summary>
        ///     Runs all steps and user interaction that is required during the conversion
        /// </summary>
        public void RunWorkflow()
        {
            try
            {
                DoWorkflowWork();
                CleanUp();
            }
            catch (ProcessingException ex)
            {
                Logger.Error("Error " + ex.ErrorCode + ": " + ex.Message);
                EvaluateActionResult(new ActionResult(ex.ErrorCode));
            }
            catch (ManagePrintJobsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void DoWorkflowWork()
        {
            WorkflowStep = WorkflowStep.Init;

            Logger.Debug("Starting conversion...");
            Logger.Debug("clawPDF Version: " + VersionHelper.Instance.FormatWithBuildNumber());
            Logger.Debug("OSVersion: " + new OsHelper().GetWindowsVersion());

            var originalMetadata = JobInfo.Metadata.Copy();
            Job.InitMetadata();

            Job.OnEvaluateActionResult += EvaluateActionResult;
            Job.OnRetypeSmtpPassword += RetypeSmtpPassword;

            WorkflowStep = WorkflowStep.SelectTarget;
            Logger.Debug("Querying the place to save the file");

            try
            {
                QueryTargetFile();
            }
            catch (ManagePrintJobsException)
            {
                // revert metadata changes and rethrow exception
                JobInfo.Metadata = originalMetadata;
                throw;
            }

            if (Cancel)
                return;

            var preCheck = ProfileChecker.ProfileCheck(Job.Profile);
            if (!EvaluateActionResult(preCheck))
                return;

            Logger.Debug("Output filename template is: {0}", Job.OutputFilenameTemplate);
            Logger.Debug("Output format is: {0}", Job.Profile.OutputFormat);

            if (!SetActions())
                return;

            WorkflowStep = WorkflowStep.Convert;

            Logger.Info("Converting " + Job.OutputFilenameTemplate);

            if (Job.Profile.ShowProgress)
                ShowConversionProgress();

            Job.RunJob();

            if (!Job.Success) NotifyUserAboutFailedJob();

            WorkflowStep = WorkflowStep.Finished;
            OnJobFinished(EventArgs.Empty);
        }

        private void OnJobFinished(EventArgs e)
        {
            var handler = JobFinished;
            if (handler != null) handler(this, e);
        }

        private void CleanUp()
        {
            Job.CleanUp();
            if (DirectoryHelper != null)
                DirectoryHelper.DeleteCreatedDirectories();
        }

        private bool SetActions()
        {
            // Skip Security and Signature if OutputFormat is not PDF
            if (Job.Profile.OutputFormat == OutputFormat.Pdf
                || Job.Profile.OutputFormat == OutputFormat.PdfA1B
                || Job.Profile.OutputFormat == OutputFormat.PdfA2B
                || Job.Profile.OutputFormat == OutputFormat.PdfX)
            {
                if (Job.Profile.PdfSettings.Security.Enabled)
                {
                    WorkflowStep = WorkflowStep.SetSecurityPasswords;

                    Logger.Debug("Querying encryption passwords");
                    if (!QueryEncryptionPasswords() || Cancel)
                    {
                        Logger.Warn("Canceled setting encryption passwords. No PDF will be created.");
                        return false;
                    }
                }

                if (Job.Profile.PdfSettings.Signature.Enabled)
                {
                    WorkflowStep = WorkflowStep.SetSignaturePassword;

                    Logger.Debug("Querying signature password");
                    if (!QuerySignaturePassword() || Cancel)
                    {
                        Logger.Error("Canceled setting password for the digital signature. No PDF will be created.");
                        return false;
                    }
                }
            }

            if (Job.Profile.EmailSmtp.Enabled)
            {
                WorkflowStep = WorkflowStep.SetSmtpPassword;

                Logger.Debug("Querying SMTP password");
                if (!QueryEmailSmtpPassword() || Cancel)
                {
                    Logger.Error("Canceled setting password for email over SMTP. No PDF will be created.");
                    return false;
                }
            }

            if (Job.Profile.Ftp.Enabled)
            {
                WorkflowStep = WorkflowStep.SetFtpPassword;

                Logger.Debug("Querying FTP password");
                if (!QueryFtpPassword() || Cancel)
                {
                    Logger.Error("Canceled setting password for email over SMTP. No PDF will be created.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        ///     Show something to the user (if desired) while the conversion is going on
        /// </summary>
        protected virtual void ShowConversionProgress()
        {
            var progressWindowThread = new SynchronizedThread(ShowConversionProgressDialog);
            progressWindowThread.SetApartmentState(ApartmentState.STA);

            progressWindowThread.Name = "ProgressForm";

            ThreadManager.Instance.StartSynchronizedThread(progressWindowThread);
        }

        [STAThread]
        private void ShowConversionProgressDialog()
        {
            var conversionStatusForm = new ConversionProgressWindow();

            conversionStatusForm.ApplyJob(Job);
            TopMostHelper.ShowDialogTopMost(conversionStatusForm, true);
        }

        /// <summary>
        ///     Function to evaluate action result with the contained success status and error code.
        /// </summary>
        /// <param name="actionResult">action result</param>
        /// <returns>true if proceed with further actions, else cancel process</returns>
        protected abstract bool EvaluateActionResult(ActionResult actionResult);

        protected abstract void RetypeSmtpPassword(object sender, QueryPasswordEventArgs e);
    }
}