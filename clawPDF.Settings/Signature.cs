using System;
using System.Globalization;
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
    /// <summary>
    ///     Digitally sign the PDF document
    /// </summary>
    public class Signature
    {
        /// <summary>
        ///     Password for the certificate file
        /// </summary>
        private string _signaturePassword;

        /// <summary>
        ///     Password for the time server
        /// </summary>
        private string _timeServerPassword;

        public Signature()
        {
            Init();
        }

        /// <summary>
        ///     If true, the PDF file may be signed by additional persons
        /// </summary>
        public bool AllowMultiSigning { get; set; }

        /// <summary>
        ///     Path to the certificate
        /// </summary>
        public string CertificateFile { get; set; }

        /// <summary>
        ///     If true, the signature will be displayed in the PDF file
        /// </summary>
        public bool DisplaySignatureInDocument { get; set; }

        /// <summary>
        ///     If true, the signature will be displayed in the PDF document
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Signature location: Top left corner (X part)
        /// </summary>
        public int LeftX { get; set; }

        /// <summary>
        ///     Signature location: Top left corner (Y part)
        /// </summary>
        public int LeftY { get; set; }

        /// <summary>
        ///     Signature location: Bottom right corner (X part)
        /// </summary>
        public int RightX { get; set; }

        /// <summary>
        ///     Signature location: Bottom right corner (Y part)
        /// </summary>
        public int RightY { get; set; }

        /// <summary>
        ///     Contact name of the signature
        /// </summary>
        public string SignContact { get; set; }

        /// <summary>
        ///     Signature location
        /// </summary>
        public string SignLocation { get; set; }

        /// <summary>
        ///     Reason for the signature
        /// </summary>
        public string SignReason { get; set; }

        /// <summary>
        ///     If the signature page is set to custom, this property defines the page where the signature will be displayed
        /// </summary>
        public int SignatureCustomPage { get; set; }

        /// <summary>
        ///     Defines the page on which the signature will be displayed. Valid values are: FirstPage, LastPage, CustomPage
        /// </summary>
        public SignaturePage SignaturePage { get; set; }

        public string SignaturePassword
        {
            get
            {
                try
                {
                    return Data.Decrypt(_signaturePassword);
                }
                catch
                {
                    return "";
                }
            }
            set => _signaturePassword = Data.Encrypt(value);
        }

        /// <summary>
        ///     Set to true, if the time server needs authentication
        /// </summary>
        public bool TimeServerIsSecured { get; set; }

        /// <summary>
        ///     Login name for the time server
        /// </summary>
        public string TimeServerLoginName { get; set; }

        public string TimeServerPassword
        {
            get
            {
                try
                {
                    return Data.Decrypt(_timeServerPassword);
                }
                catch
                {
                    return "";
                }
            }
            set => _timeServerPassword = Data.Encrypt(value);
        }

        /// <summary>
        ///     URL of a time server that provides a signed timestamp
        /// </summary>
        public string TimeServerUrl { get; set; }

        private void Init()
        {
            AllowMultiSigning = false;
            CertificateFile = "";
            DisplaySignatureInDocument = false;
            Enabled = false;
            LeftX = 100;
            LeftY = 100;
            RightX = 200;
            RightY = 200;
            SignContact = "";
            SignLocation = "";
            SignReason = "";
            SignatureCustomPage = 1;
            SignaturePage = SignaturePage.FirstPage;
            SignaturePassword = "";
            TimeServerIsSecured = false;
            TimeServerLoginName = "";
            TimeServerPassword = "";
            TimeServerUrl = "http://timestamp.globalsign.com/scripts/timstamp.dll";
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                AllowMultiSigning = bool.Parse(data.GetValue(@"" + path + @"AllowMultiSigning"));
            }
            catch
            {
                AllowMultiSigning = false;
            }

            try
            {
                CertificateFile = Data.UnescapeString(data.GetValue(@"" + path + @"CertificateFile"));
            }
            catch
            {
                CertificateFile = "";
            }

            try
            {
                DisplaySignatureInDocument = bool.Parse(data.GetValue(@"" + path + @"DisplaySignatureInDocument"));
            }
            catch
            {
                DisplaySignatureInDocument = false;
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
                LeftX = int.Parse(data.GetValue(@"" + path + @"LeftX"), CultureInfo.InvariantCulture);
            }
            catch
            {
                LeftX = 100;
            }

            try
            {
                LeftY = int.Parse(data.GetValue(@"" + path + @"LeftY"), CultureInfo.InvariantCulture);
            }
            catch
            {
                LeftY = 100;
            }

            try
            {
                RightX = int.Parse(data.GetValue(@"" + path + @"RightX"), CultureInfo.InvariantCulture);
            }
            catch
            {
                RightX = 200;
            }

            try
            {
                RightY = int.Parse(data.GetValue(@"" + path + @"RightY"), CultureInfo.InvariantCulture);
            }
            catch
            {
                RightY = 200;
            }

            try
            {
                SignContact = Data.UnescapeString(data.GetValue(@"" + path + @"SignContact"));
            }
            catch
            {
                SignContact = "";
            }

            try
            {
                SignLocation = Data.UnescapeString(data.GetValue(@"" + path + @"SignLocation"));
            }
            catch
            {
                SignLocation = "";
            }

            try
            {
                SignReason = Data.UnescapeString(data.GetValue(@"" + path + @"SignReason"));
            }
            catch
            {
                SignReason = "";
            }

            try
            {
                SignatureCustomPage = int.Parse(data.GetValue(@"" + path + @"SignatureCustomPage"),
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                SignatureCustomPage = 1;
            }

            try
            {
                SignaturePage =
                    (SignaturePage)Enum.Parse(typeof(SignaturePage), data.GetValue(@"" + path + @"SignaturePage"));
            }
            catch
            {
                SignaturePage = SignaturePage.FirstPage;
            }

            _signaturePassword = data.GetValue(@"" + path + @"SignaturePassword");
            try
            {
                TimeServerIsSecured = bool.Parse(data.GetValue(@"" + path + @"TimeServerIsSecured"));
            }
            catch
            {
                TimeServerIsSecured = false;
            }

            try
            {
                TimeServerLoginName = Data.UnescapeString(data.GetValue(@"" + path + @"TimeServerLoginName"));
            }
            catch
            {
                TimeServerLoginName = "";
            }

            _timeServerPassword = data.GetValue(@"" + path + @"TimeServerPassword");
            try
            {
                TimeServerUrl = Data.UnescapeString(data.GetValue(@"" + path + @"TimeServerUrl"));
            }
            catch
            {
                TimeServerUrl = "http://timestamp.globalsign.com/scripts/timstamp.dll";
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"AllowMultiSigning", AllowMultiSigning.ToString());
            data.SetValue(@"" + path + @"CertificateFile", Data.EscapeString(CertificateFile));
            data.SetValue(@"" + path + @"DisplaySignatureInDocument", DisplaySignatureInDocument.ToString());
            data.SetValue(@"" + path + @"Enabled", Enabled.ToString());
            data.SetValue(@"" + path + @"LeftX", LeftX.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"LeftY", LeftY.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"RightX", RightX.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"RightY", RightY.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"SignContact", Data.EscapeString(SignContact));
            data.SetValue(@"" + path + @"SignLocation", Data.EscapeString(SignLocation));
            data.SetValue(@"" + path + @"SignReason", Data.EscapeString(SignReason));
            data.SetValue(@"" + path + @"SignatureCustomPage",
                SignatureCustomPage.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"SignaturePage", SignaturePage.ToString());
            data.SetValue(@"" + path + @"SignaturePassword", _signaturePassword);
            data.SetValue(@"" + path + @"TimeServerIsSecured", TimeServerIsSecured.ToString());
            data.SetValue(@"" + path + @"TimeServerLoginName", Data.EscapeString(TimeServerLoginName));
            data.SetValue(@"" + path + @"TimeServerPassword", _timeServerPassword);
            data.SetValue(@"" + path + @"TimeServerUrl", Data.EscapeString(TimeServerUrl));
        }

        public Signature Copy()
        {
            var copy = new Signature();

            copy.AllowMultiSigning = AllowMultiSigning;
            copy.CertificateFile = CertificateFile;
            copy.DisplaySignatureInDocument = DisplaySignatureInDocument;
            copy.Enabled = Enabled;
            copy.LeftX = LeftX;
            copy.LeftY = LeftY;
            copy.RightX = RightX;
            copy.RightY = RightY;
            copy.SignContact = SignContact;
            copy.SignLocation = SignLocation;
            copy.SignReason = SignReason;
            copy.SignatureCustomPage = SignatureCustomPage;
            copy.SignaturePage = SignaturePage;
            copy.SignaturePassword = SignaturePassword;
            copy.TimeServerIsSecured = TimeServerIsSecured;
            copy.TimeServerLoginName = TimeServerLoginName;
            copy.TimeServerPassword = TimeServerPassword;
            copy.TimeServerUrl = TimeServerUrl;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is Signature)) return false;
            var v = o as Signature;

            if (!AllowMultiSigning.Equals(v.AllowMultiSigning)) return false;
            if (!CertificateFile.Equals(v.CertificateFile)) return false;
            if (!DisplaySignatureInDocument.Equals(v.DisplaySignatureInDocument)) return false;
            if (!Enabled.Equals(v.Enabled)) return false;
            if (!LeftX.Equals(v.LeftX)) return false;
            if (!LeftY.Equals(v.LeftY)) return false;
            if (!RightX.Equals(v.RightX)) return false;
            if (!RightY.Equals(v.RightY)) return false;
            if (!SignContact.Equals(v.SignContact)) return false;
            if (!SignLocation.Equals(v.SignLocation)) return false;
            if (!SignReason.Equals(v.SignReason)) return false;
            if (!SignatureCustomPage.Equals(v.SignatureCustomPage)) return false;
            if (!SignaturePage.Equals(v.SignaturePage)) return false;
            if (!SignaturePassword.Equals(v.SignaturePassword)) return false;
            if (!TimeServerIsSecured.Equals(v.TimeServerIsSecured)) return false;
            if (!TimeServerLoginName.Equals(v.TimeServerLoginName)) return false;
            if (!TimeServerPassword.Equals(v.TimeServerPassword)) return false;
            if (!TimeServerUrl.Equals(v.TimeServerUrl)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("AllowMultiSigning=" + AllowMultiSigning);
            sb.AppendLine("CertificateFile=" + CertificateFile);
            sb.AppendLine("DisplaySignatureInDocument=" + DisplaySignatureInDocument);
            sb.AppendLine("Enabled=" + Enabled);
            sb.AppendLine("LeftX=" + LeftX);
            sb.AppendLine("LeftY=" + LeftY);
            sb.AppendLine("RightX=" + RightX);
            sb.AppendLine("RightY=" + RightY);
            sb.AppendLine("SignContact=" + SignContact);
            sb.AppendLine("SignLocation=" + SignLocation);
            sb.AppendLine("SignReason=" + SignReason);
            sb.AppendLine("SignatureCustomPage=" + SignatureCustomPage);
            sb.AppendLine("SignaturePage=" + SignaturePage);
            sb.AppendLine("SignaturePassword=" + SignaturePassword);
            sb.AppendLine("TimeServerIsSecured=" + TimeServerIsSecured);
            sb.AppendLine("TimeServerLoginName=" + TimeServerLoginName);
            sb.AppendLine("TimeServerPassword=" + TimeServerPassword);
            sb.AppendLine("TimeServerUrl=" + TimeServerUrl);

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