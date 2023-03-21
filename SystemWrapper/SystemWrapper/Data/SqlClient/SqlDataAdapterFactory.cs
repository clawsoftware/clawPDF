namespace SystemWrapper.Data.SqlClient
{
    using System.Data.SqlClient;

    using SystemInterface.Data.SqlClient;

    /// <summary>
    ///     Factory to create a new <see cref="ISqlDataAdapter"/> instance.
    /// </summary>
    public class SqlDataAdapterFactory : ISqlDataAdapterFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="ISqlDataAdapter"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISqlDataAdapter"/>.
        /// </returns>
        public ISqlDataAdapter Create()
        {
            return new SqlDataAdapterWrap();
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlDataAdapter"/> instance passing the sql command.
        /// </summary>
        /// <param name="sqlCommand">
        ///     The sql command.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlDataAdapter"/>.
        /// </returns>
        public ISqlDataAdapter Create(SqlCommand sqlCommand)
        {
            return new SqlDataAdapterWrap(sqlCommand);
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlDataAdapter"/> instance passing the select command text and the connection.
        /// </summary>
        /// <param name="selectCommandText">
        ///     The select command text.
        /// </param>
        /// <param name="selectConnection">
        ///     The select connection.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlDataAdapter"/>.
        /// </returns>
        public ISqlDataAdapter Create(string selectCommandText,
                                      SqlConnection selectConnection)
        {
            return new SqlDataAdapterWrap(selectCommandText, selectConnection);
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlDataAdapter"/> instance passing the select command text and the connection string.
        /// </summary>
        /// <param name="selectCommandText">
        ///     The select command text.
        /// </param>
        /// <param name="selectConnectionString">
        ///     The select connection string.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlDataAdapter"/>.
        /// </returns>
        public ISqlDataAdapter Create(string selectCommandText,
                                      string selectConnectionString)
        {
            return new SqlDataAdapterWrap(selectCommandText, selectConnectionString);
        }

        #endregion
    }
}