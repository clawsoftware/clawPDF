using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using SystemInterface;
using SystemInterface.IO;
using SystemInterface.Security.AccessControl;
using SystemWrapper.Security.AccessControl;

namespace SystemWrapper.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.File"/> class.
    /// </summary>
    [Serializable, ComVisible(true)]
    public class FileWrap : IFile {
        /// <inheritdoc />
        public void AppendAllLines(string path, IEnumerable<string> contents) 
        {
            File.AppendAllLines(path, contents);
        }

        /// <inheritdoc />
        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding) 
        {
            File.AppendAllLines(path, contents, encoding);
        }

        /// <inheritdoc />
        public void AppendAllText(string path, string contents)
        {
            File.AppendAllText(path, contents);
        }

        /// <inheritdoc />
        public void AppendAllText(string path, string contents, Encoding encoding)
        {
            File.AppendAllText(path, contents, encoding);
        }

        /// <inheritdoc />
        public IStreamWriter AppendText(string path)
        {
            return new StreamWriterWrap(File.AppendText(path));
        }

        /// <inheritdoc />
        public void Copy(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        /// <inheritdoc />
        public void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <inheritdoc />
        public IFileStream Create(string path)
        {
            return new FileStreamWrap(File.Create(path));
        }

        /// <inheritdoc />
        public IFileStream Create(string path, int bufferSize)
        {
            return new FileStreamWrap(File.Create(path, bufferSize));
        }

        /// <inheritdoc />
        public IFileStream Create(string path, int bufferSize, FileOptions options)
        {
            return new FileStreamWrap(File.Create(path, bufferSize, options));
        }

        /// <inheritdoc />
        public IFileStream Create(string path, int bufferSize, FileOptions options, IFileSecurity fileSecurity)
        {
            return new FileStreamWrap(File.Create(path, bufferSize, options, fileSecurity.FileSecurityInstance));
        }

        /// <inheritdoc />
        public IStreamWriter CreateText(string path)
        {
            return new StreamWriterWrap(File.CreateText(path));
        }

        /// <inheritdoc />
        public void Decrypt(string path)
        {
            File.Decrypt(path);
        }

        /// <inheritdoc />
        public void Delete(string path)
        {
            File.Delete(path);
        }

        /// <inheritdoc />
        public void Encrypt(string path)
        {
            File.Encrypt(path);
        }

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc />
        public IFileSecurity GetAccessControl(string path)
        {
            return new FileSecurityWrap(File.GetAccessControl(path));
        }

        /// <inheritdoc />
        public IFileSecurity GetAccessControl(string path, AccessControlSections includeSections)
        {
            return new FileSecurityWrap(File.GetAccessControl(path, includeSections));
        }

        /// <inheritdoc />
        public FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        /// <inheritdoc />
        public IDateTime GetCreationTime(string path)
        {
            return new DateTimeWrap(File.GetCreationTime(path));
        }

        /// <inheritdoc />
        public IDateTime GetCreationTimeUtc(string path)
        {
            return new DateTimeWrap(File.GetCreationTimeUtc(path));
        }

        /// <inheritdoc />
        public IDateTime GetLastAccessTime(string path)
        {
            return new DateTimeWrap(File.GetLastAccessTime(path));
        }

        /// <inheritdoc />
        public IDateTime GetLastAccessTimeUtc(string path)
        {
            return new DateTimeWrap(File.GetLastAccessTimeUtc(path));
        }

        /// <inheritdoc />
        public IDateTime GetLastWriteTime(string path)
        {
            return new DateTimeWrap(File.GetLastWriteTime(path));
        }

        /// <inheritdoc />
        public IDateTime GetLastWriteTimeUtc(string path)
        {
            return new DateTimeWrap(File.GetLastWriteTimeUtc(path));
        }

        /// <inheritdoc />
        public void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        /// <inheritdoc />
        public IFileStream Open(string path, FileMode mode)
        {
            return new FileStreamWrap(File.Open(path, mode));
        }

        /// <inheritdoc />
        public IFileStream Open(string path, FileMode mode, FileAccess access)
        {
            return new FileStreamWrap(File.Open(path, mode, access));
        }

        /// <inheritdoc />
        public IFileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return new FileStreamWrap(File.Open(path, mode, access, share));
        }

        /// <inheritdoc />
        public IFileStream OpenRead(string path)
        {
            return new FileStreamWrap(File.OpenRead(path));
        }

        /// <inheritdoc />
        public IStreamReader OpenText(string path)
        {
            return new StreamReaderWrap(File.OpenText(path));
        }

        /// <inheritdoc />
        public IFileStream OpenWrite(string path)
        {
            return new FileStreamWrap(File.OpenWrite(path));
        }

        /// <inheritdoc />
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        /// <inheritdoc />
        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        /// <inheritdoc />
        public string[] ReadAllLines(string path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding);
        }

        /// <inheritdoc />
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        /// <inheritdoc />
        public string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        /// <inheritdoc />
        public IEnumerable<string> ReadLines(string path) 
        {
            return File.ReadLines(path);
        }
        /// <inheritdoc />
        public IEnumerable<string> ReadLines(string path, Encoding encoding) 
        {
            return File.ReadLines(path, encoding);
        }

        /// <inheritdoc />
        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
        {
            File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);
        }

        /// <inheritdoc />
        public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
        }

        /// <inheritdoc />
        public void SetAccessControl(string path, IFileSecurity fileSecurity)
        {
            File.SetAccessControl(path, fileSecurity.FileSecurityInstance);
        }

        /// <inheritdoc />
        public void SetAttributes(string path, FileAttributes fileAttributes)
        {
            File.SetAttributes(path, fileAttributes);
        }

        /// <inheritdoc />
        public void SetCreationTime(string path, IDateTime creationTime)
        {
            File.SetCreationTime(path, creationTime.DateTimeInstance);
        }

        /// <inheritdoc />
        public void SetCreationTimeUtc(string path, IDateTime creationTimeUtc)
        {
            File.SetCreationTimeUtc(path, creationTimeUtc.DateTimeInstance);
        }

        /// <inheritdoc />
        public void SetLastAccessTime(string path, IDateTime lastAccessTime)
        {
            File.SetLastAccessTime(path, lastAccessTime.DateTimeInstance);
        }

        /// <inheritdoc />
        public void SetLastAccessTimeUtc(string path, IDateTime lastAccessTimeUtc)
        {
            File.SetLastAccessTimeUtc(path, lastAccessTimeUtc.DateTimeInstance);
        }

        /// <inheritdoc />
        public void SetLastWriteTime(string path, IDateTime lastWriteTime)
        {
            File.SetLastWriteTime(path, lastWriteTime.DateTimeInstance);
        }

        /// <inheritdoc />
        public void SetLastWriteTimeUtc(string path, IDateTime lastWriteTimeUtc)
        {
            File.SetLastWriteTimeUtc(path, lastWriteTimeUtc.DateTimeInstance);
        }

        /// <inheritdoc />
        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        /// <inheritdoc />
        public void WriteAllLines(string path, string[] contents)
        {
            File.WriteAllLines(path, contents);
        }

        /// <inheritdoc />
        public void WriteAllLines(string path, string[] contents, Encoding encoding)
        {
            File.WriteAllLines(path, contents, encoding);
        }

        /// <inheritdoc />
        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        /// <inheritdoc />
        public void WriteAllText(string path, string contents, Encoding encoding)
        {
            File.WriteAllText(path, contents, encoding);
        }
    }
}
