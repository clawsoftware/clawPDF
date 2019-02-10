using System;
using clawSoft.clawPDF.Core.Jobs;

namespace clawSoft.clawPDF
{
    /// <summary>
    ///     EventArgs class that contains the new JobInfo
    /// </summary>
    public class NewJobInfoEventArgs : EventArgs
    {
        public NewJobInfoEventArgs(IJobInfo jobInfo)
        {
            JobInfo = jobInfo;
        }

        public IJobInfo JobInfo { get; }
    }
}