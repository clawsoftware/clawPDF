using System.IO;
using SystemWrapper.IO;
using NUnit.Framework;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    public class StreamWriterWrapTests
    {
        [Test]
        public void Dispose_Test()
        {
            var tmpFile = Path.GetTempFileName();

            var writer = new StreamWriterWrap(tmpFile);

            const string expectedContent = "Hello World.";
            writer.Write(expectedContent);
            writer.Dispose();

            var actualContent = File.ReadAllText(tmpFile);

            Assert.AreEqual(expectedContent, actualContent);
        }
    }
}