using System;
using System.Collections.Generic;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Utilities;
using SystemInterface.IO;

namespace clawSoft.clawPDF.Core.Ghostscript.OutputDevices
{
    public class OCRDevice : OutputDevice
    {
        private readonly bool _useOCRDevice = true;

        public OCRDevice(IJob job) : base(job)
        {
        }

        public OCRDevice(IJob job, IFile file, IOsHelper osHelper) : base(job, file, osHelper)
        {
        }

        protected override void AddDeviceSpecificParameters(IList<string> parameters)
        {
            parameters.Add("-sDEVICE=ocr");
            parameters.Add("-dCompatibilityLevel=1.4");
            parameters.Add("-dPDFSETTINGS=/default");
            parameters.Add("-dEmbedAllFonts=true");

            SetPageOrientation(parameters, DistillerDictonaries);

            if (_useOCRDevice)
                AddOCRDeviceParameters(parameters);
        }

        private void AddOCRDeviceParameters(IList<string> parameters)
        {
            // Solution with gs ORCdevice
            parameters.Add("-r200");
            parameters.Add("-sOCRLanguage=" + Job.Profile.OCRSettings.OCRLanguage);
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
            return Job.JobTempFileName + ".txt";
        }
    }
}