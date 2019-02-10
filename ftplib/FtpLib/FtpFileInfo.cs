using System;
using System.IO;

namespace clawSoft.clawPDF.ftplib.FtpLib
{
    public class FtpFileInfo : FileSystemInfo
    {
        private DateTime? _creationTime;

        private string _filePath;

        private DateTime? _lastAccessTime;

        private DateTime? _lastWriteTime;

        public FtpFileInfo(FtpConnection ftp, string filePath)
        {
            if (filePath == null) throw new ArgumentNullException("fileName");
            OriginalPath = filePath;
            FullPath = filePath;
            _filePath = filePath;
            FtpConnection = ftp;
            Name = Path.GetFileName(filePath);
        }

        public FtpConnection FtpConnection { get; }

        public new DateTime? LastAccessTime
        {
            get
            {
                if (!_lastAccessTime.HasValue) return null;
                return _lastAccessTime.Value;
            }
            internal set => _lastAccessTime = value;
        }

        public new DateTime? CreationTime
        {
            get
            {
                if (!_creationTime.HasValue) return null;
                return _creationTime.Value;
            }
            internal set => _creationTime = value;
        }

        public new DateTime? LastWriteTime
        {
            get
            {
                if (!_lastWriteTime.HasValue) return null;
                return _lastWriteTime.Value;
            }
            internal set => _lastWriteTime = value;
        }

        public new DateTime? LastAccessTimeUtc
        {
            get
            {
                if (!_lastAccessTime.HasValue) return null;
                return _lastAccessTime.Value.ToUniversalTime();
            }
        }

        public new DateTime? CreationTimeUtc
        {
            get
            {
                if (!_creationTime.HasValue) return null;
                return _creationTime.Value.ToUniversalTime();
            }
        }

        public new DateTime? LastWriteTimeUtc
        {
            get
            {
                if (!_lastWriteTime.HasValue) return null;
                return _lastWriteTime.Value.ToUniversalTime();
            }
        }

        public new FileAttributes Attributes { get; internal set; }

        public override string Name { get; }

        public override bool Exists => FtpConnection.FileExists(FullName);

        public override void Delete()
        {
            FtpConnection.RemoveFile(FullName);
        }
    }
}