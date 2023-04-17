namespace SystemWrapper.IO
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using SystemInterface.IO;

    /// <summary>
    ///      Wrapper for <see cref="T:System.IO.Stream"/> class.
    /// </summary>
    public class StreamWrap : IStream
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamWrap"/> class.
        /// </summary>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        public StreamWrap(Stream stream)
        {
            this.StreamInstance = stream;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     When overridden in a derived class, gets a value indicating whether the current stream supports reading. 
        /// </summary>
        public bool CanRead
        {
            get
            {
                return this.StreamInstance.CanRead;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets a value indicating whether the current stream supports seeking. 
        /// </summary>
        public bool CanSeek
        {
            get
            {
                return this.StreamInstance.CanSeek;
            }
        }

        /// <summary>
        ///     Gets a value that determines whether the current stream can time out. 
        /// </summary>
        public bool CanTimeout
        {
            get
            {
                return this.StreamInstance.CanTimeout;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets a value indicating whether the current stream supports writing. 
        /// </summary>
        public bool CanWrite
        {
            get
            {
                return this.StreamInstance.CanWrite;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets the length in bytes of the stream. 
        /// </summary>
        public long Length
        {
            get
            {
                return this.StreamInstance.Length;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, gets or sets the position within the current stream. 
        /// </summary>
        public long Position
        {
            get
            {
                return this.StreamInstance.Position;
            }

            set
            {
                this.StreamInstance.Position = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value, in milliseconds, that determines how long the stream will attempt to read before timing out. 
        /// </summary>
        public int ReadTimeout
        {
            get
            {
                return this.StreamInstance.ReadTimeout;
            }

            set
            {
                this.StreamInstance.ReadTimeout = value;
            }
        }

        /// <summary>
        ///     Gets <see cref="T:System.IO.Stream"/> object.
        /// </summary>
        public Stream StreamInstance { get; private set; }

        /// <summary>
        ///     Gets or sets a value, in milliseconds, that determines how long the stream will attempt to write before timing out. 
        /// </summary>
        public int WriteTimeout
        {
            get
            {
                return this.StreamInstance.WriteTimeout;
            }

            set
            {
                this.StreamInstance.WriteTimeout = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Begins an asynchronous read operation. 
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to read the data into.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer at which to begin writing data read from the stream.
        /// </param>
        /// <param name="count">
        ///     The maximum number of bytes to read.
        /// </param>
        /// <param name="callback">
        ///     An optional asynchronous callback, to be called when the read is complete.
        /// </param>
        /// <param name="state">
        ///     A user-provided object that distinguishes this particular asynchronous read request from other requests.
        /// </param>
        /// <returns>
        ///     An IAsyncResult that represents the asynchronous read, which could still be pending.
        /// </returns>
        public IAsyncResult BeginRead(byte[] buffer,
                                      int offset,
                                      int count,
                                      AsyncCallback callback,
                                      object state)
        {
            return this.StreamInstance.BeginRead(buffer, offset, count, callback, state);
        }

        /// <summary>
        ///     Begins an asynchronous write operation. 
        /// </summary>
        /// <param name="buffer">
        ///     The buffer to write data from.
        /// </param>
        /// <param name="offset">
        ///     The byte offset in buffer from which to begin writing.
        /// </param>
        /// <param name="count">
        ///     The maximum number of bytes to write.
        /// </param>
        /// <param name="callback">
        ///     An optional asynchronous callback, to be called when the write is complete.
        /// </param>
        /// <param name="state">
        ///     A user-provided object that distinguishes this particular asynchronous write request from other requests.
        /// </param>
        /// <returns>
        ///     An IAsyncResult that represents the asynchronous write, which could still be pending.
        /// </returns>
        public IAsyncResult BeginWrite(byte[] buffer,
                                       int offset,
                                       int count,
                                       AsyncCallback callback,
                                       object state)
        {
            return this.StreamInstance.BeginWrite(buffer, offset, count, callback, state);
        }

        /// <summary>
        ///     Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream. 
        /// </summary>
        public void Close()
        {
            this.StreamInstance.Close();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.StreamInstance.Dispose();
        }

        /// <summary>
        ///     Waits for the pending asynchronous read to complete. 
        /// </summary>
        /// <param name="asyncResult">
        ///     The reference to the pending asynchronous request to finish.
        /// </param>
        /// <returns>
        ///     The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end 
        ///     of the stream, otherwise, they should block until at least one byte is available.
        /// </returns>
        public int EndRead(IAsyncResult asyncResult)
        {
            return this.StreamInstance.EndRead(asyncResult);
        }

        /// <summary>
        ///     Ends an asynchronous write operation. 
        /// </summary>
        /// <param name="asyncResult">
        ///     A reference to the outstanding asynchronous I/O request.
        /// </param>
        public void EndWrite(IAsyncResult asyncResult)
        {
            this.StreamInstance.EndWrite(asyncResult);
        }

        /// <summary>
        ///     When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public void Flush()
        {
            this.StreamInstance.Flush();
        }

        public async Task FlushAsync()
        {
            await StreamInstance.FlushAsync();
        }

        /// <summary>
        ///     When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the 
        ///     number of bytes read.
        /// </summary>
        /// <param name="buffer">
        ///     An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and 
        ///     (offset + count - 1) replaced by the bytes read from the current source.
        /// </param>
        /// <param name="offset">
        ///     The zero-based byte offset in buffer at which to begin storing the data read from the current stream.
        /// </param>
        /// <param name="count">
        ///     The maximum number of bytes to be read from the current stream.
        /// </param>
        /// <returns>
        ///     The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently 
        ///     available, or zero (0) if the end of the stream has been reached.
        /// </returns>
        public int Read(byte[] buffer,
                        int offset,
                        int count)
        {
            return this.StreamInstance.Read(buffer, offset, count);
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            return await StreamInstance.ReadAsync(buffer, offset, count);
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return await StreamInstance.ReadAsync(buffer, offset, count, cancellationToken);
        }

        /// <summary>
        ///     Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream. 
        /// </summary>
        /// <returns>
        ///     The unsigned byte cast to an Int32, or -1 if at the end of the stream.
        /// </returns>
        public int ReadByte()
        {
            return this.StreamInstance.ReadByte();
        }

        /// <summary>
        ///     When overridden in a derived class, sets the position within the current stream. 
        /// </summary>
        /// <param name="offset">
        ///     A byte offset relative to the origin parameter.
        /// </param>
        /// <param name="origin">
        ///     A value of type SeekOrigin indicating the reference point used to obtain the new position.
        /// </param>
        /// <returns>
        ///     The new position within the current stream.
        /// </returns>
        public long Seek(long offset,
                         SeekOrigin origin)
        {
            return this.StreamInstance.Seek(offset, origin);
        }

        /// <summary>
        ///     When overridden in a derived class, sets the length of the current stream. 
        /// </summary>
        /// <param name="value">T
        ///     he desired length of the current stream in bytes.
        /// </param>
        public void SetLength(long value)
        {
            this.StreamInstance.SetLength(value);
        }

        /// <summary>
        ///     Creates a thread-safe (synchronized) wrapper around the specified Stream object. 
        /// </summary>
        /// <param name="stream">
        ///     The IStreamWrap object to synchronize.
        /// </param>
        /// <returns>
        ///     A thread-safe IStreamWrap object.
        /// </returns>
        public IStream Synchronized(IStream stream)
        {
            return new StreamWrap(Stream.Synchronized(stream.StreamInstance));
        }

        /// <summary>
        ///     When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this 
        ///     stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">
        ///     An array of bytes. This method copies count bytes from buffer to the current stream.
        /// </param>
        /// <param name="offset">
        ///     The zero-based byte offset in buffer at which to begin copying bytes to the current stream.
        /// </param>
        /// <param name="count">
        ///     The number of bytes to be written to the current stream.
        /// </param>
        public void Write(byte[] buffer,
                          int offset,
                          int count)
        {
            this.StreamInstance.Write(buffer, offset, count);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count)
        {
            await StreamInstance.WriteAsync(buffer, offset, count);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await StreamInstance.WriteAsync(buffer, offset, count, cancellationToken);
        }

        /// <summary>
        ///     Writes a byte to the current position in the stream and advances the position within the stream by one byte. 
        /// </summary>
        /// <param name="value">
        ///     The byte to write to the stream.
        /// </param>
        public void WriteByte(byte value)
        {
            this.StreamInstance.WriteByte(value);
        }

        public void CopyTo(IStream destination)
        {
            StreamInstance.CopyTo(destination.StreamInstance);
        }

        public void CopyTo(IStream destination, int bufferSize)
        {
            StreamInstance.CopyTo(destination.StreamInstance, bufferSize);
        }

        public async Task CopyToAsync(IStream destination)
        {
            await StreamInstance.CopyToAsync(destination.StreamInstance);
        }

        public async Task CopyToAsync(IStream destination, int bufferSize)
        {
            await StreamInstance.CopyToAsync(destination.StreamInstance, bufferSize);
        }

        public async Task CopyToAsync(IStream destination, int bufferSize, CancellationToken cancellationToken)
        {
            await StreamInstance.CopyToAsync(destination.StreamInstance, bufferSize, cancellationToken);
        }

        #endregion
    }
}
