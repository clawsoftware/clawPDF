using System;
using System.IO;
using System.Runtime.InteropServices;
using SystemInterface.IO;

namespace clawSoft.clawPDF.Utilities.IO
{
    [Serializable]
    [ComVisible(true)]
    public class PathWrapSafe : IPathSafe, IPath
    {
        public virtual string GetFullPath(string path)
        {
            throw new NotImplementedException();
        }

        public virtual string GetTempFileName()
        {
            throw new NotImplementedException();
        }

        public virtual string GetTempPath()
        {
            throw new NotImplementedException();
        }

        public char AltDirectorySeparatorChar => Path.AltDirectorySeparatorChar;

        public char DirectorySeparatorChar => Path.DirectorySeparatorChar;

        public char PathSeparator => Path.PathSeparator;

        public char VolumeSeparatorChar => Path.VolumeSeparatorChar;

        public string ChangeExtension(string path, string extension)
        {
            return Path.ChangeExtension(path, extension);
        }

        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public string Combine(string path1, string path2, string path3)
        {
            return Path.Combine(path1, path2, path3);
        }

        public string Combine(string path1, string path2, string path3, string path4)
        {
            return Path.Combine(path1, path2, path3, path4);
        }

        public string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public string GetExtension(string path)
        {
            return Path.GetExtension(path);
        }

        public string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public char[] GetInvalidFileNameChars()
        {
            return Path.GetInvalidFileNameChars();
        }

        public char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        public string GetPathRoot(string path)
        {
            return Path.GetPathRoot(path);
        }

        public string GetRandomFileName()
        {
            return Path.GetRandomFileName();
        }

        public bool HasExtension(string path)
        {
            return Path.HasExtension(path);
        }

        public bool IsPathRooted(string path)
        {
            return Path.IsPathRooted(path);
        }
    }
}