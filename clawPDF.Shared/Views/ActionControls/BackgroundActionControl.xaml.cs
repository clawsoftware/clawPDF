using System.Collections.Generic;
using System.Windows;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.Views.ActionControls
{
    public partial class BackgroundActionControl : ActionControl
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public BackgroundActionControl()
        {
            InitializeComponent();

            DisplayName = TranslationHelper.Instance.TranslatorInstance.GetTranslation("BackgroundSettings",
                "DisplayName", "Add background (only for PDF)");
            Description = TranslationHelper.Instance.TranslatorInstance.GetTranslation("BackgroundSettings",
                "Description",
                "Add a background to your PDF documents.\r\nThe background file must be a PDF file and may contain multiple pages.");

            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public static IEnumerable<EnumValue<BackgroundRepetition>> BackgroundRepetitionValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<BackgroundRepetition>();

        public override bool IsActionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.BackgroundPage.Enabled;
            }
            set => CurrentProfile.BackgroundPage.Enabled = value;
        }

        private void SelectBackgroundFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var title = TranslationHelper.Instance.TranslatorInstance.GetTranslation("BackgroundSettings",
                "SelectBackgroundFile", "Select background file");
            var filter =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("BackgroundSettings", "PDFFiles",
                    "PDF files")
                + @" (*.pdf)|*.pdf|"
                + TranslationHelper.Instance.TranslatorInstance.GetTranslation("BackgroundSettings", "AllFiles",
                    "All files")
                + @" (*.*)|*.*";

            FileDialogHelper.ShowSelectFileDialog(BackgroundFileTextBox, title, filter);
        }
    }
}