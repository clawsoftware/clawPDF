using System;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Threading;
using clawSoft.clawPDF.Utilities.Communication;
using NLog;

namespace clawSoft.clawPDF.Startup
{
    internal abstract class MaybePipedStart : IAppStart
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public bool StartManagePrintJobs { get; protected internal set; }

        public bool Run()
        {
            var retry = 0;
            var success = false;

            // Make n Attempts: Look if a pipe server exists, if so, send a message. If that fails, retry (and maybe do the job yourself)
            while (!success && retry++ <= 5)
            {
                _logger.Debug("Starting attempt {0}: ", retry);
                if (PipeServer.SessionServerInstanceRunning(ThreadManager.PipeName))
                {
                    success = TrySendPipeMessage();
                    _logger.Debug("TrySendPipeMessage: " + success);
                }
                else
                {
                    success = TryStartApplication();
                    _logger.Debug("TryStartApplication: " + success);
                }
            }

            return success;
        }

        internal abstract string ComposePipeMessage();

        internal abstract bool StartApplication();

        private bool TrySendPipeMessage()
        {
            _logger.Debug("Found another running instance of clawPDF, so we send our data there");

            var message = ComposePipeMessage();

            var pipeClient = PipeClient.CreateSessionPipeClient(ThreadManager.PipeName);

            if (pipeClient.SendMessage(message))
            {
                _logger.Debug("Pipe message successfully sent");
                return true;
            }

            _logger.Warn("There was an error while communicating through the pipe");
            return false;
        }

        private bool TryStartApplication()
        {
            try
            {
                _logger.Debug("Starting pipe server");
                var success = ThreadManager.Instance.StartPipeServerThread();

                if (!success)
                    return false;

                _logger.Debug("Reloading settings");
                // Settings may have changed as this may have not been the only instance till now
                SettingsHelper.ReloadSettings();

                ThreadManager.Instance.PipeServer.OnNewMessage += PipeServer_OnNewMessage;

                JobRunner.Instance.RegisterJobInfoQueueHandler();

                if (StartManagePrintJobs) JobRunner.Instance.ManagePrintJobs();

                _logger.Debug("Finding spooled jobs");
                JobInfoQueue.Instance.FindSpooledJobs();

                if (StartApplication())
                {
                    _logger.Debug("Starting Cleanup thread");
                    ThreadManager.Instance.StartCleanUpThread();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "There was an error while starting the application");
                return false;
            }
        }

        private void PipeServer_OnNewMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.StartsWith("ReloadSettings|", StringComparison.OrdinalIgnoreCase))
            {
                _logger.Info("Pipe Command: Reloading settings");
                SettingsHelper.ReloadSettings();
            }
        }
    }
}