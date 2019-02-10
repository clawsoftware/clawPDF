using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace clawSoft.clawPDF.Utilities.IO
{
    /// <summary>
    ///     Helper class to clean a folder with files older then a specific date
    /// </summary>
    public class FolderCleaner
    {
        /// <summary>
        ///     Build a new FolderCleander with the given path. The folder itself will NOT be deleted during cleanup.
        /// </summary>
        /// <param name="cleanupFolder">The path to be cleaned</param>
        public FolderCleaner(string cleanupFolder)
        {
            CleanupFolder = cleanupFolder;
        }

        public string CleanupFolder { get; }

        public Dictionary<string, Exception> CleanupExceptions { get; } = new Dictionary<string, Exception>();

        /// <summary>
        ///     Clean all files in the configured folder. The folder itself will NOT be deleted during cleanup.
        ///     If exceptions occur while cleaning up, they will be stored in CleanupExceptions.
        /// </summary>
        public void Clean()
        {
            Clean(TimeSpan.Zero);
        }

        /// <summary>
        ///     Clean all files in the configured folder. The folder itself will NOT be deleted during cleanup.
        ///     If exceptions occur while cleaning up, they will be stored in CleanupExceptions.
        ///     <param name="minAge">The minimum TimeSpan between file creation date and current time.</param>
        /// </summary>
        public void Clean(TimeSpan minAge)
        {
            if (!Directory.Exists(CleanupFolder))
                return;

            Clean(CleanupFolder, minAge);
        }

        private void Clean(string folder, TimeSpan minAge)
        {
            var folders = new DirectoryInfo(folder).GetDirectories();
            foreach (var dir in folders)
            {
                var subFolder = dir.FullName;
                Clean(subFolder, minAge);
                if (!Directory.EnumerateFileSystemEntries(subFolder).Any())
                    try
                    {
                        Directory.Delete(subFolder);
                    }
                    catch (Exception ex)
                    {
                        HandleException(subFolder, ex);
                    }
            }

            var files = new DirectoryInfo(folder).GetFiles();
            foreach (var file in files)
            {
                var fileAge = DateTime.UtcNow - file.CreationTimeUtc;
                if (fileAge >= minAge)
                    try
                    {
                        File.Delete(file.FullName);
                    }
                    catch (Exception ex)
                    {
                        HandleException(file.FullName, ex);
                    }
            }
        }

        private void HandleException(string path, Exception ex)
        {
            CleanupExceptions[path] = ex;
        }
    }
}