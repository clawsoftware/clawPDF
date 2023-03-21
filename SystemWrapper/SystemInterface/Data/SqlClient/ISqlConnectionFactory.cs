namespace SystemInterface.Data.SqlClient
{
    using System.Data.SqlClient;

    /// <summary>
    ///     Factory to create a new <see cref="ISqlConnection"/> instance.
    /// </summary>
    public interface ISqlConnectionFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="ISqlConnection"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="ISqlConnection"/>.
        /// </returns>
        ISqlConnection Create();

        /// <summary>
        ///     Creates a new <see cref="ISqlConnection"/> instance passing the connection.
        /// </summary>
        /// <param name="connection">
        ///     The connection.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlConnection"/>.
        /// </returns>
        ISqlConnection Create(SqlConnection connection);

        /// <summary>
        ///     Creates a new <see cref="ISqlConnection"/> instance passingn the connection string.
        /// </summary>
        /// <param name="connectionString">
        ///     The connection string.
        /// </param>
        /// <returns>
        ///     The <see cref="ISqlConnection"/>.
        /// </returns>
        ISqlConnection Create(string connectionString);

        #endregion
    }
}