using System;
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace SystemInterface.Reflection
{
    /// <summary>
    /// This interfaces includes static methods from the <see cref="System.Reflection.Assembly"/> type.
    /// </summary>
    public interface IAssemblyFactory
    {
        IAssembly Load(byte[] rawAssembly);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly Load(IAssemblyName assemblyRef);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly Load(string assemblyString);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly Load(IAssemblyName assemblyRef, Evidence assemblySecurity);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly Load(string assemblyString, Evidence assemblySecurity);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly Load(byte[] rawAssembly, byte[] rawSymbolStore);

        [MethodImpl(MethodImplOptions.NoInlining), SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
        IAssembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);

        IAssembly LoadFile(string path);

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence)]
        IAssembly LoadFile(string path, Evidence securityEvidence);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly LoadFrom(string assemblyFile, Evidence securityEvidence);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly ReflectionOnlyLoad(string assemblyString);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly ReflectionOnlyLoad(byte[] rawAssembly);

        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly ReflectionOnlyLoadFrom(string assemb);

        /// <summary>
        /// Loads an assembly given its file name or path.
        /// </summary>
        /// <param name="assemblyFile">The name or path of the file that contains the manifest of the assembly.</param>
        /// <returns>The loaded assembly. </returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        IAssembly LoadFrom(string assemblyFile);
    }
}
