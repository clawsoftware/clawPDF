using System;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.IO;
using SystemInterface.IO;
using SystemInterface.Microsoft.Win32;
using SystemWrapper.IO;
using SystemWrapper.Microsoft.Win32;

namespace clawSoft.clawPDF.Core.Ghostscript
{
    public class GhostscriptDiscovery
    {
        private readonly IFile _file;
        private readonly IPathSafe _pathSafe = new PathWrapSafe();

        public GhostscriptDiscovery() : this(new FileWrap(), new RegistryWrap(), new AssemblyHelper(), new OsHelper())
        {
        }

        public GhostscriptDiscovery(IFile file, IRegistry registry, IAssemblyHelper assemblyHelper, IOsHelper osHelper)
        {
            _file = file;
            ApplicationPath = assemblyHelper.GetCurrentAssemblyDirectory();
        }

        public string ApplicationPath { get; }

        /// <summary>
        ///     Search for Ghostscript instances in the application folder
        /// </summary>
        /// <returns>A GhostscriptVersion if an internal instance exists, null otherwise</returns>
        public GhostscriptVersion FindInternalInstance()
        {
            string[] paths =
            {
                ApplicationPath
            };

            foreach (var path in paths)
            {
                string dllPath;
                if (Environment.Is64BitOperatingSystem)
                {
                    dllPath = _pathSafe.Combine(path, @"gsdll64.dll");
                }
                else
                {
                    dllPath = _pathSafe.Combine(path, @"gsdll32.dll");
                }

                if (_file.Exists(dllPath)) return new GhostscriptVersion("<internal>", dllPath);
            }

            return null;
        }

        /// <summary>
        ///     Get the internal instance if it exists, otherwise the installed instance in the given version
        /// </summary>
        /// <returns>The best matching Ghostscript instance</returns>
        public GhostscriptVersion GetBestGhostscriptInstance()
        {
            var version = FindInternalInstance();
            return version;
        }
    }
}