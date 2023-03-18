using System.ComponentModel;

namespace clawSoft.clawPDF.Core.Settings.Enums
{
    public enum OutputFormat
    {
        [Description("PDF")] Pdf,
        [Description("PDF/A-1b")] PdfA1B,
        [Description("PDF/A-2b")] PdfA2B,
        [Description("PDF/X")] PdfX,
        [Description("PDF/Image")] PdfImage,
        [Description("JPEG")] Jpeg,
        [Description("PNG")] Png,
        [Description("TIFF")] Tif,
        [Description("OCRText")] OCRTxt,
        [Description("Text")] Txt
    }
}