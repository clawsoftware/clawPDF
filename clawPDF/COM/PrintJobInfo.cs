using System.Runtime.InteropServices;
using clawSoft.clawPDF.Core.Jobs;

namespace clawSoft.clawPDF.COM
{
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("E0F6B8E7-8E89-400C-B623-67EFB0A7A9A0")]
    public interface IPrintJobInfo
    {
        string PrintJobName { get; set; }
        string PrintJobAuthor { get; set; }
        string Subject { get; set; }
        string Keywords { get; set; }
        string Producer { get; }
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class PrintJobInfo : IPrintJobInfo
    {
        private readonly Metadata _metadata;

        public PrintJobInfo(ref Metadata metadata)
        {
            if (metadata == null) throw new COMException("This job doesn't have any metadata.");

            _metadata = metadata;
        }

        /// <summary>
        ///     Title from PrintJob
        /// </summary>
        public string PrintJobName
        {
            get => _metadata.PrintJobName;
            set => _metadata.PrintJobName = value;
        }

        /// <summary>
        ///     Author from PrintJob
        /// </summary>
        public string PrintJobAuthor
        {
            get => _metadata.PrintJobAuthor;
            set => _metadata.PrintJobAuthor = value;
        }

        /// <summary>
        ///     Subject of the document
        /// </summary>
        public string Subject
        {
            get => _metadata.Subject;
            set => _metadata.Subject = value;
        }

        /// <summary>
        ///     Keywords that describe the document
        /// </summary>
        public string Keywords
        {
            get => _metadata.Keywords;
            set => _metadata.Keywords = value;
        }

        /// <summary>
        ///     Name of the application that produced the document
        /// </summary>
        public string Producer => _metadata.Producer;
    }
}