using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using clawSoft.clawPDF.Core.Ghostscript.OutputDevices;
using NLog;

namespace clawSoft.clawPDF.Core.Ghostscript
{
    /// <summary>
    ///     Provides access to Ghostscript, either through DLL access or by calling the Ghostscript exe
    /// </summary>
    public class GhostScript
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public GhostScript(GhostscriptVersion ghostscriptVersion)
        {
            GhostscriptVersion = ghostscriptVersion;
        }

        public GhostscriptVersion GhostscriptVersion { get; }

        public event EventHandler<OutputEventArgs> Output;

        private bool Run(IList<string> parameters, string tempOutputFolder)
        {
            _logger.Debug("Ghostscript Parameters:\r\n" + string.Join("\r\n", parameters));

            // Start the child process.
            var p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = GhostscriptVersion.DllPath;
            p.StartInfo.CreateNoWindow = true;

            var ghostScriptArguments = parameters.ToArray();
            GhostScriptANY.CallAPI(ghostScriptArguments);

            return true;
        }

        private void RaiseOutputEvent(string message)
        {
            if (Output != null) Output(this, new OutputEventArgs(message));
        }

        /// <summary>
        ///     Runs Ghostscript with the parameters specified by the OutputDevice
        /// </summary>
        /// <param name="output">The output device to use for conversion</param>
        /// <param name="tempOutputFolder">Full path to the folder, where temporary files can be stored</param>
        public bool Run(OutputDevice output, string tempOutputFolder)
        {
            var parameters = (List<string>)output.GetGhostScriptParameters(GhostscriptVersion);
            var success = Run(parameters.ToArray(), tempOutputFolder);

            var outputFolder = Path.GetDirectoryName(output.Job.OutputFilenameTemplate);

            if (outputFolder != null && !Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            output.Job.CollectTemporaryOutputFiles();

            return success;
        }
    }

    public class OutputEventArgs : EventArgs
    {
        public OutputEventArgs(string output)
        {
            Output = output;
        }

        public string Output { get; }
    }
}