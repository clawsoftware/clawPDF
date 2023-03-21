namespace SystemWrapper.IO
{
    using SystemInterface.IO;

    public class FileSystemWatcherFactory : IFileSystemWatcherFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new instance of the <see cref='IFileSystemWatcher'/> class.
        /// </summary>
        /// <returns>
        ///     The <see cref="IFileSystemWatcher"/>.
        /// </returns>
        public IFileSystemWatcher Create()
        {
            return new FileSystemWatcherWrap();
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="System.IO.FileSystemWatcher"/> class,
        ///     given the specified directory to monitor.  
        /// </summary>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <returns>
        ///     The <see cref="IFileSystemWatcher"/>.
        /// </returns>
        public IFileSystemWatcher Create(string path)
        {
            return new FileSystemWatcherWrap(path);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref='System.IO.FileSystemWatcher'/> class,
        ///     given the specified directory and type of files to monitor.
        /// </summary>
        /// <param name="path">
        ///     The path.
        /// </param>
        /// <param name="filter">
        ///     The filter.
        /// </param>
        /// <returns>
        ///     The <see cref="IFileSystemWatcher"/>.
        /// </returns>
        public IFileSystemWatcher Create(string path,
                                         string filter)
        {
            return new FileSystemWatcherWrap(path, filter);
        }

        #endregion
    }
}