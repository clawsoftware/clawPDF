using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using clawSoft.clawPDF.Utilities.Process;

namespace clawSoft.clawPDF.PrintFile
{
    /// <summary>
    ///     The PrintCommandGroup provides a way to call multiple PrintCommands at the same time.
    /// </summary>
    internal class PrintCommandGroup : IEnumerable<PrintCommand>
    {
        private readonly List<PrintCommand> _printCommands = new List<PrintCommand>();

        public ProcessWrapperFactory ProcessWrapperFactory { get; set; } = new ProcessWrapperFactory();

        /// <summary>
        ///     Cheks if all files are printable
        /// </summary>
        public bool IsPrintable
        {
            get { return _printCommands.TrueForAll(p => p.IsPrintable); }
        }

        /// <summary>
        ///     Checks if at least one file requires to change the default printer
        /// </summary>
        public bool RequiresDefaultPrinter
        {
            get { return _printCommands.Any(p => p.RequiresDefaultPrinter); }
        }

        /// <summary>
        ///     Get the PrintCommand with the given index
        /// </summary>
        /// <param name="i">index of the PrintCommand</param>
        /// <returns></returns>
        public PrintCommand this[int i]
        {
            get => _printCommands[i];
            set => _printCommands[i] = value;
        }

        /// <summary>
        ///     Get the Enumerator
        /// </summary>
        /// <returns>the Enumerator</returns>
        public IEnumerator<PrintCommand> GetEnumerator()
        {
            return _printCommands.GetEnumerator();
        }

        /// <summary>
        ///     Get the Enumerator
        /// </summary>
        /// <returns>the Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Add a print command to this group
        /// </summary>
        /// <param name="command">The PrintCommand to add</param>
        public void Add(PrintCommand command)
        {
            _printCommands.Add(command);
        }

        /// <summary>
        ///     Prints all items with a default Process timeout (60 seconds)
        /// </summary>
        /// <returns>true, if all items were converted successfully</returns>
        public bool PrintAll()
        {
            return PrintAll(TimeSpan.FromSeconds(60));
        }

        /// <summary>
        ///     Prints all items.
        /// </summary>
        /// <param name="processTimeout">The timeout the process will wait for the print job to succeed</param>
        /// <returns>true, if all items were converted successfully</returns>
        public bool PrintAll(TimeSpan processTimeout)
        {
            if (_printCommands.Any(p => p.CommandType == PrintType.Unprintable))
                throw new InvalidOperationException("The list of print commands contains unprintable files");

            foreach (var p in _printCommands)
            {
                p.ProcessWrapperFactory = ProcessWrapperFactory;

                var success = p.Print(processTimeout);
                if (!success)
                    return false;
            }

            return true;
        }

        /// <summary>
        ///     Finds all items matching the predicate
        /// </summary>
        /// <param name="match">Predicate to match</param>
        /// <returns>A list of matches</returns>
        public IList<PrintCommand> FindAll(Predicate<PrintCommand> match)
        {
            return _printCommands.FindAll(match);
        }
    }
}