using System;
using System.Security.AccessControl;
using Microsoft.Win32;
using SystemInterface.Microsoft.Win32;
using SystemInterface.Microsoft.Win32.SafeHandles;
using SystemInterface.Security.AccessControl;
using SystemWrapper.Microsoft.Win32.SafeHandles;
using SystemWrapper.Security.AccessControl;

namespace SystemWrapper.Microsoft.Win32
{
    /// <summary>
    ///
    /// </summary>
    public class RegistryKeyWrap : IRegistryKey
    {
        #region Implementation of IDisposable

        /// <summary>
        /// Implementation of the Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            RegistryKeyInstance.Dispose();
        }

        #endregion Implementation of IDisposable

        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Microsoft.Win32.RegistryKeyWrap"/> class.
        /// </summary>
        /// <param name="registryKey"></param>
        public RegistryKeyWrap(RegistryKey registryKey)
        {
            Initialize(registryKey);
        }

        /// <summary>
        /// Initiatlized the RegistryKey instance.
        /// </summary>
        /// <param name="registryKey"></param>
        public void Initialize(RegistryKey registryKey)
        {
            this._registryKeyInstance = registryKey;
        }

        #endregion Constructors and Initializers

        #region Implementation of IRegistryKey

        private RegistryKey _registryKeyInstance;

        /// <summary>
        /// Gets <see cref="T:Microsoft.Win32.RegistryKey"/> object.
        /// </summary>
        public RegistryKey RegistryKeyInstance
        {
            get { return this._registryKeyInstance; }
        }

        /// <summary>
        ///
        /// </summary>
        public ISafeRegistryHandle Handle
        {
            get { return new SafeRegistryHandleWrap(RegistryKeyInstance.Handle); }
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        public string Name
        {
            get { return RegistryKeyInstance.Name; }
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        public int SubKeyCount
        {
            get { return RegistryKeyInstance.SubKeyCount; }
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        public int ValueCount
        {
            get { return RegistryKeyInstance.ValueCount; }
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        public RegistryView View
        {
            get { return RegistryKeyInstance.View; }
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        public void Close()
        {
            RegistryKeyInstance.Close();
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <returns></returns>
        public IRegistryKey CreateSubKey(string subkey)
        {
            RegistryKey key = RegistryKeyInstance.CreateSubKey(subkey);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <returns></returns>
        public IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
        {
            RegistryKey key = RegistryKeyInstance.CreateSubKey(subkey, permissionCheck);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
        {
            RegistryKey key = RegistryKeyInstance.CreateSubKey(subkey, permissionCheck, options);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="registrySecurity"></param>
        /// <returns></returns>
        public IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, IRegistrySecurity registrySecurity)
        {
            RegistryKey key = RegistryKeyInstance.CreateSubKey(subkey, permissionCheck, registrySecurity.RegistrySecurityInstance);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="registryOptions"></param>
        /// <param name="registrySecurity"></param>
        /// <returns></returns>
        public IRegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, IRegistrySecurity registrySecurity)
        {
            RegistryKey key = RegistryKeyInstance.CreateSubKey(subkey, permissionCheck, registryOptions, registrySecurity.RegistrySecurityInstance);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        public void DeleteSubKey(string subkey)
        {
            RegistryKeyInstance.DeleteSubKey(subkey);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="throwOnMissingSubKey"></param>
        public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
        {
            RegistryKeyInstance.DeleteSubKey(subkey, throwOnMissingSubKey);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        public void DeleteSubKeyTree(string subkey)
        {
            RegistryKeyInstance.DeleteSubKeyTree(subkey);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="subkey"></param>
        /// <param name="throwOnMissingSubKey"></param>
        public void DeleteSubKeyTree(string subkey, bool throwOnMissingSubKey)
        {
            RegistryKeyInstance.DeleteSubKeyTree(subkey, throwOnMissingSubKey);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        public void DeleteValue(string name)
        {
            RegistryKeyInstance.DeleteValue(name);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="throwOnMissingValue"></param>
        public void DeleteValue(string name, bool throwOnMissingValue)
        {
            RegistryKeyInstance.DeleteValue(name, throwOnMissingValue);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        public void Flush()
        {
            RegistryKeyInstance.Flush();
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public IRegistryKey FromHandle(ISafeRegistryHandle handle)
        {
            RegistryKey key = RegistryKey.FromHandle(handle.SafeRegistryHandleInstance);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public IRegistryKey FromHandle(ISafeRegistryHandle handle, RegistryView view)
        {
            RegistryKey key = RegistryKey.FromHandle(handle.SafeRegistryHandleInstance, view);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <returns></returns>
        public IRegistrySecurity GetAccessControl()
        {
            RegistrySecurity rs = RegistryKeyInstance.GetAccessControl();
            if (null == rs)
                return null;
            else
                return new RegistrySecurityWrap(rs);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="includeSections"></param>
        /// <returns></returns>
        public IRegistrySecurity GetAccessControl(AccessControlSections includeSections)
        {
            RegistrySecurity rs = RegistryKeyInstance.GetAccessControl(includeSections);
            if (null == rs)
                return null;
            else
                return new RegistrySecurityWrap(rs);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <returns></returns>
        public string[] GetSubKeyNames()
        {
            return RegistryKeyInstance.GetSubKeyNames();
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            return RegistryKeyInstance.GetValue(name);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public object GetValue(string name, object defaultValue)
        {
            return RegistryKeyInstance.GetValue(name, defaultValue);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public object GetValue(string name, object defaultValue, RegistryValueOptions options)
        {
            return RegistryKeyInstance.GetValue(name, defaultValue, options);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RegistryValueKind GetValueKind(string name)
        {
            return RegistryKeyInstance.GetValueKind(name);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <returns></returns>
        public string[] GetValueNames()
        {
            return RegistryKeyInstance.GetValueNames();
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="hKey"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public IRegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
        {
            RegistryKey key = RegistryKey.OpenBaseKey(hKey, view);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="hKey"></param>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public IRegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
        {
            RegistryKey key = RegistryKey.OpenRemoteBaseKey(hKey, machineName);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="hKey"></param>
        /// <param name="machineName"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public IRegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName, RegistryView view)
        {
            RegistryKey key = RegistryKey.OpenRemoteBaseKey(hKey, machineName, view);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IRegistryKey OpenSubKey(string name)
        {
            RegistryKey key = RegistryKeyInstance.OpenSubKey(name);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="permissionCheck"></param>
        /// <returns></returns>
        public IRegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
        {
            RegistryKey key = RegistryKeyInstance.OpenSubKey(name, permissionCheck);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="writable"></param>
        /// <returns></returns>
        public IRegistryKey OpenSubKey(string name, bool writable)
        {
            RegistryKey key = RegistryKeyInstance.OpenSubKey(name, writable);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="permissionCheck"></param>
        /// <param name="rights"></param>
        /// <returns></returns>
        public IRegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
        {
            RegistryKey key = RegistryKeyInstance.OpenSubKey(name, permissionCheck, rights);
            if (null == key)
                return null;
            else
                return new RegistryKeyWrap(key);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="registrySecurity"></param>
        public void SetAccessControl(IRegistrySecurity registrySecurity)
        {
            RegistryKeyInstance.SetAccessControl(registrySecurity.RegistrySecurityInstance);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetValue(string name, object value)
        {
            RegistryKeyInstance.SetValue(name, value);
        }

        /// <summary>
        /// TODO: Add Comment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="valueKind"></param>
        public void SetValue(string name, object value, RegistryValueKind valueKind)
        {
            RegistryKeyInstance.SetValue(name, value, valueKind);
        }

        #endregion Implementation of IRegistryKey
    }
}