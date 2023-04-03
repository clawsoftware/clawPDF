using System.Text;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using iText.Kernel.Pdf;
using NLog;

namespace clawSoft.clawPDF.PDFProcessing
{
    public static class Encrypter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void SetEncryption(WriterProperties writerProperties, ConversionProfile profile, JobPasswords jobPasswords)
        {
            if (!profile.PdfSettings.Security.Enabled)
                return;

            var encryption = CalculatePermissionValue(profile);
            Logger.Debug("Calculated Permission Value: " + encryption);

            if (string.IsNullOrEmpty(jobPasswords.PdfOwnerPassword))
            {
                Logger.Error("Launched encryption without owner password.");
                throw new ProcessingException("Launched encryption without owner password.", 25201);
            }

            var ownerPassword = Encoding.Default.GetBytes(jobPasswords.PdfOwnerPassword);

            byte[] userPassword = null;

            if (profile.PdfSettings.Security.RequireUserPassword)
            {
                if (string.IsNullOrEmpty(jobPasswords.PdfUserPassword))
                {
                    Logger.Error("Launched encryption without user password.");
                    throw new ProcessingException("Launched encryption without user password.", 25201);
                }
                userPassword = Encoding.Default.GetBytes(jobPasswords.PdfUserPassword);
            }

            switch (profile.PdfSettings.Security.EncryptionLevel)
            {
                case EncryptionLevel.Rc40Bit:
                    writerProperties.SetStandardEncryption(userPassword, ownerPassword,
                        encryption, EncryptionConstants.STANDARD_ENCRYPTION_40);
                    break;

                case EncryptionLevel.Rc128Bit:
                    writerProperties.SetStandardEncryption(userPassword, ownerPassword,
                        encryption, EncryptionConstants.STANDARD_ENCRYPTION_128);
                    break;

                case EncryptionLevel.Aes128Bit:
                    writerProperties.SetStandardEncryption(userPassword, ownerPassword,
                        encryption, EncryptionConstants.ENCRYPTION_AES_128);
                    break;

                case EncryptionLevel.Aes256Bit:
                    writerProperties.SetStandardEncryption(userPassword, ownerPassword,
                        encryption, EncryptionConstants.ENCRYPTION_AES_256);
                    break;
            }
        }

        /// <summary>
        ///     Calculates the PDF permission value that results in the settings from the given profile
        /// </summary>
        /// <param name="profile">The profile to do the calculations with.</param>
        /// <returns>An integer that encodes the PDF security permissions</returns>
        private static int CalculatePermissionValue(ConversionProfile profile)
        {
            var permissionValue = 0;

            if (profile.PdfSettings.Security.AllowPrinting) permissionValue = permissionValue | EncryptionConstants.ALLOW_PRINTING;
            if (profile.PdfSettings.Security.AllowToEditTheDocument)
                permissionValue = permissionValue | EncryptionConstants.ALLOW_MODIFY_CONTENTS;
            if (profile.PdfSettings.Security.AllowToCopyContent) permissionValue = permissionValue | EncryptionConstants.ALLOW_COPY;
            if (profile.PdfSettings.Security.AllowToEditComments)
                permissionValue = permissionValue | EncryptionConstants.ALLOW_MODIFY_ANNOTATIONS;

            if ((profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Rc128Bit)
                || (profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes128Bit)
                || (profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes256Bit))
            {
                if (profile.PdfSettings.Security.AllowPrinting)
                    if (profile.PdfSettings.Security.RestrictPrintingToLowQuality)
                        permissionValue = permissionValue ^ EncryptionConstants.ALLOW_PRINTING ^ EncryptionConstants.ALLOW_DEGRADED_PRINTING;
                //Remove higher bit of AllowPrinting
                if (profile.PdfSettings.Security.AllowToFillForms)
                    permissionValue = permissionValue | EncryptionConstants.ALLOW_FILL_IN; //Set automatically for 40Bit
                if (profile.PdfSettings.Security.AllowScreenReader)
                    permissionValue = permissionValue | EncryptionConstants.ALLOW_SCREENREADERS; //Set automatically for 40Bit
                if (profile.PdfSettings.Security.AllowToEditAssembly)
                    permissionValue = permissionValue | EncryptionConstants.ALLOW_ASSEMBLY; //Set automatically for 40Bit
            }
            return permissionValue;
        }
    }
}