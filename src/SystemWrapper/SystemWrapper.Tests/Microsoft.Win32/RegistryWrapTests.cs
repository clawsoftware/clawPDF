using NUnit.Framework;
using Microsoft.Win32;
using SystemWrapper.Microsoft.Win32;

namespace SystemWrapper.Tests.Microsoft.Win32
{
    [TestFixture]
    [Author("Jared Barneck", "rhyous@yahoo.com")]
    public class RegistryWrapTests
    {
        [Test]
        public void Registry_ClassesRoot_Test()
        {
            var key = Registry.ClassesRoot;
            var keyWrap = new RegistryWrap().ClassesRoot.RegistryKeyInstance;
            Assert.AreSame(key, keyWrap);
        }

        [Test]
        public void Registry_CurrentConfig_Test()
        {
            var key = Registry.CurrentConfig;
            var keyWrap = new RegistryWrap().CurrentConfig.RegistryKeyInstance;
            Assert.AreSame(key, keyWrap);
        }

        [Test]
        public void Registry_CurrentUser_Test()
        {
            var key = Registry.CurrentUser;
            var keyWrap = new RegistryWrap().CurrentUser.RegistryKeyInstance;
            Assert.AreSame(key, keyWrap);
        }

        [Test]
        public void Registry_LocalMachine_Test()
        {
            var key = Registry.LocalMachine;
            var keyWrap = new RegistryWrap().LocalMachine.RegistryKeyInstance;
            Assert.AreSame(key, keyWrap);
        }

        [Test]
        public void Registry_PerformanceData_Test()
        {
            var key = Registry.PerformanceData;
            var keyWrap = new RegistryWrap().PerformanceData.RegistryKeyInstance;
            Assert.AreSame(key, keyWrap);
        }

        [Test]
        public void Registry_Users_Test()
        {
            var key = Registry.Users;
            var keyWrap = new RegistryWrap().Users.RegistryKeyInstance;
            Assert.AreSame(key, keyWrap);
        }
    }
}