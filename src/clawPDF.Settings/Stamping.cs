using System.Drawing;
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
    ///     Place a stamp text on all pages of the document
    /// </summary>
    public class Stamping
    {
        public Stamping()
        {
            Init();
        }

        /// <summary>
        ///     Color of the text
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     If true, the document all pages will be stamped with a text
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     If true, the text will be rendered as outline. If false, it will be filled.
        /// </summary>
        public bool FontAsOutline { get; set; }

        /// <summary>
        ///     Name of the font. (this is only used as a hint, the PostScriptFontName contains the real name)
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        ///     Width of the outline
        /// </summary>
        public int FontOutlineWidth { get; set; }

        /// <summary>
        ///     Size of the font
        /// </summary>
        public float FontSize { get; set; }

        /// <summary>
        ///     PostScript name of the stamp font.
        /// </summary>
        public string PostScriptFontName { get; set; }

        /// <summary>
        ///     Text that will be stamped
        /// </summary>
        public string StampText { get; set; }

        private void Init()
        {
            Color = ColorTranslator.FromHtml("#CCCCCC");
            Enabled = false;
            FontAsOutline = true;
            FontName = "Arial";
            FontOutlineWidth = 2;
            FontSize = 48;
            PostScriptFontName = "Arial";
            StampText = "Confidential";
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                var value = data.GetValue(@"" + path + @"Color").Trim();
                if (value.Length == 0) Color = ColorTranslator.FromHtml("#CCCCCC");
                else Color = ColorTranslator.FromHtml(value);
            }
            catch
            {
                Color = ColorTranslator.FromHtml("#CCCCCC");
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
                FontAsOutline = bool.Parse(data.GetValue(@"" + path + @"FontAsOutline"));
            }
            catch
            {
                FontAsOutline = true;
            }

            try
            {
                FontName = Data.UnescapeString(data.GetValue(@"" + path + @"FontName"));
            }
            catch
            {
                FontName = "Arial";
            }

            try
            {
                FontOutlineWidth = int.Parse(data.GetValue(@"" + path + @"FontOutlineWidth"),
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                FontOutlineWidth = 2;
            }

            try
            {
                FontSize = float.Parse(data.GetValue(@"" + path + @"FontSize"), CultureInfo.InvariantCulture);
            }
            catch
            {
                FontSize = 48;
            }

            try
            {
                PostScriptFontName = Data.UnescapeString(data.GetValue(@"" + path + @"PostScriptFontName"));
            }
            catch
            {
                PostScriptFontName = "Arial";
            }

            try
            {
                StampText = Data.UnescapeString(data.GetValue(@"" + path + @"StampText"));
            }
            catch
            {
                StampText = "Confidential";
            }
        }

        public void StoreValues(Data data, string path)
        {
            data.SetValue(@"" + path + @"Color", ColorTranslator.ToHtml(Color));
            data.SetValue(@"" + path + @"Enabled", Enabled.ToString());
            data.SetValue(@"" + path + @"FontAsOutline", FontAsOutline.ToString());
            data.SetValue(@"" + path + @"FontName", Data.EscapeString(FontName));
            data.SetValue(@"" + path + @"FontOutlineWidth", FontOutlineWidth.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"FontSize", FontSize.ToString(CultureInfo.InvariantCulture));
            data.SetValue(@"" + path + @"PostScriptFontName", Data.EscapeString(PostScriptFontName));
            data.SetValue(@"" + path + @"StampText", Data.EscapeString(StampText));
        }

        public Stamping Copy()
        {
            var copy = new Stamping();

            copy.Color = Color;
            copy.Enabled = Enabled;
            copy.FontAsOutline = FontAsOutline;
            copy.FontName = FontName;
            copy.FontOutlineWidth = FontOutlineWidth;
            copy.FontSize = FontSize;
            copy.PostScriptFontName = PostScriptFontName;
            copy.StampText = StampText;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is Stamping)) return false;
            var v = o as Stamping;

            if (!Color.Equals(v.Color)) return false;
            if (!Enabled.Equals(v.Enabled)) return false;
            if (!FontAsOutline.Equals(v.FontAsOutline)) return false;
            if (!FontName.Equals(v.FontName)) return false;
            if (!FontOutlineWidth.Equals(v.FontOutlineWidth)) return false;
            if (!FontSize.Equals(v.FontSize)) return false;
            if (!PostScriptFontName.Equals(v.PostScriptFontName)) return false;
            if (!StampText.Equals(v.StampText)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Color=" + Color);
            sb.AppendLine("Enabled=" + Enabled);
            sb.AppendLine("FontAsOutline=" + FontAsOutline);
            sb.AppendLine("FontName=" + FontName);
            sb.AppendLine("FontOutlineWidth=" + FontOutlineWidth);
            sb.AppendLine("FontSize=" + FontSize);
            sb.AppendLine("PostScriptFontName=" + PostScriptFontName);
            sb.AppendLine("StampText=" + StampText);

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