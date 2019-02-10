using System.Windows;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.Shared.Views.ActionControls
{
    public partial class AttachmentActionControl : ActionControl
    {
        public AttachmentActionControl()
        {
            InitializeComponent();

            DisplayName =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("AttachmentSettings", "DisplayName",
                    "Add attachment");
            Description = TranslationHelper.Instance.TranslatorInstance.GetTranslation("AttachmentSettings",
                "Description",
                "Add an attachment to your documents.\r\nThe attachment file must be a PDF file and may contain multiple pages.");

            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        public override bool IsActionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.AttachmentPage.Enabled;
            }
            set => CurrentProfile.AttachmentPage.Enabled = value;
        }

        private void SelectAttachmentFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var title = TranslationHelper.Instance.TranslatorInstance.GetTranslation("AttachmentSettings",
                "SelectAttachmentFile", "Select attachment file");
            var filter =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("AttachmentSettings", "PDFFiles",
                    "PDF files")
                + @" (*.pdf)|*.pdf|"
                + TranslationHelper.Instance.TranslatorInstance.GetTranslation("AttachmentSettings", "AllFiles",
                    "All files")
                + @" (*.*)|*.*";

            FileDialogHelper.ShowSelectFileDialog(AttachmentFileTextBox, title, filter);
        }
    }
}