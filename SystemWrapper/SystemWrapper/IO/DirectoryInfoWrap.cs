using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security.AccessControl;
using SystemInterface;
using SystemInterface.IO;
using SystemInterface.Security.AccessControl;
using SystemWrapper.Security.AccessControl;

namespace SystemWrapper.IO
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Wrapper for <see cref="T:System.IO.DirectoryInfo"/> class.
    /// </summary>
    [Serializable, ComVisible(true)]
    public class DirectoryInfoWrap : IDirectoryInfo
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.DirectoryInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="directoryInfo">A <see cref="T:System.IO.DirectoryInfo"/> object.</param>
        public DirectoryInfoWrap(DirectoryInfo directoryInfo)
        {
            Initialize(directoryInfo);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.DirectoryInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="directoryInfo">A <see cref="T:System.IO.DirectoryInfo"/> object.</param>
        public void Initialize(DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.DirectoryInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="path">A string specifying the path on which to create the <see cref="T:SystemWrapper.IO.DirectoryInfoWrap"/>. </param>
        public DirectoryInfoWrap(string path)
        {
            Initialize(path);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.DirectoryInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="path">A string specifying the path on which to create the <see cref="T:SystemWrapper.IO.DirectoryInfoWrap"/>. </param>
        public void Initialize(string path)
        {
            DirectoryInfo = new DirectoryInfo(path);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public FileAttributes Attributes
        {
            get { return DirectoryInfo.Attributes; }
            set { DirectoryInfo.Attributes = value; }
        }

        /// <inheritdoc />
        public IDateTime CreationTime
        {
            get { return new DateTimeWrap(DirectoryInfo.CreationTime); }
            set { DirectoryInfo.CreationTime = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime CreationTimeUtc
        {
            get { return new DateTimeWrap(DirectoryInfo.CreationTimeUtc); }
            set { DirectoryInfo.CreationTimeUtc = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public DirectoryInfo DirectoryInfo { get; private set; }

        /// <inheritdoc />
        public bool Exists
        {
            get { return DirectoryInfo.Exists; }
        }

        /// <inheritdoc />
        public string Extension
        {
            get { return DirectoryInfo.Extension; }
        }

        /// <inheritdoc />
        public string FullName
        {
            get { return DirectoryInfo.FullName; }
        }

        /// <inheritdoc />
        public IDateTime LastAccessTime
        {
            get { return new DateTimeWrap(DirectoryInfo.LastAccessTime); }
            set { DirectoryInfo.LastAccessTime = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime LastAccessTimeUtc
        {
            get { return new DateTimeWrap(DirectoryInfo.LastAccessTimeUtc); }
            set { DirectoryInfo.LastAccessTimeUtc = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime LastWriteTime
        {
            get { return new DateTimeWrap(DirectoryInfo.LastWriteTime); }
            set { DirectoryInfo.LastWriteTime = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public IDateTime LastWriteTimeUtc
        {
            get { return new DateTimeWrap(DirectoryInfo.LastWriteTimeUtc); }
            set { DirectoryInfo.LastWriteTimeUtc = value.DateTimeInstance; }
        }

        /// <inheritdoc />
        public string Name
        {
            get { return DirectoryInfo.Name; }
        }

        /// <inheritdoc />
        public IDirectoryInfo Parent
        {
            get { return new DirectoryInfoWrap(DirectoryInfo.Parent); }
        }

        /// <inheritdoc />
        public IDirectoryInfo Root
        {
            get { return new DirectoryInfoWrap(DirectoryInfo.Root); }
        }

        /// <inheritdoc />
        public void Create()
        {
            DirectoryInfo.Create();
        }

        /// <inheritdoc />
        public void Create(IDirectorySecurity directorySecurity)
        {
            DirectoryInfo.Create(directorySecurity.DirectorySecurityInstance);
        }

        /// <inheritdoc />
        public ObjRef CreateObjRef(Type requestedType)
        {
            return DirectoryInfo.CreateObjRef(requestedType);
        }

        /// <inheritdoc />
        public IDirectoryInfo CreateSubdirectory(string path)
        {
            return new DirectoryInfoWrap(DirectoryInfo.CreateSubdirectory(path));
        }

        /// <inheritdoc />
        public IDirectoryInfo CreateSubdirectory(string path, IDirectorySecurity directorySecurity)
        {
            return new DirectoryInfoWrap(DirectoryInfo.CreateSubdirectory(path, directorySecurity.DirectorySecurityInstance));
        }

        /// <inheritdoc />
        public void Delete()
        {
            DirectoryInfo.Delete();
        }

        /// <inheritdoc />
        public void Delete(bool recursive)
        {
            DirectoryInfo.Delete(recursive);
        }

        /// <inheritdoc />
        public IDirectorySecurity GetAccessControl()
        {
            return new DirectorySecurityWrap(DirectoryInfo.GetAccessControl());
        }

        /// <inheritdoc />
        public IDirectorySecurity GetAccessControl(AccessControlSections includeSections)
        {
            return new DirectorySecurityWrap(DirectoryInfo.GetAccessControl(includeSections));
        }

        /// <inheritdoc />
        public IDirectoryInfo[] GetDirectories()
        {
            DirectoryInfo[] directoryInfos = DirectoryInfo.GetDirectories();
            return ConvertDirectoryInfoArrayIntoIDirectoryInfoWrapArray(directoryInfos);
        }

        /// <inheritdoc />
        public IDirectoryInfo[] GetDirectories(string searchPattern)
        {
            DirectoryInfo[] directoryInfos = DirectoryInfo.GetDirectories(searchPattern);
            return ConvertDirectoryInfoArrayIntoIDirectoryInfoWrapArray(directoryInfos);
        }

        /// <inheritdoc />
        public IDirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
        {
            DirectoryInfo[] directoryInfos = DirectoryInfo.GetDirectories(searchPattern, searchOption);
            return ConvertDirectoryInfoArrayIntoIDirectoryInfoWrapArray(directoryInfos);
        }

        /// <inheritdoc />
        public IFileInfo[] GetFiles()
        {
            FileInfo[] fileInfos = DirectoryInfo.GetFiles();
            return FileInfoWrap.ConvertFileInfoArrayIntoIFileInfoWrapArray(fileInfos);
        }

        /// <inheritdoc />
        public IFileInfo[] GetFiles(string searchPattern)
        {
            FileInfo[] fileInfos = DirectoryInfo.GetFiles(searchPattern);
            return FileInfoWrap.ConvertFileInfoArrayIntoIFileInfoWrapArray(fileInfos);
        }

        /// <inheritdoc />
        public IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            FileInfo[] fileInfos = DirectoryInfo.GetFiles(searchPattern, searchOption);
            return FileInfoWrap.ConvertFileInfoArrayIntoIFileInfoWrapArray(fileInfos);
        }

        /// <inheritdoc />
        public FileSystemInfo[] GetFileSystemInfos()
        {
            return DirectoryInfo.GetFileSystemInfos();
        }

        /// <inheritdoc />
        public FileSystemInfo[] GetFileSystemInfos(string searchPattern)
        {
            return DirectoryInfo.GetFileSystemInfos(searchPattern);
        }

        /// <inheritdoc />
        public object GetLifetimeService()
        {
            return DirectoryInfo.GetLifetimeService();
        }

        /// <inheritdoc />
        public object InitializeLifetimeService()
        {
            return DirectoryInfo.InitializeLifetimeService();
        }

        /// <inheritdoc />
        public void MoveTo(string destDirName)
        {
            DirectoryInfo.MoveTo(destDirName);
        }

        /// <inheritdoc />
        public void Refresh()
        {
            DirectoryInfo.Refresh();
        }

        /// <inheritdoc />
        public void SetAccessControl(IDirectorySecurity directorySecurity)
        {
            DirectoryInfo.SetAccessControl(directorySecurity.DirectorySecurityInstance);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return DirectoryInfo.ToString();
        }

        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        private static IDirectoryInfo[] ConvertDirectoryInfoArrayIntoIDirectoryInfoWrapArray(DirectoryInfo[] directoryInfos)
        {
            IDirectoryInfo[] directoryInfoWraps = new DirectoryInfoWrap[directoryInfos.Length];
            for (int i = 0; i < directoryInfos.Length; i++)
                directoryInfoWraps[i] = new DirectoryInfoWrap(directoryInfos[i]);
            return directoryInfoWraps;
        }
    }
}