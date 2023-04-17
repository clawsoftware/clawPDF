using System;
using System.Windows;
using System.Windows.Controls;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class InputBoxWindow : Window
    {
        public InputBoxWindow()
        {
            InitializeComponent();
        }

        public Func<string, InputBoxValidation> IsValidInput { get; set; }

        public string InputText
        {
            get => TextBox.Text;
            set => TextBox.Text = value;
        }

        public string QuestionText
        {
            set => Label.Content = value;
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsValidInput == null)
                return;

            var validation = IsValidInput(TextBox.Text);

            OkButton.IsEnabled = validation.IsValid;
            ValidationMessageText.Text = validation.Message;
        }
    }

    public class InputBoxValidation
    {
        public InputBoxValidation(bool isValid)
        {
            IsValid = isValid;
        }

        public InputBoxValidation(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public bool IsValid { get; set; }

        public string Message { get; set; }
    }
}