using System.Collections.Specialized;
using System.Configuration;

namespace SystemInterface.Configuration
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Configuration.ConfigurationManager"/> class.
    /// </summary>
    public interface IConfigurationManager
    {
        // Properties

        /// <summary>
        /// Gets the AppSettingsSection  data for the current application's default configuration.
        /// </summary>
        NameValueCollection AppSettings { get; }

        /// <summary>
        /// Gets the ConnectionStringsSection  data for the current application's default configuration.
        /// </summary>
        ConnectionStringSettingsCollection ConnectionStrings { get; }

        // Methods

        /// <summary>
        /// Retrieves a specified configuration section for the current application's default configuration.
        /// </summary>
        /// <param name="exePath">The configuration section path and name.</param>
        /// <returns>The specified ConfigurationSection object, or a null reference if the section does not exist.</returns>
        object GetSection(string exePath);

        /// <summary>
        /// Opens the specified client configuration file as a Configuration object.
        /// </summary>
        /// <param name="pExePath">The path to the configuration file associated with the executable file.</param>
        /// <returns>A Configuration object.</returns>
        IConfiguration OpenExeConfiguration(string pExePath);

        /// <summary>
        /// Opens the configuration file for the current application as a Configuration object.
        /// </summary>
        /// <param name="userLevel">The ConfigurationUserLevel for which you are opening the configuration.</param>
        /// <returns>A Configuration object.</returns>
        IConfiguration OpenExeConfiguration(ConfigurationUserLevel userLevel);

        /// <summary>
        /// Opens the machine configuration file on the current computer as a Configuration object.
        /// </summary>
        /// <returns>A Configuration object.</returns>
        IConfiguration OpenMachineConfiguration();

        /// <summary>
        /// Opens the specified client configuration file as a Configuration object using the specified file mapping and user level.
        /// </summary>
        /// <param name="fileMap"></param>
        /// <param name="userLevel"></param>
        /// <returns>A Configuration object.</returns>
        IConfiguration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);

        /// <summary>
        /// Opens the machine configuration file as a Configuration object using the specified file mapping.
        /// </summary>
        /// <param name="fileMap">An ExeConfigurationFileMap object that references configuration file to use instead of the application default configuration file.</param>
        /// <returns>A Configuration object.</returns>
        IConfiguration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);

        /// <summary>
        /// Refreshes the named section so the next time it is retrieved it will be re-read from disk.
        /// </summary>
        /// <param name="sectionName">The configuration section name or the configuration path and section name of the section to refresh.</param>
        void RefreshSection(string sectionName);

        /*

                // Methods
            public static object GetSection(string sectionName);
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            public static System.Configuration.Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            public static System.Configuration.Configuration OpenExeConfiguration(string exePath);
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            public static System.Configuration.Configuration OpenMachineConfiguration();
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            public static System.Configuration.Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            public static System.Configuration.Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);
            public static void RefreshSection(string sectionName);
        */
    }
}
