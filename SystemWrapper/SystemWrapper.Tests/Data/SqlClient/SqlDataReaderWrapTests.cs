using System.Data;
using System.Data.SqlClient;
using SystemWrapper.Data.SqlClient;
using NUnit.Framework;
using Rhino.Mocks;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    [Author("Brad Irby", "Brad@BradIrby.com")]
	public class SqlDataReaderWrapTests
    {
    	private MockRepository _mockRepository;

			[SetUp]
			public void Setup()
			{
				_mockRepository = new MockRepository();
			}

			//there is no default constructor for a SqlDataReader so we cannot mock it.
			//Can't figure out how to test this.

			//[Test]
			//public void Constructor_Sets_Connection_Instance()
			//{
			//  var mockReader = _mockRepository.Stub<SqlDataReader>();
			//  var instance = new SqlDataReaderWrap(mockReader);
			//  Assert.AreSame(mockReader, instance.SqlDataReaderInstance);
			//}


			//[Test]
			//public void Initialize_Sets_Command_Instance()
			//{
			//  var mockReader = _mockRepository.Stub<SqlDataReader>();
			//  var instance = new SqlDataReaderWrap();
			//  instance.Initialize(mockReader);
			//  Assert.AreSame(mockReader, instance.SqlDataReaderInstance);
			//}

    }
}