using System;
using System.Security.AccessControl;
using Microsoft.Win32;
using SystemInterface.Microsoft.Win32.SafeHandles;
using SystemInterface.Security.AccessControl;

namespace SystemInterface.Microsoft.Win32
{
    /// <summary>
    /// Wrapper for <see cref="T:Microsoft.Win32.RegistryKey"/> class.
    /// </summary>
    public interface IRegistryKey : IDisposable
    {
        #region Properties

        /// <inheritdoc />
        ISafeRegistryHandle Handle { get; }

        /// <inheritdoc />
        string Name { get; }

        /// <summary>
        /// Gets <see cref="T:Microsoft.Win32.RegistryKey"/> object.
        /// </summary>
        RegistryKey RegistryKeyInstance { get; }

        /// <inheritdoc />
        int SubKeyCount { get; }

        /// <inheritdoc />
        int ValueCount { get; }

        /// <summary>
        /// Initialize IRegistryKeyWrap.
        /// </summary>
        /// <param name="registryKey">RegistryKey object.</param>
        void Initialize(RegistryKey registryKey);

        /// <inheritdoc />
        RegistryView View { get; }

        #endregion Properties

        /// <summary>
        /// Closes the key and flushes it to disk if its contents have been modified.
        /// </summary>
        void Close();

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <returns></returns>
        IRegistryKey CreateSubKey(string subkey);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <returns></returns>
        IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="registrySecurity"></param>
        /// <returns></returns>
        IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, IRegistrySecurity registrySecurity);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="registryOptions"></param>
        /// <param name="registrySecurity"></param>
        /// <returns></returns>
        IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, IRegistrySecurity registrySecurity);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        void DeleteSubKey(string subkey);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="throwOnMissingSubKey"></param>
        void DeleteSubKey(string subkey, bool throwOnMissingSubKey);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        void DeleteSubKeyTree(string subkey);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="throwOnMissingSubKey"></param>
        void DeleteSubKeyTree(string subkey, bool throwOnMissingSubKey);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        void DeleteValue(string name);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="throwOnMissingValue"></param>
        void DeleteValue(string name, bool throwOnMissingValue);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        void Flush();

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        IRegistryKey FromHandle(ISafeRegistryHandle handle);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        IRegistryKey FromHandle(ISafeRegistryHandle handle, RegistryView view);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <returns></returns>
        IRegistrySecurity GetAccessControl();

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="includeSections"></param>
        /// <returns></returns>
        IRegistrySecurity GetAccessControl(AccessControlSections includeSections);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <returns></returns>
        string[] GetSubKeyNames();

        /// <summary>
        /// Retrieves the value associated with the specified name. Returns null if the name/value pair does not exist in the registry.
        /// </summary>
        /// <param name="name">The name of the value to retrieve. This string is not case-sensitive.</param>
        /// <returns>The value associated with name, or null if name is not found.</returns>
        object GetValue(string name);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        object GetValue(string name, object defaultValue);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        object GetValue(string name, object defaultValue, RegistryValueOptions options);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        RegistryValueKind GetValueKind(string name);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <returns></returns>
        string[] GetValueNames();

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="hKey"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        IRegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="hKey"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        IRegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="hKey"></param>
        /// <param name="machineName"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        IRegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName, RegistryView view);

        /// <summary>
        /// Retrieves a subkey as read-only.
        /// </summary>
        /// <param name="name">The name or path of the subkey to open as read-only. </param>
        /// <returns>The subkey requested, or null if the operation failed.</returns>
        IRegistryKey OpenSubKey(string name);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="permissionCheck"></param>
        /// <returns></returns>
        IRegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck);

        /// <summary>
        /// Retrieves a specified subkey, and specifies whether write access is to be applied to the key.
        /// </summary>
        /// <param name="name">Name or path of the subkey to open. </param>
        /// <param name="writable">Set to true if you need write access to the key. </param>
        /// <returns>The subkey requested, or null if the operation failed.</returns>
        IRegistryKey OpenSubKey(string name, bool writable);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="rights"></param>
        /// <returns></returns>
        IRegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="registrySecurity"></param>
        void SetAccessControl(IRegistrySecurity registrySecurity);

        /// <summary>
        /// Sets the specified name/value pair.
        /// </summary>
        /// <param name="name">The name of the value to store.</param>
        /// <param name="value">The data to be stored.</param>
        void SetValue(string name, object value);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="valueKind"></param>
        void SetValue(string name, object value, RegistryValueKind valueKind);

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
