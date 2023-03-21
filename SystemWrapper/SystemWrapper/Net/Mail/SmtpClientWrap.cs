using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace SystemWrapper.Net.Mail
{
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Mail;
    using SystemInterface.Net.Mail;

    public class SmtpClientWrap : ISmtpClient
    {
        #region Constructors
        public SmtpClient Instance
        {
            get { return _Instance ?? (_Instance = new SmtpClient()); }
            set { _Instance = value; }
        }
        private SmtpClient _Instance;

        public SmtpClientWrap()
        {
        }

        public SmtpClientWrap(string host)
        {
            Instance = new SmtpClient(host);
        }

        public SmtpClientWrap(string host, int port)
        {
            Instance = new SmtpClient(host, port);
        }

        public SmtpClientWrap(SmtpClient smtpClient)
        {
            Instance = smtpClient;
        }
        #endregion

        #region Methods
        public void Send(string from, string recipients, string subject, string body)
        {
            Instance.Send(from, recipients, subject, body);
        }

        public void Send(MailMessage message)
        {
            Instance.Send(message);
        }

        public void SendAsync(MailMessage message, object userToken)
        {
            AddHandler();
            Instance.SendAsync(message, userToken);
        }

        public void SendAsync(string from, string recipients, string subject, string body, object userToken)
        {
            AddHandler();
            Instance.SendAsync(from, recipients, subject, body, userToken);
        }

        public void SendAsyncCancel()
        {
            Instance.SendAsyncCancel();
        }

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public Task SendMailAsync(MailMessage message)
        {
            return Instance.SendMailAsync(message);
        }

        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public Task SendMailAsync(string from, string recipients, string subject, string body)
        {
            return Instance.SendMailAsync(from, recipients, subject, body);
        }

        #endregion

        #region properties
        public X509CertificateCollection ClientCertificates
        {
            get { return Instance.ClientCertificates; }
        }


        public ICredentialsByHost Credentials
        {
            get { return Instance.Credentials; }
            set { Instance.Credentials = value; }
        }

        public SmtpDeliveryFormat DeliveryFormat
        {
            get { return Instance.DeliveryFormat; }
            set { Instance.DeliveryFormat = value; }
        }

        public SmtpDeliveryMethod DeliveryMethod
        {
            get { return Instance.DeliveryMethod; }
            set { Instance.DeliveryMethod = value; }
        }

        public bool EnableSsl
        {
            get { return Instance.EnableSsl; }
            set { Instance.EnableSsl = value; }
        }

        public string Host
        {
            get { return Instance.Host; }
            set { Instance.Host = value; }
        }

        public string PickupDirectoryLocation
        {
            get { return Instance.PickupDirectoryLocation; }
            set { Instance.PickupDirectoryLocation = value; }
        }

        public int Port
        {
            get { return Instance.Port; }
            set { Instance.Port = value; }
        }

        public ServicePoint ServicePoint
        {
            get { return Instance.ServicePoint; }
        }

        public string TargetName
        {
            get { return Instance.TargetName; }
            set { Instance.TargetName = value; }
        }

        public int Timeout
        {
            get { return Instance.Timeout; }
            set { Instance.Timeout = value; }
        }

        public bool UseDefaultCredentials
        {
            get { return Instance.UseDefaultCredentials; }
            set { Instance.UseDefaultCredentials = value; }
        }

        #endregion

        #region SendCompleted Event

        public event SendCompletedEventHandler SendCompleted;

        internal bool HandlerAdded;

        internal void SendCompletedHandler(object sender, AsyncCompletedEventArgs e)
        {
            OnSendCompleted(e);
        }

        protected void OnSendCompleted(AsyncCompletedEventArgs e)
        {
            if (SendCompleted != null)
            {
                SendCompleted.Invoke(this, e);
            }
        }

        internal void AddHandler()
        {
            if (!HandlerAdded)
            {
                Instance.SendCompleted += SendCompletedHandler;
                HandlerAdded = true;
            }
        }

        #endregion

        #region IDisposable

        internal bool _Disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Dispose(bool disposing)
        {
            if (_Disposed)
            {
                return;
            }
            if (!disposing)
            {
                Instance.Dispose();
            }
            _Disposed = true;
        }
        #endregion
    }
}