using System.Diagnostics;
using SystemWrapper.Diagnostics;
using NUnit.Framework;

namespace SystemWrapper.Tests.Diagnostics
{
    [TestFixture]
    [Author("Brad Irby", "rhyous@yahoo.com")]
    class TraceSourceWrapTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_Sets_TraceSourceWrap()
        {
            var instance = new TraceSourceWrap("TRACE");
            Assert.IsNotNull(instance.TraceSourceInstance);
            Assert.AreEqual(instance.TraceSourceInstance.Name, instance.Name);
        }

        [Test]
        public void Constructor_Sets_TraceSourceWrap_2()
        {
            var instance = new TraceSourceWrap("TRACE", SourceLevels.All);
            Assert.IsNotNull(instance.TraceSourceInstance);
        }
    }
}
