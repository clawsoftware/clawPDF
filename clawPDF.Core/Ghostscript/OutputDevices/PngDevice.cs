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
    public class PngDevice : OutputDevice
    {
        public PngDevice(IJob job) : base(job)
        {
        }

        public PngDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            switch (Job.Profile.PngSettings.Color)
            {
                case PngColor.BlackWhite:
                    parameters.Add("-sDEVICE=pngmonod");
                    break;

                case PngColor.Color24Bit:
                    parameters.Add("-sDEVICE=png16m");
                    break;

                case PngColor.Color32BitTransp:
                    parameters.Add("-sDEVICE=pngalpha");
                    break;

                case PngColor.Color4Bit:
                    parameters.Add("-sDEVICE=png16");
                    break;

                case PngColor.Color8Bit:
                    parameters.Add("-sDEVICE=png256");
                    break;

                case PngColor.Gray8Bit:
                    parameters.Add("-sDEVICE=pnggray");
                    break;
            }

            parameters.Add("-r" + Job.Profile.PngSettings.Dpi);
            parameters.Add("-dTextAlphaBits=4");
            parameters.Add("-dGraphicsAlphaBits=4");
        }

        protected override string ComposeOutputFilename()
        {
            //%d for multiple Pages
            return Job.JobTempFileName + "%d.png";
        }
    }
}