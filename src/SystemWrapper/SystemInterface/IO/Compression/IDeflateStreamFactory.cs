using System.IO.Compression;
using System.Runtime.Remoting.Messaging;

namespace SystemInterface.IO.Compression
{
    public interface IDeflateStreamFactory
    {
        IDeflateStream Create(IMemoryStream memoryStream, CompressionMode compressionMode);
    }
}
