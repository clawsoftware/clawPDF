using System;
using System.Diagnostics;
using System.IO;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Utilities.IO;
using NLog;

namespace clawSoft.clawPDF.Shared.Helper
{
    public static class PsFileHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly IPathSafe PathSafe = new PathWrapSafe();

        /// <summary>
        ///     Create unique job folder in spool folder and copy ps file to it.
        ///     Create inf file from ps file.
        /// </summary>
        /// <returns>inf file in spool folder</returns>
        public static string TransformToInfFile(string psFile, string spoolFolder, string printerName = "clawPDF")
        {
            if (string.IsNullOrEmpty(psFile) || !File.Exists(psFile))
            {
                Logger.Error("Launched job without or not existing ps-file.");
                return "";
            }

            string jobFolder;
            try
            {
                jobFolder = CreateJobFolderInSpool(psFile, spoolFolder);
            }
            catch (Exception ex)
            {
                Logger.Error("Error while creating spool directory for ps-job:\r\n" + ex.Message);
                return "";
            }

            try
            {
                var psFileInJobFolder = CopyPsFileToJobFolder(jobFolder, psFile);
                return CreateInfFile(psFile, jobFolder, psFileInJobFolder, printerName);
            }
            catch (Exception ex)
            {
                Logger.Error("Error while coping ps-file in spool folder:\r\n" + ex.Message);
                Directory.Delete(jobFolder, true); //Delete created folder and files
                return "";
            }
        }

        private static string CreateJobFolderInSpool(string psFile, string spoolFolder)
        {
            var psFilename = PathSafe.GetFileName(psFile);
            var jobFolder = PathSafe.Combine(spoolFolder, psFilename);
            jobFolder = new UniqueDirectory(jobFolder).MakeUniqueDirectory();
            Directory.CreateDirectory(jobFolder);
            Logger.Trace("Created spool directory for ps-file job: " + jobFolder);

            return jobFolder;
        }

        private static string CopyPsFileToJobFolder(string jobFolder, string psFile)
        {
            var psFilename = PathSafe.GetFileName(psFile);
            var psFileInJobFolder = PathSafe.Combine(jobFolder, psFilename);
            File.Copy(psFile, psFileInJobFolder);
            Logger.Debug("Copied ps-file in spool folder: " + psFileInJobFolder);

            return psFileInJobFolder;
        }

        private static string CreateInfFile(string psFile, string jobFolder, string psFileInJobFolder,
            string printerName)
        {
            var psFilename = PathSafe.GetFileName(psFile);
            var infFile = PathSafe.Combine(jobFolder, psFilename + ".inf");

            var jobInfo = new JobInfo();

            var sourceFileInfo = new SourceFileInfo();
            sourceFileInfo.Filename = psFileInJobFolder;
            sourceFileInfo.Author = Environment.UserName;
            sourceFileInfo.ClientComputer = Environment.MachineName.Replace("\\", "");
            sourceFileInfo.Copies = 1;
            sourceFileInfo.DocumentTitle = psFile;
            sourceFileInfo.JobCounter = 0;
            sourceFileInfo.JobId = 0;
            sourceFileInfo.PrinterName = printerName;
            sourceFileInfo.SessionId = Process.GetCurrentProcess().SessionId;
            sourceFileInfo.TotalPages = 1;
            sourceFileInfo.Type = JobType.PsJob;
            sourceFileInfo.WinStation = Environment.GetEnvironmentVariable("SESSIONNAME");
            jobInfo.SourceFiles.Add(sourceFileInfo);

            jobInfo.SaveInf(infFile);
            Logger.Debug("Created inf-file for ps-file: " + infFile);

            return infFile;
        }
    }
}