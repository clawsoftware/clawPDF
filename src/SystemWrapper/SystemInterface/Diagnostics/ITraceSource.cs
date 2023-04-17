using System;
using System.Collections.Specialized;
using System.Diagnostics;

namespace SystemInterface.Diagnostics
{
    /// <summary>
    ///     Provides a set of methods and properties that enable applications to trace
    ///     the execution of code and associate trace messages with their source.
    /// </summary>
    public interface ITraceSource
    {
        /// <summary>
        ///     Gets the custom switch attributes defined in the application configuration
        ///     file.
        /// </summary>
        /// <returns>A System.Collections.Specialized.StringDictionary containing the custom attributes
        ///    for the trace switch.
        /// </returns>
        StringDictionary Attributes { get; }

        /// <summary>
        /// Gets the collection of trace listeners for the trace source.
        /// </summary>
        /// <returns>A System.Diagnostics.TraceListenerCollection that contains the active trace
        /// listeners associated with the source.</returns>
        TraceListenerCollection Listeners { get; }

        /// <summary>
        ///     Gets the name of the trace source.
        /// </summary>
        /// <returns>The name of the trace source.</returns>
        string Name { get; }

        /// <summary>
        ///     Gets or sets the source switch value.
        /// </summary>
        /// <returns>A System.Diagnostics.SourceSwitch object representing the source switch value.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">
        ///    System.Diagnostics.TraceSource.Switch is set to null.
        /// </exception>
        SourceSwitch Switch { get; set; }

        /// <summary>
        /// Closes all the trace listeners in the trace listener collection.
        /// </summary>
        void Close();

        /// <summary>Flushes all the trace listeners in the trace listener collection.</summary>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void Flush();

        /// <summary>
        ///    Writes trace data to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///    collection using the specified event type, event identifier, and trace data.
        /// </summary>
        ///
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values that specifies the event
        ///    type of the trace data.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data.</param>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void TraceData(TraceEventType eventType, int id, object data);

        /// <summary>
        ///    Writes trace data to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///    collection using the specified event type, event identifier, and trace data
        ///    array.
        /// </summary>
        ///
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values that specifies the event
        ///    type of the trace data.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data.</param>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void TraceData(TraceEventType eventType, int id, params object[] data);

        /// <summary>
        ///    Writes a trace event message to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///    collection using the specified event type and event identifier.
        /// </summary>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values that specifies the event
        ///    type of the trace data.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void TraceEvent(TraceEventType eventType, int id);

        /// <summary>
        ///     Writes a trace event message to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///     collection using the specified event type, event identifier, and message.
        /// </summary>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values that specifies the event
        ///     type of the trace data.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">The trace message to write.</param>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void TraceEvent(TraceEventType eventType, int id, string message);

        /// <summary>
        ///    Writes a trace event to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///    collection using the specified event type, event identifier, and argument
        ///    array and format.
        /// </summary>
        ///
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values that specifies the event
        ///    type of the trace data.
        /// </param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="format">A composite format string that contains text intermixed with zero or more
        ///     format items, which correspond to objects in the args array.
        /// </param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="System.FormatException">
        ///    <paramref name="format"/> is invalid. -or- The number that indicates an argument to format is
        ///    less than zero, or greater than or equal to the number of specified objects
        ///    to format.
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void TraceEvent(TraceEventType eventType, int id, string format, params object[] args);

        /// <summary>
        ///    Writes an informational message to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///    collection using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void TraceInformation(string message);

        /// <summary>
        ///    Writes an informational message to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///    collection using the specified object array and formatting information.
        /// </summary>
        /// <param name="format">A composite format string that contains text intermixed with zero or more
        ///     format items, which correspond to objects in the args array.
        /// </param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="format"/> is null.</exception>
        /// <exception cref="System.FormatException">
        ///    <paramref name="format"/> is invalid. -or- The number that indicates an argument to format is
        ///    less than zero, or greater than or equal to the number of specified objects
        ///    to format.
        /// </exception>
        /// <exception cref="System.ObjectDisposedException">
        ///    An attempt was made to trace an event during finalization.
        /// </exception>
        void TraceInformation(string format, params object[] args);

        /// <summary>
        ///    Writes a trace transfer message to the trace listeners in the System.Diagnostics.TraceSource.Listeners
        ///    collection using the specified numeric identifier, message, and related activity
        ///    identifier.
        /// </summary>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">The trace message to write.</param>
        /// <param name="relatedActivityId">A System.Guid structure that identifies the related activity.</param>
        void TraceTransfer(int id, string message, Guid relatedActivityId);
    }
}
