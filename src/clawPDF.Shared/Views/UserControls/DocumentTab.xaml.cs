using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Shared.Converter;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;
using clawSoft.clawPDF.Utilities.Tokens;
using ComboBox = System.Windows.Controls.ComboBox;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace clawSoft.clawPDF.Shared.Views.UserControls
{
    public partial class DocumentTab : UserControl
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;
        private readonly TokenReplacer _tokenReplacer = TokenHelper.TokenReplacerWithPlaceHolders;

        public DocumentTab()
        {
            InitializeComponent();
            if (TranslationHelper.IsInitialized) TranslationHelper.TranslatorInstance.Translate(this);

            foreach (var token in TokenHelper.GetTokenListForTitle())
                TitleTokensComboBox.Items.Add(token);

            foreach (var token in TokenHelper.GetTokenListForAuthor())
                AuthorTokensComboBox.Items.Add(token);

            TokenReplacerConverter.TokenReplacer = TokenHelper.TokenReplacerWithPlaceHolders;
        }

        private TokenReplacerConverter TokenReplacerConverter =>
            FindResource("TokenReplacerConverter") as TokenReplacerConverter;

        public CurrentProfileViewModel ViewModel => (CurrentProfileViewModel)DataContext;

        private void AuthorTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            AuthorPreviewText.Text = _tokenReplacer.ReplaceTokens(AuthorTextBox.Text);
        }

        private void TitleTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TitlePreviewText.Text = _tokenReplacer.ReplaceTokens(TitleTextBox.Text);
        }

        private void AuthorTokensComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InsertToken(AuthorTextBox, (ComboBox)sender);
        }

        private void TitleTokensComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InsertToken(TitleTextBox, (ComboBox)sender);
        }

        private void InsertToken(TextBox textBox, ComboBox comboBox)
        {
            var text = comboBox.Items[comboBox.SelectedIndex] as string;
            // use binding-safe way to insert text with settings focus
            TextBoxHelper.InsertText(textBox, text);
        }

        public void UpdateFontLabel(Stamping stamping)
        {
            var fontSize = stamping.FontSize.ToString();
            var fontstring = string.Format("{0} {1}pt", stamping.FontName, fontSize);
            if (fontstring.Length > 25)
            {
                fontstring = stamping.FontName.Substring(0, 25 - 4 - fontSize.Length).TrimEnd();
                fontstring = string.Format("{0}. {1}pt", fontstring, fontSize);
            }

            StampFontButton.Content = fontstring;
        }

        private void StampChooseColorButton_OnClick(object sender, RoutedEventArgs e)
        {
            var colorDialog = new ColorDialog();
            colorDialog.Color = ViewModel.CurrentProfile.Stamping.Color;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ViewModel.CurrentProfile.Stamping.Color = colorDialog.Color;
                ViewModel.RaiseCurrentProfileChanged();
            }
        }

        private void StampChooseFontButton_OnClick(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();
            fontDialog.Font = new Font(ViewModel.CurrentProfile.Stamping.FontName,
                ViewModel.CurrentProfile.Stamping.FontSize);

            try
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    var postScriptName = FontHelper.FindPostScriptName(fontDialog.Font.Name);

                    if (postScriptName == null)
                    {
                        var message = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow",
                            "FontFileNotSupported",
                            "The selected font is not supported. Please select a different font.");

                        throw new InvalidOperationException(message);
                    }

                    ViewModel.CurrentProfile.Stamping.FontName = fontDialog.Font.Name;
                    ViewModel.CurrentProfile.Stamping.PostScriptFontName = postScriptName;
                    ViewModel.CurrentProfile.Stamping.FontSize = fontDialog.Font.Size;

                    UpdateFontLabel(ViewModel.CurrentProfile.Stamping);
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is InvalidOperationException)
                {
                    var message = TranslationHelper.TranslatorInstance.GetTranslation("ProfileSettingsWindow",
                        "FontFileNotSupported",
                        "The selected font is not supported. Please select a different font.");
                    MessageWindow.ShowTopMost(message, "clawPDF", MessageWindowButtons.OK,
                        MessageWindowIcon.Warning);
                    return;
                }

                throw;
            }
        }
    }
}