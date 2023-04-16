using clawSoft.clawPDF.Shared.ViewModels;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class MessageWindow
    {
        public static AdonisUI.Controls.MessageBoxResult Show(string message, string caption, string[] labels, MessageWindowButtons buttons,
            MessageWindowIcon icon)
        {
            var messageWindow = AdonisUI.Controls.MessageBox.Show(message, caption, labels, (AdonisUI.Controls.MessageBoxButton)buttons, (AdonisUI.Controls.MessageBoxImage)icon);
            return messageWindow;
        }
    }

    public enum MessageWindowButtons
    {
        MoreInfoCancel,
        OK,
        OKCancel,
        RetryCancel,
        YesNo,
        YesLaterNo
    }

    public enum MessageWindowIcon
    {
        clawPDF,
        clawSoft,
        Warning,
        Error,
        Exclamation,
        Question,
        Info,
        None
    }
}