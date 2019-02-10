using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Utilities.Communication;
using clawSoft.clawPDF.Utilities.Threading;
using clawSoft.clawPDF.Views;
using NLog;

namespace clawSoft.clawPDF.Threading
{
    /// <summary>
    ///     The ThreadManager class handles and watches all applications threads. If all registered threads are finished, the
    ///     application will exit.
    /// </summary>
    internal class ThreadManager
    {
        /// <summary>
        ///     The name of the pipe
        /// </summary>
        public const string PipeName = "clawPDF";

        private static readonly object LockObject = new object();

        private static ThreadManager _instance;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly List<ISynchronizedThread> _threads = new List<ISynchronizedThread>();
        private ISynchronizedThread _cleanUpThread;

        private bool _isShuttingDown;

        private ISynchronizedThread _mainFormThread;

        private ThreadManager()
        {
        }

        /// <summary>
        ///     Gets the singleton instance of the ThreadManager
        /// </summary>
        public static ThreadManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ThreadManager();
                return _instance;
            }
        }

        public PipeServer PipeServer { get; private set; }

        public Action UpdateAfterShutdownAction { get; set; }

        /// <summary>
        ///     Wait for all Threads and exit the application afterwards
        /// </summary>
        private void WaitForThreads()
        {
            CleanUpThreads();

            _logger.Debug("Waiting for all synchronized threads to end");

            while (_threads.Count > 0)
            {
                _logger.Debug(_threads.Count + " Threads remaining");

                try
                {
                    _threads[0].Join();
                }
                catch (ArgumentOutOfRangeException)
                {
                } // thread has been removed just after checking while condition

                CleanUpThreads();
            }

            _logger.Debug("All synchronized threads have ended");
        }

        public void WaitForThreadsAndShutdown(Application app)
        {
            WaitForThreads();
            Shutdown(app);
        }

        /// <summary>
        ///     Remove all finished threads
        /// </summary>
        private void CleanUpThreads()
        {
            lock (LockObject)
            {
                try
                {
                    _threads.RemoveAll(t => t.IsAlive == false);
                }
                catch (NullReferenceException ex)
                {
                    _logger.Warn(ex, "There was an exception while cleaning up threads");
                }
            }
        }

        /// <summary>
        ///     Adds and starts a synchronized thread to the thread list. The application will wait for all of these to end before
        ///     it terminates
        /// </summary>
        /// <param name="thread">A thread that needs to be synchronized. This thread will not be automatically started</param>
        public void StartSynchronizedThread(ISynchronizedThread thread)
        {
            lock (LockObject)
            {
                if (_isShuttingDown)
                {
                    _logger.Warn("Tried to start thread while shutdown already started!");
                    return;
                }

                _logger.Debug("Adding thread " + thread.Name);

                _threads.Add(thread);
                thread.OnThreadFinished += thread_OnThreadFinished;

                if (thread.ThreadState == ThreadState.Unstarted)
                    thread.Start();
            }
        }

        /// <summary>
        ///     Remove threads when they have finished
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">EventArgs with information about the thread</param>
        private void thread_OnThreadFinished(object sender, ThreadFinishedEventArgs e)
        {
            try
            {
                lock (LockObject)
                {
                    _threads.Remove(e.SynchronizedThread);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        /// <summary>
        ///     Add the Main Form thread to the thread list and start it
        /// </summary>
        public void StartMainWindowThread()
        {
            if (_mainFormThread != null)
                return;

            _logger.Debug("Starting main form thread");

            var t = new SynchronizedThread(() =>
            {
                try
                {
                    MainWindow.ShowDialogTopMost();
                    _mainFormThread = null;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            });

            _mainFormThread = t;
            _mainFormThread.Name = "MainFormThread";
            _mainFormThread.SetApartmentState(ApartmentState.STA);

            StartSynchronizedThread(t);
        }

        /// <summary>
        ///     Add the clean up thread to the thread list and start it.
        ///     The thread will look for outdated temporary files and will delete them
        /// </summary>
        public void StartCleanUpThread()
        {
            if (_cleanUpThread != null)
                return;

            _logger.Debug("Starting cleanup");

            var t = new SynchronizedThread(() =>
            {
                JobInfoQueue.Instance.CleanTempFiles();
                _cleanUpThread = null;
            });
            _cleanUpThread = t;
            _cleanUpThread.Name = "CleanUpThread";

            StartSynchronizedThread(t);
        }

        /// <summary>
        ///     Starts the Pipe Server Thread
        /// </summary>
        public bool StartPipeServerThread()
        {
            _logger.Debug("Starting pipe server thread");

            PipeServer = PipeServer.CreateSessionPipeServer(PipeName);

            if (!PipeServer.Start())
                return false;

            JobInfoQueue.Instance.AddEventHandler(PipeServer);

            return true;
        }

        private void Shutdown(Application app)
        {
            lock (LockObject)
            {
                _logger.Debug("Shutting down the application");
                _isShuttingDown = true;

                if (PipeServer != null)
                {
                    _logger.Debug("Preparing PipeServer for ShutDown");
                    PipeServer.PrepareShutdown();
                }

                // convert _threads to array to prevent InvalidOperationException when an item is removed
                foreach (var t in _threads.ToArray())
                {
                    if (string.IsNullOrEmpty(t.Name))
                        _logger.Debug("Aborting thread");
                    else
                        _logger.Debug("Aborting thread " + t.Name);

                    t.Abort();
                }

                if (PipeServer != null)
                {
                    _logger.Debug("Saving settings");
                    SettingsHelper.SaveSettings();

                    _logger.Debug("Stopping pipe server");
                    PipeServer.Stop();
                }

                _logger.Debug("Exiting...");

                if (UpdateAfterShutdownAction != null)
                {
                    _logger.Debug("Starting application update...");
                    UpdateAfterShutdownAction();
                }

                app.Shutdown();
            }
        }
    }
}