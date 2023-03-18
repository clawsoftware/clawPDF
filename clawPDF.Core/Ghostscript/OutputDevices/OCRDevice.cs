using System;
using System.Collections.Generic;
using clawSoft.clawPDF.Core.Jobs;
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
            if (_useOCRDevice)
                AddOCRDeviceParameters(parameters);
        }

        private void AddOCRDeviceParameters(IList<string> parameters)
        {
            // Solution with gs ORCdevice
            parameters.Add("-sDEVICE=ocr");
            parameters.Add("-r200");
            parameters.Add("-sOCRLanguage=" + Job.Profile.OCRSettings.OCRLanguage);
        }

        protected override string ComposeOutputFilename()
        {
            return Job.JobTempFileName + ".txt";
        }
    }
}