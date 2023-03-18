using System.Globalization;
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
    ///     Settings for the OCRSettings
    /// </summary>
    public class OCRSettings
    {
        public OCRSettings()
        {
            Init();
        }

        /// <summary>
        ///     Settings for OCRLanguage
        /// </summary>
        public string OCRLanguage { get; set; }

        private void Init()
        {
            OCRLanguage = "eng+deu";
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                OCRLanguage = data.GetValue(@"" + path + @"OCRLanguage").ToString();
            }
            catch
            {
                OCRLanguage = "eng+deu";
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"OCRLanguage", OCRLanguage.ToString(CultureInfo.InvariantCulture));
        }

        public OCRSettings Copy()
        {
            var copy = new OCRSettings();

            copy.OCRLanguage = OCRLanguage;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is OCRSettings)) return false;
            var v = o as OCRSettings;

            if (!OCRLanguage.Equals(v.OCRLanguage)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("OCRLanguage=" + OCRLanguage);

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