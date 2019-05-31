using System.IO;
using System.Windows;
using System.Windows.Controls;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;

namespace clawSoft.clawPDF.Shared.Views.UserControls
{
    public partial class PdfTab : UserControl
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public PdfTab()
        {
            InitializeComponent();
            if (TranslationHelper.IsInitialized) TranslationHelper.TranslatorInstance.Translate(this);
        }

        public PdfTabViewModel ViewModel => (PdfTabViewModel)DataContext;

        private void CertificationFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var title = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow", "SelectCertFile",
                "Select certificate file");
            var filter =
                TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow", "PfxP12Files",
                    "PFX/P12 files")
                + @" (*.pfx, *.p12)|*.pfx;*.p12|"
                + TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow", "AllFiles", "All files")
                + @" (*.*)|*.*";

            FileDialogHelper.ShowSelectFileDialog(CertificationFileTextBox, title, filter);
        }

        private void DefaultTimeServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxHelper.SetText(TimeServerUrlTextBox, @"http://timestamp.globalsign.com/scripts/timstamp.dll");
            SecuredTimeserverCheckBox.IsChecked = false;
        }

        private void SecurityPasswordsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var askUserPassword = ViewModel.CurrentProfile.PdfSettings.Security.RequireUserPassword;

            var pwWindow = new EncryptionPasswordsWindow(EncryptionPasswordMiddleButton.Remove, true, askUserPassword);
            pwWindow.OwnerPassword = ViewModel.CurrentProfile.PdfSettings.Security.OwnerPassword;
            pwWindow.UserPassword = ViewModel.CurrentProfile.PdfSettings.Security.UserPassword;

            pwWindow.ShowDialogTopMost();

            if (pwWindow.Response == EncryptionPasswordResponse.OK)
            {
                ViewModel.CurrentProfile.PdfSettings.Security.OwnerPassword = pwWindow.OwnerPassword;
                ViewModel.CurrentProfile.PdfSettings.Security.UserPassword = pwWindow.UserPassword;
            }
            else if (pwWindow.Response == EncryptionPasswordResponse.Remove)
            {
                ViewModel.CurrentProfile.PdfSettings.Security.UserPassword = "";
                ViewModel.CurrentProfile.PdfSettings.Security.OwnerPassword = "";
            }
        }

        private void SignaturePasswordButton_OnClick(object sender, RoutedEventArgs e)
        {
            var certificationFile = CertificationFileTextBox.Text;

            if (!File.Exists(certificationFile))
            {
                var message = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow",
                    "CertificateDoesNotExist",
                    "The certificate file does not exist.");
                var caption = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow",
                    "PDFSignature",
                    "PDF Signature");
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
                return;
            }

            var pwWindow = new SignaturePasswordWindow(PasswordMiddleButton.Remove, certificationFile);
            pwWindow.Password = ViewModel.CurrentProfile.PdfSettings.Signature.SignaturePassword;

            pwWindow.ShowDialog();
            if (pwWindow.SignaturePasswordViewModel.Result == SignaturePasswordResult.StorePassword)
                ViewModel.CurrentProfile.PdfSettings.Signature.SignaturePassword = pwWindow.Password;
            else if (pwWindow.SignaturePasswordViewModel.Result == SignaturePasswordResult.RemovePassword)
                ViewModel.CurrentProfile.PdfSettings.Signature.SignaturePassword = "";
        }

        private void TimeServerLoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            var pwWindow = new TimeServerPasswordWindow();
            pwWindow.TimeServerLoginName = ViewModel.CurrentProfile.PdfSettings.Signature.TimeServerLoginName;
            pwWindow.TimeServerPassword = ViewModel.CurrentProfile.PdfSettings.Signature.TimeServerPassword;

            pwWindow.ShowDialogTopMost();

            if (pwWindow.Response == TimeServerPasswordResponse.OK)
            {
                ViewModel.CurrentProfile.PdfSettings.Signature.TimeServerLoginName = pwWindow.TimeServerLoginName;
                ViewModel.CurrentProfile.PdfSettings.Signature.TimeServerPassword = pwWindow.TimeServerPassword;
            }
            else if (pwWindow.Response == TimeServerPasswordResponse.Remove)
            {
                ViewModel.CurrentProfile.PdfSettings.Signature.TimeServerLoginName = "";
                ViewModel.CurrentProfile.PdfSettings.Signature.TimeServerPassword = "";
            }
        }

        private void JpegFactorTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var cursorPosition = JpegFactorTextBox.SelectionStart;
            JpegFactorTextBox.Text = JpegFactorTextBox.Text.Replace(',', '.');
            JpegFactorTextBox.SelectionStart = cursorPosition;
        }
    }
}