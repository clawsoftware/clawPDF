using System;
using SystemInterface.IO;
using pdfforge.PDFCreator.Core.Jobs;
using pdfforge.PDFCreator.Utilities;
using pdfforge.PDFCreator.Utilities.IO;

namespace pdfforge.PDFCreator.Core.Ghostscript.OutputDevices
{
    public abstract class MultiFileDevice : OutputDevice
    {
        protected MultiFileDevice(IJob job) : base (job)
        { }

        protected MultiFileDevice(IJob job, IFile file, IDirectory directory, IOsHelper osHelper) : base(job, file, directory, osHelper)
        { }

        public override void MoveOutputFiles()
        {   
            string outputDir = PathSafe.GetDirectoryName(Job.OutputFilenameTemplate) ?? "";
            string filenameBase = PathSafe.GetFileNameWithoutExtension(Job.OutputFilenameTemplate) ?? "output";
            string outfilebody = PathSafe.Combine(outputDir, filenameBase);

            Job.OutputFiles = new string[TempOutputFiles.Count]; //reserve space

            foreach (string tempoutputfile in TempOutputFiles)
            {
                string extension = PathSafe.GetExtension(TempOutputFiles[0]);

                string tempFileBase = PathSafe.GetFileNameWithoutExtension(tempoutputfile) ?? "output";
                string num = tempFileBase.Replace(Job.JobTempFileName, "");

                int numValue;
                if (int.TryParse(num, out numValue))
                {
                    int numDigits = (int)Math.Floor(Math.Log10(TempOutputFiles.Count) + 1);
                    num = numValue.ToString("D" + numDigits);
                }

                string outputfile;
                if (num == "1")
                    outputfile = outfilebody + extension;
                else
                    outputfile = outfilebody + num + extension;

                lock (_lockObject)
                {
                    var uniqueFilename = new UniqueFilename(outputfile, DirectoryWrap, FileWrap);

                    if (Job.Profile.AutoSave.Enabled && Job.Profile.AutoSave.EnsureUniqueFilenames)
                    {
                        outputfile = EnsureUniqueFilename(uniqueFilename);
                    }

                    if (!CopyFile(tempoutputfile, outputfile))
                    {
                        outputfile = EnsureUniqueFilename(uniqueFilename);

                        if (!CopyFile(tempoutputfile, outputfile))
                            //Throw exception after second attempt to copy failes.
                            throw new DeviceException("Error while copying to target file in second attempt. Process gets canceled.", 2);
                    }
                }
                DeleteFile(tempoutputfile);
                Job.OutputFiles[Convert.ToInt32(num) - 1] = outputfile;
           }
        }
    }
}
