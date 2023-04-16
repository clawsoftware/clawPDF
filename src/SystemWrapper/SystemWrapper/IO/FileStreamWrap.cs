using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using SystemInterface.IO;
using SystemInterface.Microsoft.Win32.SafeHandles;
using SystemInterface.Security.AccessControl;
using SystemWrapper.Microsoft.Win32.SafeHandles;
using SystemWrapper.Security.AccessControl;

namespace SystemWrapper.IO
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Wrapper for <see cref="T:System.IO.FileStream"/> class.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public class FileStreamWrap : IFileStream
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="stream">A <see cref="T:System.IO.Stream"/> object.</param>
        public FileStreamWrap(Stream stream)
        {
            Initialize(stream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="stream">A <see cref="T:System.IO.Stream"/> object.</param>
        public void Initialize(Stream stream)
        {
            FileStreamInstance = stream as FileStream;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileStream">A <see cref="T:System.IO.FileStream"/> object.</param>
        public FileStreamWrap(FileStream fileStream)
        {
            Initialize(fileStream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileStream">A <see cref="T:System.IO.FileStream"/> object.</param>
        public void Initialize(FileStream fileStream)
        {
            FileStreamInstance = fileStream;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        public FileStreamWrap(ISafeFileHandle handle, FileAccess access)
        {
            Initialize(handle, access);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        public void Initialize(ISafeFileHandle handle, FileAccess access)
        {
            FileStreamInstance = new FileStream(handle.SafeFileHandleInstance, access);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path and creation mode.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        public FileStreamWrap(string path, FileMode mode)
        {
            Initialize(path, mode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path and creation mode.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        public void Initialize(string path, FileMode mode)
        {
            FileStreamInstance = new FileStream(path, mode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        public FileStreamWrap(ISafeFileHandle handle, FileAccess access, int bufferSize)
        {
            Initialize(handle, access, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        public void Initialize(ISafeFileHandle handle, FileAccess access, int bufferSize)
        {
            FileStreamInstance = new FileStream(handle.SafeFileHandleInstance, access, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path and creation mode.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        public FileStreamWrap(string path, FileMode mode, FileAccess access)
        {
            Initialize(path, mode, access);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path and creation mode.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        public void Initialize(string path, FileMode mode, FileAccess access)
        {
            FileStreamInstance = new FileStream(path, mode, access);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size, and synchronous or asynchronous state.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        /// <param name="isAsync"> <c>true</c> if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public FileStreamWrap(ISafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
        {
            Initialize(handle, access, bufferSize, isAsync);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class for the specified file handle, with the specified read/write permission, and buffer size, and synchronous or asynchronous state.
        /// </summary>
        /// <param name="handle">A file handle for the file that the current FileStream object will encapsulate. </param>
        /// <param name="access">A FileAccess constant that sets the CanRead and CanWrite properties of the FileStream object. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        /// <param name="isAsync"> <c>true</c> if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, false. </param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void Initialize(ISafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
        {
            FileStreamInstance = new FileStream(handle.SafeFileHandleInstance, access, bufferSize, isAsync);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        public FileStreamWrap(string path, FileMode mode, FileAccess access, FileShare share)
        {
            Initialize(path, mode, access, share);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        public void Initialize(string path, FileMode mode, FileAccess access, FileShare share)
        {
            FileStreamInstance = new FileStream(path, mode, access, share);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission, and buffer size.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        public FileStreamWrap(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
        {
            Initialize(path, mode, access, share, bufferSize);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.IO.FileStreamWrap"/> class with the specified path, creation mode, read/write permission, and sharing permission, and buffer size.
        /// </summary>
        /// <param name="path">A relative or absolute path for the file that the current FileStream object will encapsulate.</param>
        /// <param name="mode">A FileMode constant that determines how to open or create the file.</param>
        /// <param name="access">A FileAccess constant that determines how the file can be accessed by the FileStream object. This gets the CanRead and CanWrite properties of the FileStream object. CanSeek is true if path specifies a disk file.</param>
        /// <param name="share">A FileShare constant that determines how the file will be shared by processes. </param>
        /// <param name="bufferSize">A positive Int32 value greater than 0 indicating the buffer size. For bufferSize values between one and eight, the actual buffer size is set to eight bytes. </param>
        public void Initialize(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize)
        {
            FileStreamInstance = new FileStream(path, mode, access, share, bufferSize);
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
        public FileStreamWrap(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
        {
            Initialize(path, mode, access, share, bufferSize, useAsync);
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
        public void Initialize(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync)
        {
            FileStreamInstance = new FileStream(path, mode, access, share, bufferSize, useAsync);
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
        public FileStreamWrap(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
        {
            Initialize(path, mode, access, share, bufferSize, options);
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
        public void Initialize(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options)
        {
            FileStreamInstance = new FileStream(path, mode, access, share, bufferSize, options);
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
        public FileStreamWrap(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
        {
            Initialize(path, mode, rights, share, bufferSize, options);
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
        public void Initialize(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options)
        {
            FileStreamInstance = new FileStream(path, mode, rights, share, bufferSize, options);
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
        public FileStreamWrap(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
        {
            Initialize(path, mode, rights, share, bufferSize, options, fileSecurity);
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
        public void Initialize(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity)
        {
            FileStreamInstance = new FileStream(path, mode, rights, share, bufferSize, options, fileSecurity);
        }

        #endregion Constructors and Initializers

        /// <summary>
        /// Gets a value indicating whether the current stream supports reading.
        /// </summary>
        public bool CanRead
        {
            get { return FileStreamInstance.CanRead; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports seeking.
        /// </summary>
        public bool CanSeek
        {
            get { return FileStreamInstance.CanSeek; }
        }

        /// <inheritdoc />
        public bool CanTimeout
        {
            get { return FileStreamInstance.CanTimeout; }
        }

        /// <summary>
        /// Gets a value indicating whether the current stream supports writing.
        /// </summary>
        public bool CanWrite
        {
            get { return FileStreamInstance.CanWrite; }
        }

        /// <inheritdoc />
        public int ReadTimeout
        {
            get { return FileStreamInstance.ReadTimeout; }
            set { FileStreamInstance.ReadTimeout = value; }
        }

        /// <summary>
        /// Gets <see cref="T:System.IO.Stream"/> object.
        /// </summary>
        public Stream StreamInstance
        {
            get
            {
                return FileStreamInstance;
            }
        }

        /// <inheritdoc />
        public int WriteTimeout
        {
            get { return FileStreamInstance.WriteTimeout; }
            set { FileStreamInstance.WriteTimeout = value; }
        }

        /// <inheritdoc />
        public FileStream FileStreamInstance { get; private set; }

        /// <inheritdoc />
        public bool IsAsync
        {
            get { return FileStreamInstance.IsAsync; }
        }

        /// <summary>
        /// Gets the length in bytes of the stream.
        /// </summary>
        public long Length
        {
            get { return FileStreamInstance.Length; }
        }

        /// <inheritdoc />
        public string Name
        {
            get { return FileStreamInstance.Name; }
        }

        /// <summary>
        /// Gets or sets the current position of this stream.
        /// </summary>
        public long Position
        {
            get { return FileStreamInstance.Position; }
            set { FileStreamInstance.Position = value; }
        }

        /// <inheritdoc />
        public ISafeFileHandle SafeFileHandle
        {
            get { return new SafeFileHandleWrap(FileStreamInstance.SafeFileHandle); }
        }

        /// <summary>
        /// Begins an asynchronous read.
        /// </summary>
        /// <param name="array">The buffer to read data into.</param>
        /// <param name="offset">The byte offset in array at which to begin reading.</param>
        /// <param name="numBytes">The maximum number of bytes to read.</param>
        /// <param name="userCallback">The method to be called when the asynchronous read operation is completed.</param>
        /// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
        /// <returns>A user-provided object that distinguishes this particular asynchronous read request from other requests.</returns>
        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
        {
            return FileStreamInstance.BeginRead(array, offset, numBytes, userCallback, stateObject);
        }

        /// <summary>
        /// Begins an asynchronous write.
        /// </summary>
        /// <param name="array">The buffer to read data into.</param>
        /// <param name="offset">The byte offset in array at which to begin reading.</param>
        /// <param name="numBytes">The maximum number of bytes to read.</param>
        /// <param name="userCallback">The method to be called when the asynchronous read operation is completed.</param>
        /// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
        /// <returns>An IAsyncResult that references the asynchronous write.</returns>
        [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
        public IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
        {
            return FileStreamInstance.BeginWrite(array, offset, numBytes, userCallback, stateObject);
        }

        /// <summary>
        /// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
        /// </summary>
        public void Close()
        {
            FileStreamInstance.Close();
        }

        /// <summary>
        /// Waits for the pending asynchronous read to complete.
        /// </summary>
        /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
        /// <returns>The number of bytes read from the stream, between 0 and the number of bytes you requested. Streams only return 0 at the end of the stream, otherwise, they should block until at least 1 byte is available.</returns>
        public int EndRead(IAsyncResult asyncResult)
        {
            return FileStreamInstance.EndRead(asyncResult);
        }

        /// <summary>
        /// Ends an asynchronous write, blocking until the I/O operation has completed.
        /// </summary>
        /// <param name="asyncResult">The pending asynchronous I/O request. </param>
        public void EndWrite(IAsyncResult asyncResult)
        {
            FileStreamInstance.EndWrite(asyncResult);
        }

        /// <summary>
        /// Clears all buffers for this stream and causes any buffered data to be written to the file system.
        /// </summary>
        public void Flush()
        {
            FileStreamInstance.Flush();
        }

        /// <inheritdoc />
        public IFileSecurity GetAccessControl()
        {
            return new FileSecurityWrap(FileStreamInstance.GetAccessControl());
        }

        /// <inheritdoc />
        public virtual void Lock(long position, long length)
        {
            FileStreamInstance.Lock(position, length);
        }

        /// <summary>
        /// Reads a block of bytes from the stream and writes the data in a given buffer.
        /// </summary>
        /// <param name="buffer">When this method returns, contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source. </param>
        /// <param name="offset">The byte offset in array at which the read bytes will be placed. </param>
        /// <param name="count">The maximum number of bytes to read. </param>
        /// <returns>The total number of bytes read into the buffer. This might be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached. </returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            return FileStreamInstance.Read(buffer, offset, count);
        }

        /// <summary>
        /// Reads a byte from the file and advances the read position one byte.
        /// </summary>
        /// <returns>The byte, cast to an Int32, or -1 if the end of the stream has been reached.</returns>
        public int ReadByte()
        {
            return FileStreamInstance.ReadByte();
        }

        /// <summary>
        /// Sets the current position of this stream to the given value.
        /// </summary>
        /// <param name="offset">The point relative to origin from which to begin seeking.</param>
        /// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for origin, using a value of type SeekOrigin. </param>
        /// <returns>The new position in the stream.</returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            return FileStreamInstance.Seek(offset, origin);
        }

        /// <inheritdoc />
        public void SetAccessControl(IFileSecurity fileSecurity)
        {
            FileStreamInstance.SetAccessControl(fileSecurity.FileSecurityInstance);
        }

        /// <summary>
        /// Sets the length of this stream to the given value.
        /// </summary>
        /// <param name="value">The new length of the stream.</param>
        public void SetLength(long value)
        {
            FileStreamInstance.SetLength(value);
        }

        /// <inheritdoc />
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        public IStream Synchronized(IStream stream)
        {
            return new FileStreamWrap(Stream.Synchronized(stream.StreamInstance));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return FileStreamInstance.ToString();
        }

        /// <inheritdoc />
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
            FileStreamInstance.Dispose();
        }

        /// <inheritdoc />
        public void Unlock(long position, long length)
        {
            FileStreamInstance.Unlock(position, length);
        }

        /// <summary>
        /// Writes a block of bytes to this stream using data from a buffer.
        /// </summary>
        /// <param name="buffer">The buffer containing data to write to the stream.</param>
        /// <param name="offset">The zero-based byte offset in array at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            FileStreamInstance.Write(buffer, offset, count);
        }

        /// <summary>
        /// Writes a byte to the current position in the file stream.
        /// </summary>
        /// <param name="value">A byte to write to the stream.</param>
        public void WriteByte(byte value)
        {
            FileStreamInstance.WriteByte(value);
        }

        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        internal static IFileStream[] ConvertFileStreamArrayIntoIFileStreamWrapArray(FileStream[] fileStreams)
        {
            var fileStreamWraps = new FileStreamWrap[fileStreams.Length];
            for (int i = 0; i < fileStreams.Length; i++)
                fileStreamWraps[i] = new FileStreamWrap(fileStreams[i]);
            return fileStreamWraps;
        }

        public async Task FlushAsync()
        {
            await FileStreamInstance.FlushAsync();
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            return await FileStreamInstance.ReadAsync(buffer, offset, count);
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return await FileStreamInstance.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count)
        {
            await FileStreamInstance.WriteAsync(buffer, offset, count);
        }

        public async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await FileStreamInstance.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public void CopyTo(IStream destination)
        {
            StreamInstance.CopyTo(destination.StreamInstance);
        }

        public void CopyTo(IStream destination, int bufferSize)
        {
            StreamInstance.CopyTo(destination.StreamInstance, bufferSize);
        }

        public async Task CopyToAsync(IStream destination)
        {
            await StreamInstance.CopyToAsync(destination.StreamInstance);
        }

        public async Task CopyToAsync(IStream destination, int bufferSize)
        {
            await StreamInstance.CopyToAsync(destination.StreamInstance, bufferSize);
        }

        public async Task CopyToAsync(IStream destination, int bufferSize, CancellationToken cancellationToken)
        {
            await StreamInstance.CopyToAsync(destination.StreamInstance, bufferSize, cancellationToken);
        }
    }
}
