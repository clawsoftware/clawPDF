using System;
using System.IO;
using System.Runtime.InteropServices;
using SystemInterface.IO;

namespace SystemWrapper.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.Path"/> class.
    /// </summary>
    [Serializable, ComVisible(true)]
    public class PathWrap : IPath
    {
        /// <inheritdoc />
        public char AltDirectorySeparatorChar
        {
            get { return Path.AltDirectorySeparatorChar; }
        }

        /// <inheritdoc />
        public char DirectorySeparatorChar
        {
            get { return Path.DirectorySeparatorChar; }
        }

        /// <inheritdoc />
        public char PathSeparator
        {
            get { return Path.PathSeparator; }
        }

        /// <inheritdoc />
        public char VolumeSeparatorChar
        {
            get { return Path.VolumeSeparatorChar; }
        }

        /// <inheritdoc />
        public string ChangeExtension(string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        /// <inheritdoc />
        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        /// <inheritdoc />
        public string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        /// <inheritdoc />
        public string Combine(string path1, string path2, string path3)
        {
            return Path.Combine(path1, path2, path3);
        }

        /// <inheritdoc />
        public string Combine(string path1, string path2, string path3, string path4)
        {
            return Path.Combine(path1, path2, path3, path4);
        }

        /// <inheritdoc />
        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        /// <inheritdoc />
        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        /// <inheritdoc />
        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <inheritdoc />
        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        /// <inheritdoc />
        public string GetFullPath(string path)
        {
            return Path.GetFullPath(path);
        }

        /// <inheritdoc />
        public char[] GetInvalidFileNameChars()
        {
            return Path.GetInvalidFileNameChars();
        }

        /// <inheritdoc />
        public char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        /// <inheritdoc />
        public string GetPathRoot(string path)
        {
            return Path.GetPathRoot(path);
        }

        /// <inheritdoc />
        public string GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        /// <inheritdoc />
        public string GetTempFileName()
        {
            return Path.GetTempFileName();
        }

        /// <inheritdoc />
        public string GetTempPath()
        {
            return Path.GetTempPath();
        }

        /// <inheritdoc />
        public bool HasExtension(string path)
        {
            return Path.HasExtension(path);
        }

        /// <inheritdoc />
        public bool IsPathRooted(string path)
        {
            return Path.IsPathRooted(path);
        }
    }
}