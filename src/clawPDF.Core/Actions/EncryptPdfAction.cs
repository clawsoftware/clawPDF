using System;
using System.IO;
using iTextSharp.text.pdf;
using pdfforge.PDFCreator.Core.Jobs;
using pdfforge.PDFCreator.Core.Settings;
using NLog;

namespace pdfforge.PDFCreator.Core.Actions
{   
    /// <summary>
    /// Implements the action to encrypt PDF files
    /// </summary>
    public class EncryptPdfAction : IAction
    {
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// The profile that is used for encrypting the pdf file
        /// </summary>
        public ConversionProfile Profile { get; private set; }

        /// <summary>
        /// Creates a new EncryptPdfAction
        /// </summary>
        /// <param name="profile">The profile that is used for encrypting the pdf file</param>
        public EncryptPdfAction(ConversionProfile profile)
        {
            this.Profile = profile;
        }
        
        /// <summary>
        /// Encrypt a list of PDF files
        /// </summary>
        /// <param name="files">the files to process</param>
        /// <returns>the processed files</returns>
        public bool ProcessJob(IJob job)
        {
            logger.Debug("Launched Encrypt-Action");
            bool success = true;
            
            foreach (string file in job.OutputFiles)
            {
                if (!file.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                success = success && EncryptPDFFile(file, job);
            }

            return success;
        }

        /// <summary>
        /// Encrypt a PDF file. The destination must be different from the source
        /// </summary>
        /// <param name="sourceFilename">full path of the source file</param>
        /// <param name="destinationFilename">full path of the destination file</param>
        /// <returns>true upon success</returns>
        private bool EncryptPDFFile(string sourceFilename, string destinationFilename, IJob job)
        {
            if (Profile.PdfSettings.Security.Enable)
            {
                int encryption = callculatePermissionValue(Profile);
                logger.Trace("Callculated Permission Value: " + encryption);

                string userPassword = null;
                string ownerPassword = null;

                ownerPassword = job.PdfPasswords.OwnerPassword;
                if (Profile.PdfSettings.Security.RequireUserPassword)
                    userPassword = job.PdfPasswords.UserPassword;

                PdfReader reader = new PdfReader(sourceFilename); 
                FileStream fileStream = new FileStream(destinationFilename, FileMode.Create, FileAccess.Write);
                PdfStamper stamper = null;

                switch(Profile.PdfSettings.Security.EncryptionLevel)
                {
                    case EncryptionLevel.Low40Bit:
                        stamper = new PdfStamper(reader, fileStream, PdfWriter.VERSION_1_4);
                        stamper.SetEncryption(PdfWriter.STRENGTH40BITS, userPassword, ownerPassword, encryption);
                        break;
                    case EncryptionLevel.Medium128Bit:
                        stamper = new PdfStamper(reader, fileStream, PdfWriter.VERSION_1_4);
                        stamper.SetEncryption(PdfWriter.STRENGTH128BITS, userPassword, ownerPassword, encryption);
                        break;
                    case EncryptionLevel.High128BitAES:
                        stamper = new PdfStamper(reader, fileStream, PdfWriter.VERSION_1_6);
                        stamper.SetEncryption(PdfWriter.ENCRYPTION_AES_128, userPassword, ownerPassword, encryption);
                        break;
                    default:
                        break;
                }
                if (stamper != null)    
                    stamper.Close();
                reader.Close();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Calculates the PDF permission value that results in the settings from the given profile
        /// </summary>
        /// <param name="Profile">The profile to do the calculations with.</param>
        /// <returns>An integer that encodes the PDF security permissions</returns>
        public static int callculatePermissionValue(ConversionProfile Profile)
        {
            int encryption = 0;

            if (Profile.PdfSettings.Security.AllowPrinting) encryption = encryption | PdfWriter.ALLOW_PRINTING;
            if (Profile.PdfSettings.Security.AllowModifyContents) encryption = encryption | PdfWriter.ALLOW_MODIFY_CONTENTS;
            if (Profile.PdfSettings.Security.AllowCopy) encryption = encryption | PdfWriter.ALLOW_COPY;
            if (Profile.PdfSettings.Security.AllowModifiyAnnotations) encryption = encryption | PdfWriter.ALLOW_MODIFY_ANNOTATIONS;

            if ((Profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Medium128Bit)
                || (Profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.High128BitAES))
            {
                if (Profile.PdfSettings.Security.AllowDegradedPrinting) encryption = encryption | PdfWriter.ALLOW_DEGRADED_PRINTING; //Set automatically with AllowPrinting
                if (Profile.PdfSettings.Security.AllowFillIn) encryption = encryption | PdfWriter.ALLOW_FILL_IN; //Set automatically for 40Bit
                if (Profile.PdfSettings.Security.AllowScreenReaders) encryption = encryption | PdfWriter.ALLOW_SCREENREADERS; //Set automatically for 40Bit
                if (Profile.PdfSettings.Security.AllowAssembly) encryption = encryption | PdfWriter.ALLOW_ASSEMBLY; //Set automatically for 40Bit
            }
            return encryption;
        }

        /// <summary>
        /// Encrypt a PDF file.
        /// </summary>
        /// <param name="sourceFilename">full path of the source file</param>
        /// <returns>true upon success</returns>
        private bool EncryptPDFFile(string sourceFilename, IJob job)
        {
            string tmp = Path.GetTempFileName();
            bool ret = EncryptPDFFile(sourceFilename, tmp, job);

            if (ret)
            {
                try
                {
                    File.Copy(tmp, sourceFilename, true);
                    logger.Trace(sourceFilename + " encrypted!");
                }
                catch
                {
                    logger.Error("Could not copy file from tmp to " + sourceFilename);
                    throw;
                    // return false;
                }
            }
            else
                logger.Debug("Failed to encrypt " + sourceFilename);

            File.Delete(tmp);

            return ret;
        }
    }
}
