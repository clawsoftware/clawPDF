using System.Windows;
using System.Windows.Input;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class TimeServerPasswordWindow : Window
    {
        public TimeServerPasswordWindow()
        {
            Loaded += (sender, e) =>
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            InitializeComponent();

            TimeServerPasswordViewModel.CloseViewAction = delegate (bool? result) { DialogResult = result; };
        }

        public TimeServerPasswordViewModel TimeServerPasswordViewModel => (TimeServerPasswordViewModel)DataContext;

        public string TimeServerLoginName
        {
            get => TimeServerPasswordViewModel.TimeServerLoginName;
            set
            {
                TimeServerPasswordViewModel.TimeServerLoginName = value;
                TimeServerLoginNameBox.Text = value;
            }
        }

        public string TimeServerPassword
        {
            get => TimeServerPasswordViewModel.TimeServerPassword;
            set
            {
                TimeServerPasswordViewModel.TimeServerPassword = value;
                TimeServerPasswordBox.Password = value;
            }
        }

        public TimeServerPasswordResponse Response => TimeServerPasswordViewModel.Response;

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            TimeServerPasswordViewModel.TimeServerPassword = TimeServerPasswordBox.Password;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public TimeServerPasswordResponse ShowDialogTopMost()
        {
            TopMostHelper.ShowDialogTopMost(this, false);
            return Response;
        }
    }
}