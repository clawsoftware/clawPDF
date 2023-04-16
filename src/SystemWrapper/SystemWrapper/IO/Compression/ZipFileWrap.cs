using System.IO.Compression;
using System.Text;
using SystemInterface.IO.Compression;

namespace SystemWrapper.IO.Compression
{
    public class ZipFileWrap : IZipFile
    {
        public void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }

        public void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory);
        }

        public void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory, Encoding entryNameEncoding)
        {
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName, compressionLevel, includeBaseDirectory, entryNameEncoding);
        }

        public void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
        }

        public void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, Encoding entryNameEncoding)
        {
            ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName, entryNameEncoding);
        }

        public IZipArchive Open(string archiveFileName, System.IO.Compression.ZipArchiveMode mode)
        {
            return new ZipArchiveWrap(ZipFile.Open(archiveFileName, mode));
        }

        public IZipArchive Open(string archiveFileName, System.IO.Compression.ZipArchiveMode mode, Encoding entryNameEncoding)
        {
            return new ZipArchiveWrap(ZipFile.Open(archiveFileName, mode, entryNameEncoding));
        }

        public IZipArchive OpenRead(string archiveFileName)
        {
            return new ZipArchiveWrap(ZipFile.OpenRead(archiveFileName));
        }
    }
}
