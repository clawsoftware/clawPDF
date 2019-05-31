using System.IO;
using System.Text;
using System.Windows;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;
using NLog;

namespace clawSoft.clawPDF.Shared.Views.ActionControls
{
    public partial class EmailSmtpActionControl : ActionControl
    {
        public EmailSmtpActionControl(bool displayMailClientTextCheckbox)
        {
            DisplayMailClientTextCheckbox = displayMailClientTextCheckbox;
            InitializeComponent();

            DisplayName = TranslationHelper.Instance.TranslatorInstance.GetTranslation("SmtpEmailActionSettings",
                "DisplayName", "Send e-mail over SMTP");
            Description = TranslationHelper.Instance.TranslatorInstance.GetTranslation("SmtpEmailActionSettings",
                "Description",
                "The SMTP e-mail action allows to directly send files via e-mail without further user interaction. Notice: This action is intended for advanced users and requires careful attention as it can silently send the converted documents via e-mail to the configured recipients.");

            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public EmailSmtpActionControl() : this(true)
        {
        }

        public bool DisplayMailClientTextCheckbox { get; set; }

        public override bool IsActionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.EmailSmtp.Enabled;
            }
            set => CurrentProfile.EmailSmtp.Enabled = value;
        }

        private EmailSmtp EmailSmtpSettings
        {
            get
            {
                if (DataContext == null)
                    return null;
                return ((ActionsTabViewModel)DataContext).CurrentProfile.EmailSmtp;
            }
        }

        private string Password
        {
            get
            {
                if (DataContext == null)
                    return null;
                return ((ActionsTabViewModel)DataContext).CurrentProfile.EmailSmtp.Password;
            }
            set => ((ActionsTabViewModel)DataContext).CurrentProfile.EmailSmtp.Password = value;
        }

        private void EditMailTextButton_OnClick(object sender, RoutedEventArgs e)
        {
            var setEmailTextForm = new EditEmailTextWindow(EmailSmtpSettings.AddSignature);
            setEmailTextForm.Subject = EmailSmtpSettings.Subject;
            setEmailTextForm.Body = EmailSmtpSettings.Content;

            if (setEmailTextForm.ShowDialog() == true)
            {
                EmailSmtpSettings.Subject = setEmailTextForm.Subject;
                EmailSmtpSettings.Content = setEmailTextForm.Body;
                EmailSmtpSettings.AddSignature = setEmailTextForm.AddSignature;
            }
        }

        private void SetPasswordButton_OnClick(object sender, RoutedEventArgs e)
        {
            var pwWindow = new SmtpPasswordWindow(SmtpPasswordMiddleButton.Remove);
            pwWindow.SmtpPassword = Password;

            pwWindow.ShowDialogTopMost();

            if (pwWindow.Response == SmtpPasswordResponse.OK)
                Password = pwWindow.SmtpPassword;
            else if (pwWindow.Response == SmtpPasswordResponse.Remove) Password = "";
        }

        private void SendTestMailButton_OnClick(object sender, RoutedEventArgs e)
        {
            var smtpMailAction = new SmtpMailAction(MailSignatureHelper.ComposeMailSignature(CurrentProfile.EmailSmtp));

            var currentProfile = ((ActionsTabViewModel)DataContext).CurrentProfile.Copy();

            #region check profile

            var result = smtpMailAction.Check(currentProfile);
            if (!result.Success)
            {
                var caption = TranslationHelper.Instance.TranslatorInstance.GetTranslation("SmtpEmailActionSettings",
                    "SendTestMail", "Send test mail");
                var message = ErrorCodeInterpreter.GetFirstErrorText(result, true);
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
                return;
            }

            #endregion check profile

            #region create job

            var tempFolder = Path.GetTempPath();
            var tmpTestFolder = Path.Combine(tempFolder, "clawPDFTest\\SendSmtpTestmail");
            Directory.CreateDirectory(tmpTestFolder);
            var tmpInfFile = Path.Combine(tmpTestFolder, "SmtpTest.inf");

            var sb = new StringBuilder();
            sb.AppendLine("[1]");
            sb.AppendLine("SessionId=1");
            sb.AppendLine("WinStation=Console");
            sb.AppendLine("UserName=SampleUser1234");
            sb.AppendLine("ClientComputer=\\PC1");
            sb.AppendLine("PrinterName=clawPDF");
            sb.AppendLine("JobId=1");
            sb.AppendLine("DocumentTitle=SmtpTest");
            sb.AppendLine("");

            File.WriteAllText(tmpInfFile, sb.ToString(), Encoding.GetEncoding("Unicode"));

            var jobTranslations = new JobTranslations();
            jobTranslations.EmailSignature = MailSignatureHelper.ComposeMailSignature(true);

            var tempFolderProvider = new StaticTempFolderProvider(Path.Combine(Path.GetTempPath(), "clawPDF"));

            var job = new GhostscriptJob(new JobInfo(tmpInfFile), new ConversionProfile(), tempFolderProvider,
                jobTranslations);

            job.Profile = currentProfile;

            #endregion create job

            #region add password

            if (string.IsNullOrEmpty(Password))
            {
                var pwWindow = new SmtpPasswordWindow(SmtpPasswordMiddleButton.None, currentProfile.EmailSmtp.Address,
                    currentProfile.EmailSmtp.Recipients);
                if (pwWindow.ShowDialogTopMost() != SmtpPasswordResponse.OK)
                {
                    Directory.Delete(tmpTestFolder, true);
                    return;
                }

                job.Passwords.SmtpPassword = pwWindow.SmtpPassword;
            }
            else
            {
                job.Passwords.SmtpPassword = Password;
            }

            #endregion add password

            #region add testfile

            var testFile = Path.Combine(tmpTestFolder, "testfile.txt");
            File.WriteAllText(testFile, @"clawPDF", Encoding.GetEncoding("Unicode"));
            job.OutputFiles.Add(testFile);

            #endregion add testfile

            LogManager.GetCurrentClassLogger().Info("Send test mail over smtp.");
            result = smtpMailAction.ProcessJob(job);
            Directory.Delete(tmpTestFolder, true);

            if (!result.Success)
            {
                var caption = TranslationHelper.Instance.TranslatorInstance.GetTranslation("SmtpEmailActionSettings",
                    "SendTestMail",
                    "Send test mail");
                var message = ErrorCodeInterpreter.GetFirstErrorText(result, true);
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
            }
            else
            {
                var caption = TranslationHelper.Instance.TranslatorInstance.GetTranslation("SmtpEmailActionSettings",
                    "SendTestMail", "Send test mail");
                var message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation(
                    "SmtpEmailActionSettings", "TestMailSent",
                    "Test mail sent to {0}.", job.Profile.EmailSmtp.Recipients);
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Info);
            }
        }
    }

    internal class StaticTempFolderProvider : ITempFolderProvider
    {
        public StaticTempFolderProvider(string tempFolder)
        {
            TempFolder = tempFolder;
        }

        public string TempFolder { get; }
    }
}