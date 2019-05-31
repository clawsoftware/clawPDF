using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.ftplib.FtpLib;
using NLog;

namespace clawSoft.clawPDF.Core.Actions
{
    public class FtpAction : IAction, ICheckable
    {
        private const int ActionId = 18;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly string InvalidPathCharRegex = string.Format(@"/\\|[{0}]+",
            Regex.Escape(new string(Path.GetInvalidPathChars()) + ":*?"));

        /// <summary>
        ///     Upload all output files with ftp
        /// </summary>
        /// <param name="job">The job to process</param>
        /// <returns>An ActionResult to determine the success and a list of errors</returns>
        public ActionResult ProcessJob(IJob job)
        {
            Logger.Debug("Launched ftp-Action");
            try
            {
                var result = FtpUpload(job);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception while upload file to ftp:\r\n" + ex.Message);
                return new ActionResult(ActionId, 999);
            }
        }

        /// <summary>
        ///     Check if the profile is configured properly for this action
        /// </summary>
        /// <param name="profile">The profile to check</param>
        /// <returns>ActionResult with configuration problems</returns>
        public ActionResult Check(ConversionProfile profile)
        {
            var actionResult = new ActionResult();
            if (profile.Ftp.Enabled)
            {
                if (string.IsNullOrEmpty(profile.Ftp.Server))
                {
                    Logger.Error("No FTP server specified.");
                    actionResult.Add(ActionId, 100);
                }

                if (string.IsNullOrEmpty(profile.Ftp.UserName))
                {
                    Logger.Error("No FTP username specified.");
                    actionResult.Add(ActionId, 101);
                }

                if (profile.AutoSave.Enabled)
                    if (string.IsNullOrEmpty(profile.Ftp.Password))
                    {
                        Logger.Error("Automatic saving without ftp password.");
                        actionResult.Add(ActionId, 109);
                    }
            }

            return actionResult;
        }

        private ActionResult FtpUpload(IJob job)
        {
            var actionResult = Check(job.Profile);
            if (!actionResult)
            {
                Logger.Error("Canceled FTP upload action.");
                return actionResult;
            }

            if (string.IsNullOrEmpty(job.Passwords.FtpPassword))
            {
                Logger.Error("No ftp password specified in action");
                return new ActionResult(ActionId, 102);
            }

            Logger.Debug("Creating ftp connection.\r\nServer: " + job.Profile.Ftp.Server + "\r\nUsername: " +
                         job.Profile.Ftp.UserName);
            var ftp = new FtpConnection(job.Profile.Ftp.Server, job.Profile.Ftp.UserName, job.Passwords.FtpPassword);

            try
            {
                ftp.Open();
                ftp.Login();
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode.Equals(12007))
                {
                    Logger.Error("Can not connect to the internet for login to ftp. Win32Exception Message:\r\n" +
                                 ex.Message);
                    ftp.Close();
                    return new ActionResult(ActionId, 108);
                }

                Logger.Error("Win32Exception while login to ftp server:\r\n" + ex.Message);
                ftp.Close();
                return new ActionResult(ActionId, 104);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception while login to ftp server:\r\n" + ex.Message);
                ftp.Close();
                return new ActionResult(ActionId, 104);
            }

            var fullDirectory = job.TokenReplacer.ReplaceTokens(job.Profile.Ftp.Directory).Trim();
            if (!IsValidPath(fullDirectory))
            {
                Logger.Warn("Directory contains invalid characters \"" + fullDirectory + "\"");
                fullDirectory = MakeValidPath(fullDirectory);
            }

            Logger.Debug("Directory on ftp server: " + fullDirectory);

            var directories = fullDirectory.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                foreach (var directory in directories)
                {
                    if (!ftp.DirectoryExists(directory))
                    {
                        Logger.Debug("Create folder: " + directory);
                        ftp.CreateDirectory(directory);
                    }

                    Logger.Debug("Move to: " + directory);
                    ftp.SetCurrentDirectory(directory);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Exception while setting directory on ftp server\r\n:" + ex.Message);
                ftp.Close();
                return new ActionResult(ActionId, 105);
            }

            var addendum = "";
            if (job.Profile.Ftp.EnsureUniqueFilenames)
            {
                Logger.Debug("Generate addendum for unique filename");
                try
                {
                    addendum = AddendumForUniqueFilename(Path.GetFileName(job.OutputFiles[0]), ftp);
                    Logger.Debug("The addendum for unique filename is \"" + addendum +
                                 "\" If empty, the file was already unique.");
                }
                catch (Exception ex)
                {
                    Logger.Error("Exception while generating unique filename\r\n:" + ex.Message);
                    ftp.Close();
                    return new ActionResult(ActionId, 106);
                }
            }

            foreach (var file in job.OutputFiles)
                try
                {
                    var targetFile = Path.GetFileNameWithoutExtension(file) + addendum + Path.GetExtension(file);
                    ftp.PutFile(file, MakeValidPath(targetFile));
                }
                catch (Exception ex)
                {
                    Logger.Error("Exception while uploading the file \"" + file + "\": \r\n" + ex.Message);
                    ftp.Close();
                    return new ActionResult(ActionId, 107);
                }

            ftp.Close();
            return new ActionResult();
        }

        private string AddendumForUniqueFilename(string fileName, FtpConnection ftp)
        {
            var i = 1;
            var uniqueFileName = fileName;
            while (ftp.FileExists(uniqueFileName))
            {
                Logger.Debug("The file \"" + uniqueFileName + "\" already exist on ftp server directory");
                i++;
                uniqueFileName = fileName + "_" + i;
            }

            if (i > 1)
                return "_" + i;

            return "";
        }

        public static bool IsValidPath(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            return !Regex.IsMatch(path, InvalidPathCharRegex);
        }

        public static string MakeValidPath(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            return Regex.Replace(path, InvalidPathCharRegex, "_");
        }
    }
}