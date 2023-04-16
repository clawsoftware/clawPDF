using System.IO;
using System.Text;
using SystemWrapper.IO;
using NUnit.Framework;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    public class StreamWriterFactoryTests
    {
        [Test]
        public void WhenCreate_WithStream_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var memoryStream = new MemoryStreamWrap();

            // Act
            var result = factory.Create(memoryStream);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.StreamWriterInstance);
            Assert.AreEqual(memoryStream.StreamInstance, result.BaseStream);
        }

        [Test]
        public void WhenCreate_WithStreamAndEncoding_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var memoryStream = new MemoryStreamWrap();

            // Act
            var result = factory.Create(memoryStream, Encoding.UTF8);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.StreamWriterInstance);
            Assert.AreEqual(memoryStream.StreamInstance, result.BaseStream);
        }

        [Test]
        public void WhenCreate_WithStreamEncodingAndBufferSize_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var memoryStream = new MemoryStreamWrap();

            // Act
            var result = factory.Create(memoryStream, Encoding.UTF8, 123);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.StreamWriterInstance);
            Assert.AreEqual(memoryStream.StreamInstance, result.BaseStream);
        }

        [Test]
        public void WhenCreate_WithStreamWriter_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var memoryStream = new StreamWriterWrap(new MemoryStream());

            // Act
            var result = factory.Create(memoryStream);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(memoryStream.StreamWriterInstance, result.StreamWriterInstance);
        }

        [Test]
        public void WhenCreate_WithPath_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var path = Path.GetTempFileName();

            // Act
            var result = factory.Create(path);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.StreamWriterInstance);
        }

        [Test]
        public void WhenCreate_WithPathAndAppend_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var path = Path.GetTempFileName();

            // Act
            var result = factory.Create(path, true);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.StreamWriterInstance);
        }

        [Test]
        public void WhenCreate_WithPathAppendAndEncoding_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var path = Path.GetTempFileName();

            // Act
            var result = factory.Create(path, false, Encoding.UTF32);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.StreamWriterInstance);
        }

        [Test]
        public void WhenCreate_WithPathAppendEncodingAndBufferSize_ReturnsWrappedStreamWriter()
        {
            // Arrange
            var factory = new StreamWriterFactory();
            var path = Path.GetTempFileName();

            // Act
            var result = factory.Create(path, false, Encoding.UTF8, 123);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.StreamWriterInstance);
        }
    }
}
