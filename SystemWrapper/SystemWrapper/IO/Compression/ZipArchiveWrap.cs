using System;
using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Linq;
using SystemInterface.IO.Compression;

namespace SystemWrapper.IO.Compression
{
    public class ZipArchiveWrap : IZipArchive
    {
        public ZipArchive Instance { get; private set; }

        public ZipArchiveWrap(ZipArchive instance)
        {
            this.Instance = instance;
        }

        public ReadOnlyCollection<IZipArchiveEntry> Entries
        {
            get
            {
                return new ReadOnlyCollection<IZipArchiveEntry>(Instance.Entries.Select(s => new ZipArchiveEntryWrap(s)).Cast<IZipArchiveEntry>().ToList());
            }
        }

        public ZipArchiveMode Mode
        {
            get
            {
                return Instance.Mode;
            }
        }

        public IZipArchiveEntry CreateEntry(string entryName)
        {
            return new ZipArchiveEntryWrap(Instance.CreateEntry(entryName));
        }

        public IZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
        {
            return new ZipArchiveEntryWrap(Instance.CreateEntry(entryName, compressionLevel));
        }

        public IZipArchiveEntry GetEntry(string entryName)
        {
            return new ZipArchiveEntryWrap(Instance.GetEntry(entryName));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            Instance.Dispose();
        }
    }
}
