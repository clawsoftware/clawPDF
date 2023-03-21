using System.IO;
using System.Text;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.StreamReader"/> class.
    /// </summary>
    public interface IStreamReader : ITextReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.StreamReaderWrap"/> class on the specified path.
        /// </summary>
        /// <param name="textReader">A <see cref="T:System.IO.TextReader"/> object.</param>
        void Initialize(TextReader textReader);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.StreamReaderWrap"/> class on the specified path.
        /// </summary>
        /// <param name="streamReader">A <see cref="T:System.IO.StreamReader"/> object.</param>
        void Initialize(StreamReader streamReader);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.IO.StreamReader"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        void Initialize(Stream stream);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.IO.StreamReader"/> class for the specified stream.
        /// </summary>
        /// <param name="stream">The stream wrapper to write to.</param>
        void Initialize(IStream stream);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified file name.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        void Initialize(string path);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified stream, with the specified byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read. </param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        void Initialize(Stream stream, bool detectEncodingFromByteOrderMarks);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified stream, with the specified character encoding.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        void Initialize(Stream stream, Encoding encoding);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified file name, with the specified byte order mark detection option.
        /// </summary>
        /// <param name="path">The complete file path to be read. </param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        void Initialize(string path, bool detectEncodingFromByteOrderMarks);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified file name, with the specified character encoding.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        /// <param name="encoding">The character encoding to use.</param>
        void Initialize(string path, Encoding encoding);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified stream, with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="stream">The stream to be read. </param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        void Initialize(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified file name, with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        void Initialize(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified stream, with the specified character encoding and byte order mark detection option, and buffer size.
        /// </summary>
        /// <param name="stream">The stream to be read. </param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        /// <param name="bufferSize">The minimum buffer size. </param>
        void Initialize(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize);

        /// <summary>
        /// Initializes a new instance of the StreamReader class for the specified file name, with the specified character encoding and byte order mark detection option.
        /// </summary>
        /// <param name="path">The complete file path to be read.</param>
        /// <param name="encoding">The character encoding to use. </param>
        /// <param name="detectEncodingFromByteOrderMarks">Indicates whether to look for byte order marks at the beginning of the file.</param>
        /// <param name="bufferSize">The minimum buffer size, in number of 16-bit characters.</param>
        void Initialize(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize);

        // Properties

        /// <summary>
        /// Returns the underlying stream.
        /// </summary>
        /// <value>The underlying stream.</value>
        Stream BaseStream { get; }

        /// <summary>
        /// Gets the current character encoding that the current IStreamReaderWrap object is using.
        /// </summary>
        /// <value>The current character encoding used by the current reader. The value can be different after the first call to any Read method of IStreamReaderWrap, since encoding autodetection is not done until the first call to a Read method. </value>
        Encoding CurrentEncoding { get; }

        /// <summary>
        /// Gets a value that indicates whether the current stream position is at the end of the stream.
        /// </summary>
        /// <value> <c>true</c> if the current stream position is at the end of the stream; otherwise <c>false</c>. </value>
        bool EndOfStream { get; }

        /// <summary>
        /// Gets <see cref="T:System.IO.StreamReader"/> object.
        /// </summary>
        StreamReader StreamReaderInstance { get; }

        // Methods

        /// <summary>
        /// Allows a IStreamReaderWrap object to discard its current data.
        /// </summary>
        void DiscardBufferedData();
    }
}
