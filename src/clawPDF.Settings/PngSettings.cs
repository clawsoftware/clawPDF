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
    ///     Settings for the PNG output format
    /// </summary>
    public class PngSettings
    {
        public PngSettings()
        {
            Init();
        }

        /// <summary>
        ///     Number of colors. Valid values are: Color32BitTransp, Color24Bit, Color8Bit, Color4Bit, Gray8Bit, BlackWhite
        /// </summary>
        public PngColor Color { get; set; }

        /// <summary>
        ///     Resolution of the PNG files
        /// </summary>
        public int Dpi { get; set; }

        private void Init()
        {
            Color = PngColor.Color24Bit;
            Dpi = 150;
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                Color = (PngColor)Enum.Parse(typeof(PngColor), data.GetValue(@"" + path + @"Color"));
            }
            catch
            {
                Color = PngColor.Color24Bit;
            }

            try
            {
                Dpi = int.Parse(data.GetValue(@"" + path + @"Dpi"), CultureInfo.InvariantCulture);
            }
            catch
            {
                Dpi = 150;
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"Color", Color.ToString());
            data.SetValue(@"" + path + @"Dpi", Dpi.ToString(CultureInfo.InvariantCulture));
        }

        public PngSettings Copy()
        {
            var copy = new PngSettings();

            copy.Color = Color;
            copy.Dpi = Dpi;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is PngSettings)) return false;
            var v = o as PngSettings;

            if (!Color.Equals(v.Color)) return false;
            if (!Dpi.Equals(v.Dpi)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Color=" + Color);
            sb.AppendLine("Dpi=" + Dpi);

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