using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Threading;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;

namespace clawSoft.clawPDF.ViewModels
{
    internal class PrintJobViewModel : ViewModelBase
    {
        private readonly Dispatcher _currentThreadDispatcher;
        private readonly IJobInfoQueue _jobInfoQueue;
        private readonly ConversionProfile _preselectedProfile;
        private readonly TranslationHelper _translationHelper;
        private ApplicationSettings _applicationSettings;
        private IJobInfo _jobInfo;
        private IList<ConversionProfile> _profiles;

        public PrintJobViewModel(ApplicationSettings appSettings, IList<ConversionProfile> profiles,
            IJobInfoQueue jobInfoQueue, ConversionProfile preselectedProfile = null, IJobInfo jobInfo = null,
            TranslationHelper translationHelper = null)
        {
            _currentThreadDispatcher = Dispatcher.CurrentDispatcher;

            Profiles = profiles;
            _preselectedProfile = preselectedProfile;
            //must be set before ApplicationSettings because it is evaluated in the Set method of appsettings.
            ApplicationSettings = appSettings;

            _jobInfoQueue = jobInfoQueue;
            _jobInfoQueue.OnNewJobInfo += NewJobInfo;

            SaveCommand = new DelegateCommand(ExecuteSave);
            EmailCommand = new DelegateCommand(ExecuteMail);
            ManagePrintJobsCommand = new DelegateCommand(ExecuteManagePrintJobs);

            if (jobInfo != null) JobInfo = jobInfo;

            _translationHelper = translationHelper;
        }

        private PrintJobViewModel(IJobInfoQueue jobInfoQueue, ConversionProfile preselectedProfile = null)
            : this(
                SettingsHelper.Settings.ApplicationSettings, SettingsHelper.Settings.ConversionProfiles, jobInfoQueue,
                preselectedProfile)
        {
        }

        public PrintJobViewModel()
            : this(JobInfoQueue.Instance)
        {
            JobInfo = new JobInfo
            {
                Metadata = new Metadata()
            };

            _translationHelper = TranslationHelper.Instance;
        }

        public PrintJobViewModel(IJobInfo jobInfo, ConversionProfile preselectedProfile)
            : this(JobInfoQueue.Instance, preselectedProfile)
        {
            JobInfo = jobInfo;
            _translationHelper = TranslationHelper.Instance;
        }

        public PrintJobAction PrintJobAction { get; private set; }
        public ICollectionView ProfilesView { get; private set; }
        public Metadata Metadata { get; set; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand EmailCommand { get; }
        public DelegateCommand ManagePrintJobsCommand { get; }

        public IJobInfo JobInfo
        {
            get => _jobInfo;
            private set
            {
                _jobInfo = value;
                RaisePropertyChanged("JobInfo");
                if (_jobInfo != null) Metadata = _jobInfo.Metadata.Copy();
            }
        }

        public ConversionProfile SelectedProfile => (ConversionProfile)ProfilesView.CurrentItem;

        public IList<ConversionProfile> Profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
                ProfilesView = CollectionViewSource.GetDefaultView(_profiles);
                ProfilesView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                RaisePropertyChanged("Profiles");
                RaisePropertyChanged("ProfilesView");
            }
        }

        public ApplicationSettings ApplicationSettings
        {
            get => _applicationSettings;
            set
            {
                _applicationSettings = value;
                RaisePropertyChanged("App");
                if (_preselectedProfile != null)
                    SelectProfileByGuid(_preselectedProfile.Guid);
                else
                    SelectProfileByGuid(value.LastUsedProfileGuid);
            }
        }

        public string PendingJobsText
        {
            get
            {
                var additionalJobs = _jobInfoQueue.Count - 1;
                if (additionalJobs == 1)
                    return _translationHelper.TranslatorInstance.GetTranslation("PrintJobWindow", "OneMoreJobWaiting",
                        "One more Job waiting");

                if (additionalJobs > 1)
                    return _translationHelper.TranslatorInstance.GetFormattedTranslation("PrintJobWindow",
                        "MoreJobsWaiting",
                        "There are {0} more Jobs waiting", additionalJobs);

                return _translationHelper.TranslatorInstance.GetTranslation("PrintJobWindow", "NoJobsWaiting",
                    "Print more documents to merge or rearrange them");
            }
        }

        public void SelectProfileByGuid(string guid)
        {
            foreach (var conversionProfile in Profiles)
                if (conversionProfile.Guid == guid)
                    ProfilesView.MoveCurrentTo(conversionProfile);
        }

        private void ExecuteSave(object obj)
        {
            JobInfo.Metadata = Metadata;
            ApplicationSettings.LastUsedProfileGuid = SelectedProfile.Guid;
            PrintJobAction = PrintJobAction.Save;
        }

        private void ExecuteMail(object obj)
        {
            JobInfo.Metadata = Metadata;

            ApplicationSettings.LastUsedProfileGuid = SelectedProfile.Guid;
            PrintJobAction = PrintJobAction.EMail;
        }

        private void ExecuteManagePrintJobs(object obj)
        {
            PrintJobAction = PrintJobAction.ManagePrintJobs;
        }

        private void NewJobInfo(object sender, NewJobInfoEventArgs e)
        {
            Action<IJobInfo> addMethod = RaisePropertyChangedEvents;

            if (!_currentThreadDispatcher.Thread.IsAlive)
            {
                _jobInfoQueue.OnNewJobInfo -= NewJobInfo;
                return;
            }

            _currentThreadDispatcher.Invoke(addMethod, e.JobInfo);
        }

        private void RaisePropertyChangedEvents(IJobInfo jobInfo)
        {
            RaisePropertyChanged("JobCount");
            RaisePropertyChanged("PendingJobsText");
            ManagePrintJobsCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged("CanManagePrintJobs");
        }
    }

    internal enum PrintJobAction
    {
        Cancel,
        Save,
        EMail,
        ManagePrintJobs
    }
}