using System;
using clawSoft.clawPDF.Shared.Helper;
using SystemInterface.Microsoft.Win32;
using SystemWrapper.Microsoft.Win32;

namespace clawSoft.clawPDF.Helper
{
    internal class WelcomeSettingsHelper
    {
        public const string RegistryKeyForWelcomeSettings = @"HKEY_CURRENT_USER\Software\clawSoft\clawPDF";
        public const string RegistryValueForWelcomeVersion = @"LatestWelcomeVersion";

        private readonly IRegistry _registryWrap;
        private readonly VersionHelper _versionHelper;

        public WelcomeSettingsHelper()
        {
            _registryWrap = new RegistryWrap();
            _versionHelper = new VersionHelper();
        }

        public bool IsFirstRun()
        {
            var currentApplicationVersion = _versionHelper.FormatWithBuildNumber();
            var welcomeVersionFromRegistry = GetWelcomeVersionFromRegistry();

            if (currentApplicationVersion.Equals(welcomeVersionFromRegistry, StringComparison.OrdinalIgnoreCase))
                return false;

            return false;
        }

        private string GetWelcomeVersionFromRegistry()
        {
            var value = _registryWrap.GetValue(RegistryKeyForWelcomeSettings, RegistryValueForWelcomeVersion, null);
            if (value == null)
                return "";
            return value.ToString();
        }

        public void SetCurrentApplicationVersionAsWelcomeVersionInRegistry()
        {
            var currentApplicationVersion = _versionHelper.FormatWithBuildNumber();
            _registryWrap.SetValue(RegistryKeyForWelcomeSettings, RegistryValueForWelcomeVersion,
                currentApplicationVersion);
        }
    }
}