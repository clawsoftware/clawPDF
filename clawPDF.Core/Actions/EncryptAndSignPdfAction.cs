using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NLog;

namespace clawSoft.clawPDF.Core.Actions
{
    /// <summary>
    ///     Implements the action to sign PDF-files
    /// </summary>
    public class EncryptAndSignPdfAction
    {
        private const int ActionId = 12;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     This method returns true if a password for a given certificate file is valid.
        /// </summary>
        /// <param name="certficateFilename">Name of p12 or pfx certificate file.</param>
        /// <param name="certifcatePassword">A password string.</param>
        /// <returns>
        ///     True if the password is valid.
        /// </returns>
        /// <example>
        ///     <code lang="C#">
        /// X509 x509 = new X509();
        /// string certificateFile = "MyCertificate.pfx";
        /// string password = "MyPassword";
        /// Console.WriteLine("Check the password for certifcate '" + certificateFile + "':");
        /// if (x509.IsValidCertificatePassword(certificateFile, password))
        ///  Console.WriteLine("The password '" + password + "' is valid!");
        /// else
        ///  Console.WriteLine("The password '" + password + "'is invalid!");
        /// password = "WrongPassword";
        /// if (x509.IsValidCertificatePassword(certificateFile, password))
        ///  Console.WriteLine("The password '" + password + "' is valid!");
        /// else
        ///  Console.WriteLine("The password '" + password + "'is invalid!");
        /// x509 = null;
        /// Console.ReadLine();
        /// </code>
        ///     <code lang="vbscript">
        /// Option Explicit
        /// Dim x509, password, certificateFile, resStr
        /// certificateFile = "MyCertificate.pfx"
        /// password = "MyPassword"
        /// resStr = "Check the password for certifcate '" + certificateFile + "':"
        /// Set x509 = WScript.CreateObject("clawSoft.x509.x509")
        /// if (pdf.IsValidCertificatePassword(certificateFile, password)) then
        ///  resStr = resStr + vbCrLf + "The password '" + password + "' is valid!"
        /// else
        ///  resStr = resStr + vbCrLf + "The password '" + password + "'is invalid!"
        /// end if
        /// password = "WrongPassword"
        /// if (pdf.IsValidCertificatePassword(certificateFile, password)) then
        ///  resStr = resStr + vbCrLf + "The password '" + password + "' is valid!"
        /// else
        ///  resStr = resStr + vbCrLf + "The password '" + password + "'is invalid!"
        /// end if
        /// Set x509 = Nothing
        /// MsgBox resStr
        /// </code>
        /// </example>
        public static bool IsValidCertificatePassword(string certficateFilename, string certifcatePassword)
        {
            try
            {
                var cert = new X509Certificate2(certficateFilename, certifcatePassword);
                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }
    }
}