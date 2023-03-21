using System;
using System.Data.SqlClient;
using SystemWrapper.Data.SqlClient;
using NUnit.Framework;

namespace SystemWrapper.Tests.Data.SqlClient
{
    [TestFixture]
    public class SqlConnectionFactoryTests
    {
        [Test]
        public void Create_NoArgument_CreatesDefaultWrapper()
        {
            // Arrange
            var factory = new SqlConnectionFactory();

            // Act
            var actualSqlConnection = factory.Create();

            // Assert
            Assert.IsNotNull(actualSqlConnection);
            Assert.IsNotNull(actualSqlConnection.SqlConnectionInstance);
        }

        [Test]
        public void Create_ExistingSqlConnectionObject_WrapsTheObject()
        {
            // Arrange
            var expectedSqlConnection = new SqlConnection();
            var factory = new SqlConnectionFactory();

            // Act
            var actualSqlConnection = factory.Create(expectedSqlConnection);

            // Assert
            Assert.IsNotNull(actualSqlConnection);
            Assert.AreSame(expectedSqlConnection, actualSqlConnection.SqlConnectionInstance);
        }

        [Test]
        public void Create_ConnectionStringArgument_CreatesSqlConnection()
        {
            // Arrange
            var expectedConnectionString = "Server=localhost;Database=db1;Trusted_Connection=True";
            var factory = new SqlConnectionFactory();

            // Act
            var actualSqlConnection = factory.Create(expectedConnectionString);

            // Assert
            Assert.IsNotNull(actualSqlConnection);
            Assert.AreEqual(expectedConnectionString, actualSqlConnection.ConnectionString);
        }
    }
}
