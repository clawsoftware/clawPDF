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
    ///     Extends OutputDevice to create XPS files
    /// </summary>
    public class XpsDevice : OutputDevice
    {
        private const int DpiMin = 4;
        private const int DpiMax = 2400;

        public XpsDevice(IJob job) : base(job)
        {
        }

        public XpsDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            parameters.Add("-sDEVICE=xpswrite");
            parameters.Add("-dEmbedAllFonts=true");

            SetPageOrientation(parameters, DistillerDictonaries);
            SetColorSchemeParameters(parameters);           
            GrayAndColorImagesCompressionAndResample(parameters, DistillerDictonaries);
            MonoImagesCompression(parameters);
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
            return Job.JobTempFileName + ".oxps";
        }
    }
}