using System.IO;
using System.Reflection;
using System.Threading;

using NUnit.Framework;

using SystemWrapper.IO.Compression;

namespace SystemWrapper.Tests.IO.Compression
{
    [TestFixture(Category = "SystemWrapper.IO.Compression")]
    [Author("Chris Bush", "cjbush77@gmail.com")]
    public class ZipArchiveWrapTests
    {
        private string ArchiveDirectory;
        private string ArchiveFileName;
        private string UnarchiveDirectory;
        private FileInfo ArchiveFileInfo;
        private ZipFileWrap zipfile;

        private readonly object monitorObject = new object();

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Monitor.Enter(monitorObject);
            var assembly = Assembly.GetAssembly(typeof(ZipArchiveWrap));
            var CurrentDirectory = Path.GetDirectoryName(assembly.Location);
            ArchiveDirectory = assembly.CodeBase.Substring(8); //remove the "file://" from the front
            ArchiveFileName = $@"{CurrentDirectory}\ZipArchiveWrapTests.zip";
            ArchiveFileInfo = new FileInfo(ArchiveFileName);
            UnarchiveDirectory = $@"{CurrentDirectory}\ZipArchiveWrapTests\";

            ArchiveDirectory = $@"{Path.GetDirectoryName(ArchiveDirectory)}\ZipArchiveWrapTestsSource";
            if (!Directory.Exists(ArchiveDirectory))
            {
                Directory.CreateDirectory(ArchiveDirectory);
            }

            File.WriteAllText($@"{ArchiveDirectory}\ZipArchiveWrapTests.txt", "ZipArchiveWrapTests");
            File.WriteAllText($@"{ArchiveDirectory}\AnotherFile.txt", "Another File!");

            if (File.Exists(ArchiveFileName))
            {
                File.Delete(ArchiveFileName);
            }

            zipfile = new ZipFileWrap();
            zipfile.CreateFromDirectory(ArchiveDirectory, ArchiveFileName);

            if (Directory.Exists(UnarchiveDirectory))
            {
                Directory.Delete(UnarchiveDirectory, true);
            }

            zipfile.ExtractToDirectory(ArchiveFileName, UnarchiveDirectory);
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

        [Test]
        public void Entries_ShouldContain_ZipArchiveEntries()
        {
            using (var instance = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Read))
            {
                Assert.IsNotNull(instance.Entries);
                CollectionAssert.IsNotEmpty(instance.Entries);
            }
        }

        [Test]
        public void Mode_Should_BeRead()
        {
            using (var instance = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Read))
            {
                Assert.AreEqual(System.IO.Compression.ZipArchiveMode.Read, instance.Mode);
            }
        }

        [Test]
        public void Mode_Should_BeCreate()
        {
            if (File.Exists(ArchiveFileName))
            {
                File.Delete(ArchiveFileName);
            }
            using (var instance = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Create))
            {
                Assert.AreEqual(System.IO.Compression.ZipArchiveMode.Create, instance.Mode);
            }
        }

        [Test]
        public void Mode_Should_BeUpdate()
        {
            using (var instance = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Update))
            {
                Assert.AreEqual(System.IO.Compression.ZipArchiveMode.Update, instance.Mode);
            }
        }

        [Test]
        public void CreateEntry_ShouldCreate_ZipArchiveEntryWrap()
        {
            using (var instance = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Update))
            {
                var entry = instance.CreateEntry("entryTest");
                Assert.IsNotNull(entry);
                Assert.IsInstanceOf<ZipArchiveEntryWrap>(entry);
            }
        }

        [Test]
        public void CreateEntry_WithCompressionLevel_ShouldCreate_ZipArchiveEntryWrap()
        {
            using (var instance = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Update))
            {
                var entry = instance.CreateEntry("entryTest", System.IO.Compression.CompressionLevel.Fastest);
                Assert.IsNotNull(entry);
                Assert.IsInstanceOf<ZipArchiveEntryWrap>(entry);
            }
        }

        [Test]
        public void GetEntry_ShouldReturn_ZipArchiveEntry()
        {
            using (var instance = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Update))
            {
                var expected = instance.CreateEntry("entryTest", System.IO.Compression.CompressionLevel.Fastest);
                var actual = instance.GetEntry("entryTest");
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Instance.Name, actual.Instance.Name);
            }
        }
    }
}