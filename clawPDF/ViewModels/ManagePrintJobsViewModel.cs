using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Shared.ViewModels;

namespace clawSoft.clawPDF.ViewModels
{
    internal class ManagePrintJobsViewModel : ViewModelBase
    {
        private readonly Dispatcher _currentThreadDispatcher;
        private readonly IJobInfoQueue _jobInfoQueue;
        private readonly ObservableCollection<IJobInfo> _jobInfos;

        public ManagePrintJobsViewModel()
            : this(JobInfoQueue.Instance)
        {
        }

        public ManagePrintJobsViewModel(IJobInfoQueue jobInfoQueue)
        {
            _currentThreadDispatcher = Dispatcher.CurrentDispatcher;
            _jobInfoQueue = jobInfoQueue;
            _jobInfoQueue.OnNewJobInfo += NewJobInfo;

            DeleteJobCommand = new DelegateCommand(ExecuteDeleteJob, CanExecuteDeleteJob);
            MergeJobsCommand = new DelegateCommand(ExecuteMergeJobs, CanExecuteMergeJobs);
            MergeAllJobsCommand = new DelegateCommand(ExecuteMergeAllJobs, CanExecuteMergeAllJobs);
            MoveUpCommand = new DelegateCommand(ExecuteMoveUp, CanExecuteMoveUp);
            MoveDownCommand = new DelegateCommand(ExecuteMoveDown, CanExecuteMoveDown);

            _jobInfos = new ObservableCollection<IJobInfo>();
            JobInfos = new CollectionView(_jobInfos);
            JobInfos.CurrentChanged += CurrentJobInfoChanged;

            foreach (var jobInfo in _jobInfoQueue.JobInfos) AddJobInfo(jobInfo);
        }

        public CollectionView JobInfos { get; }
        public DelegateCommand DeleteJobCommand { get; }
        public DelegateCommand MergeJobsCommand { get; }
        public DelegateCommand MergeAllJobsCommand { get; }
        public DelegateCommand MoveUpCommand { get; }
        public DelegateCommand MoveDownCommand { get; }

        private void CurrentJobInfoChanged(object sender, EventArgs e)
        {
            RaiseRefreshView();
        }

        private void NewJobInfo(object sender, NewJobInfoEventArgs e)
        {
            Action<IJobInfo> addMethod = AddJobInfo;

            if (!_currentThreadDispatcher.Thread.IsAlive)
            {
                _jobInfoQueue.OnNewJobInfo -= NewJobInfo;
                return;
            }

            _currentThreadDispatcher.Invoke(addMethod, e.JobInfo);
        }

        private void AddJobInfo(IJobInfo jobInfo)
        {
            _jobInfos.Add(jobInfo);

            if (JobInfos.CurrentItem == null)
                JobInfos.MoveCurrentToFirst();

            RaiseRefreshView();
        }

        public void RaiseRefreshView()
        {
            RaiseRefreshView(false);
        }

        private void RaiseRefreshView(bool refreshCollectionView)
        {
            DeleteJobCommand.RaiseCanExecuteChanged();
            MergeJobsCommand.RaiseCanExecuteChanged();
            MergeAllJobsCommand.RaiseCanExecuteChanged();
            MoveUpCommand.RaiseCanExecuteChanged();
            MoveDownCommand.RaiseCanExecuteChanged();

            if (refreshCollectionView)
                JobInfos.Refresh();
        }

        private void ExecuteDeleteJob(object o)
        {
            var jobs = o as IEnumerable<object>;
            if (jobs == null)
                return;

            foreach (var job in jobs.ToArray())
            {
                var jobInfo = (IJobInfo)job;
                var position = JobInfos.CurrentPosition;
                _jobInfos.Remove(jobInfo);
                _jobInfoQueue.Remove(jobInfo, true);

                if (_jobInfos.Count > 0)
                    JobInfos.MoveCurrentToPosition(Math.Max(0, position - 1));
            }

            RaiseRefreshView();
        }

        private bool CanExecuteDeleteJob(object o)
        {
            var jobs = o as IEnumerable<object>;
            return jobs != null && jobs.Any();
        }

        private void ExecuteMergeJobs(object o)
        {
            if (!CanExecuteMergeJobs(o))
                throw new InvalidOperationException("CanExecute is false");

            var jobObjects = o as IEnumerable<object>;
            if (jobObjects == null)
                return;

            var jobs = jobObjects.ToList();
            var first = (IJobInfo)jobs.First();

            foreach (var jobObject in jobs.Skip(1))
            {
                var job = (IJobInfo)jobObject;
                if (job.JobType != first.JobType)
                    continue;
                first.Merge(job);
                _jobInfos.Remove(job);
                _jobInfoQueue.Remove(job, false);
            }

            first.SaveInf();

            RaiseRefreshView(true);
        }

        private bool CanExecuteMergeJobs(object o)
        {
            var jobs = o as IEnumerable<object>;
            return jobs != null && jobs.Count() > 1;
        }

        private void ExecuteMergeAllJobs(object o)
        {
            ExecuteMergeJobs(_jobInfos);
        }

        private bool CanExecuteMergeAllJobs(object o)
        {
            return _jobInfos.Count > 1;
        }

        private void ExecuteMoveUp(object o)
        {
            if (!CanExecuteMoveUp(o))
                throw new InvalidOperationException();

            var jobs = o as IEnumerable<object>;

            if (jobs == null)
                return;

            var job = (IJobInfo)jobs.First();

            MoveJob(job, -1);
            RaiseRefreshView();
        }

        private bool CanExecuteMoveUp(object o)
        {
            var jobs = o as IEnumerable<object>;
            if (jobs == null)
                return false;

            var jobList = jobs.ToList();
            if (jobList.Count != 1)
                return false;

            return _jobInfos.IndexOf((IJobInfo)jobList.First()) > 0;
        }

        private void ExecuteMoveDown(object o)
        {
            if (!CanExecuteMoveDown(o))
                throw new InvalidOperationException();

            var jobs = o as IEnumerable<object>;

            if (jobs == null)
                return;

            var job = (IJobInfo)jobs.First();

            MoveJob(job, +1);
            RaiseRefreshView();
        }

        private bool CanExecuteMoveDown(object o)
        {
            var jobs = o as IEnumerable<object>;
            if (jobs == null)
                return false;

            var jobList = jobs.ToList();
            if (jobList.Count != 1)
                return false;

            return _jobInfos.IndexOf((IJobInfo)jobList.First()) < _jobInfos.Count - 1;
        }

        private void MoveJob(IJobInfo jobInfo, int positionDifference)
        {
            var oldIndex = _jobInfos.IndexOf(jobInfo);
            _jobInfos.Move(oldIndex, oldIndex + positionDifference);

            JobInfos.MoveCurrentToPosition(oldIndex + positionDifference);
        }
    }
}