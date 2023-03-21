using System.Windows.Forms;
using SystemWrapper.Configuration;
using NUnit.Framework;

namespace SystemWrapper.Tests.IO
{
	[TestFixture]
	[Author("Brad Irby", "Brad@BradIrby.com")]
	public class ConfigurationManagerWrapTests
	{
		private ConfigurationManagerWrap _mgr;

		[SetUp]
		public void Setup()
		{
			_mgr = new ConfigurationManagerWrap();
		}

		[Test]
		public void AppSettings_Does_Not_Return_Null()
		{
			Assert.IsNotNull(_mgr.AppSettings);
		}

		[Test]
		public void ConnectionStrings_Does_Not_Return_Null()
		{
			Assert.IsNotNull(_mgr.ConnectionStrings);
		}

		[Test]
		public void GetSection_Does_Not_Throw_Exception()
		{
			_mgr.GetSection("section does not exist");
		}

		[Test]
		public void OpenExeConfiguration_Does_Not_Throw_Exception()
		{
			_mgr.OpenExeConfiguration(Application.ExecutablePath);
		}

		[Test]
		public void OpenMachineConfiguration_Does_Not_Throw_Exception()
		{
			_mgr.OpenMachineConfiguration();
		}


		[Test]
		public void RefreshSection_Does_Not_Throw_Exception()
		{
			_mgr.RefreshSection("section does not exist");
		}
	}
}