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
    ///     Compression settings for color and greyscale images
    /// </summary>
    public class CompressColorAndGray
    {
        public CompressColorAndGray()
        {
            Init();
        }

        /// <summary>
        ///     Settings for the compression method. Valid values are: Automatic, JpegMaximum, JpegHigh, JpegMedium, JpegLow,
        ///     JpegMinimum, JpegManual, Zip
        /// </summary>
        public CompressionColorAndGray Compression { get; set; }

        /// <summary>
        ///     Images will be resampled to this maximum resolution of the images, if resampling is enabled
        /// </summary>
        public int Dpi { get; set; }

        /// <summary>
        ///     If true, color and grayscale images will be processed according to the algorithm. If false, they will remain
        ///     uncompressed
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     Define a custom compression factor (requires JpegManual as method)
        /// </summary>
        public double JpegCompressionFactor { get; set; }

        /// <summary>
        ///     If true, the images will be resampled to a maximum resolution
        /// </summary>
        public bool Resampling { get; set; }

        private void Init()
        {
            Compression = CompressionColorAndGray.Automatic;
            Dpi = 300;
            Enabled = true;
            JpegCompressionFactor = 0.66;
            Resampling = false;
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                Compression = (CompressionColorAndGray)Enum.Parse(typeof(CompressionColorAndGray),
                    data.GetValue(@"" + path + @"Compression"));
            }
            catch
            {
                Compression = CompressionColorAndGray.Automatic;
            }

            try
            {
                Dpi = int.Parse(data.GetValue(@"" + path + @"Dpi"), CultureInfo.InvariantCulture);
            }
            catch
            {
                Dpi = 300;
            }

            try
            {
                Enabled = bool.Parse(data.GetValue(@"" + path + @"Enabled"));
            }
            catch
            {
                Enabled = true;
            }

            try
            {
                JpegCompressionFactor = double.Parse(data.GetValue(@"" + path + @"JpegCompressionFactor"),
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                JpegCompressionFactor = 0.66;
            }

            try
            {
                Resampling = bool.Parse(data.GetValue(@"" + path + @"Resampling"));
            }
            catch
            {
                Resampling = false;
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"Compression", Compression.ToString());
            data.SetValue(@"" + path + @"Dpi", Dpi.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"Enabled", Enabled.ToString());
            data.SetValue(@"" + path + @"JpegCompressionFactor",
                JpegCompressionFactor.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"Resampling", Resampling.ToString());
        }

        public CompressColorAndGray Copy()
        {
            var copy = new CompressColorAndGray();

            copy.Compression = Compression;
            copy.Dpi = Dpi;
            copy.Enabled = Enabled;
            copy.JpegCompressionFactor = JpegCompressionFactor;
            copy.Resampling = Resampling;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is CompressColorAndGray)) return false;
            var v = o as CompressColorAndGray;

            if (!Compression.Equals(v.Compression)) return false;
            if (!Dpi.Equals(v.Dpi)) return false;
            if (!Enabled.Equals(v.Enabled)) return false;
            if (!JpegCompressionFactor.Equals(v.JpegCompressionFactor)) return false;
            if (!Resampling.Equals(v.Resampling)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Compression=" + Compression);
            sb.AppendLine("Dpi=" + Dpi);
            sb.AppendLine("Enabled=" + Enabled);
            sb.AppendLine("JpegCompressionFactor=" + JpegCompressionFactor);
            sb.AppendLine("Resampling=" + Resampling);

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