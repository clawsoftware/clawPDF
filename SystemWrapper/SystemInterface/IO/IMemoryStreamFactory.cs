namespace SystemInterface.IO
{
    using System.IO;

    /// <summary>
    ///     Factory that creates a new <see cref="IMemoryStreamFactory"/> instance.
    /// </summary>
    public interface IMemoryStreamFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class.
        /// </summary>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        IMemoryStream Create();

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        IMemoryStream Create(Stream stream);

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the memory stream.
        /// </summary>
        /// <param name="memoryStream">
        ///     The memory stream.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        IMemoryStream Create(MemoryStream memoryStream);

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the byte buffer.
        /// </summary>
        /// <param name="buffer">
        ///     The buffer.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        IMemoryStream Create(byte[] buffer);

        /// <summary>
        ///     Creates a new instance of the <see cref='IMemoryStream'/> class passing the capacity.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <returns>
        ///     The <see cref="IMemoryStream"/>.
        /// </returns>
        IMemoryStream Create(int capacity);

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
        IMemoryStream Create(byte[] buffer,
                             bool writable);

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
        IMemoryStream Create(byte[] buffer,
                             int index,
                             int count);

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
        IMemoryStream Create(byte[] buffer,
                             int index,
                             int count,
                             bool writable);

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
        IMemoryStream Create(byte[] buffer,
                             int index,
                             int count,
                             bool writable,
                             bool publiclyVisible);

        #endregion
    }
}