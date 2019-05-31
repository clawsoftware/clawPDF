using System.Collections.Generic;
using System.Linq;
using System.Windows;
using clawSoft.clawPDF.Assistants;
using clawSoft.clawPDF.Shared.Helper;
using NLog;
using SystemInterface.IO;
using SystemWrapper.IO;

namespace clawSoft.clawPDF.Helper
{
    public static class DragAndDropHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Returns true if no argument starts with "/"
        /// </summary>
        public static bool IsDragAndDrop(ICollection<string> args)
        {
            if (args == null)
                return false;

            if (!args.Any())
                return false;

            return !args.Any(x => x.StartsWith("/"));
        }

        /// <summary>
        ///     Sets the DragDropEffect to Copy for a FileDrop
        /// </summary>
        public static void DragEnter(DragEventArgs e)
        {
            if (((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 0)
                e.Effects = DragDropEffects.None;
            else
                e.Effects = DragDropEffects.Copy;
        }

        /// <summary>
        ///     Removes invalid files, adds the files that do not need to be printed to the current JobInfoQueue
        ///     and launches print jobs for the remaining files.
        /// </summary>
        public static void Drop(DragEventArgs e)
        {
            var droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            Drop(droppedFiles);
        }

        /// <summary>
        ///     Removes invalid files, adds the files that do not need to be printed to the current JobInfoQueue
        ///     and launches print jobs for the remaining files.
        /// </summary>
        public static void Drop(ICollection<string> droppedFiles)
        {
            Logger.Debug("Launched Drag & Drop");
            var validFiles = RemoveInvalidFiles(droppedFiles);
            AddFilesToJobInfoQueue(validFiles);
            PrintPrintableFiles(validFiles);
        }

        private static bool IsPrintFile(string file)
        {
            if (file.EndsWith(".ps"))
                return false;

            if (file.EndsWith(".pdf"))
                return false;

            return true;
        }

        /// <summary>
        ///     Removes not existing files from dropped files list.
        /// </summary>
        public static ICollection<string> RemoveInvalidFiles(IEnumerable<string> droppedFiles)
        {
            return RemoveInvalidFiles(droppedFiles, new FileWrap());
        }

        public static ICollection<string> RemoveInvalidFiles(IEnumerable<string> droppedFiles, IFile fileWrap)
        {
            var validFiles = new List<string>();
            foreach (var file in droppedFiles)
            {
                if (!fileWrap.Exists(file))
                {
                    Logger.Warn("The file " + file + " does not exist.");
                    continue;
                }

                validFiles.Add(file);
            }

            return validFiles;
        }

        /// <summary>
        ///     Adds files that do not have to be printed to the current JobInfoQueue.
        /// </summary>
        /// <param name="droppedFiles"></param>
        public static void AddFilesToJobInfoQueue(IEnumerable<string> droppedFiles)
        {
            foreach (var file in droppedFiles)
            {
                if (IsPrintFile(file))
                    continue;

                var infFile = PsFileHelper.TransformToInfFile(file, JobInfoQueue.Instance.SpoolFolder,
                    SettingsHelper.Settings.ApplicationSettings.PrimaryPrinter);
                JobInfoQueue.Instance.Add(infFile);
            }
        }

        /// <summary>
        ///     Launches a print job for all dropped files that can be printed.
        /// </summary>
        public static void PrintPrintableFiles(IEnumerable<string> droppedFiles)
        {
            var launchPrintAll = false;
            var printFileAssistant = new PrintFileAssistant();
            foreach (var file in droppedFiles)
            {
                if (!IsPrintFile(file))
                    continue;

                if (!printFileAssistant.AddFile(file))
                {
                    Logger.Warn("The file " + file + " is not printable.");
                    continue;
                }

                launchPrintAll = true;
            }

            if (launchPrintAll)
                printFileAssistant.PrintAll();
        }
    }
}