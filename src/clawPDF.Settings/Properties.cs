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
    ///     Properties of the profile
    /// </summary>
    public class Properties
    {
        public Properties()
        {
            Init();
        }

        /// <summary>
        ///     Can users delete this profile?
        /// </summary>
        public bool Deletable { get; set; }

        /// <summary>
        ///     Can users edit this profile?
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        ///     Can users rename this profile?
        /// </summary>
        public bool Renamable { get; set; }

        private void Init()
        {
            Deletable = true;
            Editable = true;
            Renamable = true;
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                Deletable = bool.Parse(data.GetValue(@"" + path + @"Deletable"));
            }
            catch
            {
                Deletable = true;
            }

            try
            {
                Editable = bool.Parse(data.GetValue(@"" + path + @"Editable"));
            }
            catch
            {
                Editable = true;
            }

            try
            {
                Renamable = bool.Parse(data.GetValue(@"" + path + @"Renamable"));
            }
            catch
            {
                Renamable = true;
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"Deletable", Deletable.ToString());
            data.SetValue(@"" + path + @"Editable", Editable.ToString());
            data.SetValue(@"" + path + @"Renamable", Renamable.ToString());
        }

        public Properties Copy()
        {
            var copy = new Properties();

            copy.Deletable = Deletable;
            copy.Editable = Editable;
            copy.Renamable = Renamable;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is Properties)) return false;
            var v = o as Properties;

            if (!Deletable.Equals(v.Deletable)) return false;
            if (!Editable.Equals(v.Editable)) return false;
            if (!Renamable.Equals(v.Renamable)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Deletable=" + Deletable);
            sb.AppendLine("Editable=" + Editable);
            sb.AppendLine("Renamable=" + Renamable);

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