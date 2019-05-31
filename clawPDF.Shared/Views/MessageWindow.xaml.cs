using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using Image = System.Windows.Controls.Image;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class MessageWindow : Window
    {
        private readonly MessageWindowViewModel _messageWindowVM;

        public MessageWindow(string message, string caption, MessageWindowButtons buttons, MessageWindowIcon icon)
        {
            InitializeComponent();
            MessageText.Text = message;
            Title = caption;
            SetButtons(buttons);
            SetIcon(icon);
            _messageWindowVM = new MessageWindowViewModel(buttons);
            _messageWindowVM.CloseViewAction = delegate (bool? result) { DialogResult = result; };
            DataContext = _messageWindowVM;
        }

        public MessageWindowResponse DialogResponse => _messageWindowVM.Response;

        public static MessageWindowResponse ShowTopMost(string message, string caption, MessageWindowButtons buttons,
            MessageWindowIcon icon)
        {
            var messageWindow = new MessageWindow(message, caption, buttons, icon);
            TopMostHelper.ShowDialogTopMost(messageWindow, false);
            return messageWindow.DialogResponse;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        private void SetButtons(MessageWindowButtons buttons)
        {
            var translator = TranslationHelper.Instance.TranslatorInstance;

            switch (buttons)
            {
                case MessageWindowButtons.MoreInfoCancel:
                    LeftButton.Visibility = Visibility.Visible;
                    LeftButton.Content = translator.GetTranslation("MessageWindow", "MoreInfo", "More information");
                    LeftButton.IsDefault = true;

                    MiddleButton.Visibility = Visibility.Collapsed;

                    RightButton.Visibility = Visibility.Visible;
                    RightButton.Content = translator.GetTranslation("MessageWindow", "Cancel", "Cancel");
                    RightButton.Tag = MessageWindowResponse.Cancel;
                    break;

                case MessageWindowButtons.OK:
                    RightButton.Visibility = Visibility.Visible;
                    RightButton.Content = translator.GetTranslation("MessageWindow", "Ok", "OK");
                    RightButton.IsDefault = true;
                    break;

                case MessageWindowButtons.OKCancel:
                    LeftButton.Visibility = Visibility.Visible;
                    LeftButton.Content = translator.GetTranslation("MessageWindow", "Ok", "OK");
                    LeftButton.IsDefault = true;

                    MiddleButton.Visibility = Visibility.Collapsed;

                    RightButton.Visibility = Visibility.Visible;
                    RightButton.Content = translator.GetTranslation("MessageWindow", "Cancel", "Cancel");
                    RightButton.Tag = MessageWindowResponse.Cancel;
                    break;

                case MessageWindowButtons.RetryCancel:
                    LeftButton.Visibility = Visibility.Visible;
                    LeftButton.Content = translator.GetTranslation("MessageWindow", "Retry", "Retry");
                    LeftButton.IsDefault = true;

                    MiddleButton.Visibility = Visibility.Collapsed;

                    RightButton.Visibility = Visibility.Visible;
                    RightButton.Content = translator.GetTranslation("MessageWindow", "Cancel", "Cancel");
                    RightButton.Tag = MessageWindowResponse.Cancel;
                    break;

                case MessageWindowButtons.YesLaterNo:
                    LeftButton.Visibility = Visibility.Visible;
                    LeftButton.Content = translator.GetTranslation("MessageWindow", "Yes", "Yes");
                    LeftButton.IsDefault = true;

                    MiddleButton.Visibility = Visibility.Visible;
                    MiddleButton.Content = translator.GetTranslation("MessageWindow", "Later", "Remind me");

                    RightButton.Visibility = Visibility.Visible;
                    RightButton.Content = translator.GetTranslation("MessageWindow", "Skip", "Skip version");
                    break;

                case MessageWindowButtons.YesNo:
                    LeftButton.Visibility = Visibility.Visible;
                    LeftButton.Content = translator.GetTranslation("MessageWindow", "Yes", "Yes");
                    LeftButton.IsDefault = true;

                    MiddleButton.Visibility = Visibility.Collapsed;

                    RightButton.Visibility = Visibility.Visible;
                    RightButton.Content = translator.GetTranslation("MessageWindow", "No", "No");
                    break;
            }
        }

        private void SetIcon(MessageWindowIcon icon)
        {
            IconBox.Visibility = Visibility.Visible;
            IconBox.Width = 32;
            IconBox.Height = 32;
            var img = new Image();

            switch (icon)
            {
                case MessageWindowIcon.Error:
                    img.Source = ConvertBitmap(SystemIcons.Error.ToBitmap());
                    SystemSounds.Hand.Play();
                    IconBox.Content = img;
                    break;

                case MessageWindowIcon.Exclamation:
                    img.Source = ConvertBitmap(SystemIcons.Exclamation.ToBitmap());
                    SystemSounds.Exclamation.Play();
                    IconBox.Content = img;
                    break;

                case MessageWindowIcon.Info:
                    img.Source = ConvertBitmap(SystemIcons.Information.ToBitmap());
                    IconBox.Content = img;
                    SystemSounds.Asterisk.Play();
                    break;

                case MessageWindowIcon.Question:
                    img.Source = ConvertBitmap(SystemIcons.Question.ToBitmap());
                    IconBox.Content = img;
                    SystemSounds.Question.Play();
                    break;

                case MessageWindowIcon.Warning:
                    img.Source = ConvertBitmap(SystemIcons.Warning.ToBitmap());
                    IconBox.Content = img;
                    SystemSounds.Exclamation.Play();
                    break;

                case MessageWindowIcon.None:
                    IconBox.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private BitmapImage ConvertBitmap(Bitmap value)
        {
            var ms = new MemoryStream();
            value.Save(ms, ImageFormat.Png);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        private void CommandBinding_CopyExecuted(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            Clipboard.SetText(MessageText.Text);
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