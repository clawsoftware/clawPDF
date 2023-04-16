namespace SystemWrapper.Data.SqlClient
{
    using System.Data.SqlClient;

    using SystemInterface.Data.SqlClient;

    /// <summary>
    ///     Factory to create a new <see cref="ISqlCommand"/> instance.
    /// </summary>
    public class SqlCommandFactory : ISqlCommandFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="ISqlCommand"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISqlCommand"/>.
        /// </returns>
        public ISqlCommand Create()
        {
            return new SqlCommandWrap();
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlCommand"/> instance passing the command.
        /// </summary>
        /// <param name="command">
        ///     The command.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlCommand"/>.
        /// </returns>
        public ISqlCommand Create(SqlCommand command)
        {
            return new SqlCommandWrap(command);
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlCommand"/> instance passing the command text.
        /// </summary>
        /// <param name="cmdText">
        ///     The cmd text.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlCommand"/>.
        /// </returns>
        public ISqlCommand Create(string cmdText)
        {
            return new SqlCommandWrap(cmdText);
        }

        /// <summary>
        ///     Creates a new <see cref="ISqlCommand"/> instance passing the command text and the connection.
        /// </summary>
        /// <param name="cmdText">
        ///     The cmd text.
        /// </param>
        /// <param name="connection">
        ///     The connection.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlCommand"/>.
        /// </returns>
        public ISqlCommand Create(string cmdText, ISqlConnection connection)
        {
            return new SqlCommandWrap(cmdText, connection);
        }

        #endregion
    }
}
