using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace SystemInterface.Data.SqlClient
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Data.SqlClient.SqlConnection"/> class.
    /// </summary>
    public interface ISqlConnection
    {
        /// <summary>
        /// Initializes a new instance of the SqlConnectionWrap class.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the SqlConnectionWrap class.
        /// </summary>
        /// <param name="connection">SqlConnection object.</param>
        void Initialize(SqlConnection connection);

        /// <summary>
        /// Initializes a new instance of the SqlConnection class when given a string that contains the connection string.
        /// </summary>
        /// <param name="connectionString">The connection used to open the SQL Server database.</param>
        void Initialize(string connectionString);

        // Properties

        /// <summary>
        /// Gets or sets the string used to open a SQL Server database.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.
        /// </summary>
        int ConnectionTimeout { get; }

        /// <summary>
        /// Gets the name of the current database or the database to be used after a connection is opened.
        /// </summary>
        string Database { get; }

        /// <summary>
        /// Gets the name of the instance of SQL Server to which to connect.
        /// </summary>
        [Browsable(true)]
        string DataSource { get; }

        /// <summary>
        /// Gets or sets the FireInfoMessageEventOnUserErrors property.
        /// </summary>
        bool FireInfoMessageEventOnUserErrors { get; set; }

        /// <summary>
        /// Gets the size (in bytes) of network packets used to communicate with an instance of SQL Server.
        /// </summary>
        int PacketSize { get; }

        /// <summary>
        /// Gets a string that contains the version of the instance of SQL Server to which the client is connected.
        /// </summary>
        [Browsable(false)]
        string ServerVersion { get; }

        /// <summary>
        /// Gets <see cref="T:System.Data.SqlClient.SqlConnection"/> object.
        /// </summary>
        SqlConnection SqlConnectionInstance { get; }

        /// <summary>
        /// Indicates the state of the SqlConnection.
        /// </summary>
        [Browsable(false)]
        ConnectionState State { get; }

        /// <summary>
        /// When set to true, enables statistics gathering for the current connection.
        /// </summary>
        bool StatisticsEnabled { get; set; }

        /// <summary>
        /// Gets a string that identifies the database client.
        /// </summary>
        string WorkstationId { get; }

        // Methods

        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing any open connection.
        /// </summary>
        void Close();

        /// <summary>
        /// Opens a database connection with the property settings specified by the ConnectionString.
        /// </summary>
        void Open();

        /*
         *

             // Events
            [ResCategory("DataCategory_InfoMessage"), ResDescription("DbConnection_InfoMessage")]
            public event SqlInfoMessageEventHandler InfoMessage;

            // Methods
            public SqlTransaction BeginTransaction();
            public SqlTransaction BeginTransaction(IsolationLevel iso);
            public SqlTransaction BeginTransaction(string transactionName);
            public SqlTransaction BeginTransaction(IsolationLevel iso, string transactionName);
            public override void ChangeDatabase(string database);
            public static void ChangePassword(string connectionString, string newPassword);
            public static void ClearAllPools();
            public static void ClearPool(SqlConnection connection);
            public override void Close();
            public SqlCommand CreateCommand();
            public void EnlistDistributedTransaction(ITransaction transaction);
            public override void EnlistTransaction(Transaction transaction);
            public override DataTable GetSchema();
            public override DataTable GetSchema(string collectionName);
            public override DataTable GetSchema(string collectionName, string[] restrictionValues);
            public void ResetStatistics();
            public IDictionary RetrieveStatistics();
        */
    }
}
