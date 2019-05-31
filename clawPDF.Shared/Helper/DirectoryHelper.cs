using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Shared.Helper
{
    public class DirectoryHelper
    {
        private readonly IDirectoryInfo _directoryInfo;

        private readonly IDirectory _directoryWrap;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private List<string> _createdDirectories;

        public DirectoryHelper(IDirectoryInfo directoryInfoWrap, IDirectory directoryWrap)
        {
            _directoryInfo = directoryInfoWrap;
            _directoryWrap = directoryWrap;
            _createdDirectories = new List<string>();
        }

        public DirectoryHelper(string directory) : this(new DirectoryInfoWrap(directory), new DirectoryWrap())
        {
        }

        public string Directory => _directoryInfo.FullName;

        public virtual List<string> CreatedDirectories => _createdDirectories;

        /// <summary>
        ///     Returns a list that contains all parent directories of the directory
        /// </summary>
        public List<string> GetDirectoryTree()
        {
            var directoryTree = new List<string>();
            var currentDirectory = _directoryInfo;
            while (currentDirectory != null) //while directory has parent
            {
                directoryTree.Add(currentDirectory.FullName);
                currentDirectory = _directoryWrap.GetParent(currentDirectory.FullName);
            }

            return directoryTree;
        }

        /// <summary>
        ///     Creates the directory and stores in "CreatedDirectories" which parent directories hab to be created
        /// </summary>
        /// <returns>false if creating directory throws exception</returns>
        public bool CreateDirectory()
        {
            var directoryTree = GetDirectoryTree();
            directoryTree = directoryTree.OrderBy(x => x).ToList(); //start with smallest

            foreach (var directory in directoryTree)
                if (!_directoryWrap.Exists(directory))
                {
                    try
                    {
                        _directoryWrap.CreateDirectory(directory);
                        _logger.Debug("Created directory " + directory);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("Exception while creating \"" + directory + "\"\r\n" + ex.Message);
                        return false;
                    }

                    _createdDirectories.Add(directory);
                }

            return true;
        }

        /// <summary>
        ///     Deletes all parent directories that had to be created to reach the requested directory.
        ///     If a parent directory contains files, it will not be deleted, likewise further parent directories.
        /// </summary>
        /// <returns>false if deleting directory throws exception</returns>
        public bool DeleteCreatedDirectories()
        {
            _createdDirectories = CreatedDirectories.OrderByDescending(x => x).ToList(); //start with tallest
            foreach (var createdDirectory in _createdDirectories)
            {
                if (!_directoryWrap.Exists(createdDirectory))
                    continue;

                if (_directoryWrap.EnumerateFiles(createdDirectory).Any())
                    break;

                try
                {
                    _directoryWrap.Delete(createdDirectory);
                    _logger.Debug("Deleted previously created bust unused directory " + createdDirectory);
                }
                catch (Exception ex)
                {
                    _logger.Warn("Exception while deleting created bust unused directory \"" + createdDirectory +
                                 "\"\r\n" + ex.Message);
                    return false;
                }
            }

            return true;
        }
    }
}