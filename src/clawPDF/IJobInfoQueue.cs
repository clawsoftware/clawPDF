using System;
using System.Collections.Generic;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Utilities.Communication;

namespace clawSoft.clawPDF
{
    public interface IJobInfoQueue
    {
        /// <summary>
        ///     The List of jobs currently waiting
        /// </summary>
        IList<IJobInfo> JobInfos { get; }

        /// <summary>
        ///     Get the number ob items in the JobInfo Queue
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Get the next pending job. If this is null, the queue is empty
        /// </summary>
        IJobInfo NextJob { get; }

        /// <summary>
        ///     Determines if the Queue is emtpy
        /// </summary>
        /// <returns>true, if the Queue is empty</returns>
        bool IsEmpty { get; }

        /// <summary>
        ///     Gets the folder in which the print jobs will be stored
        /// </summary>
        string SpoolFolder { get; }

        event EventHandler<NewJobInfoEventArgs> OnNewJobInfo;

        void FindSpooledJobs();

        /// <summary>
        ///     Reads a JobInfo from the given file and adds it to the end of the JobInfo Queue
        /// </summary>
        /// <param name="jobInfo">The JobInfo file to read and add</param>
        void Add(string jobInfo);

        /// <summary>
        ///     Appends an item to the end of the JobInfo Queue
        /// </summary>
        /// <param name="jobInfo">The JobInfo to add</param>
        void Add(IJobInfo jobInfo);

        /// <summary>
        ///     Removes a JobInfo from the Queue
        /// </summary>
        /// <param name="jobInfo">The JobInfo to remove</param>
        /// <returns>true, if successful</returns>
        bool Remove(IJobInfo jobInfo);

        /// <summary>
        ///     Removes a JobInfo from the Queue
        /// </summary>
        /// <param name="jobInfo">The JobInfo to remove</param>
        /// <param name="deleteFiles">If true, the inf and source files will be deleted</param>
        /// <returns>true, if successful</returns>
        bool Remove(IJobInfo jobInfo, bool deleteFiles);

        /// <summary>
        ///     Add event handler to the pipe server. This allows the Queue to listen for new messages and catch new jobs to place
        ///     them in the queue
        /// </summary>
        /// <param name="pipeServer">The PipeServer object to use</param>
        void AddEventHandler(PipeServer pipeServer);

        void AddTestPage();

        /// <summary>
        ///     Removes all files and subdirectories (including all files contained in them) from the given temporary folders, that
        ///     are older than one day.
        ///     Use with caution!
        /// </summary>
        void CleanTempFiles();
    }
}