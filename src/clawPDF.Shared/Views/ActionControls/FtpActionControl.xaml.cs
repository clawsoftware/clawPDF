using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Utilities.Tokens;

namespace clawSoft.clawPDF.Shared.Views.ActionControls
{
    public partial class FtpActionControl : ActionControl
    {
        private readonly TokenReplacer _tokenReplacer;

        public FtpActionControl() : this(new TokenReplacer())
        {
        }

        public FtpActionControl(TokenReplacer tokenReplacer)
        {
            _tokenReplacer = tokenReplacer;
            Tokens = new List<string>(_tokenReplacer.GetTokenNames(true));

            InitializeComponent();

            DisplayName =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("FtpActionSettings", "DisplayName",
                    "Upload with FTP");
            Description = TranslationHelper.Instance.TranslatorInstance.GetTranslation("FtpActionSettings",
                "Description", "The FTP action allows to upload the created documents to a server via FTP.");

            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public List<string> Tokens { get; private set; }

        public override bool IsActionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.Ftp.Enabled;
            }
            set => CurrentProfile.Ftp.Enabled = value;
        }

        private string Password
        {
            get
            {
                if (CurrentProfile == null)
                    return null;
                return CurrentProfile.Ftp.Password;
            }
            set => CurrentProfile.Ftp.Password = value;
        }

        private void FtpTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            DirectoryPreviewTextBox.Text = FtpAction.MakeValidPath(_tokenReplacer.ReplaceTokens(DirectoryTextBox.Text));
        }

        private void SetPasswordButton_OnClick(object sender, RoutedEventArgs e)
        {
            var pwWindow = new FtpPasswordWindow(FtpPasswordMiddleButton.Remove);
            pwWindow.FtpPassword = Password;

            pwWindow.ShowDialogTopMost();
            if (pwWindow.Response == FtpPasswordResponse.OK)
                Password = pwWindow.FtpPassword;
            else if (pwWindow.Response == FtpPasswordResponse.Remove) Password = "";
        }

        private void TokenComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxHelper.InsertText(DirectoryTextBox,
                AddTokenComboBox.Items[AddTokenComboBox.SelectedIndex] as string);
        }
    }
}