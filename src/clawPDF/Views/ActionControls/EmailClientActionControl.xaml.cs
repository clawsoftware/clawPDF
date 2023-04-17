using System;
using System.IO;
using System.Windows;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Mail;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.Shared.Views.ActionControls;
using NLog;

namespace clawSoft.clawPDF.Views.ActionControls
{
    internal partial class EmailClientActionControl : ActionControl
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public EmailClientActionControl()
        {
            InitializeComponent();

            DisplayName = TranslationHelper.Instance.TranslatorInstance.GetTranslation("EmailClientActionSettings",
                "DisplayName", "Open e-mail client");
            Description = TranslationHelper.Instance.TranslatorInstance.GetTranslation("EmailClientActionSettings",
                "Description",
                "Opens a new e-mail in the default client. There you can add receipients, text and other information and then send the mail to your contacts.");
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public override bool IsActionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.EmailClient.Enabled;
            }
            set => CurrentProfile.EmailClient.Enabled = value;
        }

        private EmailClient EmailClientSettings
        {
            get
            {
                if (DataContext == null)
                    return null;
                return ((CurrentProfileViewModel)DataContext).CurrentProfile.EmailClient;
            }
        }

        private void EmailClientTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            var emailClientFactory = new EmailClientFactory();
            var emailClient = emailClientFactory.CreateEmailClient();

            if (emailClient == null)
            {
                var caption = TranslationHelper.Instance.TranslatorInstance.GetTranslation("EmailClientActionSettings",
                    "CheckMailClient", "Check e-mail client");
                var message = TranslationHelper.Instance.TranslatorInstance.GetTranslation("EmailClientActionSettings",
                    "NoMapiClientFound",
                    "Could not find MAPI client (e.g. Thunderbird or Outlook).");
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Warning);
                return;
            }

            var eMail = new Email();
            eMail.To.Add(EmailClientSettings.Recipients);
            eMail.Subject = EmailClientSettings.Subject;
            eMail.Body = EmailClientSettings.Content + MailSignatureHelper.ComposeMailSignature(EmailClientSettings);

            try
            {
                var tempFolder = Path.GetTempPath();
                var tmpTestFolder = Path.Combine(tempFolder, "clawPDF-Test\\SendSmtpTestmail");
                Directory.CreateDirectory(tmpTestFolder);
                var tmpFile = Path.Combine(tmpTestFolder, "clawPDF Mail Client Test.pdf");
                File.WriteAllText(tmpFile, "");
                eMail.Attachments.Add(new Attachment(tmpFile));

                emailClient.ShowEmailClient(eMail);
            }
            catch (Exception ex)
            {
                _logger.Warn(ex, "Exception while creating mail");
            }
        }

        private void EditEmailTextButton_OnClick(object sender, RoutedEventArgs e)
        {
            var setEmailTextForm = new EditEmailTextWindow(EmailClientSettings.AddSignature);
            setEmailTextForm.Subject = EmailClientSettings.Subject;
            setEmailTextForm.Body = EmailClientSettings.Content;

            if (setEmailTextForm.ShowDialog() == true)
            {
                EmailClientSettings.Subject = setEmailTextForm.Subject;
                EmailClientSettings.Content = setEmailTextForm.Body;
                EmailClientSettings.AddSignature = setEmailTextForm.AddSignature;
            }
        }
    }
}