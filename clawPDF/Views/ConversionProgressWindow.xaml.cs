using System;
using System.Timers;
using System.Windows;
using clawSoft.clawPDF.Core.Jobs;

namespace clawSoft.clawPDF.Views
{
    internal partial class ConversionProgressWindow : Window
    {
        private readonly Timer _timer = new Timer(500);

        public ConversionProgressWindow()
        {
            InitializeComponent();
        }

        public void ApplyJob(IJob job)
        {
            job.OnJobCompleted += job_OnJobCompleted;

            // Backup timer to close window if the event should not be fired for some reason
            _timer.Elapsed += (sender, args) =>
            {
                if (job.Completed) job_OnJobCompleted(this, null);
            };
            _timer.Start();
        }

        private void job_OnJobCompleted(object sender, JobCompletedEventArgs e)
        {
            Action closeAction = Close;
            Dispatcher.BeginInvoke(closeAction);

            if (_timer != null)
                _timer.Enabled = false;
        }
    }
}