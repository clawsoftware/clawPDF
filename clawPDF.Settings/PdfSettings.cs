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
    ///     Settings for the PDF output format
    /// </summary>
    public class PdfSettings
    {
        public PdfSettings()
        {
            Init();
        }

        /// <summary>
        ///     Compression settings for color and greyscale images
        /// </summary>
        public CompressColorAndGray CompressColorAndGray { get; set; }

        /// <summary>
        ///     Compression settings for monochrome images
        /// </summary>
        public CompressMonochrome CompressMonochrome { get; set; }

        /// <summary>
        ///     PDF Security options
        /// </summary>
        public Security Security { get; set; }

        /// <summary>
        ///     Digitally sign the PDF document
        /// </summary>
        public Signature Signature { get; set; }

        /// <summary>
        ///     Color model of the PDF (does not apply to images). Valid values are: Rgb, Cmyk, Gray
        /// </summary>
        public ColorModel ColorModel { get; set; }

        /// <summary>
        ///     Defines which controls will be opened in the reader. Valid values are: NoOutLineNoThumbnailImages,Outline,
        ///     ThumbnailImages, FullScreen, ContentGroupPanel ,AttachmentsPanel
        /// </summary>
        public DocumentView DocumentView { get; set; }

        /// <summary>
        ///     If true, the PDF will be linearized, which allows the first pages to be displayed even if the rest of the document
        ///     still is loading.
        /// </summary>
        public bool FastWebView { get; set; }

        /// <summary>
        ///     Define how pages are automatically rotated. Valid values are: Automatic, Portrait, Landscape
        /// </summary>
        public PageOrientation PageOrientation { get; set; }

        /// <summary>
        ///     Defines how the document will be opened in the reader. Valid values are: OnePage, OneColumn, TwoColumnsOddLeft,
        ///     TwoColumnsOddRight, TwoPagesOddLeft, TwoPagesOddRight
        /// </summary>
        public PageView PageView { get; set; }

        /// <summary>
        ///     Defines the page number the viewer will start on when opening the document
        /// </summary>
        public int ViewerStartsOnPage { get; set; }

        private void Init()
        {
            CompressColorAndGray = new CompressColorAndGray();
            CompressMonochrome = new CompressMonochrome();
            Security = new Security();
            Signature = new Signature();
            ColorModel = ColorModel.Rgb;
            DocumentView = DocumentView.NoOutLineNoThumbnailImages;
            FastWebView = true;
            PageOrientation = PageOrientation.Automatic;
            PageView = PageView.OnePage;
            ViewerStartsOnPage = 1;
        }

        public void ReadValues(Data data, string path)
        {
            CompressColorAndGray.ReadValues(data, path + @"CompressColorAndGray\");
            CompressMonochrome.ReadValues(data, path + @"CompressMonochrome\");
            Security.ReadValues(data, path + @"Security\");
            Signature.ReadValues(data, path + @"Signature\");
            try
            {
                ColorModel = (ColorModel)Enum.Parse(typeof(ColorModel), data.GetValue(@"" + path + @"ColorModel"));
            }
            catch
            {
                ColorModel = ColorModel.Rgb;
            }

            try
            {
                DocumentView =
                    (DocumentView)Enum.Parse(typeof(DocumentView), data.GetValue(@"" + path + @"DocumentView"));
            }
            catch
            {
                DocumentView = DocumentView.NoOutLineNoThumbnailImages;
            }

            try
            {
                FastWebView = bool.Parse(data.GetValue(@"" + path + @"FastWebView"));
            }
            catch
            {
                FastWebView = true;
            }

            try
            {
                PageOrientation = (PageOrientation)Enum.Parse(typeof(PageOrientation),
                    data.GetValue(@"" + path + @"PageOrientation"));
            }
            catch
            {
                PageOrientation = PageOrientation.Automatic;
            }

            try
            {
                PageView = (PageView)Enum.Parse(typeof(PageView), data.GetValue(@"" + path + @"PageView"));
            }
            catch
            {
                PageView = PageView.OnePage;
            }

            try
            {
                ViewerStartsOnPage = int.Parse(data.GetValue(@"" + path + @"ViewerStartsOnPage"),
                    CultureInfo.InvariantCulture);
            }
            catch
            {
                ViewerStartsOnPage = 1;
            }
        }

        public void StoreValues(Data data, string path)
        {
            CompressColorAndGray.StoreValues(data, path + @"CompressColorAndGray\");
            CompressMonochrome.StoreValues(data, path + @"CompressMonochrome\");
            Security.StoreValues(data, path + @"Security\");
            Signature.StoreValues(data, path + @"Signature\");
            data.SetValue(@"" + path + @"ColorModel", ColorModel.ToString());
            data.SetValue(@"" + path + @"DocumentView", DocumentView.ToString());
            data.SetValue(@"" + path + @"FastWebView", FastWebView.ToString());
            data.SetValue(@"" + path + @"PageOrientation", PageOrientation.ToString());
            data.SetValue(@"" + path + @"PageView", PageView.ToString());
            data.SetValue(@"" + path + @"ViewerStartsOnPage",
                ViewerStartsOnPage.ToString(CultureInfo.InvariantCulture));
        }

        public PdfSettings Copy()
        {
            var copy = new PdfSettings();

            copy.CompressColorAndGray = CompressColorAndGray.Copy();
            copy.CompressMonochrome = CompressMonochrome.Copy();
            copy.Security = Security.Copy();
            copy.Signature = Signature.Copy();
            copy.ColorModel = ColorModel;
            copy.DocumentView = DocumentView;
            copy.FastWebView = FastWebView;
            copy.PageOrientation = PageOrientation;
            copy.PageView = PageView;
            copy.ViewerStartsOnPage = ViewerStartsOnPage;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is PdfSettings)) return false;
            var v = o as PdfSettings;

            if (!CompressColorAndGray.Equals(v.CompressColorAndGray)) return false;
            if (!CompressMonochrome.Equals(v.CompressMonochrome)) return false;
            if (!Security.Equals(v.Security)) return false;
            if (!Signature.Equals(v.Signature)) return false;
            if (!ColorModel.Equals(v.ColorModel)) return false;
            if (!DocumentView.Equals(v.DocumentView)) return false;
            if (!FastWebView.Equals(v.FastWebView)) return false;
            if (!PageOrientation.Equals(v.PageOrientation)) return false;
            if (!PageView.Equals(v.PageView)) return false;
            if (!ViewerStartsOnPage.Equals(v.ViewerStartsOnPage)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[CompressColorAndGray]");
            sb.AppendLine(CompressColorAndGray.ToString());
            sb.AppendLine("[CompressMonochrome]");
            sb.AppendLine(CompressMonochrome.ToString());
            sb.AppendLine("[Security]");
            sb.AppendLine(Security.ToString());
            sb.AppendLine("[Signature]");
            sb.AppendLine(Signature.ToString());
            sb.AppendLine("ColorModel=" + ColorModel);
            sb.AppendLine("DocumentView=" + DocumentView);
            sb.AppendLine("FastWebView=" + FastWebView);
            sb.AppendLine("PageOrientation=" + PageOrientation);
            sb.AppendLine("PageView=" + PageView);
            sb.AppendLine("ViewerStartsOnPage=" + ViewerStartsOnPage);

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