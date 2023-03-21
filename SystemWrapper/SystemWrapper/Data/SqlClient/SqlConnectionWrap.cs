using System.Data;
using System.Data.SqlClient;
using SystemInterface.Data.SqlClient;

namespace SystemWrapper.Data.SqlClient
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Data.SqlClient.SqlConnection"/> class.
    /// </summary>
    public class SqlConnectionWrap : ISqlConnection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SqlConnectionWrap class.
        /// </summary>
        public SqlConnectionWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the SqlConnectionWrap class.
        /// </summary>
        public void Initialize()
        {
            SqlConnectionInstance = new SqlConnection();
        }

        /// <summary>
        /// Initializes a new instance of the SqlConnectionWrap class.
        /// </summary>
        /// <param name="connection">SqlConnection object.</param>
        public SqlConnectionWrap(SqlConnection connection)
        {
            Initialize(connection);
        }

        /// <summary>
        /// Initializes a new instance of the SqlConnectionWrap class.
        /// </summary>
        /// <param name="connection">SqlConnection object.</param>
        public void Initialize(SqlConnection connection)
        {
            SqlConnectionInstance = connection;
        }

        /// <summary>
        /// Initializes a new instance of the SqlConnection class when given a string that contains the connection string.
        /// </summary>
        /// <param name="connectionString">The connection used to open the SQL Server database.</param>
        public SqlConnectionWrap(string connectionString)
        {
            Initialize(connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the SqlConnection class when given a string that contains the connection string.
        /// </summary>
        /// <param name="connectionString">The connection used to open the SQL Server database.</param>
        public void Initialize(string connectionString)
        {
            SqlConnectionInstance = new SqlConnection(connectionString);
        }

        #endregion Constructors

        /// <inheritdoc />
        public string ConnectionString
        {
            get { return SqlConnectionInstance.ConnectionString; }
            set { SqlConnectionInstance.ConnectionString = value; }
        }

        /// <inheritdoc />
        public int ConnectionTimeout
        {
            get { return SqlConnectionInstance.ConnectionTimeout; }
        }

        /// <inheritdoc />
        public string Database
        {
            get { return SqlConnectionInstance.Database; }
        }

        /// <inheritdoc />
        public string DataSource
        {
            get { return SqlConnectionInstance.DataSource; }
        }

        /// <inheritdoc />
        public bool FireInfoMessageEventOnUserErrors
        {
            get { return SqlConnectionInstance.FireInfoMessageEventOnUserErrors; }
            set { SqlConnectionInstance.FireInfoMessageEventOnUserErrors = value; }
        }

        /// <inheritdoc />
        public int PacketSize
        {
            get { return SqlConnectionInstance.PacketSize; }
        }

        /// <inheritdoc />
        public string ServerVersion
        {
            get { return SqlConnectionInstance.ServerVersion; }
        }

        /// <inheritdoc />
        public SqlConnection SqlConnectionInstance { get; private set; }

        /// <inheritdoc />
        public ConnectionState State
        {
            get { return SqlConnectionInstance.State; }
        }

        /// <inheritdoc />
        public bool StatisticsEnabled
        {
            get { return SqlConnectionInstance.StatisticsEnabled; }
            set { SqlConnectionInstance.StatisticsEnabled = value; }
        }

        /// <inheritdoc />
        public string WorkstationId
        {
            get { return SqlConnectionInstance.WorkstationId; }
        }

        /// <inheritdoc />
        public void Close()
        {
            SqlConnectionInstance.Close();
        }

        /// <inheritdoc />
        public void Open()
        {
            SqlConnectionInstance.Open();
        }
    }
}