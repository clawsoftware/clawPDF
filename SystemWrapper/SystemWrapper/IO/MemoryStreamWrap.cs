using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using SystemInterface.IO;

namespace SystemWrapper.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.MemoryStream"/> class.
    /// </summary>
    public class MemoryStreamWrap : IMemoryStream
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized to zero.
        /// </summary>
        public MemoryStreamWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized to zero.
        /// </summary>
        public void Initialize()
        {
            MemoryStreamInstance = new MemoryStream();
        }

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized to zero.
        /// </summary>
        public MemoryStreamWrap(Stream stream)
        {
            Initialize(stream);
        }

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized to zero.
        /// </summary>
        public void Initialize(Stream stream)
        {
            MemoryStreamInstance = stream as MemoryStream;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.MemoryStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="memoryStream">A <see cref="T:System.IO.MemoryStream"/> object.</param>
        public MemoryStreamWrap(MemoryStream memoryStream)
        {
            Initialize(memoryStream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.MemoryStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="memoryStream">A <see cref="T:System.IO.MemoryStream"/> object.</param>
        public void Initialize(MemoryStream memoryStream)
        {
            MemoryStreamInstance = memoryStream;
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified byte array.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
        public MemoryStreamWrap(byte[] buffer)
        {
            Initialize(buffer);
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified byte array.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
        public void Initialize(byte[] buffer)
        {
            MemoryStreamInstance = new MemoryStream(buffer);
        }

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized as specified.
        /// </summary>
        /// <param name="capacity"></param>
        public MemoryStreamWrap(int capacity)
        {
            Initialize(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized as specified.
        /// </summary>
        /// <param name="capacity"></param>
        public void Initialize(int capacity)
        {
            MemoryStreamInstance = new MemoryStream(capacity);
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified byte array with the CanWrite property set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        public MemoryStreamWrap(byte[] buffer, bool writable)
        {
            Initialize(buffer, writable);
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified byte array with the CanWrite property set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        public void Initialize(byte[] buffer, bool writable)
        {
            MemoryStreamInstance = new MemoryStream(buffer, writable);
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified region (index) of a byte array.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes. </param>
        public MemoryStreamWrap(byte[] buffer, int index, int count)
        {
            Initialize(buffer, index, count);
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified region (index) of a byte array.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes. </param>
        public void Initialize(byte[] buffer, int index, int count)
        {
            MemoryStreamInstance = new MemoryStream(buffer, index, count);
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified region of a byte array, with the CanWrite property set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes. </param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        public MemoryStreamWrap(byte[] buffer, int index, int count, bool writable)
        {
            Initialize(buffer, index, count, writable);
        }

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified region of a byte array, with the CanWrite property set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes. </param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        public void Initialize(byte[] buffer, int index, int count, bool writable)
        {
            MemoryStreamInstance = new MemoryStream(buffer, index, count, writable);
        }

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class based on the specified region of a byte array, with the CanWrite property set as specified, and the ability to call GetBuffer set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes.</param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        /// <param name="publiclyVisible"> <c>true</c> to enable GetBuffer, which returns the unsigned byte array from which the stream was created; otherwise, false. </param>
        public MemoryStreamWrap(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
        {
            Initialize(buffer, index, count, writable, publiclyVisible);
        }

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class based on the specified region of a byte array, with the CanWrite property set as specified, and the ability to call GetBuffer set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes.</param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        /// <param name="publiclyVisible"> <c>true</c> to enable GetBuffer, which returns the unsigned byte array from which the stream was created; otherwise, false. </param>
        public void Initialize(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
        {
            MemoryStreamInstance = new MemoryStream(buffer, index, count, writable, publiclyVisible);
        }

        #endregion Constructors and Initializers

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        public bool CanRead
        {
            get { return MemoryStreamInstance.CanRead; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        public bool CanSeek
        {
            get { return MemoryStreamInstance.CanSeek; }
        }

        /// <inheritdoc />
        [ComVisible(false)]
        public bool CanTimeout
        {
            get { return MemoryStreamInstance.CanTimeout; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// </summary>
        public bool CanWrite
        {
            get { return MemoryStreamInstance.CanWrite; }
        }

        /// <inheritdoc />
        public int Capacity
        {
            get { return MemoryStreamInstance.Capacity; }
            set { MemoryStreamInstance.Capacity = value; }
        }

        /// <summary>
        /// Gets the length of the stream in bytes.
        /// </summary>
        public long Length
        {
            get { return MemoryStreamInstance.Length; }
        }

        /// <inheritdoc />
        public MemoryStream MemoryStreamInstance { get; private set; }

        /// <summary>
        /// Gets or sets the current position within the stream.
        /// </summary>
        public long Position
        {
            get { return MemoryStreamInstance.Position; }
            set { MemoryStreamInstance.Position = value; }
        }

        /// <inheritdoc />
        [ComVisible(false)]
        public int ReadTimeout
        {
            get { return MemoryStreamInstance.ReadTimeout; }
            set { MemoryStreamInstance.ReadTimeout = value; }
        }

        /// <inheritdoc />
        public Stream StreamInstance
        {
            get
            {
                return MemoryStreamInstance;
            }
        }

        /// <inheritdoc />
        [ComVisible(false)]
        public int WriteTimeout
        {
            get { return MemoryStreamInstance.WriteTimeout; }
            set { MemoryStreamInstance.WriteTimeout = value; }
        }

        /// <inheritdoc />
        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return MemoryStreamInstance.BeginRead(buffer, offset, count, callback, state);
        }

        /// <inheritdoc />
        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return MemoryStreamInstance.BeginWrite(buffer, offset, count, callback, state);
        }

        /// <inheritdoc />
        public virtual void Close()
        {
            MemoryStreamInstance.Close();
        }

        /// <inheritdoc />
        public int EndRead(IAsyncResult asyncResult)
        {
            return MemoryStreamInstance.EndRead(asyncResult);
        }

        /// <inheritdoc />
        public void EndWrite(IAsyncResult asyncResult)
        {
            MemoryStreamInstance.EndWrite(asyncResult);
        }

        /// <summary>
        /// Overrides Stream.Flush so that no action is performed.
        /// </summary>
        public void Flush()
        {
            MemoryStreamInstance.Flush();
        }

        /// <summary>
        /// Reads a block of bytes from the current stream and writes the data to buffer.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified byte array with the values between offset and (offset + count - 1) replaced by the characters read from the current stream. </param>
        /// <param name="offset">The byte offset in buffer at which to begin reading.</param>
        /// <param name="count">The maximum number of bytes to read. </param>
        /// <returns>The total number of bytes written into the buffer. This can be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached before any bytes are read. </returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            return MemoryStreamInstance.Read(buffer, offset, count);
        }

        /// <summary>
        /// Reads a byte from the current stream.
        /// </summary>
        /// <returns>The byte cast to a Int32, or -1 if the end of the stream has been reached.</returns>
        public int ReadByte()
        {
            return MemoryStreamInstance.ReadByte();
        }

        /// <summary>
        /// Sets the position within the current stream to the specified value.
        /// </summary>
        /// <param name="offset">The new position within the stream. This is relative to the loc parameter, and can be positive or negative. </param>
        /// <param name="origin">A value of type SeekOrigin, which acts as the seek reference point. </param>
        /// <returns>The new position within the stream, calculated by combining the initial reference point and the offset.</returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            return MemoryStreamInstance.Seek(offset, origin);
        }

        /// <summary>
        /// Sets the length of the current stream to the specified value.
        /// </summary>
        /// <param name="value">The value at which to set the length.</param>
        public void SetLength(long value)
        {
            MemoryStreamInstance.SetLength(value);
        }

        /// <inheritdoc />
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        public IStream Synchronized(IStream stream)
        {
            return new MemoryStreamWrap(Stream.Synchronized(stream.StreamInstance));
        }

        /// <summary>
        /// Writes a block of bytes to the current stream using data read from buffer.
        /// </summary>
        /// <param name="buffer">The buffer to write data from. </param>
        /// <param name="offset">The byte offset in buffer at which to begin writing from. </param>
        /// <param name="count">The maximum number of bytes to write. </param>
        public void Write(byte[] buffer, int offset, int count)
        {
            MemoryStreamInstance.Write(buffer, offset, count);
        }

        /// <summary>
        /// Writes a byte to the current stream at the current position.
        /// </summary>
        /// <param name="value">The byte to write. </param>
        public void WriteByte(byte value)
        {
            MemoryStreamInstance.WriteByte(value);
        }

        /// <inheritdoc />
        public byte[] GetBuffer()
        {
            return MemoryStreamInstance.GetBuffer();
        }

        /// <inheritdoc />
        public byte[] ToArray()
        {
            return MemoryStreamInstance.ToArray();
        }

        /// <inheritdoc />
        public void WriteTo(IStream stream)
        {
            MemoryStreamInstance.WriteTo(stream.StreamInstance);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            MemoryStreamInstance.Dispose();
        }

        public async Task FlushAsync()
        {
            await MemoryStreamInstance.FlushAsync();
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            return await MemoryStreamInstance.ReadAsync(buffer, offset, count);
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return await MemoryStreamInstance.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count)
        {
            await MemoryStreamInstance.WriteAsync(buffer, offset, count);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await MemoryStreamInstance.WriteAsync(buffer, offset, count, cancellationToken);
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
    }
}
