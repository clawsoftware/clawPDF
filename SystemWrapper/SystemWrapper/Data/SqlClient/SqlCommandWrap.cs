using System.Data.SqlClient;
using SystemInterface.Data.SqlClient;

namespace SystemWrapper.Data.SqlClient
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Data.SqlClient.SqlCommand"/> class.
    /// </summary>
    public class SqlCommandWrap : ISqlCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class.
        /// </summary>
        public SqlCommandWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class.
        /// </summary>
        public void Initialize()
        {
            SqlCommandInstance = new SqlCommand();
        }

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class.
        /// </summary>
        /// <param name="command">SqlCommand object.</param>
        public SqlCommandWrap(SqlCommand command)
        {
            Initialize(command);
        }

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class.
        /// </summary>
        /// <param name="command">SqlCommand object.</param>
        public void Initialize(SqlCommand command)
        {
            SqlCommandInstance = command;
        }

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class with the text of the query.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        public SqlCommandWrap(string cmdText)
        {
            Initialize(cmdText);
        }

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class with the text of the query.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        public void Initialize(string cmdText)
        {
            SqlCommandInstance = new SqlCommand(cmdText);
        }

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class with the text of the query and a ISqlConnectionWrap.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">A ISqlConnectionWrap that represents the connection to an instance of SQL Server.</param>
        public SqlCommandWrap(string cmdText, ISqlConnection connection)
        {
            Initialize(cmdText, connection);
        }

        /// <summary>
        /// Initializes a new instance of the SqlCommandWrap class with the text of the query and a ISqlConnectionWrap.
        /// </summary>
        /// <param name="cmdText">The text of the query.</param>
        /// <param name="connection">A ISqlConnectionWrap that represents the connection to an instance of SQL Server.</param>
        public void Initialize(string cmdText, ISqlConnection connection)
        {
            SqlCommandInstance = new SqlCommand(cmdText, connection.SqlConnectionInstance);
        }

        #endregion Constructors

        /// <inheritdoc />
        public SqlCommand SqlCommandInstance { get; private set; }

        /// <inheritdoc />
        public ISqlDataReader ExecuteReader()
        {
            return new SqlDataReaderWrap(SqlCommandInstance.ExecuteReader());
        }
    }
}