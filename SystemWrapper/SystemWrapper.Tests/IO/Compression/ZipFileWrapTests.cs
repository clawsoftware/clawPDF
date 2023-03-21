using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

using NUnit.Framework;

using SystemWrapper.IO.Compression;

namespace SystemWrapper.Tests.IO.Compression
{
    [TestFixture(Category = "SystemWrapper.IO.Compression")]
    [Author("Chris Bush", "cjbush77@gmail.com")]
    public class ZipFileWrapTests
    {
        private string ArchiveDirectory;
        private string ArchiveFileName;
        private string UnarchiveDirectory;
        private FileInfo ArchiveFileInfo;

        private readonly object monitorObject = new object();

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Monitor.Enter(monitorObject);
            var assembly = Assembly.GetAssembly(typeof(ZipFileWrap));
            var CurrentDirectory = Path.GetDirectoryName(assembly.Location);
            ArchiveDirectory = assembly.CodeBase.Substring(8); //remove the "file://" from the front
            ArchiveFileName = $@"{CurrentDirectory}\test.zip";
            ArchiveFileInfo = new FileInfo(ArchiveFileName);
            UnarchiveDirectory = $@"{CurrentDirectory}\ZipFileWrapTests\";

            ArchiveDirectory = $@"{Path.GetDirectoryName(ArchiveDirectory)}\ZipFileWrapTestsSource";
            if (!Directory.Exists(ArchiveDirectory))
            {
                Directory.CreateDirectory(ArchiveDirectory);
            }

            File.WriteAllText($@"{ArchiveDirectory}\ZipFileWrapTests.txt", "ZipFileWrapTests");
            File.WriteAllText($@"{ArchiveDirectory}\AnotherFile.txt", "Another File!");
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            if (File.Exists(ArchiveFileName))
            {
                File.Delete(ArchiveFileName);
            }
            if (Directory.Exists(UnarchiveDirectory))
            {
                Directory.Delete(UnarchiveDirectory, true);
            }
            if (Directory.Exists(ArchiveDirectory))
            {
                Directory.Delete(ArchiveDirectory, true);
            }
            Monitor.Exit(monitorObject);
        }

        [SetUp]
        public void SetUp()
        {
            if (File.Exists(ArchiveFileName))
            {
                ArchiveFileInfo.Delete();
            }
            Assert.IsFalse(File.Exists(ArchiveFileName));

            if (Directory.Exists(UnarchiveDirectory))
            {
                Directory.Delete(UnarchiveDirectory, true);
            }
            Assert.IsFalse(Directory.Exists(UnarchiveDirectory));
        }

        [Test]
        public void CreateFromDirectory_Creates_Archive()
        {
            var instance = new ZipFileWrap();
            instance.CreateFromDirectory(ArchiveDirectory, ArchiveFileName);
            Assert.IsTrue(File.Exists(ArchiveFileName));
            Assert.IsTrue(ArchiveFileInfo.Length > 0);
        }

        [Test]
        public void CreateFromDirectory_WithCompressionLevel_Creates_Archive()
        {
            var instance = new ZipFileWrap();
            instance.CreateFromDirectory(ArchiveDirectory, ArchiveFileName, System.IO.Compression.CompressionLevel.Fastest, true);
            Assert.IsTrue(File.Exists(ArchiveFileName));
            Assert.IsTrue(ArchiveFileInfo.Length > 0);
        }

        [Test]
        public void CreateFromDirectory_WithCompressionLevelAndEncoding_Creates_Archive()
        {
            var instance = new ZipFileWrap();
            instance.CreateFromDirectory(ArchiveDirectory, ArchiveFileName, System.IO.Compression.CompressionLevel.Fastest, true, Encoding.UTF8);
            Assert.IsTrue(File.Exists(ArchiveFileName));
            Assert.IsTrue(ArchiveFileInfo.Length > 0);
        }

        [Test]
        public void ExtractToDirectory_Creates_Directory()
        {
            var instance = new ZipFileWrap();
            instance.CreateFromDirectory(ArchiveDirectory, ArchiveFileName);
            instance.ExtractToDirectory(ArchiveFileName, UnarchiveDirectory);
            Assert.IsTrue(Directory.Exists(UnarchiveDirectory));
            Assert.IsTrue(Directory.GetFiles(UnarchiveDirectory).Length > 0);
        }

        [Test]
        public void ExtractToDirectory_WithEncoding_CreatesDirectory()
        {
            var instance = new ZipFileWrap();
            instance.CreateFromDirectory(ArchiveDirectory, ArchiveFileName);
            instance.ExtractToDirectory(ArchiveFileName, UnarchiveDirectory, Encoding.UTF8);
            Assert.IsTrue(Directory.Exists(UnarchiveDirectory));
            Assert.IsTrue(Directory.GetFiles(UnarchiveDirectory).Length > 0);
        }

        [Test]
        public void Open_Creates_ZipArchiveWrap()
        {
            var instance = new ZipFileWrap();
            instance.CreateFromDirectory(ArchiveDirectory, ArchiveFileName);
            using (var archive = instance.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Read))
            {
                Assert.IsNotNull(archive);
                Assert.IsInstanceOf<ZipArchiveWrap>(archive);
            }
        }

        [Test]
        public void Open_WithUTF8Encoding_Creates_OpensZipWithCorrectEncoding()
        {
            var assembly = Assembly.GetAssembly(typeof(ZipFileWrapTests));
            var testFilePath = assembly.CodeBase.Substring(8);  //remove the "file://" from the front
            testFilePath = Path.GetDirectoryName(testFilePath) + @"\TestData\Encoding_UTF8.zip";

            var instance = new ZipFileWrap();
            using (var archive = instance.Open(testFilePath, System.IO.Compression.ZipArchiveMode.Read, Encoding.UTF8))
            {
                Assert.IsNotNull(archive);
                Assert.IsInstanceOf<ZipArchiveWrap>(archive);

                var entry = archive.Entries.First();
                Assert.AreEqual("text-ľščťžýáíé.txt", entry.Name);
            }
        }

        [Test]
        public void Open_WithWindows1250Encoding_OpensZipWithCorrectEncoding()
        {
            var assembly = Assembly.GetAssembly(typeof(ZipFileWrapTests));
            var testFilePath = assembly.CodeBase.Substring(8);  //remove the "file://" from the front
            testFilePath = Path.GetDirectoryName(testFilePath) + @"\TestData\Encoding_Windows1250.zip";

            var instance = new ZipFileWrap();

            var encodingWindows1250 = Encoding.GetEncoding(1250);
            using (var archive = instance.Open(testFilePath, System.IO.Compression.ZipArchiveMode.Read, encodingWindows1250))
            {
                Assert.IsNotNull(archive);
                Assert.IsInstanceOf<ZipArchiveWrap>(archive);

                var entry = archive.Entries.First();
                Assert.AreEqual("text-ľščťžýáíé.txt", entry.Name);
            }
        }

        [Test]
        public void OpenRead_Creates_ZipArchiveWrap()
        {
            var instance = new ZipFileWrap();
            instance.CreateFromDirectory(ArchiveDirectory, ArchiveFileName);
            using (var archive = instance.OpenRead(ArchiveFileName))
            {
                Assert.IsNotNull(archive);
                Assert.IsInstanceOf<ZipArchiveWrap>(archive);
            }
        }
    }
}
