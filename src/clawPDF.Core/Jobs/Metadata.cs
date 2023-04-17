using clawSoft.clawPDF.Utilities;

namespace clawSoft.clawPDF.Core.Jobs
{
    /// <summary>
    ///     The Metadata class holds information about the printed document
    /// </summary>
    public class Metadata
    {
        private readonly IAssemblyHelper _assemblyHelper;

        public Metadata(IAssemblyHelper assemblyHelper = null)
        {
            _assemblyHelper = assemblyHelper ?? new AssemblyHelper();
        }

        /// <summary>
        ///     Title from PrintJob
        /// </summary>
        public string PrintJobName { get; set; }

        /// <summary>
        ///     Title of the document
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Author from PrintJob
        /// </summary>
        public string PrintJobAuthor { get; set; }

        /// <summary>
        ///     Name of the Author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Subject of the document
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     Keywords that describe the document
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        ///     Name of the application that produced the document
        /// </summary>
        public string Producer => "clawPDF " + _assemblyHelper.GetCurrentAssemblyVersion();

        /// <summary>
        ///     Create an exact copy of this object. Changes to the copy will not affect the original and vice versa
        /// </summary>
        /// <returns>An exact copy of the object</returns>
        public Metadata Copy()
        {
            var metadata = new Metadata
            {
                PrintJobName = PrintJobName,
                Title = Title,
                Author = Author,
                PrintJobAuthor = PrintJobAuthor,
                Subject = Subject,
                Keywords = Keywords
            };

            return metadata;
        }
    }
}