using System;
using System.Threading;

namespace clawSoft.clawPDF.Utilities.Threading
{
    /// <summary>
    ///     SynchronizedThread wraps a Thread object and adds some functionality to manage multiple threads. It allows to
    ///     subscribe to an event that is launched when the Thread work has finished.
    /// </summary>
    public sealed class SynchronizedThread : ISynchronizedThread
    {
        private readonly Thread _thread;
        private readonly ThreadStart _threadFunction;

        /// <summary>
        ///     Creates a new SynchronizedThread with the given function
        /// </summary>
        /// <param name="threadFunction">Thread delegate</param>
        public SynchronizedThread(ThreadStart threadFunction)
        {
            _thread = new Thread(RunThread);
            _threadFunction = threadFunction;
        }

        /// <summary>
        ///     OnThreadFinished is fired when the ThreadExecution has finished
        /// </summary>
        public event EventHandler<ThreadFinishedEventArgs> OnThreadFinished;

        /// <summary>
        ///     Gets or sets the name of the Thread
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the current state of the thread
        /// </summary>
        public ThreadState ThreadState => _thread.ThreadState;

        /// <summary>
        ///     Determines if the thread is alive
        /// </summary>
        public bool IsAlive => _thread.IsAlive;

        /// <summary>
        ///     Sets the threading apartment state of the Thread. This must be called before starting the thread
        /// </summary>
        /// <param name="state">The new state</param>
        public void SetApartmentState(ApartmentState state)
        {
            _thread.SetApartmentState(state);
        }

        /// <summary>
        ///     Waits for the Thread to finish
        /// </summary>
        public void Join()
        {
            _thread.Join();
        }

        /// <summary>
        ///     Waits for the Thread to finish for the given amount of milliseconds
        /// </summary>
        /// <param name="millisecondsTimeout">Number of milliseconds to wait</param>
        public bool Join(int millisecondsTimeout)
        {
            return _thread.Join(millisecondsTimeout);
        }

        /// <summary>
        ///     Waits for the Thread to finish for the given TimeSpan
        /// </summary>
        /// <param name="timeout">TimeSpan to wait</param>
        public bool Join(TimeSpan timeout)
        {
            return _thread.Join(timeout);
        }

        /// <summary>
        ///     Starts thread execution
        /// </summary>
        public void Start()
        {
            if (!string.IsNullOrEmpty(Name))
                _thread.Name = Name;

            _thread.Start();
        }

        /// <summary>
        ///     Aborts the thread
        /// </summary>
        public void Abort()
        {
            _thread.Abort();
        }

        private void RunThread()
        {
            try
            {
                _threadFunction();
            }
            finally
            {
                if (OnThreadFinished != null) OnThreadFinished(this, new ThreadFinishedEventArgs(this));
            }
        }
    }

    /// <summary>
    ///     ThreadFinishedEventArgs holds arguments for the OnThreadFinished event
    /// </summary>
    public class ThreadFinishedEventArgs : EventArgs
    {
        /// <summary>
        ///     Create new EventArgs
        /// </summary>
        /// <param name="thread">SynchronizedThread that is concerned</param>
        public ThreadFinishedEventArgs(ISynchronizedThread thread)
        {
            SynchronizedThread = thread;
        }

        /// <summary>
        ///     Gets the SynchronizedThread object
        /// </summary>
        public ISynchronizedThread SynchronizedThread { get; }
    }
}