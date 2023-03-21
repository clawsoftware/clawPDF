using System;
using SystemInterface.Reflection;

namespace SystemInterface
{
    /// <summary>
    /// Wrapper for <see cref="System.AppDomain"/> class.
    /// </summary>
    public interface IAppDomain
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.AppDomainWrap"/> class.
        /// </summary>
        /// <param name="appDomain">AppDomain object.</param>
        void Initialize(AppDomain appDomain);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.AppDomainWrap"/> class and creates a new application domain with the specified name.
        /// </summary>
        /// <param name="friendlyName">The friendly name of the domain.</param>
        void Initialize(string friendlyName);

        // Properties

        /// <summary>
        /// Gets <see cref="T:System.AppDomain"/> object.
        /// </summary>
        AppDomain AppDomainInstance { get; }

        /// <summary>
        /// Gets the current application domain for the current Thread.
        /// </summary>
        IAppDomain CurrentDomain { get; }

        // Methods

        /// <summary>
        /// Gets the value stored in the current application domain for the specified name.
        /// </summary>
        /// <param name="name">The name of a predefined application domain property, or the name of an application domain property you have defined. </param>
        /// <returns>The value of the name property. </returns>
        object GetData(string name);

        /// <summary>
        /// Loads an Assembly given its IAssemblyNameWrap.
        /// </summary>
        /// <param name="assemblyRef">An object that describes the assembly to load.</param>
        /// <returns>The loaded assembly.</returns>
        IAssembly Load(IAssemblyName assemblyRef);

        /// <summary>
        /// Assigns the specified value to the specified application domain property.
        /// </summary>
        /// <param name="name">The name of a user-defined application domain property to create or change.</param>
        /// <param name="data">The value of the property.</param>
        void SetData(string name, object data);

        /// <summary>
        /// Unloads the specified application domain.
        /// </summary>
        /// <param name="domain">An application domain to unload.</param>
        void Unload(IAppDomain domain);

        // Events

        /// <summary>
        /// Occurs when the resolution of an assembly fails.
        /// </summary>
        event ResolveEventHandler AssemblyResolve;

        /*

                   // Events
            public event AssemblyLoadEventHandler AssemblyLoad;
            public event EventHandler DomainUnload;
            public event EventHandler ProcessExit;
            public event ResolveEventHandler ReflectionOnlyAssemblyResolve;
            public event ResolveEventHandler ResourceResolve;
            public event ResolveEventHandler TypeResolve;
            public event UnhandledExceptionEventHandler UnhandledException;

            // Methods
            [ComVisible(false)]
            public string ApplyPolicy(string assemblyName);
            public ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName);
            public ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm);
            public static AppDomain CreateDomain(string friendlyName);
            public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo);
            [SecurityPermission(SecurityAction.Demand, ControlAppDomain=true)]
            public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info);
            public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info, PermissionSet grantSet, params StrongName[] fullTrustAssemblies);
            public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles);
            public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs);
            public ObjectHandle CreateInstance(string assemblyName, string typeName);
            public ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes);
            public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);
            public object CreateInstanceAndUnwrap(string assemblyName, string typeName);
            public object CreateInstanceAndUnwrap(string assemblyName, string typeName, object[] activationAttributes);
            public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);
            public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);
            public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes);
            public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);
            public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName);
            public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, object[] activationAttributes);
            public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes);
            public void DoCallBack(CrossAppDomainDelegate callBackDelegate);
            public int ExecuteAssembly(string assemblyFile);
            public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity);
            public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args);
            public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm);
            public int ExecuteAssemblyByName(string assemblyName);
            public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity);
            public int ExecuteAssemblyByName(AssemblyName assemblyName, Evidence assemblySecurity, params string[] args);
            public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity, params string[] args);
            [MethodImpl(MethodImplOptions.InternalCall)]
            public Assembly[] GetAssemblies();
            [Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.  http://go.microsoft.com/fwlink/?linkid=14202", false), DllImport("kernel32.dll")]
            public static extern int GetCurrentThreadId();
            public Type GetType();
            public override object InitializeLifetimeService();
            public bool IsDefaultAppDomain();
            [MethodImpl(MethodImplOptions.InternalCall), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            public extern bool IsFinalizingForUnload();
            [MethodImpl(MethodImplOptions.NoInlining)]
            public Assembly Load(string assemblyString);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public Assembly Load(byte[] rawAssembly);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public Assembly Load(string assemblyString, Evidence assemblySecurity);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);
            [MethodImpl(MethodImplOptions.NoInlining), SecurityPermission(SecurityAction.Demand, ControlEvidence=true)]
            public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);
            public Assembly[] ReflectionOnlyGetAssemblies();
            [SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy=true)]
            public void SetAppDomainPolicy(PolicyLevel domainPolicy);
            [SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain=true)]
            public void SetData(string name, object data, IPermission permission);
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlPrincipal)]
            public void SetPrincipalPolicy(PrincipalPolicy policy);
            [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlPrincipal)]
            public void SetThreadPrincipal(IPrincipal principal);
            public override string ToString();
            [ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail), SecurityPermission(SecurityAction.Demand, ControlAppDomain=true)]
            public static void Unload(AppDomain domain);

            // Properties
            public ActivationContext ActivationContext { [SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy=true)] get; }
            public ApplicationIdentity ApplicationIdentity { [SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy=true)] get; }
            public ApplicationTrust ApplicationTrust { [SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy=true)] get; }
            public string BaseDirectory { get; }
            public AppDomainManager DomainManager { [SecurityPermission(SecurityAction.LinkDemand, ControlDomainPolicy=true)] get; }
            public string DynamicDirectory { get; }
            public Evidence Evidence { [SecurityPermission(SecurityAction.Demand, ControlEvidence=true)] get; }
            public string FriendlyName { get; }
            public int Id { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get; }
            public string RelativeSearchPath { get; }
            public AppDomainSetup SetupInformation { get; }
            public bool ShadowCopyFiles { get; }
        */
    }
}
