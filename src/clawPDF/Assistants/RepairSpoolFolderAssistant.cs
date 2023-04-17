using System;
using System.IO;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.Utilities;
using NLog;

namespace clawSoft.clawPDF.Assistants
{
    internal class RepairSpoolFolderAssistant
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public bool TryRepairSpoolPath()
        {
            Logger.Error(
                "The spool folder is not accessible due to a permission problem. clawPDF will not work this way");

            var tempFolder = Path.GetFullPath(Path.Combine(JobInfoQueue.Instance.SpoolFolder, ".."));
            var username = Environment.UserName;

            Logger.Debug("Username is {0}", username);

            var title = TranslationHelper.Instance.TranslatorInstance.GetTranslation("Application",
                "SpoolFolderAccessDenied", "Access Denied");
            var message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation("Application",
                "SpoolFolderAskToRepair",
                "The temporary path where clawPDF stores the print jobs can't be accessed. This is a configuration problem on your machine and needs to be fixed. Do you want clawPDF to attempt repairing it?\r\nYour spool folder is: {0}",
                tempFolder);

            Logger.Debug("Asking to start repair..");
            if (MessageWindow.ShowTopMost(message, title, MessageWindowButtons.YesNo, MessageWindowIcon.Exclamation) ==
                MessageWindowResponse.Yes)
            {
                var repairToolPath = AppDomain.CurrentDomain.BaseDirectory;
                repairToolPath = Path.Combine(repairToolPath, "RepairFolderPermissions.exe");

                var repairToolParameters = string.Format("\"{0}\" \"{1}\"", username, tempFolder);

                Logger.Debug("RepairTool path is: {0}", repairToolPath);
                Logger.Debug("Parameters: {0}", repairToolParameters);

                if (!File.Exists(repairToolPath))
                {
                    Logger.Error("RepairFolderPermissions.exe does not exist!");
                    title = TranslationHelper.Instance.TranslatorInstance.GetTranslation("Application",
                        "RepairToolNotFound", "RepairTool not found");
                    message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation("Application",
                        "SetupFileMissing",
                        "An important clawPDF file is missing ('{0}'). Please reinstall clawPDF!",
                        Path.GetFileName(repairToolPath));

                    MessageWindow.ShowTopMost(message, title, MessageWindowButtons.OK, MessageWindowIcon.Error);
                    return false;
                }

                Logger.Debug("Starting RepairTool...");
                var shellExecuteHelper = new ShellExecuteHelper();
                var result = shellExecuteHelper.RunAsAdmin(repairToolPath, repairToolParameters);
                Logger.Debug("Done: {0}", result.ToString());
            }

            Logger.Debug("Now we'll check again, if the spool folder is not accessible");
            if (!JobInfoQueue.Instance.SpoolFolderIsAccessible())
            {
                Logger.Info("The spool folder could not be repaired.");
                title = TranslationHelper.Instance.TranslatorInstance.GetTranslation("Application",
                    "SpoolFolderAccessDenied", "Access Denied");
                message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation("Application",
                    "SpoolFolderUnableToRepair",
                    "clawPDF was not able to repair your spool folder. Please contact your administrator or the support to assist you in changing the permissions of the path '{0}'.",
                    tempFolder);

                MessageWindow.ShowTopMost(message, title, MessageWindowButtons.OK, MessageWindowIcon.Exclamation);
                return false;
            }

            Logger.Info("The spool folder was repaired successfully");

            return true;
        }
    }
}