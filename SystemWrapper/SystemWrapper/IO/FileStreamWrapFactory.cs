namespace SystemWrapper.IO
{
    using System.IO;
    using System.Security.AccessControl;

    using SystemInterface.IO;
    using SystemInterface.Microsoft.Win32.SafeHandles;

    /// <summary>
    ///     Factory that creates a new <see cref="IFileStreamFactory"/> instance.
    /// </summary>
    public class FileStreamWrapFactory : IFileStreamFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new instance of the <see cref='IFileStream'/> class.
        /// </summary>
        /// <returns>
        ///     The <see cref="IFileStream"/>.
        /// </returns>
        public IFileStream Create(string path,
                                  FileMode fileMode)
        {
            return new FileStreamWrap(path, fileMode);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IFileStream'/> class.
        /// </summary>
        /// <param name="stream">
        ///     The file stream.
        /// </param>
        /// <returns>
        ///     The <see cref="IFileStream"/>.
        /// </returns>
        public IFileStream Create(Stream stream)
        {
            return new FileStreamWrap(stream);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IFileStream'/> class passing the memory stream.
        /// </summary>
        /// <param name="fileStream">
        ///     The file stream.
        /// </param>
        /// <returns>
        ///     The <see cref="IFileStream"/>.
        /// </returns>
        public IFileStream Create(FileStream fileStream)
        {
            return new FileStreamWrap(fileStream);
        }

        /// <summary>
        ///     Creates a new instance of the <see cref='IFileStream'/> class passing the byte buffer.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <returns>
        ///     The <see cref="IFileStream"/>.
        /// </returns>
        public IFileStream Create(ISafeFileHandle handle,
                                  FileAccess access)
        {
            return new FileStreamWrap(handle, access);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size. 
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        public IFileStream Create(ISafeFileHandle handle,
                                  FileAccess access,
                                  int bufferSize)
        {
            return new FileStreamWrap(handle, access, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path and creation mode. 
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        public IFileStream Create(string path,
                                  FileMode mode,
                                  FileAccess access)
        {
            return new FileStreamWrap(path, mode, access);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size, and synchronous or asynchronous state. 
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        /// <param name="isAsync"> true if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>        
        public IFileStream Create(ISafeFileHandle handle,
                                  FileAccess access,
                                  int bufferSize,
                                  bool isAsync)
        {
            return new FileStreamWrap(handle, access, bufferSize, isAsync);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        public IFileStream Create(string path,
                                  FileMode mode,
                                  FileAccess access,
                                  FileShare share)
        {
            return new FileStreamWrap(path, mode, access, share);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission, and buffer size.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        public IFileStream Create(string path,
                                  FileMode mode,
                                  FileAccess access,
                                  FileShare share,
                                  int bufferSize)
        {
            return new FileStreamWrap(path, mode, access, share, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission, and buffer size.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="useAsync">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        public IFileStream Create(string path,
                                  FileMode mode,
                                  FileAccess access,
                                  FileShare share,
                                  int bufferSize,
                                  bool useAsync)
        {
            return new FileStreamWrap(path, mode, access, share, bufferSize, useAsync);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, and additional file options.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="options">A FileOptions value that specifies additional file options.</param>
        public IFileStream Create(string path,
                                  FileMode mode,
                                  FileAccess access,
                                  FileShare share,
                                  int bufferSize,
                                  FileOptions options)
        {
            return new FileStreamWrap(path, mode, access, share, bufferSize, options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, access rights and sharing permission, the buffer size, and additional file options.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="rights">A FileSystemRights constant that determines the access rights to use when creating access and audit rules for the file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="options">A FileOptions value that specifies additional file options.</param>
        public IFileStream Create(string path,
                                  FileMode mode,
                                  FileSystemRights rights,
                                  FileShare share,
                                  int bufferSize,
                                  FileOptions options)
        {
            return new FileStreamWrap(path, mode, rights, share, bufferSize, options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, access rights and sharing permission, the buffer size, additional file options, access control and audit security.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="rights">A FileSystemRights constant that determines the access rights to use when creating access and audit rules for the file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes.</param>
        /// <param name="options">A FileOptions value that specifies additional file options.</param>
        /// <param name="fileSecurity">A FileSecurity constant that determines the access control and audit security for the file.</param>
        public IFileStream Create(string path,
                                  FileMode mode,
                                  FileSystemRights rights,
                                  FileShare share,
                                  int bufferSize,
                                  FileOptions options,
                                  FileSecurity fileSecurity)
        {
            return new FileStreamWrap(path, mode, rights, share, bufferSize, options, fileSecurity);
        }

        #endregion
    }
}