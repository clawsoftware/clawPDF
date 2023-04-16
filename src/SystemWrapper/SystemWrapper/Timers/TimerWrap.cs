namespace SystemWrapper.Timers
{
    using System.ComponentModel;
    using System.Timers;

    using SystemInterface.Timers;

    /// <summary>
    ///     Wrapper for <see cref="System.Timers.Timer"/> class.
    /// </summary>
    public class TimerWrap : ITimer
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimerWrap"/> class.
        /// </summary>
        public TimerWrap()
        {
            this.TimerInstance = new Timer();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimerWrap"/> class.
        /// </summary>
        /// <param name="interval">
        ///     The interval.
        /// </param>
        public TimerWrap(double interval)
        {
            this.TimerInstance = new Timer(interval);
        }

        #endregion

        #region Public Events

        public event ElapsedEventHandler Elapsed
        {
            add
            {
                this.TimerInstance.Elapsed += value;
            }

            remove
            {
                this.TimerInstance.Elapsed -= value;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the Timer raises the Tick event each time the specified
        ///     Interval has elapsed, when Enabled is set to true.
        /// </summary>
        public bool AutoReset
        {
            get
            {
                return this.TimerInstance.AutoReset;
            }

            set
            {
                this.TimerInstance.AutoReset = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the <see cref='System.Timers.Timer'/>
        ///     is able to raise events at a defined interval.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.TimerInstance.Enabled;
            }

            set
            {
                this.TimerInstance.Enabled = value;
            }
        }

        /// <summary>
        ///     Gets or sets the interval on which to raise events.
        /// </summary>
        public double Interval
        {
            get
            {
                return this.TimerInstance.Interval;
            }

            set
            {
                this.TimerInstance.Interval = value;
            }
        }

        /// <summary>
        ///     Sets the enable property in design mode to true by default.
        /// </summary>
        public ISite Site
        {
            get
            {
                return this.TimerInstance.Site;
            }

            set
            {
                this.TimerInstance.Site = value;
            }
        }

        /// <summary>
        ///     Gets or sets the object used to marshal event-handler calls that are issued when
        ///     an interval has elapsed.
        /// </summary>
        public ISynchronizeInvoke SynchronizingObject
        {
            get
            {
                return this.TimerInstance.SynchronizingObject;
            }

            set
            {
                this.TimerInstance.SynchronizingObject = value;
            }
        }

        /// <summary>
        ///     Gets the timer instance.
        /// </summary>
        public Timer TimerInstance { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Notifies the object that initialization is beginning and tells it to stand by.
        /// </summary>
        public void BeginInit()
        {
            this.TimerInstance.BeginInit();
        }

        /// <summary>
        ///     Disposes of the resources (other than memory) used by the <see cref='System.Timers.Timer'/>.
        /// </summary>
        public void Close()
        {
            this.TimerInstance.Close();
        }

        /// <summary>
        ///     Notifies the object that initialization is complete.
        /// </summary>
        public void EndInit()
        {
            this.TimerInstance.EndInit();
        }

        /// <summary>
        ///     Starts the timing by setting <see cref='System.Timers.Timer.Enabled'/> to <see langword='true'/>.
        /// </summary>
        public void Start()
        {
            this.TimerInstance.Start();
        }

        /// <summary>
        ///     Stops the timing by setting <see cref='System.Timers.Timer.Enabled'/> to <see langword='false'/>.
        /// </summary>
        public void Stop()
        {
            this.TimerInstance.Stop();
        }

        #endregion
    }
}