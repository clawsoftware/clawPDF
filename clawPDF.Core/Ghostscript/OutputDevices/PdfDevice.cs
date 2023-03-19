using System.Collections.Generic;
using System.Globalization;
using System.Text;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Utilities;
using SystemInterface.IO;

namespace clawSoft.clawPDF.Core.Ghostscript.OutputDevices
{
    /// <summary>
    ///     Extends OutputDevice to create PDF files
    /// </summary>
    public class PdfDevice : OutputDevice
    {
        private const int DpiMin = 4;
        private const int DpiMax = 2400;

        public PdfDevice(IJob job) : base(job)
        {
        }

        public PdfDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            parameters.Add("-sDEVICE=pdfwrite");
            parameters.Add("-dCompatibilityLevel=1.4");
            parameters.Add("-dPDFSETTINGS=/default");
            parameters.Add("-dEmbedAllFonts=true");

            SetPageOrientation(parameters, DistillerDictonaries);

            if (Job.Profile.OutputFormat != OutputFormat.PdfImage32
                && Job.Profile.OutputFormat != OutputFormat.PdfImage24
                && Job.Profile.OutputFormat != OutputFormat.PdfImage8
                && Job.Profile.OutputFormat != OutputFormat.PdfOCR32
                && Job.Profile.OutputFormat != OutputFormat.PdfOCR24
                && Job.Profile.OutputFormat != OutputFormat.PdfOCR8)
            {
                SetColorSchemeParameters(parameters);
            }

            //ColorSheme must be defined before adding def files of PdfA/X
            if (Job.Profile.OutputFormat == OutputFormat.PdfX)
                SetPdfXParameters(parameters);
            else if (Job.Profile.OutputFormat == OutputFormat.PdfImage32)
                SetPdfImage32Parameters(parameters);
            else if (Job.Profile.OutputFormat == OutputFormat.PdfImage24)
                SetPdfImage24Parameters(parameters);
            else if (Job.Profile.OutputFormat == OutputFormat.PdfImage8)
                SetPdfImage8Parameters(parameters);
            else if (Job.Profile.OutputFormat == OutputFormat.PdfOCR32)
                SetPdfOCR32Parameters(parameters);
            else if (Job.Profile.OutputFormat == OutputFormat.PdfOCR24)
                SetPdfOCR24Parameters(parameters);
            else if (Job.Profile.OutputFormat == OutputFormat.PdfOCR8)
                SetPdfOCR8Parameters(parameters);
            else if (Job.Profile.OutputFormat == OutputFormat.PdfA1B
                     || Job.Profile.OutputFormat == OutputFormat.PdfA2B
                     || Job.Profile.OutputFormat == OutputFormat.PdfA3B)
                SetPdfAParameters(parameters);

                GrayAndColorImagesCompressionAndResample(parameters, DistillerDictonaries);
                MonoImagesCompression(parameters);
        }

        private void SetPdfAParameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            switch (Job.Profile.OutputFormat)
            {
                case OutputFormat.PdfA1B:
                    parameters.Add("-dPDFA=1");
                    break;

                case OutputFormat.PdfA2B:
                    parameters.Add("-dPDFA=2");
                    break;

                case OutputFormat.PdfA3B:
                    parameters.Add("-dPDFA=3");
                    break;
            }

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath + "\"");

            //Add ICC profile
            var iccFile = PathSafe.Combine(shortenedTempPath, "profile.icc");

            //Set ICC Profile according to the color model
            switch (Job.Profile.PdfSettings.ColorModel)
            {
                case ColorModel.Cmyk:
                    FileWrap.WriteAllBytes(iccFile, CoreResources.WebCoatedFOGRA28);
                    break;

                case ColorModel.Gray:
                    FileWrap.WriteAllBytes(iccFile, CoreResources.ISOcoated_v2_grey1c_bas);
                    break;

                default:
                    FileWrap.WriteAllBytes(iccFile, CoreResources.eciRGB_v2);
                    break;
            }

            parameters.Add("-sPDFACompatibilityPolicy=1");

            parameters.Add("-sOutputICCProfile=" + iccFile);

            var defFile = PathSafe.Combine(Job.JobTempFolder, "pdfa_def.ps");
            var sb = new StringBuilder(CoreResources.PdfaDefinition);
            sb.Replace("[ICC_PROFILE]", "(" + EncodeGhostscriptParametersOctal(iccFile.Replace('\\', '/')) + ")");
            FileWrap.WriteAllText(defFile, sb.ToString());
            parameters.Add(defFile);
        }

        private void SetPdfXParameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            parameters.Add("-dPDFX");

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath +
                         "\"");

            //Add ICC profile
            var iccFile = PathSafe.Combine(shortenedTempPath, "profile.icc");
            FileWrap.WriteAllBytes(iccFile, CoreResources.ISOcoated_v2_300_eci);
            parameters.Add("-sOutputICCProfile=" + iccFile);

            var defFile = PathSafe.Combine(shortenedTempPath, "pdfx_def.ps");
            var sb = new StringBuilder(CoreResources.PdfxDefinition);
            sb.Replace("%/ICCProfile (ISO Coated sb.icc)",
                "/ICCProfile (" + EncodeGhostscriptParametersOctal(iccFile.Replace('\\', '/')) + ")");
            FileWrap.WriteAllText(defFile, sb.ToString());
            parameters.Add(defFile);
        }

        private void SetPdfImage32Parameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            int replaceDevice = parameters.IndexOf("-sDEVICE=pdfwrite");
            if (replaceDevice != -1)
            {
                parameters[replaceDevice] = "-sDEVICE=pdfimage32";
            }

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath +
                         "\"");
        }

        private void SetPdfImage24Parameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            int replaceDevice = parameters.IndexOf("-sDEVICE=pdfwrite");
            if (replaceDevice != -1)
            {
                parameters[replaceDevice] = "-sDEVICE=pdfimage24";
            }

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath +
                         "\"");
        }

        private void SetPdfImage8Parameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            int replaceDevice = parameters.IndexOf("-sDEVICE=pdfwrite");
            if (replaceDevice != -1)
            {
                parameters[replaceDevice] = "-sDEVICE=pdfimage8";
            }

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath +
                         "\"");
        }

        private void SetPdfOCR32Parameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            int replaceDevice = parameters.IndexOf("-sDEVICE=pdfwrite");
            if (replaceDevice != -1)
            {
                parameters[replaceDevice] = "-sDEVICE=pdfocr32";
            }
            parameters.Add("-r200");
            parameters.Add("-sOCRLanguage=" + Job.Profile.OCRSettings.OCRLanguage);

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath +
                         "\"");
        }

        private void SetPdfOCR24Parameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            int replaceDevice = parameters.IndexOf("-sDEVICE=pdfwrite");
            if (replaceDevice != -1)
            {
                parameters[replaceDevice] = "-sDEVICE=pdfocr24";
            }
            parameters.Add("-r200");
            parameters.Add("-sOCRLanguage=" + Job.Profile.OCRSettings.OCRLanguage);

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath +
                         "\"");
        }

        private void SetPdfOCR8Parameters(IList<string> parameters)
        {
            var shortenedTempPath = Job.JobTempFolder;

            int replaceDevice = parameters.IndexOf("-sDEVICE=pdfwrite");
            if (replaceDevice != -1)
            {
                parameters[replaceDevice] = "-sDEVICE=pdfocr8";
            }
            parameters.Add("-r200");
            parameters.Add("-sOCRLanguage=" + Job.Profile.OCRSettings.OCRLanguage);

            Logger.Debug("Shortened Temppath from\r\n\"" + Job.JobTempFolder + "\"\r\nto\r\n\"" + shortenedTempPath +
                         "\"");
        }

        private void GrayAndColorImagesCompressionAndResample(IList<string> parameters,
            IList<string> distillerDictonaries)
        {
            if (!Job.Profile.PdfSettings.CompressColorAndGray.Enabled)
            {
                parameters.Add("-dAutoFilterColorImages=false");
                parameters.Add("-dAutoFilterGrayImages=false");
                parameters.Add("-dEncodeColorImages=false");
                parameters.Add("-dEncodeGrayImages=false");
                return;
            }

            #region compress parameters

            switch (Job.Profile.PdfSettings.CompressColorAndGray.Compression)
            {
                case CompressionColorAndGray.JpegMaximum:
                    parameters.Add("-dAutoFilterColorImages=false");
                    parameters.Add("-dAutoFilterGrayImages=false");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/DCTEncode");
                    parameters.Add("-dGrayImageFilter=/DCTEncode");
                    distillerDictonaries.Add(
                        "<< /ColorImageDict <</QFactor 2.4 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    distillerDictonaries.Add(
                        "<< /GrayImageDict <</QFactor 2.4 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    break;

                case CompressionColorAndGray.JpegHigh:
                    parameters.Add("-dAutoFilterColorImages=false");
                    parameters.Add("-dAutoFilterGrayImages=false");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/DCTEncode");
                    parameters.Add("-dGrayImageFilter=/DCTEncode");
                    distillerDictonaries.Add(
                        "<< /ColorImageDict <</QFactor 1.3 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    distillerDictonaries.Add(
                        "<< /GrayImageDict <</QFactor 1.3 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    break;

                case CompressionColorAndGray.JpegMedium:
                    parameters.Add("-dAutoFilterColorImages=false");
                    parameters.Add("-dAutoFilterGrayImages=false");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/DCTEncode");
                    parameters.Add("-dGrayImageFilter=/DCTEncode");
                    distillerDictonaries.Add(
                        "<< /ColorImageDict <</QFactor 0.76 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    distillerDictonaries.Add(
                        "<< /GrayImageDict <</QFactor 0.76 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    break;

                case CompressionColorAndGray.JpegLow:
                    parameters.Add("-dAutoFilterColorImages=false");
                    parameters.Add("-dAutoFilterGrayImages=false");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/DCTEncode");
                    parameters.Add("-dGrayImageFilter=/DCTEncode");
                    distillerDictonaries.Add(
                        "<< /ColorImageDict <</QFactor 0.40 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    distillerDictonaries.Add(
                        "<< /GrayImageDict <</QFactor 0.40 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    break;

                case CompressionColorAndGray.JpegMinimum:
                    parameters.Add("-dAutoFilterColorImages=false");
                    parameters.Add("-dAutoFilterGrayImages=false");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/DCTEncode");
                    parameters.Add("-dGrayImageFilter=/DCTEncode");
                    distillerDictonaries.Add(
                        "<< /ColorImageDict <</QFactor 0.15 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    distillerDictonaries.Add(
                        "<< /GrayImageDict <</QFactor 0.15 /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    break;

                case CompressionColorAndGray.Zip:
                    parameters.Add("-dAutoFilterColorImages=false");
                    parameters.Add("-dAutoFilterGrayImages=false");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/FlateEncode");
                    parameters.Add("-dGrayImageFilter=/FlateEncode");
                    break;

                case CompressionColorAndGray.JpegManual:
                    parameters.Add("-dAutoFilterColorImages=false");
                    parameters.Add("-dAutoFilterGrayImages=false");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/DCTEncode");
                    parameters.Add("-dGrayImageFilter=/DCTEncode");
                    distillerDictonaries.Add("<< /ColorImageDict <</QFactor " +
                                             Job.Profile.PdfSettings.CompressColorAndGray.JpegCompressionFactor.ToString(CultureInfo.InvariantCulture) +
                                             " /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    distillerDictonaries.Add("<< /GrayImageDict <</QFactor " +
                                             Job.Profile.PdfSettings.CompressColorAndGray.JpegCompressionFactor.ToString(CultureInfo.InvariantCulture) +
                                             " /Blend 1 /HSample [2 1 1 2] /VSample [2 1 1 2]>> >> setdistillerparams");
                    break;

                case CompressionColorAndGray.Automatic:
                default:
                    parameters.Add("-dAutoFilterColorImages=true");
                    parameters.Add("-dAutoFilterGrayImages=true");
                    parameters.Add("-dEncodeColorImages=true");
                    parameters.Add("-dEncodeGrayImages=true");
                    parameters.Add("-dColorImageFilter=/DCTEncode");
                    parameters.Add("-dGrayImageFilter=/DCTEncode");
                    break;
            } //close switch

            #endregion compress parameters

            #region resample parameters

            if (Job.Profile.PdfSettings.CompressColorAndGray.Compression == CompressionColorAndGray.Automatic)
                return;

            if (Job.Profile.PdfSettings.CompressColorAndGray.Resampling)
            {
                if (Job.Profile.PdfSettings.CompressColorAndGray.Dpi < DpiMin)
                    Job.Profile.PdfSettings.CompressColorAndGray.Dpi = DpiMin;
                else if (Job.Profile.PdfSettings.CompressColorAndGray.Dpi > DpiMax)
                    Job.Profile.PdfSettings.CompressColorAndGray.Dpi = DpiMax;

                parameters.Add("-dDownsampleColorImages=true");
                parameters.Add("-dColorImageResolution=" + Job.Profile.PdfSettings.CompressColorAndGray.Dpi);
                parameters.Add("-dDownsampleGrayImages=true");
                parameters.Add("-dGrayImageResolution=" + Job.Profile.PdfSettings.CompressColorAndGray.Dpi);
            }

            #endregion resample parameters
        }

        private void MonoImagesCompression(IList<string> parameters)
        {
            if (!Job.Profile.PdfSettings.CompressMonochrome.Enabled)
            {
                parameters.Add("-dEncodeMonoImages=false");
                return;
            }

            switch (Job.Profile.PdfSettings.CompressMonochrome.Compression)
            {
                case CompressionMonochrome.CcittFaxEncoding:
                    parameters.Add("-dEncodeMonoImages=true");
                    parameters.Add("-dMonoImageFilter=/CCITTFaxEncode");
                    break;

                case CompressionMonochrome.RunLengthEncoding:
                    parameters.Add("-dEncodeMonoImages=true");
                    parameters.Add("-dMonoImageFilter=/RunLengthEncode");
                    break;

                case CompressionMonochrome.Zip:
                default:
                    parameters.Add("-dEncodeMonoImages=true");
                    parameters.Add("-dMonoImageFilter=/FlateEncode");
                    break;
            }

            if (Job.Profile.PdfSettings.CompressMonochrome.Resampling)
            {
                if (Job.Profile.PdfSettings.CompressMonochrome.Dpi < DpiMin)
                    Job.Profile.PdfSettings.CompressMonochrome.Dpi = DpiMin;
                else if (Job.Profile.PdfSettings.CompressMonochrome.Dpi > DpiMax)
                    Job.Profile.PdfSettings.CompressMonochrome.Dpi = DpiMax;

                parameters.Add("-dDownsampleMonoImages=true");
                parameters.Add("-dMonoImageDownsampleType=/Bicubic");
                parameters.Add("-dMonoImageResolution=" + Job.Profile.PdfSettings.CompressMonochrome.Dpi);
            }
        }

        private void SetColorSchemeParameters(IList<string> parameters)
        {
            // PDF/X only supports CMYK Colors
            if (Job.Profile.OutputFormat == OutputFormat.PdfX)
                if (Job.Profile.PdfSettings.ColorModel == ColorModel.Rgb)
                    Job.Profile.PdfSettings.ColorModel = ColorModel.Cmyk;

            switch (Job.Profile.PdfSettings.ColorModel)
            {
                case ColorModel.Cmyk:
                    parameters.Add(
                        "-sColorConversionStrategy=CMYK"); //Executes to execute the actual conversion to CMYK
                    parameters.Add("-dProcessColorModel=/DeviceCMYK");
                    break;

                case ColorModel.Gray:
                    parameters.Add("-sColorConversionStrategy=Gray"); //Executes the actual conversion to Gray
                    parameters.Add("-dProcessColorModel=/DeviceGray");
                    break;

                case ColorModel.Rgb:
                    /* if ((Job.Profile.OutputFormat == OutputFormat.PdfA1B) || (Job.Profile.OutputFormat == OutputFormat.PdfA2B))
                        parameters.Add("-sColorConversionStrategy=/UseDeviceIndependentColor");
                    else */
                    parameters.Add("-sColorConversionStrategy=RGB");
                    parameters.Add("-dProcessColorModel=/DeviceRGB");
                    parameters.Add("-dConvertCMYKImagesToRGB=true");
                    break;
            }
        }

        private void SetPageOrientation(IList<string> parameters, IList<string> distillerDictonaries)
        {
            switch (Job.Profile.PdfSettings.PageOrientation)
            {
                case PageOrientation.Landscape:
                    parameters.Add("-dAutoRotatePages=/None");
                    distillerDictonaries.Add("<</Orientation 3>> setpagedevice");
                    break;

                case PageOrientation.Automatic:
                    parameters.Add("-dAutoRotatePages=/PageByPage");
                    parameters.Add("-dParseDSCComments=false"); //necessary for automatic rotation
                    break;
                //case  PageOrientation.Portrait:
                default:
                    parameters.Add("-dAutoRotatePages=/None");
                    distillerDictonaries.Add("<</Orientation 0>> setpagedevice");
                    break;
            }
        }

        protected override string ComposeOutputFilename()
        {
            return Job.JobTempFileName + ".pdf";
        }
    }
}