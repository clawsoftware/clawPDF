using System.Collections.Generic;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Utilities;
using SystemInterface.IO;

namespace clawSoft.clawPDF.Core.Ghostscript.OutputDevices
{
    /// <summary>
    ///     Extends OutputDevice to create PNG files
    /// </summary>
    public class TiffDevice : OutputDevice
    {
        public TiffDevice(IJob job) : base(job)
        {
        }

        public TiffDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            switch (Job.Profile.TiffSettings.Color)
            {
                case TiffColor.BlackWhiteG4Fax:
                    parameters.Add("-sDEVICE=tiffg4");
                    break;

                case TiffColor.BlackWhiteG3Fax:
                    parameters.Add("-sDEVICE=tiffg3");
                    break;

                case TiffColor.BlackWhiteLzw:
                    parameters.Add("-sDEVICE=tifflzw");
                    break;

                case TiffColor.Gray8Bit:
                    parameters.Add("-sDEVICE=tiffgray");
                    parameters.Add("-sCompression=lzw");
                    break;

                case TiffColor.Color12Bit:
                    parameters.Add("-sDEVICE=tiff12nc");
                    parameters.Add("-sCompression=lzw");
                    break;

                case TiffColor.Color24Bit:
                    parameters.Add("-sDEVICE=tiff24nc");
                    parameters.Add("-sCompression=lzw");
                    break;
            }

            parameters.Add("-r" + Job.Profile.TiffSettings.Dpi);
            parameters.Add("-dTextAlphaBits=4");
            parameters.Add("-dGraphicsAlphaBits=4");
        }

        protected override string ComposeOutputFilename()
        {
            return Job.JobTempFileName + ".tif";
        }
    }
}