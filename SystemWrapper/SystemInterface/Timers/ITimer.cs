namespace SystemInterface.Timers
{
    using System.ComponentModel;
    using System.Timers;

    /// <summary>
    ///     Wrapper for <see cref="System.Timers.Timer"/> class.
    /// </summary>
    public interface ITimer
    {
        #region Public Events

        /// <summary>
        ///     Occurs when the <see cref='System.Timers.Timer.Interval'/> has elapsed.
        /// </summary>
        event ElapsedEventHandler Elapsed;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the Timer raises the Tick event each time the specified
        ///     Interval has elapsed, when Enabled is set to true.
        /// </summary>
        bool AutoReset { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the <see cref='System.Timers.Timer'/>
        ///     is able to raise events at a defined interval.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        ///     Gets or sets the interval on which to raise events.
        /// </summary>
        double Interval { get; set; }

        /// <summary>
        ///     Sets the enable property in design mode to true by default.
        /// </summary>
        ISite Site { get; set; }

        /// <summary>
        ///     Gets or sets the object used to marshal event-handler calls that are issued when
        ///     an interval has elapsed.
        /// </summary>
        ISynchronizeInvoke SynchronizingObject { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Notifies the object that initialization is beginning and tells it to stand by.
        /// </summary>
        void BeginInit();

        /// <summary>
        ///     Disposes of the resources (other than memory) used by the <see cref='System.Timers.Timer'/>.
        /// </summary>
        void Close();

        /// <summary>
        ///     Notifies the object that initialization is complete.
        /// </summary>
        void EndInit();

        /// <summary>
        ///     Starts the timing by setting <see cref='System.Timers.Timer.Enabled'/> to <see langword='true'/>.
        /// </summary>
        void Start();

        /// <summary>
        ///     Stops the timing by setting <see cref='System.Timers.Timer.Enabled'/> to <see langword='false'/>.
        /// </summary>
        void Stop();

        #endregion
    }
}