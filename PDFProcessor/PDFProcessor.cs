using System;
using System.IO;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using iTextSharp.text.pdf;
using NLog;

namespace clawSoft.clawPDF.PDFProcessing
{
    public static class PDFProcessor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Check if processing is required.
        ///     Therefore the output format must be pdf and security or background page or signing must be enabled.
        ///     Furthermore PDF/A requires processing the metadata.
        /// </summary>
        /// <param name="profile">Profile with PDF settings</param>
        /// <returns>True if processing is required</returns>
        public static bool ProcessingRequired(ConversionProfile profile)
        {
            switch (profile.OutputFormat)
            {
                case OutputFormat.Pdf:
                case OutputFormat.PdfA2B:
                case OutputFormat.PdfX:
                    return profile.PdfSettings.Security.Enabled
                           || profile.BackgroundPage.Enabled
                           || profile.OutputFormat == OutputFormat.PdfA2B
                           || profile.PdfSettings.Signature.Enabled;
            }

            return false;
        }

        /// <summary>
        ///     Process PDF with updating metadata (for PDF/A), Encryption, adding background and signing according to profile.
        /// </summary>
        /// <param name="pdfFile"></param>
        /// <param name="profile"></param>
        /// <param name="jobPasswords"></param>
        public static void ProcessPDF(string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            if (!ProcessingRequired(profile))
            {
                Logger.Debug("No processing required.");
                return;
            }

            Logger.Debug("Start processing of " + pdfFile);

            if (!File.Exists(pdfFile))
                throw new ProcessingException("File in PdfProcessor does not exist: " + pdfFile, 26100);

            try
            {
                DoProcessPDF(pdfFile, profile, jobPasswords);
            }
            catch (ProcessingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.GetType() + " while processing file:" + pdfFile + Environment.NewLine + ex.Message);
                throw new ProcessingException(
                    ex.GetType() + " while processing file:" + pdfFile + Environment.NewLine + ex.Message, 26999);
            }
        }

        /// <summary>
        ///     Determine PDF-Version according to settings in conversion profile.
        /// </summary>
        /// <param name="profile">ConversionProfile</param>
        /// <returns>PDF Version as string, i.e. 1.6</returns>
        public static string DeterminePdfVersion(ConversionProfile profile)
        {
            var pdfVersion = "1.4";
            if (profile.PdfSettings.Security.Enabled &&
                profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes128Bit)
                pdfVersion = "1.6";
            return pdfVersion;
        }

        /// <summary>
        ///     Moves original file to preprocess file, which is the original file with an appended "_PrePdfProcessor.pdf".
        /// </summary>
        /// <param name="pdfFile">Full path to PDF file</param>
        /// <returns>Path to preprocess file</returns>
        public static string MoveFileToPreProcessFile(string pdfFile)
        {
            string preProcessFile;
            try
            {
                //create copy of original file
                preProcessFile = Path.ChangeExtension(pdfFile, "_PrePdfProcessor.pdf").Replace("._", "_");

                File.Move(pdfFile, preProcessFile);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.GetType() + " while creating pdf preprocess file:" + Environment.NewLine + ex.Message);
                throw new ProcessingException(
                    ex.GetType() + " while creating pdf preprocess file:" + Environment.NewLine + ex.Message, 26101);
            }

            return preProcessFile;
        }

        private static void DoProcessPDF(string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            string preProcessFile = null;
            try
            {
                preProcessFile = MoveFileToPreProcessFile(pdfFile);
                using (var stamper =
                    StamperCreator.CreateStamperAccordingToEncryptionAndSignature(preProcessFile, pdfFile, profile))
                {
                    Encrypter.SetEncryption(stamper, profile,
                        jobPasswords); //Encryption before adding Background and Signing!
                    XmpMetadataUpdater.UpdateXmpMetadata(stamper, profile);
                    BackgroundAdder.AddBackground(stamper, profile);
                    Signer.SignPdfFile(stamper, profile,
                        jobPasswords); //Signing after adding background and update metadata
                    stamper.Close();
                }
            }
            finally
            {
                //delete copy of original file
                if (!string.IsNullOrEmpty(preProcessFile))
                    if (File.Exists(preProcessFile))
                        File.Delete(preProcessFile);
            }
        }

        /// <summary>
        ///     Determines number of pages in PDF file
        /// </summary>
        /// <param name="pdfFile">Full path to PDF file</param>
        /// <returns>Number of pages in pdf file</returns>
        public static int GetNumberOfPages(string pdfFile)
        {
            var pdfReader = new PdfReader(pdfFile);
            var numberOfPages = pdfReader.NumberOfPages;
            pdfReader.Close();
            return numberOfPages;
        }
    }
}