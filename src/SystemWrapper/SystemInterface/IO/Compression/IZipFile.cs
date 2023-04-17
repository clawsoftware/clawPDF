using System.IO.Compression;
using System.Text;

namespace SystemInterface.IO.Compression
{
    public interface IZipFile : IStaticWrapper
    {
        void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName);

        void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory);

        void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding);

        void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName);

        void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, Encoding entryNameEncoding);

        IZipArchive Open(string archiveFileName, ZipArchiveMode mode);

        IZipArchive Open(string archiveFileName, ZipArchiveMode mode, Encoding entryNameEncoding);

        IZipArchive OpenRead(string archiveFileName);
    }
}
