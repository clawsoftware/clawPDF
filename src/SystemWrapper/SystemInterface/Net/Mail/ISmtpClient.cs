using System.Threading.Tasks;

namespace SystemInterface.Net.Mail
{
    using System;
    using System.Net;
    using System.Net.Mail;

    public interface ISmtpClient : IDisposable
    {
        event SendCompletedEventHandler SendCompleted;

        void Send(string from, string recipients, string subject, string body);

        void Send(MailMessage message);

        void SendAsync(MailMessage message, object userToken);

        void SendAsync(string from, string recipients, string subject, string body, object userToken);

        void SendAsyncCancel();

        Task SendMailAsync(MailMessage message);

        Task SendMailAsync(string from, string recipients, string subject, string body);

        ICredentialsByHost Credentials { get; set; }

        string Host { get; set; }
    }
}