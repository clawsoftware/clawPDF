using System;
using System.Collections.ObjectModel;
using System.IO.Compression;

namespace SystemInterface.IO.Compression
{
    public interface IZipArchive : IDisposable, IWrapper<ZipArchive>
    {
        ReadOnlyCollection<IZipArchiveEntry> Entries { get; }

        ZipArchiveMode Mode { get; }

        IZipArchiveEntry CreateEntry(string entryName);

        IZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel);

        IZipArchiveEntry GetEntry(string entryName);
    }
}
