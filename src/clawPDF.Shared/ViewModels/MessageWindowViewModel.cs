using clawSoft.clawPDF.Shared.Views;

namespace clawSoft.clawPDF.Shared.ViewModels
{
    public class MessageWindowViewModel : ViewModelBase
    {
        private readonly MessageWindowButtons _buttons;
        public MessageWindowResponse Response;

        public MessageWindowViewModel()
        {
        }

        public MessageWindowViewModel(MessageWindowButtons buttons)
        {
            Response = MessageWindowResponse.Cancel;
            _buttons = buttons;
            ButtonRightCommand = new DelegateCommand(ExecuteButtonRight); //CanExecuteButtonRight == true
            ButtonMiddleCommand = new DelegateCommand(ExecuteButtonMiddle, CanExecuteButtonMiddle);
            ButtonLeftCommand = new DelegateCommand(ExecuteButtonLeft, CanExecuteButtonLeft);
        }

        public DelegateCommand ButtonLeftCommand { get; }
        public DelegateCommand ButtonMiddleCommand { get; }
        public DelegateCommand ButtonRightCommand { get; }

        private void ExecuteButtonRight(object obj)
        {
            switch (_buttons)
            {
                case MessageWindowButtons.OK:
                    Response = MessageWindowResponse.OK;
                    RaiseCloseView(true);
                    break;

                case MessageWindowButtons.OKCancel:
                case MessageWindowButtons.RetryCancel:
                case MessageWindowButtons.MoreInfoCancel:
                    Response = MessageWindowResponse.Cancel;
                    RaiseCloseView(false);
                    break;

                case MessageWindowButtons.YesLaterNo:
                case MessageWindowButtons.YesNo:
                    Response = MessageWindowResponse.No;
                    RaiseCloseView(false);
                    break;
            }
        }

        private void ExecuteButtonMiddle(object obj)
        {
            switch (_buttons)
            {
                case MessageWindowButtons.OK:
                case MessageWindowButtons.OKCancel:
                case MessageWindowButtons.RetryCancel:
                //case MessageWindowButtons.YesNo:
                //    return; //ButtonMiddle should be collapsed!
                case MessageWindowButtons.YesLaterNo:
                    Response = MessageWindowResponse.Later;
                    RaiseCloseView(false);
                    break;
            }
        }

        private bool CanExecuteButtonMiddle(object obj)
        {
            return _buttons == MessageWindowButtons.YesLaterNo;
        }

        private void ExecuteButtonLeft(object obj)
        {
            switch (_buttons)
            {
                //case MessageWindowButtons.OK:
                //    return; //ButtonLeft schould be collapsed!
                case MessageWindowButtons.MoreInfoCancel:
                    Response = MessageWindowResponse.MoreInfo;
                    break;

                case MessageWindowButtons.OKCancel:
                    Response = MessageWindowResponse.OK;
                    break;

                case MessageWindowButtons.RetryCancel:
                    Response = MessageWindowResponse.Retry;
                    break;

                case MessageWindowButtons.YesNo:
                case MessageWindowButtons.YesLaterNo:
                    Response = MessageWindowResponse.Yes;
                    break;
            }

            RaiseCloseView(true);
        }

        private bool CanExecuteButtonLeft(object obj)
        {
            return _buttons != MessageWindowButtons.OK;
        }
    }

    public enum MessageWindowResponse
    {
        Cancel,
        Later,
        MoreInfo,
        No,
        OK,
        Retry,
        Yes
    }
}