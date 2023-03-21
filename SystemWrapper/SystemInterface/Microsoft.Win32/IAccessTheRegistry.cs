using System;

namespace SystemInterface.Microsoft.Win32
{
    /// <summary>
    /// This is an interface that demonstrates a good practice
    /// for implementing a class that accesses the registry.
    /// </summary>
    public interface IAccessTheRegistry
    {
        /// <summary>
        /// This property allows for passing in a Mock registry key
        /// object that implements IRegistryKey to Mock the registry.
        /// The implementor should use RegistryKeyWrap in production.
        /// </summary>
        /// <example>
        /// <code>
        /// private IRegistryKey _BaseKey;
        ///
        /// IRegistryKey BaseKey
        /// {
        ///    get
        ///    {
        ///        if (null == _BaseKey)
        ///            _BaseKey = new RegistryWrap().LocalMachine;
        ///        return _BaseKey;
        ///    }
        /// }
        /// </code>
        /// </example>
        IRegistryKey BaseKey { get; }

        /// <summary>
        /// This allows for switching the BaseKey to a different IRegistry object.
        /// It is expected this will only be used to mock the registry for unit testing.
        /// We use a function because it is intended that the property should remain
        /// read only.
        /// </summary>
        /// <example>
        /// <code>
        /// public void ChangeBaseKey(IRegistryKey inBaseKey)
        /// {
        ///     _BaseKey = inBaseKey;
        /// }
        /// </code>
        /// </example>
        /// <param name="inBaseKey"></param>
        void ChangeBaseKey(IRegistryKey inBaseKey);
    }
}
