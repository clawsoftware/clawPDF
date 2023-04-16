using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SystemInterface.Reflection;

using SystemWrapper.Reflection;

namespace SystemWrapper
{
    public static class SystemWrapperHelpers
    {
        public static IAssemblyName ToInterface(this AssemblyName assemblyName)
        {
            return new AssemblyNameWrap(assemblyName);
        }
    }
}
