using System.ComponentModel;

namespace clawSoft.clawPDF.Core.Settings.Enums
{
    public enum OutputFormat
    {
        [Description("PDF")] Pdf,
        [Description("PDF/A-1b")] PdfA1B,
        [Description("PDF/A-2b")] PdfA2B,
        [Description("PDF/A-2b")] PdfA3B,
        [Description("PDF/X")] PdfX,
        [Description("PDF/GrayImage")] PdfImage8,
        [Description("PDF/RGBImage")] PdfImage24,
        [Description("PDF/CMYKImage")] PdfImage32,
        [Description("JPEG")] Jpeg,
        [Description("PNG")] Png,
        [Description("TIFF")] Tif,
        [Description("OCRText")] OCRTxt,
        [Description("Text")] Txt
    }
}