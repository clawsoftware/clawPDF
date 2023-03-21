using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SystemWrapper.Tests
{
    using SystemInterface;

    [TestFixture]
    public class EnvironmentWrapTests
    {
        [SetUp]
        public void SetUp()
        {
            this.EnvironmentWrap = new EnvironmentWrap();
        }

        public IEnvironment EnvironmentWrap { get; set; }

        [Test]
        public void Is64BitOperatingSystem_CurrentEnvironment_ReturnsCorrectValue()
        {
            // Arrange
            var expectedValue = Environment.Is64BitOperatingSystem;

            // Act
            var actualValue = this.EnvironmentWrap.Is64BitOperatingSystem;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void Is64BitProcess_CurrentEnvironment_ReturnsCorrectValue()
        {
            // Arrange
            var expectedValue = Environment.Is64BitProcess;

            // Act
            var actualValue = this.EnvironmentWrap.Is64BitProcess;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void ExpandEnvironmentVariables_CurrentEnvironment_EscapesValuesInString()
        {
            // Arrange
            var expectedValue = Environment.GetEnvironmentVariable("SystemRoot");

            // Act
            var actualValue = this.EnvironmentWrap.ExpandEnvironmentVariables("%SystemRoot%");

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void ExpandEnvironmentVariables_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            var expectedValue = String.Empty;

            // Act
            var actualValue = this.EnvironmentWrap.ExpandEnvironmentVariables("");

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void GetFolderPath_ValidSpecialFolderValue_ReturnsCorrectFolderPath()
        {
            // Arrange
            var expectedValue = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms);

            // Act
            var actualValue = this.EnvironmentWrap.GetFolderPath(Environment.SpecialFolder.CommonPrograms);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void GetFolderPath_ValidSpecialFolderValueAndSpecialFolderOptionValue_CanBeCalled()
        {
            // Arrange
            var expectedValue = Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms, Environment.SpecialFolderOption.DoNotVerify);

            // Act
            var actualValue = this.EnvironmentWrap.GetFolderPath(Environment.SpecialFolder.CommonPrograms, Environment.SpecialFolderOption.DoNotVerify);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void SetEnvironmentVariable_EnvironmentVariableWithValue_EnvironmentVariableIsSetToValue()
        {
            // Arrange
            const string environmentVariableName = "SystemWrapperTest_SetEnv";
            const string expectedValue = "test";

            // Act
            this.EnvironmentWrap.SetEnvironmentVariable(environmentVariableName, expectedValue);

            // Assert
            var actualValue = Environment.GetEnvironmentVariable(environmentVariableName);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void GetEnvironmentVariable_ExistingEnvironmentVariable_ReturnsEnvironmentVariableValue()
        {
            // Arrange
            const string environmentVariableName = "SystemWrapperTest_GetEnv";
            const string expectedValue = "test";

            Environment.SetEnvironmentVariable(environmentVariableName, expectedValue);

            // Act
            var actualValue = this.EnvironmentWrap.GetEnvironmentVariable(environmentVariableName);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
