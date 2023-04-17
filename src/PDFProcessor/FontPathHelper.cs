using clawSoft.clawPDF.Utilities.IO;
using NLog;
using System;
using System.IO;
using SystemInterface.IO;

namespace clawSoft.clawPDF.PDFProcessing
{
    public class FontPathHelper
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public readonly static string DEFAULT_FONT_FILE = "arial.ttf";
        public readonly static string DEFAULT_FONT_NAME = "Arial";
        private readonly static IPathSafe _pathSafe = new PathWrapSafe();

        public static string TryGetFontPath(string fontFile)
        {
            var globalFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            _logger.Trace("Global font folder: " + globalFontFolder);
            if (string.IsNullOrEmpty(fontFile))
            {
                fontFile = DEFAULT_FONT_FILE;
            }

            var fontPath = _pathSafe.Combine(globalFontFolder, fontFile);
            if (!File.Exists(fontPath))
            {
                var userFontFolder = Environment.ExpandEnvironmentVariables(@"%LocalAppData%\Microsoft\Windows\Fonts");
                _logger.Trace("User font folder: " + userFontFolder);

                fontPath = _pathSafe.Combine(userFontFolder, fontFile);
                if (!File.Exists(fontPath))
                {
                    _logger.Error($"Font file not found: {fontFile}");
                    return null;
                }
            }

            _logger.Debug("Font path: " + fontPath);

            return fontPath;
        }
    }
}
