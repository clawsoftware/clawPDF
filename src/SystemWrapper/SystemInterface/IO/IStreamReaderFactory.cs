namespace SystemInterface.IO
{
    using System.IO;
    using System.Text;

    /// <summary>
    ///     Factory that creates a new <see cref="IStreamReader"/> instance.
    /// </summary>
    public interface IStreamReaderFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new instance of the <see cref="IStreamReader"/> class on the specified path.
        /// </summary>
        /// <param name="textReader">
        ///     A <see cref="T:System.IO.TextReader"/> object.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(TextReader textReader);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class on the specified path.
        /// </summary>
        /// <param name="streamReader">
        ///     A <see cref="T:System.IO.StreamReader"/> object.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(StreamReader streamReader);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream to write to.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(Stream stream);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream to write to.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(IStream stream);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified stream.
        /// </summary>
        /// <param name="path">
        ///     The complete file path to be read.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(string path);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified stream, with the specified byte order mark detection option.
        /// </summary>
        /// <param name="stream">
        ///     The stream to be read.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(Stream stream,
                             bool detectEncodingFromByteOrderMarks);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified stream, with the specified character encoding.
        /// </summary>
        /// <param name="stream">
        ///     The stream to be read.
        /// </param>
        /// <param name="encoding">
        ///     The character encoding to use.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(Stream stream,
                             Encoding encoding);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified file name, with the specified byte order mark 
        ///     detection option.
        /// </summary>
        /// <param name="path">
        ///     The complete file path to be read.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(string path,
                             bool detectEncodingFromByteOrderMarks);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified file name, with the specified character encoding.
        /// </summary>
        /// <param name="path">
        ///     The complete file path to be read.
        /// </param>
        /// <param name="encoding">
        ///     The character encoding to use.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(string path,
                             Encoding encoding);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified stream, with the specified character encoding 
        ///     and byte order mark detection option.
        /// </summary>
        /// <param name="stream">
        ///     The stream to be read.
        /// </param>
        /// <param name="encoding">
        ///     The character encoding to use.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(Stream stream,
                             Encoding encoding,
                             bool detectEncodingFromByteOrderMarks);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified file name, with the specified character encoding 
        ///     and byte order mark detection option. 
        /// </summary>
        /// <param name="path">
        ///     The complete file path to be read.
        /// </param>
        /// <param name="encoding">
        ///     The character encoding to use.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(string path,
                             Encoding encoding,
                             bool detectEncodingFromByteOrderMarks);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified stream, with the specified character encoding and 
        ///     byte order mark detection option, and buffer size.
        /// </summary>
        /// <param name="stream">
        ///     The stream to be read.
        /// </param>
        /// <param name="encoding">
        ///     The character encoding to use.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <param name="bufferSize">
        ///     The minimum buffer size.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(Stream stream,
                             Encoding encoding,
                             bool detectEncodingFromByteOrderMarks,
                             int bufferSize);

        /// <summary>
        ///     Creates a new instance of the <see cref='IStreamReader'/> class for the specified file name, with the specified character encoding and 
        ///     byte order mark detection option. 
        /// </summary>
        /// <param name="path">
        ///     The complete file path to be read.
        /// </param>
        /// <param name="encoding">
        ///     The character encoding to use.
        /// </param>
        /// <param name="detectEncodingFromByteOrderMarks">
        ///     Indicates whether to look for byte order marks at the beginning of the file.
        /// </param>
        /// <param name="bufferSize">
        ///     The minimum buffer size, in number of 16-bit characters.
        /// </param>
        /// <returns>
        ///     The <see cref="IStreamReader"/>.
        /// </returns>
        IStreamReader Create(string path,
                             Encoding encoding,
                             bool detectEncodingFromByteOrderMarks,
                             int bufferSize);

        #endregion
    }
}