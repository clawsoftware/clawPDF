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
    public class JpegDevice : OutputDevice
    {
        public JpegDevice(IJob job) : base(job)
        {
        }

        public JpegDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            switch (Job.Profile.JpegSettings.Color)
            {
                case JpegColor.Gray8Bit:
                    parameters.Add("-sDEVICE=jpeggray");
                    break;

                default:
                    parameters.Add("-sDEVICE=jpeg");
                    break;
            }

            parameters.Add("-dJPEGQ=" + Job.Profile.JpegSettings.Quality);
            parameters.Add("-r" + Job.Profile.JpegSettings.Dpi);
            parameters.Add("-dTextAlphaBits=4");
            parameters.Add("-dGraphicsAlphaBits=4");
        }

        protected override string ComposeOutputFilename()
        {
            //%d for multiple Pages
            return Job.JobTempFileName + "%d.jpg";
        }
    }
}