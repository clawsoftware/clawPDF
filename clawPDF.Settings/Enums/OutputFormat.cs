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
        [Description("PDF/Gray-Image")] PdfImage8,
        [Description("PDF/RGB-Image")] PdfImage24,
        [Description("PDF/CMYK-Image")] PdfImage32,
        [Description("PDF/Gray-OCR")] PdfOCR8,
        [Description("PDF/RGB-OCR")] PdfOCR24,
        [Description("PDF/CMYK-OCR")] PdfOCR32,        
        [Description("JPEG")] Jpeg,
        [Description("PNG")] Png,
        [Description("TIFF")] Tif,
        [Description("SVG")] SVG,
        //[Description("DOCX")] DOCX,
        //[Description("XPS")] XPS,
        [Description("OCRText")] OCRTxt,
        [Description("Text")] Txt
    }
}