using NUnit.Framework;
using Rhino.Mocks;
using SystemInterface.IO;

namespace SystemWrapper.Samples.IO
{
    public class DirectoryInfoSample
    {
        public bool TryToCreateDirectory(IDirectoryInfo directory)
        {
            if (directory.Exists)
                return false;

            directory.Create();
            return true;
        }
    }

    public class DirectoryInfoSampleTests
    {
        [Test]
        public void When_try_to_create_directory_that_already_exists_return_false()
        {
            var directoryInfoWrap = MockRepository.GenerateStub<IDirectoryInfo>();
            directoryInfoWrap.Stub(x => x.Exists).Return(true);
            // directoryInfoWrap.Expect(x => x.Create()).Repeat.Never();
            Assert.AreEqual(false, new DirectoryInfoSample().TryToCreateDirectory(directoryInfoWrap));

            directoryInfoWrap.AssertWasNotCalled(x => x.Create());
            // directoryInfoWrap.VerifyAllExpectations();
        }

        [Test]
        public void When_try_to_create_directory_that_does_not_exist_return_true()
        {
            var directoryInfoWrap = MockRepository.GenerateStub<IDirectoryInfo>();
            directoryInfoWrap.Stub(x => x.Exists).Return(false);
            // directoryInfoWrap.Expect(x => x.Create());
            Assert.AreEqual(true, new DirectoryInfoSample().TryToCreateDirectory(directoryInfoWrap));

            directoryInfoWrap.AssertWasCalled(x => x.Create());
            // directoryInfoWrap.VerifyAllExpectations();
        }
    }
}