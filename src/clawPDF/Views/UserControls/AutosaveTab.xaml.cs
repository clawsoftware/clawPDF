using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;
using ComboBox = System.Windows.Controls.ComboBox;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

namespace clawSoft.clawPDF.Views.UserControls
{
    internal partial class AutosaveTab : UserControl
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public AutosaveTab()
        {
            InitializeComponent();
            if (TranslationHelper.IsInitialized) TranslationHelper.TranslatorInstance.Translate(this);

            foreach (var token in TokenHelper.GetTokenListForDirectory()) TargetFolderTokensComboBox.Items.Add(token);
        }

        public CurrentProfileViewModel ViewModel => (CurrentProfileViewModel)DataContext;

        private void TargetFolderTokensComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InsertToken(TargetFolderTextBox, (ComboBox)sender);
        }

        private void InsertToken(TextBox textBox, ComboBox comboBox)
        {
            var text = comboBox.Items[comboBox.SelectedIndex] as string;
            // use binding-safe way to insert text with settings focus
            TextBoxHelper.InsertText(textBox, text);
        }

        private void AutoSaveBrowseFolderButton_OnClick(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();

            folderBrowserDialog.Description = TranslationHelper.TranslatorInstance.GetTranslation(
                "ProfileSettingsWindow",
                "SelectAutoSaveFolder", "Select folder for automatic saving");
            folderBrowserDialog.ShowNewFolderButton = true;

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            // use binding-safe way to insert text with settings focus
            TextBoxHelper.SetText(TargetFolderTextBox, folderBrowserDialog.SelectedPath);
        }
    }
}