using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SystemInterface.IO;

namespace SystemWrapper.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.BinaryReader"/> class.
    /// </summary>
    [ComVisible(true)]
    public class BinaryReaderWrap : IBinaryReader
    {
        #region Constructors and Initializers

        /// <summary>
        /// Creates an uninitialized instance of the <see cref="T:SystemWrapper.IO.BinaryReaderWrap"/> class on the specified path.
        /// </summary>
        public BinaryReaderWrap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.BinaryReaderWrap"/> class on the specified path.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.IO.BinaryReader"/> object.</param>
        public BinaryReaderWrap(BinaryReader reader)
        {
            Initialize(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.BinaryReaderWrap"/> class on the specified path.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.IO.BinaryReader"/> object.</param>
        public void Initialize(BinaryReader reader)
        {
            BinaryReaderInstance = reader;
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and using UTF8Encoding.
        /// </summary>
        /// <param name="input">A <see cref="T:System.IO.Stream"/> object.</param>
        public BinaryReaderWrap(Stream input)
        {
            Initialize(input);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and using UTF8Encoding.
        /// </summary>
        /// <param name="input">A <see cref="T:System.IO.Stream"/> object.</param>
        public void Initialize(Stream input)
        {
            BinaryReaderInstance = new BinaryReader(input);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and using UTF8Encoding.
        /// </summary>
        /// <param name="input">A <see cref="T:System.IO.Stream"/> object.</param>
        public BinaryReaderWrap(IStream input)
        {
            Initialize(input.StreamInstance);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and using UTF8Encoding.
        /// </summary>
        /// <param name="input">A <see cref="T:System.IO.Stream"/> object.</param>
        public void Initialize(IStream input)
        {
            BinaryReaderInstance = new BinaryReader(input.StreamInstance);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="stream">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryReaderWrap(Stream stream, Encoding encoding)
        {
            Initialize(stream, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="stream">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public void Initialize(Stream stream, Encoding encoding)
        {
            BinaryReaderInstance = new BinaryReader(stream, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="stream">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryReaderWrap(IStream stream, Encoding encoding)
        {
            Initialize(stream.StreamInstance, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryReader class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="stream">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public void Initialize(IStream stream, Encoding encoding)
        {
            BinaryReaderInstance = new BinaryReader(stream.StreamInstance, encoding);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public Stream BaseStream
        {
            get { return BinaryReaderInstance.BaseStream; }
        }

        /// <inheritdoc />
        public BinaryReader BinaryReaderInstance { get; private set; }

        /// <inheritdoc />
        public virtual void Close()
        {
            BinaryReaderInstance.Close();
        }

        /// <inheritdoc />
        public char[] ReadChars(int count)
        {
            return BinaryReaderInstance.ReadChars(count);
        }

        /// <inheritdoc />
        public decimal ReadDecimal()
        {
            return BinaryReaderInstance.ReadDecimal();
        }

        /// <inheritdoc />
        public double ReadDouble()
        {
            return BinaryReaderInstance.ReadDouble();
        }

        /// <inheritdoc />
        public short ReadInt16()
        {
            return BinaryReaderInstance.ReadInt16();
        }

        /// <inheritdoc />
        public int ReadInt32()
        {
            return BinaryReaderInstance.ReadInt32();
        }

        /// <inheritdoc />
        public long ReadInt64()
        {
            return BinaryReaderInstance.ReadInt64();
        }

        /// <inheritdoc />
        public char ReadChar()
        {
            return BinaryReaderInstance.ReadChar();
        }

        /// <inheritdoc />
        public int PeekChar()
        {
            return BinaryReaderInstance.PeekChar();
        }

        /// <inheritdoc />
        public int Read()
        {
            return BinaryReaderInstance.Read();
        }

        /// <inheritdoc />
        public int Read(byte[] buffer, int index, int count)
        {
            return BinaryReaderInstance.Read(buffer, index, count);
        }

        /// <inheritdoc />
        public int Read(char[] buffer, int index, int count)
        {
            return BinaryReaderInstance.Read(buffer, index, count);
        }

        /// <inheritdoc />
        public bool ReadBoolean()
        {
            return BinaryReaderInstance.ReadBoolean();
        }

        /// <inheritdoc />
        public byte ReadByte()
        {
            return BinaryReaderInstance.ReadByte();
        }

        /// <inheritdoc />
        public virtual byte[] ReadBytes(int count)
        {
            return BinaryReaderInstance.ReadBytes(count);
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        public sbyte ReadSByte()
        {
            return BinaryReaderInstance.ReadSByte();
        }

        /// <inheritdoc />
        public float ReadSingle()
        {
            return BinaryReaderInstance.ReadSingle();
        }

        /// <inheritdoc />
        public string ReadString()
        {
            return BinaryReaderInstance.ReadString();
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        public ushort ReadUInt16()
        {
            return BinaryReaderInstance.ReadUInt16();
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        public uint ReadUInt32()
        {
            return BinaryReaderInstance.ReadUInt32();
        }

        /// <inheritdoc />
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Valid method name from .NET API.")]
        public ulong ReadUInt64()
        {
            return BinaryReaderInstance.ReadUInt64();
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
            BinaryReaderInstance.Close();
        }
    }
}
