using SystemInterface.Microsoft.Win32;
using SystemWrapper.Microsoft.Win32;

namespace clawSoft.clawPDF.Mail
{
    public class EmailClientFactory : IEmailClientFactory
    {
        private readonly IRegistry _registryWrap;

        public EmailClientFactory()
        {
            _registryWrap = new RegistryWrap();
        }

        public EmailClientFactory(IRegistry registryWrap)
        {
            _registryWrap = registryWrap;
        }

        public IEmailClient CreateEmailClient()
        {
            var defaultMailClientName = FindDefaultMailClientName();

            if (defaultMailClientName != null)
            {
                var outlookClient = CreateOutlookClient();
                if (defaultMailClientName.Contains("Outlook") && outlookClient.IsOutlookInstalled)
                    return outlookClient;
            }

            var mapiClient = CreateMapiClient();
            if (mapiClient.IsMapiClientInstalled)
            {
                mapiClient.StartInOwnThread = true;

                return mapiClient;
            }

            return null;
        }

        protected virtual OutlookClient CreateOutlookClient()
        {
            return new OutlookClient();
        }

        protected virtual MapiClient CreateMapiClient()
        {
            return new MapiClient(_registryWrap);
        }

        private string FindDefaultMailClientName()
        {
            var mailClient = _registryWrap.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Clients\Mail", "", null);
            if (mailClient != null)
                return mailClient.ToString();

            mailClient = _registryWrap.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\Mail", "", null);
            if (mailClient != null)
                return mailClient.ToString();

            return null;
        }
    }
}