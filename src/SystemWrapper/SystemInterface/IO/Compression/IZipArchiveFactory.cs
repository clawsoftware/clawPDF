using System.IO.Compression;
using System.Text;

namespace SystemInterface.IO.Compression
{
    public interface IZipArchiveFactory
    {
        IZipArchive Create(IStream stream, ZipArchiveMode zipArchiveMode);
        IZipArchive Create(IStream stream, ZipArchiveMode zipArchiveMode, bool leaveOpen);
        IZipArchive Create(IStream stream, ZipArchiveMode zipArchiveMode, bool leaveOpen, Encoding encoding);
    }
}
