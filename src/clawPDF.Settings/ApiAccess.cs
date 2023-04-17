using System;
using System.Text;
using clawSoft.clawPDF.Core.Settings.Enums;
using pdfforge.DataStorage;

// Custom Code starts here
// START_CUSTOM_SECTION:INCLUDES

// END_CUSTOM_SECTION:INCLUDES
// Custom Code ends here. Do not edit below

// ! This file is generated automatically.
// ! Do not edit it outside the sections for custom code.
// ! These changes will be deleted during the next generation run

namespace clawSoft.clawPDF.Core.Settings
{
    public class ApiAccess
    {
        private string _apiSecret;

        public ApiAccess()
        {
            Init();
        }

        // Custom Code starts here
        // START_CUSTOM_SECTION:GENERAL
        public ApiAccess(ApiProvider apiProvider)
        {
            Init();
            ProviderName = apiProvider;
        }

        public string AccountName { get; set; }

        public string ApiKey { get; set; }

        public string ApiSecret
        {
            get
            {
                try
                {
                    return Data.Decrypt(_apiSecret);
                }
                catch
                {
                    return "";
                }
            }
            set => _apiSecret = Data.Encrypt(value);
        }

        public ApiProvider ProviderName { get; set; }

        private void Init()
        {
            AccountName = "";
            ApiKey = "";
            ApiSecret = "";
            ProviderName = ApiProvider.AttachMe;
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                AccountName = Data.UnescapeString(data.GetValue(@"" + path + @"AccountName"));
            }
            catch
            {
                AccountName = "";
            }

            try
            {
                ApiKey = Data.UnescapeString(data.GetValue(@"" + path + @"ApiKey"));
            }
            catch
            {
                ApiKey = "";
            }

            _apiSecret = data.GetValue(@"" + path + @"ApiSecret");
            try
            {
                ProviderName =
                    (ApiProvider)Enum.Parse(typeof(ApiProvider), data.GetValue(@"" + path + @"ProviderName"));
            }
            catch
            {
                ProviderName = ApiProvider.AttachMe;
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"AccountName", Data.EscapeString(AccountName));
            data.SetValue(@"" + path + @"ApiKey", Data.EscapeString(ApiKey));
            data.SetValue(@"" + path + @"ApiSecret", _apiSecret);
            data.SetValue(@"" + path + @"ProviderName", ProviderName.ToString());
        }

        public ApiAccess Copy()
        {
            var copy = new ApiAccess();

            copy.AccountName = AccountName;
            copy.ApiKey = ApiKey;
            copy.ApiSecret = ApiSecret;
            copy.ProviderName = ProviderName;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is ApiAccess)) return false;
            var v = o as ApiAccess;

            if (!AccountName.Equals(v.AccountName)) return false;
            if (!ApiKey.Equals(v.ApiKey)) return false;
            if (!ApiSecret.Equals(v.ApiSecret)) return false;
            if (!ProviderName.Equals(v.ProviderName)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("AccountName=" + AccountName);
            sb.AppendLine("ApiKey=" + ApiKey);
            sb.AppendLine("ApiSecret=" + ApiSecret);
            sb.AppendLine("ProviderName=" + ProviderName);

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        // END_CUSTOM_SECTION:GENERAL
        // Custom Code ends here. Do not edit below
    }
}