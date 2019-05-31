using System.Windows;
using System.Windows.Input;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class FtpPasswordWindow : Window
    {
        public FtpPasswordWindow(FtpPasswordMiddleButton middleButton)
        {
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            InitializeComponent();

            switch (middleButton)
            {
                case FtpPasswordMiddleButton.Skip:
                    RemoveButton.Visibility = Visibility.Collapsed;
                    PasswordHintText.Visibility = Visibility.Collapsed;
                    break;

                default:
                    SkipButton.Visibility = Visibility.Collapsed;
                    break;
            }

            FtpPasswordViewModel.CloseViewAction = delegate (bool? result) { DialogResult = result; };
        }

        public FtpPasswordViewModel FtpPasswordViewModel => (FtpPasswordViewModel)DataContext;

        public string FtpPassword
        {
            get => FtpPasswordViewModel.FtpPassword;
            set
            {
                FtpPasswordViewModel.FtpPassword = value;
                FtpPasswordBox.Password = value;
            }
        }

        public FtpPasswordResponse Response => FtpPasswordViewModel.Response;

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            FtpPasswordViewModel.FtpPassword = FtpPasswordBox.Password;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public FtpPasswordResponse ShowDialogTopMost()
        {
            TopMostHelper.ShowDialogTopMost(this, false);
            return Response;
        }
    }

    public enum FtpPasswordMiddleButton
    {
        Remove,
        Skip
    }
}