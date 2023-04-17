using System.IO;
using SystemWrapper.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    [Author("Brad Irby", "Brad@BradIrby.com")]
    public class BinaryReaderWrapTests
    {
		private FileStream _fileStream;

		[TestFixtureSetUp]
		public void FixtureSetup()
		{
			var assembly = System.Reflection.Assembly.GetAssembly(typeof(BinaryReaderWrap));
			var testFilePath = assembly.CodeBase.Substring(8);  //remove the "file://" from the front
			testFilePath = Path.GetDirectoryName(testFilePath) + "\\TestData\\BinaryReaderWrapTestData.txt";

			_fileStream = new FileStream(testFilePath, FileMode.Open);
		}


		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Constructor_1_Sets_BinaryReaderInstance()
		{
			var bReader = new BinaryReader(_fileStream);
			var instance = new BinaryReaderWrap(bReader);
			Assert.AreSame(bReader, instance.BinaryReaderInstance);
		}

		[Test]
		public void Initialize_1_Sets_BinaryReaderInstance()
		{
			var instance = new BinaryReaderWrap();
			var bReader = new BinaryReader(_fileStream);
			instance.Initialize(bReader);
			Assert.AreSame(bReader, instance.BinaryReaderInstance);
		}

		[Test]
		public void Constructor_2_Sets_BinaryReaderInstance()
		{
			var instance = new BinaryReaderWrap(_fileStream);
			Assert.IsNotNull(instance.BinaryReaderInstance);
		}

		[Test]
		public void Initialize_2_Sets_BinaryReaderInstance()
		{
			var instance = new BinaryReaderWrap();
			instance.Initialize(_fileStream);
			Assert.IsNotNull(instance.BinaryReaderInstance);
		}


    }
}