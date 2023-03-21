using System;
using System.ComponentModel;
using System.IO;

namespace SystemInterface.IO
{
    /// <summary>
    /// Wrapper for <see cref="T:System.IO.FileSystemWatcher"/> class.
    /// </summary>
    public interface IFileSystemWatcher : ISupportInitialize, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileSystemWatcherWrap"/> class on the specified path.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.IO.FileSystemWatcherWrap"/> class on the specified path.
        /// </summary>
        /// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation.</param>
        /// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files.</param>
        void Initialize(string path, string filter);

        /// <summary>
        /// Gets instance of FileSystemWatcher object.
        /// </summary>
        FileSystemWatcher FileSystemWatcherInstance { get; }

        /// <summary>
        ///       Gets or sets the type of changes to watch for.
        /// </summary>
        [DefaultValue(NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName)]
        NotifyFilters NotifyFilter { get; set; }

        /// <summary>
        ///    Gets or sets a value indicating whether the component is enabled.
        /// </summary>
        [DefaultValue(false)]
        bool EnableRaisingEvents { get; set; }

        /// <summary>
        ///    Gets or sets the filter string, used to determine what files are monitored in a directory.
        /// </summary>
        [DefaultValue("*.*")]
        string Filter { get; set; }

        /// <summary>
        ///       Gets or sets a
        ///       value indicating whether subdirectories within the specified path should be monitored.
        /// </summary>
        [DefaultValue(false)]
        bool IncludeSubdirectories { get; set; }

        /// <summary>
        ///    Gets or
        ///       sets the size of the internal buffer.
        /// </summary>
        [DefaultValue(8192)]
        int InternalBufferSize { get; set; }

        /// <summary>
        ///    Gets or sets the path of the directory to watch.
        /// </summary>
        [DefaultValue("")]
        string Path { get; set; }

        /// <summary>
        ///       Gets or sets the object used to marshal the event handler calls issued as a
        ///       result of a directory change.
        /// </summary>
        [DefaultValue(null)]
        ISynchronizeInvoke SynchronizingObject { get; set; }

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is changed.
        /// </summary>
        event FileSystemEventHandler Changed;

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is created.
        /// </summary>
        event FileSystemEventHandler Created;

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is deleted.
        /// </summary>
        event FileSystemEventHandler Deleted;

        /// <summary>
        ///       Occurs when the internal buffer overflows.
        /// </summary>
        event ErrorEventHandler Error;

        /// <summary>
        ///       Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path'/>
        ///       is renamed.
        /// </summary>
        event RenamedEventHandler Renamed;

        /// <summary>
        ///       A synchronous method that returns a structure that
        ///       contains specific information on the change that occurred, given the type
        ///       of change that you wish to monitor.
        /// </summary>
        WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType);

        /// <summary>
        ///       A synchronous
        ///       method that returns a structure that contains specific information on the change that occurred, given the
        ///       type of change that you wish to monitor and the time (in milliseconds) to wait before timing out.
        /// </summary>
        WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout);
    }
}
