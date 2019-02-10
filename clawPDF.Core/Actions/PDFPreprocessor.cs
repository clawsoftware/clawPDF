using System;
using System.IO;
using System.Text;
using System.Xml;
using iTextSharp.text.pdf;
using NLog;
using pdfforge.PDFCreator.Core.Jobs;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using iTextSharp.text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using pdfforge.PDFCreator.Core.Settings;
using pdfforge.PDFCreator.Core.Settings.Enums;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

namespace pdfforge.PDFCreator.Core.Actions
{
    /// <summary>
    ///     Implements the action to sign PDF-files
    /// </summary>
    public class PdfPreProcessor : ICheckable
    {
        private const int ActionId = 99;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Sign a list of PDF files
        /// </summary>
        /// <param name="pdfFile"></param>
        /// <param name="job">The job to process</param>
        /// <returns>An ActionResult to determine the success and a list of errors</returns>
        public ActionResult ProcessPDF(string pdfFile, IJob job)
        {
            Logger.Debug("Launched PdfPreProcessor for: " + pdfFile);

            ActionResult result;

            if (pdfFile.EndsWith(".pdf"))
            {
                Logger.Debug("File in PdfPreProcessor in no PDF: " + pdfFile);
                return null; //ActionResult!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
            if (!File.Exists(pdfFile))
            {
                Logger.Debug("File in PdfPreProcessor does not exist: " + pdfFile);
                return null; //ActionResult!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }

            string preProcessFile = pdfFile.Replace(".pdf", "_PreProcess.pdf");

            File.Move(pdfFile, preProcessFile);

            try
            {
                var basicCheckResult = Check(job.Profile);
                if (!basicCheckResult)
                {
                    Logger.Error("Canceled PDFPreprocessor");
                    return basicCheckResult;
                }

                //Check passwords..
                //Check passwords during stamper creation
                //Stamper as Attribute???

                var stamper = CreateStamperAccordingToEncryption(preProcessFile, pdfFile, job);
                
                
                result = SignPdfFile(stamper, job);
                
                if (!result.Success)
                    return result;
                return new ActionResult();
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in Encrypt and Sign Action\r\n" + ex.Message); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return new ActionResult(ActionId, 999);
            }
        }

        public ActionResult Check(ConversionProfile profile)
        {
            var result = CheckBackgroundSettings(profile);
            result.AddRange(CheckEncryptAndSignSettings(profile));

            return result;
        }

        #region Background
        
        private ActionResult AddBackgroundToPdf(string sourceFilename, string destinationFilename, IJob job)
        {
            Logger.Trace("Start adding Background to " + sourceFilename);

            var pdfReader = new PdfReader(sourceFilename);
            int nFile = pdfReader.NumberOfPages;

            var backgroundPdfReader = new PdfReader(job.Profile.BackgroundPage.File);
            Logger.Debug("BackgroundFile: " + Path.GetFullPath(job.Profile.BackgroundPage.File));
            int nBackground = backgroundPdfReader.NumberOfPages;

            int startOffset; //number of front pages without background 
            try
            {
                startOffset = StartOffsetAccordingToCover(job.Profile);
            }
            catch (Exception ex)
            {
                Logger.Error("Excpetion while detecting number of pages of the cover file: " + job.Profile.AttachmentPage.File + "\r\n" + ex);
                return new ActionResult(ActionId, 105);
            }

            int endPage; //last page with background
            try
            {
                endPage = nFile - EndOffsetAccordingToAttachment(job.Profile);
            }
            catch (Exception ex)
            {
                Logger.Error("Excpetion while detecting number of pages of the attachment file: " + job.Profile.AttachmentPage.File + "\r\n" + ex);
                return new ActionResult(ActionId, 106);
            }

            var stream = new FileStream(destinationFilename, FileMode.Create, FileAccess.Write);
            var stamper = new PdfStamper(pdfReader, stream);

            for (int i = 1; i <= nFile; i++)
            {
                int backgroundPage = GetBackgroundPageNumber(i, nBackground, job.Profile.BackgroundPage.Repeat, startOffset, endPage);
                if (backgroundPage < 1)
                    continue;
                PdfImportedPage page = stamper.GetImportedPage(backgroundPdfReader, backgroundPage);
                PdfContentByte background = stamper.GetUnderContent(i);

                Rectangle wmsize = backgroundPdfReader.GetPageSize(backgroundPage);
                Rectangle pdfsize = pdfReader.GetPageSize(i);

                if ((pdfReader.GetPageRotation(i) == 90) || (pdfReader.GetPageRotation(i) == 270))
                {
                    background.AddTemplate(page, 0, -1f, 1f, 0, ((pdfsize.Height - wmsize.Height) / 2),
                        wmsize.Width + (pdfsize.Width - wmsize.Width) / 2); //270°
                    Logger.Debug("BackgroundPage " + backgroundPage + " turned right, because of automatic rotation of page " + i);
                }
                else if ((pdfsize.Height < pdfsize.Width) && (wmsize.Height > wmsize.Width))
                {
                    //pdfsize.height and width must be changed for this calculation, because the page was rotated     
                    background.AddTemplate(page, 0, -1f, 1f, 0, (pdfsize.Width - wmsize.Height) / 2,
                        wmsize.Width + ((pdfsize.Height - wmsize.Width) / 2)); //270°
                    Logger.Debug("BackgroundPage " + backgroundPage + " turned right to fit better in page " + i);
                }
                else
                    background.AddTemplate(page, (pdfsize.Width - wmsize.Width) / 2, (pdfsize.Height - wmsize.Height) / 2);
                //0° 
            }

            //                              cos  sin  -sin  cos  dx  dy
            //background.AddTemplate(page,  0,   1f,  -1f,   0,  0,  0 ); //90°
            //background.AddTemplate(page, -1f,   0,    0, -1f,  0,  0 ); //180°
            //background.AddTemplate(page,  0,  -1f,   1f,   0,  0,  0 ); //270°
            //background.AddTemplate(page,  1f,   0,    0,  1f,  0,  0 ); //0°

            stamper.Close();
            return new ActionResult();
        }

        private int StartOffsetAccordingToCover(ConversionProfile profile)
        {
            int nCover = 0;
            if (profile.CoverPage.Enable)
            {
                if (!profile.CoverPage.AddBackground)
                {
                    var coverPdfReader = new PdfReader(profile.CoverPage.File);
                    nCover = coverPdfReader.NumberOfPages;
                }
            }
            return nCover;
        }

        private int EndOffsetAccordingToAttachment(ConversionProfile profile)
        {
            int nAttachment = 0;
            if (profile.AttachmentPage.Enable)
            {
                if (!profile.AttachmentPage.AddBackground)
                {
                    var attachmentPdfReader = new PdfReader(profile.AttachmentPage.File);
                    nAttachment = attachmentPdfReader.NumberOfPages;
                }
            }
            return nAttachment;
        }

        private static int GetBackgroundPageNumber(int currentDocumentPage, int numberOfBackgroundpages, BackgroundRepeat repeat, int startOffset, int endPage)
        {
            if (currentDocumentPage <= startOffset)
                return -1;
            if (currentDocumentPage > endPage)
                return -1;

            switch (repeat)
            {
                case BackgroundRepeat.RepeatAllPages:
                    return (((currentDocumentPage - startOffset - 1) % numberOfBackgroundpages) + 1);
                case BackgroundRepeat.RepeatLastPage:
                    if ((currentDocumentPage - startOffset) < numberOfBackgroundpages)
                        return currentDocumentPage - startOffset;
                    //else if (currentDocumentPage - startOffset >= numberOfBackgroundpages)
                    return numberOfBackgroundpages;
                case BackgroundRepeat.NoRepetition:
                    if ((currentDocumentPage - startOffset) <= numberOfBackgroundpages)
                        return currentDocumentPage - startOffset;
                    //else
                    return -1;
            }
            return -1; //default
        }

        public ActionResult CheckBackgroundSettings(ConversionProfile profile)
        {
            var actionResult = new ActionResult();

            if ((profile.OutputFormat != OutputFormat.Pdf) && (profile.OutputFormat != OutputFormat.PdfA) && (profile.OutputFormat != OutputFormat.PdfX))
                return actionResult;

            if (profile.BackgroundPage.Enable)
            {
                if (string.IsNullOrEmpty(profile.BackgroundPage.File))
                {
                    Logger.Error("No background file is specified.");
                    actionResult.Add(ActionId, 100);
                }
                else if (!File.Exists(profile.BackgroundPage.File))
                {
                    Logger.Error("The background file \"" + profile.BackgroundPage.File + "\" does not exist.");
                    actionResult.Add(ActionId, 101);
                }
                else if (!profile.BackgroundPage.File.EndsWith(".pdf"))
                {
                    Logger.Error("The background file \"" + profile.BackgroundPage.File + "\" is no pdf file.");
                    actionResult.Add(ActionId, 102);
                }
            }
            return actionResult;
        }
        #endregion

        #region Update XMP Metadata for PDF/A
        /// <summary>
        /// This method updates the the xmp metadata of a given pdf file.
        /// </summary>
        /// <param name="sourceFilename">Name of the pdf source file.</param>
        /// <param name="destinationFilename">Name of the pdf destination file.</param>
        private ActionResult UpdateXMPMetadata(string sourceFilename, string destinationFilename)
        {
            var ms = new PDFMetadataStrings("", "", "", "", "", "", "", "");
            var reader = new PdfReader(sourceFilename);

            var doc = new XmlDocument();
            string xmlMetadataString = Encoding.UTF8.GetString(reader.Metadata);
            doc.LoadXml(xmlMetadataString);
            string pdfa = "";
            if (xmlMetadataString.IndexOf("pdfaid:part='1'", StringComparison.InvariantCultureIgnoreCase) >= 0)
                pdfa = "<pdfaid:part>1</pdfaid:part>\n";
            else if (xmlMetadataString.IndexOf("pdfaid:part='2'", StringComparison.InvariantCultureIgnoreCase) >= 0)
                pdfa = "<pdfaid:part>2</pdfaid:part>\n";
            string pdfaConformance = "";
            if (xmlMetadataString.IndexOf("pdfaid:conformance='A'", StringComparison.InvariantCultureIgnoreCase) >= 0)
                pdfaConformance = "<pdfaid:conformance>A</pdfaid:conformance>\n";
            else if (xmlMetadataString.IndexOf("pdfaid:conformance='B'", StringComparison.InvariantCultureIgnoreCase) >= 0)
                pdfaConformance = "<pdfaid:conformance>B</pdfaid:conformance>\n";

            var documentIDs = (PdfArray)(reader.Trailer.Get(PdfName.ID));

            string sDocumentID = GetHEXString(GetRandomString(16));
            if (documentIDs != null)
            {
                var o = (PdfObject)documentIDs.ArrayList[0];
                string s = "";
                s = o.ToString();
                if (s.Length > 0)
                {
                    sDocumentID = GetHEXString(s);
                }
            }

            string xmlAuthor = "";
            if (reader.Info.ContainsKey("Author"))
            {
                ms.Author = reader.Info["Author"];
                xmlAuthor = "    <dc:creator>\n" +
                    "     <rdf:Seq>\n" +
                    "      <rdf:li>" + ms.Author + "</rdf:li>\n" +
                    "     </rdf:Seq>\n" +
                    "    </dc:creator>\n";
            }

            if (reader.Info.ContainsKey("CreationDate"))
                ms.CreationDate = reader.Info["CreationDate"];

            if (reader.Info.ContainsKey("Creator"))
                ms.Creator = reader.Info["Creator"].ToString();

            string xmlKeywords = "", xmlKeywords2 = "";
            if (reader.Info.ContainsKey("Keywords"))
            {
                ms.Keywords = reader.Info["Keywords"].ToString();
                xmlKeywords = "    <dc:subject>\n" +
                    "     <rdf:Bag>\n" +
                    "      <rdf:li>" + ms.Keywords + "</rdf:li>\n" +
                    "     </rdf:Bag>\n" +
                    "    </dc:subject>\n";
                xmlKeywords2 = "    <pdf:Keywords>" + ms.Keywords + "</pdf:Keywords>\n";
            }

            if (reader.Info.ContainsKey("ModDate"))
                ms.ModDate = reader.Info["ModDate"].ToString();

            if (reader.Info.ContainsKey("Producer"))
                ms.Producer = reader.Info["Producer"].ToString();

            string xmlSubject = "";
            if (reader.Info.ContainsKey("Subject"))
            {
                ms.Subject = reader.Info["Subject"].ToString();
                xmlSubject = "    <dc:description>\n" +
                    "     <rdf:Alt>\n" +
                    "      <rdf:li xml:lang='x-default'>" + ms.Subject + "</rdf:li>\n" +
                    "     </rdf:Alt>\n" +
                    "    </dc:description>\n";
            }

            string xmlTitle = "";
            if (reader.Info.ContainsKey("Title"))
            {
                ms.Title = reader.Info["Title"].ToString();
                xmlTitle = "    <dc:title>\n" +
                "     <rdf:Alt>\n" +
                "      <rdf:li xml:lang='x-default'>" + ms.Title + "</rdf:li>\n" +
                "     </rdf:Alt>\n" +
                "    </dc:title>\n";
            }

            string metadataStr = "";
            metadataStr =
                "<?xpacket begin='﻿' id='W5M0MpCehiHzreSzNTczkc9d'?>\n" +
                " <x:xmpmeta xmlns:x='adobe:ns:meta/' x:xmptk='Adobe XMP Core 4.2.1-c041 52.342996, 2008/05/07-20:48:00'>\n" +
                "  <rdf:RDF xmlns:rdf='http://www.w3.org/1999/02/22-rdf-syntax-ns#'>\n" +
                "   <rdf:Description rdf:about='' xmlns:pdfaid='http://www.aiim.org/pdfa/ns/id/'>\n" +
                pdfa +
                pdfaConformance +
                "   </rdf:Description>\n" +
                "   <rdf:Description rdf:about=''\n" +
                "     xmlns:xmp='http://ns.adobe.com/xap/1.0/'>\n" +
                "    <xmp:CreateDate>" + GetXMPDate(ms.CreationDate) + "</xmp:CreateDate>\n" +
                "    <xmp:ModifyDate>" + GetXMPDate(ms.ModDate) + "</xmp:ModifyDate>\n" +
                "    <xmp:CreatorTool>" + ms.Creator + "</xmp:CreatorTool>\n" +
                "   </rdf:Description>\n" +
                "   <rdf:Description rdf:about=''\n" +
                "     xmlns:dc='http://purl.org/dc/elements/1.1/'>\n" +
                "    <dc:format>application/pdf</dc:format>\n" + xmlTitle + xmlSubject + xmlAuthor + xmlKeywords +
                "   </rdf:Description>\n" +
                "   <rdf:Description rdf:about=''\n" +
                "     xmlns:xmpMM='http://ns.adobe.com/xap/1.0/mm/'\n" +
                "     xmlns:stEvt='http://ns.adobe.com/xap/1.0/sType/ResourceEvent#'>\n" +
                "    <xmpMM:DocumentID>uuid:" + sDocumentID + "</xmpMM:DocumentID>\n" +
                "    <xmpMM:History><rdf:Seq><rdf:li rdf:parseType='Resource'></rdf:li></rdf:Seq></xmpMM:History>\n" +
                "   </rdf:Description>\n" +
                "   <rdf:Description rdf:about=''\n" +
                "     xmlns:pdf='http://ns.adobe.com/pdf/1.3/'>\n" +
                "    <pdf:Producer>" + ms.Producer + "</pdf:Producer>\n" +
                xmlKeywords2 +
                "   </rdf:Description>\n" +
                "  </rdf:RDF>\n" +
                " </x:xmpmeta>\n" +
                "<?xpacket end='w'?>";

            Encoding textEncoding = Encoding.GetEncoding("iso-8859-1");
            byte[] newMetadata = Encoding.Convert(Encoding.Default, Encoding.UTF8, textEncoding.GetBytes(metadataStr));

            var os = new FileStream(destinationFilename, FileMode.Create);
            var stamper = new PdfStamper(reader, os);
            stamper.Writer.XmpMetadata = newMetadata;
            stamper.Close();
            os.Close();

            return new ActionResult(); ////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private struct PDFMetadataStrings
        {
            public string Author, CreationDate, Creator, Keywords, ModDate, Producer, Subject, Title;
            public PDFMetadataStrings(string author, string creationDate, string creator, string keywords, string modDate, string producer, string subject, string title)
            {
                Author = author;
                CreationDate = creationDate;
                Creator = creator;
                Keywords = keywords;
                ModDate = modDate;
                Producer = producer;
                Subject = subject;
                Title = title;
            }
        }

        /// <summary>
        /// Convert a string to a hex string.
        /// </summary>
        /// <param name="s">
        /// A string to convert.
        /// </param>
        /// <returns>
        /// The hex string.
        /// </returns>
        private string GetHEXString(string s)
        {
            string ts = "";
            var sb = new StringBuilder();
            foreach (char t in s)
            {
                ts = Convert.ToInt32(t).ToString("x");
                if (ts.Length == 1) ts = "0" + ts;
                sb.Append(ts);
            }
            s = sb.ToString().ToUpper();
            return s;
        }

        /// <summary>
        /// Get a string with random chars.
        /// </summary>
        /// <param name="length">Length of he random string.</param>
        /// <returns>A string that contains random chars.</returns>
        private string GetRandomString(int length)
        {
            var rnd = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(Convert.ToChar(((byte)rnd.Next(254))).ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converts a metadata date to a xmp date.
        /// </summary>
        /// <param name="metadataDate">
        /// A string with a metadata date.
        /// </param>
        /// <returns>
        /// The xmp date.
        /// </returns>
        private string GetXMPDate(string metadataDate)
        {
            if (metadataDate.Length != 23)
                return metadataDate;
            string s;
            s = metadataDate.Substring(2, 4) + "-" + metadataDate.Substring(6, 2) + "-" + metadataDate.Substring(8, 2) + "T" +
                metadataDate.Substring(10, 2) + ":" + metadataDate.Substring(12, 2) + ":" + metadataDate.Substring(14, 2) + "+" +
                metadataDate.Substring(17, 2) + ":" + metadataDate.Substring(20, 2);

            return s;
        }
    
        #endregion

        #region Encryption And Signing

        private ActionResult SignPdfFile(PdfStamper stamper, IJob job)
        {
            Signing s = job.Profile.PdfSettings.Signing;

            //Leave without signing //WEG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (!s.Enable)
            {
                if (stamper != null)
                {
                    stamper.Close();
                    return new ActionResult();
                }

                Logger.Error("Could not create Stamper for Encryption, without Signing");
                return new ActionResult(ActionId, 104);
            }

            //Continue for Signing
            s.CertificationFile = Path.GetFullPath(s.CertificationFile);

            if (IsValidCertificatePassword(s.CertificationFile, job.Passwords.PdfSignaturePassword) == false)
            {
                Logger.Error("Canceled signing. The password for certificate '" + s.CertificationFile + "' is wrong.");
                stamper.Close();
                return new ActionResult(ActionId, 105);
            }
            if (CertificateHasPrivateKey(s.CertificationFile, job.Passwords.PdfSignaturePassword) == false)
            {
                Logger.Error("Canceled signing. The certificate '" + s.CertificationFile + "' has no private key.");
                stamper.Close();
                return new ActionResult(ActionId, 106);
            }

            var fsCert = new FileStream(s.CertificationFile, FileMode.Open);
            var ks = new Pkcs12Store(fsCert, job.Passwords.PdfSignaturePassword.ToCharArray());
            string alias = null;
            foreach (string al in ks.Aliases)
            {
                if (ks.IsKeyEntry(al) && ks.GetKey(al).Key.IsPrivate)
                {
                    alias = al;
                    break;
                }
            }
            fsCert.Close();
            ICipherParameters pk = ks.GetKey(alias).Key;
            X509CertificateEntry[] x = ks.GetCertificateChain(alias);
            var chain = new X509Certificate[x.Length];
            for (int k = 0; k < x.Length; ++k)
                chain[k] = x[k].Certificate;

            ITSAClient tsc = null;
            if (s.TimeServerUrl.Trim() != "") //Timeserver with LogIn?
                tsc = new TSAClientBouncyCastle(s.TimeServerUrl /*, TimeServerLogonName, TimeServerLogonPassword*/);

            PdfSignatureAppearance sap = stamper.SignatureAppearance;
            if (tsc == null)
                sap.SetCrypto(pk, chain, null, PdfSignatureAppearance.WINCER_SIGNED);
            else
                sap.SetCrypto(null, chain, null, PdfSignatureAppearance.SELF_SIGNED);

            sap.Reason = s.SignReason;
            sap.Contact = s.SignContact;
            sap.Location = s.SignLocation;

            if (s.DisplaySignatureInPdf)
            {
                int signPage = SignPageNr(job);
                sap.SetVisibleSignature(new Rectangle(s.LeftX, s.LeftY, s.RightX, s.RightY),
                    signPage, null);
            }

            var dic = new PdfSignature(PdfName.ADOBE_PPKLITE, new PdfName("adbe.pkcs7.detached"));
            dic.Reason = sap.Reason;
            dic.Location = sap.Location;
            dic.Contact = sap.Contact;
            dic.Date = new PdfDate(sap.SignDate);
            sap.CryptoDictionary = dic;

            const int contentEstimated = 15000;
            // Preallocate excluded byte-range for the signature content (hex encoded)
            var exc = new Dictionary<PdfName, int>();
            exc[PdfName.CONTENTS] = contentEstimated*2 + 2;
            sap.PreClose(exc);
            const string hashAlgorithm = "SHA1"; //Always use HashAlgorithm "SHA1"
            var sgn = new PdfPKCS7(pk, chain, null, hashAlgorithm, false);
            IDigest messageDigest = DigestUtilities.GetDigest(hashAlgorithm);
            Stream data = sap.GetRangeStream();
            var buf = new byte[8192];
            int n;
            while ((n = data.Read(buf, 0, buf.Length)) > 0)
            {
                messageDigest.BlockUpdate(buf, 0, n);
            }
            var hash = new byte[messageDigest.GetDigestSize()];
            messageDigest.DoFinal(hash, 0);
            byte[] ocsp = null;
            if (chain.Length >= 2)
            {
                String url = PdfPKCS7.GetOCSPURL(chain[0]);
                if (!string.IsNullOrEmpty(url))
                    ocsp = new OcspClientBouncyCastle().GetEncoded(chain[0], chain[1], url);
            }
            DateTime cal = sap.SignDate;
            byte[] sh = sgn.GetAuthenticatedAttributeBytes(hash, cal, ocsp);
            sgn.Update(sh, 0, sh.Length);

            var paddedSig = new byte[contentEstimated];

            if (tsc != null)
            {
                byte[] encodedSigTsa = sgn.GetEncodedPKCS7(hash, cal, tsc, ocsp);
                Array.Copy(encodedSigTsa, 0, paddedSig, 0, encodedSigTsa.Length);
                if (contentEstimated + 2 < encodedSigTsa.Length)
                {
                    Logger.Error("Not enough space for signature");
                    return new ActionResult(ActionId, 107);
                }
            }
            else
            {
                byte[] encodedSig = sgn.GetEncodedPKCS7(hash, cal);
                Array.Copy(encodedSig, 0, paddedSig, 0, encodedSig.Length);
                if (contentEstimated + 2 < encodedSig.Length)
                {
                    Logger.Error("Not enough space for signature");
                    return new ActionResult(ActionId, 107);
                }
            }

            var dic2 = new PdfDictionary();
            dic2.Put(PdfName.CONTENTS, new PdfString(paddedSig).SetHexWriting(true));
            sap.Close(dic2);

            return new ActionResult();
        }

        private PdfStamper CreateStamperAccordingToEncryption(string sourceFilename, string destinationFilename,
            IJob job)
        {
            var reader = new PdfReader(sourceFilename);
            var fileStream = new FileStream(destinationFilename, FileMode.Create, FileAccess.Write);
            PdfStamper stamper = null;

            if (!job.Profile.PdfSettings.Security.Enable)
            {
                if (job.Profile.PdfSettings.Signing.Enable)
                    stamper = PdfStamper.CreateSignature(reader, fileStream, '\0', null,
                        job.Profile.PdfSettings.Signing.AllowMultiSigning);
                else
                    stamper = new PdfStamper(reader, fileStream, '\0', job.Profile.PdfSettings.Signing.AllowMultiSigning);
            }
            else
            {
                int encryption = CalculatePermissionValue(job.Profile);
                Logger.Trace("Callculated Permission Value: " + encryption);

                byte[] userPassword = null;

                byte[] ownerPassword = Encoding.Default.GetBytes(job.Passwords.PdfOwnerPassword);

                if (job.Profile.PdfSettings.Security.RequireUserPassword)
                    userPassword = Encoding.Default.GetBytes(job.Passwords.PdfUserPassword);

                switch (job.Profile.PdfSettings.Security.EncryptionLevel)
                {
                    case EncryptionLevel.Low40Bit:
                        if (job.Profile.PdfSettings.Signing.Enable)
                            stamper = PdfStamper.CreateSignature(reader, fileStream, PdfWriter.VERSION_1_4, null,
                                job.Profile.PdfSettings.Signing.AllowMultiSigning);
                        else
                            stamper = new PdfStamper(reader, fileStream, PdfWriter.VERSION_1_4,
                                job.Profile.PdfSettings.Signing.AllowMultiSigning);
                        stamper.SetEncryption(userPassword, ownerPassword, encryption, PdfWriter.STRENGTH40BITS);
                        break;
                    case EncryptionLevel.Medium128Bit:
                        if (job.Profile.PdfSettings.Signing.Enable)
                            stamper = PdfStamper.CreateSignature(reader, fileStream, PdfWriter.VERSION_1_4, null,
                                job.Profile.PdfSettings.Signing.AllowMultiSigning);
                        else
                            stamper = new PdfStamper(reader, fileStream, PdfWriter.VERSION_1_4,
                                job.Profile.PdfSettings.Signing.AllowMultiSigning);
                        stamper.SetEncryption(userPassword, ownerPassword, encryption, PdfWriter.STRENGTH128BITS);
                        break;
                    case EncryptionLevel.High128BitAes:
                        if (job.Profile.PdfSettings.Signing.Enable)
                            stamper = PdfStamper.CreateSignature(reader, fileStream, PdfWriter.VERSION_1_6, null,
                                job.Profile.PdfSettings.Signing.AllowMultiSigning);
                        else
                            stamper = new PdfStamper(reader, fileStream, PdfWriter.VERSION_1_6,
                                job.Profile.PdfSettings.Signing.AllowMultiSigning);
                        stamper.SetEncryption(userPassword, ownerPassword, encryption, PdfWriter.ENCRYPTION_AES_128);
                        break;
                    //default:
                    //    break;
                }
            }
            return stamper;
        }

        /// <summary>
        ///     Calculates the PDF permission value that results in the settings from the given profile
        /// </summary>
        /// <param name="profile">The profile to do the calculations with.</param>
        /// <returns>An integer that encodes the PDF security permissions</returns>
        public static int CalculatePermissionValue(ConversionProfile profile)
        {
            int encryption = 0;

            if (profile.PdfSettings.Security.AllowPrinting) encryption = encryption | PdfWriter.ALLOW_PRINTING;
            if (profile.PdfSettings.Security.AllowModifyContents)
                encryption = encryption | PdfWriter.ALLOW_MODIFY_CONTENTS;
            if (profile.PdfSettings.Security.AllowCopy) encryption = encryption | PdfWriter.ALLOW_COPY;
            if (profile.PdfSettings.Security.AllowModifiyAnnotations)
                encryption = encryption | PdfWriter.ALLOW_MODIFY_ANNOTATIONS;

            if ((profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Medium128Bit)
                || (profile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.High128BitAes))
            {
                if (profile.PdfSettings.Security.AllowPrinting)
                    if (profile.PdfSettings.Security.RestrictToDegradedPrinting)
                        encryption = encryption ^ (PdfWriter.ALLOW_PRINTING ^ PdfWriter.ALLOW_DEGRADED_PRINTING);
                            //Remove higher bit of AllowPrinting
                if (profile.PdfSettings.Security.AllowFillIn)
                    encryption = encryption | PdfWriter.ALLOW_FILL_IN; //Set automatically for 40Bit
                if (profile.PdfSettings.Security.AllowScreenReaders)
                    encryption = encryption | PdfWriter.ALLOW_SCREENREADERS; //Set automatically for 40Bit
                if (profile.PdfSettings.Security.AllowAssembly)
                    encryption = encryption | PdfWriter.ALLOW_ASSEMBLY; //Set automatically for 40Bit
            }
            return encryption;
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
        /// resStr = "Check the password for certifcate '" + certificateFile + "':"
        /// Set x509 = WScript.CreateObject("pdfforge.x509.x509")
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
        ///  Console.WriteLine("The certifcate '{0}' has a private key.", certificateFile);
        /// else
        ///  Console.WriteLine("The certifcate '{0}' has NO private key.", certificateFile);
        /// x509 = null;
        /// Console.ReadLine();
        /// </code>
        ///     <code lang="vbscript">
        /// Option Explicit
        /// Dim x509, password, certificateFile
        /// certificateFile = "MyCertificate.pfx"
        /// password = "MyPassword"
        /// Set x509 = WScript.CreateObject("pdfforge.x509.x509")
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

        private int SignPageNr(IJob job)
        {
            switch (job.Profile.PdfSettings.Signing.DisplaySignatureOnPage)
            {
                case SignaturePosition.CustomPage:
                    if (job.Profile.PdfSettings.Signing.DisplaySignatureOnCustomPage > job.PageCount)
                        return job.PageCount;
                    if (job.Profile.PdfSettings.Signing.DisplaySignatureOnCustomPage < 1)
                        return 1;
                    return job.Profile.PdfSettings.Signing.DisplaySignatureOnCustomPage;
                case SignaturePosition.LastPage:
                    return job.PageCount;
                //case SignaturePosition.FirstPage:
                default:
                    return 1;
            }
        }

        /*
        private ActionResult EncyptAndSignPdfFile(string sourceFilename, IJob job)
        {
            string tmp = Path.GetTempFileName();
            ActionResult result = SignPdfFile(sourceFilename, tmp, job);

            if (result.Success)
            {
                try
                {
                    File.Copy(tmp, sourceFilename, true);
                    File.Delete(tmp);
                    Logger.Trace(sourceFilename + " encrypted and/or signed!");
                }
                catch
                {
                    Logger.Error("Could not copy encrypted and/or signed file from tmp to " + sourceFilename);
                    return new ActionResult(ActionId, 108);
                }
            }
            else
                Logger.Error("Failed to encrypt and/or sign " + sourceFilename);

            return result;
        }
        */

        public ActionResult CheckEncryptAndSignSettings(ConversionProfile profile)
        {
            var actionResult = new ActionResult();

            Signing sign = profile.PdfSettings.Signing;
            if (sign.Enable)
            {
                if (string.IsNullOrEmpty(sign.CertificationFile))
                {
                    Logger.Error("Error in signing. Missing certification file.");
                    actionResult.Add(ActionId, 100);
                }
                else if (File.Exists(sign.CertificationFile) == false)
                {
                    Logger.Error("Error in signing. The certification file '" + sign.CertificationFile +
                                 "' doesn't exist.");
                    actionResult.Add(ActionId, 101);
                }

                if (profile.AutoSave.Enable)
                {
                    if (string.IsNullOrEmpty(sign.SignaturePassword))
                    {
                        Logger.Error("Automatic saving without certificate password.");
                        actionResult.Add(ActionId, 109);
                    }
                }
            }

            Security security = profile.PdfSettings.Security;
            if (security.Enable)
            {
                if (profile.AutoSave.Enable)
                {
                    if (string.IsNullOrEmpty(security.OwnerPassword))
                    {
                        Logger.Error("No saved owner password for security in automatic saving.");
                        actionResult.Add(ActionId, 110);
                    }

                    if (security.RequireUserPassword)
                    {
                        if (string.IsNullOrEmpty(security.UserPassword))
                        {
                            Logger.Error("No saved user password for security in automatic saving.");
                            actionResult.Add(ActionId, 111);
                        }
                    }
                }
            }

            return actionResult;
        }
        #endregion  
    }
}