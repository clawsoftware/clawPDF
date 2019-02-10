using System.Windows;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.Shared.Views.ActionControls
{
    public partial class CoverActionControl : ActionControl
    {
        public CoverActionControl()
        {
            InitializeComponent();

            DisplayName =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("CoverSettings", "DisplayName",
                    "Add cover");
            Description = TranslationHelper.Instance.TranslatorInstance.GetTranslation("CoverSettings", "Description",
                "Add a cover to your documents.\r\nThe cover file must be a PDF file and may contain multiple pages.");

            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public override bool IsActionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.CoverPage.Enabled;
            }
            set => CurrentProfile.CoverPage.Enabled = value;
        }

        private void SelectCoverFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var title = TranslationHelper.Instance.TranslatorInstance.GetTranslation("CoverSettings", "SelectCoverFile",
                "Select cover file");
            var filter =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("CoverSettings", "PDFFiles", "PDF files")
                + @" (*.pdf)|*.pdf|"
                + TranslationHelper.Instance.TranslatorInstance.GetTranslation("CoverSettings", "AllFiles", "All files")
                + @" (*.*)|*.*";

            FileDialogHelper.ShowSelectFileDialog(CoverFileTextBox, title, filter);
        }
    }
}