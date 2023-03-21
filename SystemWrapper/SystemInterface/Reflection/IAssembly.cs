using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using SystemInterface.IO;

namespace SystemInterface.Reflection
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Wrapper for <see cref="System.Reflection.Assembly"/> class.
    /// </summary>
    public interface IAssembly : IEvidenceFactory, ICustomAttributeProvider, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Reflection.AssemblyWrap"/> class.
        /// </summary>
        /// <param name="assembly">Assembly object.</param>
        void Initialize(Assembly assembly);

        // Properties

        /// <summary>
        /// Gets <see cref="T:System.Reflection.Assembly"/> object.
        /// </summary>
        Assembly AssemblyInstance { get; }

        /// <summary>
        /// Gets the location of the assembly as specified originally, for example, in an <see cref="System.Reflection.AssemblyName"/> object.
        /// </summary>
        string CodeBase { get; }

        /// <summary>
        /// Gets the entry point of this assembly.
        /// </summary>
        MethodInfo EntryPoint { get; }

        /// <summary>
        /// Gets the URI, including escape characters, that represents the codebase.
        /// </summary>
        string EscapedCodeBase { get; }

        /// <summary>
        /// Gets the display name of the assembly.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets a value indicating whether the assembly was loaded from the global assembly cache.
        /// </summary>
        bool GlobalAssemblyCache { get; }

        /// <summary>
        /// Gets the host context with which the assembly was loaded.
        /// </summary>
        [ComVisible(false)]
        long HostContext { get; }

        /// <summary>
        /// Gets a string representing the version of the common language runtime (CLR) saved in the file containing the manifest.
        /// </summary>
        [ComVisible(false)]
        string ImageRuntimeVersion { get; }

        /// <summary>
        /// Gets the path or UNC location of the loaded file that contains the manifest.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// Gets the module that contains the manifest for the current assembly.
        /// </summary>
        [ComVisible(false)]
        Module ManifestModule { get; }

        /// <summary>
        /// Gets a <see cref="T:System.Boolean"/> value indicating whether this assembly was loaded into the reflection-only context.
        /// </summary>
        [ComVisible(false)]
        bool ReflectionOnly { get; }

        // Methods

        /// <summary>
        /// Locates the specified type from this assembly and creates an instance of it using the system activator, using case-sensitive search.
        /// </summary>
        /// <param name="typeName">The Type.FullName of the type to locate.</param>
        /// <returns>An instance of Object representing the type, with culture, arguments, binder, and activation attributes set to nullNothingnullptra null reference (Nothing in Visual Basic), and BindingFlags set to Public or Instance, or nullNothingnullptra null reference (Nothing in Visual Basic) if typeName is not found.</returns>
        object CreateInstance(string typeName);

        /// <summary>
        /// Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search.
        /// </summary>
        /// <param name="typeName">The Type.FullName of the type to locate.</param>
        /// <param name="ignoreCase"> <c>true</c> to ignore the case of the type name; otherwise, false.</param>
        /// <returns>An instance of Object representing the type, with culture, arguments, binder, and activation attributes set to nullNothingnullptra null reference (Nothing in Visual Basic), and BindingFlags set to Public or Instance, or nullNothingnullptra null reference (Nothing in Visual Basic) if typeName is not found.</returns>
        object CreateInstance(string typeName, bool ignoreCase);

        /// <summary>
        /// Locates the specified type from this assembly and creates an instance of it using the system activator, with optional case-sensitive search and having the specified culture, arguments, and binding and activation attributes.
        /// </summary>
        /// <param name="typeName">The Type.FullName of the type to locate.</param>
        /// <param name="ignoreCase"> <c>true</c> to ignore the case of the type name; otherwise, <c>false</c>.</param>
        /// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of bit flags from BindingFlags.</param>
        /// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If binder is nullNothingnullptra null reference (Nothing in Visual Basic), the default binder is used.</param>
        /// <param name="args">An array of type Object containing the arguments to be passed to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to be invoked. If the default constructor is desired, args must be an empty array or nullNothingnullptra null reference (Nothing in Visual Basic).</param>
        /// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is nullNothingnullptra null reference (Nothing in Visual Basic), the CultureInfo for the current thread is used. (This is necessary to convert a String that represents 1000 to a Double value, for example, since 1000 is represented differently by different cultures.)</param>
        /// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single UrlAttribute object. The UrlAttribute specifies the URL that is required to activate a remote object.</param>
        /// <returns>An instance of Object representing the type and matching the specified criteria, or nullNothingnullptra null reference (Nothing in Visual Basic) if typeName is not found.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:DocumentationTextMustEndWithAPeriod", Justification = "Reviewed. Suppression is OK here.")]
        object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes);

        /// <summary>
        /// Creates the name of a type qualified by the display name of its assembly.
        /// </summary>
        /// <param name="assemblyName">The display name of an assembly.</param>
        /// <param name="typeName">The full name of a type.</param>
        /// <returns>A String that is the full name of the type qualified by the display name of the assembly.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        string CreateQualifiedName(string assemblyName, string typeName);

        /// <summary>
        /// Determines whether this assembly and the specified object are equal.
        /// </summary>
        /// <param name="o">The object to compare with this instance.</param>
        /// <returns> <c>true</c> if o is equal to this instance; otherwise, false.</returns>
        bool Equals(object o);

        /// <summary>
        /// Gets the currently loaded assembly in which the specified class is defined.
        /// </summary>
        /// <param name="type">A Type object representing a class in the assembly that will be returned.</param>
        /// <returns>The assembly in which the specified class is defined.</returns>
        IAssembly GetAssembly(Type type);

        /// <summary>
        /// Returns the IAssemblyWrap of the method that invoked the currently executing method.
        /// </summary>
        /// <returns>The Assembly object of the method that invoked the currently executing method.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly GetCallingAssembly();

        /// <summary>
        /// Gets the process executable in the default application domain. In other application domains, this is the first executable that was executed by AppDomain.ExecuteAssembly.
        /// </summary>
        /// <returns>The Assembly that is the process executable in the default application domain, or the first executable that was executed by AppDomain.ExecuteAssembly. Can return nullNothingnullptra null reference (Nothing in Visual Basic) when called from unmanaged code. </returns>
        IAssembly GetEntryAssembly();

        /// <summary>
        /// Gets the assembly that contains the code that is currently executing.
        /// </summary>
        /// <returns>A IAssemblyWrap representing the assembly that contains the code that is currently executing. </returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly GetExecutingAssembly();
        
        /// <summary>
        /// Gets the types defined in this assembly.
        /// </summary>
        /// <returns>An array that contains all the types that are defined in this assembly.</returns>
        /// <exception cref="ReflectionTypeLoadException">The assembly contains one or more types that cannot be loaded.The array returned by the Types property of this exception contains a Type object for each type that was loaded and null for each type that could not be loaded, while the LoaderExceptions property contains an exception for each type that could not be loaded.</exception>
        Type[] GetTypes();

        /// <summary>
        /// Gets the public types defined in this assembly that are visible outside the assembly.
        /// </summary>
        /// <returns>An array of Type objects that represent the types defined in this assembly that are visible outside the assembly.</returns>
        Type[] GetExportedTypes();

        /// <summary>
        /// Gets a IFileStreamWrap for the specified file in the file table of the manifest of this assembly.
        /// </summary>
        /// <param name="name">The name of the specified file. Do not include the path to the file.</param>
        /// <returns>A IFileStreamWrap for the specified file, or nullNothingnullptra null reference (Nothing in Visual Basic) if the file is not found.</returns>
        IFileStream GetFile(string name);

        /// <summary>
        /// Gets the files in the file table of an assembly manifest.
        /// </summary>
        /// <returns>An array of IFileStreamWrap objects.</returns>
        IFileStream[] GetFiles();

        /// <summary>
        /// Gets the files in the file table of an assembly manifest, specifying whether to include resource modules.
        /// </summary>
        /// <param name="getResourceModules"> <c>true</c> to include resource modules; otherwise, <c>false</c>.</param>
        /// <returns>An array of IFileStreamWrap objects.</returns>
        IFileStream[] GetFiles(bool getResourceModules);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        int GetHashCode();

        /// <summary>
        /// Gets an <see cref="T:SystemInterface.Reflection.IAssemblyNameWrap"/> for this assembly.
        /// </summary>
        /// <returns>An <see cref="T:SystemInterface.Reflection.IAssemblyNameWrap"/> for this assembly. </returns>
        IAssemblyName GetName();

        /// <summary>
        /// Gets the <see cref="T:SystemInterface.Reflection.IAssemblyNameWrap"/> objects for all the assemblies referenced by this assembly.
        /// </summary>
        /// <returns>An array of type <see cref="T:SystemInterface.Reflection.IAssemblyNameWrap"/> containing all the assemblies referenced by this assembly.</returns>
        IAssemblyName[] GetReferencedAssemblies();

        /// <summary>
        /// Loads an assembly given its file name or path.
        /// </summary>
        /// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
        /// <returns>The loaded assembly. </returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        [Obsolete("This interface for static method Assembly.LoadFrom(string) was moved to the IAssemblyFactory.LoadFrom(string).")]
        IAssembly LoadFrom(string assemblyFile);

        /// <summary>
        /// Loads an assembly given the long form of its name.
        /// </summary>
        /// <param name="assemblyString">The long form of the assembly name. </param>
        /// <returns>The loaded assembl.</returns>
        [System.Security.SecuritySafeCritical]
        [MethodImplAttribute(MethodImplOptions.NoInlining)]
        [Obsolete("This interface for static method Assembly.LoadFrom(string) was moved to the IAssemblyFactory.LoadFrom(string).")]
        IAssembly Load(string assemblyString);

        /// <summary>
        /// Returns the names of all the resources in this assembly.
        /// </summary>
        /// <returns>An array that contains the names of all the resources.</returns>
        string[] GetManifestResourceNames();

        /// <summary>
        /// Loads the specified manifest resource from this assembly.
        /// </summary>
        /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
        /// <returns>The manifest resource; or null if no resources were specified during compilation or if the resource is not visible to the caller.</returns>
        IStream GetManifestResourceStream(string name);

        /*
			  public Module[] GetLoadedModules();
			  public Module[] GetLoadedModules(bool getResourceModules);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName);
			  public virtual string[] GetManifestResourceNames();
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public virtual Stream GetManifestResourceStream(string name);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public virtual Stream GetManifestResourceStream(Type type, string name);
			  public Module GetModule(string name);
			  public Module[] GetModules();
			  public Module[] GetModules(bool getResourceModules);
			  public virtual AssemblyName GetName(bool copiedName);
			  [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
			  public virtual void GetObjectData(SerializationInfo info, StreamingContext context);
			  public Assembly GetSatelliteAssembly(CultureInfo culture);
			  public Assembly GetSatelliteAssembly(CultureInfo culture, Version version);
			  public virtual Type GetType(string name);
			  public virtual Type GetType(string name, bool throwOnError);
			  public Type GetType(string name, bool throwOnError, bool ignoreCase);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public virtual Type[] GetTypes();
			  public virtual bool IsDefined(Type attributeType, bool inherit);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly Load(byte[] rawAssembly);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly Load(AssemblyName assemblyRef);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly Load(string assemblyString);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly Load(string assemblyString, Evidence assemblySecurity);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);
			  [MethodImpl(MethodImplOptions.NoInlining), SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlEvidence)]
			  public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);
			  public static Assembly LoadFile(string path);
			  [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlEvidence)]
			  public static Assembly LoadFile(string path, Evidence securityEvidence);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm);
			  public Module LoadModule(string moduleName, byte[] rawModule);
			  public Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly ReflectionOnlyLoad(string assemblyString);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly ReflectionOnlyLoad(byte[] rawAssembly);
			  [MethodImpl(MethodImplOptions.NoInlining)]
			  public static Assembly ReflectionOnlyLoadFrom(string assemblyFile);
			  Type _Assembly.GetType();
			  public override string ToString();

		   * // Events
			  public event ModuleResolveEventHandler ModuleResolve;

		 */
    }
}
