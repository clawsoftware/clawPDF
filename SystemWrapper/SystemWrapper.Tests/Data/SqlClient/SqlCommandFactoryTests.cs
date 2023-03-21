using System;
using System.Data.SqlClient;
using SystemWrapper.Data.SqlClient;
using NUnit.Framework;

namespace SystemWrapper.Tests.Data.SqlClient
{
    [TestFixture]
    public class SqlCommandFactoryTests
    {
        [Test]
        public void Create_NoArgument_CreatesDefaultWrapper()
        {
            // Arrange
            var factory = new SqlCommandFactory();

            // Act
            var actualSqlComand = factory.Create();

            // Assert
            Assert.IsNotNull(actualSqlComand);
            Assert.IsNotNull(actualSqlComand.SqlCommandInstance);
        }

        [Test]
        public void Create_ExistingSqlCommandObject_WrapsTheObject()
        {
            // Arrange
            var expectedSqlCommand = new SqlCommand();
            var factory = new SqlCommandFactory();

            // Act
            var actualSqlComand = factory.Create(expectedSqlCommand);

            // Assert
            Assert.IsNotNull(actualSqlComand);
            Assert.AreSame(expectedSqlCommand, actualSqlComand.SqlCommandInstance);
        }

        [Test]
        public void Create_SqlTextArgument_CreatesSqlCommandWithGivenSqlText()
        {
            // Arrange
            var expectedSqlCommand = "SELECT * FROM dbo.table1";
            var factory = new SqlCommandFactory();

            // Act
            var actualSqlComand = factory.Create(expectedSqlCommand);

            // Assert
            Assert.IsNotNull(actualSqlComand);
            Assert.AreEqual(expectedSqlCommand, actualSqlComand.SqlCommandInstance.CommandText);
        }

        [Test]
        public void Create_SqlTextAndSqlConnectionArgument_CreatesSqlCommandWithGivenArguments()
        {
            // Arrange
            var expectedSqlCommand = "SELECT * FROM dbo.table1";
            var expectedSqlConnection = new SqlConnectionWrap();
            var factory = new SqlCommandFactory();

            // Act
            var actualSqlComand = factory.Create(expectedSqlCommand, expectedSqlConnection);

            // Assert
            Assert.IsNotNull(actualSqlComand);
            Assert.AreEqual(expectedSqlCommand, actualSqlComand.SqlCommandInstance.CommandText);
            Assert.AreSame(expectedSqlConnection.SqlConnectionInstance, actualSqlComand.SqlCommandInstance.Connection);
        }
    }
}
