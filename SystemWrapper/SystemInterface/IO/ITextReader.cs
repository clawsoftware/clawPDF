using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace SystemInterface.IO
{
    /// <summary>
    /// Description of ITextReaderWrap.
    /// </summary>
    public interface ITextReader : IDisposable
    {
        /// <summary>
        /// Gets <see cref="T:System.IO.TextReader"/> object.
        /// </summary>
        TextReader TextReaderInstance { get; }

        /// <summary>
        /// Closes the IStreamReaderWrap object and the underlying stream, and releases any system resources associated with the reader.
        /// </summary>
        void Close();

        /// <summary>
        /// Reads the next character without changing the state of the reader or the character source. Returns the next available character without actually reading it from the input stream.
        /// </summary>
        /// <returns>An integer representing the next character to be read, or -1 if no more characters are available or the stream does not support seeking.</returns>
        int Peek();

        /// <summary>
        /// Reads the next character from the input stream and advances the character position by one character.
        /// </summary>
        /// <returns>The next character from the input stream represented as an Int32 object, or -1 if no more characters are available.</returns>
        int Read();

        /// <summary>
        /// Reads a maximum of count characters from the current stream and writes the data to buffer, beginning at index.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified character array with the values between index and (index + count - 1) replaced by the characters read from the current source.</param>
        /// <param name="index">The place in buffer at which to begin writing.</param>
        /// <param name="count">The maximum number of characters to read. If the end of the stream is reached before count of characters is read into buffer, the current method returns.</param>
        /// <returns>The number of characters that have been read. The number will be less than or equal to count, depending on whether the data is available within the stream. This method returns zero if called when no more characters are left to read.</returns>
        int Read([In, Out] char[] buffer, int index, int count);

        /// <summary>
        /// Reads a maximum of count characters from the current stream, and writes the data to buffer, beginning at index.
        /// </summary>
        /// <param name="buffer">When this method returns, this parameter contains the specified character array with the values between index and (index + count -1) replaced by the characters read from the current source.</param>
        /// <param name="index">The place in buffer at which to begin writing.</param>
        /// <param name="count">The maximum number of characters to read. </param>
        /// <returns>The position of the underlying stream is advanced by the number of characters that were read into buffer.
        /// The number of characters that have been read. The number will be less than or equal to count, depending on whether all input characters have been read. </returns>
        int ReadBlock([In, Out] char[] buffer, int index, int count);

        /// <summary>
        /// Reads a line of characters from the current stream and returns the data as a string.
        /// </summary>
        /// <returns>The next line from the input stream, or nullNothingnullptra null reference (Nothing in Visual Basic) if the end of the input stream is reached.</returns>
        string ReadLine();

        /// <summary>
        /// Reads all characters from the current position to the end of the ITextReaderWrap and returns them as one string.
        /// </summary>
        /// <returns>A string containing all characters from the current position to the end of the ITextReaderWrap.</returns>
        string ReadToEnd();

        /// <summary>
        /// Creates a thread-safe wrapper around the specified ITextReaderWrap.
        /// </summary>
        /// <param name="reader">The ITextReaderWrap to synchronize.</param>
        /// <returns>A thread-safe ITextReaderWrap.</returns>
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        ITextReader Synchronized(ITextReader reader);
    }
}
