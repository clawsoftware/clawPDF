using System.IO;
using System.Windows.Forms;
using clawSoft.clawPDF.Core;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Exceptions;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.IO;
using clawSoft.clawPDF.ViewModels;
using clawSoft.clawPDF.Views;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Workflow
{
    /// <summary>
    ///     The interactive workflow implements the workflow steps where user interaction is required.
    /// </summary>
    internal class InteractiveWorkflow : ConversionWorkflow
    {
        private readonly IPathSafe _pathSafe = new PathWrapSafe();
        private readonly Translator _translator = TranslationHelper.Instance.TranslatorInstance;

        /// <summary>
        ///     Create a new Workflow object with the given job info
        /// </summary>
        /// <param name="job">Job to use for the conversion</param>
        /// <param name="settings">Settigns to use during the conversion workflow</param>
        public InteractiveWorkflow(IJob job, clawPDFSettings settings)
        {
            WorkflowStep = WorkflowStep.Init;

            job.OnActionAdded += job_OnActionAdded;
            job.OnRetypeOutputFilename += RetypeOutputFilename;

            JobInfo = job.JobInfo;
            Job = job;
            Settings = settings;
        }

        protected override void QueryTargetFile()
        {
            if (!Job.Profile.SkipPrintDialog)
            {
                Job.ApplyMetadata();
                var w = new PrintJobWindow();
                var model = new PrintJobViewModel(Job.JobInfo, Job.Profile);
                w.DataContext = model;

                if (TopMostHelper.ShowDialogTopMost(w, true) != true || model.PrintJobAction == PrintJobAction.Cancel)
                {
                    Cancel = true;
                    WorkflowStep = WorkflowStep.AbortedByUser;
                    return;
                }

                if (model.PrintJobAction == PrintJobAction.ManagePrintJobs)
                    throw new ManagePrintJobsException();

                Job.Profile = model.SelectedProfile.Copy();
                Job.ApplyMetadata();

                if (model.PrintJobAction == PrintJobAction.EMail)
                {
                    Job.SkipSaveFileDialog = true;
                    Job.Profile.EmailClient.Enabled = true;
                    Job.Profile.AutoSave.Enabled = false;
                    Job.Profile.OpenViewer = false;
                }
            }

            if (Job.SkipSaveFileDialog)
            {
                ITempFolderProvider tempFolderProvider = JobInfoQueue.Instance;

                var sendFilesFolder = _pathSafe.Combine(tempFolderProvider.TempFolder,
                    "Job" + Job.JobInfo.SourceFiles[0].JobId + "_SendFiles");
                Directory.CreateDirectory(sendFilesFolder);
                var filePath = _pathSafe.Combine(sendFilesFolder, Job.ComposeOutputFilename());
                filePath = FileUtil.Instance.EllipsisForTooLongPath(filePath);
                Job.OutputFilenameTemplate = filePath;
            }
            else
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title =
                    _translator.GetTranslation("InteractiveWorkflow", "SelectDestination", "Select destination");
                saveFileDialog.Filter = _translator.GetTranslation("InteractiveWorkflow", "PdfFile", "PDF file") +
                                        @" (*.pdf)|*.pdf";
                saveFileDialog.Filter +=
                    @"|" + _translator.GetTranslation("InteractiveWorkflow", "PdfA1bFile", "PDF/A-1b file") +
                    @" (*.pdf)|*.pdf";
                saveFileDialog.Filter +=
                    @"|" + _translator.GetTranslation("InteractiveWorkflow", "PdfA2bFile", "PDF/A-2b file") +
                    @" (*.pdf)|*.pdf";
                saveFileDialog.Filter +=
                    @"|" + _translator.GetTranslation("InteractiveWorkflow", "PdfXFile", "PDF/X file") +
                    @" (*.pdf)|*.pdf";
                saveFileDialog.Filter +=
                    @"|" + _translator.GetTranslation("InteractiveWorkflow", "JpegFile", "JPEG file") +
                    @" (*.jpg)|*.jpg;*.jpeg;";
                saveFileDialog.Filter +=
                    @"|" + _translator.GetTranslation("InteractiveWorkflow", "PngFile", "PNG file") +
                    @" (*.png)|*.png;";
                saveFileDialog.Filter +=
                    @"|" + _translator.GetTranslation("InteractiveWorkflow", "TiffFile", "TIFF file") +
                    @" (*.tif)|*.tif;*.tiff";
                saveFileDialog.Filter +=
                    @"|" + _translator.GetTranslation("InteractiveWorkflow", "TextFile", "Text file") +
                    @" (*.txt)|*.txt;";

                saveFileDialog.FilterIndex = (int)Job.Profile.OutputFormat + 1;
                saveFileDialog.OverwritePrompt = true;

                if (Job.Profile.SaveDialog.SetDirectory)
                {
                    var saveDirectory =
                        FileUtil.Instance.MakeValidFolderName(
                            Job.TokenReplacer.ReplaceTokens(Job.Profile.SaveDialog.Folder));
                    DirectoryHelper = new DirectoryHelper(saveDirectory);
                    if (DirectoryHelper.CreateDirectory())
                    {
                        saveFileDialog.RestoreDirectory = true;
                        saveFileDialog.InitialDirectory = saveDirectory;
                        Logger.Debug("Set directory in save file dialog: " + saveDirectory);
                    }
                    else
                    {
                        Logger.Warn(
                            "Could not create directory for save file dialog. It will be opened with default save location.");
                    }
                }

                Cancel = !LaunchSaveFileDialog(saveFileDialog);
            }
        }

        /// <summary>
        ///     Sets the job's filenametemplate and extension by savefiledialog.
        ///     Recursive call of the savefile dialog if filename (+path) is to long.
        /// </summary>
        /// <param name="saveFileDialog">saveFileDialiog</param>
        /// <returns>false if user cancels savefiledialog</returns>
        private bool LaunchSaveFileDialog(SaveFileDialog saveFileDialog)
        {
            var tmpFile = Job.ComposeOutputFilename();

            saveFileDialog.FileName = tmpFile;

            var result = TopMostHelper.ShowDialogTopMost(saveFileDialog, !Job.Profile.SkipPrintDialog);

            if (result != DialogResult.OK)
            {
                Logger.Info("Cancelled the save dialog. No file will be created.");
                WorkflowStep = WorkflowStep.AbortedByUser;
                return false;
            }

            Job.Profile.OutputFormat = (OutputFormat)saveFileDialog.FilterIndex - 1;

            try
            {
                var outputFile = saveFileDialog.FileName;
                if (!OutputFormatHelper.HasValidExtension(outputFile, Job.Profile.OutputFormat))
                    outputFile = OutputFormatHelper.EnsureValidExtension(outputFile, Job.Profile.OutputFormat);

                Job.OutputFilenameTemplate = outputFile;
                return true;
            }
            catch (PathTooLongException)
            {
                Logger.Error("Filename (+ path) from savefile dialog is too long.");
                var message = _translator.GetTranslation("InteractiveWorkflow", "SelectedPathTooLong",
                    "The total length of path and filename is too long.\r\nPlease use a shorter name.");
                var caption =
                    _translator.GetTranslation("InteractiveWorkflow", "SelectDestination", "Select destination");
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Warning);
                return LaunchSaveFileDialog(saveFileDialog);
            }
        }

        protected override bool QueryEncryptionPasswords()
        {
            Job.Passwords.PdfOwnerPassword = Job.Profile.PdfSettings.Security.OwnerPassword;
            Job.Passwords.PdfUserPassword = Job.Profile.PdfSettings.Security.UserPassword;

            var askOwnerPw = string.IsNullOrEmpty(Job.Profile.PdfSettings.Security.OwnerPassword);
            var askUserPw = Job.Profile.PdfSettings.Security.RequireUserPassword &&
                            string.IsNullOrEmpty(Job.Profile.PdfSettings.Security.UserPassword);

            if (askOwnerPw || askUserPw) //overwrite values with value from form
            {
                var pwWindow =
                    new EncryptionPasswordsWindow(EncryptionPasswordMiddleButton.Skip, askOwnerPw, askUserPw);

                pwWindow.ShowDialogTopMost();

                if (pwWindow.Response == EncryptionPasswordResponse.OK)
                {
                    if (askOwnerPw)
                        Job.Passwords.PdfOwnerPassword = pwWindow.OwnerPassword;
                    if (askUserPw)
                        Job.Passwords.PdfUserPassword = pwWindow.UserPassword;
                }
                else if (pwWindow.Response == EncryptionPasswordResponse.Skip)
                {
                    Job.Profile.PdfSettings.Security.Enabled = false;
                    Logger.Info("User skipped encryption password dialog. Encryption disabled.");
                    return true;
                }
                else
                {
                    Cancel = true;
                    Logger.Warn("Cancelled the PDF password dialog. No PDF will be created.");
                    WorkflowStep = WorkflowStep.AbortedByUser;
                    return false;
                }
            }

            return true;
        }

        protected override bool QuerySignaturePassword()
        {
            if (!string.IsNullOrEmpty(Job.Profile.PdfSettings.Signature.SignaturePassword))
            {
                Job.Passwords.PdfSignaturePassword = Job.Profile.PdfSettings.Signature.SignaturePassword;
                return true;
            }

            var passwordWindow = new SignaturePasswordWindow(PasswordMiddleButton.Skip,
                Job.Profile.PdfSettings.Signature.CertificateFile);

            TopMostHelper.ShowDialogTopMost(passwordWindow, true);

            if (passwordWindow.SignaturePasswordViewModel.Result == SignaturePasswordResult.StorePassword)
            {
                Job.Passwords.PdfSignaturePassword = passwordWindow.Password;
                return true;
            }

            if (passwordWindow.SignaturePasswordViewModel.Result == SignaturePasswordResult.Skip)
            {
                Job.Profile.PdfSettings.Signature.Enabled = false;
                Logger.Info("User skipped Signature Password. Signing disabled.");
                return true;
            }

            Cancel = true;
            Logger.Warn("Cancelled the signature password dialog. No PDF will be created.");
            WorkflowStep = WorkflowStep.AbortedByUser;
            return false;
        }

        protected override bool QueryEmailSmtpPassword()
        {
            if (!string.IsNullOrEmpty(Job.Profile.EmailSmtp.Password))
            {
                Job.Passwords.SmtpPassword = Job.Profile.EmailSmtp.Password;
                return true;
            }

            var pwWindow = new SmtpPasswordWindow(SmtpPasswordMiddleButton.Skip, Job.Profile.EmailSmtp.Address,
                Job.Profile.EmailSmtp.Recipients);

            pwWindow.ShowDialogTopMost();

            if (pwWindow.Response == SmtpPasswordResponse.OK)
            {
                Job.Passwords.SmtpPassword = pwWindow.SmtpPassword;
                return true;
            }

            if (pwWindow.Response == SmtpPasswordResponse.Skip)
            {
                Job.Profile.EmailSmtp.Enabled = false;
                Logger.Info("User skipped Smtp Password. Smtp Email disabled.");
                return true;
            }

            Cancel = true;
            Logger.Warn("Cancelled the SMTP dialog. No PDF will be created.");
            WorkflowStep = WorkflowStep.AbortedByUser;
            return false;
        }

        protected override bool QueryFtpPassword()
        {
            if (!string.IsNullOrEmpty(Job.Profile.Ftp.Password))
            {
                Job.Passwords.FtpPassword = Job.Profile.Ftp.Password;
                return true;
            }

            var pwWindow = new FtpPasswordWindow(FtpPasswordMiddleButton.Skip);
            pwWindow.ShowDialogTopMost();

            if (pwWindow.Response == FtpPasswordResponse.OK)
            {
                Job.Passwords.FtpPassword = pwWindow.FtpPassword;
                return true;
            }

            if (pwWindow.Response == FtpPasswordResponse.Skip)
            {
                Job.Profile.PdfSettings.Signature.Enabled = false;
                Logger.Info("User skipped ftp password. Ftp upload disabled.");
                return true;
            }

            Cancel = true;
            Logger.Warn("Cancelled the FTP password dialog. No PDF will be created.");
            WorkflowStep = WorkflowStep.AbortedByUser;
            return false;
        }

        protected override void NotifyUserAboutFailedJob()
        {
            string errorText;

            switch (Job.ErrorType)
            {
                case JobError.Ghostscript:
                    errorText = _translator.GetTranslation("InteractiveWorkflow", "GhostscriptError",
                        "Internal Ghostscript error");
                    break;

                default:
                    errorText = _translator.GetTranslation("InteractiveWorkflow", "UnknownError",
                        "Unkown internal error");
                    break;
            }

            var caption = _translator.GetTranslation("InteractiveWorkflow", "Error", "Error");
            var opener = _translator.GetFormattedTranslation("InteractiveWorkflow", "ErrorWhileConverting",
                "clawPDF was not able to convert the document, because an error occured:\r\n{0}\r\n\r\nYou can find additional information in the log file.",
                errorText);

            MessageWindow.ShowTopMost(opener, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
        }

        protected override bool EvaluateActionResult(ActionResult actionResult)
        {
            if (actionResult.Success)
                return true;
            var caption = _translator.GetTranslation("InteractiveWorkflow", "Error", "Error");
            var opener = _translator.GetTranslation("InteractiveWorkflow", "AnErrorOccured", "An error occured:");
            var errorText = ErrorCodeInterpreter.GetErrorText(actionResult[0], true);
            MessageWindow.ShowTopMost(opener + "\r\n" + errorText, caption, MessageWindowButtons.OK,
                MessageWindowIcon.Error);

            return actionResult.Success;
        }

        protected override void RetypeSmtpPassword(object sender, QueryPasswordEventArgs e)
        {
            Logger.Debug("Launched E-mail password Form");
            var pwWindow = new SmtpPasswordWindow(SmtpPasswordMiddleButton.None, Job.Profile.EmailSmtp.Address,
                Job.Profile.EmailSmtp.Recipients);
            pwWindow.SmtpPassword = Job.Passwords.SmtpPassword;
            pwWindow.Message = _translator.GetTranslation("InteractiveWorkflow", "RetypeSmtpPwMessage",
                "Could not authenticate at server.\r\nPlease check your password and verify that you have a working internet connection.");

            if (pwWindow.ShowDialogTopMost() == SmtpPasswordResponse.OK)
            {
                Job.Passwords.SmtpPassword = pwWindow.SmtpPassword;
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                Logger.Warn("Cancelled the SMTP dialog. No PDF will be created.");
                WorkflowStep = WorkflowStep.AbortedByUser;
            }
        }

        private void RetypeOutputFilename(object sender, QueryFilenameEventArgs e)
        {
            var caption = "clawPDF";
            var message = e.Job.OutputFilenameTemplate + "\r\n";
            message += _translator.GetTranslation("InteractiveWorkflow", "RetypeFilenameMessage",
                "The file could not be saved. Maybe the file is currently in use or you do not have the required permissions.\r\nPlease select another filename and try again.");
            MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Warning);

            var saveFileDialog = CreateRetypeFilenameSaveDialog(e.Job);

            if (!LaunchSaveFileForRetyping(saveFileDialog))
            {
                Logger.Debug("User cancelled retyping filename");
                e.Cancelled = true;
            }
        }

        private SaveFileDialog CreateRetypeFilenameSaveDialog(IJob job)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title =
                _translator.GetTranslation("InteractiveWorkflow", "SelectDestination", "Select destination");

            switch (job.Profile.OutputFormat)
            {
                case OutputFormat.Pdf:
                    saveFileDialog.Filter = _translator.GetTranslation("InteractiveWorkflow", "PdfFile", "PDF file") +
                                            @" (*.pdf)|*.pdf";
                    break;

                case OutputFormat.PdfA1B:
                    saveFileDialog.Filter =
                        _translator.GetTranslation("InteractiveWorkflow", "PdfA1bFile", "PDF/A-1b file") +
                        @" (*.pdf)|*.pdf";
                    break;

                case OutputFormat.PdfA2B:
                    saveFileDialog.Filter =
                        _translator.GetTranslation("InteractiveWorkflow", "PdfA2bFile", "PDF/A-2b file") +
                        @" (*.pdf)|*.pdf";
                    break;

                case OutputFormat.PdfX:
                    saveFileDialog.Filter =
                        _translator.GetTranslation("InteractiveWorkflow", "PdfXFile", "PDF/X file") + @" (*.pdf)|*.pdf";
                    break;

                case OutputFormat.Jpeg:
                    saveFileDialog.Filter = _translator.GetTranslation("InteractiveWorkflow", "JpegFile", "JPEG file") +
                                            @" (*.jpg)|*.jpg;*.jpeg;";
                    break;

                case OutputFormat.Png:
                    saveFileDialog.Filter = _translator.GetTranslation("InteractiveWorkflow", "PngFile", "PNG file") +
                                            @" (*.png)|*.png;";
                    break;

                case OutputFormat.Tif:
                    saveFileDialog.Filter = _translator.GetTranslation("InteractiveWorkflow", "TiffFile", "TIFF file") +
                                            @" (*.tif)|*.tif;*.tiff";
                    break;

                case OutputFormat.Txt:
                    saveFileDialog.Filter = _translator.GetTranslation("InteractiveWorkflow", "TextFile", "Text file") +
                                            @" (*.txt)|*.txt;";
                    break;
            }

            saveFileDialog.FilterIndex = 1;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = Path.GetFileName(job.OutputFilenameTemplate);
            saveFileDialog.InitialDirectory = Path.GetDirectoryName(job.OutputFilenameTemplate);

            return saveFileDialog;
        }

        private bool LaunchSaveFileForRetyping(SaveFileDialog saveFileDialog)
        {
            var result = TopMostHelper.ShowDialogTopMost(saveFileDialog, !Job.Profile.SkipPrintDialog);

            if (result != DialogResult.OK)
            {
                Logger.Info("User cancelled the save dialog. No file will be created.");
                WorkflowStep = WorkflowStep.AbortedByUser;
                return false;
            }

            try
            {
                var outputFile = saveFileDialog.FileName;
                if (!OutputFormatHelper.HasValidExtension(outputFile, Job.Profile.OutputFormat))
                    outputFile = OutputFormatHelper.EnsureValidExtension(outputFile, Job.Profile.OutputFormat);

                Job.OutputFilenameTemplate = outputFile;
                return true;
            }
            catch (PathTooLongException)
            {
                Logger.Error("Filename (+ path) from savefile dialog is too long.");
                var message = _translator.GetTranslation("InteractiveWorkflow", "SelectedPathTooLong",
                    "The total length of path and filename is too long.\r\nPlease use a shorter name.");
                var caption =
                    _translator.GetTranslation("InteractiveWorkflow", "SelectDestination", "Select destination");
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Warning);
                return LaunchSaveFileForRetyping(saveFileDialog);
            }
        }

        private void job_OnActionAdded(object sender, ActionAddedEventArgs e)
        {
            // code for AttachMe here
            /*if (e.Action is AttachMeAction)
            {
                var attachMe = e.Action as AttachMeAction;
                attachMe.OnAttachmentCreated += attachMe_OnAttachmentCreated;
            }*/
        }
    }
}