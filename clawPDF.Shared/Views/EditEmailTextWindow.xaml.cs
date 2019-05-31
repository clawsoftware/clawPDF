using System.Windows;
using System.Windows.Controls;
using clawSoft.clawPDF.Shared.Converter;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.Shared.Views
{
    public partial class EditEmailTextWindow : Window
    {
        private readonly string _signatureText;

        public EditEmailTextWindow(bool addSignature)
        {
            AddSignature = addSignature;

            InitializeComponent();

            TranslationHelper.Instance.TranslatorInstance.Translate(this);

            BodyTokenReplacerConverter.TokenReplacer = TokenHelper.TokenReplacerWithPlaceHolders;
            HeaderTokenReplacerConverter.TokenReplacer = BodyTokenReplacerConverter.TokenReplacer;

            var tokens = TokenHelper.GetTokenListForEmail();

            SubjectTokenComboBox.ItemsSource = tokens;
            BodyTokenComboBox.ItemsSource = tokens;

            _signatureText = MailSignatureHelper.ComposeMailSignature();
            AttachSignature_OnChecked(null, null);
        }

        public string Subject
        {
            get => SubjectText.Text;
            set => SubjectText.Text = value;
        }

        public string Body
        {
            get => BodyText.Text;
            set => BodyText.Text = value;
        }

        public bool AddSignature { get; set; }

        private TokenReplacerConverter BodyTokenReplacerConverter =>
            FindResource("TokenReplacerConverterWithFooter") as TokenReplacerConverter;

        private TokenReplacerConverter HeaderTokenReplacerConverter =>
            FindResource("TokenReplacerConverter") as TokenReplacerConverter;

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void AttachSignature_OnChecked(object sender, RoutedEventArgs e)
        {
            BodyTokenReplacerConverter.Footer = AddSignature ? _signatureText : "";

            var be = FinalBodyText.GetBindingExpression(TextBox.TextProperty);
            be.UpdateTarget();
        }

        private void SubjectTokenComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InsertToken(SubjectText, (ComboBox)sender);
        }

        private void BodyTokenComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InsertToken(BodyText, (ComboBox)sender);
        }

        private void InsertToken(TextBox txt, ComboBox cmb)
        {
            var text = cmb.SelectedItem.ToString();
            var newSelectionStart = txt.SelectionStart + text.Length;
            txt.Text = txt.Text.Insert(txt.SelectionStart, text);
            txt.SelectionStart = newSelectionStart;
        }
    }
}