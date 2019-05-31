using System;
using System.IO;
using System.Reflection;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.Utilities;

namespace clawSoft.clawPDF.Shared.Assistants
{
    public class UacAssistant
    {
        public bool RepairPrinter(string printerName)
        {
            return CallPrinterHelper("/Driver=Add");
        }

        public bool AddPrinter(string printerName)
        {
            return CallPrinterHelper("/Printer=Add /Name=\"" + printerName + "\"");
        }

        public bool DeletePrinter(string printerName)
        {
            return CallPrinterHelper("/Printer=Remove /Name=\"" + printerName + "\"");
        }

        /// <summary>
        ///     Call the SetupHelper.exe to add or remove explorer context menu integration
        /// </summary>
        /// <param name="arguments">Command line arguments that will be passed to SetupHelper.exe</param>
        /// <returns>true, if the action was successful</returns>
        private void CallSetupHelper(string arguments)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (assemblyPath == null)
                return;

            var setupHelperPath = Path.Combine(assemblyPath, "SetupHelper.exe");

            if (!File.Exists(setupHelperPath))
            {
                var message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation("Application",
                    "SetupFileMissing",
                    "An important clawPDF file is missing ('{0}'). Please reinstall clawPDF!",
                    Path.GetFileName(setupHelperPath));
                var caption =
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("ApplicationSettingsWindow", "Error",
                        "Error");
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
                return;
            }

            if (Environment.OSVersion.Version.Major <= 5)
            {
                var osHelper = new OsHelper();
                if (!osHelper.UserIsAdministrator())
                {
                    var message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation(
                        "ApplicationSettingsWindow", "OperationRequiresAdminPrivileges",
                        "This operation requires admin privileges and it looks like you are not an admin. Do you want to continue anyway?\r\nNote: It is safe to continue even if you are unsure if you have approrpiate rights, but the operation will not be completed.",
                        Path.GetFileName(setupHelperPath));
                    var caption = TranslationHelper.Instance.TranslatorInstance.GetTranslation(
                        "ApplicationSettingsWindow", "AdminPrivilegesRequired", "Admin privileges required");
                    if (MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.YesNo,
                            MessageWindowIcon.Info) == MessageWindowResponse.No)
                        return;
                }
            }

            CallProgramAsAdmin(setupHelperPath, arguments);
        }

        private bool CallProgramAsAdmin(string path, string arguments)
        {
            var shellExecuteHelper = new ShellExecuteHelper();
            var result = shellExecuteHelper.RunAsAdmin(path, arguments);

            if (result == ShellExecuteResult.RunAsWasDenied)
            {
                var message = TranslationHelper.Instance.TranslatorInstance.GetTranslation("ApplicationSettingsWindow",
                    "SufficientPermissions",
                    "Operation failed. You probably do not have sufficient permissions.");
                var caption =
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("ApplicationSettingsWindow", "Error",
                        "Error");

                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);

                return false;
            }

            return result == ShellExecuteResult.Success;
        }

        /// <summary>
        ///     Call the SetupHelper.exe to add, rename or delete printers
        /// </summary>
        /// <param name="arguments">Command line arguments that will be passed to SetupHelper.exe</param>
        /// <returns>true, if the action was successful</returns>
        private bool CallPrinterHelper(string arguments)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyPath == null)
                return false;

            var printerHelperPath = Path.Combine(assemblyPath, "SetupHelper.exe");

            if (!File.Exists(printerHelperPath))
            {
                var message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation("Application",
                    "SetupFileMissing",
                    "An important clawPDF file is missing ('{0}'). Please reinstall clawPDF!",
                    Path.GetFileName(printerHelperPath));
                var caption =
                    TranslationHelper.Instance.TranslatorInstance.GetTranslation("ApplicationSettingsWindow", "Error",
                        "Error");
                MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.OK, MessageWindowIcon.Error);
                return false;
            }

            if (Environment.OSVersion.Version.Major <= 5)
            {
                var osHelper = new OsHelper();
                if (!osHelper.UserIsAdministrator())
                {
                    var message = TranslationHelper.Instance.TranslatorInstance.GetFormattedTranslation(
                        "ApplicationSettingsWindow", "OperationRequiresAdminPrivileges",
                        "This operation requires admin privileges and it looks like you are not an admin. Do you want to continue anyway?\r\nNote: It is safe to continue even if you are unsure if you have appropriate rights, but the operation will not be completed.",
                        Path.GetFileName(printerHelperPath));
                    var caption = TranslationHelper.Instance.TranslatorInstance.GetTranslation(
                        "ApplicationSettingsWindow", "AdminPrivilegesRequired", "Admin privileges required");
                    if (MessageWindow.ShowTopMost(message, caption, MessageWindowButtons.YesNo,
                            MessageWindowIcon.Info) ==
                        MessageWindowResponse.No)
                        return false;
                }
            }

            return CallProgramAsAdmin(printerHelperPath, arguments);
        }
    }
}