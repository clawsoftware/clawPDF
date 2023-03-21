using System;
using System.ComponentModel;
using System.IO;
using SystemInterface.IO;

namespace SystemWrapper.IO
{
    /// <summary>
    /// Wrapper for <see cref="FileSystemWatcher"/> class.
    /// </summary>
    public class FileSystemWatcherWrap : IFileSystemWatcher
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref='System.IO.FileSystemWatcher'/> class.
        /// </summary>
        public FileSystemWatcherWrap()
        {
            Initialize();
        }

        /// <summary>
        ///       Initializes a new instance of the <see cref='System.IO.FileSystemWatcher'/> class,
        ///       given the specified directory to monitor.
        /// </summary>
        public FileSystemWatcherWrap(string path)
            : this(path, "*.*")
        {
        }

        /// <summary>
        ///       Initializes a new instance of the <see cref='System.IO.FileSystemWatcher'/> class,
        ///       given the specified directory and type of files to monitor.
        /// </summary>
        public FileSystemWatcherWrap(string path, string filter)
        {
            Initialize(path, filter);
        }

        /// <inheritdoc />
        public void Initialize()
        {
            FileSystemWatcherInstance = new FileSystemWatcher();
        }

        /// <inheritdoc />
        public void Initialize(string path, string filter)
        {
            FileSystemWatcherInstance = new FileSystemWatcher(path, filter);
        }

        #endregion Constructors and Initializers

        #region IFileSystemWatcherWrap Members

        /// <inheritdoc />
        public FileSystemWatcher FileSystemWatcherInstance { get; private set; }

        /// <summary>
        ///       Gets or sets the type of changes to watch for.
        /// </summary>
        [DefaultValue(NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName)]
        public NotifyFilters NotifyFilter
        {
            get { return FileSystemWatcherInstance.NotifyFilter; }
            set { FileSystemWatcherInstance.NotifyFilter = value; }
        }

        /// <summary>
        ///    Gets or sets a value indicating whether the component is enabled.
        /// </summary>
        [DefaultValue(false)]
        public bool EnableRaisingEvents
        {
            get { return FileSystemWatcherInstance.EnableRaisingEvents; }
            set { FileSystemWatcherInstance.EnableRaisingEvents = value; }
        }

        /// <summary>
        ///    Gets or sets the filter string, used to determine what files are monitored in a directory.
        /// </summary>
        [DefaultValue("*.*")]
        public string Filter
        {
            get { return FileSystemWatcherInstance.Filter; }
            set { FileSystemWatcherInstance.Filter = value; }
        }

        /// <summary>
        ///       Gets or sets a
        ///       value indicating whether subdirectories within the specified path should be monitored.
        /// </summary>
        [DefaultValue(false)]
        public bool IncludeSubdirectories
        {
            get { return FileSystemWatcherInstance.IncludeSubdirectories; }
            set { FileSystemWatcherInstance.IncludeSubdirectories = value; }
        }

        /// <summary>
        ///    Gets or
        ///       sets the size of the internal buffer.
        /// </summary>
        [DefaultValue(8192)]
        public int InternalBufferSize
        {
            get { return FileSystemWatcherInstance.InternalBufferSize; }
            set { FileSystemWatcherInstance.InternalBufferSize = value; }
        }

        /// <summary>
        ///    Gets or sets the path of the directory to watch.
        /// </summary>
        [DefaultValue("")]
        public string Path
        {
            get { return FileSystemWatcherInstance.Path; }
            set { FileSystemWatcherInstance.Path = value; }
        }

        /// <summary>
        ///       Gets or sets the object used to marshal the event handler calls issued as a
        ///       result of a directory change.
        /// </summary>
        [DefaultValue(null)]
        public ISynchronizeInvoke SynchronizingObject
        {
            get { return FileSystemWatcherInstance.SynchronizingObject; }
            set { FileSystemWatcherInstance.SynchronizingObject = value; }
        }

        /// <summary>
        ///    Notifies the object that initialization is beginning and tells it to standby.
        /// </summary>
        public void BeginInit()
        {
            FileSystemWatcherInstance.BeginInit();
        }

        /// <summary>
        ///
        ///       Notifies the object that initialization is complete.
        ///
        /// </summary>
        public void EndInit()
        {
            FileSystemWatcherInstance.EndInit();
        }

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is changed.
        /// </summary>
        public event FileSystemEventHandler Changed
        {
            add { FileSystemWatcherInstance.Changed += value; }
            remove { FileSystemWatcherInstance.Changed -= value; }
        }

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is created.
        /// </summary>
        public event FileSystemEventHandler Created
        {
            add { FileSystemWatcherInstance.Created += value; }
            remove { FileSystemWatcherInstance.Created -= value; }
        }

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is deleted.
        /// </summary>
        public event FileSystemEventHandler Deleted
        {
            add { FileSystemWatcherInstance.Deleted += value; }
            remove { FileSystemWatcherInstance.Deleted -= value; }
        }

        /// <summary>
        ///       Occurs when the internal buffer overflows.
        /// </summary>
        public event ErrorEventHandler Error
        {
            add { FileSystemWatcherInstance.Error += value; }
            remove { FileSystemWatcherInstance.Error -= value; }
        }

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is renamed.
        /// </summary>
        public event RenamedEventHandler Renamed
        {
            add { FileSystemWatcherInstance.Renamed += value; }
            remove { FileSystemWatcherInstance.Renamed -= value; }
        }

        /// <summary>
        ///       A synchronous method that returns a structure that
        ///       contains specific information on the change that occurred, given the type
        ///       of change that you wish to monitor.
        /// </summary>
        public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
        {
            return FileSystemWatcherInstance.WaitForChanged(changeType);
        }

        /// <summary>
        ///       A synchronous
        ///       method that returns a structure that contains specific information on the change that occurred, given the
        ///       type of change that you wish to monitor and the time (in milliseconds) to wait before timing out.
        /// </summary>
        public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
        {
            return FileSystemWatcherInstance.WaitForChanged(changeType, timeout);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IFileSystemWatcherWrap Members

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (FileSystemWatcherInstance != null)
                {
                    FileSystemWatcherInstance.Dispose();
                    FileSystemWatcherInstance = null;
                }

                // get rid of managed resources
            } // get rid of unmanaged resources
        }

        /// <inheritdoc />
        ~FileSystemWatcherWrap()
        {
            Dispose(false);
        }
    }
}