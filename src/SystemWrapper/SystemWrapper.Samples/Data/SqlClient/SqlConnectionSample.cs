using System.Data;
using NUnit.Framework;
using Rhino.Mocks;
using SystemInterface.Data.SqlClient;

namespace SystemWrapper.Samples.Data.SqlClient
{
    public class SqlConnectionSample
    {
        public ConnectionState OpenSqlConnection(ISqlConnection connection)
        {
            connection.Open();
            ConnectionState connectionState = connection.State;
            connection.Close();
            return connectionState;
        }
    }

    public class SqlConnectionSampleTests
    {
        [Test]
        public void OpenSqlConnection_test()
        {
            ISqlConnection connectionStub = MockRepository.GenerateStub<ISqlConnection>();
            connectionStub.Stub(x => x.State).Return(ConnectionState.Open);
            Assert.AreEqual(ConnectionState.Open, new SqlConnectionSample().OpenSqlConnection(connectionStub));
            connectionStub.AssertWasCalled(x => x.Open());
            connectionStub.AssertWasCalled(x => x.Close());
        }
    }
}