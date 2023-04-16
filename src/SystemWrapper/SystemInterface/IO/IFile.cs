using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using SystemInterface.Security.AccessControl;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.File"/> class.
    /// </summary>
    [CLSCompliant(false)]
    public interface IFile
    {
        // Methods

        /// <summary>
        /// Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        void AppendAllLines(string path, IEnumerable<string> contents);

        /// <summary>
        /// Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to append the lines to. The file is created if it doesn't already exist.</param>
        /// <param name="contents">The lines to append to the file.</param>
        void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        /// <summary>
        /// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
        /// </summary>
        /// <param name="path">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        void AppendAllText(string path, string contents);

        /// <summary>
        /// Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="path">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        void AppendAllText(string path, string contents, Encoding encoding);

        /// <summary>
        /// Creates a IStreamWriterWrap that appends UTF-8 encoded text to an existing file.
        /// </summary>
        /// <param name="path">The path to the file to append to.</param>
        /// <returns>A IStreamWriterWrap that appends UTF-8 encoded text to an existing file.</returns>
        IStreamWriter AppendText(string path);

        /// <summary>
        /// Copies an existing file to a new file. Overwriting a file of the same name is not allowed.
        /// </summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
        void Copy(string sourceFileName, string destFileName);

        /// <summary>
        /// Copies an existing file to a new file. Overwriting a file of the same name is allowed.
        /// </summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file. This cannot be a directory.</param>
        /// <param name="overwrite"> <c>true</c> if the destination file can be overwritten; otherwise, false.</param>
        void Copy(string sourceFileName, string destFileName, bool overwrite);

        /// <summary>
        /// Creates or overwrites a file in the specified path.
        /// </summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <returns>A IFileStreamWrap that provides read/write access to the file specified in path.</returns>
        IFileStream Create(string path);

        /// <summary>
        /// Creates or overwrites the specified file.
        /// </summary>
        /// <param name="path">The name of the file.</param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
        /// <returns>A IFileStreamWrap with the specified buffer size that provides read/write access to the file specified in path. </returns>
        IFileStream Create(string path, int bufferSize);

        /// <summary>
        /// Creates or overwrites the specified file, specifying a buffer size and a FileOptions value that describes how to create or overwrite the file.
        /// </summary>
        /// <param name="path">The name of the file. </param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
        /// <param name="options">One of the FileOptions values that describes how to create or overwrite the file. </param>
        /// <returns>A new file with the specified buffer size. </returns>
        IFileStream Create(string path, int bufferSize, FileOptions options);

        /// <summary>
        /// Creates or overwrites the specified file with the specified buffer size, file options, and file security.
        /// </summary>
        /// <param name="path">The name of the file. </param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file. </param>
        /// <param name="options">One of the FileOptions values that describes how to create or overwrite the file. </param>
        /// <param name="fileSecurity">One of the IFileSecurityWrap values that determines the access control and audit security for the file. </param>
        /// <returns>A new file with the specified buffer size, file options, and file security. </returns>
        IFileStream Create(string path, int bufferSize, FileOptions options, IFileSecurity fileSecurity);

        /// <summary>
        /// Creates or opens a file for writing UTF-8 encoded text.
        /// </summary>
        /// <param name="path">The file to be opened for writing. </param>
        /// <returns>A IStreamWriterWrap that writes to the specified file using UTF-8 encoding. </returns>
        IStreamWriter CreateText(string path);

        /// <summary>
        /// Decrypts a file that was encrypted by the current account using the Encrypt method.
        /// </summary>
        /// <param name="path">A path that describes a file to decrypt.</param>
        void Decrypt(string path);

        /// <summary>
        /// Deletes the specified file. An exception is not thrown if the specified file does not exist.
        /// </summary>
        /// <param name="path">The name of the file to be deleted.</param>
        void Delete(string path);

        /// <summary>
        /// Encrypts a file so that only the account used to encrypt the file can decrypt it.
        /// </summary>
        /// <param name="path">A path that describes a file to encrypt.</param>
        void Encrypt(string path);

        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        /// <param name="path">The file to check.</param>
        /// <returns> <c>true</c> if the caller has the required permissions and path contains the name of an existing file; otherwise, <c>false</c>. This method also returns false if path is nullNothingnullptra null reference (Nothing in Visual Basic), an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns false regardless of the existence of path.</returns>
        bool Exists(string path);

        /// <summary>
        /// Gets a IFileSecurityWrap object that encapsulates the access control list (ACL) entries for a specified file.
        /// </summary>
        /// <param name="path">The path to a file containing a FileSecurity object that describes the file's access control list (ACL) information.</param>
        /// <returns>A IFileSecurityWrap object that encapsulates the access control rules for the file described by the path parameter. </returns>
        IFileSecurity GetAccessControl(string path);

        /// <summary>
        /// Gets a IFileSecurityWrap object that encapsulates the access control list (ACL) entries for a specified file.
        /// </summary>
        /// <param name="path">The path to a file containing a FileSecurity object that describes the file's access control list (ACL) information.</param>
        /// <param name="includeSections">One of the AccessControlSections values that specifies the type of access control list (ACL) information to receive.</param>
        /// <returns>A IFileSecurityWrap object that encapsulates the access control rules for the file described by the path parameter.</returns>
        IFileSecurity GetAccessControl(string path, AccessControlSections includeSections);

        /// <summary>
        /// Gets the FileAttributes of the file on the path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The FileAttributes of the file on the path. </returns>
        FileAttributes GetAttributes(string path);

        /// <summary>
        /// Returns the creation date and time of the specified file or directory.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain creation date and time information.</param>
        /// <returns>A IDateTimeWrap set to the creation date and time for the specified file or directory. This value is expressed in local time.</returns>
        IDateTime GetCreationTime(string path);

        /// <summary>
        /// Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain creation date and time information.</param>
        /// <returns>A IDateTimeWrap set to the creation date and time for the specified file or directory. This value is expressed in UTC time.</returns>
        IDateTime GetCreationTimeUtc(string path);

        /// <summary>
        /// Returns the date and time the specified file or directory was last accessed.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain access date and time information.</param>
        /// <returns>A IDateTimeWrap set to the date and time that the specified file or directory was last accessed. This value is expressed in local time.</returns>
        IDateTime GetLastAccessTime(string path);

        /// <summary>
        /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain access date and time information.</param>
        /// <returns>A IDateTimeWrap set to the date and time that the specified file or directory was last accessed. This value is expressed in UTC time.</returns>
        IDateTime GetLastAccessTimeUtc(string path);

        /// <summary>
        /// Returns the date and time the specified file or directory was last written to.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain write date and time information.</param>
        /// <returns>A IDateTimeWrap set to the date and time that the specified file or directory was last written to. This value is expressed in local time.</returns>
        IDateTime GetLastWriteTime(string path);

        /// <summary>
        /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.
        /// </summary>
        /// <param name="path">The file or directory for which to obtain write date and time information.</param>
        /// <returns>A IDateTimeWrap set to the date and time that the specified file or directory was last written to. This value is expressed in UTC time. </returns>
        IDateTime GetLastWriteTimeUtc(string path);

        /// <summary>
        /// Moves a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        /// <param name="sourceFileName">The name of the file to move.</param>
        /// <param name="destFileName">The new path for the file.</param>
        void Move(string sourceFileName, string destFileName);

        /// <summary>
        /// Opens a FileStream on the specified path with read/write access.
        /// </summary>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A FileMode value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <returns>A FileStream opened in the specified mode and path, with read/write access and not shared. </returns>
        IFileStream Open(string path, FileMode mode);

        /// <summary>
        /// Opens a FileStream on the specified path, with the specified mode and access.
        /// </summary>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A FileMode value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        /// <param name="access">A FileAccess value that specifies the operations that can be performed on the file. </param>
        /// <returns>An unshared FileStream that provides access to the specified file, with the specified mode and access.</returns>
        IFileStream Open(string path, FileMode mode, FileAccess access);

        /// <summary>
        /// Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
        /// </summary>
        /// <param name="path">The file to open. </param>
        /// <param name="mode">A FileMode value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten. </param>
        /// <param name="access">A FileAccess value that specifies the operations that can be performed on the file. </param>
        /// <param name="share">A FileShare value specifying the type of access other threads have to the file. </param>
        /// <returns>A FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option. </returns>
        IFileStream Open(string path, FileMode mode, FileAccess access, FileShare share);

        /// <summary>
        /// Opens an existing file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading.</param>
        /// <returns>A read-only FileStream on the specified path. </returns>
        IFileStream OpenRead(string path);

        /// <summary>
        /// Opens an existing UTF-8 encoded text file for reading.
        /// </summary>
        /// <param name="path">The file to be opened for reading. </param>
        /// <returns>A StreamReader on the specified path. </returns>
        IStreamReader OpenText(string path);

        /// <summary>
        /// Opens an existing file for writing.
        /// </summary>
        /// <param name="path">The file to be opened for writing. </param>
        /// <returns>An unshared FileStream object on the specified path with Write access. </returns>
        IFileStream OpenWrite(string path);

        /// <summary>
        /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        byte[] ReadAllBytes(string path);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading. </param>
        /// <returns>A string array containing all lines of the file. </returns>
        string[] ReadAllLines(string path);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        /// <returns>A string array containing all lines of the file. </returns>
        string[] ReadAllLines(string path, Encoding encoding);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading. </param>
        /// <returns>A string containing all lines of the file. </returns>
        string ReadAllText(string path);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="path">The file to open for reading. </param>
        /// <param name="encoding">The encoding applied to the contents of the file. </param>
        /// <returns>A string array containing all lines of the file. </returns>
        string ReadAllText(string path, Encoding encoding);

        /// <summary>
        /// Reads the lines of a file
        /// </summary>
        /// <param name="path">The file to read.</param>
        /// <returns>All the lines of the file, or the lines that are the result of a query. </returns>
        IEnumerable<string> ReadLines(string path);

        /// <summary>
        /// Read the lines of a file that has a specific encoding.
        /// </summary>
        /// <param name="path">The file to read. </param>
        /// <param name="encoding">The encoding that is applied to the contents of the file. </param>
        /// <returns>All the lines of the file, or the lines that are the result of a query. </returns>
        IEnumerable<string> ReadLines(string path, Encoding encoding);

        /// <summary>
        /// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file.
        /// </summary>
        /// <param name="sourceFileName">The name of a file that replaces the file specified by destinationFileName. </param>
        /// <param name="destinationFileName">The name of the file being replaced. </param>
        /// <param name="destinationBackupFileName">The name of the backup file. </param>
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName);

        /// <summary>
        /// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file and optionally ignores merge errors.
        /// </summary>
        /// <param name="sourceFileName">The name of a file that replaces the file specified by destinationFileName. </param>
        /// <param name="destinationFileName">The name of the file being replaced. </param>
        /// <param name="destinationBackupFileName">The name of the backup file. </param>
        /// <param name="ignoreMetadataErrors"><c>true</c> to ignore merge errors (such as attributes and access control lists (ACLs)) from the replaced file to the replacement file; otherwise, <c>false</c></param>
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

        /// <summary>
        /// Applies access control list (ACL) entries described by a IFileSecurityWrap object to the specified file.
        /// </summary>
        /// <param name="path">A file to add or remove access control list (ACL) entries from. </param>
        /// <param name="fileSecurity">A IFileSecurityWrap object that describes an ACL entry to apply to the file described by the path parameter. </param>
        void SetAccessControl(string path, IFileSecurity fileSecurity);

        /// <summary>
        /// Sets the specified FileAttributes of the file on the specified path.
        /// </summary>
        /// <param name="path">The path to the file. </param>
        /// <param name="fileAttributes">The desired FileAttributes, such as Hidden, ReadOnly, Normal, and Archive. </param>
        void SetAttributes(string path, FileAttributes fileAttributes);

        /// <summary>
        /// Sets the date and time the file was created.
        /// </summary>
        /// <param name="path">The file for which to set the creation date and time information. </param>
        /// <param name="creationTime">A IDateTimeWrap containing the value to set for the creation date and time of path. This value is expressed in local time. </param>
        void SetCreationTime(string path, IDateTime creationTime);

        /// <summary>
        /// Sets the date and time, in coordinated universal time (UTC), that the file was created.
        /// </summary>
        /// <param name="path">The file for which to set the creation date and time information. </param>
        /// <param name="creationTimeUtc">A IDateTimeWrap containing the value to set for the creation date and time of path. This value is expressed in UTC time.</param>
        void SetCreationTimeUtc(string path, IDateTime creationTimeUtc);

        /// <summary>
        /// Sets the date and time the specified file was last accessed.
        /// </summary>
        /// <param name="path">The file for which to set the access date and time information. </param>
        /// <param name="lastAccessTime">A IDateTimeWrap containing the value to set for the last access date and time of path. This value is expressed in local time. </param>
        void SetLastAccessTime(string path, IDateTime lastAccessTime);

        /// <summary>
        /// Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.
        /// </summary>
        /// <param name="path">The file for which to set the access date and time information. </param>
        /// <param name="lastAccessTimeUtc">A IDateTimeWrap containing the value to set for the last access date and time of path. This value is expressed in UTC time. </param>
        void SetLastAccessTimeUtc(string path, IDateTime lastAccessTimeUtc);

        /// <summary>
        /// Sets the date and time that the specified file was last written to.
        /// </summary>
        /// <param name="path">The file for which to set the date and time information. </param>
        /// <param name="lastWriteTime">A IDateTimeWrap containing the value to set for the last write date and time of path. This value is expressed in local time. </param>
        void SetLastWriteTime(string path, IDateTime lastWriteTime);

        /// <summary>
        /// Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.
        /// </summary>
        /// <param name="path">The file for which to set the date and time information. </param>
        /// <param name="lastWriteTimeUtc">A IDateTimeWrap containing the value to set for the last write date and time of path. This value is expressed in UTC time. </param>
        void SetLastWriteTimeUtc(string path, IDateTime lastWriteTimeUtc);

        /// <summary>
        /// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="bytes">The bytes to write to the file.</param>
        void WriteAllBytes(string path, byte[] bytes);

        /// <summary>
        /// Creates a new file, write the specified string array to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string array to write to the file. </param>
        void WriteAllLines(string path, string[] contents);

        /// <summary>
        /// Creates a new file, writes the specified string array to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string array to write to the file. </param>
        /// <param name="encoding">An Encoding object that represents the character encoding applied to the string array. </param>
        void WriteAllLines(string path, string[] contents, Encoding encoding);

        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string to write to the file. </param>
        void WriteAllText(string path, string contents);

        /// <summary>
        /// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="path">The file to write to. </param>
        /// <param name="contents">The string to write to the file. </param>
        /// <param name="encoding">An Encoding object that represents the encoding to apply to the string. </param>
        void WriteAllText(string path, string contents, Encoding encoding);
    }
}
