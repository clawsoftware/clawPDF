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
    /// <summary>
    ///     PDF Security options
    /// </summary>
    public class Security
    {
        /// <summary>
        ///     Password that can be used to modify the document
        /// </summary>
        private string _ownerPassword;

        /// <summary>
        ///     Password that must be used to open the document (if set)
        /// </summary>
        private string _userPassword;

        public Security()
        {
            Init();
        }

        /// <summary>
        ///     Allow to user to print the document
        /// </summary>
        public bool AllowPrinting { get; set; }

        /// <summary>
        ///     Allow to user to use a screen reader
        /// </summary>
        public bool AllowScreenReader { get; set; }

        /// <summary>
        ///     Allow to user to copy content from the PDF
        /// </summary>
        public bool AllowToCopyContent { get; set; }

        /// <summary>
        ///     Allow to user to make changes to the assembly
        /// </summary>
        public bool AllowToEditAssembly { get; set; }

        /// <summary>
        ///     Allow to user to edit comments
        /// </summary>
        public bool AllowToEditComments { get; set; }

        /// <summary>
        ///     Allow to user to edit the document
        /// </summary>
        public bool AllowToEditTheDocument { get; set; }

        /// <summary>
        ///     Allow to user to fill in forms
        /// </summary>
        public bool AllowToFillForms { get; set; }

        /// <summary>
        ///     If true, the PDF file will be password protected
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Defines the encryption level. Valid values are: Rc40Bit, Rc128Bit, Aes128Bit
        /// </summary>
        public EncryptionLevel EncryptionLevel { get; set; }

        public string OwnerPassword
        {
            get
            {
                try
                {
                    return Data.Decrypt(_ownerPassword);
                }
                catch
                {
                    return "";
                }
            }
            set => _ownerPassword = Data.Encrypt(value);
        }

        /// <summary>
        ///     If true, a password is required to open the document.
        /// </summary>
        public bool RequireUserPassword { get; set; }

        /// <summary>
        ///     If true, only printing in low resolution will be supported
        /// </summary>
        public bool RestrictPrintingToLowQuality { get; set; }

        public string UserPassword
        {
            get
            {
                try
                {
                    return Data.Decrypt(_userPassword);
                }
                catch
                {
                    return "";
                }
            }
            set => _userPassword = Data.Encrypt(value);
        }

        private void Init()
        {
            AllowPrinting = true;
            AllowScreenReader = true;
            AllowToCopyContent = true;
            AllowToEditAssembly = true;
            AllowToEditComments = true;
            AllowToEditTheDocument = true;
            AllowToFillForms = true;
            Enabled = false;
            EncryptionLevel = EncryptionLevel.Rc128Bit;
            OwnerPassword = "";
            RequireUserPassword = false;
            RestrictPrintingToLowQuality = true;
            UserPassword = "";
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                AllowPrinting = bool.Parse(data.GetValue(@"" + path + @"AllowPrinting"));
            }
            catch
            {
                AllowPrinting = true;
            }

            try
            {
                AllowScreenReader = bool.Parse(data.GetValue(@"" + path + @"AllowScreenReader"));
            }
            catch
            {
                AllowScreenReader = true;
            }

            try
            {
                AllowToCopyContent = bool.Parse(data.GetValue(@"" + path + @"AllowToCopyContent"));
            }
            catch
            {
                AllowToCopyContent = true;
            }

            try
            {
                AllowToEditAssembly = bool.Parse(data.GetValue(@"" + path + @"AllowToEditAssembly"));
            }
            catch
            {
                AllowToEditAssembly = true;
            }

            try
            {
                AllowToEditComments = bool.Parse(data.GetValue(@"" + path + @"AllowToEditComments"));
            }
            catch
            {
                AllowToEditComments = true;
            }

            try
            {
                AllowToEditTheDocument = bool.Parse(data.GetValue(@"" + path + @"AllowToEditTheDocument"));
            }
            catch
            {
                AllowToEditTheDocument = true;
            }

            try
            {
                AllowToFillForms = bool.Parse(data.GetValue(@"" + path + @"AllowToFillForms"));
            }
            catch
            {
                AllowToFillForms = true;
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
                EncryptionLevel = (EncryptionLevel)Enum.Parse(typeof(EncryptionLevel),
                    data.GetValue(@"" + path + @"EncryptionLevel"));
            }
            catch
            {
                EncryptionLevel = EncryptionLevel.Rc128Bit;
            }

            _ownerPassword = data.GetValue(@"" + path + @"OwnerPassword");
            try
            {
                RequireUserPassword = bool.Parse(data.GetValue(@"" + path + @"RequireUserPassword"));
            }
            catch
            {
                RequireUserPassword = false;
            }

            try
            {
                RestrictPrintingToLowQuality = bool.Parse(data.GetValue(@"" + path + @"RestrictPrintingToLowQuality"));
            }
            catch
            {
                RestrictPrintingToLowQuality = true;
            }

            _userPassword = data.GetValue(@"" + path + @"UserPassword");
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"AllowPrinting", AllowPrinting.ToString());
            data.SetValue(@"" + path + @"AllowScreenReader", AllowScreenReader.ToString());
            data.SetValue(@"" + path + @"AllowToCopyContent", AllowToCopyContent.ToString());
            data.SetValue(@"" + path + @"AllowToEditAssembly", AllowToEditAssembly.ToString());
            data.SetValue(@"" + path + @"AllowToEditComments", AllowToEditComments.ToString());
            data.SetValue(@"" + path + @"AllowToEditTheDocument", AllowToEditTheDocument.ToString());
            data.SetValue(@"" + path + @"AllowToFillForms", AllowToFillForms.ToString());
            data.SetValue(@"" + path + @"Enabled", Enabled.ToString());
            data.SetValue(@"" + path + @"EncryptionLevel", EncryptionLevel.ToString());
            data.SetValue(@"" + path + @"OwnerPassword", _ownerPassword);
            data.SetValue(@"" + path + @"RequireUserPassword", RequireUserPassword.ToString());
            data.SetValue(@"" + path + @"RestrictPrintingToLowQuality", RestrictPrintingToLowQuality.ToString());
            data.SetValue(@"" + path + @"UserPassword", _userPassword);
        }

        public Security Copy()
        {
            var copy = new Security();

            copy.AllowPrinting = AllowPrinting;
            copy.AllowScreenReader = AllowScreenReader;
            copy.AllowToCopyContent = AllowToCopyContent;
            copy.AllowToEditAssembly = AllowToEditAssembly;
            copy.AllowToEditComments = AllowToEditComments;
            copy.AllowToEditTheDocument = AllowToEditTheDocument;
            copy.AllowToFillForms = AllowToFillForms;
            copy.Enabled = Enabled;
            copy.EncryptionLevel = EncryptionLevel;
            copy.OwnerPassword = OwnerPassword;
            copy.RequireUserPassword = RequireUserPassword;
            copy.RestrictPrintingToLowQuality = RestrictPrintingToLowQuality;
            copy.UserPassword = UserPassword;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is Security)) return false;
            var v = o as Security;

            if (!AllowPrinting.Equals(v.AllowPrinting)) return false;
            if (!AllowScreenReader.Equals(v.AllowScreenReader)) return false;
            if (!AllowToCopyContent.Equals(v.AllowToCopyContent)) return false;
            if (!AllowToEditAssembly.Equals(v.AllowToEditAssembly)) return false;
            if (!AllowToEditComments.Equals(v.AllowToEditComments)) return false;
            if (!AllowToEditTheDocument.Equals(v.AllowToEditTheDocument)) return false;
            if (!AllowToFillForms.Equals(v.AllowToFillForms)) return false;
            if (!Enabled.Equals(v.Enabled)) return false;
            if (!EncryptionLevel.Equals(v.EncryptionLevel)) return false;
            if (!OwnerPassword.Equals(v.OwnerPassword)) return false;
            if (!RequireUserPassword.Equals(v.RequireUserPassword)) return false;
            if (!RestrictPrintingToLowQuality.Equals(v.RestrictPrintingToLowQuality)) return false;
            if (!UserPassword.Equals(v.UserPassword)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("AllowPrinting=" + AllowPrinting);
            sb.AppendLine("AllowScreenReader=" + AllowScreenReader);
            sb.AppendLine("AllowToCopyContent=" + AllowToCopyContent);
            sb.AppendLine("AllowToEditAssembly=" + AllowToEditAssembly);
            sb.AppendLine("AllowToEditComments=" + AllowToEditComments);
            sb.AppendLine("AllowToEditTheDocument=" + AllowToEditTheDocument);
            sb.AppendLine("AllowToFillForms=" + AllowToFillForms);
            sb.AppendLine("Enabled=" + Enabled);
            sb.AppendLine("EncryptionLevel=" + EncryptionLevel);
            sb.AppendLine("OwnerPassword=" + OwnerPassword);
            sb.AppendLine("RequireUserPassword=" + RequireUserPassword);
            sb.AppendLine("RestrictPrintingToLowQuality=" + RestrictPrintingToLowQuality);
            sb.AppendLine("UserPassword=" + UserPassword);

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