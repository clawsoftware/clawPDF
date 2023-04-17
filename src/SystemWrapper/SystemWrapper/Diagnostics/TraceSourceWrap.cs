using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime;
using SystemInterface.Diagnostics;

namespace SystemWrapper.Diagnostics
{
    /// <inheritdoc />
    public class TraceSourceWrap : ITraceSource
    {
        #region Instance & Constructors

        /// <summary>
        /// Actual <see cref="TraceSource"/> instance.
        /// </summary>
        public TraceSource TraceSourceInstance { get; internal set; }

        /// <inheritdoc />
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public TraceSourceWrap(string name)
        {
            TraceSourceInstance = new TraceSource(name);
        }

        /// <inheritdoc />
        public TraceSourceWrap(string name, SourceLevels defaultLevel)
        {
            TraceSourceInstance = new TraceSource(name, defaultLevel);
        }

        #endregion Instance & Constructors

        #region Properties

        /// <inheritdoc />
        public StringDictionary Attributes
        {
            get { return TraceSourceInstance.Attributes; }
        }

        /// <inheritdoc />
        public TraceListenerCollection Listeners
        {
            get { return TraceSourceInstance.Listeners; }
        }

        /// <inheritdoc />
        public string Name
        {
            get { return TraceSourceInstance.Name; }
        }

        /// <inheritdoc />
        public SourceSwitch Switch
        {
            get { return TraceSourceInstance.Switch; }
            set { TraceSourceInstance.Switch = value; }
        }

        #endregion Properties

        /// <inheritdoc />
        public void Close()
        {
            TraceSourceInstance.Close();
        }

        /// <inheritdoc />
        public void Flush()
        {
            TraceSourceInstance.Flush();
        }

        /// <inheritdoc />
        public void TraceData(TraceEventType eventType, int id, object data)
        {
            TraceSourceInstance.TraceData(eventType, id, data);
        }

        /// <inheritdoc />
        public void TraceData(TraceEventType eventType, int id, params object[] data)
        {
            TraceSourceInstance.TraceData(eventType, id, data);
        }

        /// <inheritdoc />
        public void TraceEvent(TraceEventType eventType, int id)
        {
            TraceSourceInstance.TraceData(eventType, id);
        }

        /// <inheritdoc />
        public void TraceEvent(TraceEventType eventType, int id, string message)
        {
            TraceSourceInstance.TraceData(eventType, id, message);
        }

        /// <inheritdoc />
        public void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
        {
            TraceSourceInstance.TraceData(eventType, id, format, args);
        }

        /// <inheritdoc />
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void TraceInformation(string message)
        {
            TraceSourceInstance.TraceInformation(message);
        }

        /// <inheritdoc />
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void TraceInformation(string format, params object[] args)
        {
            TraceSourceInstance.TraceInformation(format, args);
        }

        /// <inheritdoc />
        public void TraceTransfer(int id, string message, Guid relatedActivityId)
        {
            TraceSourceInstance.TraceTransfer(id, message, relatedActivityId);
        }
    }
}