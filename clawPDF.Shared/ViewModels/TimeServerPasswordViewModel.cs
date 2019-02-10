namespace clawSoft.clawPDF.Shared.ViewModels
{
    public class TimeServerPasswordViewModel : ViewModelBase
    {
        private string _timeServerLoginName;

        private string _timeServerPassword;

        public TimeServerPasswordResponse Response;

        public TimeServerPasswordViewModel()
        {
            Response = TimeServerPasswordResponse.Cancel;

            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            RemoveCommand = new DelegateCommand(ExecuteRemove);
        }

        public DelegateCommand OkCommand { get; protected set; }
        public DelegateCommand RemoveCommand { get; protected set; }

        public string TimeServerPassword
        {
            get => _timeServerPassword;
            set
            {
                _timeServerPassword = value;
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        public string TimeServerLoginName
        {
            get => _timeServerLoginName;
            set
            {
                _timeServerLoginName = value;
                OkCommand.RaiseCanExecuteChanged();
            }
        }

        private void ExecuteRemove(object obj)
        {
            _timeServerPassword = "";
            _timeServerLoginName = "";
            Response = TimeServerPasswordResponse.Remove;
            RaiseCloseView(true);
        }

        private bool CanExecuteOk(object obj)
        {
            return !string.IsNullOrEmpty(_timeServerPassword)
                   && !string.IsNullOrEmpty(_timeServerLoginName);
        }

        private void ExecuteOk(object obj)
        {
            Response = TimeServerPasswordResponse.OK;
            RaiseCloseView(true);
        }
    }

    public enum TimeServerPasswordResponse
    {
        OK,
        Remove,
        Cancel
    }
}