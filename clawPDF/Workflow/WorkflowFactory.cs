using System;
using clawSoft.clawPDF.Core.Jobs;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using NLog;

namespace clawSoft.clawPDF.Workflow
{
    internal static class WorkflowFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Create a workflow based on the job and settings objects provided. This will create an AutoSave workflow if the
        ///     job's printer has an AutoSave profile associated or the default profile uses AutoSave.
        ///     Otherwise, an interactive workflow will be created.
        /// </summary>
        /// <param name="jobInfo">The jobinfo used for the decision</param>
        /// <param name="settings">The settings used for the decision</param>
        /// <returns>A ConversionWorkflow either for AutoSave or interactive use</returns>
        public static ConversionWorkflow CreateWorkflow(IJobInfo jobInfo, clawPDFSettings settings)
        {
            Logger.Trace("Creating Workflow");

            var preselectedProfile = PreselectedProfile(jobInfo, settings).Copy();

            Logger.Debug("Profile: {0} (GUID {1})", preselectedProfile.Name, preselectedProfile.Guid);

            var jobTranslations = new JobTranslations();
            jobTranslations.EmailSignature = MailSignatureHelper.ComposeMailSignature();

            var job = JobFactory.CreateJob(jobInfo, preselectedProfile, JobInfoQueue.Instance, jobTranslations);
            job.AutoCleanUp = true;

            if (preselectedProfile.AutoSave.Enabled)
            {
                Logger.Trace("Creating AutoSaveWorkflow");
                return new AutoSaveWorkflow(job, settings);
            }

            Logger.Trace("Creating InteractiveWorkflow");
            return new InteractiveWorkflow(job, settings);
        }

        /// <summary>
        ///     Determines the preselected profile for the printer that was used while creating the job
        /// </summary>
        /// <param name="jobInfo">The jobinfo used for the decision</param>
        /// <param name="settings">The settings used for the decision</param>
        /// <returns>The profile that is associated with the printer or the default profile</returns>
        private static ConversionProfile PreselectedProfile(IJobInfo jobInfo, clawPDFSettings settings)
        {
            foreach (var mapping in settings.ApplicationSettings.PrinterMappings)
                if (mapping.PrinterName.Equals(jobInfo.SourceFiles[0].PrinterName, StringComparison.OrdinalIgnoreCase))
                {
                    var p = settings.GetProfileByGuid(mapping.ProfileGuid);
                    if (p != null)
                        return p;
                    break;
                }

            var lastUsedProfile = settings.GetLastUsedProfile();
            if (lastUsedProfile != null)
                return lastUsedProfile;

            // try default profile
            var defaultProfile = SettingsHelper.GetDefaultProfile(settings.ConversionProfiles);
            if (defaultProfile != null)
                return defaultProfile;

            // last resort: first profile from the list
            return settings.ConversionProfiles[0];
        }
    }
}