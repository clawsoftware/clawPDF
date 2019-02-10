using clawSoft.clawPDF.Core.Settings;

namespace clawSoft.clawPDF.Core.Jobs
{
    public static class JobFactory
    {
        public static IJob CreateJob(IJobInfo jobInfo, ConversionProfile conversionProfile,
            ITempFolderProvider tempFolder, JobTranslations jobTranslations)
        {
            if (jobInfo.JobType == JobType.XpsJob)
                return new XpsJob(jobInfo, conversionProfile, jobTranslations);

            return new GhostscriptJob(jobInfo, conversionProfile, tempFolder, jobTranslations);
        }
    }
}