using System;
using System.Configuration;
using System.Runtime.Versioning;

namespace SystemInterface.Configuration
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Configuration.Configuration"/> class.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// See <see cref="System.Configuration.Configuration.AppSettings"/>.
        /// </summary>
        AppSettingsSection AppSettings { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.ConnectionStrings"/>.
        /// </summary>
        ConnectionStringsSection ConnectionStrings { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.FilePath"/>.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.HasFile"/>.
        /// </summary>
        bool HasFile { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.Locations"/>.
        /// </summary>
        ConfigurationLocationCollection Locations { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.EvaluationContext"/>.
        /// </summary>
        ContextInformation EvaluationContext { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.RootSectionGroup"/>.
        /// </summary>
        ConfigurationSectionGroup RootSectionGroup { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.Sections"/>.
        /// </summary>
        ConfigurationSectionCollection Sections { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.Sections"/>.
        /// </summary>
        ConfigurationSectionGroupCollection SectionGroups { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.NamespaceDeclared"/>.
        /// </summary>
        bool NamespaceDeclared { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.GetSection(string)"/>.
        /// </summary>
        ConfigurationSection GetSection(string sectionName);

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.GetSectionGroup(string)"/>.
        /// </summary>
        ConfigurationSectionGroup GetSectionGroup(string sectionGroupName);

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.TypeStringTransformer"/>.
        /// </summary>
        Func<string, string> TypeStringTransformer { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.AssemblyStringTransformer"/>.
        /// </summary>
        Func<string, string> AssemblyStringTransformer { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.TargetFramework"/>.
        /// </summary>
        FrameworkName TargetFramework { get; }

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.Save()"/>.
        /// </summary>
        void Save();

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.Save(ConfigurationSaveMode)"/>.
        /// </summary>
        void Save(ConfigurationSaveMode saveMode);

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.Save(ConfigurationSaveMode,bool)"/>.
        /// </summary>
        void Save(ConfigurationSaveMode saveMode, bool forceSaveAll);

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.SaveAs(string)"/>.
        /// </summary>
        void SaveAs(string fileName);

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.SaveAs(string,ConfigurationSaveMode)"/>.
        /// </summary>
        void SaveAs(string fileName, ConfigurationSaveMode saveMode);

        /// <summary>
        /// See <see cref="System.Configuration.Configuration.SaveAs(string,ConfigurationSaveMode,bool)"/>.
        /// </summary>
        void SaveAs(string fileName, ConfigurationSaveMode saveMode, bool forceSaveAll);
    }
}