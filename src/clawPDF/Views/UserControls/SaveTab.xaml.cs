using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels.UserControls;
using pdfforge.DynamicTranslator;
using ComboBox = System.Windows.Controls.ComboBox;
using TextBox = System.Windows.Controls.TextBox;

namespace clawSoft.clawPDF.Views.UserControls
{
    internal partial class SaveTab
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public SaveTab()
        {
            InitializeComponent();
            if (TranslationHelper.IsInitialized) TranslationHelper.TranslatorInstance.Translate(this);

            foreach (var token in TokenHelper.GetTokenListForFilename())
                FilenameTokensComboBox.Items.Add(token);

            foreach (var token in TokenHelper.GetTokenListForDirectory())
                SaveDialogFolderTokensComboBox.Items.Add(token);
        }

        public static IEnumerable<EnumValue<OutputFormat>> DefaultFileFormatValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<OutputFormat>();

        public CurrentProfileViewModel ViewModel => (CurrentProfileViewModel)DataContext;

        private void FilenameTokensComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InsertToken(FilenameTemplateTextBox, (ComboBox)sender);
        }

        private void InsertToken(TextBox textBox, ComboBox comboBox)
        {
            var text = comboBox.Items[comboBox.SelectedIndex] as string;
            // use binding-safe way to insert text with settings focus
            TextBoxHelper.InsertText(textBox, text);
        }

        private void SaveDialogFolderTokensComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InsertToken(SaveDialogFolderTextBox, (ComboBox)sender);
        }

        private void SaveDialogDirectoryBrowseFolderButton_OnClick(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();

            folderBrowserDialog.Description = TranslationHelper.TranslatorInstance.GetTranslation(
                "ProfileSettingsWindow",
                "SelectSaveDialogFolder", "Select save dialog folder");
            folderBrowserDialog.ShowNewFolderButton = true;

            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            TextBoxHelper.SetText(SaveDialogFolderTextBox, folderBrowserDialog.SelectedPath);
        }
    }
}