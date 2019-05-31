using System.Windows;
using System.Windows.Input;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class SignaturePasswordWindow
    {
        public SignaturePasswordWindow(PasswordMiddleButton middleButton, string certificateFile)
        {
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            InitializeComponent();

            if (middleButton == PasswordMiddleButton.Skip)
            {
                RemoveButton.Visibility = Visibility.Collapsed;
                PasswordHintText.Visibility = Visibility.Collapsed;
            }
            else
            {
                SkipButton.Visibility = Visibility.Collapsed;
                PasswordHintText.Visibility = Visibility.Visible;
            }

            SignaturePasswordViewModel.SignaturePasswordCheck = new SignaturePasswordCheck(certificateFile);
            SignaturePasswordViewModel.CloseViewAction = delegate (bool? result) { DialogResult = result; };
        }

        public SignaturePasswordViewModel SignaturePasswordViewModel => (SignaturePasswordViewModel)DataContext;

        public string Password
        {
            get => SignaturePasswordViewModel.Password;
            set
            {
                SignaturePasswordViewModel.Password = value;
                PasswordBox.Password = value;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            SignaturePasswordViewModel.Password = PasswordBox.Password;
        }
    }

    public enum PasswordMiddleButton
    {
        Skip,
        Remove
    }
}