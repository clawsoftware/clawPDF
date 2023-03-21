using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using SystemInterface.IO;

namespace SystemWrapper.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.BinaryWriter"/> class.
    /// </summary>
    [Serializable, ComVisible(true)]
    public class BinaryWriterWrap : IBinaryWriter
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.BinaryWriterWrap"/> class on the specified path.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.IO.BinaryWriter"/> object.</param>
        public BinaryWriterWrap(BinaryWriter writer)
        {
            Initialize(writer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.BinaryWriterWrap"/> class on the specified path.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.IO.BinaryWriter"/> object.</param>
        public void Initialize(BinaryWriter writer)
        {
            BinaryWriterInstance = writer;
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public BinaryWriterWrap(Stream output)
        {
            Initialize(output);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public void Initialize(Stream output)
        {
            BinaryWriterInstance = new BinaryWriter(output);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public BinaryWriterWrap(IStream output)
        {
            Initialize(output.StreamInstance);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and using UTF-8 as the encoding for strings.
        /// </summary>
        /// <param name="output">The output stream.</param>
        public void Initialize(IStream output)
        {
            BinaryWriterInstance = new BinaryWriter(output.StreamInstance);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryWriterWrap(Stream output, Encoding encoding)
        {
            Initialize(output, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public void Initialize(Stream output, Encoding encoding)
        {
            BinaryWriterInstance = new BinaryWriter(output, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryWriterWrap(IStream output, Encoding encoding)
        {
            Initialize(output.StreamInstance, encoding);
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriterWrap class based on the supplied stream and a specific character encoding.
        /// </summary>
        /// <param name="output">The supplied stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public void Initialize(IStream output, Encoding encoding)
        {
            BinaryWriterInstance = new BinaryWriter(output.StreamInstance, encoding);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public Stream BaseStream
        {
            get { return BinaryWriterInstance.BaseStream; }
        }

        /// <inheritdoc />
        public BinaryWriter BinaryWriterInstance { get; private set; }

        /// <inheritdoc />
        public void Close()
        {
            BinaryWriterInstance.Close();
        }

        /// <inheritdoc />
        public void Flush()
        {
            BinaryWriterInstance.Flush();
        }

        /// <inheritdoc />
        public long Seek(int offset, SeekOrigin origin)
        {
            return BinaryWriterInstance.Seek(offset, origin);
        }

        /// <inheritdoc />
        public void Write(bool value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(byte value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(byte[] buffer)
        {
            BinaryWriterInstance.Write(buffer);
        }

        /// <inheritdoc />
        public void Write(char ch)
        {
            BinaryWriterInstance.Write(ch);
        }

        /// <inheritdoc />
        public void Write(char[] chars)
        {
            BinaryWriterInstance.Write(chars);
        }

        /// <inheritdoc />
        public void Write(decimal value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(double value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(short value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(int value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(long value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(sbyte value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(float value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(string value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(ushort value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(uint value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(ulong value)
        {
            BinaryWriterInstance.Write(value);
        }

        /// <inheritdoc />
        public void Write(byte[] buffer, int index, int count)
        {
            BinaryWriterInstance.Write(buffer, index, count);
        }

        /// <inheritdoc />
        public void Write(char[] chars, int index, int count)
        {
            BinaryWriterInstance.Write(chars, index, count);
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
            BinaryWriterInstance.Close();
        }
    }
}
