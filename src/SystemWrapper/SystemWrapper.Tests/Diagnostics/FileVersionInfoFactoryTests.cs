using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using SystemInterface.Diagnostics;
using SystemWrapper.Diagnostics;

namespace SystemWrapper.Tests.Diagnostics
{
    [TestFixture]
    public class FileVersionInfoFactoryTests
    {
        const string TestFilename = @"TestData\BinaryReaderWrapTestData.txt";

        [Test]
        public void GetVersionInfo_DefaultState_ReturnsWrappedFileVersionInfo()
        {
            // Arrange
            IFileVersionInfoFactory factory = new FileVersionInfoFactory();

            // Act
            var actualFileVersionInfo = factory.GetVersionInfo(TestFilename);

            // Assert
            Assert.IsNotNull(actualFileVersionInfo);
            Assert.IsInstanceOf<FileVersionInfoWrap>(actualFileVersionInfo);
        }
    }
}
