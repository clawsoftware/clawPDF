using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterface;
using NUnit.Framework;

using SystemInterface.Diagnostics;
using SystemWrapper.Diagnostics;

namespace SystemWrapper.Tests.Diagnostics
{
    [TestFixture]
    [Author("Chris Bush", "cjbush77@gmail.com")]
    public class FileVersionInfoWrapTests
    {
        private const string TestFilename = @"TestData\BinaryReaderWrapTestData.txt";

        [Test]
        public void Instance_Property_ReturnsWrappedObjectInstance()
        {
            // Arrange 
            var filename = Path.GetFullPath(TestFilename);
            var expectedFileVersioInfo = FileVersionInfo.GetVersionInfo(filename);

            // Act
            IWrapper<FileVersionInfo> actualFileVersionInfo = new FileVersionInfoWrap(expectedFileVersioInfo);

            // Assert
            Assert.AreSame(expectedFileVersioInfo, actualFileVersionInfo.Instance);
        }

        [Test]
        public void FileVersionInfoWrap_Properties_ReturnCorrectValues()
        {
            // Arrange 
            var filename = Path.GetFullPath(TestFilename);
            var expectedFileVersioInfo = FileVersionInfo.GetVersionInfo(filename);

            // Act
            var actualFileVersionInfo = new FileVersionInfoWrap(expectedFileVersioInfo);

            // Assert
            Assert.AreEqual(expectedFileVersioInfo.Comments, actualFileVersionInfo.Comments);
            Assert.AreEqual(expectedFileVersioInfo.CompanyName, actualFileVersionInfo.CompanyName);
            Assert.AreEqual(expectedFileVersioInfo.FileBuildPart, actualFileVersionInfo.FileBuildPart);
            Assert.AreEqual(expectedFileVersioInfo.FileDescription, actualFileVersionInfo.FileDescription);
            Assert.AreEqual(expectedFileVersioInfo.FileMajorPart, actualFileVersionInfo.FileMajorPart);
            Assert.AreEqual(expectedFileVersioInfo.FileMinorPart, actualFileVersionInfo.FileMinorPart);
            Assert.AreEqual(expectedFileVersioInfo.FileName, actualFileVersionInfo.FileName);
            Assert.AreEqual(expectedFileVersioInfo.FilePrivatePart, actualFileVersionInfo.FilePrivatePart);
            Assert.AreEqual(expectedFileVersioInfo.FileVersion, actualFileVersionInfo.FileVersion);
            Assert.AreEqual(expectedFileVersioInfo.InternalName, actualFileVersionInfo.InternalName);
            Assert.AreEqual(expectedFileVersioInfo.IsDebug, actualFileVersionInfo.IsDebug);
            Assert.AreEqual(expectedFileVersioInfo.IsPatched, actualFileVersionInfo.IsPatched);
            Assert.AreEqual(expectedFileVersioInfo.IsPreRelease, actualFileVersionInfo.IsPreRelease);
            Assert.AreEqual(expectedFileVersioInfo.IsPrivateBuild, actualFileVersionInfo.IsPrivateBuild);
            Assert.AreEqual(expectedFileVersioInfo.IsSpecialBuild, actualFileVersionInfo.IsSpecialBuild);
            Assert.AreEqual(expectedFileVersioInfo.Language, actualFileVersionInfo.Language);
            Assert.AreEqual(expectedFileVersioInfo.LegalCopyright, actualFileVersionInfo.LegalCopyright);
            Assert.AreEqual(expectedFileVersioInfo.LegalTrademarks, actualFileVersionInfo.LegalTrademarks);
            Assert.AreEqual(expectedFileVersioInfo.OriginalFilename, actualFileVersionInfo.OriginalFilename);
            Assert.AreEqual(expectedFileVersioInfo.PrivateBuild, actualFileVersionInfo.PrivateBuild);
            Assert.AreEqual(expectedFileVersioInfo.ProductBuildPart, actualFileVersionInfo.ProductBuildPart);
            Assert.AreEqual(expectedFileVersioInfo.ProductMajorPart, actualFileVersionInfo.ProductMajorPart);
            Assert.AreEqual(expectedFileVersioInfo.ProductMinorPart, actualFileVersionInfo.ProductMinorPart);
            Assert.AreEqual(expectedFileVersioInfo.ProductName, actualFileVersionInfo.ProductName);
            Assert.AreEqual(expectedFileVersioInfo.ProductPrivatePart, actualFileVersionInfo.ProductPrivatePart);
            Assert.AreEqual(expectedFileVersioInfo.ProductVersion, actualFileVersionInfo.ProductVersion);
            Assert.AreEqual(expectedFileVersioInfo.SpecialBuild, actualFileVersionInfo.SpecialBuild);
            Assert.AreEqual(expectedFileVersioInfo.ToString(), actualFileVersionInfo.ToString());
        }
    }
}
