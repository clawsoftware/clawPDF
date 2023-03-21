using System;
using System.IO;
using System.Runtime.Remoting;
using System.Security.AccessControl;
using SystemInterface.Security.AccessControl;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.DirectoryInfo"/> class.
    /// </summary>
    [CLSCompliant(false)]
    public interface IDirectoryInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.DirectoryInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="directoryInfo">A <see cref="T:System.IO.DirectoryInfo"/> object.</param>
        void Initialize(DirectoryInfo directoryInfo);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.DirectoryInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="path">A string specifying the path on which to create the <see cref="T:SystemInterface.IO.DirectoryInfoWrap"/>. </param>
        void Initialize(string path);

        // Properties

        /// <summary>
        ///  Gets or sets the <see cref="System.IO.FileAttributes"/> of the current Gets or sets the creation time, in coordinated universal time (UTC), of the current FileSystemInfo object. .
        /// </summary>
        FileAttributes Attributes { get; set; }

        /// <summary>
        /// Gets or sets the creation time of the current <see cref="T:System.IO.FileSystemInfo"/> object.
        /// </summary>
        IDateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the creation time, in coordinated universal time (UTC), of the current <see cref="T:System.IO.FileSystemInfo"/> object.
        /// </summary>
        IDateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// Gets <see cref="T:System.IO.DirectoryInfo"/> object.
        /// </summary>
        DirectoryInfo DirectoryInfo { get; }

        /// <summary>
        ///  Gets a value indicating whether the directory exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets the string representing the extension part of the file.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets or sets the time the current file or directory was last accessed.
        /// </summary>
        IDateTime LastAccessTime { get; set; }

        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.
        /// </summary>
        IDateTime LastAccessTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>
        IDateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.
        /// </summary>
        IDateTime LastWriteTimeUtc { get; set; }

        /// <summary>
        /// Gets the name of this <see cref="T:System.IO.DirectoryInfo"/> instance.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the parent directory of a specified subdirectory.
        /// </summary>
        IDirectoryInfo Parent { get; }

        /// <summary>
        /// A <see cref="T:SystemInterface.IO.IDirectoryInfoWrap"/> object representing the root of a path.
        /// </summary>
        IDirectoryInfo Root { get; }

        // Methods

        /// <summary>
        /// Creates a directory.
        /// </summary>
        void Create();

        /// <summary>
        /// Creates a directory using a DirectorySecurity object.
        /// </summary>
        /// <param name="directorySecurity">The access control to apply to the directory.</param>
        void Create(IDirectorySecurity directorySecurity);

        /// <summary>
        /// Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.
        /// </summary>
        /// <param name="requestedType">The <see cref="T:System.Type"/> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef"/> will reference. </param>
        /// <returns>Information required to generate a proxy.</returns>
        ObjRef CreateObjRef(Type requestedType);

        /// <summary>
        /// Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the IDirectoryInfoWrap class.
        /// </summary>
        /// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name. </param>
        /// <returns>The last directory specified in <paramref name="path"/>.</returns>
        IDirectoryInfo CreateSubdirectory(string path);

        /// <summary>
        /// Creates a subdirectory or subdirectories on the specified path with the specified security. The specified path can be relative to this instance of the IDirectoryInfoWrap class.
        /// </summary>
        /// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name. </param>
        /// <param name="directorySecurity">The security to apply. </param>
        /// <returns>The last directory specified in <paramref name="path"/>.</returns>
        IDirectoryInfo CreateSubdirectory(string path, IDirectorySecurity directorySecurity);

        /// <summary>
        /// Deletes this <see cref="T:System.IO.DirectoryInfo"/> if it is empty.
        /// </summary>
        void Delete();

        /// <summary>
        /// Deletes this instance of a IDirectoryInfoWrap, specifying whether to delete subdirectories and files.
        /// </summary>
        /// <param name="recursive"><c>true</c> to delete this directory, its subdirectories, and all files; otherwise, <c>false</c>.</param>
        void Delete(bool recursive);

        /// <summary>
        /// Gets a <see cref="T:SystemInterface.Security.AccessControl.IDirectorySecurityWrap"/> object that encapsulates the access control list (ACL) entries for the directory described by the current DirectoryInfo object.
        /// </summary>
        /// <returns>A <see cref="T:SystemInterface.Security.AccessControl.IDirectorySecurityWrap"/> object that encapsulates the access control rules for the directory.</returns>
        IDirectorySecurity GetAccessControl();

        /// <summary>
        /// Gets a <see cref="T:SystemInterface.Security.AccessControl.IDirectorySecurityWrap"/> object that encapsulates the specified type of access control list (ACL) entries for the directory described by the current DirectoryInfo object.
        /// </summary>
        /// <param name="includeSections">One of the <see cref="T:System.Security.AccessControl.AccessControlSections"/> values that specifies the type of access control list (ACL) information to receive.</param>
        /// <returns>A <see cref="T:SystemInterface.Security.AccessControl.IDirectorySecurityWrap"/> object that encapsulates the access control rules for the file described by the path parameter.</returns>
        IDirectorySecurity GetAccessControl(AccessControlSections includeSections);

        /// <summary>
        /// Returns the subdirectories of the current directory.
        /// </summary>
        /// <returns>An array of <see cref="T:SystemInterface.IO.IDirectoryInfoWrap"/> objects. </returns>
        IDirectoryInfo[] GetDirectories();

        /// <summary>
        /// Returns an array of directories in the current DirectoryInfo matching the given search criteria.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
        /// <returns>An array of type IDirectoryInfoWrap matching <paramref name="searchPattern"/>.</returns>
        IDirectoryInfo[] GetDirectories(string searchPattern);

        /// <summary>
        /// Returns an array of directories in the current DirectoryInfo matching the given search criteria and using a value to determine whether to search subdirectories.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
        /// <param name="searchOption">One of the values of the <see cref="T:System.IO.SearchOption"/> enumeration that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
        /// <returns></returns>
        IDirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption);

        /// <summary>
        /// Returns a file list from the current directory.
        /// </summary>
        /// <returns>An array of type <see cref="SystemInterface.IO.IFileInfo"/> FileInfo. </returns>
        IFileInfo[] GetFiles();

        /// <summary>
        /// Returns a file list from the current directory matching the given <paramref name="searchPattern"/>.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "*.txt".</param>
        /// <returns>An array of type <see cref="T:SystemInterface.IO.IFileInfoWrap"/>.</returns>
        IFileInfo[] GetFiles(string searchPattern);

        /// <summary>
        /// Returns a file list from the current directory matching the given <paramref name="searchPattern"/> and using a value to determine whether to search subdirectories.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
        /// <param name="searchOption">One of the values of the <see cref="T:System.IO.SearchOption"/> enumeration that specifies whether the search operation should include only the current directory or should include all subdirectories.</param>
        /// <returns>An array of type <see cref="T:SystemInterface.IO.IFileInfoWrap"/>.</returns>
        IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption);

        /// <summary>
        /// Returns an array of strongly typed <see cref="T:System.IO.FileSystemInfo"/> entries representing all the files and subdirectories in a directory.
        /// </summary>
        /// <returns>An array of strongly typed <see cref="T:System.IO.FileSystemInfo"/> entries.</returns>
        FileSystemInfo[] GetFileSystemInfos();

        /// <summary>
        /// Retrieves an array of strongly typed <see cref="T:System.IO.FileSystemInfo"/> objects representing the files and subdirectories matching the specified search criteria.
        /// </summary>
        /// <param name="searchPattern">The search string, such as "System*", used to search for all directories beginning with the word "System".</param>
        /// <returns>An array of strongly typed <see cref="T:System.IO.FileSystemInfo"/> entries.</returns>
        FileSystemInfo[] GetFileSystemInfos(string searchPattern);

        /// <summary>
        /// Retrieves the current lifetime service object that controls the lifetime policy for this instance.
        /// </summary>
        /// <returns>An object of type ILease used to control the lifetime policy for this instance. </returns>
        object GetLifetimeService();

        /// <summary>
        /// Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>An object of type ILease used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the LifetimeServices.LeaseManagerPollTime property.</returns>
        object InitializeLifetimeService();

        /// <summary>
        /// Moves a <see cref="T:System.IO.DirectoryInfo"/> instance and its contents to a new path.
        /// </summary>
        /// <param name="destDirName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory.</param>
        void MoveTo(string destDirName);

        /// <summary>
        /// Refreshes the state of the object.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Applies access control list (ACL) entries described by a DirectorySecurity object to the directory described by the current DirectoryInfo object.
        /// </summary>
        /// <param name="directorySecurity">A IDirectorySecurityWrap object that describes an ACL entry to apply to the directory described by the path parameter. </param>
        void SetAccessControl(IDirectorySecurity directorySecurity);

        /// <summary>
        /// Returns the original path that was passed by the user.
        /// </summary>
        /// <returns>Returns the original path that was passed by the user.</returns>
        string ToString();
    }
}
