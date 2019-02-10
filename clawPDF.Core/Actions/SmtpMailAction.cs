using System;
using System.Net;
using System.Net.Mail;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using NLog;

namespace clawSoft.clawPDF.Core.Actions
{
    public class SmtpMailAction : IAction, ICheckable
    {
        private const int ActionId = 15;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string _signatureText;

        public SmtpMailAction(string signatureText)
        {
            _signatureText = signatureText;
        }

        /// <summary>
        ///     Sends the created files using SMTP
        /// </summary>
        /// <param name="job"></param>
        /// <returns>An ActionResult to determine the success and a list of errors</returns>
        public ActionResult ProcessJob(IJob job)
        {
            Logger.Debug("Launched smtp mail action");
            try
            {
                var result = SendMailOverSmtp(job);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in smtp mail action:\r\n" + ex.Message);
                return new ActionResult(ActionId, 999);
            }
        }

        /*
        public static bool TestConnection(string Host, int Port, bool Ssl, string UserName, string Password)
        {
            SmtpClient smtp = new SmtpClient(Host, Port);
            smtp.EnableSsl = Ssl;
            //System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
            smtp.Credentials = new System.Net.NetworkCredential(UserName, Password);

            return true;
        }
        */

        public ActionResult Check(ConversionProfile profile)
        {
            var actionResult = new ActionResult();

            if (profile.EmailSmtp.Enabled)
            {
                if (string.IsNullOrEmpty(profile.EmailSmtp.Address))
                {
                    Logger.Error("No SMTP email address is specified.");
                    actionResult.Add(ActionId, 100);
                }

                if (string.IsNullOrEmpty(profile.EmailSmtp.Recipients))
                {
                    Logger.Error("No SMTP email recipients are specified.");
                    actionResult.Add(ActionId, 101);
                }

                if (string.IsNullOrEmpty(profile.EmailSmtp.Server))
                {
                    Logger.Error("No SMTP host is specified.");
                    actionResult.Add(ActionId, 102);
                }

                if (profile.EmailSmtp.Port < 0)
                {
                    Logger.Error("Invalid SMTP port.");
                    actionResult.Add(ActionId, 103);
                }

                if (string.IsNullOrEmpty(profile.EmailSmtp.UserName))
                {
                    Logger.Error("No SMTP UserName is specified.");
                    actionResult.Add(ActionId, 104);
                }

                if (profile.AutoSave.Enabled)
                    if (string.IsNullOrEmpty(profile.EmailSmtp.Password))
                    {
                        Logger.Error("No SMTP password for automatic saving.");
                        actionResult.Add(ActionId, 110);
                    }
            }

            return actionResult;
        }

        public event EventHandler<QueryPasswordEventArgs> QueryRetypeSmtpPassword;

        private ActionResult SendMailOverSmtp(IJob job)
        {
            var actionResult = Check(job.Profile);
            if (!actionResult)
            {
                Logger.Error("Canceled SMTP mail action.");
                return actionResult;
            }

            if (string.IsNullOrEmpty(job.Passwords.SmtpPassword))
            {
                Logger.Error("SendMailOverSmtp canceled. Action launched without Password.");
                return new ActionResult(ActionId, 105);
            }

            var mail = new MailMessage(job.Profile.EmailSmtp.Address,
                job.Profile.EmailSmtp.Recipients.Replace(';', ','));
            mail.Subject = job.TokenReplacer.ReplaceTokens(job.Profile.EmailSmtp.Subject);
            mail.IsBodyHtml = false;
            mail.Body = job.TokenReplacer.ReplaceTokens(job.Profile.EmailSmtp.Content);

            if (job.Profile.EmailSmtp.AddSignature) mail.Body += _signatureText;

            Logger.Debug("Created new Mail"
                         + "\r\nFrom: " + mail.From
                         + "\r\nTo: " + mail.To
                         + "\r\nSubject: " + mail.Subject
                         + "\r\nContent: " + mail.Body
            );

            var i = 1;
            foreach (var file in job.OutputFiles)
            {
                var attach = new Attachment(file);
                //attach.NameEncoding = System.Text.Encoding.ASCII;
                mail.Attachments.Add(attach);
                Logger.Debug("Attachement " + i + "/" + job.OutputFiles.Count + ":" + file);
                i++;
            }

            var smtp = new SmtpClient(job.Profile.EmailSmtp.Server, job.Profile.EmailSmtp.Port);
            smtp.EnableSsl = job.Profile.EmailSmtp.Ssl;

            Logger.Debug("Created new SmtpClient:"
                         + "\r\nHost: " + smtp.Host
                         + "\r\nPort: " + smtp.Port
            );

            return SendEmail(job, smtp, mail);
        }

        private ActionResult SendEmail(IJob job, SmtpClient smtp, MailMessage mail)
        {
            var credentials = new NetworkCredential(job.Profile.EmailSmtp.UserName, job.Passwords.SmtpPassword);
            smtp.Credentials = credentials;

            try
            {
                smtp.Send(mail);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                Logger.Error("The message could not be delivered to one or more of the recipients\r\n" + ex.Message);
                mail.Dispose();
                return new ActionResult(ActionId, 106);
            }
            catch (SmtpException ex)
            {
                Logger.Warn("Could not authorize to host.\r\n" + ex.Message);
                if (QueryRetypeSmtpPassword == null)
                {
                    Logger.Error("SendMailOverSmtp canceled. No Retype Smtp Password specified.");
                    mail.Dispose();
                    return new ActionResult(ActionId, 107);
                }

                var queryPassword = new QueryPasswordEventArgs(job);

                QueryRetypeSmtpPassword(this, queryPassword);

                if (!queryPassword.Cancel)
                    return SendEmail(job, smtp, mail);

                Logger.Warn("User canceled setting SMTP e-mail Password, SendMailOverSmtp canceled");
                mail.Dispose();
                return new ActionResult(ActionId, 108);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception while sending mail over smtp:\r\n" + ex.Message);
                mail.Dispose();
                return new ActionResult(ActionId, 109);
            }

            mail.Dispose();
            return new ActionResult();
        }
    }
}