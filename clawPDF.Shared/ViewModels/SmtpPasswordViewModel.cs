namespace clawSoft.clawPDF.Shared.ViewModels
{
    public class SmtpPasswordViewModel : ViewModelBase
    {
        private string _smtpPassword;

        public SmtpPasswordResponse Response;

        public SmtpPasswordViewModel()
        {
            Response = SmtpPasswordResponse.Cancel;

            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            RemoveCommand = new DelegateCommand(ExecuteRemove);
            SkipCommand = new DelegateCommand(ExecuteSkip);
        }

        public DelegateCommand OkCommand { get; protected set; }
        public DelegateCommand RemoveCommand { get; protected set; }
        public DelegateCommand SkipCommand { get; protected set; }

        public string SmtpPassword
        {
            get => _smtpPassword;
            set
            {
                _smtpPassword = value;
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private void ExecuteSkip(object obj)
        {
            _smtpPassword = "";
            Response = SmtpPasswordResponse.Skip;
            RaiseCloseView(true);
        }

        private void ExecuteRemove(object obj)
        {
            _smtpPassword = "";
            Response = SmtpPasswordResponse.Remove;
            RaiseCloseView(true);
        }

        private bool CanExecuteOk(object obj)
        {
            return !string.IsNullOrEmpty(_smtpPassword);
        }

        private void ExecuteOk(object obj)
        {
            Response = SmtpPasswordResponse.OK;
            RaiseCloseView(true);
        }
    }

    public enum SmtpPasswordResponse
    {
        OK,
        Remove,
        Skip,
        Cancel
    }
}