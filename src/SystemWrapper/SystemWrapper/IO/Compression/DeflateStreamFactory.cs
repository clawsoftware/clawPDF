using System.IO.Compression;
using SystemInterface.IO;
using SystemInterface.IO.Compression;

namespace SystemWrapper.IO.Compression
{
    public class DeflateStreamFactory : IDeflateStreamFactory
    {
        public IDeflateStream Create(IMemoryStream memoryStream, CompressionMode compressionMode)
        {
            return new DeflateStreamWrap(memoryStream, compressionMode);
        }
    }
}
