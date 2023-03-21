using System.IO;
using System.IO.Compression;
using System.Text;
using SystemWrapper.IO;
using SystemWrapper.IO.Compression;
using NUnit.Framework;
using Rhino.Mocks;
using SystemInterface.IO;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    [Author("Brad Irby", "Brad@BradIrby.com")]
    public class DeflateStreamWrapTests
    {
        private FileStream _fileStream;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(DeflateStreamWrap));
            var testFilePath = assembly.CodeBase.Substring(8);  //remove the "file://" from the front
            testFilePath = Path.GetDirectoryName(testFilePath) + "\\TestData\\DeflateStreamWrapTestData.txt";

            _fileStream = new FileStream(testFilePath, FileMode.Open);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_Sets_DeflateStreamInstance()
        {
            var mockStream = MockRepository.GenerateMock<IStream>();
            mockStream.Stub(mo => mo.StreamInstance).Return(_fileStream);

            var instance = new DeflateStreamWrap(mockStream, CompressionMode.Compress);
            Assert.IsNotNull(instance.DeflateStreamInstance);
        }

        [Test]
        public void Initialize_Sets_DeflateStreamInstance()
        {
            var mockStream = MockRepository.GenerateMock<IStream>();
            mockStream.Stub(mo => mo.StreamInstance).Return(_fileStream);
            var instance = new DeflateStreamWrap();
            instance.Initialize(mockStream, CompressionMode.Compress);
            Assert.IsNotNull(instance.DeflateStreamInstance);
        }

        [Test]
        public void Integration_Test()
        {
            var textToCompress = "Hello World";
            var originalBytes = Encoding.UTF8.GetBytes(textToCompress);
            byte[] compressedBytes;
            byte[] decompressedBytes;

            using (var memoryStream = new MemoryStreamWrap())
            {
                var compressInstance = new DeflateStreamWrap(memoryStream, CompressionMode.Compress);
                compressInstance.Write(originalBytes, 0, originalBytes.Length);
                compressInstance.Dispose();

                compressedBytes = memoryStream.ToArray();
            }

            using (var compressedMemoryStream = new MemoryStreamWrap(compressedBytes))
            { 
                using (var deflateStream = new DeflateStreamWrap(compressedMemoryStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStreamWrap())
                    {
                        deflateStream.CopyTo(resultStream);
                        decompressedBytes = resultStream.ToArray();
                    }
                }
            }

            Assert.AreEqual(textToCompress, Encoding.UTF8.GetString(decompressedBytes));
        }

    }
}