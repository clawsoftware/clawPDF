using System;
using System.Text;
using System.Text.RegularExpressions;
using NLog;

namespace clawSoft.clawPDF.Mail
{
    public class OutlookClient : IEmailClient
    {
        private const int OlBodyFormatText = 1;
        private const int OlBodyFormatHtml = 2;
        private const int OlBodyFormatRichText = 3;

        private static bool _outlookTypeWasDetermined;
        private static Type _outlookType;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private static Type OutlookType
        {
            get
            {
                if (!_outlookTypeWasDetermined)
                {
                    _outlookTypeWasDetermined = true;
                    _outlookType = Type.GetTypeFromProgID("Outlook.Application");
                }

                return _outlookType;
            }
        }

        public virtual bool IsOutlookInstalled => OutlookType != null;

        public bool IsClientInstalled => IsOutlookInstalled;

        public bool ShowEmailClient(Email email)
        {
            _logger.Info("Starting Outlook");

            if (!IsClientInstalled)
            {
                _logger.Error("Outlook is not installed!");
                return false;
            }

            try
            {
                return ShowOutlook(email);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while starting Outlook");
                return false;
            }
        }

        private bool ShowOutlook(Email email)
        {
            dynamic outlook = Activator.CreateInstance(OutlookType);

            if (outlook == null)
                return false;

            var mail = outlook.CreateItem(0);

            mail.To = string.Join(";", email.To);
            //mail.CC = "";
            //mail.BCC = "";
            mail.Subject = email.Subject;
            //mail.BodyFormat = 2; // 1 = Text, 2 = HTML
            mail.Display();

            // We need to set the body after the mail is displayed to not overwrite the signature
            if (mail.BodyFormat == OlBodyFormatHtml)
                AddHtmlBody(mail, email.Body);
            else
                AddTextBody(mail, email.Body);

            foreach (var attachment in email.Attachments) mail.Attachments.Add(attachment.Filename);

            _logger.Info("Done starting Outlook");

            return true;
        }

        private void AddHtmlBody(dynamic mail, string body)
        {
            if (!body.Contains("<")) body = ConvertToHtml(body);

            string html = mail.HtmlBody;

            var bodyMatch = Regex.Match(html, "<body .*?>");

            if (bodyMatch.Success)
            {
                var bodyTag = bodyMatch.Groups[0].Value;
                html = html.Replace(bodyTag, bodyTag + body);
            }
            else
            {
                html = "<html><body>" + body + "</body></html>";
            }

            mail.HtmlBody = html;
        }

        private void AddTextBody(dynamic mail, string body)
        {
            mail.Body = body + mail.Body;
        }

        private string ConvertToHtml(string body)
        {
            var sb = new StringBuilder();

            foreach (var line in body.Split('\n')) sb.AppendFormat("<p class=\"MsoNormal\">{0}</p>", line.Trim('\r'));

            return sb.ToString();
        }
    }
}