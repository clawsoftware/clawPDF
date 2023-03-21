using System;
using System.Diagnostics;
using SystemInterface.Diagnostics;

namespace SystemWrapper.Diagnostics
{
    public class StopwatchWrap : IStopwatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Diagnostics.Stopwatch"/> class.
        /// </summary>
        public StopwatchWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Diagnostics.Stopwatch"/> class.
        /// </summary>
        public StopwatchWrap(Stopwatch stopwatch)
        {
            Initialize(stopwatch);
        }

        /// <inheritdoc />
        public void Initialize()
        {
            StopwatchInstance = new Stopwatch();
        }

        /// <inheritdoc />
        public void Initialize(Stopwatch stopwatch)
        {
            StopwatchInstance = stopwatch;
        }

        /// <inheritdoc />
        public Stopwatch StopwatchInstance { get; private set; }

        /// <inheritdoc />
        public TimeSpan Elapsed => StopwatchInstance.Elapsed;

        /// <inheritdoc />
        public long ElapsedMilliseconds => StopwatchInstance.ElapsedMilliseconds;

        /// <inheritdoc />
        public long ElapsedTicks => StopwatchInstance.ElapsedTicks;

        /// <inheritdoc />
        public bool IsRunning => StopwatchInstance.IsRunning;

        /// <inheritdoc />
        public void Reset()
        {
            StopwatchInstance.Reset();
        }

        /// <inheritdoc />
        public void Restart()
        {
            StopwatchInstance.Restart();
        }

        /// <inheritdoc />
        public void Start()
        {
            StopwatchInstance.Start();
        }

        /// <inheritdoc />
        public void Stop()
        {
            StopwatchInstance.Stop();
        }
    }
}
