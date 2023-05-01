using System;
using System.Text.RegularExpressions;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Mail;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using NLog;
using System.Collections.Generic;
using PdfToSvg;
using Org.BouncyCastle.Cms;
using System.Linq;

namespace clawSoft.clawPDF.Core.Actions
{
    public class EMailClientAction : IAction
    {
        private const int ActionId = 11;
        private readonly IEmailClientFactory _emailClientFactory = new EmailClientFactory();
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly string _signatureText;

        public EMailClientAction(string signatureText)
        {
            _signatureText = signatureText;
        }

        public EMailClientAction(IEmailClientFactory emailClientFactory, string signatureText)
        {
            _emailClientFactory = emailClientFactory;
            _signatureText = signatureText;
        }

        public ActionResult ProcessJob(IJob job)
        {
            try
            {
                _logger.Info("Launched client e-mail action");

                var message = new Email();

                message.Subject = job.TokenReplacer.ReplaceTokens(job.Profile.EmailClient.Subject);
                message.Body = job.TokenReplacer.ReplaceTokens(job.Profile.EmailClient.Content);

                if (job.Profile.EmailClient.AddSignature) message.Body += _signatureText;

                if (!job.Profile.EmailClient.Autodetect)
                {
                    foreach (var recipient in job.Profile.EmailClient.Recipients.Replace(',', ';').Split(';'))
                        if (!string.IsNullOrWhiteSpace(recipient))
                            message.To.Add(recipient.Trim());
                }
                else
                {
                    foreach (var file in job.OutputFiles)
                    {
                        if (file.Contains(".pdf"))
                        {
                            List<string> emails = ExtractEmailsFromPdf(file);
                            List<string> distinctemails = emails.Distinct().ToList();
                            foreach (string email in distinctemails)
                            {
                                message.To.Add(email.Trim());
                            }
                        }
                    }
                }

                foreach (var file in job.OutputFiles) message.Attachments.Add(new Attachment(file));

                _logger.Info("Starting e-mail client");

                var mailClient = _emailClientFactory.CreateEmailClient();

                if (mailClient == null)
                {
                    _logger.Error("No compatible e-mail client installed");
                    return new ActionResult(ActionId, 101);
                }

                var success = mailClient.ShowEmailClient(message);

                if (!success)
                {
                    _logger.Warn("Could not start e-mail client");
                    return new ActionResult(ActionId, 100);
                }

                _logger.Info("Done starting e-mail client");
                return new ActionResult();
            }
            catch (Exception ex)
            {
                _logger.Error("Exception in e-mail client Action \r\n" + ex.Message);
                return new ActionResult(ActionId, 999);
            }
        }

        public static List<string> ExtractEmailsFromPdf(string pdfPath)
        {
            var emails = new List<string>();

            using (PdfReader reader = new PdfReader(pdfPath))
            {
                using (iText.Kernel.Pdf.PdfDocument pdfDocument = new iText.Kernel.Pdf.PdfDocument(reader))
                {
                    var pageCount = pdfDocument.GetNumberOfPages();
                    var regexPattern = new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b");

                    for (int i = 1; i <= pageCount; i++)
                    {
                        var pageContent = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), new SimpleTextExtractionStrategy());
                        var matches = regexPattern.Matches(pageContent);

                        foreach (Match match in matches)
                        {
                            emails.Add(match.Value);
                        }
                    }
                }
            }

            return emails;
        }

        public static bool CheckEmailClientInstalled()
        {
            var emailClientFactory = new EmailClientFactory();
            return emailClientFactory.CreateEmailClient() != null;
        }
    }
}