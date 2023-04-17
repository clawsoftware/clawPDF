using System.IO.Compression;
using System.Text;
using SystemWrapper.IO;
using SystemWrapper.IO.Compression;
using NUnit.Framework;

namespace SystemWrapper.Tests.IO.Compression
{
    [TestFixture]
    public class ZipArchiveFactoryTests
    {
        [Test]
        public void Create_WithStreamAndMode_ReturnsWrappedZipArchive()
        {
            // Arrange
            var factory = new ZipArchiveFactory();
            var memoryStream = new MemoryStreamWrap();
            var mode = ZipArchiveMode.Create;

            // Act
            var result = factory.Create(memoryStream, mode);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Instance);
            Assert.AreEqual(ZipArchiveMode.Create, result.Mode);
        }

        [Test]
        public void Create_WithStreamModeAndLeaveOpen_ReturnsWrappedZipArchive()
        {
            // Arrange
            var factory = new ZipArchiveFactory();
            var memoryStream = new MemoryStreamWrap();
            var mode = ZipArchiveMode.Create;

            // Act
            var result = factory.Create(memoryStream, mode, true);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Instance);
            Assert.AreEqual(ZipArchiveMode.Create, result.Mode);
        }

        [Test]
        public void Create_WithStreamModeAndEncoding_ReturnsWrappedZipArchive()
        {
            // Arrange
            var factory = new ZipArchiveFactory();
            var memoryStream = new MemoryStreamWrap();
            var mode = ZipArchiveMode.Create;

            // Act
            var result = factory.Create(memoryStream, mode, false, Encoding.UTF8);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Instance);
            Assert.AreEqual(ZipArchiveMode.Create, result.Mode);
        }
    }
}
