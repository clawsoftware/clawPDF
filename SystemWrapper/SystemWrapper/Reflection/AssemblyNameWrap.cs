using System;
using System.Configuration.Assemblies;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using SystemInterface;
using SystemInterface.Reflection;

namespace SystemWrapper.Reflection
{
    /// <summary>
    /// Wrapper for <see cref="System.Reflection.AssemblyName"/> class.
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    public class AssemblyNameWrap : IAssemblyName
    {
        #region Constructors and Initializers

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        public AssemblyNameWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        public void Initialize()
        {
            AssemblyNameInstance = new AssemblyName();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        /// <param name="assemblyName">AssemblyName object.</param>
        public AssemblyNameWrap(AssemblyName assemblyName)
        {
            Initialize(assemblyName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        /// <param name="assemblyName">AssemblyName object.</param>
        public void Initialize(AssemblyName assemblyName)
        {
            AssemblyNameInstance = assemblyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        /// <param name="assemblyName">The display name of the assembly, as returned by the FullName property.</param>
        public AssemblyNameWrap(string assemblyName)
        {
            Initialize(assemblyName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        /// <param name="assemblyName">The display name of the assembly, as returned by the FullName property.</param>
        public void Initialize(string assemblyName)
        {
            AssemblyNameInstance = new AssemblyName(assemblyName);
        }

        #endregion Constructors and Initializers

        /// <inheritdoc />
        public AssemblyName AssemblyNameInstance { get; private set; }

        /// <inheritdoc />
        public string CodeBase
        {
            get { return AssemblyNameInstance.CodeBase; }
            set { AssemblyNameInstance.CodeBase = value; }
        }

        /// <inheritdoc />
        public CultureInfo CultureInfo
        {
            get { return AssemblyNameInstance.CultureInfo; }
            set { AssemblyNameInstance.CultureInfo = value; }
        }

        /// <inheritdoc />
        public string EscapedCodeBase
        {
            get { return AssemblyNameInstance.EscapedCodeBase; }
        }

        /// <inheritdoc />
        public AssemblyNameFlags Flags
        {
            get { return AssemblyNameInstance.Flags; }
            set { AssemblyNameInstance.Flags = value; }
        }

        /// <inheritdoc />
        public string FullName
        {
            get { return AssemblyNameInstance.FullName; }
        }

        /// <inheritdoc />
        public AssemblyHashAlgorithm HashAlgorithm
        {
            get { return AssemblyNameInstance.HashAlgorithm; }
            set { AssemblyNameInstance.HashAlgorithm = value; }
        }

        /// <inheritdoc />
        public StrongNameKeyPair KeyPair
        {
            get { return AssemblyNameInstance.KeyPair; }
            set { AssemblyNameInstance.KeyPair = value; }
        }

        /// <inheritdoc />
        public string Name
        {
            get { return AssemblyNameInstance.Name; }
            set { AssemblyNameInstance.Name = value; }
        }

        /// <inheritdoc />
        public ProcessorArchitecture ProcessorArchitecture
        {
            get { return AssemblyNameInstance.ProcessorArchitecture; }
            set { AssemblyNameInstance.ProcessorArchitecture = value; }
        }

        /// <inheritdoc />
        public IVersion Version
        {
            get { return new VersionWrap(AssemblyNameInstance.Version); }
            set { AssemblyNameInstance.Version = value.VersionInstance; }
        }

        /// <inheritdoc />
        public AssemblyVersionCompatibility VersionCompatibility
        {
            get { return AssemblyNameInstance.VersionCompatibility; }
            set { AssemblyNameInstance.VersionCompatibility = value; }
        }

        /// <inheritdoc />
        public object Clone()
        {
            return AssemblyNameInstance.Clone();
        }

        /// <inheritdoc />
        public IAssemblyName GetAssemblyName(string assemblyFile)
        {
            return new AssemblyNameWrap(AssemblyName.GetAssemblyName(assemblyFile));
        }

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            AssemblyNameInstance.GetObjectData(info, context);
        }

        /// <inheritdoc />
        public byte[] GetPublicKey()
        {
            return AssemblyNameInstance.GetPublicKey();
        }

        /// <inheritdoc />
        public byte[] GetPublicKeyToken()
        {
            return AssemblyNameInstance.GetPublicKeyToken();
        }

        /// <inheritdoc />
        public void OnDeserialization(object sender)
        {
            AssemblyNameInstance.OnDeserialization(sender);
        }

        /// <inheritdoc />
        public bool ReferenceMatchesDefinition(IAssemblyName reference, IAssemblyName definition)
        {
            return AssemblyName.ReferenceMatchesDefinition(reference.AssemblyNameInstance, definition.AssemblyNameInstance);
        }

        /// <inheritdoc />
        public void SetPublicKey(byte[] publicKey)
        {
            AssemblyNameInstance.SetPublicKey(publicKey);
        }

        /// <inheritdoc />
        public void SetPublicKeyToken(byte[] publicKeyToken)
        {
            AssemblyNameInstance.SetPublicKeyToken(publicKeyToken);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return AssemblyNameInstance.ToString();
        }

        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        internal static IAssemblyName[] ConvertFileInfoArrayIntoIFileInfoWrapArray(AssemblyName[] assemblyNames)
        {
            AssemblyNameWrap[] wrapAssemblyNames = new AssemblyNameWrap[assemblyNames.Length];
            for (int i = 0; i < assemblyNames.Length; i++)
                wrapAssemblyNames[i] = new AssemblyNameWrap(assemblyNames[i]);
            return wrapAssemblyNames;
        }
    }
}