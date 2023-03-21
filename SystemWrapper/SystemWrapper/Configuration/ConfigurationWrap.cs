using System;
using System.Configuration;
using System.Runtime.Versioning;
using SystemInterface.Configuration;

namespace SystemWrapper.Configuration
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Configuration.Configuration"/> class.
    /// </summary>
    public class ConfigurationWrap : IConfiguration
    {
        private readonly System.Configuration.Configuration _configuration;

        public ConfigurationWrap(System.Configuration.Configuration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public AppSettingsSection AppSettings => _configuration.AppSettings;

        /// <inheritdoc />
        public ConnectionStringsSection ConnectionStrings => _configuration.ConnectionStrings;

        /// <inheritdoc />
        public string FilePath => _configuration.FilePath;

        /// <inheritdoc />
        public bool HasFile => _configuration.HasFile;

        /// <inheritdoc />
        public ConfigurationLocationCollection Locations => _configuration.Locations;

        /// <inheritdoc />
        public ContextInformation EvaluationContext => _configuration.EvaluationContext;

        /// <inheritdoc />
        public ConfigurationSectionGroup RootSectionGroup => _configuration.RootSectionGroup;

        /// <inheritdoc />
        public ConfigurationSectionCollection Sections => _configuration.Sections;

        /// <inheritdoc />
        public ConfigurationSectionGroupCollection SectionGroups => _configuration.SectionGroups;

        /// <inheritdoc />
        public bool NamespaceDeclared => _configuration.NamespaceDeclared;

        /// <inheritdoc />
        public ConfigurationSection GetSection(string sectionName)
        {
            return _configuration.GetSection(sectionName);
        }

        /// <inheritdoc />
        public ConfigurationSectionGroup GetSectionGroup(string sectionGroupName)
        {
            return _configuration.GetSectionGroup(sectionGroupName);
        }

        /// <inheritdoc />
        public Func<string, string> TypeStringTransformer => _configuration.TypeStringTransformer;

        /// <inheritdoc />
        public Func<string, string> AssemblyStringTransformer => _configuration.AssemblyStringTransformer;

        /// <inheritdoc />
        public FrameworkName TargetFramework => _configuration.TargetFramework;

        /// <inheritdoc />
        public void Save()
        {
            _configuration.Save();
        }

        /// <inheritdoc />
        public void Save(ConfigurationSaveMode saveMode)
        {
            _configuration.Save(saveMode);
        }

        /// <inheritdoc />
        public void Save(ConfigurationSaveMode saveMode, bool forceSaveAll)
        {
            _configuration.Save(saveMode, forceSaveAll);
        }

        /// <inheritdoc />
        public void SaveAs(string fileName)
        {
            _configuration.SaveAs(fileName);
        }

        /// <inheritdoc />
        public void SaveAs(string fileName, ConfigurationSaveMode saveMode)
        {
            _configuration.SaveAs(fileName, saveMode);
        }

        /// <inheritdoc />
        public void SaveAs(string fileName, ConfigurationSaveMode saveMode, bool forceSaveAll)
        {
            _configuration.SaveAs(fileName, saveMode, forceSaveAll);
        }
    }
}