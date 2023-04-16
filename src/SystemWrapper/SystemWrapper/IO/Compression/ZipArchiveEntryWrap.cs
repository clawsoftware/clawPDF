using System;
using System.IO.Compression;
using SystemInterface.IO;
using SystemInterface.IO.Compression;

namespace SystemWrapper.IO.Compression
{
    public class ZipArchiveEntryWrap : IZipArchiveEntry
    {
        public ZipArchiveEntryWrap(ZipArchiveEntry instance)
        {
            this.Instance = instance;
        }

        public ZipArchiveEntry Instance { get; private set; }

        public IZipArchive Archive
        {
            get
            {
                return new ZipArchiveWrap(Instance.Archive);
            }
        }

        public long CompressedLength
        {
            get
            {
                return Instance.CompressedLength;
            }
        }

        public string FullName
        {
            get
            {
                return Instance.FullName;
            }
        }

        public DateTimeOffset LastWriteTime
        {
            get
            {
                return Instance.LastWriteTime;
            }

            set
            {
                Instance.LastWriteTime = value;
            }
        }

        public long Length
        {
            get
            {
                return Instance.Length;
            }
        }

        public string Name
        {
            get
            {
                return Instance.Name;
            }
        }

        public void Delete()
        {
            Instance.Delete();
        }

        public IStream Open()
        {
            return new StreamWrap(Instance.Open());
        }

        public override string ToString()
        {
            return Instance.ToString();
        }
    }
}
