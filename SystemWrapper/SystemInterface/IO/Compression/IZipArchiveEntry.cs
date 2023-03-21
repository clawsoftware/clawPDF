using System;
using System.IO.Compression;

namespace SystemInterface.IO.Compression
{
    public interface IZipArchiveEntry : IWrapper<ZipArchiveEntry>
    {
        IZipArchive Archive { get; }

        long CompressedLength { get; }

        string FullName { get; }

        DateTimeOffset LastWriteTime { get; set; }

        long Length { get; }

        string Name { get; }

        void Delete();

        IStream Open();
    }
}
