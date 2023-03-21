namespace SystemWrapper.Timers
{
    using SystemInterface.Timers;

    /// <summary>
    ///     Factory to create a new <see cref="ITimer"/> instance.
    /// </summary>
    public class TimerFactory : ITimerFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="ITimer"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="ITimer"/>.
        /// </returns>
        public ITimer Create()
        {
            return new TimerWrap();
        }

        /// <summary>
        ///     Creates a new <see cref="ITimer"/> instance passing an interval.
        /// </summary>
        /// <param name="interval">
        ///     The interval.
        /// </param>
        /// <returns>
        ///     The <see cref="ITimer"/>.
        /// </returns>
        public ITimer Create(double interval)
        {
            return new TimerWrap(interval);
        }

        #endregion
    }
}