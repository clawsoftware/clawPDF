using System;
using System.IO;
using System.Threading;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Xps;
using clawSoft.clawPDF.Core.Xps.OutputFileMover;

namespace clawSoft.clawPDF.Core.Jobs
{
    internal class XpsJob : AbstractJob
    {
        public XpsJob(IJobInfo jobInfo, ConversionProfile profile, JobTranslations jobTranslations)
            : base(jobInfo, profile, jobTranslations)
        {
            JobTempFolder = Path.Combine(Path.Combine(Path.GetTempPath(), "clawPDF\\Temp"),
                "Job_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
            JobTempOutputFolder = Path.Combine(JobTempFolder, "tempoutput");
            Directory.CreateDirectory(JobTempFolder);
            Directory.CreateDirectory(JobTempOutputFolder);
        }

#pragma warning disable CS0067

        public override event EventHandler<QueryPasswordEventArgs> OnRetypeSmtpPassword;

#pragma warning restore CS0067

        protected override JobState RunJobWork()
        {
            SetThreadName();

            OutputFiles.Clear();
            SetUpActions();

            var converter = new XpsConverter(JobInfo);
            var path = Path.Combine(JobTempOutputFolder, JobTempFileName + Path.GetExtension(OutputFilenameTemplate));
            converter.Convert(path);

            //PDFProcessor.process

            MoveOutputFile(path);

            JobState = JobState.Succeeded;
            return JobState;
        }

        private void MoveOutputFile(string file)
        {
            var fileMover = new SingleFileMover();

            if (Profile.AutoSave.Enabled && Profile.AutoSave.EnsureUniqueFilenames)
                fileMover.UniqueFileNameEnabled = true;

            var movedFile = fileMover.MoveSingleOutputFile(file, OutputFilenameTemplate);
            OutputFiles.Add(movedFile);
        }

        private void SetUpActions()
        {
            // TODO
        }

        private void SetThreadName()
        {
            try
            {
                if (string.IsNullOrEmpty(Thread.CurrentThread.Name))
                    Thread.CurrentThread.Name = "JobWorker";
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}