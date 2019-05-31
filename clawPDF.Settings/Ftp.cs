using System.Text;
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
    /// <summary>
    ///     Upload the converted documents with FTP
    /// </summary>
    public class Ftp
    {
        /// <summary>
        ///     Password that is used to authenticate at the server
        /// </summary>
        private string _password;

        public Ftp()
        {
            Init();
        }

        /// <summary>
        ///     Target directory on the server
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        ///     If true, this action will be executed
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     If true, files with the same name will not be overwritten on the server. A counter will be appended instead (i.e.
        ///     document_2.pdf)
        /// </summary>
        public bool EnsureUniqueFilenames { get; set; }

        public string Password
        {
            get
            {
                try
                {
                    return Data.Decrypt(_password);
                }
                catch
                {
                    return "";
                }
            }
            set => _password = Data.Encrypt(value);
        }

        /// <summary>
        ///     Hostname or IP address of the FTP server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        ///     User name that is used to authenticate at the server
        /// </summary>
        public string UserName { get; set; }

        private void Init()
        {
            Directory = "";
            Enabled = false;
            EnsureUniqueFilenames = false;
            Password = "";
            Server = "";
            UserName = "";
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                Directory = Data.UnescapeString(data.GetValue(@"" + path + @"Directory"));
            }
            catch
            {
                Directory = "";
            }

            try
            {
                Enabled = bool.Parse(data.GetValue(@"" + path + @"Enabled"));
            }
            catch
            {
                Enabled = false;
            }

            try
            {
                EnsureUniqueFilenames = bool.Parse(data.GetValue(@"" + path + @"EnsureUniqueFilenames"));
            }
            catch
            {
                EnsureUniqueFilenames = false;
            }

            _password = data.GetValue(@"" + path + @"Password");
            try
            {
                Server = Data.UnescapeString(data.GetValue(@"" + path + @"Server"));
            }
            catch
            {
                Server = "";
            }

            try
            {
                UserName = Data.UnescapeString(data.GetValue(@"" + path + @"UserName"));
            }
            catch
            {
                UserName = "";
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"Directory", Data.EscapeString(Directory));
            data.SetValue(@"" + path + @"Enabled", Enabled.ToString());
            data.SetValue(@"" + path + @"EnsureUniqueFilenames", EnsureUniqueFilenames.ToString());
            data.SetValue(@"" + path + @"Password", _password);
            data.SetValue(@"" + path + @"Server", Data.EscapeString(Server));
            data.SetValue(@"" + path + @"UserName", Data.EscapeString(UserName));
        }

        public Ftp Copy()
        {
            var copy = new Ftp();

            copy.Directory = Directory;
            copy.Enabled = Enabled;
            copy.EnsureUniqueFilenames = EnsureUniqueFilenames;
            copy.Password = Password;
            copy.Server = Server;
            copy.UserName = UserName;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is Ftp)) return false;
            var v = o as Ftp;

            if (!Directory.Equals(v.Directory)) return false;
            if (!Enabled.Equals(v.Enabled)) return false;
            if (!EnsureUniqueFilenames.Equals(v.EnsureUniqueFilenames)) return false;
            if (!Password.Equals(v.Password)) return false;
            if (!Server.Equals(v.Server)) return false;
            if (!UserName.Equals(v.UserName)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Directory=" + Directory);
            sb.AppendLine("Enabled=" + Enabled);
            sb.AppendLine("EnsureUniqueFilenames=" + EnsureUniqueFilenames);
            sb.AppendLine("Password=" + Password);
            sb.AppendLine("Server=" + Server);
            sb.AppendLine("UserName=" + UserName);

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        // Custom Code starts here
        // START_CUSTOM_SECTION:GENERAL
        // END_CUSTOM_SECTION:GENERAL
        // Custom Code ends here. Do not edit below
    }
}