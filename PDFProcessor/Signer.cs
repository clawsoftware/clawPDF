using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NLog;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace clawSoft.clawPDF.PDFProcessing
{
    internal class Signer
    {
        //ActionId = 12;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Add a signature (set in profile) to a document, that is opened in the stamper.
        ///     The function does nothing, if signature settings are disabled.
        /// </summary>
        /// <param name="stamper">Stamper with document</param>
        /// <param name="profile">Profile with signature settings</param>
        /// <param name="jobPasswords">Passwords with PdfSignaturePassword</param>
        /// <exception cref="ProcessingException">In case of any error.</exception>
        public static void SignPdfFile(PdfStamper stamper, ConversionProfile profile, JobPasswords jobPasswords)
        {
            try
            {
                DoSignPdfFile(stamper, profile, jobPasswords);
            }
            catch (ProcessingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProcessingException(ex.GetType() + " while signing:" + Environment.NewLine + ex.Message,
                    12999);
            }
        }

        private static void DoSignPdfFile(PdfStamper stamper, ConversionProfile profile, JobPasswords jobPasswords)
        {
            var signing = profile.PdfSettings.Signature;

            if (!signing.Enabled) //Leave without signing
                return;

            Logger.Debug("Start signing file.");

            signing.CertificateFile = Path.GetFullPath(signing.CertificateFile);

            if (string.IsNullOrEmpty(jobPasswords.PdfSignaturePassword))
            {
                Logger.Error("Launched signing without certification password.");
                throw new ProcessingException("Launched signing without certification password.", 12204);
            }

            if (IsValidCertificatePassword(signing.CertificateFile, jobPasswords.PdfSignaturePassword) == false)
            {
                Logger.Error("Canceled signing. The password for certificate '" + signing.CertificateFile +
                             "' is wrong.");
                throw new ProcessingException(
                    "Canceled signing. The password for certificate '" + signing.CertificateFile + "' is wrong.",
                    12200);
            }

            if (CertificateHasPrivateKey(signing.CertificateFile, jobPasswords.PdfSignaturePassword) == false)
            {
                Logger.Error("Canceled signing. The certificate '" + signing.CertificateFile + "' has no private key.");
                throw new ProcessingException(
                    "Canceled signing. The certificate '" + signing.CertificateFile + "' has no private key.", 12201);
            }

            var fsCert = new FileStream(signing.CertificateFile, FileMode.Open);
            var ks = new Pkcs12Store(fsCert, jobPasswords.PdfSignaturePassword.ToCharArray());
            string alias = null;
            foreach (string al in ks.Aliases)
                if (ks.IsKeyEntry(al) && ks.GetKey(al).Key.IsPrivate)
                {
                    alias = al;
                    break;
                }

            fsCert.Close();
            ICipherParameters pk = ks.GetKey(alias).Key;
            var x = ks.GetCertificateChain(alias);
            var chain = new X509Certificate[x.Length];
            for (var k = 0; k < x.Length; ++k)
                chain[k] = x[k].Certificate;

            ITSAClient tsc = null;
            if (!string.IsNullOrEmpty(signing.TimeServerUrl.Trim()))
            {
                if (!signing.TimeServerIsSecured)
                    tsc = new TSAClientBouncyCastle(signing.TimeServerUrl);
                else
                    tsc = new TSAClientBouncyCastle(signing.TimeServerUrl, signing.TimeServerLoginName,
                        signing.TimeServerPassword);
            }

            var psa = stamper.SignatureAppearance;
            if (tsc == null)
                psa.SetCrypto(pk, chain, null, PdfSignatureAppearance.WINCER_SIGNED);
            else
                psa.SetCrypto(null, chain, null, PdfSignatureAppearance.SELF_SIGNED);

            if (!profile.PdfSettings.Signature.AllowMultiSigning)
                //Lock PDF, except for annotations and form filling (irrelevant for clawPDF)
                psa.CertificationLevel = PdfSignatureAppearance.CERTIFIED_FORM_FILLING_AND_ANNOTATIONS;

            psa.Reason = signing.SignReason;
            psa.Contact = signing.SignContact;
            psa.Location = signing.SignLocation;

            if (signing.DisplaySignatureInDocument)
            {
                var signPage = SignPageNr(stamper, signing);

                psa.SetVisibleSignature(new Rectangle(signing.LeftX, signing.LeftY, signing.RightX, signing.RightY),
                    signPage, null);
            }

            var dic = new PdfSignature(PdfName.ADOBE_PPKLITE, new PdfName("adbe.pkcs7.detached"));
            dic.Reason = psa.Reason;
            dic.Location = psa.Location;
            dic.Contact = psa.Contact;
            dic.Date = new PdfDate(psa.SignDate);
            psa.CryptoDictionary = dic;

            const int contentEstimated = 15000;
            // Preallocate excluded byte-range for the signature content (hex encoded)
            var exc = new Dictionary<PdfName, int>();
            exc[PdfName.CONTENTS] = contentEstimated * 2 + 2;
            psa.PreClose(exc);
            const string hashAlgorithm = "SHA1"; //Always use HashAlgorithm "SHA1"
            var sgn = new PdfPKCS7(pk, chain, null, hashAlgorithm, false);
            var messageDigest = DigestUtilities.GetDigest(hashAlgorithm);
            var data = psa.GetRangeStream();
            var buf = new byte[8192];
            int n;
            while ((n = data.Read(buf, 0, buf.Length)) > 0) messageDigest.BlockUpdate(buf, 0, n);
            var hash = new byte[messageDigest.GetDigestSize()];
            messageDigest.DoFinal(hash, 0);
            byte[] ocsp = null;
            if (chain.Length >= 2)
            {
                var url = PdfPKCS7.GetOCSPURL(chain[0]);
                if (!string.IsNullOrEmpty(url))
                    ocsp = new OcspClientBouncyCastle().GetEncoded(chain[0], chain[1], url);
            }

            var cal = psa.SignDate;
            var sh = sgn.GetAuthenticatedAttributeBytes(hash, cal, ocsp);
            sgn.Update(sh, 0, sh.Length);

            var paddedSig = new byte[contentEstimated];

            if (tsc != null)
            {
                byte[] encodedSigTsa = null;
                try
                {
                    encodedSigTsa = sgn.GetEncodedPKCS7(hash, cal, tsc, ocsp);
                    Array.Copy(encodedSigTsa, 0, paddedSig, 0, encodedSigTsa.Length);
                }
                catch (Exception ex)
                {
                    throw new ProcessingException(
                        ex.GetType() + " while connecting to timeserver (can't connect to timeserver): " + ex.Message,
                        12205);
                }

                if (contentEstimated + 2 < encodedSigTsa.Length)
                    throw new ProcessingException(
                        "Not enough space for signature", 12202);
            }
            else
            {
                var encodedSig = sgn.GetEncodedPKCS7(hash, cal);
                Array.Copy(encodedSig, 0, paddedSig, 0, encodedSig.Length);
                if (contentEstimated + 2 < encodedSig.Length)
                    throw new ProcessingException("Not enough space for signature", 12203);
            }

            var dic2 = new PdfDictionary();
            dic2.Put(PdfName.CONTENTS, new PdfString(paddedSig).SetHexWriting(true));
            psa.Close(dic2);
        }

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
        /// resStr = "Check the password for certificate '" + certificateFile + "':"
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

        /// <summary>
        ///     This method returns true if a certificate has a private key.
        /// </summary>
        /// <param name="certficateFilename">Name of p12 or pfx certificate file.</param>
        /// <param name="certifcatePassword">A password string.</param>
        /// <returns>
        ///     True if the certificate has a private key.
        /// </returns>
        /// <example>
        ///     <code lang="C#">
        /// X509 x509 = new X509();
        /// string certificateFile = "MyCertificate.pfx";
        /// string password = "MyPassword";
        /// if (x509.CertificateHasPrivateKey(certificateFile, password))
        ///  Console.WriteLine("The certificate '{0}' has a private key.", certificateFile);
        /// else
        ///  Console.WriteLine("The certificate '{0}' has NO private key.", certificateFile);
        /// x509 = null;
        /// Console.ReadLine();
        /// </code>
        ///     <code lang="vbscript">
        /// Option Explicit
        /// Dim x509, password, certificateFile
        /// certificateFile = "MyCertificate.pfx"
        /// password = "MyPassword"
        /// Set x509 = WScript.CreateObject("clawSoft.x509.x509")
        /// if (pdf.CertificateHasPrivateKey(certificateFile, password)) then
        ///  MsgBox "The certifcate '" + certificateFile + "' has a private key."
        /// else
        ///  MsgBox "The certifcate '" + certificateFile + "' has a NO private key."
        /// end if
        /// Set x509 = Nothing
        /// </code>
        /// </example>
        public static bool CertificateHasPrivateKey(string certficateFilename, string certifcatePassword)
        {
            var cert = new X509Certificate2(certficateFilename, certifcatePassword);
            if (cert.HasPrivateKey)
                return true;
            return false;
        }

        private static int SignPageNr(PdfStamper stamper, Signature signing)
        {
            switch (signing.SignaturePage)
            {
                case SignaturePage.CustomPage:
                    if (signing.SignatureCustomPage > stamper.Reader.NumberOfPages)
                        return stamper.Reader.NumberOfPages;
                    if (signing.SignatureCustomPage < 1)
                        return 1;
                    return signing.SignatureCustomPage;

                case SignaturePage.LastPage:
                    return stamper.Reader.NumberOfPages;
                //case SignaturePosition.FirstPage:
                default:
                    return 1;
            }
        }
    }
}