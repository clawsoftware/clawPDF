using System;
using System.IO;

namespace clawSoft.clawPDF.ftplib.FtpLib
{
    public class FtpDirectoryInfo : FileSystemInfo
    {
        private DateTime? _creationTime;

        private DateTime? _lastAccessTime;

        private DateTime? _lastWriteTime;

        public FtpDirectoryInfo(FtpConnection ftp, string path)
        {
            FtpConnection = ftp;
            FullPath = path;
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

        public override bool Exists => FtpConnection.DirectoryExists(FullName);

        public override string Name => Path.GetFileName(FullPath);

        public override void Delete()
        {
            try
            {
                FtpConnection.RemoveDirectory(Name);
            }
            catch (FtpException innerException)
            {
                throw new Exception("Unable to delete directory.", innerException);
            }
        }

        public FtpDirectoryInfo[] GetDirectories()
        {
            return FtpConnection.GetDirectories(FullPath);
        }

        public FtpDirectoryInfo[] GetDirectories(string path)
        {
            path = Path.Combine(FullPath, path);
            return FtpConnection.GetDirectories(path);
        }

        public FtpFileInfo[] GetFiles()
        {
            return GetFiles(FtpConnection.GetCurrentDirectory());
        }

        public FtpFileInfo[] GetFiles(string mask)
        {
            return FtpConnection.GetFiles(mask);
        }
    }
}