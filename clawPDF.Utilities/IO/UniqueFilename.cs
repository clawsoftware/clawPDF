using System;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Utilities.IO
{
    /// <summary>
    ///     Creates a file path for a file that does not exist yet. It takes a path and appends a counting number (_2, _3, etc)
    ///     to ensure this in a readable way.
    /// </summary>
    public class UniqueFilename
    {
        private readonly string _directory;

        private readonly IDirectory _directoryWrap;
        private readonly string _extension;
        private readonly string _fileBody;
        private readonly IFile _fileWrap;
        private readonly IPathSafe _pathSafe = new PathWrapSafe();

        // this is the counting number that is appended to the filename
        // starting with 2 to name the first duplicate somename_2
        private int _appendix = 2;

        public UniqueFilename(string originalFilename) : this(originalFilename, new DirectoryWrap(), new FileWrap())
        {
        }

        public UniqueFilename(string originalFilename, IDirectory directory, IFile file)
        {
            if (originalFilename == null)
                throw new ArgumentNullException("originalFilename");

            _directoryWrap = directory;
            _fileWrap = file;

            OriginalFilename = originalFilename;
            LastUniqueFilename = originalFilename;
            _directory = _pathSafe.GetDirectoryName(OriginalFilename) ?? "";
            _fileBody = _pathSafe.GetFileNameWithoutExtension(OriginalFilename);
            _extension = _pathSafe.GetExtension(OriginalFilename);
        }

        public string OriginalFilename { get; }
        public string LastUniqueFilename { get; private set; }

        /// <summary>
        ///     Creates a file path for a file that does not exist yet. It takes a path and appends a counting number (_2, _3, etc)
        ///     to ensure this in a readable way.
        /// </summary>
        /// <returns>A unique filename</returns>
        public string CreateUniqueFileName()
        {
            while (_fileWrap.Exists(LastUniqueFilename) || _directoryWrap.Exists(LastUniqueFilename))
            {
                LastUniqueFilename = _pathSafe.Combine(_directory, _fileBody + "_" + _appendix + _extension);
                _appendix++;
                if (LastUniqueFilename.Length > FileUtil.MAX_PATH)
                    LastUniqueFilename = FileUtil.Instance.EllipsisForTooLongPath(LastUniqueFilename);
            }

            return LastUniqueFilename;
        }
    }
}