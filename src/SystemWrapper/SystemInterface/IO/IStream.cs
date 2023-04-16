using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace SystemInterface.IO
{
    /// <summary>
    /// Description of IStreamWrap.
    /// </summary>
    public interface IStream : IDisposable
    {
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
        /// </summary>
        bool CanSeek { get; }

        /// <summary>
        /// Gets a value that determines whether the current stream can time out.
        /// </summary>
        bool CanTimeout { get; }

        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        /// When overridden in a derived class, gets the length in bytes of the stream.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// When overridden in a derived class, gets or sets the position within the current stream.
        /// </summary>
        long Position { get; set; }

        /// <summary>
        /// Gets or sets a value, in milliseconds, that determines how long the stream will attempt to read before timing out.
        /// </summary>
        int ReadTimeout { get; set; }

        /// <summary>
        /// Gets <see cref="T:System.IO.Stream"/> object.
        /// </summary>
        Stream StreamInstance { get; }

        /// <summary>
        /// Gets or sets a value, in milliseconds, that determines how long the stream will attempt to write before timing out.
        /// </summary>
        int WriteTimeout { get; set; }

        // Methods

        /// <summary>
        /// Begins an asynchronous read operation.
        /// </summary>
        /// <param name="buffer">The buffer to read the data into. </param>
        /// <param name="offset">The byte offset in buffer at which to begin writing data read from the stream. </param>
        /// <param name="count">The maximum number of bytes to read. </param>
        /// <param name="callback">An optional asynchronous callback, to be called when the read is complete. </param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests. </param>
        /// <returns>An IAsyncResult that represents the asynchronous read, which could still be pending. </returns>
        IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state);

        /// <summary>
        /// Begins an asynchronous write operation.
        /// </summary>
        /// <param name="buffer">The buffer to write data from. </param>
        /// <param name="offset">The byte offset in buffer from which to begin writing. </param>
        /// <param name="count">The maximum number of bytes to write. </param>
        /// <param name="callback">An optional asynchronous callback, to be called when the write is complete. </param>
        /// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
        /// <returns>An IAsyncResult that represents the asynchronous write, which could still be pending. </returns>
        IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state);

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
        /// </summary>
        void Close();

        /// <summary>
        /// Waits for the pending asynchronous read to complete.
        /// </summary>
        /// <param name="asyncResult">The reference to the pending asynchronous request to finish. </param>
        /// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end of the stream, otherwise, they should block until at least one byte is available.</returns>
        int EndRead(IAsyncResult asyncResult);

        /// <summary>
        /// Ends an asynchronous write operation.
        /// </summary>
        /// <param name="asyncResult">A reference to the outstanding asynchronous I/O request. </param>
        void EndWrite(IAsyncResult asyncResult);

        /// <summary>
        /// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        void Flush();

        //
        // Summary:
        //     Asynchronously clears all buffers for this stream and causes any buffered data
        //     to be written to the underlying device.
        //
        // Returns:
        //     A task that represents the asynchronous flush operation.
        //
        // Exceptions:
        //   T:System.ObjectDisposedException:
        //     The stream has been disposed.
        [ComVisible(false)]
        Task FlushAsync();

        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source. </param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream. </param>
        /// <param name="count">The maximum number of bytes to be read from the current stream. </param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached. </returns>
        int Read([In, Out] byte[] buffer, int offset, int count);

        //
        // Summary:
        //     Asynchronously reads a sequence of bytes from the current stream and advances
        //     the position within the stream by the number of bytes read.
        //
        // Parameters:
        //   buffer:
        //     The buffer to write the data into.
        //
        //   offset:
        //     The byte offset in buffer at which to begin writing data from the stream.
        //
        //   count:
        //     The maximum number of bytes to read.
        //
        // Returns:
        //     A task that represents the asynchronous read operation. The value of the TResult
        //     parameter contains the total number of bytes read into the buffer. The result
        //     value can be less than the number of bytes requested if the number of bytes currently
        //     available is less than the requested number, or it can be 0 (zero) if the end
        //     of the stream has been reached.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     offset or count is negative.
        //
        //   T:System.ArgumentException:
        //     The sum of offset and count is larger than the buffer length.
        //
        //   T:System.NotSupportedException:
        //     The stream does not support reading.
        //
        //   T:System.ObjectDisposedException:
        //     The stream has been disposed.
        //
        //   T:System.InvalidOperationException:
        //     The stream is currently in use by a previous read operation.
        [ComVisible(false)]
        Task<int> ReadAsync(byte[] buffer, int offset, int count);

        //
        // Summary:
        //     Asynchronously reads a sequence of bytes from the current stream, advances the
        //     position within the stream by the number of bytes read, and monitors cancellation
        //     requests.
        //
        // Parameters:
        //   buffer:
        //     The buffer to write the data into.
        //
        //   offset:
        //     The byte offset in buffer at which to begin writing data from the stream.
        //
        //   count:
        //     The maximum number of bytes to read.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous read operation. The value of the TResult
        //     parameter contains the total number of bytes read into the buffer. The result
        //     value can be less than the number of bytes requested if the number of bytes currently
        //     available is less than the requested number, or it can be 0 (zero) if the end
        //     of the stream has been reached.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     offset or count is negative.
        //
        //   T:System.ArgumentException:
        //     The sum of offset and count is larger than the buffer length.
        //
        //   T:System.NotSupportedException:
        //     The stream does not support reading.
        //
        //   T:System.ObjectDisposedException:
        //     The stream has been disposed.
        //
        //   T:System.InvalidOperationException:
        //     The stream is currently in use by a previous read operation.
        [ComVisible(false)]
        Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);

        /// <summary>
        /// Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
        /// </summary>
        /// <returns>The unsigned byte cast to an Int32, or -1 if at the end of the stream.</returns>
        int ReadByte();

        /// <summary>
        /// When overridden in a derived class, sets the position within the current stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter. </param>
        /// <param name="origin">A value of type SeekOrigin indicating the reference point used to obtain the new position. </param>
        /// <returns>The new position within the current stream. </returns>
        long Seek(long offset, SeekOrigin origin);

        /// <summary>
        /// When overridden in a derived class, sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes. </param>
        void SetLength(long value);

        /// <summary>
        /// Creates a thread-safe (synchronized) wrapper around the specified Stream object.
        /// </summary>
        /// <param name="stream">The IStreamWrap object to synchronize. </param>
        /// <returns>A thread-safe IStreamWrap object. </returns>
        IStream Synchronized(IStream stream);

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream. </param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream. </param>
        /// <param name="count">The number of bytes to be written to the current stream. </param>
        void Write(byte[] buffer, int offset, int count);

        //
        // Summary:
        //     Asynchronously writes a sequence of bytes to the current stream and advances
        //     the current position within this stream by the number of bytes written.
        //
        // Parameters:
        //   buffer:
        //     The buffer to write data from.
        //
        //   offset:
        //     The zero-based byte offset in buffer from which to begin copying bytes to the
        //     stream.
        //
        //   count:
        //     The maximum number of bytes to write.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     offset or count is negative.
        //
        //   T:System.ArgumentException:
        //     The sum of offset and count is larger than the buffer length.
        //
        //   T:System.NotSupportedException:
        //     The stream does not support writing.
        //
        //   T:System.ObjectDisposedException:
        //     The stream has been disposed.
        //
        //   T:System.InvalidOperationException:
        //     The stream is currently in use by a previous write operation.
        [ComVisible(false)]
        Task WriteAsync(byte[] buffer, int offset, int count);

        //
        // Summary:
        //     Asynchronously writes a sequence of bytes to the current stream, advances the
        //     current position within this stream by the number of bytes written, and monitors
        //     cancellation requests.
        //
        // Parameters:
        //   buffer:
        //     The buffer to write data from.
        //
        //   offset:
        //     The zero-based byte offset in buffer from which to begin copying bytes to the
        //     stream.
        //
        //   count:
        //     The maximum number of bytes to write.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous write operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     buffer is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     offset or count is negative.
        //
        //   T:System.ArgumentException:
        //     The sum of offset and count is larger than the buffer length.
        //
        //   T:System.NotSupportedException:
        //     The stream does not support writing.
        //
        //   T:System.ObjectDisposedException:
        //     The stream has been disposed.
        //
        //   T:System.InvalidOperationException:
        //     The stream is currently in use by a previous write operation.
        [ComVisible(false)]
        Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);


        /// <summary>
        /// Writes a byte to the current position in the stream and advances the position within the stream by one byte.
        /// </summary>
        /// <param name="value">The byte to write to the stream. </param>
        void WriteByte(byte value);

        //
        // Summary:
        //     Reads the bytes from the current stream and writes them to another stream.
        //
        // Parameters:
        //   destination:
        //     The stream to which the contents of the current stream will be copied.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     destination is null.
        //
        //   T:System.NotSupportedException:
        //     The current stream does not support reading.-or-destination does not support
        //     writing.
        //
        //   T:System.ObjectDisposedException:
        //     Either the current stream or destination were closed before the System.IO.Stream.CopyTo(System.IO.Stream)
        //     method was called.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        void CopyTo(IStream destination);
        //
        // Summary:
        //     Reads the bytes from the current stream and writes them to another stream, using
        //     a specified buffer size.
        //
        // Parameters:
        //   destination:
        //     The stream to which the contents of the current stream will be copied.
        //
        //   bufferSize:
        //     The size of the buffer. This value must be greater than zero. The default size
        //     is 81920.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     destination is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     bufferSize is negative or zero.
        //
        //   T:System.NotSupportedException:
        //     The current stream does not support reading.-or-destination does not support
        //     writing.
        //
        //   T:System.ObjectDisposedException:
        //     Either the current stream or destination were closed before the System.IO.Stream.CopyTo(System.IO.Stream)
        //     method was called.
        //
        //   T:System.IO.IOException:
        //     An I/O error occurred.
        void CopyTo(IStream destination, int bufferSize);
        //
        // Summary:
        //     Asynchronously reads the bytes from the current stream and writes them to another
        //     stream.
        //
        // Parameters:
        //   destination:
        //     The stream to which the contents of the current stream will be copied.
        //
        // Returns:
        //     A task that represents the asynchronous copy operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     destination is null.
        //
        //   T:System.ObjectDisposedException:
        //     Either the current stream or the destination stream is disposed.
        //
        //   T:System.NotSupportedException:
        //     The current stream does not support reading, or the destination stream does not
        //     support writing.
        [ComVisible(false)]
        Task CopyToAsync(IStream destination);
        //
        // Summary:
        //     Asynchronously reads the bytes from the current stream and writes them to another
        //     stream, using a specified buffer size.
        //
        // Parameters:
        //   destination:
        //     The stream to which the contents of the current stream will be copied.
        //
        //   bufferSize:
        //     The size, in bytes, of the buffer. This value must be greater than zero. The
        //     default size is 81920.
        //
        // Returns:
        //     A task that represents the asynchronous copy operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     destination is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     buffersize is negative or zero.
        //
        //   T:System.ObjectDisposedException:
        //     Either the current stream or the destination stream is disposed.
        //
        //   T:System.NotSupportedException:
        //     The current stream does not support reading, or the destination stream does not
        //     support writing.
        [ComVisible(false)]
        Task CopyToAsync(IStream destination, int bufferSize);
        //
        // Summary:
        //     Asynchronously reads the bytes from the current stream and writes them to another
        //     stream, using a specified buffer size and cancellation token.
        //
        // Parameters:
        //   destination:
        //     The stream to which the contents of the current stream will be copied.
        //
        //   bufferSize:
        //     The size, in bytes, of the buffer. This value must be greater than zero. The
        //     default size is 81920.
        //
        //   cancellationToken:
        //     The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.
        //
        // Returns:
        //     A task that represents the asynchronous copy operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     destination is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     buffersize is negative or zero.
        //
        //   T:System.ObjectDisposedException:
        //     Either the current stream or the destination stream is disposed.
        //
        //   T:System.NotSupportedException:
        //     The current stream does not support reading, or the destination stream does not
        //     support writing.
        [ComVisible(false)]
        Task CopyToAsync(IStream destination, int bufferSize, CancellationToken cancellationToken);
    }
}
