using System.Collections.Specialized;
using System.Configuration;
using SystemInterface.Configuration;

namespace SystemWrapper.Configuration
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Configuration.ConfigurationManager"/> class.
    /// </summary>
    public class ConfigurationManagerWrap : IConfigurationManager
    {
        /// <inheritdoc />
        public NameValueCollection AppSettings
        {
            get { return ConfigurationManager.AppSettings; }
        }

        /// <inheritdoc />
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get { return ConfigurationManager.ConnectionStrings; }
        }

        /// <inheritdoc />
        public object GetSection(string pSectionName)
        {
            return ConfigurationManager.GetSection(pSectionName);
        }

        /// <inheritdoc />
        public IConfiguration OpenExeConfiguration(string pExePath)
        {
            return new ConfigurationWrap(ConfigurationManager.OpenExeConfiguration(pExePath));
        }

        /// <inheritdoc />
        public IConfiguration OpenExeConfiguration(ConfigurationUserLevel pConfigurationUserLevel)
        {
            return new ConfigurationWrap(ConfigurationManager.OpenExeConfiguration(pConfigurationUserLevel));
        }

        /// <inheritdoc />
        public IConfiguration OpenMachineConfiguration()
        {
            return new ConfigurationWrap(ConfigurationManager.OpenMachineConfiguration());
        }

        /// <inheritdoc />
        public IConfiguration OpenMappedExeConfiguration(ExeConfigurationFileMap pExeConfigurationFileMap, ConfigurationUserLevel pConfigurationUserLevel)
        {
            return new ConfigurationWrap(ConfigurationManager.OpenMappedExeConfiguration(pExeConfigurationFileMap, pConfigurationUserLevel));
        }

        /// <inheritdoc />
        public IConfiguration OpenMappedMachineConfiguration(ConfigurationFileMap pConfigurationFileMap)
        {
            return new ConfigurationWrap(ConfigurationManager.OpenMappedMachineConfiguration(pConfigurationFileMap));
        }

        /// <inheritdoc />
        public void RefreshSection(string pSectionName)
        {
            ConfigurationManager.RefreshSection(pSectionName);
        }
    }
}