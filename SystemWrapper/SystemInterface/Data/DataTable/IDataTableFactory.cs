namespace SystemInterface.Data.DataTable
{
    using System.Data;

    /// <summary>
    ///     Factory to create a new <see cref="IDataTable"/> instance.
    /// </summary>
    public interface IDataTableFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="IDataTable"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="IDataTable"/>.
        /// </returns>
        IDataTable Create();

        /// <summary>
        ///     Creates a new <see cref="IDataTable"/> instance passing the table name.
        /// </summary>
        /// <param name="tableName">
        ///     The table name.
        /// </param>
        /// <returns>
        ///     The <see cref="IDataTable"/>.
        /// </returns>
        IDataTable Create(string tableName);

        /// <summary>
        ///     Creates a new <see cref="IDataTable"/> instance passing the table name and namespace.
        /// </summary>
        /// <param name="tableName">
        ///     The table name.
        /// </param>
        /// <param name="tableNamespace">
        ///     The table namespace.
        /// </param>
        /// <returns>
        ///     The <see cref="IDataTable"/>.
        /// </returns>
        IDataTable Create(string tableName,
                          string tableNamespace);

        /// <summary>
        ///     Creates a new <see cref="IDataTable"/> instance passing a data table.
        /// </summary>
        /// <param name="dataTable">
        ///     The data table.
        /// </param>
        /// <returns>
        ///     The <see cref="IDataTable"/>.
        /// </returns>
        IDataTable Create(DataTable dataTable);

        #endregion
    }
}