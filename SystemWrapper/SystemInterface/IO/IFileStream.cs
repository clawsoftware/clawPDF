using System.IO;
using System.Security.AccessControl;
using System.Security.Permissions;
using SystemInterface.Microsoft.Win32.SafeHandles;
using SystemInterface.Security.AccessControl;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.FileStream"/> class.
    /// </summary>
    public interface IFileStream : IStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="stream">A <see cref="T:System.IO.Stream"/> object.</param>
        void Initialize(Stream stream);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileStream">A <see cref="T:System.IO.FileStream"/> object.</param>
        void Initialize(FileStream fileStream);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        void Initialize(ISafeFileHandle handle, FileAccess access);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path and creation mode.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        void Initialize(string path, FileMode mode);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        void Initialize(ISafeFileHandle handle, FileAccess access, int bufferSize);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path and creation mode.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is <c>true</c> if path specifies a disk file.</param>
        void Initialize(string path, FileMode mode, FileAccess access);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size, and synchronous or asynchronous state.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        /// <param name="isAsync"> <c>true</c> if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, <c>false</c>. </param>
        void Initialize(ISafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        void Initialize(string path, FileMode mode, FileAccess access, FileShare share);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission, and buffer size.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        void Initialize(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission, and buffer size.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="useAsync">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        void Initialize(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, and additional file options.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="options">A FileOptions value that specifies additional file options.</param>
        void Initialize(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path, creation mode, access rights and sharing permission, the buffer size, and additional file options.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="rights">A FileSystemRights constant that determines the access rights to use when creating access and audit rules for the file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="options">A FileOptions value that specifies additional file options.</param>
        void Initialize(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileStreamWrap"/> class with the specified path, creation mode, access rights and sharing permission, the buffer size, additional file options, access control and audit security.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="rights">A FileSystemRights constant that determines the access rights to use when creating access and audit rules for the file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="options">A FileOptions value that specifies additional file options.</param>
        /// <param name="fileSecurity">A FileSecurity constant that determines the access control and audit security for the file.</param>
        void Initialize(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity);

        // Properties

        /// <summary>
        /// Gets <see cref="T:System.IO.FileStream"/> object.
        /// </summary>
        FileStream FileStreamInstance { get; }

        /// <summary>
        /// Gets a value indicating whether the FileStream was opened asynchronously or synchronously.
        /// </summary>
        bool IsAsync { get; }

        /// <summary>
        /// Gets the name of the IFileStreamWrap that was passed to the constructor.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a ISafeFileHandleWrap object that represents the operating system file handle for the file that the current FileStream object encapsulates.
        /// </summary>
        ISafeFileHandle SafeFileHandle { [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode), SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get; }

        // Methods

        /// <summary>
        /// Gets a IFileSecurityWrap object that encapsulates the access control list (ACL) entries for the file described by the current FileStream object.
        /// </summary>
        /// <returns>A IFileSecurityWrap object that encapsulates the access control settings for the file described by the current FileStream object. </returns>
        IFileSecurity GetAccessControl();

        /// <summary>
        /// Prevents other processes from changing the FileStream while permitting read access.
        /// </summary>
        /// <param name="position">The beginning of the range to lock. The value of this parameter must be equal to or greater than zero (0). </param>
        /// <param name="length">The range to be locked. </param>
        void Lock(long position, long length);

        /// <summary>
        /// Applies access control list (ACL) entries described by a IFileSecurityWrap object to the file described by the current FileStream object.
        /// </summary>
        /// <param name="fileSecurity">A IFileSecurityWrap object that describes an ACL entry to apply to the current file.</param>
        void SetAccessControl(IFileSecurity fileSecurity);

        /// <summary>
        /// Returns a String that represents the current Object.
        /// </summary>
        /// <returns>A String that represents the current Object. </returns>
        string ToString();

        /// <summary>
        /// Allows access by other processes to all or part of a file that was previously locked.
        /// </summary>
        /// <param name="position">The beginning of the range to unlock.</param>
        /// <param name="length">The range to be unlocked.</param>
        void Unlock(long position, long length);
    }
}
