using System;
using System.Runtime.InteropServices;
using SystemInterface;
using SystemInterface.Reflection;
using SystemWrapper.Reflection;

namespace SystemWrapper
{
    /// <summary>
    /// Wrapper for <see cref="System.AppDomain"/> class.
    /// </summary>
    [ComVisible(true)]
    [Serializable]
    public class AppDomainWrap : IAppDomain
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.AppDomainWrap"/> class.
        /// </summary>
        /// <param name="appDomain">AppDomain object.</param>
        public AppDomainWrap(AppDomain appDomain)
        {
            Initialize(appDomain);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.AppDomainWrap"/> class.
        /// </summary>
        /// <param name="appDomain">AppDomain object.</param>
        public void Initialize(AppDomain appDomain)
        {
            AppDomainInstance = appDomain;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.AppDomainWrap"/> class and creates a new application domain with the specified name.
        /// </summary>
        /// <param name="friendlyName">The friendly name of the domain.</param>
        public AppDomainWrap(string friendlyName)
        {
            Initialize(friendlyName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.AppDomainWrap"/> class and creates a new application domain with the specified name.
        /// </summary>
        /// <param name="friendlyName">The friendly name of the domain.</param>
        public void Initialize(string friendlyName)
        {
            AppDomainInstance = AppDomain.CreateDomain(friendlyName);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public AppDomain AppDomainInstance { get; private set; }

        /// <inheritdoc />
        public IAppDomain CurrentDomain
        {
            get { return new AppDomainWrap(AppDomain.CurrentDomain); }
        }

        /// <inheritdoc />
        public object GetData(string name)
        {
            return AppDomainInstance.GetData(name);
        }

        /// <inheritdoc />
        public IAssembly Load(IAssemblyName assemblyRef)
        {
            return new AssemblyWrap(AppDomainInstance.Load(assemblyRef.AssemblyNameInstance));
        }

        /// <inheritdoc />
        public void SetData(string name, object data)
        {
            AppDomainInstance.SetData(name, data);
        }

        /// <inheritdoc />
        public void Unload(IAppDomain domain)
        {
            AppDomain.Unload(domain.AppDomainInstance);
        }

        /// <inheritdoc />
        event ResolveEventHandler IAppDomain.AssemblyResolve
        {
            add { AppDomainInstance.AssemblyResolve += value; }
            remove { AppDomainInstance.AssemblyResolve -= value; }
        }
    }
}