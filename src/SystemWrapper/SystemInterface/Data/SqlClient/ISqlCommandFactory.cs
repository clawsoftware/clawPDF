namespace SystemInterface.Data.SqlClient
{
    using System.Data.SqlClient;

    /// <summary>
    ///     Factory to create a new <see cref="ISqlCommand"/> instance.
    /// </summary>
    public interface ISqlCommandFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="ISqlCommand"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISqlCommand"/>.
        /// </returns>
        ISqlCommand Create();

        /// <summary>
        ///     Creates a new <see cref="ISqlCommand"/> instance passing the command.
        /// </summary>
        /// <param name="command">
        ///     The command.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlCommand"/>.
        /// </returns>
        ISqlCommand Create(SqlCommand command);

        /// <summary>
        ///     Creates a new <see cref="ISqlCommand"/> instance passing the command text.
        /// </summary>
        /// <param name="cmdText">
        ///     The cmd text.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlCommand"/>.
        /// </returns>
        ISqlCommand Create(string cmdText);

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
        ISqlCommand Create(string cmdText, ISqlConnection connection);

        #endregion
    }
}
