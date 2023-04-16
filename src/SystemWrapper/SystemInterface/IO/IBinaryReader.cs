using System;
using System.IO;
using System.Text;

namespace SystemInterface.IO
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Wrapper for <see cref="T:System.IO.BinaryReader"/> class.
    /// </summary>
    [CLSCompliant(false)]
    public interface IBinaryReader : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.BinaryReaderWrap"/> class on the specified path.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.IO.BinaryReader"/> object.</param>
        void Initialize(BinaryReader reader);

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and using UTF8Encoding.
        /// </summary>
        /// <param name="input">A <see cref="T:System.IO.Stream"/> object.</param>
        void Initialize(Stream input);

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and using UTF8Encoding.
        /// </summary>
        /// <param name="input">A <see cref="T:System.IO.Stream"/> object.</param>
        void Initialize(IStream input);

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="stream">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        void Initialize(Stream stream, Encoding encoding);

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="stream">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        void Initialize(IStream stream, Encoding encoding);

        // Properties

        /// <summary>
        /// Gets <see cref="T:System.IO.BinaryReader"/> object.
        /// </summary>
        BinaryReader BinaryReaderInstance { get; }

        /// <summary>
        /// Exposes access to the underlying stream of the BinaryReader.
        /// </summary>
        Stream BaseStream { get; }

        // Methods

        /// <summary>
        /// Closes the current reader and the underlying stream.
        /// </summary>
        void Close();

        /// <summary>
        /// Returns the next available character and does not advance the byte or character position.
        /// </summary>
        /// <returns>The next available character, or -1 if no more characters are available or the stream does not support seeking. </returns>
        int PeekChar();

        /// <summary>
        /// Reads characters from the underlying stream and advances the current position of the stream in accordance with the Encoding used and the specific character being read from the stream.
        /// </summary>
        /// <returns>The next character from the input stream, or -1 if no characters are currently available.</returns>
        int Read();

        /// <summary>
        /// Reads count bytes from the stream with index as the starting point in the byte array.
        /// </summary>
        /// <param name="buffer">The buffer to read data into.</param>
        /// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
        /// <param name="count">The number of characters to read.</param>
        /// <returns></returns>
        int Read(byte[] buffer, int index, int count);

        /// <summary>
        /// Reads count characters from the stream with index as the starting point in the character array.
        /// </summary>
        /// <param name="buffer">The buffer to read data into.</param>
        /// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
        /// <param name="count">The number of characters to read.</param>
        /// <returns>The total number of characters read into the buffer. This might be less than the number of characters requested if that many characters are not currently available, or it might be zero if the end of the stream is reached.</returns>
        int Read(char[] buffer, int index, int count);

        /// <summary>
        /// Reads a Boolean value from the current stream and advances the current position of the stream by one byte.
        /// </summary>
        /// <returns> <c>true</c> if the byte is nonzero; otherwise, <c>false</c>.</returns>
        bool ReadBoolean();

        /// <summary>
        /// Reads the next byte from the current stream and advances the current position of the stream by one byte.
        /// </summary>
        /// <returns>The next byte read from the current stream.</returns>
        byte ReadByte();

        /// <summary>
        /// Reads count bytes from the current stream into a byte array and advances the current position by count bytes.
        /// </summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A byte array containing data read from the underlying stream. This might be less than the number of bytes requested if the end of the stream is reached.</returns>
        byte[] ReadBytes(int count);

        /// <summary>
        /// Reads the next character from the current stream and advances the current position of the stream in accordance with the Encoding used and the specific character being read from the stream.
        /// </summary>
        /// <returns>A character read from the current stream.</returns>
        char ReadChar();

        /// <summary>
        /// Reads count characters from the current stream, returns the data in a character array, and advances the current position in accordance with the Encoding used and the specific character being read from the stream.
        /// </summary>
        /// <param name="count">The number of characters to read.</param>
        /// <returns>A character array containing data read from the underlying stream. This might be less than the number of characters requested if the end of the stream is reached.</returns>
        char[] ReadChars(int count);

        /// <summary>
        /// Reads a decimal value from the current stream and advances the current position of the stream by sixteen bytes.
        /// </summary>
        /// <returns>A decimal value read from the current stream.</returns>
        decimal ReadDecimal();

        /// <summary>
        /// Reads an 8-byte floating point value from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <returns>An 8-byte floating point value read from the current stream.</returns>
        double ReadDouble();

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        short ReadInt16();

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte signed integer read from the current stream. </returns>
        int ReadInt32();

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes.
        /// </summary>
        /// <returns>An 8-byte signed integer read from the current stream.</returns>
        long ReadInt64();

        /// <summary>
        /// Reads a signed byte from this stream and advances the current position of the stream by one byte.
        /// </summary>
        /// <returns>A signed byte read from the current stream.</returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        sbyte ReadSByte();

        /// <summary>
        /// Reads a 4-byte floating point value from the current stream and advances the current position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte floating point value read from the current stream.</returns>
        float ReadSingle();

        /// <summary>
        /// Reads a string from the current stream. The string is prefixed with the length, encoded as an integer seven bits at a time.
        /// </summary>
        /// <returns>The string being read.</returns>
        string ReadString();

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using little-endian encoding and advances the position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte unsigned integer read from this stream.</returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        ushort ReadUInt16();

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from this stream.</returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        uint ReadUInt32();

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the position of the stream by eight bytes.
        /// </summary>
        /// <returns>An 8-byte unsigned integer read from this stream.</returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        ulong ReadUInt64();
    }
}
