using System;
using System.IO;
using System.Reflection;

using SystemWrapper.IO;
using SystemWrapper.Reflection;
using NUnit.Framework;
using SystemInterface.IO;
using SystemInterface.Reflection;

namespace SystemWrapper.Tests.Reflection
{
    [TestFixture]
    public class AssemblyWrapTests
    {
        [Test]
        public void GetFiles_Test()
        {
            IAssembly sampleAssembly = new AssemblyWrap();
            sampleAssembly = sampleAssembly.GetAssembly(new Int32().GetType());
            IFileStream[] fileStreams = sampleAssembly.GetFiles();
            Assert.AreEqual(1, fileStreams.Length);
            StringAssert.EndsWith("mscorlib.dll", fileStreams[0].Name);
        }

        [Test]
        public void Load_AssemblyString_ReturnsWrapperAssemblyObject()
        {
            // Arrange
            var expectedAssemblyFullName = "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            var assembly = new AssemblyWrap();

            // Act
            var actualAssembly = assembly.Load(expectedAssemblyFullName);

            // Assert
            Assert.IsNotNull(actualAssembly);
            Assert.AreEqual(expectedAssemblyFullName, actualAssembly.FullName);
        }

        [Test]
        public void Load_NonExistingAssemblyString_ThrowsFileNotFoundException()
        {
            // Arrange
            var expectedAssemblyFullName = "FakeAssemblyName, Version=0.0.1.0, Culture=neutral";
            var assembly = new AssemblyWrap();

            // Act & Assert
            var actualException = Assert.Throws<FileNotFoundException>(() => assembly.Load(expectedAssemblyFullName));
            StringAssert.StartsWith("Could not load file or assembly 'FakeAssemblyName, Version=0.0.1.0, Culture=neutral' or one of its dependencies.", actualException.Message);
        }

        [Test]
        public void AssemblyInstance_AssemblyWrapCreatedWithDefaultConstructor_ThrowsException()
        {
            // Arrange
            var assembly = new AssemblyWrap();

            // Act & Assert
            var actualException = Assert.Throws<InvalidOperationException>(() => { var var1 = assembly.AssemblyInstance; });

            Assert.AreEqual(actualException.Message, "AssemblyWrap instance was not initialized with Assembly object. Use Initialize() method to set Assembly object.");
        }

        [Test]
        public void GetTypes_AssemblyWrap_ReturnsSameTypesAsOriginalAssembly()
        {
            // Arrange
            var assembly = Assembly.GetExecutingAssembly();
            var expectedTypes = assembly.GetTypes();
            var wrapper = new AssemblyWrap(assembly);

            // Act
            var actualTypes = wrapper.GetTypes();

            // Assert
            CollectionAssert.AreEquivalent(expectedTypes, actualTypes, $"{nameof(AssemblyWrap)} must return the same types as original Assembly instance.");
        }
    }
}
