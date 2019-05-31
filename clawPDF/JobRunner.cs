using System;
using System.Threading;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Exceptions;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Threading;
using clawSoft.clawPDF.Utilities.Threading;
using clawSoft.clawPDF.Views;
using clawSoft.clawPDF.Workflow;
using NLog;

namespace clawSoft.clawPDF
{
    /// <summary>
    ///     The JobRunner class manages the thread that processes the print jobs. It listens for new jobs in the
    ///     <see cref="clawSoft.clawPDF.JobInfoQueue" /> and creates the processing thread if required.
    /// </summary>
    internal class JobRunner
    {
        private static JobRunner _instance;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private bool _managePrintJobs;
        private ISynchronizedThread _processingThread;

        private JobRunner()
        {
            ManagePrintJobsAction = ShowManagePrintJobsWindow;
        }

        private Action ManagePrintJobsAction { get; }

        public static JobRunner Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new JobRunner();

                return _instance;
            }
        }

        public void ManagePrintJobs()
        {
            _managePrintJobs = true;
            StartProcessing();
        }

        public void RegisterJobInfoQueueHandler()
        {
            JobInfoQueue.Instance.OnNewJobInfo += JobInfoQueue_OnNewJobInfo;
        }

        private void JobInfoQueue_OnNewJobInfo(object sender, NewJobInfoEventArgs e)
        {
            if (_processingThread == null) StartProcessing();
        }

        private void StartProcessing()
        {
            if (JobInfoQueue.Instance.IsEmpty && !_managePrintJobs) return;

            _processingThread = new SynchronizedThread(ProcessJobs) { Name = "ProcessingThread" };
            _processingThread.SetApartmentState(ApartmentState.STA);

            ThreadManager.Instance.StartSynchronizedThread(_processingThread);
        }

        [STAThread]
        private void ProcessJobs()
        {
            try
            {
                while (!JobInfoQueue.Instance.IsEmpty || _managePrintJobs)
                    try
                    {
                        if (_managePrintJobs) throw new ManagePrintJobsException();

                        var jobInfo = JobInfoQueue.Instance.NextJob;

                        if (jobInfo.SourceFiles.Count == 0)
                        {
                            _logger.Info("JobInfo has no source files and will be skipped");
                            JobInfoQueue.Instance.Remove(jobInfo);
                            continue;
                        }

                        _logger.Debug("New PrintJob {0} from Printer {1}", jobInfo.InfFile,
                            jobInfo.SourceFiles[0].PrinterName);

                        var repeatJob = true;

                        try
                        {
                            ProcessJob(jobInfo);

                            // If Job was processed without ManagePrintJobsException, it can be removed
                            repeatJob = false;
                        }
                        finally
                        {
                            if (!repeatJob)
                            {
                                // ensure that the current job is removed even if an exception is thrown
                                _logger.Trace("Removing job from Queue");
                                JobInfoQueue.Instance.Remove(jobInfo);
                            }
                        }
                    }
                    catch (ManagePrintJobsException)
                    {
                        _managePrintJobs = false;
                        _logger.Trace("Managing print jobs");
                        if (ManagePrintJobsAction != null)
                            ManagePrintJobsAction();
                    }
            }
            catch (Exception ex)
            {
                _logger.Error("There was an error while processing the print jobs: " + ex);
            }
            finally
            {
                if (!JobInfoQueue.Instance.IsEmpty)
                    _logger.Warn("Processing finishes while there are print jobs left.");

                _processingThread = null;
            }
        }

        /// <summary>
        ///     Process a single job
        /// </summary>
        /// <param name="jobInfo">The jobinfo to process</param>
        /// <returns>True if the job was processed. If the user decided to manage the print jobs instead, this returns false</returns>
        private void ProcessJob(IJobInfo jobInfo)
        {
            _logger.Trace("Creating job workflow");
            var cw = WorkflowFactory.CreateWorkflow(jobInfo, SettingsHelper.Settings);

            _logger.Trace("Running workflow");
            cw.RunWorkflow();

            if (cw.WorkflowStep != WorkflowStep.Finished)
            {
                if (cw.WorkflowStep == WorkflowStep.AbortedByUser)
                    _logger.Info("The job '{0}' was aborted by the user.",
                        cw.Job.JobInfo.Metadata.Title, cw.WorkflowStep);
                else
                    _logger.Error("The job '{0}' terminated at step {1} and did not end successfully.",
                        cw.Job.JobInfo.Metadata.Title, cw.WorkflowStep);
            }
        }

        private void ShowManagePrintJobsWindow()
        {
            var window = new ManagePrintJobsWindow();
            window.ShowDialog();
        }
    }
}