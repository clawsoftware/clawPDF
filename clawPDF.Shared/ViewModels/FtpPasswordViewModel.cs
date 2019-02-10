namespace clawSoft.clawPDF.Shared.ViewModels
{
    public class FtpPasswordViewModel : ViewModelBase
    {
        private string _ftpPassword;

        public FtpPasswordResponse Response;

        public FtpPasswordViewModel()
        {
            Response = FtpPasswordResponse.Cancel;

            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            RemoveCommand = new DelegateCommand(ExecuteRemove);
            SkipCommand = new DelegateCommand(ExecuteSkip);
        }

        public DelegateCommand OkCommand { get; protected set; }
        public DelegateCommand RemoveCommand { get; protected set; }
        public DelegateCommand SkipCommand { get; protected set; }

        public string FtpPassword
        {
            get => _ftpPassword;
            set
            {
                _ftpPassword = value;
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private void ExecuteSkip(object obj)
        {
            _ftpPassword = "";
            Response = FtpPasswordResponse.Skip;
            RaiseCloseView(true);
        }

        private void ExecuteRemove(object obj)
        {
            _ftpPassword = "";
            Response = FtpPasswordResponse.Remove;
            RaiseCloseView(true);
        }

        private bool CanExecuteOk(object obj)
        {
            return !string.IsNullOrEmpty(_ftpPassword);
        }

        private void ExecuteOk(object obj)
        {
            Response = FtpPasswordResponse.OK;
            RaiseCloseView(true);
        }
    }

    public enum FtpPasswordResponse
    {
        OK,
        Remove,
        Skip,
        Cancel
    }
}