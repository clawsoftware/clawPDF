using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SystemInterface.Reflection;

using NUnit.Framework;

namespace SystemWrapper.Tests.Reflection
{
    [TestFixture]
    public class AssemblyNameWrapTests
    {

        [Test]
        public void ToInterface_AssemblyNameExtensionMethod_ReturnWrappedObject()
        {
            // Arrange
            var assemblyName = new AssemblyName();

            // Act
            IAssemblyName actualAssemblyNameWrap = assemblyName.ToInterface();

            // Assert
            Assert.IsNotNull(actualAssemblyNameWrap);
            Assert.AreSame(actualAssemblyNameWrap.AssemblyNameInstance, assemblyName);
        }
    }
}
