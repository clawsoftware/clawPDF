using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Utilities;
using clawSoft.clawPDF.Utilities.Tokens;
using NLog;

namespace clawSoft.clawPDF.Core.Actions
{
    /// <summary>
    ///     Executes a script or executable after the conversion process.
    ///     The script receives the full paths to all created files and a string with user-configurable parameters as arguments
    /// </summary>
    public class ScriptAction : IAction, ICheckable
    {
        private const int ActionId = 14;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Calls the script
        /// </summary>
        /// <param name="job">The current job</param>
        /// <returns>An ActionResult to determine the success and a list of errors</returns>
        public ActionResult ProcessJob(IJob job)
        {
            Logger.Debug("Launched Script-Action");

            var scriptFile = Path.GetFullPath(ComposeScriptPath(job.Profile.Scripting.ScriptFile, job.TokenReplacer));

            var actionResult = Check(job.Profile);
            if (!actionResult)
                return actionResult;

            var proc = new Process();

            proc.StartInfo.FileName = scriptFile;
            Logger.Debug("Script-File: " + proc.StartInfo.FileName);

            var parameters = ComposeScriptParameters(job.Profile.Scripting.ParameterString, job.OutputFiles,
                job.TokenReplacer);

            proc.StartInfo.Arguments = parameters;
            Logger.Debug("Script-Parameters: " + parameters);

            var scriptDir = Path.GetDirectoryName(scriptFile);
            if (scriptDir != null)
                proc.StartInfo.WorkingDirectory = scriptDir;
            Logger.Debug("Script-Working Directory: " + scriptDir);

            proc.EnableRaisingEvents = true;
            proc.Exited += (sender, args) => proc.Close();

            try
            {
                Logger.Debug("Launching script...");
                proc.Start();

                if (job.Profile.Scripting.WaitForScript)
                {
                    Logger.Debug("Waiting for script to end");
                    proc.WaitForExit();
                    Logger.Debug("Script execution ended");
                }
                else
                {
                    Logger.Debug("The script is executed in the background");
                }

                return new ActionResult();
            }
            catch (Exception ex)
            {
                Logger.Error("Exception while running the script file \"" + scriptFile + "\"\r\n" +
                             ex.Message);
                return new ActionResult(ActionId, 999);
            }
        }

        public ActionResult Check(ConversionProfile profile)
        {
            var actionResult = new ActionResult();

            if (profile.Scripting.Enabled)
            {
                var tokenReplacer = new TokenReplacer();
                tokenReplacer.AddToken(new EnvironmentToken());

                var scriptFile = ComposeScriptPath(profile.Scripting.ScriptFile, tokenReplacer);
                if (string.IsNullOrEmpty(scriptFile))
                {
                    Logger.Error("Missing script file.");
                    actionResult.Add(ActionId, 100);
                }
                else if (!FileUtil.Instance.IsValidFilename(scriptFile))
                {
                    Logger.Error("The script file \"" + scriptFile + "\" contains illegal characters.");
                    actionResult.Add(ActionId, 101);
                }
                //Skip check for network path
                else if (!scriptFile.StartsWith(@"\\") && !File.Exists(scriptFile))
                {
                    Logger.Error("The script file \"" + scriptFile + "\" does not exist.");
                    actionResult.Add(ActionId, 101);
                }
            }

            return actionResult;
        }

        public static string ComposeScriptPath(string path, TokenReplacer tokenReplacer)
        {
            var composedPath = tokenReplacer.ReplaceTokens(path);
            composedPath = FileUtil.Instance.MakeValidFolderName(composedPath);

            return composedPath;
        }

        public static string ComposeScriptParameters(string parameterString, IList<string> outputFiles,
            TokenReplacer tokenReplacer)
        {
            var composedParameters = new StringBuilder();
            composedParameters.Append(tokenReplacer.ReplaceTokens(parameterString) + " ");

            composedParameters.Append(string.Join(" ", outputFiles.Select(s => "\"" + Path.GetFullPath(s) + "\"")));

            return composedParameters.ToString();
        }
    }
}