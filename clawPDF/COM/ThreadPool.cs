using System;
using System.Collections.Generic;
using System.Threading;
using clawSoft.clawPDF.Utilities.Threading;
using NLog;

namespace clawSoft.clawPDF.COM
{
    public class ThreadPool
    {
        private static readonly Queue<ISynchronizedThread> ThreadQueue = new Queue<ISynchronizedThread>();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static ISynchronizedThread _conversionThread;
        private static bool _isThreadRunning;
        private static ThreadPool _instance;

        /// <summary>
        ///     Registers to the OnThreadAdded event
        /// </summary>
        private ThreadPool()
        {
            OnThreadAdded += (sender, args) => StartThread();
        }

        public static int Count => ThreadQueue.Count;

        public static ThreadPool Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ThreadPool();

                return _instance;
            }
        }

        private static event EventHandler<EventArgs> OnThreadAdded;

        /// <summary>
        ///     Adds a new thread to the thread pool
        /// </summary>
        /// <param name="aThread">The thread to be added</param>
        public void AddThread(ISynchronizedThread aThread)
        {
            if (aThread.ThreadState == ThreadState.Running) aThread.Abort();

            Logger.Trace("COM: Pushing new thread into the thread queue.");
            ThreadQueue.Enqueue(aThread);

            if (Count != 1 || _isThreadRunning)
                return;

            Logger.Trace("COM: Thread was added therefore the corresponding event is fired.");
            OnThreadAdded?.Invoke(new object(), new EventArgs());
        }

        /// <summary>
        ///     Starts the first thread from the thread queue
        /// </summary>
        private void StartThread()
        {
            Logger.Trace("COM: Starting thread...");
            _isThreadRunning = true;
            _conversionThread = ThreadQueue.Dequeue();

            _conversionThread.OnThreadFinished += ConversionThreadFinished;
            _conversionThread.Start();
        }

        /// <summary>
        ///     Restarts the thread if other threads are available in the queue
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Detailed information about the event</param>
        private void ConversionThreadFinished(object sender, ThreadFinishedEventArgs e)
        {
            _conversionThread = null;
            _isThreadRunning = false;
            if (Count > 0)
                StartThread();
        }

        /// <summary>
        ///     Waits for each thread to finish
        /// </summary>
        /// <returns>True, if each thread ended within timeOut seconds, false otherwise</returns>
        public void Join()
        {
            while (_conversionThread != null)
            {
                Logger.Trace("Joining the running thread from the thread pool.");
                _conversionThread.Join();
            }
        }

        /// <summary>
        ///     Waits for each thread to finish
        /// </summary>
        /// <param name="timeOut">Amount of time to wait for a each thread</param>
        /// <returns>True, if each thread ended within timeOut seconds, false otherwise</returns>
        public bool Join(int timeOut)
        {
            var isJoinSuccessful = true;

            while (_conversionThread != null)
            {
                Logger.Trace("Joining the running thread from the thread pool.");
                isJoinSuccessful &= _conversionThread.Join(timeOut);
            }

            return isJoinSuccessful;
        }

        /// <summary>
        ///     Waits for each thread to finish
        /// </summary>
        /// <param name="timeOut">Amount of time to wait for a each thread</param>
        /// <returns>True, if each thread ended within timeOut, false otherwise</returns>
        public bool Join(TimeSpan timeOut)
        {
            var isJoinSuccessful = true;

            while (_conversionThread != null)
            {
                Logger.Trace("Joining the running thread from the thread pool.");
                isJoinSuccessful &= _conversionThread.Join(timeOut);
            }

            return isJoinSuccessful;
        }
    }
}