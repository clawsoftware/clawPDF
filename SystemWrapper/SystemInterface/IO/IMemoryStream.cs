using System.IO;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.MemoryStream"/> class.
    /// </summary>
    public interface IMemoryStream : IStream
    {
        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized to zero.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized to zero.
        /// </summary>
        void Initialize(Stream stream);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.MemoryStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="memoryStream">A <see cref="T:System.IO.MemoryStream"/> object.</param>
        void Initialize(MemoryStream memoryStream);

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified byte array.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create the current stream. </param>
        void Initialize(byte[] buffer);

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class with an expandable capacity initialized as specified.
        /// </summary>
        /// <param name="capacity"></param>
        void Initialize(int capacity);

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified byte array with the CanWrite property set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        void Initialize(byte[] buffer, bool writable);

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified region (index) of a byte array.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes. </param>
        void Initialize(byte[] buffer, int index, int count);

        /// <summary>
        /// Initializes a new non-resizable instance of the MemoryStreamWrap class based on the specified region of a byte array, with the CanWrite property set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes. </param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        void Initialize(byte[] buffer, int index, int count, bool writable);

        /// <summary>
        /// Initializes a new instance of the MemoryStreamWrap class based on the specified region of a byte array, with the CanWrite property set as specified, and the ability to call GetBuffer set as specified.
        /// </summary>
        /// <param name="buffer">The array of unsigned bytes from which to create this stream. </param>
        /// <param name="index">The index into buffer at which the stream begins.</param>
        /// <param name="count">The length of the stream in bytes.</param>
        /// <param name="writable">The setting of the CanWrite property, which determines whether the stream supports writing. </param>
        /// <param name="publiclyVisible"> <c>true</c> to enable GetBuffer, which returns the unsigned byte array from which the stream was created; otherwise, <c>false</c>. </param>
        void Initialize(byte[] buffer, int index, int count, bool writable, bool publiclyVisible);

        // Properties

        /// <summary>
        /// Gets or sets the number of bytes allocated for this stream.
        /// </summary>
        int Capacity { get; set; }

        /// <summary>
        /// Gets <see cref="T:System.IO.MemoryStream"/> object.
        /// </summary>
        MemoryStream MemoryStreamInstance { get; }

        // Methods

        /// <summary>
        /// Returns the array of unsigned bytes from which this stream was created.
        /// </summary>
        /// <returns>The byte array from which this stream was created, or the underlying array if a byte array was not provided to the MemoryStream constructor during construction of the current instance.</returns>
        byte[] GetBuffer();

        /// <summary>
        /// Writes the stream contents to a byte array, regardless of the Position property.
        /// </summary>
        /// <returns>A new byte array.</returns>
        byte[] ToArray();

        /// <summary>
        /// Writes the entire contents of this memory stream to another stream.
        /// </summary>
        /// <param name="stream">The stream to write this memory stream to.</param>
        void WriteTo(IStream stream);
    }
}
