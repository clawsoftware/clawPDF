using clawSoft.clawPDF.Threading;
using clawSoft.clawPDF.Views;
using NLog;

namespace clawSoft.clawPDF.Startup
{
    internal class MainWindowStart : MaybePipedStart
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        internal override string ComposePipeMessage()
        {
            return "ShowMain|";
        }

        internal override bool StartApplication()
        {
            _logger.Debug("Starting main form");
            var _mainWindow = new MainWindow();
            _mainWindow.ShowDialog();

            return true;
        }
    }
}