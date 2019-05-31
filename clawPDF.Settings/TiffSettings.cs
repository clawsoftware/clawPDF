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
    ///     Settings for the TIFF output format
    /// </summary>
    public class TiffSettings
    {
        public TiffSettings()
        {
            Init();
        }

        /// <summary>
        ///     Number of colors. Valid values are: Color24Bit, Color12Bit, BlackWhite
        /// </summary>
        public TiffColor Color { get; set; }

        /// <summary>
        ///     Resolution of the TIFF files
        /// </summary>
        public int Dpi { get; set; }

        private void Init()
        {
            Color = TiffColor.Color24Bit;
            Dpi = 150;
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                Color = (TiffColor)Enum.Parse(typeof(TiffColor), data.GetValue(@"" + path + @"Color"));
            }
            catch
            {
                Color = TiffColor.Color24Bit;
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

        public TiffSettings Copy()
        {
            var copy = new TiffSettings();

            copy.Color = Color;
            copy.Dpi = Dpi;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is TiffSettings)) return false;
            var v = o as TiffSettings;

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