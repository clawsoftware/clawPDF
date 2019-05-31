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
    public class PrinterMapping
    {
        public PrinterMapping()
        {
            Init();
        }

        // Custom Code starts here
        // START_CUSTOM_SECTION:GENERAL
        public PrinterMapping(string printerName, string profileGuid)
        {
            PrinterName = printerName;
            ProfileGuid = profileGuid;
        }

        public string PrinterName { get; set; }

        public string ProfileGuid { get; set; }

        private void Init()
        {
            PrinterName = "";
            ProfileGuid = "";
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                PrinterName = Data.UnescapeString(data.GetValue(@"" + path + @"PrinterName"));
            }
            catch
            {
                PrinterName = "";
            }

            try
            {
                ProfileGuid = Data.UnescapeString(data.GetValue(@"" + path + @"ProfileGuid"));
            }
            catch
            {
                ProfileGuid = "";
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"PrinterName", Data.EscapeString(PrinterName));
            data.SetValue(@"" + path + @"ProfileGuid", Data.EscapeString(ProfileGuid));
        }

        public PrinterMapping Copy()
        {
            var copy = new PrinterMapping();

            copy.PrinterName = PrinterName;
            copy.ProfileGuid = ProfileGuid;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is PrinterMapping)) return false;
            var v = o as PrinterMapping;

            if (!PrinterName.Equals(v.PrinterName)) return false;
            if (!ProfileGuid.Equals(v.ProfileGuid)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("PrinterName=" + PrinterName);
            sb.AppendLine("ProfileGuid=" + ProfileGuid);

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