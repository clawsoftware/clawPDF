using System;
using System.Threading;
using SystemInterface.Threading;

namespace SystemWrapper.Threading
{
    /// <summary>
    /// Wrapper for <see cref="System.Threading.Thread"/> class.
    /// </summary>
    public class ThreadWrap : IThread
    {
        /// <inheritdoc />
        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        /// <inheritdoc />
        public void Sleep(TimeSpan timeout)
        {
            Thread.Sleep(timeout);
        }
    }
}