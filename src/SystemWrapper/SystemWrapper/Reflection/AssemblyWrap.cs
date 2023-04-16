using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Policy;
using SystemInterface.IO;
using SystemInterface.Reflection;
using SystemWrapper.IO;

namespace SystemWrapper.Reflection
{
    /// <summary>
    /// Wrapper for <see cref="System.Reflection.Assembly"/> class.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public class AssemblyWrap : IAssembly
    {
        private Assembly _assembly;

        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyWrap"/> class.
        /// </summary>
        public AssemblyWrap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyWrap"/> class.
        /// </summary>
        /// <param name="assembly">Assembly object.</param>
        public AssemblyWrap(Assembly assembly)
        {
            Initialize(assembly);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyWrap"/> class.
        /// </summary>
        /// <param name="assembly">Assembly object.</param>
        public void Initialize(Assembly assembly)
        {
            _assembly = assembly;
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public Assembly AssemblyInstance
        {
            get
            {
                if (_assembly == null)
                {
                    throw new InvalidOperationException("AssemblyWrap instance was not initialized with Assembly object. Use Initialize() method to set Assembly object.");
                }

                return _assembly;
            }
        }

        /// <inheritdoc />
        public string CodeBase
        {
            get { return AssemblyInstance.CodeBase; }
        }

        /// <inheritdoc />
        public MethodInfo EntryPoint
        {
            get { return AssemblyInstance.EntryPoint; }
        }

        /// <inheritdoc />
        public string EscapedCodeBase
        {
            get { return AssemblyInstance.EscapedCodeBase; }
        }

        /// <inheritdoc />
        public Evidence Evidence
        {
            get { return AssemblyInstance.Evidence; }
        }

        /// <inheritdoc />
        public string FullName
        {
            get { return AssemblyInstance.FullName; }
        }

        /// <inheritdoc />
        public bool GlobalAssemblyCache
        {
            get { return AssemblyInstance.GlobalAssemblyCache; }
        }

        /// <inheritdoc />
        public long HostContext
        {
            get { return AssemblyInstance.HostContext; }
        }

        /// <inheritdoc />
        public string ImageRuntimeVersion
        {
            get { return AssemblyInstance.ImageRuntimeVersion; }
        }

        /// <inheritdoc />
        public string Location
        {
            get { return AssemblyInstance.Location; }
        }

        /// <inheritdoc />
        public Module ManifestModule
        {
            get { return AssemblyInstance.ManifestModule; }
        }

        /// <inheritdoc />
        public bool ReflectionOnly
        {
            get { return AssemblyInstance.ReflectionOnly; }
        }

        /// <inheritdoc />
        public object CreateInstance(string typeName)
        {
            return AssemblyInstance.CreateInstance(typeName);
        }

        /// <inheritdoc />
        public object CreateInstance(string typeName, bool ignoreCase)
        {
            return AssemblyInstance.CreateInstance(typeName, ignoreCase);
        }

        /// <inheritdoc />
        public object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
        {
            return AssemblyInstance.CreateInstance(typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
        }

        /// <inheritdoc />
        public string CreateQualifiedName(string assemblyName, string typeName)
        {
            return Assembly.CreateQualifiedName(assemblyName, typeName);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return AssemblyInstance.Equals(obj);
        }

        /// <inheritdoc />
        public IAssembly GetAssembly(Type type)
        {
            return new AssemblyWrap(Assembly.GetAssembly(type));
        }

        /// <inheritdoc />
        public IAssembly GetCallingAssembly()
        {
            return new AssemblyWrap(Assembly.GetCallingAssembly());
        }

        /// <inheritdoc />
        public virtual object[] GetCustomAttributes(bool inherit)
        {
            return AssemblyInstance.GetCustomAttributes(inherit);
        }

        /// <inheritdoc />
        public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return AssemblyInstance.GetCustomAttributes(attributeType, inherit);
        }

        /// <inheritdoc />
        public IAssembly GetEntryAssembly()
        {
            return new AssemblyWrap(Assembly.GetEntryAssembly());
        }

        /// <inheritdoc />
        public IAssembly GetExecutingAssembly()
        {
            return new AssemblyWrap(Assembly.GetExecutingAssembly());
        }

        /// <inheritdoc />
        public virtual Type[] GetTypes()
        {
            return AssemblyInstance.GetTypes();
        }

        /// <inheritdoc />
        public virtual Type[] GetExportedTypes()
        {
            return AssemblyInstance.GetExportedTypes();
        }

        /// <inheritdoc />
        public IFileStream GetFile(string name)
        {
            return new FileStreamWrap(AssemblyInstance.GetFile(name));
        }

        /// <inheritdoc />
        public virtual IFileStream[] GetFiles()
        {
            return FileStreamWrap.ConvertFileStreamArrayIntoIFileStreamWrapArray(AssemblyInstance.GetFiles());
        }

        /// <inheritdoc />
        public IFileStream[] GetFiles(bool getResourceModules)
        {
            return FileStreamWrap.ConvertFileStreamArrayIntoIFileStreamWrapArray(AssemblyInstance.GetFiles(getResourceModules));
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return AssemblyInstance.GetHashCode();
        }

        /// <inheritdoc />
        public IAssemblyName GetName()
        {
            return new AssemblyNameWrap(AssemblyInstance.GetName());
        }

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            AssemblyInstance.GetObjectData(info, context);
        }

        /// <inheritdoc />
        public IAssemblyName[] GetReferencedAssemblies()
        {
            AssemblyName[] assemblyNames = AssemblyInstance.GetReferencedAssemblies();
            return AssemblyNameWrap.ConvertFileInfoArrayIntoIFileInfoWrapArray(assemblyNames);
        }

        /// <inheritdoc />
        public bool IsDefined(Type attributeType, bool inherit)
        {
            return AssemblyInstance.IsDefined(attributeType, inherit);
        }

        /// <inheritdoc />
        public IAssembly LoadFrom(string assemblyFile)
        {
            return new AssemblyWrap(Assembly.LoadFrom(assemblyFile));
        }

        /// <inheritdoc />
        public IAssembly Load(string assemblyString)
        {
            return new AssemblyWrap(Assembly.Load(assemblyString));
        }

        public string[] GetManifestResourceNames()
        {
            return AssemblyInstance.GetManifestResourceNames();
        }

        public IStream GetManifestResourceStream(string name)
        {
            return new StreamWrap(AssemblyInstance.GetManifestResourceStream(name));
        }
    }
}
