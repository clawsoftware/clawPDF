using System.Text;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using iTextSharp.text.pdf;
using NLog;

namespace clawSoft.clawPDF.PDFProcessing
{
    public static class Encrypter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void SetEncryption(PdfStamper stamper, ConversionProfile profile, JobPasswords jobPasswords)
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
                    throw new ProcessingException("Launched encryption without user password.", 25202);
                }

                userPassword = Encoding.Default.GetBytes(jobPasswords.PdfUserPassword);
            }

            switch (profile.PdfSettings.Security.EncryptionLevel)
            {
                case EncryptionLevel.Rc40Bit:
                    stamper.SetEncryption(userPassword, ownerPassword, encryption, PdfWriter.STRENGTH40BITS);
                    break;

                case EncryptionLevel.Rc128Bit:
                    stamper.SetEncryption(userPassword, ownerPassword, encryption, PdfWriter.STRENGTH128BITS);
                    break;

                case EncryptionLevel.Aes128Bit:
                    stamper.SetEncryption(userPassword, ownerPassword, encryption, PdfWriter.ENCRYPTION_AES_128);
                    break;
                    //default:
                    //    break;
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

            if (profile.PdfSettings.Security.AllowPrinting)
                permissionValue = permissionValue | PdfWriter.ALLOW_PRINTING;
            if (profile.PdfSettings.Security.AllowToEditTheDocument)
                permissionValue = permissionValue | PdfWriter.ALLOW_MODIFY_CONTENTS;
            if (profile.PdfSettings.Security.AllowToCopyContent)
                permissionValue = permissionValue | PdfWriter.ALLOW_COPY;
            if (profile.PdfSettings.Security.AllowToEditComments)
                permissionValue = permissionValue | PdfWriter.ALLOW_MODIFY_ANNOTATIONS;

            if (profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Rc128Bit
                || profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes128Bit)
            {
                if (profile.PdfSettings.Security.AllowPrinting)
                    if (profile.PdfSettings.Security.RestrictPrintingToLowQuality)
                        permissionValue = permissionValue ^ PdfWriter.ALLOW_PRINTING ^
                                          PdfWriter.ALLOW_DEGRADED_PRINTING;
                //Remove higher bit of AllowPrinting
                if (profile.PdfSettings.Security.AllowToFillForms)
                    permissionValue = permissionValue | PdfWriter.ALLOW_FILL_IN; //Set automatically for 40Bit
                if (profile.PdfSettings.Security.AllowScreenReader)
                    permissionValue = permissionValue | PdfWriter.ALLOW_SCREENREADERS; //Set automatically for 40Bit
                if (profile.PdfSettings.Security.AllowToEditAssembly)
                    permissionValue = permissionValue | PdfWriter.ALLOW_ASSEMBLY; //Set automatically for 40Bit
            }

            return permissionValue;
        }
    }
}