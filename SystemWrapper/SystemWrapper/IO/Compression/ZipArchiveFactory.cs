using System.IO.Compression;
using System.Text;
using SystemInterface.IO;
using SystemInterface.IO.Compression;

namespace SystemWrapper.IO.Compression
{
    public class ZipArchiveFactory : IZipArchiveFactory
    {
        public IZipArchive Create(IStream stream, ZipArchiveMode zipArchiveMode)
        {
            return new ZipArchiveWrap(new ZipArchive(stream.StreamInstance, zipArchiveMode));
        }

        public IZipArchive Create(IStream stream, ZipArchiveMode zipArchiveMode, bool leaveOpen)
        {
            return new ZipArchiveWrap(new ZipArchive(stream.StreamInstance, zipArchiveMode, leaveOpen));
        }

        public IZipArchive Create(IStream stream, ZipArchiveMode zipArchiveMode, bool leaveOpen, Encoding encoding)
        {
            return new ZipArchiveWrap(new ZipArchive(stream.StreamInstance, zipArchiveMode, leaveOpen, encoding));
        }
    }
}
