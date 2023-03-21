using System.Data.SqlClient;
using SystemWrapper.Data.SqlClient;
using NUnit.Framework;
using Rhino.Mocks;

namespace SystemWrapper.Tests.IO
{
    [TestFixture]
    [Author("Brad Irby", "Brad@BradIrby.com")]
    public class SqlConnectionWrapTests
    {
    	private MockRepository _mockRepository;

			[SetUp]
			public void Setup()
			{
				_mockRepository = new MockRepository();
			}

			[Test]
			public void Constructor_1_Sets_SqlConnectionInstance()
			{
				var instance = new SqlConnectionWrap();
				Assert.IsNotNull(instance.SqlConnectionInstance);
			}

			[Test]
			public void Constructor_2_Sets_SqlConnectionInstance()
			{
				var newConn = new SqlConnection();
				var instance = new SqlConnectionWrap(newConn);
				Assert.AreSame(newConn, instance.SqlConnectionInstance);
			}

			[Test]
			public void Constructor_3_Sets_SqlConnectionInstance()
			{
				var instance = new SqlConnectionWrap("Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI;");
				Assert.IsNotNull(instance.SqlConnectionInstance);
			}


			[Test]
			public void Initialize_1_Sets_SqlConnectionInstance()
			{
				var instance = new SqlConnectionWrap();
				instance.Initialize();
				Assert.IsNotNull(instance.SqlConnectionInstance);
			}

			[Test]
			public void Initialize_2_Sets_SqlConnectionInstance()
			{
				var instance = new SqlConnectionWrap();
				var newCmd = new SqlConnection();
				instance.Initialize(newCmd);
				Assert.AreSame(newCmd, instance.SqlConnectionInstance);
			}

			[Test]
			public void Initialize_3_Sets_SqlConnectionInstance()
			{
				var instance = new SqlConnectionWrap();
				instance.Initialize("Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI;");
				Assert.IsNotNull(instance.SqlConnectionInstance);
			}
    }
}