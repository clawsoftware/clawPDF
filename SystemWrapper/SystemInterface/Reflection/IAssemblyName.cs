using System.Configuration.Assemblies;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace SystemInterface.Reflection
{
    /// <summary>
    /// Wrapper for <see cref="System.Reflection.AssemblyName"/> class.
    /// </summary>
    public interface IAssemblyName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        /// <param name="assemblyName">AssemblyName object.</param>
        void Initialize(AssemblyName assemblyName);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Reflection.AssemblyNameWrap"/> class.
        /// </summary>
        /// <param name="assemblyName">The display name of the assembly, as returned by the FullName property.</param>
        void Initialize(string assemblyName);

        // Properties

        /// <summary>
        /// Gets <see cref="T:System.Reflection.AssemblyName"/> object.
        /// </summary>
        AssemblyName AssemblyNameInstance { get; }

        /// <summary>
        /// Gets or sets the location of the assembly as a URL.
        /// </summary>
        string CodeBase { get; set; }

        /// <summary>
        /// Gets or sets the culture supported by the assembly.
        /// </summary>
        CultureInfo CultureInfo { get; set; }

        /// <summary>
        /// Gets the URI, including escape characters, that represents the codebase.
        /// </summary>
        string EscapedCodeBase { get; }

        /// <summary>
        /// Gets or sets the attributes of the assembly.
        /// </summary>
        AssemblyNameFlags Flags { get; set; }

        /// <summary>
        /// Gets the full name of the assembly, also known as the display name.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets or sets the hash algorithm used by the assembly manifest.
        /// </summary>
        AssemblyHashAlgorithm HashAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets the public and private cryptographic key pair that is used to create a strong name signature for the assembly.
        /// </summary>
        StrongNameKeyPair KeyPair { get; set; }

        /// <summary>
        /// Gets or sets the simple name of the assembly. This is usually, but not necessarily, the file name of the manifest file of the assembly, minus its extension.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that identifies the processor and bits-per-word of the platform targeted by an executable.
        /// </summary>
        ProcessorArchitecture ProcessorArchitecture { get; set; }

        /// <summary>
        /// Gets or sets the major, minor, build, and revision numbers of the assembly.
        /// </summary>
        IVersion Version { get; set; }

        /// <summary>
        /// Defines the different types of assembly version compatibility. This feature is not available in version 1.0 of the .NET Framework.
        /// </summary>
        AssemblyVersionCompatibility VersionCompatibility { get; set; }

        // Methods

        /// <summary>
        /// Makes a copy of this IAssemblyNameWrap object.
        /// </summary>
        /// <returns>An object that is a copy of this IAssemblyNameWrap object. </returns>
        object Clone();

        /// <summary>
        /// Gets the AssemblyName for a given file.
        /// </summary>
        /// <param name="assemblyFile">The path for the assembly whose AssemblyName is to be returned.</param>
        /// <returns>An IAssemblyNameWrap object representing the given file. </returns>
        IAssemblyName GetAssemblyName(string assemblyFile);

        /// <summary>
        /// Gets serialization information with all of the data needed to recreate an instance of this AssemblyName.
        /// </summary>
        /// <param name="info">The object to be populated with serialization information. </param>
        /// <param name="context">The destination context of the serialization. </param>
        void GetObjectData(SerializationInfo info, StreamingContext context);

        /// <summary>
        /// Gets the public key of the assembly.
        /// </summary>
        /// <returns>An array of type byte containing the public key of the assembly. </returns>
        byte[] GetPublicKey();

        /// <summary>
        /// Gets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.
        /// </summary>
        /// <returns>An array of type byte containing the public key token. </returns>
        byte[] GetPublicKeyToken();

        /// <summary>
        /// Implements the ISerializable interface and is called back by the deserialization event when deserialization is complete.
        /// </summary>
        /// <param name="sender">The source of the deserialization event. </param>
        void OnDeserialization(object sender);

        /// <summary>
        /// Returns a value indicating whether the loader resolves two assembly names to the same assembly.
        /// </summary>
        /// <param name="reference">The reference assembly name. </param>
        /// <param name="definition"></param>
        /// <returns> <c>true</c> if the loader resolves definition to the same assembly as reference; otherwise, <c>false</c>. </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        bool ReferenceMatchesDefinition(IAssemblyName reference, IAssemblyName definition);

        /// <summary>
        /// Sets the public key identifying the assembly.
        /// </summary>
        /// <param name="publicKey">A byte array containing the public key of the assembly.</param>
        void SetPublicKey(byte[] publicKey);

        /// <summary>
        /// Sets the public key token, which is the last 8 bytes of the SHA-1 hash of the public key under which the application or assembly is signed.
        /// </summary>
        /// <param name="publicKeyToken">A byte array containing the public key token of the assembly. </param>
        void SetPublicKeyToken(byte[] publicKeyToken);

        /// <summary>
        /// Returns the full name of the assembly, also known as the display name.
        /// </summary>
        /// <returns>A String that is the full name of the assembly, or the class name if the full name of the assembly cannot be determined.</returns>
        string ToString();
    }
}
