using System;
using System.IO;
using clawSoft.clawPDF.Core.Settings.Enums;

namespace clawSoft.clawPDF.Core
{
    public class OutputFormatHelper
    {
        public static bool HasValidExtension(string file, OutputFormat outputFormat)
        {
            var extension = Path.GetExtension(file);

            if (extension == null)
                return false;

            switch (outputFormat)
            {
                case OutputFormat.Pdf:
                case OutputFormat.PdfA1B:
                case OutputFormat.PdfA2B:
                case OutputFormat.PdfX:
                    return extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase);

                case OutputFormat.Jpeg:
                    return extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                           extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase);

                case OutputFormat.Png:
                    return extension.Equals(".png", StringComparison.OrdinalIgnoreCase);

                case OutputFormat.Tif:
                    return extension.Equals(".tif", StringComparison.OrdinalIgnoreCase) ||
                           extension.Equals(".tiff", StringComparison.OrdinalIgnoreCase);

                case OutputFormat.Txt:
                    return extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public static string EnsureValidExtension(string file, OutputFormat outputFormat)
        {
            if (HasValidExtension(file, outputFormat))
                return file;

            switch (outputFormat)
            {
                case OutputFormat.Pdf:
                case OutputFormat.PdfA1B:
                case OutputFormat.PdfA2B:
                case OutputFormat.PdfX:
                    return Path.ChangeExtension(file, ".pdf");

                case OutputFormat.Jpeg:
                    return Path.ChangeExtension(file, ".jpg");

                case OutputFormat.Png:
                    return Path.ChangeExtension(file, ".png");

                case OutputFormat.Tif:
                    return Path.ChangeExtension(file, ".tif");

                case OutputFormat.Txt:
                    return Path.ChangeExtension(file, ".txt");
            }

            return file;
        }
    }
}