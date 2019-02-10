using System;
using System.Collections.Generic;
using System.IO;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using NLog;

namespace clawSoft.clawPDF.Core.Helper
{
    public static class ProfileChecker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static ActionResultDict ProfileCheckDict(IList<ConversionProfile> profileList)
        {
            var nameResultDict = new ActionResultDict();

            foreach (var profile in profileList)
            {
                var result = ProfileCheck(profile);
                if (!result)
                    nameResultDict.Add(profile.Name, result);
            }

            return nameResultDict;
        }

        public static ActionResultDict ProfileCheckDict(ConversionProfile profile)
        {
            var nameResultDict = new ActionResultDict();
            var result = ProfileCheck(profile);
            nameResultDict.Add(profile.Name, result);

            return nameResultDict;
        }

        public static ActionResult ProfileCheck(ConversionProfile profile)
        {
            var actionResult = new ActionResult();

            actionResult.AddRange(CheckAutosaveSettings(profile));
            actionResult.AddRange(CheckSaveSettings(profile));
            actionResult.AddRange(CheckCoverPageSettings(profile));
            actionResult.AddRange(CheckAttachmentPageSettings(profile));
            actionResult.AddRange(CheckStampingSettings(profile));
            actionResult.AddRange(CheckEncryptionSettings(profile));
            actionResult.AddRange(CheckBackgroundpageSettings(profile));
            actionResult.AddRange(CheckSignatureSettings(profile));

            var ftpAction = new FtpAction();
            actionResult.AddRange(ftpAction.Check(profile));
            var scriptAction = new ScriptAction();
            actionResult.AddRange(scriptAction.Check(profile));
            var smtpMailAction = new SmtpMailAction("");
            actionResult.AddRange(smtpMailAction.Check(profile));

            return actionResult;
        }

        private static ActionResult CheckSaveSettings(ConversionProfile profile)
        {
            const int actionId = 28;

            var actionResult = new ActionResult();

            if (profile.SaveDialog.SetDirectory
                && !profile.AutoSave.Enabled) //Skip if Autosave is enabled.
                if (string.IsNullOrEmpty(profile.SaveDialog.Folder))
                {
                    Logger.Error("Preselected folder for savedialog is empty.");
                    actionResult.Add(actionId, 100);
                }

            return actionResult;
        }

        private static ActionResult CheckAutosaveSettings(ConversionProfile profile)
        {
            const int actionId = 21;

            var actionResult = new ActionResult();

            if (profile.AutoSave.Enabled)
            {
                if (string.IsNullOrEmpty(profile.AutoSave.TargetDirectory))
                {
                    Logger.Error("Automatic saving without target directory.");
                    actionResult.Add(actionId, 100);
                }

                if (string.IsNullOrEmpty(profile.FileNameTemplate))
                {
                    Logger.Error("Automatic saving without filename template.");
                    actionResult.Add(actionId, 101);
                }
            }

            return actionResult;
        }

        private static ActionResult CheckCoverPageSettings(ConversionProfile profile)
        {
            const int actionId = 22;

            var actionResult = new ActionResult();

            if (profile.CoverPage.Enabled)
            {
                if (string.IsNullOrEmpty(profile.CoverPage.File))
                {
                    Logger.Error("No cover file is specified.");
                    actionResult.Add(actionId, 100);
                }
                //Skip check for network path
                else if (!profile.CoverPage.File.StartsWith(@"\\") && !File.Exists(profile.CoverPage.File))
                {
                    Logger.Error("The cover file \"" + profile.CoverPage.File + "\" does not exist.");
                    actionResult.Add(actionId, 101);
                }
                else if (!profile.CoverPage.File.EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase))
                {
                    Logger.Error("The cover file \"" + profile.CoverPage.File + "\" is no pdf file.");
                    actionResult.Add(actionId, 102);
                }
            }

            return actionResult;
        }

        private static ActionResult CheckAttachmentPageSettings(ConversionProfile profile)
        {
            const int actionId = 23;

            var actionResult = new ActionResult();

            if (profile.AttachmentPage.Enabled)
            {
                if (string.IsNullOrEmpty(profile.AttachmentPage.File))
                {
                    Logger.Error("No attachment file is specified.");
                    actionResult.Add(actionId, 100);
                }
                //Skip check for network path
                else if (!profile.AttachmentPage.File.StartsWith(@"\\") && !File.Exists(profile.AttachmentPage.File))
                {
                    Logger.Error("The attachment file \"" + profile.AttachmentPage.File + "\" does not exist.");
                    actionResult.Add(actionId, 101);
                }
                else if (!profile.AttachmentPage.File.EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase))
                {
                    Logger.Error("The attachment file \"" + profile.CoverPage.File + "\" is no pdf file.");
                    actionResult.Add(actionId, 102);
                }
            }

            return actionResult;
        }

        private static ActionResult CheckStampingSettings(ConversionProfile profile)
        {
            const int actionId = 24;

            var actionResult = new ActionResult();

            if (profile.Stamping.Enabled)
            {
                if (string.IsNullOrEmpty(profile.Stamping.StampText))
                {
                    Logger.Error("No stamp text is specified.");
                    actionResult.Add(actionId, 100);
                }

                if (string.IsNullOrEmpty(profile.Stamping.FontName))
                {
                    Logger.Error("No stamp font is specified.");
                    actionResult.Add(actionId, 101);
                }
            }

            return actionResult;
        }

        public static ActionResult CheckEncryptionSettings(ConversionProfile profile)
        {
            const int actionId = 25;

            var actionResult = new ActionResult();

            var security = profile.PdfSettings.Security;
            if (security.Enabled)
                if (profile.AutoSave.Enabled)
                {
                    if (string.IsNullOrEmpty(security.OwnerPassword))
                    {
                        Logger.Error("No saved owner password for security in automatic saving.");
                        actionResult.Add(actionId, 100);
                    }

                    if (security.RequireUserPassword)
                        if (string.IsNullOrEmpty(security.UserPassword))
                        {
                            Logger.Error("No saved user password for security in automatic saving.");
                            actionResult.Add(actionId, 101);
                        }
                }

            return actionResult;
        }

        public static ActionResult CheckBackgroundpageSettings(ConversionProfile profile)
        {
            const int actionId = 17;

            var actionResult = new ActionResult();

            if (profile.OutputFormat != OutputFormat.Pdf && profile.OutputFormat != OutputFormat.PdfA1B
                                                         && profile.OutputFormat != OutputFormat.PdfA2B &&
                                                         profile.OutputFormat != OutputFormat.PdfX)
                return actionResult;

            if (profile.BackgroundPage.Enabled)
            {
                if (string.IsNullOrEmpty(profile.BackgroundPage.File))
                {
                    Logger.Error("No background file is specified.");
                    actionResult.Add(actionId, 100);
                }
                //Skip check for network path
                else if (!profile.BackgroundPage.File.StartsWith(@"\\") && !File.Exists(profile.BackgroundPage.File))
                {
                    Logger.Error("The background file \"" + profile.BackgroundPage.File + "\" does not exist.");
                    actionResult.Add(actionId, 101);
                }
                else if (!profile.BackgroundPage.File.EndsWith(".pdf", StringComparison.InvariantCultureIgnoreCase))
                {
                    Logger.Error("The background file \"" + profile.BackgroundPage.File + "\" is no pdf file.");
                    actionResult.Add(actionId, 102);
                }
            }

            return actionResult;
        }

        public static ActionResult CheckSignatureSettings(ConversionProfile profile)
        {
            const int actionId = 12;

            var actionResult = new ActionResult();

            var sign = profile.PdfSettings.Signature;
            if (sign.Enabled)
            {
                if (string.IsNullOrEmpty(sign.CertificateFile))
                {
                    Logger.Error("Error in signing. Missing certification file.");
                    actionResult.Add(actionId, 100);
                }
                //Skip check for network path
                else if (!sign.CertificateFile.StartsWith(@"\\") && !File.Exists(sign.CertificateFile))
                {
                    Logger.Error("Error in signing. The certification file '" + sign.CertificateFile +
                                 "' doesn't exist.");
                    actionResult.Add(actionId, 101);
                }

                if (profile.AutoSave.Enabled)
                    if (string.IsNullOrEmpty(sign.SignaturePassword))
                    {
                        Logger.Error("Automatic saving without certificate password.");
                        actionResult.Add(actionId, 102);
                    }

                if (sign.TimeServerIsSecured)
                {
                    if (string.IsNullOrEmpty(sign.TimeServerLoginName))
                    {
                        Logger.Error("Secured Time Server without Login Name.");
                        actionResult.Add(actionId, 103);
                    }

                    if (string.IsNullOrEmpty(sign.TimeServerPassword))
                    {
                        Logger.Error("Secured Time Server without Password.");
                        actionResult.Add(actionId, 104);
                    }
                }
            }

            return actionResult;
        }
    }
}