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
    public class ConversionProfile
    {
        public ConversionProfile()
        {
            Init();
        }

        /// <summary>
        ///     Appends one or more pages at the end of the converted document
        /// </summary>
        public AttachmentPage AttachmentPage { get; set; }

        /// <summary>
        ///     AutoSave allows to create PDF files without user interaction
        /// </summary>
        public AutoSave AutoSave { get; set; }

        /// <summary>
        ///     Adds a page background to the resulting document
        /// </summary>
        public BackgroundPage BackgroundPage { get; set; }

        /// <summary>
        ///     Inserts one or more pages at the beginning of the converted document
        /// </summary>
        public CoverPage CoverPage { get; set; }

        /// <summary>
        ///     Opens the default E-mail client with the converted document as attachment
        /// </summary>
        public EmailClient EmailClient { get; set; }

        /// <summary>
        ///     Sends a mail without user interaction through SMTP
        /// </summary>
        public EmailSmtp EmailSmtp { get; set; }

        /// <summary>
        ///     Upload the converted documents with FTP
        /// </summary>
        public Ftp Ftp { get; set; }

        /// <summary>
        ///     Ghostscript settings
        /// </summary>
        public Ghostscript Ghostscript { get; set; }

        /// <summary>
        ///     Settings for the JPEG output format
        /// </summary>
        public JpegSettings JpegSettings { get; set; }

        /// <summary>
        ///     Settings for the PDF output format
        /// </summary>
        public PdfSettings PdfSettings { get; set; }

        /// <summary>
        ///     Settings for the PNG output format
        /// </summary>
        public PngSettings PngSettings { get; set; }

        /// <summary>
        ///     Print the document to a physical printer
        /// </summary>
        public Printing Printing { get; set; }

        /// <summary>
        ///     Properties of the profile
        /// </summary>
        public Properties Properties { get; set; }

        /// <summary>
        ///     Settings to control the behaviour of the save dialog
        /// </summary>
        public SaveDialog SaveDialog { get; set; }

        /// <summary>
        ///     The scripting action allows to run a script after the conversion
        /// </summary>
        public Scripting Scripting { get; set; }

        /// <summary>
        ///     Place a stamp text on all pages of the document
        /// </summary>
        public Stamping Stamping { get; set; }

        /// <summary>
        ///     Settings for the TIFF output format
        /// </summary>
        public TiffSettings TiffSettings { get; set; }

        /// <summary>
        ///     Template for the Author field. This may contain tokens.
        /// </summary>
        public string AuthorTemplate { get; set; }

        /// <summary>
        ///     Template of which the filename will be created. This may contain Tokens.
        /// </summary>
        public string FileNameTemplate { get; set; }

        /// <summary>
        ///     GUID of the profile
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        ///     Name of the profile
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Open the default viewer after converting the document
        /// </summary>
        public bool OpenViewer { get; set; }

        /// <summary>
        ///     Default format for this print job. Valid values are: Pdf, PdfA, PdfX, Jpeg, Png, Tif
        /// </summary>
        public OutputFormat OutputFormat { get; set; }

        /// <summary>
        ///     If true, a progress window will be shown during conversion
        /// </summary>
        public bool ShowProgress { get; set; }

        /// <summary>
        ///     Allows to skip the print dialog (where metadata are set) and directly proceed to the save dialog
        /// </summary>
        public bool SkipPrintDialog { get; set; }

        /// <summary>
        ///     Template for the Title field. This may contain tokens.
        /// </summary>
        public string TitleTemplate { get; set; }

        // Custom Code starts here
        // START_CUSTOM_SECTION:GENERAL
        public bool IsDefault => Guid == ProfileGuids.DEFAULT_PROFILE_GUID;

        private void Init()
        {
            AttachmentPage = new AttachmentPage();
            AutoSave = new AutoSave();
            BackgroundPage = new BackgroundPage();
            CoverPage = new CoverPage();
            EmailClient = new EmailClient();
            EmailSmtp = new EmailSmtp();
            Ftp = new Ftp();
            Ghostscript = new Ghostscript();
            JpegSettings = new JpegSettings();
            PdfSettings = new PdfSettings();
            PngSettings = new PngSettings();
            Printing = new Printing();
            Properties = new Properties();
            SaveDialog = new SaveDialog();
            Scripting = new Scripting();
            Stamping = new Stamping();
            TiffSettings = new TiffSettings();
            AuthorTemplate = "<PrintJobAuthor>";
            FileNameTemplate = "<Title>";
            Guid = "";
            Name = "NewProfile";
            OpenViewer = true;
            OutputFormat = OutputFormat.Pdf;
            ShowProgress = true;
            SkipPrintDialog = false;
            TitleTemplate = "<PrintJobName>";
        }

        public void ReadValues(Data data, string path)
        {
            AttachmentPage.ReadValues(data, path + @"AttachmentPage\");
            AutoSave.ReadValues(data, path + @"AutoSave\");
            BackgroundPage.ReadValues(data, path + @"BackgroundPage\");
            CoverPage.ReadValues(data, path + @"CoverPage\");
            EmailClient.ReadValues(data, path + @"EmailClient\");
            EmailSmtp.ReadValues(data, path + @"EmailSmtp\");
            Ftp.ReadValues(data, path + @"Ftp\");
            Ghostscript.ReadValues(data, path + @"Ghostscript\");
            JpegSettings.ReadValues(data, path + @"JpegSettings\");
            PdfSettings.ReadValues(data, path + @"PdfSettings\");
            PngSettings.ReadValues(data, path + @"PngSettings\");
            Printing.ReadValues(data, path + @"Printing\");
            Properties.ReadValues(data, path + @"Properties\");
            SaveDialog.ReadValues(data, path + @"SaveDialog\");
            Scripting.ReadValues(data, path + @"Scripting\");
            Stamping.ReadValues(data, path + @"Stamping\");
            TiffSettings.ReadValues(data, path + @"TiffSettings\");
            try
            {
                AuthorTemplate = Data.UnescapeString(data.GetValue(@"" + path + @"AuthorTemplate"));
            }
            catch
            {
                AuthorTemplate = "<PrintJobAuthor>";
            }

            try
            {
                FileNameTemplate = Data.UnescapeString(data.GetValue(@"" + path + @"FileNameTemplate"));
            }
            catch
            {
                FileNameTemplate = "<Title>";
            }

            try
            {
                Guid = Data.UnescapeString(data.GetValue(@"" + path + @"Guid"));
            }
            catch
            {
                Guid = "";
            }

            try
            {
                Name = Data.UnescapeString(data.GetValue(@"" + path + @"Name"));
            }
            catch
            {
                Name = "NewProfile";
            }

            try
            {
                OpenViewer = bool.Parse(data.GetValue(@"" + path + @"OpenViewer"));
            }
            catch
            {
                OpenViewer = true;
            }

            try
            {
                OutputFormat =
                    (OutputFormat)Enum.Parse(typeof(OutputFormat), data.GetValue(@"" + path + @"OutputFormat"));
            }
            catch
            {
                OutputFormat = OutputFormat.Pdf;
            }

            try
            {
                ShowProgress = bool.Parse(data.GetValue(@"" + path + @"ShowProgress"));
            }
            catch
            {
                ShowProgress = true;
            }

            try
            {
                SkipPrintDialog = bool.Parse(data.GetValue(@"" + path + @"SkipPrintDialog"));
            }
            catch
            {
                SkipPrintDialog = false;
            }

            try
            {
                TitleTemplate = Data.UnescapeString(data.GetValue(@"" + path + @"TitleTemplate"));
            }
            catch
            {
                TitleTemplate = "<PrintJobName>";
            }
        }

        public void StoreValues(Data data, string path)
        {
            AttachmentPage.StoreValues(data, path + @"AttachmentPage\");
            AutoSave.StoreValues(data, path + @"AutoSave\");
            BackgroundPage.StoreValues(data, path + @"BackgroundPage\");
            CoverPage.StoreValues(data, path + @"CoverPage\");
            EmailClient.StoreValues(data, path + @"EmailClient\");
            EmailSmtp.StoreValues(data, path + @"EmailSmtp\");
            Ftp.StoreValues(data, path + @"Ftp\");
            Ghostscript.StoreValues(data, path + @"Ghostscript\");
            JpegSettings.StoreValues(data, path + @"JpegSettings\");
            PdfSettings.StoreValues(data, path + @"PdfSettings\");
            PngSettings.StoreValues(data, path + @"PngSettings\");
            Printing.StoreValues(data, path + @"Printing\");
            Properties.StoreValues(data, path + @"Properties\");
            SaveDialog.StoreValues(data, path + @"SaveDialog\");
            Scripting.StoreValues(data, path + @"Scripting\");
            Stamping.StoreValues(data, path + @"Stamping\");
            TiffSettings.StoreValues(data, path + @"TiffSettings\");
            data.SetValue(@"" + path + @"AuthorTemplate", Data.EscapeString(AuthorTemplate));
            data.SetValue(@"" + path + @"FileNameTemplate", Data.EscapeString(FileNameTemplate));
            data.SetValue(@"" + path + @"Guid", Data.EscapeString(Guid));
            data.SetValue(@"" + path + @"Name", Data.EscapeString(Name));
            data.SetValue(@"" + path + @"OpenViewer", OpenViewer.ToString());
            data.SetValue(@"" + path + @"OutputFormat", OutputFormat.ToString());
            data.SetValue(@"" + path + @"ShowProgress", ShowProgress.ToString());
            data.SetValue(@"" + path + @"SkipPrintDialog", SkipPrintDialog.ToString());
            data.SetValue(@"" + path + @"TitleTemplate", Data.EscapeString(TitleTemplate));
        }

        public ConversionProfile Copy()
        {
            var copy = new ConversionProfile();

            copy.AttachmentPage = AttachmentPage.Copy();
            copy.AutoSave = AutoSave.Copy();
            copy.BackgroundPage = BackgroundPage.Copy();
            copy.CoverPage = CoverPage.Copy();
            copy.EmailClient = EmailClient.Copy();
            copy.EmailSmtp = EmailSmtp.Copy();
            copy.Ftp = Ftp.Copy();
            copy.Ghostscript = Ghostscript.Copy();
            copy.JpegSettings = JpegSettings.Copy();
            copy.PdfSettings = PdfSettings.Copy();
            copy.PngSettings = PngSettings.Copy();
            copy.Printing = Printing.Copy();
            copy.Properties = Properties.Copy();
            copy.SaveDialog = SaveDialog.Copy();
            copy.Scripting = Scripting.Copy();
            copy.Stamping = Stamping.Copy();
            copy.TiffSettings = TiffSettings.Copy();
            copy.AuthorTemplate = AuthorTemplate;
            copy.FileNameTemplate = FileNameTemplate;
            copy.Guid = Guid;
            copy.Name = Name;
            copy.OpenViewer = OpenViewer;
            copy.OutputFormat = OutputFormat;
            copy.ShowProgress = ShowProgress;
            copy.SkipPrintDialog = SkipPrintDialog;
            copy.TitleTemplate = TitleTemplate;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is ConversionProfile)) return false;
            var v = o as ConversionProfile;

            if (!AttachmentPage.Equals(v.AttachmentPage)) return false;
            if (!AutoSave.Equals(v.AutoSave)) return false;
            if (!BackgroundPage.Equals(v.BackgroundPage)) return false;
            if (!CoverPage.Equals(v.CoverPage)) return false;
            if (!EmailClient.Equals(v.EmailClient)) return false;
            if (!EmailSmtp.Equals(v.EmailSmtp)) return false;
            if (!Ftp.Equals(v.Ftp)) return false;
            if (!Ghostscript.Equals(v.Ghostscript)) return false;
            if (!JpegSettings.Equals(v.JpegSettings)) return false;
            if (!PdfSettings.Equals(v.PdfSettings)) return false;
            if (!PngSettings.Equals(v.PngSettings)) return false;
            if (!Printing.Equals(v.Printing)) return false;
            if (!Properties.Equals(v.Properties)) return false;
            if (!SaveDialog.Equals(v.SaveDialog)) return false;
            if (!Scripting.Equals(v.Scripting)) return false;
            if (!Stamping.Equals(v.Stamping)) return false;
            if (!TiffSettings.Equals(v.TiffSettings)) return false;
            if (!AuthorTemplate.Equals(v.AuthorTemplate)) return false;
            if (!FileNameTemplate.Equals(v.FileNameTemplate)) return false;
            if (!Guid.Equals(v.Guid)) return false;
            if (!Name.Equals(v.Name)) return false;
            if (!OpenViewer.Equals(v.OpenViewer)) return false;
            if (!OutputFormat.Equals(v.OutputFormat)) return false;
            if (!ShowProgress.Equals(v.ShowProgress)) return false;
            if (!SkipPrintDialog.Equals(v.SkipPrintDialog)) return false;
            if (!TitleTemplate.Equals(v.TitleTemplate)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[AttachmentPage]");
            sb.AppendLine(AttachmentPage.ToString());
            sb.AppendLine("[AutoSave]");
            sb.AppendLine(AutoSave.ToString());
            sb.AppendLine("[BackgroundPage]");
            sb.AppendLine(BackgroundPage.ToString());
            sb.AppendLine("[CoverPage]");
            sb.AppendLine(CoverPage.ToString());
            sb.AppendLine("[EmailClient]");
            sb.AppendLine(EmailClient.ToString());
            sb.AppendLine("[EmailSmtp]");
            sb.AppendLine(EmailSmtp.ToString());
            sb.AppendLine("[Ftp]");
            sb.AppendLine(Ftp.ToString());
            sb.AppendLine("[Ghostscript]");
            sb.AppendLine(Ghostscript.ToString());
            sb.AppendLine("[JpegSettings]");
            sb.AppendLine(JpegSettings.ToString());
            sb.AppendLine("[PdfSettings]");
            sb.AppendLine(PdfSettings.ToString());
            sb.AppendLine("[PngSettings]");
            sb.AppendLine(PngSettings.ToString());
            sb.AppendLine("[Printing]");
            sb.AppendLine(Printing.ToString());
            sb.AppendLine("[Properties]");
            sb.AppendLine(Properties.ToString());
            sb.AppendLine("[SaveDialog]");
            sb.AppendLine(SaveDialog.ToString());
            sb.AppendLine("[Scripting]");
            sb.AppendLine(Scripting.ToString());
            sb.AppendLine("[Stamping]");
            sb.AppendLine(Stamping.ToString());
            sb.AppendLine("[TiffSettings]");
            sb.AppendLine(TiffSettings.ToString());
            sb.AppendLine("AuthorTemplate=" + AuthorTemplate);
            sb.AppendLine("FileNameTemplate=" + FileNameTemplate);
            sb.AppendLine("Guid=" + Guid);
            sb.AppendLine("Name=" + Name);
            sb.AppendLine("OpenViewer=" + OpenViewer);
            sb.AppendLine("OutputFormat=" + OutputFormat);
            sb.AppendLine("ShowProgress=" + ShowProgress);
            sb.AppendLine("SkipPrintDialog=" + SkipPrintDialog);
            sb.AppendLine("TitleTemplate=" + TitleTemplate);

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