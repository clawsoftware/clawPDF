using System;
using System.Threading;

namespace clawSoft.clawPDF.Utilities.Threading
{
    public interface ISynchronizedThread
    {
        /// <summary>
        ///     Gets or sets the name of the Thread
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets the current state of the thread
        /// </summary>
        ThreadState ThreadState { get; }

        /// <summary>
        ///     Determines if the thread is alive
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        ///     OnThreadFinished is fired when the ThreadExecution has finished
        /// </summary>
        event EventHandler<ThreadFinishedEventArgs> OnThreadFinished;

        /// <summary>
        ///     Sets the threading aparment state of the Thread. This must be called before starting the thread
        /// </summary>
        /// <param name="state">The new state</param>
        void SetApartmentState(ApartmentState state);

        /// <summary>
        ///     Waits for the Thread to finish
        /// </summary>
        void Join();

        /// <summary>
        ///     Waits for the Thread to finish for the given amount of millisecods
        /// </summary>
        /// <param name="millisecondsTimeout">Number of milliseconds to wait</param>
        bool Join(int millisecondsTimeout);

        /// <summary>
        ///     Waits for the Thread to finish for the given TimeSpan
        /// </summary>
        /// <param name="timeout">TimeSpan to wait</param>
        bool Join(TimeSpan timeout);

        /// <summary>
        ///     Starts thread execution
        /// </summary>
        void Start();

        /// <summary>
        ///     Aborts the thread
        /// </summary>
        void Abort();
    }
}