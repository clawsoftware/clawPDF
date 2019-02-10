namespace clawSoft.clawPDF.Core.Ghostscript
{
    /// <summary>
    ///     Specifies information on Ghostscript installations
    /// </summary>
    public class GhostscriptVersion
    {
        /// <summary>
        ///     Creates a new GhostscriptVersion object
        /// </summary>
        /// <param name="version">Ghostscript version string</param>
        /// <param name="dllPath">Full path of the gsdll32.dll</param>
        /// <param name="libFolder">Full path of the Lib folder</param>
        public GhostscriptVersion(string version, string dllPath)
        {
            Version = version;
            DllPath = dllPath;
        }

        public string Version { get; }
        public string DllPath { get; }
    }
}