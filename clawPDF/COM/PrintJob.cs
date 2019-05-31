using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Utilities.Threading;
using clawSoft.clawPDF.Workflow;
using NLog;

namespace clawSoft.clawPDF.COM
{
    [ComVisible(true)]
    public delegate void JobFinishedDelegate();

    [ComVisible(true)]
    [Guid("489689FE-E8AF-41FF-8D5A-8212DF2F013C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IJobFinishedEvent
    {
        void JobFinished();
    }

    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("01E51AAE-D371-469A-A556-FC491A81778D")]
    public interface IPrintJob
    {
        bool IsFinished { get; }
        bool IsSuccessful { get; }
        string ConversionState { get; }

        void SetProfileByGuid(string profileGuid);

        OutputFiles GetOutputFiles { get; }

        void ConvertTo(string fullFileName);

        void ConvertToAsync(string fullFileName);

        void SetProfileSetting(string name, string value);

        PrintJobInfo PrintJobInfo { get; }

        string GetProfileSetting(string propertyName);
    }

    [ComVisible(true)]
    [ComSourceInterfaces(typeof(IJobFinishedEvent))]
    [Guid("9616B8B3-FE6E-4122-AC93-E46DBD571F87")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PrintJob : IPrintJob
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IJobInfoQueue _comJobInfoQueue;
        private readonly IJob _job;
        private readonly ThreadPool _threadPool = ThreadPool.Instance;

        private readonly List<string> _unaccessibleItems = new List<string>
            {"autosave", "properties", "skipprintdialog", "savedialog"};

        private Metadata _metadata;
        private ConversionWorkflow _workflow;

        /// <summary>
        ///     Creates a new ComJob from a job out of the
        /// </summary>
        /// <param name="job">The job to be wrapped in a ComJob</param>
        /// <param name="comJobInfoQueue">The JobInfoQueue of the job</param>
        internal PrintJob(IJob job, IJobInfoQueue comJobInfoQueue)
        {
            Logger.Trace("COM: Setting up the ComJob instance.");
            _job = job;
            JobInfo = job.JobInfo;
            _comJobInfoQueue = comJobInfoQueue;
            _metadata = JobInfo.Metadata;

            SetProfileByGuid(ProfileGuids.DEFAULT_PROFILE_GUID);
        }

        public IJobInfo JobInfo { get; private set; }

        /// <summary>
        ///     Informs about process state
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        ///     Returns true, if the job finished successfully
        /// </summary>
        public bool IsSuccessful { get; private set; }

        /// <summary>
        ///     Returns a string which describes the current conversion step. This is "Pending" if it has not been started and
        ///     "Finished", when it is completed successfully. Other values occur in between.
        /// </summary>
        public string ConversionState => _workflow == null ? "Pending" : _workflow.WorkflowStep.ToString();

        /// <summary>
        ///     Sets the profile by guid to use for COM conversion
        /// </summary>
        public void SetProfileByGuid(string profileGuid)
        {
            Logger.Trace("COM: Setting the job profile: {0}", profileGuid);
            _job.Profile = SettingsHelper.Settings.GetProfileByGuid(profileGuid).Copy();

            if (_job.Profile == null)
                throw new COMException("Invalid guid.");
        }

        /// <summary>
        ///     Returns an object of type Outputfiles if the job contains any output file
        /// </summary>
        public OutputFiles GetOutputFiles
        {
            get
            {
                if (_job.OutputFiles.Count != 0)
                    return new OutputFiles(_job.OutputFiles);
                return null;
            }
        }

        /// <summary>
        ///     Converts the job to the specified location
        /// </summary>
        /// <param name="fullFileName">Specifies the location</param>
        public void ConvertTo(string fullFileName)
        {
            LocationSetUp(fullFileName);

            Logger.Trace("COM: Starting the conversion process.");
            DoConversion();

            if (OnJobFinished != null)
                OnJobFinished(this, new EventArgs());

            if (JobFinished != null)
                JobFinished();

            Logger.Trace("COM: Removing jobinfo from the queue.");
            _comJobInfoQueue.Remove(JobInfo);
        }

        /// <summary>
        ///     Converts the job to the specified location asynchronously
        /// </summary>
        /// <param name="fullFileName">Specifies the location and the file's name</param>
        public void ConvertToAsync(string fullFileName)
        {
            LocationSetUp(fullFileName);
            CreateThread();
        }

        /// <summary>
        ///     Set a conversion profile property using two strings: One for the name (i.e. PdfSettings.Security.Enable) and one
        ///     for the value
        /// </summary>
        /// <param name="name">Name of the setting. This can include subproperties (i.e. PdfSettings.Security.Enable)</param>
        /// <param name="value">A string that can be parsed to the type</param>
        public void SetProfileSetting(string name, string value)
        {
            if (HasAccess(name) && ValueReflector.HasProperty(_job.Profile, name))
                ValueReflector.SetPropertyValue(_job.Profile, name, value);
            else
                throw new COMException("Invalid property name.");
        }

        /// <summary>
        ///     Returns a PrintJobInfo object
        /// </summary>
        public PrintJobInfo PrintJobInfo => new PrintJobInfo(ref _metadata);

        /// <summary>
        ///     Gets the current value of a specific profile property using its name.
        /// </summary>
        /// <param name="propertyName">Name of the setting. This can include subproperties (i.e. PdfSettings.Security.Enable)</param>
        /// <returns>The value of the property</returns>
        public string GetProfileSetting(string propertyName)
        {
            if (HasAccess(propertyName) && ValueReflector.HasProperty(_job.Profile, propertyName))
                return ValueReflector.GetPropertyValue(_job.Profile, propertyName);

            throw new COMException("Invalid property name.");
        }

        public event EventHandler<EventArgs> OnJobFinished; //Intern event: Not visible to COM clients

        public event JobFinishedDelegate JobFinished; //Extern event: For COM clients only

        /// <summary>
        ///     Sets the profile by name to use for COM conversion
        /// </summary>
        public void SetProfileByName(string profileName)
        {
            Logger.Trace("COM: Setting the job profile: {0}", profileName);
            _job.Profile = SettingsHelper.Settings.GetProfileByName(profileName).Copy();

            if (_job.Profile == null)
                throw new COMException("Invalid name.");
        }

        /// <summary>
        ///     Sets the location where the job should be converted to and the jobs full name
        /// </summary>
        /// <param name="fileName">Specifies the location and the name of the converted file</param>
        private void LocationSetUp(string fileName)
        {
            if (fileName == null) return;

            var tmpPath = Path.GetDirectoryName(fileName);

            if (tmpPath != null && Directory.Exists(tmpPath))
            {
                Logger.Trace("COM: Setting the full name of the job:" + fileName);
                _job.OutputFilenameTemplate = Path.ChangeExtension(fileName, _job.Profile.OutputFormat.ToString());
            }
            else
            {
                throw new COMException("Invalid path. Please check if the directory exists.");
            }
        }

        /// <summary>
        ///     Starts a new asynchronous thread
        /// </summary>
        private void CreateThread()
        {
            Logger.Trace("COM: Removing jobinfo from queue.");
            _comJobInfoQueue.Remove(JobInfo);

            Logger.Trace("COM: Creating new asynchronous thread.");
            var aThread = new SynchronizedThread(DoConversion);

            aThread.OnThreadFinished += (sender, args) =>
            {
                if (OnJobFinished != null)
                    OnJobFinished(aThread, args);
            };
            aThread.Name = "ConversionThread";

            Logger.Trace("COM: Adding the new thread to the thread pool.");
            _threadPool.AddThread(aThread);
        }

        /// <summary>
        ///     Does the actual conversion process of the job
        /// </summary>
        private void DoConversion()
        {
            DisableIrrelevantProfileSettings();
            _job.JobInfo.Metadata = _metadata;

            try
            {
                if (JobInfo.SourceFiles.Count != 0)
                {
                    Logger.Trace("COM: Creating workflow");

                    _workflow = new ComWorkflow(_job, SettingsHelper.Settings);

                    Logger.Trace("COM: Running workflow");
                    _workflow.RunWorkflow();

                    if (_workflow.WorkflowStep != WorkflowStep.Finished)
                    {
                        IsFinished = true;
                        if (_workflow.WorkflowStep == WorkflowStep.AbortedByUser)
                            Logger.Info("COM: The job '{0}' was aborted by the user.",
                                _workflow.Job.JobInfo.Metadata.Title, _workflow.WorkflowStep);
                        else
                            Logger.Error("COM: The job '{0}' terminated at step {1} and did not end successfully.",
                                _workflow.Job.JobInfo.Metadata.Title, _workflow.WorkflowStep);
                    }
                    else
                    {
                        IsFinished = true;
                        IsSuccessful = true;
                    }
                }
                else
                {
                    IsFinished = true;
                    Logger.Info("COM: JobInfo has no source files and will be skipped");
                }
            }
            catch (Exception e)
            {
                IsFinished = true;
                throw new COMException(e.Message);
            }
        }

        /// <summary>
        ///     Checks if the property name is accessible through COM
        /// </summary>
        /// <param name="propertyName">The property to check for</param>
        /// <returns>True, if the property can be accessed otherwise false</returns>
        private bool HasAccess(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return false;

            return !_unaccessibleItems.Any(item => propertyName.ToLowerInvariant().StartsWith(item));
        }

        /// <summary>
        ///     Disabling unneccessary profile settings for COM
        /// </summary>
        private void DisableIrrelevantProfileSettings()
        {
            _job.Profile.AutoSave.Enabled = false;
            _job.Profile.SaveDialog.SetDirectory = false;
        }
    }
}