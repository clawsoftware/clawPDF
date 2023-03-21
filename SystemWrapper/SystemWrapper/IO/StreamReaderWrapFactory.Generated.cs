#pragma warning disable 1591

namespace SystemWrapper.IO
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    using SystemInterface.Attributes;
    using SystemInterface.IO;

    /// <summary>
    /// The implementation for the factory generating <see cref="StreamReaderWrap" /> instances.
    /// </summary>
    public partial class StreamReaderWrapFactory : IStreamReaderFactory
    {
        #region Public Factory Methods

        public IStreamReader Create(System.IO.TextReader textReader)
        {
            return new StreamReaderWrap(textReader);
        }

        public IStreamReader Create(System.IO.StreamReader streamReader)
        {
            return new StreamReaderWrap(streamReader);
        }

        public IStreamReader Create(System.IO.Stream stream)
        {
            return new StreamReaderWrap(stream);
        }

        public IStreamReader Create(IStream stream)
        {
            return new StreamReaderWrap(stream);
        }

        public IStreamReader Create(string path)
        {
            return new StreamReaderWrap(path);
        }

        public IStreamReader Create(
            System.IO.Stream stream,
            bool detectEncodingFromByteOrderMarks)
        {
            return new StreamReaderWrap(
                stream, 
                detectEncodingFromByteOrderMarks);
        }

        public IStreamReader Create(
            System.IO.Stream stream,
            System.Text.Encoding encoding)
        {
            return new StreamReaderWrap(
                stream, 
                encoding);
        }

        public IStreamReader Create(
            string path,
            bool detectEncodingFromByteOrderMarks)
        {
            return new StreamReaderWrap(
                path, 
                detectEncodingFromByteOrderMarks);
        }

        public IStreamReader Create(
            string path,
            System.Text.Encoding encoding)
        {
            return new StreamReaderWrap(
                path, 
                encoding);
        }

        public IStreamReader Create(
            System.IO.Stream stream,
            System.Text.Encoding encoding,
            bool detectEncodingFromByteOrderMarks)
        {
            return new StreamReaderWrap(
                stream, 
                encoding, 
                detectEncodingFromByteOrderMarks);
        }

        public IStreamReader Create(
            string path,
            System.Text.Encoding encoding,
            bool detectEncodingFromByteOrderMarks)
        {
            return new StreamReaderWrap(
                path, 
                encoding, 
                detectEncodingFromByteOrderMarks);
        }

        public IStreamReader Create(
            System.IO.Stream stream,
            System.Text.Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize)
        {
            return new StreamReaderWrap(
                stream, 
                encoding, 
                detectEncodingFromByteOrderMarks, 
                bufferSize);
        }

        public IStreamReader Create(
            string path,
            System.Text.Encoding encoding,
            bool detectEncodingFromByteOrderMarks,
            int bufferSize)
        {
            return new StreamReaderWrap(
                path, 
                encoding, 
                detectEncodingFromByteOrderMarks, 
                bufferSize);
        }

        #endregion
    }
}