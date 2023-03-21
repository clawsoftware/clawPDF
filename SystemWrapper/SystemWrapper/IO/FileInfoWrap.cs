using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using SystemInterface;
using SystemInterface.IO;
using SystemInterface.Security.AccessControl;
using SystemWrapper.Security.AccessControl;

namespace SystemWrapper.IO
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Wrapper for <see cref="T:System.IO.FileInfo"/> class.
    /// </summary>
    [Serializable, ComVisible(true)]
    public class FileInfoWrap : IFileInfo
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileInfo">A <see cref="T:System.IO.FileInfo"/> object.</param>
        public FileInfoWrap(FileInfo fileInfo)
        {
            Initialize(fileInfo);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileInfo">A <see cref="T:System.IO.FileInfo"/> object.</param>
        public void Initialize(FileInfo fileInfo)
        {
            FileInfoInstance = fileInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the new file, or the relative file name.</param>
        public FileInfoWrap(string fileName)
        {
            Initialize(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the new file, or the relative file name.</param>
        public void Initialize(string fileName)
        {
            FileInfoInstance = new FileInfo(fileName);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public FileAttributes Attributes
        {
            get { return FileInfoInstance.Attributes; }
            set { FileInfoInstance.Attributes = value; }
        }

        /// <inheritdoc />
        public IDateTime CreationTime
        {
            get { return new DateTimeWrap(FileInfoInstance.CreationTime); }
            set { FileInfoInstance.CreationTime = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime CreationTimeUtc
        {
            get { return new DateTimeWrap(FileInfoInstance.CreationTimeUtc); }
            set { FileInfoInstance.CreationTimeUtc = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDirectoryInfo Directory
        {
            get { return new DirectoryInfoWrap(FileInfoInstance.Directory); }
        }

        /// <inheritdoc />
        public string DirectoryName
        {
            get { return FileInfoInstance.DirectoryName; }
        }

        /// <inheritdoc />
        public bool Exists
        {
            get { return FileInfoInstance.Exists; }
        }

        /// <inheritdoc />
        public string Extension
        {
            get { return FileInfoInstance.Extension; }
        }

        /// <inheritdoc />
        public FileInfo FileInfoInstance { get; private set; }

        /// <inheritdoc />
        public string FullName
        {
            get { return FileInfoInstance.FullName; }
        }

        /// <inheritdoc />
        public bool IsReadOnly
        {
            get { return FileInfoInstance.IsReadOnly; }
            set { FileInfoInstance.IsReadOnly = value; }
        }

        /// <inheritdoc />
        public IDateTime LastAccessTime
        {
            get { return new DateTimeWrap(FileInfoInstance.LastAccessTime); }
            set { FileInfoInstance.LastAccessTime = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime LastAccessTimeUtc
        {
            get { return new DateTimeWrap(FileInfoInstance.LastAccessTimeUtc); }
            set { FileInfoInstance.LastAccessTimeUtc = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime LastWriteTime
        {
            get { return new DateTimeWrap(FileInfoInstance.LastWriteTime); }
            set { FileInfoInstance.LastWriteTime = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime LastWriteTimeUtc
        {
            get { return new DateTimeWrap(FileInfoInstance.LastWriteTimeUtc); }
            set { FileInfoInstance.LastWriteTimeUtc = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public long Length
        {
            get { return FileInfoInstance.Length; }
        }

        /// <inheritdoc />
        public string Name
        {
            get { return FileInfoInstance.Name; }
        }

        /// <inheritdoc />
        public IStreamWriter AppendText()
        {
            return new StreamWriterWrap(FileInfoInstance.AppendText());
        }

        /// <inheritdoc />
        public void Decrypt()
        {
            FileInfoInstance.Decrypt();
        }

        /// <inheritdoc />
        public void Delete()
        {
            FileInfoInstance.Delete();
        }

        /// <inheritdoc />
        public void Encrypt()
        {
            FileInfoInstance.Encrypt();
        }

        /// <inheritdoc />
        public IFileInfo CopyTo(string destFileName)
        {
            return new FileInfoWrap(FileInfoInstance.CopyTo(destFileName));
        }

        /// <inheritdoc />
        public IFileInfo CopyTo(string destFileName, bool overwrite)
        {
            return new FileInfoWrap(FileInfoInstance.CopyTo(destFileName, overwrite));
        }

        /// <inheritdoc />
        public IFileStream Create()
        {
            return new FileStreamWrap(FileInfoInstance.Create());
        }

        /// <inheritdoc />
        public IStreamWriter CreateText()
        {
            return new StreamWriterWrap(FileInfoInstance.CreateText());
        }

        /// <inheritdoc />
        public IFileSecurity GetAccessControl()
        {
            return new FileSecurityWrap(FileInfoInstance.GetAccessControl());
        }

        /// <inheritdoc />
        public IFileSecurity GetAccessControl(AccessControlSections includeSections)
        {
            return new FileSecurityWrap(FileInfoInstance.GetAccessControl(includeSections));
        }

        /// <inheritdoc />
        public void MoveTo(string destFileName)
        {
            FileInfoInstance.MoveTo(destFileName);
        }

        /// <inheritdoc />
        public IFileStream Open(FileMode mode)
        {
            return new FileStreamWrap(FileInfoInstance.Open(mode));
        }

        /// <inheritdoc />
        public IFileStream Open(FileMode mode, FileAccess access)
        {
            return new FileStreamWrap(FileInfoInstance.Open(mode, access));
        }

        /// <inheritdoc />
        public IFileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStreamWrap(FileInfoInstance.Open(mode, access, share));
        }

        /// <inheritdoc />
        public IFileStream OpenRead()
        {
            return new FileStreamWrap(FileInfoInstance.OpenRead());
        }

        /// <inheritdoc />
        public IStreamReader OpenText()
        {
            return new StreamReaderWrap(FileInfoInstance.OpenText());
        }

        /// <inheritdoc />
        public IFileStream OpenWrite()
        {
            return new FileStreamWrap(FileInfoInstance.OpenWrite());
        }

        /// <inheritdoc />
        public void Refresh()
        {
            FileInfoInstance.Refresh();
        }

        /// <inheritdoc />
        public IFileInfo Replace(string destinationFileName, string destinationBackupFileName)
        {
            return new FileInfoWrap(FileInfoInstance.Replace(destinationFileName, destinationBackupFileName));
        }

        /// <inheritdoc />
        public IFileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            return new FileInfoWrap(FileInfoInstance.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors));
        }

        /// <inheritdoc />
        public void SetAccessControl(IFileSecurity fileSecurity)
        {
            FileInfoInstance.SetAccessControl(fileSecurity.FileSecurityInstance);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return FileInfoInstance.ToString();
        }

        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        internal static IFileInfo[] ConvertFileInfoArrayIntoIFileInfoWrapArray(FileInfo[] fileInfos)
        {
            var fileInfoWraps = new FileInfoWrap[fileInfos.Length];
            for (int i = 0; i < fileInfos.Length; i++)
                fileInfoWraps[i] = new FileInfoWrap(fileInfos[i]);
            return fileInfoWraps;
        }
    }
}