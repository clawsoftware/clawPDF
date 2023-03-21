namespace SystemWrapper.IO
{
    using System.IO;

    using SystemInterface.IO;

    /// <summary>
    ///     Factory that creates a new <see cref="IMemoryStreamFactory"/> instance.
    /// </summary>
    public class MemoryStreamFactory : IMemoryStreamFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class.
        /// </summary>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create()
        {
            return new MemoryStreamWrap();
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(Stream stream)
        {
            return new MemoryStreamWrap(stream);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the memory stream.
        /// </summary>
        /// <param name="memoryStream">
        ///     The memory stream.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(MemoryStream memoryStream)
        {
            return new MemoryStreamWrap(memoryStream);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the byte buffer.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(byte[] buffer)
        {
            return new MemoryStreamWrap(buffer);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the capacity.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(int capacity)
        {
            return new MemoryStreamWrap(capacity);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the byte buffer and the write state.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer.
        /// </param>
        /// <param name="writable">
        ///     The writable.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(byte[] buffer,
                                    bool writable)
        {
            return new MemoryStreamWrap(buffer, writable);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the byte buffer, the index and the count.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer.
        /// </param>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <param name="count">
        ///     The count.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(byte[] buffer,
                                    int index,
                                    int count)
        {
            return new MemoryStreamWrap(buffer, index, count);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the byte buffer, the index, the count and the write state.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer.
        /// </param>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <param name="count">
        ///     The count.
        /// </param>
        /// <param name="writable">
        ///     The writable.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(byte[] buffer,
                                    int index,
                                    int count,
                                    bool writable)
        {
            return new MemoryStreamWrap(buffer, index, count, writable);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the byte buffer, the index, the count, the write state and 
        ///     the visibility.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer.
        /// </param>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <param name="count">
        ///     The count.
        /// </param>
        /// <param name="writable">
        ///     The writable.
        /// </param>
        /// <param name="publiclyVisible">
        ///     The publicly visible.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        public IMemoryStream Create(byte[] buffer,
                                    int index,
                                    int count,
                                    bool writable,
                                    bool publiclyVisible)
        {
            return new MemoryStreamWrap(buffer, index, count, writable, publiclyVisible);
        }

        #endregion
    }
}