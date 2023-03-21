namespace SystemWrapper.Data.SqlClient
{
    using System.Data.SqlClient;

    using SystemInterface.Data.SqlClient;

    /// <summary>
    ///     Factory to create a new <see cref="ISqlConnection"/> instance.
    /// </summary>
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="ISqlConnection"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISqlConnection"/>.
        /// </returns>
        public ISqlConnection Create()
        {
            return new SqlConnectionWrap();
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlConnection"/> instance passing the connection.
        /// </summary>
        /// <param name="connection">
        ///     The connection.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlConnection"/>.
        /// </returns>
        public ISqlConnection Create(SqlConnection connection)
        {
            return new SqlConnectionWrap(connection);
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlConnection"/> instance passingn the connection string.
        /// </summary>
        /// <param name="connectionString">
        ///     The connection string.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlConnection"/>.
        /// </returns>
        public ISqlConnection Create(string connectionString)
        {
            return new SqlConnectionWrap(connectionString);
        }

        #endregion
    }
}