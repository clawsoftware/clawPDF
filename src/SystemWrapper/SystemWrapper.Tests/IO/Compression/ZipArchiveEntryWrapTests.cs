using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

using NUnit.Framework;

using SystemInterface.IO.Compression;
using SystemWrapper.IO.Compression;

namespace SystemWrapper.Tests.IO.Compression
{
    [TestFixture(Category = "SystemWrapper.IO.Compression")]
    [Author("Chris Bush", "cjbush77@gmail.com")]
    public class ZipArchiveEntryWrapTests
    {
        private string ArchiveDirectory;
        private string ArchiveFileName;
        private string UnarchiveDirectory;
        private FileInfo ArchiveFileInfo;
        private IZipArchive archive;
        private IZipFile zipfile;

        private readonly object monitorObject = new object();

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Monitor.Enter(monitorObject);
            var assembly = Assembly.GetAssembly(typeof(ZipArchiveWrap));
            var CurrentDirectory = Path.GetDirectoryName(assembly.Location);
            ArchiveDirectory = assembly.CodeBase.Substring(8); //remove the "file://" from the front
            ArchiveDirectory = $@"{Path.GetDirectoryName(ArchiveDirectory)}\ZipArchiveEntryWrapTestsSource";
            if (!Directory.Exists(ArchiveDirectory))
            {
                Directory.CreateDirectory(ArchiveDirectory);
            }

            File.WriteAllText($@"{ArchiveDirectory}\ZipArchiveEntryWrapTests.txt", "ZipArchiveEntryWrapTests");
            File.WriteAllText($@"{ArchiveDirectory}\AnotherFile.txt", "Another File!");

            ArchiveFileName = $@"{CurrentDirectory}\ZipArchiveEntryWrapTests.zip";
            ArchiveFileInfo = new FileInfo(ArchiveFileName);
            UnarchiveDirectory = $@"{CurrentDirectory}\ZipArchiveEntryWrapTests\";

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

        [SetUp]
        public void SetUp()
        {
            archive = zipfile.Open(ArchiveFileName, System.IO.Compression.ZipArchiveMode.Update);
        }

        [TearDown]
        public void TearDown()
        {
            archive.Dispose();
        }

        [Test]
        public void Archive_ShouldReturn_ZipArchiveWrap()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            var actual = instance.Archive;
            Assert.AreEqual(archive.Instance, actual.Instance);
        }

        [Test]
        public void CompressedLength_ShouldReturn_GreaterThanZero()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.CompressedLength > 0);
        }

        [Test]
        public void FullName_ShouldBe_ZipArchiveEntryWrapTestsTxt()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            Assert.AreEqual("ZipArchiveEntryWrapTests.txt", instance.FullName);
        }

        [Test]
        public void LastWriteTime_ShouldBe_WritableAndReadable()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            var timestamp = new DateTimeOffset(DateTime.Now);
            instance.LastWriteTime = timestamp;
            Assert.AreEqual(timestamp, instance.LastWriteTime);
        }

        [Test]
        public void Length_ShouldBe_GreaterThanZero()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            Assert.IsTrue(instance.Length > 0);
        }

        [Test]
        public void Name_ShouldBe_ZipArchiveEntryWrapTestsTxt()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            Assert.AreEqual("ZipArchiveEntryWrapTests.txt", instance.Name);
        }

        [Test]
        public void Delete_Should_DeleteEntry()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            instance.Delete();
            var actual = archive.Entries.First();
            Assert.AreNotEqual(instance.Instance, actual.Instance);
        }

        [Test]
        public void Open_ShouldOpen_Stream()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            var stream = instance.Open();
            Assert.IsNotNull(stream);
        }

        [Test]
        public void ToString_ShouldCall_InstanceToString()
        {
            var instance = archive.Entries.First();
            Assert.IsNotNull(instance);
            var actual = instance.ToString();
            var expected = instance.Instance.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}