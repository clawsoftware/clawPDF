using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.IO;
using NLog;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Core.Ghostscript.OutputDevices
{
    /// <summary>
    ///     The abstract class OutputDevice holds methods and properties that handle the Ghostscript parameters. The device
    ///     independent elements are defined here.
    ///     Other classes inherit OutputDevice to extend the functionality with device-specific functionality, i.e. to create
    ///     PDF or PNG files.
    ///     Especially the abstract function AddDeviceSpecificParameters has to be implemented to add parameters that are
    ///     required to use a given device.
    /// </summary>
    public abstract class OutputDevice
    {
        protected static object LockObject = new object();
        private readonly IFormatProvider _numberFormat = CultureInfo.InvariantCulture.NumberFormat;

        protected readonly IFile FileWrap;
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IOsHelper OsHelper;
        protected readonly IPathSafe PathSafe = new PathWrapSafe();

        /// <summary>
        ///     A list of Distiller dictionary strings. They will be added after all parameters are set.
        /// </summary>
        protected IList<string> DistillerDictonaries = new List<string>();

        /// <summary>
        ///     A list of output files produced during the conversion
        /// </summary>
        public IList<string> TempOutputFiles = new List<string>();

        protected OutputDevice(IJob job) : this(job, new FileWrap(), new OsHelper())
        {
        }

        protected OutputDevice(IJob job, IFile file, IOsHelper osHelper)
        {
            Job = job;
            FileWrap = file;
            OsHelper = osHelper;
        }

        /// <summary>
        ///     The Job that is converted
        /// </summary>
        public IJob Job { get; }

        /// <summary>
        ///     Get the list of Ghostscript Parameters. This List contains of a basic set of parameters together with some
        ///     device-specific
        ///     parameters that will be added by the device implementation
        /// </summary>
        /// <param name="ghostscriptVersion"></param>
        /// <returns>A list of parameters that will be passed to Ghostscript</returns>
        public IList<string> GetGhostScriptParameters(GhostscriptVersion ghostscriptVersion)
        {
            IList<string> parameters = new List<string>();

            parameters.Add("gs");
            parameters.Add("-sFONTPATH=" + OsHelper.WindowsFontsFolder);

            parameters.Add("-dNOPAUSE");
            parameters.Add("-dBATCH");

            if (!HasValidExtension(Job.OutputFilenameTemplate, Job.Profile.OutputFormat))
                MakeValidExtension(Job.OutputFilenameTemplate, Job.Profile.OutputFormat);

            AddOutputfileParameter(parameters);

            AddDeviceSpecificParameters(parameters);

            // Add user-defined parameters
            if (!string.IsNullOrEmpty(Job.Profile.Ghostscript.AdditionalGsParameters))
            {
                var args = FileUtil.Instance.CommandLineToArgs(Job.Profile.Ghostscript.AdditionalGsParameters);
                foreach (var s in args)
                    parameters.Add(s);
            }

            //Dictonary-Parameters must be the last Parameters
            if (DistillerDictonaries.Count > 0)
            {
                parameters.Add("-c");
                foreach (var parameter in DistillerDictonaries) parameters.Add(parameter);
            }

            //Don't add further paramters here, since the distiller-parameters should be the last!

            parameters.Add("-f");

            if (Job.Profile.Stamping.Enabled)
            {
                // Compose name of the stamp file based on the location and name of the inf file
                var stampFileName = PathSafe.Combine(Job.JobTempFolder,
                    PathSafe.GetFileNameWithoutExtension(Job.JobInfo.InfFile) + ".stm");
                CreateStampFile(stampFileName, Job.Profile);
                parameters.Add(stampFileName);
            }

            if (Job.Profile.CoverPage.Enabled)
                parameters.Add(Job.Profile.CoverPage.File);

            foreach (var sfi in Job.JobInfo.SourceFiles) parameters.Add(sfi.Filename);

            if (Job.Profile.AttachmentPage.Enabled)
                parameters.Add(Job.Profile.AttachmentPage.File);

            //Compose name of the pdfmark file based on the location and name of the inf file
            var pdfMarkFileName = PathSafe.Combine(Job.JobTempFolder, "metadata.mtd");
            Directory.CreateDirectory(Job.JobTempFolder);
            Directory.CreateDirectory(Job.JobTempFolder + @"\tempoutput");
            CreatePdfMarksFile(pdfMarkFileName);

            // Add pdfmark file as input file to set metadata
            parameters.Add(pdfMarkFileName);

            return parameters;
        }

        protected virtual void AddOutputfileParameter(IList<string> parameters)
        {
            parameters.Add("-sOutputFile=" + PathSafe.Combine(Job.JobTempOutputFolder, ComposeOutputFilename()));
        }

        /// <summary>
        ///     Create a file with metadata in the pdfmarks format. This file can be passed to Ghostscript to set Metadata of the
        ///     resulting document
        /// </summary>
        /// <param name="filename">Full path and filename of the resulting file</param>
        private void CreatePdfMarksFile(string filename)
        {
            var metadataContent = new StringBuilder();
            metadataContent.Append("/pdfmark where {pop} {userdict /pdfmark /cleartomark load put} ifelse\n[ ");
            metadataContent.Append("\n/Title " + EncodeGhostscriptParametersHex(Job.JobInfo.Metadata.Title));
            metadataContent.Append("\n/Author " + EncodeGhostscriptParametersHex(Job.JobInfo.Metadata.Author));
            metadataContent.Append("\n/Subject " + EncodeGhostscriptParametersHex(Job.JobInfo.Metadata.Subject));
            metadataContent.Append("\n/Keywords " + EncodeGhostscriptParametersHex(Job.JobInfo.Metadata.Keywords));
            metadataContent.Append("\n/Creator " + EncodeGhostscriptParametersHex(Job.JobInfo.Metadata.Producer));
            metadataContent.Append("\n/Producer " + EncodeGhostscriptParametersHex(Job.JobInfo.Metadata.Producer));
            metadataContent.Append("\n/DOCINFO pdfmark");

            AddViewerSettingsToMetadataContent(metadataContent);

            FileWrap.WriteAllText(filename, metadataContent.ToString());

            Logger.Debug("Created metadata file \"" + filename + "\"");
        }

        private string RgbToCmykColorString(Color color)
        {
            var red = color.R / 255.0;
            var green = color.G / 255.0;
            var blue = color.B / 255.0;

            var k = Math.Min(1 - red, 1 - green);
            k = Math.Min(k, 1 - blue);
            var c = (1 - red - k) / (1 - k);
            var m = (1 - green - k) / (1 - k);
            var y = (1 - blue - k) / (1 - k);

            return c.ToString("0.00", _numberFormat) + " " +
                   m.ToString("0.00", _numberFormat) + " " +
                   y.ToString("0.00", _numberFormat) + " " +
                   k.ToString("0.00", _numberFormat);
        }

        private void CreateStampFile(string filename, ConversionProfile profile)
        {
            // Create a resource manager to retrieve resources.
            var rm = new ResourceManager(typeof(CoreResources));

            var stampString = rm.GetString("PostScriptStamp");

            if (stampString == null)
                throw new InvalidOperationException("Error while fetching stamp template");

            var outlineWidth = 0;
            var outlineString = "show";

            if (profile.Stamping.FontAsOutline)
            {
                outlineWidth = profile.Stamping.FontOutlineWidth;
                outlineString = "true charpath stroke";
            }

            // Only Latin1 chars are allowed here
            stampString = stampString.Replace("[STAMPSTRING]",
                EncodeGhostscriptParametersOctal(profile.Stamping.StampText));
            stampString = stampString.Replace("[FONTNAME]", profile.Stamping.PostScriptFontName);
            stampString = stampString.Replace("[FONTSIZE]", profile.Stamping.FontSize.ToString(_numberFormat));
            stampString = stampString.Replace("[STAMPOUTLINEFONTTHICKNESS]",
                outlineWidth.ToString(CultureInfo.InvariantCulture));
            stampString = stampString.Replace("[USEOUTLINEFONT]", outlineString); // true charpath stroke OR show

            if (profile.OutputFormat == OutputFormat.PdfX ||
                profile.PdfSettings.ColorModel == ColorModel.Cmyk)
            {
                var colorString = RgbToCmykColorString(profile.Stamping.Color);
                stampString = stampString.Replace("[FONTCOLOR]", colorString);
                stampString = stampString.Replace("setrgbcolor", "setcmykcolor");
            }
            else
            {
                var colorString = (profile.Stamping.Color.R / 255.0).ToString("0.00", _numberFormat) + " " +
                                  (profile.Stamping.Color.G / 255.0).ToString("0.00", _numberFormat) + " " +
                                  (profile.Stamping.Color.B / 255.0).ToString("0.00", _numberFormat);
                stampString = stampString.Replace("[FONTCOLOR]", colorString);
            }

            FileWrap.WriteAllText(filename, stampString);
        }

        /*private string GetPostscriptFontName(string font)
        {
            return font.Replace(" ", "");
        }*/

        protected string EncodeGhostscriptParametersOctal(string String)
        {
            var sb = new StringBuilder();

            foreach (var c in String)
                switch (c)
                {
                    case '\\':
                        sb.Append("\\\\");
                        break;

                    case '{':
                        sb.Append("\\{");
                        break;

                    case '}':
                        sb.Append("\\}");
                        break;

                    case '[':
                        sb.Append("\\[");
                        break;

                    case ']':
                        sb.Append("\\]");
                        break;

                    case '(':
                        sb.Append("\\(");
                        break;

                    case ')':
                        sb.Append("\\)");
                        break;

                    default:
                        int charCode = c;
                        if (charCode > 127)
                            sb.Append("\\" + Convert.ToString(Math.Min(charCode, 255), 8));
                        else sb.Append(c);
                        break;
                }

            return sb.ToString();
        }

        protected string EncodeGhostscriptParametersHex(string String)
        {
            if (String == null)
                return "()";

            return "<FEFF" + BitConverter.ToString(Encoding.BigEndianUnicode.GetBytes(String)).Replace("-", "") + ">";
        }

        /// <summary>
        ///     This functions is called by inherited classes to add device-specific parameters to the Ghostscript parameter list
        /// </summary>
        /// <param name="parameters">The current list of parameters. This list may be modified in inherited classes.</param>
        protected abstract void AddDeviceSpecificParameters(IList<string> parameters);

        //protected abstract void SetDeviceSpecificOutputFile(IList<string> parameters);

        protected abstract string ComposeOutputFilename();

        private void AddViewerSettingsToMetadataContent(StringBuilder metadataContent)
        {
            metadataContent.Append("\n[\n/PageLayout ");

            switch (Job.Profile.PdfSettings.PageView)
            {
                case PageView.OneColumn:
                    metadataContent.Append("/OneColumn");
                    break;

                case PageView.TwoColumnsOddLeft:
                    metadataContent.Append("/TwoColumnLeft");
                    break;

                case PageView.TwoColumnsOddRight:
                    metadataContent.Append("/TwoColumnRight");
                    break;

                case PageView.TwoPagesOddLeft:
                    metadataContent.Append("/TwoPageLeft");
                    break;

                case PageView.TwoPagesOddRight:
                    metadataContent.Append("/TwoPageRight");
                    break;

                case PageView.OnePage:
                    metadataContent.Append("/SinglePage");
                    break;
            }

            metadataContent.Append("\n/PageMode ");
            switch (Job.Profile.PdfSettings.DocumentView)
            {
                case DocumentView.AttachmentsPanel:
                    metadataContent.Append("/UseAttachments");
                    break;

                case DocumentView.ContentGroupPanel:
                    metadataContent.Append("/UseOC");
                    break;

                case DocumentView.FullScreen:
                    metadataContent.Append("/FullScreen");
                    break;

                case DocumentView.Outline:
                    metadataContent.Append("/UseOutlines");
                    break;

                case DocumentView.ThumbnailImages:
                    metadataContent.Append("/UseThumbs");
                    break;

                default:
                    metadataContent.Append("/UseNone");
                    break;
            }

            if (Job.Profile.PdfSettings.ViewerStartsOnPage > Job.NumberOfPages)
                metadataContent.Append(" /Page " + Job.NumberOfPages);
            else if (Job.Profile.PdfSettings.ViewerStartsOnPage <= 0)
                metadataContent.Append(" /Page 1");
            else
                metadataContent.Append(" /Page " + Job.Profile.PdfSettings.ViewerStartsOnPage);

            metadataContent.Append("\n/DOCVIEW pdfmark");
        }

        public string[] GetValidExtensions(OutputFormat format)
        {
            string[] validExtensions;

            switch (format)
            {
                case OutputFormat.Jpeg:
                    validExtensions = new[] { ".jpg", ".jpeg" };
                    break;

                case OutputFormat.Tif:
                    validExtensions = new[] { ".tif", ".tiff" };
                    break;

                case OutputFormat.Pdf:
                case OutputFormat.PdfA1B:
                case OutputFormat.PdfA2B:
                case OutputFormat.PdfX:
                    validExtensions = new[] { ".pdf" };
                    break;

                case OutputFormat.Txt:
                    validExtensions = new[] { ".txt" };
                    break;

                default:
                    validExtensions = new[] { "." + format.ToString().ToLowerInvariant() };
                    break;
            }

            return validExtensions;
        }

        public bool HasValidExtension(string filename, OutputFormat format)
        {
            var validExtensions = GetValidExtensions(format);
            var ext = PathSafe.GetExtension(filename).ToLowerInvariant();

            return validExtensions.Contains(ext);
        }

        public string MakeValidExtension(string filename, OutputFormat format)
        {
            var validExtensions = GetValidExtensions(format);
            return PathSafe.ChangeExtension(filename, validExtensions[0]);
        }
    }
}