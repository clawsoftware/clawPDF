using System.Text;
using SystemInterface.IO;

namespace SystemWrapper.IO
{
    public class StreamWriterFactory : IStreamWriterFactory
    {
        public IStreamWriter Create(IStream stream)
        {
            return new StreamWriterWrap(stream.StreamInstance);
        }

        public IStreamWriter Create(IStream stream, Encoding encoding)
        {
            return new StreamWriterWrap(stream.StreamInstance, encoding);
        }

        public IStreamWriter Create(IStream stream, Encoding encoding, int bufferSize)
        {
            return new StreamWriterWrap(stream.StreamInstance, encoding, bufferSize);
        }

        public IStreamWriter Create(IStreamWriter streamWriter)
        {
            return new StreamWriterWrap(streamWriter.StreamWriterInstance);
        }

        public IStreamWriter Create(string path)
        {
            return new StreamWriterWrap(path);
        }

        public IStreamWriter Create(string path, bool append)
        {
            return new StreamWriterWrap(path, append);
        }

        public IStreamWriter Create(string path, bool append, Encoding encoding)
        {
            return new StreamWriterWrap(path, append, encoding);
        }

        public IStreamWriter Create(string path, bool append, Encoding encoding, int bufferSize)
        {
            return new StreamWriterWrap(path, append, encoding, bufferSize);
        }
    }
}
