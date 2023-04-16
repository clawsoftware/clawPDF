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
    ///     Ghostscript settings
    /// </summary>
    public class Ghostscript
    {
        public Ghostscript()
        {
            Init();
        }

        /// <summary>
        ///     These parameters will be provided to Ghostscript in addition to the clawPDF parameters
        /// </summary>
        public string AdditionalGsParameters { get; set; }

        private void Init()
        {
            AdditionalGsParameters = "";
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                AdditionalGsParameters = Data.UnescapeString(data.GetValue(@"" + path + @"AdditionalGsParameters"));
            }
            catch
            {
                AdditionalGsParameters = "";
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"AdditionalGsParameters", Data.EscapeString(AdditionalGsParameters));
        }

        public Ghostscript Copy()
        {
            var copy = new Ghostscript();

            copy.AdditionalGsParameters = AdditionalGsParameters;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is Ghostscript)) return false;
            var v = o as Ghostscript;

            if (!AdditionalGsParameters.Equals(v.AdditionalGsParameters)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("AdditionalGsParameters=" + AdditionalGsParameters);

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