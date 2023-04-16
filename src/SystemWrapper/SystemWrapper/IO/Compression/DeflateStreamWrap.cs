using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using SystemInterface.IO;
using SystemInterface.IO.Compression;

namespace SystemWrapper.IO.Compression
{
    /// <summary>
    /// Description of DeflateStreamWrap.
    /// </summary>
    public class DeflateStreamWrap : IDeflateStream
    {
        private CompressionMode _mode;

        #region Constructors and Initializers

        /// <summary>
        /// Creates an uninitialized version of DeflateStreamWrap.
        /// </summary>
        public DeflateStreamWrap()
        {
            // this constructor assumes caller will make a subsequent call to Initialize
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.Compression.DeflateStreamWrap"/> class.
        /// </summary>
        /// <param name="stream">The stream to compress or decompress.</param>
        /// <param name="mode">One of the CompressionMode values that indicates the action to take.</param>
        public DeflateStreamWrap(IStream stream, CompressionMode mode)
        {
            Initialize(stream, mode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.Compression.DeflateStreamWrap"/> class.
        /// </summary>
        /// <param name="stream">The stream to compress or decompress.</param>
        /// <param name="mode">One of the CompressionMode values that indicates the action to take.</param>
        public void Initialize(IStream stream, CompressionMode mode)
        {
            Initialize(stream.StreamInstance, mode);
        }

        private void Initialize(Stream stream, CompressionMode mode)
        {
            DeflateStreamInstance = new DeflateStream(stream, mode);
            _mode = mode;
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public DeflateStream DeflateStreamInstance { get; private set; }

        public Task FlushAsync()
        {
            return DeflateStreamInstance.FlushAsync();
        }

        /// <inheritdoc />
        public int Read(byte[] array, int offset, int count)
        {
            return DeflateStreamInstance.Read(array, offset, count);
        }

        public Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            return DeflateStreamInstance.ReadAsync(buffer, offset, count);
        }

        public Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return DeflateStreamInstance.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public int ReadByte()
        {
            return DeflateStreamInstance.ReadByte();
        }

        public long Seek(long offset, SeekOrigin origin)
        {
            return DeflateStreamInstance.Seek(offset, origin);
        }

        public void SetLength(long value)
        {
            DeflateStreamInstance.SetLength(value);
        }

        public IStream Synchronized(IStream stream)
        {
            var deflateStreamWrap = new DeflateStreamWrap();
            deflateStreamWrap.Initialize(Stream.Synchronized(stream.StreamInstance), _mode);

            return deflateStreamWrap;
        }

        /// <inheritdoc />
        public void Write(byte[] array, int offset, int count)
        {
            DeflateStreamInstance.Write(array, offset, count);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count)
        {
            return DeflateStreamInstance.WriteAsync(buffer, offset, count);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return DeflateStreamInstance.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public void WriteByte(byte value)
        {
            DeflateStreamInstance.WriteByte(value);
        }

        public void CopyTo(IStream destination)
        {
            DeflateStreamInstance.CopyTo(destination.StreamInstance);
        }

        public void CopyTo(IStream destination, int bufferSize)
        {
            DeflateStreamInstance.CopyTo(destination.StreamInstance, bufferSize);
        }

        public Task CopyToAsync(IStream destination)
        {
            return DeflateStreamInstance.CopyToAsync(destination.StreamInstance);
        }

        public Task CopyToAsync(IStream destination, int bufferSize)
        {
            return DeflateStreamInstance.CopyToAsync(destination.StreamInstance, bufferSize);
        }

        public Task CopyToAsync(IStream destination, int bufferSize, CancellationToken cancellationToken)
        {
            return DeflateStreamInstance.CopyToAsync(destination.StreamInstance, bufferSize, cancellationToken);
        }

        public void EndWrite(IAsyncResult asyncResult)
        {
            DeflateStreamInstance.EndWrite(asyncResult);
        }

        /// <inheritdoc />
        public void Flush()
        {
            DeflateStreamInstance.Flush();
        }

        public bool CanRead
        {
            get { return DeflateStreamInstance.CanRead; }
        }

        public bool CanSeek
        {
            get { return DeflateStreamInstance.CanSeek; }
        }


        public bool CanTimeout
        {
            get { return DeflateStreamInstance.CanTimeout; }
        }


        public bool CanWrite
        {
            get { return DeflateStreamInstance.CanWrite; }
        }


        public long Length
        {
            get { return DeflateStreamInstance.Length; }
        }

        public long Position
        {
            get { return DeflateStreamInstance.Position; }
            set { DeflateStreamInstance.Position = value; }

        }

        public int ReadTimeout
        {
            get { return DeflateStreamInstance.ReadTimeout; }
            set { DeflateStreamInstance.ReadTimeout = value; }
        }

        public Stream StreamInstance
        {
            get { return DeflateStreamInstance; }
        }

        public int WriteTimeout
        {
            get { return DeflateStreamInstance.WriteTimeout; }
            set { DeflateStreamInstance.WriteTimeout = value; }
        }

        public IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return DeflateStreamInstance.BeginRead(buffer, offset, count, callback, state);
        }

        public IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return DeflateStreamInstance.BeginWrite(buffer, offset, count, callback, state);
        }

        /// <inheritdoc />
        public void Close()
        {
            DeflateStreamInstance.Close();
        }

        public int EndRead(IAsyncResult asyncResult)
        {
            return DeflateStreamInstance.EndRead(asyncResult);
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
            DeflateStreamInstance.Dispose();
        }
    }
}
