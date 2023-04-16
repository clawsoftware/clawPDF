using System;
using System.Collections.Generic;
using System.Threading;

namespace clawSoft.clawPDF.Utilities
{
    /// <summary>
    /// Implements reusable retry logic to retry actions that might fail with an exception after a given timespan.
    ///
    /// Code largely based on http://stackoverflow.com/a/1563234
    /// </summary>
    public static class Retry
    {
        /// <summary>
        /// Execute an action that is retried <see cref="retryCount"/> times if an Exception was thrown.
        /// </summary>
        /// <param name="action">The action to execute. This will be repeated if a matching exception occurs.</param>
        /// <param name="retryInterval">The timespan to wait before doing a retry</param>
        /// <param name="retryCount">The number of maximum attempts to execute.</param>
        /// <param name="retryCondition">A Predicate that optionally needs to met to do a retry.</param>
        public static void Do(
            Action action,
            TimeSpan retryInterval,
            int retryCount = 3,
            Predicate<Exception> retryCondition = null)
        {
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount, retryCondition);
        }

        /// <summary>
        /// Execute a function that is retried <see cref="retryCount"/> times if an Exception was thrown.
        /// </summary>
        /// <param name="action">The function to execute. This will be repeated if a matching exception occurs.</param>
        /// <param name="retryInterval">The timespan to wait before doing a retry</param>
        /// <param name="retryCount">The number of maximum attempts to execute.</param>
        /// <param name="retryCondition">A Predicate that optionally needs to met to do a retry.</param>
        public static T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            int retryCount = 3,
            Predicate<Exception> retryCondition = null)
        {
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (retry > 0)
                        Thread.Sleep(retryInterval);
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);

                    if (retryCondition != null && !retryCondition(ex))
                        throw new AggregateException(exceptions);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}
