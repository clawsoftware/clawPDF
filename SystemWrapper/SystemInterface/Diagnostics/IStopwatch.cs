using System;
using System.Diagnostics;

namespace SystemInterface.Diagnostics
{
    /// <summary>
    /// Wrapper for <see cref="System.Version"/> class.
    /// </summary>
    public interface IStopwatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Diagnostics.Stopwatch"/> class.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Diagnostics.Stopwatch"/> class.
        /// </summary>
        void Initialize(Stopwatch stopwatch);

        /// <summary>
        /// Gets <see cref="T:System.Diagnostics.Stopwatch"/> object.
        /// </summary>
        Stopwatch StopwatchInstance { get; }

        /// <summary>
        /// Gets the total elapsed time measured by the current instance.
        /// </summary>
        TimeSpan Elapsed { get; }

        /// <summary>
        /// Gets the total elapsed time measured by the current instance, in milliseconds.
        /// </summary>
        long ElapsedMilliseconds { get; }

        /// <summary>
        /// Gets the total elapsed time measured by the current instance, in timer ticks.
        /// </summary>
        long ElapsedTicks { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Diagnostics.Stopwatch"/> timer is running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Stops time interval measurement and resets the elapsed time to zero.
        /// </summary>
        void Reset();

        /// <summary>
        /// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
        /// </summary>
        void Restart();

        /// <summary>
        /// Starts, or resumes, measuring elapsed time for an interval.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops measuring elapsed time for an interval.
        /// </summary>
        void Stop();
    }
}
