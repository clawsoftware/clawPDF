using System.IO.Compression;
using SystemWrapper.IO;
using SystemWrapper.IO.Compression;
using NUnit.Framework;

namespace SystemWrapper.Tests.IO.Compression
{
    [TestFixture]
    public class DeflateStreamFactoryTests
    {
        [Test]
        public void Create_ValidParameters_ReturnsWrappedStream()
        {
            // Arrange
            var factory = new DeflateStreamFactory();
            var memoryStream = new MemoryStreamWrap();
            var compressionMode = CompressionMode.Compress;

            // Act
            var result = factory.Create(memoryStream, compressionMode);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.DeflateStreamInstance);
        }
    }
}
