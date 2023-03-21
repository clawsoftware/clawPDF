using System;
using System.IO;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Text;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.StreamWriter"/> class.
    /// </summary>
    [CLSCompliant(false)]
    public interface IStreamWriter : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.StreamWriterWrap"/> class on the specified path.
        /// </summary>
        /// <param name="streamWriter">A <see cref="T:System.IO.StreamWriter"/> object.</param>
        void Initialize(StreamWriter streamWriter);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.StreamWriterWrap"/> class for the specified stream, using UTF-8 encoding and the default buffer size.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        void Initialize(Stream stream);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.StreamWriterWrap"/> class for the specified stream, using UTF-8 encoding and the default buffer size.
        /// </summary>
        /// <param name="path">The complete file path to write to. path can be a file name.</param>
        void Initialize(string path);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.StreamWriterWrap"/> class for the specified stream, using the specified encoding and the default buffer size.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        void Initialize(Stream stream, Encoding encoding);

        /// <summary>
        /// Initializes a new instance of the StreamWriter class for the specified file on the specified path, using the default encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.
        /// </summary>
        /// <param name="path">The complete file path to write to.</param>
        /// <param name="append">Determines whether data is to be appended to the file. If the file exists and append is false, the file is overwritten. If the file exists and append is true, the data is appended to the file. Otherwise, a new file is created.</param>
        void Initialize(string path, bool append);

        /// <summary>
        /// Initializes a new instance of the StreamWriter class for the specified stream, using the specified encoding and buffer size.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="bufferSize">Sets the buffer size.</param>
        void Initialize(Stream stream, Encoding encoding, int bufferSize);

        /// <summary>
        /// Initializes a new instance of the StreamWriter class for the specified file on the specified path, using the specified encoding and default buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.
        /// </summary>
        /// <param name="path">The complete file path to write to.</param>
        /// <param name="append">Determines whether data is to be appended to the file. If the file exists and append is false, the file is overwritten. If the file exists and append is true, the data is appended to the file. Otherwise, a new file is created.</param>
        /// <param name="encoding">The character encoding to use.</param>
        void Initialize(string path, bool append, Encoding encoding);

        /// <summary>
        /// Initializes a new instance of the StreamWriter class for the specified file on the specified path, using the specified encoding and buffer size. If the file exists, it can be either overwritten or appended to. If the file does not exist, this constructor creates a new file.
        /// </summary>
        /// <param name="path">The complete file path to write to.</param>
        /// <param name="append">Determines whether data is to be appended to the file. If the file exists and append is false, the file is overwritten. If the file exists and append is true, the data is appended to the file. Otherwise, a new file is created.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="bufferSize">Sets the buffer size.</param>
        void Initialize(string path, bool append, Encoding encoding, int bufferSize);

        // Properties

        /// <summary>
        /// Gets or sets a value indicating whether the IStreamWriterWrap will flush its buffer to the underlying stream after every call to IStreamWriterWrap.Write.
        /// </summary>
        bool AutoFlush { get; set; }

        /// <summary>
        /// Gets the underlying stream that interfaces with a backing store.
        /// </summary>
        Stream BaseStream { get; }

        /// <summary>
        /// Gets the Encoding in which the output is written.
        /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// Gets an object that controls formatting.
        /// </summary>
        IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Gets or sets the line terminator string used by the current TextWriter.
        /// </summary>
        string NewLine { get; set; }

        /// <summary>
        /// Gets <see cref="T:System.IO.StreamWriter"/> object.
        /// </summary>
        StreamWriter StreamWriterInstance { get; }

        // Methods

        /// <summary>
        /// Implements a TextWriter for writing characters to a stream in a particular encoding.
        /// </summary>
        void Close();

        /// <summary>
        /// Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.
        /// </summary>
        /// <param name="requestedType">The Type of the object that the new ObjRef will reference.</param>
        /// <returns>Information required to generate a proxy.</returns>
        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
        ObjRef CreateObjRef(Type requestedType);

        /// <summary>
        /// Clears all buffers for the current writer and causes any buffered data to be written to the underlying stream.
        /// </summary>
        void Flush();

        /// <summary>
        /// Writes a character to the stream.
        /// </summary>
        /// <param name="value">The character to write to the text stream.</param>
        void Write(char value);

        /// <summary>
        /// Writes a character array to the stream.
        /// </summary>
        /// <param name="buffer">A character array containing the data to write. If buffer is nullNothingnullptra null reference (Nothing in Visual Basic), nothing is written.</param>
        void Write(char[] buffer);

        /// <summary>
        /// Writes a string to the stream.
        /// </summary>
        /// <param name="value">The string to write to the stream. If value is null, nothing is written.</param>
        void Write(string value);

        /// <summary>
        /// Writes the text representation of a Boolean value to the text stream.
        /// </summary>
        /// <param name="value">The Boolean to write.</param>
        void Write(bool value);

        /// <summary>
        /// Writes the text representation of a decimal value to the text stream.
        /// </summary>
        /// <param name="value">The decimal value to write.</param>
        void Write(decimal value);

        /// <summary>
        /// Writes the text representation of an 8-byte floating-point value to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte floating-point value to write.</param>
        void Write(double value);

        /// <summary>
        /// Writes the text representation of a 4-byte signed integer to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte signed integer to write.</param>
        void Write(int value);

        /// <summary>
        /// Writes the text representation of an 8-byte signed integer to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte signed integer to write.</param>
        void Write(long value);

        /// <summary>
        /// Writes the text representation of an object to the text stream by calling ToString on that object.
        /// </summary>
        /// <param name="value">The object to write. </param>
        void Write(object value);

        /// <summary>
        /// Writes the text representation of a 4-byte floating-point value to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte floating-point value to write.</param>
        void Write(float value);

        /// <summary>
        /// Writes the text representation of a 4-byte unsigned integer to the text stream.
        /// </summary>
        /// <param name="value">The 4-byte unsigned integer to write.</param>
        void Write(uint value);

        /// <summary>
        /// Writes the text representation of an 8-byte unsigned integer to the text stream.
        /// </summary>
        /// <param name="value">The 8-byte unsigned integer to write.</param>
        void Write(ulong value);

        /// <summary>
        /// Writes out a formatted string, using the same semantics as String.Format.
        /// </summary>
        /// <param name="format">The formatting string. </param>
        /// <param name="arg0">An object to write into the formatted string.</param>
        void Write(string format, object arg0);

        /// <summary>
        /// Writes out a formatted string, using the same semantics as String.Format.
        /// </summary>
        /// <param name="format">The formatting string. </param>
        /// <param name="arg">The object array to write into the formatted string. </param>
        void Write(string format, object[] arg);

        /// <summary>
        /// Writes a subarray of characters to the stream.
        /// </summary>
        /// <param name="buffer">A character array containing the data to write. </param>
        /// <param name="index">The index into buffer at which to begin writing.</param>
        /// <param name="count">The number of characters to read from buffer. </param>
        void Write(char[] buffer, int index, int count);

        /// <summary>
        /// Writes out a formatted string, using the same semantics as String.Format.
        /// </summary>
        /// <param name="format">The formatting string. </param>
        /// <param name="arg0">An object to write into the formatted string.</param>
        /// <param name="arg1">An object to write into the formatted string.</param>
        void Write(string format, object arg0, object arg1);

        /// <summary>
        /// Writes out a formatted string, using the same semantics as String.Format.
        /// </summary>
        /// <param name="format">The formatting string. </param>
        /// <param name="arg0">An object to write into the formatted string.</param>
        /// <param name="arg1">An object to write into the formatted string.</param>
        /// <param name="arg2">An object to write into the formatted string.</param>
        void Write(string format, object arg0, object arg1, object arg2);
    }
}
