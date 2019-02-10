using System.Collections.Generic;
using clawSoft.clawPDF.Helper;
using NLog;

namespace clawSoft.clawPDF.Startup
{
    internal class DragAndDropStart : MaybePipedStart
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public DragAndDropStart(ICollection<string> droppedFiles)
        {
            _logger.Debug("Launched Drag & Drop");
            DroppedFiles = droppedFiles;
        }

        public ICollection<string> DroppedFiles { get; }

        internal override string ComposePipeMessage()
        {
            return "DragAndDrop|" + string.Join("|", DroppedFiles);
        }

        internal override bool StartApplication()
        {
            DragAndDropHelper.PrintPrintableFiles(DroppedFiles);
            DragAndDropHelper.AddFilesToJobInfoQueue(DroppedFiles);
            return true;
        }
    }
}