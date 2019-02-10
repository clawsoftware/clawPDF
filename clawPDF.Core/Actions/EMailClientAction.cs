using System;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Mail;
using NLog;

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

                foreach (var recipient in job.Profile.EmailClient.Recipients.Replace(',', ';').Split(';'))
                    if (!string.IsNullOrWhiteSpace(recipient))
                        message.To.Add(recipient.Trim());

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

        public static bool CheckEmailClientInstalled()
        {
            var emailClientFactory = new EmailClientFactory();
            return emailClientFactory.CreateEmailClient() != null;
        }
    }
}