using System.IO.Compression;

namespace SystemInterface.IO.Compression
{
    /// <summary>
    /// Description of IDeflateStreamWrap.
    /// </summary>
    public interface IDeflateStream : IStream
    {
        /// <summary>
        /// Initializes a new instance of the DeflateStream class using the specified stream and CompressionMode value, and a value that specifies whether to leave the stream open.
        /// </summary>
        /// <param name="stream">The stream to compress or decompress.</param>
        /// <param name="mode">One of the CompressionMode values that indicates the action to take.</param>
        void Initialize(IStream stream, CompressionMode mode);

        /// <summary>
        /// DeflateStream object.
        /// </summary>
        DeflateStream DeflateStreamInstance { get; }
    }
}
