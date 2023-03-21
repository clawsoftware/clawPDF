using System.Text;

namespace SystemInterface.IO
{
    public interface IStreamWriterFactory
    {
        IStreamWriter Create(IStream stream);
        IStreamWriter Create(IStream stream, Encoding encoding);
        IStreamWriter Create(IStream stream, Encoding encoding, int bufferSize);
        IStreamWriter Create(IStreamWriter streamWriter);
        IStreamWriter Create(string path);
        IStreamWriter Create(string path, bool append);
        IStreamWriter Create(string path, bool append, Encoding encoding);
        IStreamWriter Create(string path, bool append, Encoding encoding, int bufferSize);
    }
}
