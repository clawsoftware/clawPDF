using System.Diagnostics;
using System.Windows.Forms;
using SystemWrapper.Diagnostics;
using NUnit.Framework;
using Rhino.Mocks;

namespace SystemWrapper.Tests.Diagnostics
{
	[TestFixture]
	[Author("Brad Irby", "Brad@BradIrby.com")]
	public class ProcessStartInfoWrapTests
	{

			[SetUp]
			public void Setup()
			{
			}

			[Test]
			public void Constructor_1_Sets_ProcessStartInfoInstance()
			{
				var instance = new ProcessStartInfoWrap();
				Assert.IsNotNull(instance.ProcessStartInfoInstance);
			}

			[Test]
			public void Constructor_2_Sets_ProcessStartInfoInstance()
			{
				var instance = new ProcessStartInfoWrap("filename");
				Assert.IsNotNull(instance.ProcessStartInfoInstance);
			}

			[Test]
			public void Constructor_3_Sets_ProcessStartInfoInstance()
			{
				var instance = new ProcessStartInfoWrap("filename", "arguments");
				Assert.IsNotNull(instance.ProcessStartInfoInstance);
			}

			[Test]
			public void Constructor_4_Sets_ProcessStartInfoInstance()
			{
				var info = new ProcessStartInfo(Application.ExecutablePath);
				var instance = new ProcessStartInfoWrap(info);
				Assert.AreSame(info, instance.ProcessStartInfoInstance);
			}



			[Test]
			public void Initializer_1_Sets_Command_Instance()
			{
				var instance = new ProcessStartInfoWrap();
				var origInfo = instance.ProcessStartInfoInstance;
				instance.Initialize();
				Assert.AreNotSame(origInfo, instance.ProcessStartInfoInstance);
			}

			[Test]
			public void Initializer_2_Sets_Command_Instance()
			{
				var instance = new ProcessStartInfoWrap();
				var origInfo = instance.ProcessStartInfoInstance;
				instance.Initialize("filename");
				Assert.AreNotSame(origInfo, instance.ProcessStartInfoInstance);
				Assert.IsNotNull(instance.ProcessStartInfoInstance);
			}

			[Test]
			public void Initializer_3_Sets_Command_Instance()
			{
				var instance = new ProcessStartInfoWrap();
				var origInfo = instance.ProcessStartInfoInstance;
				instance.Initialize("filename", "arguments");
				Assert.AreNotSame(origInfo, instance.ProcessStartInfoInstance);
				Assert.IsNotNull(instance.ProcessStartInfoInstance);
			}

			[Test]
			public void Initializer_4_Sets_Command_Instance()
			{
				var instance = new ProcessStartInfoWrap();
				var origInfo = instance.ProcessStartInfoInstance;
				var info = new ProcessStartInfo(Application.ExecutablePath);
				instance.Initialize(info);
				Assert.AreNotSame(origInfo, instance.ProcessStartInfoInstance);
				Assert.AreSame(info, instance.ProcessStartInfoInstance);
			}



	}
}