using System;
using System.Security.AccessControl;
using SystemWrapper.IO;
using SystemWrapper.Security.AccessControl;
using NUnit.Framework;
using SystemInterface.IO;
using SystemInterface.Security.AccessControl;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    [Author("Vadim Kreynin", "Vadim@kreynin.com")]
    public class DirectoryWrapTests
    {
        const string path = "TempTest";

        private IDirectory _directoryWrap;
        private IDirectoryInfo _directoryInfoWrap;

        [SetUp]
        public void StartTest()
        {
            _directoryWrap = new DirectoryWrap();
            _directoryInfoWrap = _directoryWrap.CreateDirectory(path);
            Assert.IsTrue(_directoryInfoWrap.Exists, "Directory TempTest must be created.");
        }

        [TearDown]
        public void FinishTest()
        {
            if (_directoryWrap.Exists(path))
                _directoryWrap.Delete(path, true);
        }

        [Test]
        public void CreateDirectory_and_Delete_directory_Test()
        {
            _directoryWrap.Delete(path);
            Assert.IsFalse(_directoryWrap.Exists(path), "Directory TempTest must be removed.");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException_is_thrown_if_directorySecurity_is_null()
        {
            _directoryWrap.CreateDirectory(path, null);
        }

        [Test]
        public void Delete_with_subfolders()
        {
            _directoryWrap.Delete(path, true);
            Assert.IsFalse(_directoryWrap.Exists(path), @"Directory TempTest\Sub1 must be removed.");
        }

        [Test]
        public void GetAccessControl_test()
        {
            IDirectorySecurity directorySecurityWrap = _directoryWrap.GetAccessControl(path);
            Assert.IsNotNull(directorySecurityWrap.DirectorySecurityInstance);
        }

        [Test]
        public void GetParent_test()
        {
#if DEBUG
            var expectedName = "Debug";
#else
            var expectedName = "Release";
#endif

            IDirectoryInfo di = _directoryWrap.GetParent(path);
            Assert.AreEqual(expectedName, di.Name);
        }
    }
}