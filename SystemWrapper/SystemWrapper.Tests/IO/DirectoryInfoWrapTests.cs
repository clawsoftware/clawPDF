using SystemWrapper.IO;
using NUnit.Framework;
using SystemInterface.IO;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    [Author("Vadim Kreynin", "Vadim@kreynin.com")]
    public class DirectoryInfoWrapTests
    {
        [Test]
        public void Create_two_directories_and_then_delete_them()
        {
            string path = new DirectoryWrap().GetCurrentDirectory();
            IDirectoryInfo directoryInfoWrap = new DirectoryInfoWrap(path);
            IDirectoryInfo[] directoriesBefore = directoryInfoWrap.GetDirectories();

            directoryInfoWrap.CreateSubdirectory("Dir1");
            directoryInfoWrap.CreateSubdirectory("Dir2");
            IDirectoryInfo[] directoriesAfterCreate = directoryInfoWrap.GetDirectories();

            Assert.AreEqual("Dir1", directoriesAfterCreate[0].Name);
            Assert.AreEqual("Dir2", directoriesAfterCreate[1].Name);
            directoriesAfterCreate[0].Delete();
            directoriesAfterCreate[1].Delete();

            var directoriesAfterDelete = directoryInfoWrap.GetDirectories();
            Assert.AreEqual(directoriesBefore.Length, directoriesAfterDelete.Length);
        }

        [Test]
        public void GetFiles_must_have_files_in_Debug_folder()
        {
            IDirectoryInfo directoryWrap = new DirectoryInfoWrap(new DirectoryWrap().GetCurrentDirectory());
            IFileInfo[] fileInfoWraps = directoryWrap.GetFiles();
            Assert.IsTrue(fileInfoWraps.Length > 0);
        }

    }
}