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
    ///     Extends OutputDevice to create SVG file
    /// </summary>
    public class SvgDevice : OutputDevice
    {
        public SvgDevice(IJob job) : base(job)
        {
        }

        public SvgDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            parameters.Add("-sDEVICE=pdfwrite");
            parameters.Add("-dEmbedAllFonts=true");

            SetPageOrientation(parameters, DistillerDictonaries);
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
            return Job.JobTempFileName + ".svg";
        }
    }
}