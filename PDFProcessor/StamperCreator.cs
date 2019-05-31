using System;
using System.IO;
using clawSoft.clawPDF.Core.Settings;
using iTextSharp.text.pdf;
using NLog;

namespace clawSoft.clawPDF.PDFProcessing
{
    public static class StamperCreator
    {
        //ActionId = 25;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Create a PdfStamper with content of source file aiming to destination file.
        ///     If encryption is enabled, the stamper will be created with an corresponding signature,
        ///     which influences the PDF version. (1.4 for 40bit and 128bit encryption, 1.6 for 128bitAes).
        /// </summary>
        /// <param name="sourceFile">Full path to the source file</param>
        /// <param name="destinationFile">Full patch to the destination file</param>
        /// <param name="profile">Profile with encryption settings</param>
        /// <returns>Stamper with content of source file stream, aiming to destination file</returns>
        /// <exception cref="ProcessingException">In case of any error</exception>
        public static PdfStamper CreateStamperAccordingToEncryptionAndSignature(string sourceFile,
            string destinationFile, ConversionProfile profile)
        {
            PdfStamper stamper;

            try
            {
                stamper = DoCreateStamperAccordingToEncryptionAndSignature(sourceFile, destinationFile, profile);
            }
            catch (ProcessingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProcessingException(
                    ex.GetType() + " while creating stamper:" + Environment.NewLine + ex.Message, 25999);
            }

            return stamper;
        }

        private static PdfStamper DoCreateStamperAccordingToEncryptionAndSignature(string sourceFilename,
            string destinationFilename, ConversionProfile profile)
        {
            Logger.Debug("Started creating PdfStamper according to Encryption.");

            var reader = new PdfReader(sourceFilename);
            var fileStream = new FileStream(destinationFilename, FileMode.Create, FileAccess.Write);
            PdfStamper stamper = null;

            var pdfVersion = PdfWriter.VERSION_1_4;
            if (PDFProcessor.DeterminePdfVersion(profile).Equals("1.6"))
                pdfVersion = PdfWriter.VERSION_1_6;

            if (profile.PdfSettings.Signature.Enabled)
                stamper = PdfStamper.CreateSignature(reader, fileStream, pdfVersion);
            else
                stamper = new PdfStamper(reader, fileStream, pdfVersion);

            if (stamper == null) throw new ProcessingException("PDFStamper could not be created", 25200);
            return stamper;
        }
    }
}