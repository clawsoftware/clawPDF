using Microsoft.Win32;

namespace SystemInterface.Microsoft.Win32
{
    /// <summary>
    /// Wrapper for <see cref="T:Microsoft.Win32.Registry"/> class.
    /// </summary>
    public interface IRegistry
    {
        #region Properties

        /// <inheritdoc />
        IRegistryKey ClassesRoot { get; }

        /// <inheritdoc />
        IRegistryKey CurrentConfig { get; }

        /// <inheritdoc />
        IRegistryKey CurrentUser { get; }

        /// <inheritdoc />
        IRegistryKey LocalMachine { get; }

        /// <inheritdoc />
        IRegistryKey PerformanceData { get; }

        /// <inheritdoc />
        IRegistryKey Users { get; }

        #endregion Properties

        #region Functions

        /// <inheritdoc />
        object GetValue(string keyName, string valueName, object defaultValue);

        /// <inheritdoc />
        void SetValue(string keyName, string valueName, object value);

        /// <inheritdoc />
        void SetValue(string keyName, string valueName, object value, RegistryValueKind valueKind);

        #endregion Functions
    }
}
