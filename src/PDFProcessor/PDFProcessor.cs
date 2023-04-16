using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Utilities;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Signatures;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace clawSoft.clawPDF.PDFProcessing
{
    public static class PDFProcessor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Determine PDF-Version according to settings in conversion profile.
        /// </summary>
        /// <param name="profile">ConversionProfile</param>
        /// <returns>PDF Version as string, i.e. 1.6</returns>
        public static string DeterminePdfVersion(ConversionProfile profile)
        {
            var pdfVersion = "1.4";
            if (profile.PdfSettings.Security.Enabled &&
                (profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes128Bit || profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes256Bit))
                pdfVersion = "1.6";
            return pdfVersion;
        }

        /// <summary>
        ///     Determines number of pages in PDF file
        /// </summary>
        /// <param name="pdfFile">Full path to PDF file</param>
        /// <returns>Number of pages in pdf file</returns>
        public static int GetNumberOfPages(string pdfFile)
        {
            var pdfReader = new PdfDocument(new PdfReader(pdfFile));
            var numberOfPages = pdfReader.GetNumberOfPages();
            pdfReader.Close();
            return numberOfPages;
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
                case OutputFormat.PdfA1B:
                case OutputFormat.PdfA2B:
                case OutputFormat.PdfA3B:
                case OutputFormat.PdfImage32:
                case OutputFormat.PdfImage24:
                case OutputFormat.PdfImage8:
                case OutputFormat.PdfOCR32:
                case OutputFormat.PdfOCR24:
                case OutputFormat.PdfOCR8:
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

        private static void AddEncryption(string preProcessFile, string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            if (!profile.PdfSettings.Security.Enabled)
                return;

            var docContext = OpenPdfDocument(preProcessFile, pdfFile, profile, jobPasswords);

            WritePdfDocument(docContext);
        }

        public static void AddBackground(string preProcessFile, string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            if (!profile.BackgroundPage.Enabled)
                return;

            var docContext = OpenPdfDocument(preProcessFile, pdfFile, profile, jobPasswords);

            BackgroundAdder.AddBackground(docContext.Document, profile);
            WritePdfDocument(docContext);
        }

        private static void ApplyEncryptionToWriterProperties(WriterProperties writerProperties, ConversionProfile profile, JobPasswords jobPasswords)
        {
            if (!profile.PdfSettings.Security.Enabled)
                return;

            Encrypter.SetEncryption(writerProperties, profile, jobPasswords);
        }

        private static void DoProcessPDF(string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            string preProcessFile = null;
            try
            {
                preProcessFile = MoveFileToPreProcessFile(pdfFile);
                AddEncryption(preProcessFile, pdfFile, profile, jobPasswords);
                AddBackground(preProcessFile, pdfFile, profile, jobPasswords);
                SignEncryptConvertAndWriteFile(preProcessFile, pdfFile, profile, jobPasswords);
            }
            finally
            {
                //delete copy of original file
                if (!string.IsNullOrEmpty(preProcessFile))
                    if (File.Exists(preProcessFile))
                        File.Delete(preProcessFile);
            }
        }

        private static void DoSignEncryptAndConvertAndWritePdf(string preProcessFile, string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            var docContext = OpenPdfDocument(preProcessFile, pdfFile, profile, jobPasswords);
            SetStartPage(docContext.Document, profile);
            WritePdfDocument(docContext);
            SignPdf(docContext, profile, jobPasswords);
        }

        private static PdfReader GetReader(ConversionProfile profile, string sourceFile)
        {
            ReaderProperties readerProperties = new ReaderProperties();

            if (profile.PdfSettings.Security.Enabled)
            {
                readerProperties.SetPassword(Encoding.Default.GetBytes(profile.PdfSettings.Security.OwnerPassword));
            }
            return new PdfReader(sourceFile, readerProperties);
        }

        private static DocumentContext OpenPdfDocument(string preProcessFile, string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            DocumentContext documentContext;

            documentContext.OriginalFileName = preProcessFile;
            documentContext.TempFileName = pdfFile;

            var writerProperties = new WriterProperties();

            if (profile.PdfSettings.Security.Enabled)
                ApplyEncryptionToWriterProperties(writerProperties, profile, jobPasswords);

            try
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(jobPasswords.PdfOwnerPassword);
                PdfReader pdfReader = new PdfReader(documentContext.OriginalFileName, new ReaderProperties().SetPassword(passwordBytes));
                PdfWriter pdfWriter = new PdfWriter(documentContext.TempFileName, writerProperties);
                documentContext.Document = new PdfDocument(pdfReader, pdfWriter);

                return documentContext;
            }
            catch (Exception e)
            {
                Logger.Trace(e.Message);
                throw;
            }
        }

        private static void SetStartPage(PdfDocument pdfDocument, ConversionProfile profile)
        {
            var pageIndex = profile.PdfSettings.ViewerStartsOnPage;
            // index gets limited to at least 1 and the number of documents
            pageIndex = Math.Max(1, Math.Min(pageIndex, pdfDocument.GetNumberOfPages()));

            var navigationPage = pdfDocument.GetPage(pageIndex);
            pdfDocument.GetCatalog().SetOpenAction(PdfExplicitDestination.CreateXYZ(navigationPage, 0, 0, 1));
        }

        private static void SignEncryptConvertAndWriteFile(string preProcessFile, string pdfFile, ConversionProfile profile, JobPasswords jobPasswords)
        {
            if (!profile.PdfSettings.Signature.Enabled)
                return;

            Logger.Debug("Start processing of " + pdfFile);

            if (!File.Exists(pdfFile))
            {
                throw new ProcessingException("_file in PdfProcessor does not exist: " + pdfFile, 1);
            }

            const int retryCount = 5;
            var retryInterval = TimeSpan.FromMilliseconds(300);

            try
            {
                // Retry signing when a ProcessingException with the ErrorCode Signature_NoTimeServerConnection was thrown
                Retry.Do(
                         () => DoSignEncryptAndConvertAndWritePdf(preProcessFile, pdfFile, profile, jobPasswords),
                         retryInterval: retryInterval,
                         retryCount: retryCount,
                         retryCondition: ex => (ex as ProcessingException)?.ErrorCode == 1);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerExceptions.First();
            }
            catch (ProcessingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.GetType() + " while processing file:" + pdfFile);
                throw new ProcessingException(
                    ex.GetType() + " while processing file:" + pdfFile + Environment.NewLine + ex.Message, 1);
            }
        }

        private static void SignPdf(DocumentContext docContext, ConversionProfile profile, JobPasswords jobPasswords)
        {
            if (!profile.PdfSettings.Signature.Enabled)
                return;

            ReaderProperties readerProperties = new ReaderProperties();

            if (profile.PdfSettings.Security.Enabled)
                readerProperties.SetPassword(Encoding.Default.GetBytes(profile.PdfSettings.Security.OwnerPassword));

            using (PdfReader pdfReader = GetReader(profile, docContext.OriginalFileName))
            using (PdfWriter pdfWriter = new PdfWriter(docContext.TempFileName))
            {
                var signer = new PdfSigner(pdfReader, pdfWriter, new StampingProperties().PreserveEncryption().UseAppendMode());
                using (signer.GetDocument())
                {
                    try
                    {
                        new Signer().SignPdfFile(signer, profile, jobPasswords);
                    }
                    catch (ProcessingException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn($"Could not close iText pdf stamper.\r\n{ex}");
                    }
                }
            }
        }

        private static void WritePdfDocument(DocumentContext context)
        {
            try
            {
                context.Document.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Exception while closing pdfDocument ");
                throw;
            }
            finally
            {
                File.Copy(context.TempFileName, context.OriginalFileName, true);
            }
        }

        public struct DocumentContext
        {
            public PdfDocument Document;
            public string OriginalFileName;
            public string TempFileName;
        }
    }
}