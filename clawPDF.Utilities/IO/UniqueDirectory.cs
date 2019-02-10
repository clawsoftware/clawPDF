using System;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Utilities.IO
{
    /// <summary>
    ///     Creates a directory that does not exist yet. It takes a path and appends a counting number (_2, _3, etc) to ensure
    ///     this in a readable way.
    /// </summary>
    public class UniqueDirectory
    {
        private readonly IDirectory _directoryWrap;
        private readonly IFile _fileWrap;
        private readonly IPathSafe _pathSafe = new PathWrapSafe();
        private string _path;

        public UniqueDirectory(string path) : this(path, new DirectoryWrap(), new FileWrap())
        {
        }

        public UniqueDirectory(string path, IDirectory directory, IFile fileWrap)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            _path = path;

            _directoryWrap = directory;
            _fileWrap = fileWrap;
        }

        /// <summary>
        ///     Creates a directory that does not exist yet. It takes a path and appends a counting number (_2, _3, etc) to ensure
        ///     this in a readable way.
        /// </summary>
        /// <returns>The uniqified directory path</returns>
        public string MakeUniqueDirectory()
        {
            var directory = _pathSafe.GetDirectoryName(_path) ?? "";
            var fileBody = _pathSafe.GetFileName(_path);

            var i = 2;

            while (_directoryWrap.Exists(_path) || _fileWrap.Exists(_path))
            {
                _path = _pathSafe.Combine(directory, fileBody + "_" + i);
                i++;
            }

            return _path;
        }
    }
}