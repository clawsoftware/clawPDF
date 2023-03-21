using System;
using System.Configuration.Assemblies;
using System.Reflection;
using System.Security.Policy;
using SystemInterface.Reflection;

namespace SystemWrapper.Reflection
{
    /// <summary>
    /// Wrapper implementation for static methods from the <see cref="System.Reflection.Assembly"/> type.
    /// </summary>
    public class AssemblyFactory : IAssemblyFactory
    {
        public IAssembly Load(string assemblyString)
        {
            return new AssemblyWrap(Assembly.Load(assemblyString));
        }

        public IAssembly Load(IAssemblyName assemblyRef)
        {
            return new AssemblyWrap(Assembly.Load(assemblyRef.AssemblyNameInstance));
        }

        public IAssembly Load(byte[] rawAssembly)
        {
            return new AssemblyWrap(Assembly.Load(rawAssembly));
        }

        public IAssembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
        {
            return new AssemblyWrap(Assembly.Load(rawAssembly, rawSymbolStore));
        }

        public IAssembly Load(string assemblyString, Evidence assemblySecurity)
        {
            return new AssemblyWrap(Assembly.Load(assemblyString, assemblySecurity));
        }

        public IAssembly Load(IAssemblyName assemblyRef, Evidence assemblySecurity)
        {
            return new AssemblyWrap(Assembly.Load(assemblyRef.AssemblyNameInstance, assemblySecurity));
        }

        public IAssembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
        {
            return new AssemblyWrap(Assembly.Load(rawAssembly, rawSymbolStore, securityEvidence));
        }

        public IAssembly LoadFile(string path)
        {
            return new AssemblyWrap(Assembly.LoadFile(path));
        }

        public IAssembly LoadFile(string path, Evidence securityEvidence)
        {
            return new AssemblyWrap(Assembly.LoadFile(path, securityEvidence));
        }

        public IAssembly LoadFrom(string assemblyFile)
        {
            return new AssemblyWrap(Assembly.LoadFrom(assemblyFile));
        }

        public IAssembly LoadFrom(string assemblyFile, Evidence securityEvidence)
        {
            return new AssemblyWrap(Assembly.LoadFrom(assemblyFile, securityEvidence));
        }

        public IAssembly LoadFrom(string assemblyFile, Evidence securityEvidence, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
        {
            return new AssemblyWrap(Assembly.LoadFrom(assemblyFile, securityEvidence, hashValue, hashAlgorithm));
        }

        public IAssembly ReflectionOnlyLoad(byte[] rawAssembly)
        {
            return new AssemblyWrap(Assembly.ReflectionOnlyLoad(rawAssembly));
        }

        public IAssembly ReflectionOnlyLoad(string assemblyString)
        {
            return new AssemblyWrap(Assembly.ReflectionOnlyLoad(assemblyString));
        }

        public IAssembly ReflectionOnlyLoadFrom(string assemb)
        {
            return new AssemblyWrap(Assembly.ReflectionOnlyLoadFrom(assemb));
        }
    }
}
