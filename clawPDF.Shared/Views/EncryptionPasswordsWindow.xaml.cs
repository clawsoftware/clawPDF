using System.Windows;
using System.Windows.Input;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class EncryptionPasswordsWindow : Window
    {
        public EncryptionPasswordsWindow(EncryptionPasswordMiddleButton middleButton, bool askOwnerPassword,
            bool askUserPassword)
        {
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            InitializeComponent();

            switch (middleButton)
            {
                case EncryptionPasswordMiddleButton.Skip:
                    DataContext = new EncryptionPasswordViewModelwithSkip(askOwnerPassword, askUserPassword);
                    RemoveButton.Visibility = Visibility.Collapsed;
                    PasswordHintText.Visibility = Visibility.Collapsed;
                    break;

                default:
                    DataContext = new EncryptionPasswordViewModelwithRemove(askUserPassword);
                    SkipButton.Visibility = Visibility.Collapsed;
                    break;
            }

            EncryptionPasswordViewModel.CloseViewAction = delegate (bool? result) { DialogResult = result; };
        }

        public EncryptionPasswordViewModelwithRemove EncryptionPasswordViewModel =>
            (EncryptionPasswordViewModelwithRemove)DataContext;

        public EncryptionPasswordResponse Response => EncryptionPasswordViewModel.Response;

        public string OwnerPassword
        {
            get => EncryptionPasswordViewModel.OwnerPassword;
            set
            {
                EncryptionPasswordViewModel.OwnerPassword = value;
                OwnerPasswordBox.Password = value;
            }
        }

        public string UserPassword
        {
            get => EncryptionPasswordViewModel.UserPassword;
            set
            {
                EncryptionPasswordViewModel.UserPassword = value;
                UserPasswordBox.Password = value;
            }
        }

        private void PwbOwnerPasswordChanged(object sender, RoutedEventArgs e)
        {
            EncryptionPasswordViewModel.OwnerPassword = OwnerPasswordBox.Password;
        }

        private void PwbUserPasswordChanged(object sender, RoutedEventArgs e)
        {
            EncryptionPasswordViewModel.UserPassword = UserPasswordBox.Password;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public EncryptionPasswordResponse ShowDialogTopMost()
        {
            TopMostHelper.ShowDialogTopMost(this, false);
            return Response;
        }
    }

    public enum EncryptionPasswordMiddleButton
    {
        Skip,
        Remove
    }
}