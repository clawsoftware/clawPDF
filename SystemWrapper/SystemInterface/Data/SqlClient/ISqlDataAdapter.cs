namespace SystemInterface.Data.SqlClient
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Runtime.Remoting;

    /// <summary>
    ///     Wrapper for <see cref="T:System.Data.SqlClient.SqlCommand"/> class.
    /// </summary>
    public interface ISqlDataAdapter
    {
        #region Public Events

        /// <summary>
        ///     Occurs when the component is disposed by a call to the <see cref="M:System.ComponentModel.Component.Dispose"/> method.
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        ///     Returned when an error occurs during a fill operation.
        /// </summary>
        event FillErrorEventHandler FillError;

        /// <summary>
        ///     Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)"/> after a command is executed against the 
        ///     data source. The attempt to update is made, so the event fires.
        /// </summary>
        event SqlRowUpdatedEventHandler RowUpdated;

        /// <summary>
        ///     Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)"/> before a command is executed against the 
        ///     data source. The attempt to update is made, so the event fires.
        /// </summary>
        event SqlRowUpdatingEventHandler RowUpdating;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether <see cref="M:System.Data.DataRow.AcceptChanges"/> is called on a <see cref="T:System.Data.DataRow"/> 
        ///     after it is added to the <see cref="T:System.Data.DataTable"/> during any of the Fill operations.
        /// </summary>
        /// <returns>
        ///     true if <see cref="M:System.Data.DataRow.AcceptChanges"/> is called on the <see cref="T:System.Data.DataRow"/>; otherwise false. 
        ///     The default is true.
        /// </returns>
        bool AcceptChangesDuringFill { get; set; }

        /// <summary>
        ///     Gets or sets whether <see cref="M:System.Data.DataRow.AcceptChanges"/> is called during a 
        ///     <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)"/>.
        /// </summary>
        /// <returns>
        ///     true if <see cref="M:System.Data.DataRow.AcceptChanges"/> is called during an 
        ///     <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)"/>; otherwise false. The default is true.
        /// </returns>
        bool AcceptChangesDuringUpdate { get; set; }

        /// <summary>
        /// Returns the component's container.
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        ///     Gets or sets a value that specifies whether to generate an exception when an error is encountered during a row update.
        /// </summary>
        /// <returns>
        ///     true to continue the update without generating an exception; otherwise false. The default is false.
        /// </returns>
        bool ContinueUpdateOnError { get; set; }

        /// <summary>
        ///     Gets or sets a Transact-SQL statement or stored procedure to delete records from the data set.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.SqlClient.SqlCommand"/> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)"/> to delete records in the database that correspond to deleted rows in the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        ISqlCommand DeleteCommand { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="T:System.Data.LoadOption"/> that determines how the adapter fills the <see cref="T:System.Data.DataTable"/> 
        ///     from the <see cref="T:System.Data.Common.DbDataReader"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.LoadOption"/> value.
        /// </returns>
        LoadOption FillLoadOption { get; set; }

        /// <summary>
        ///     Gets or sets a Transact-SQL statement or stored procedure to insert new records into the data source.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.SqlClient.SqlCommand"/> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)"/> to insert records into the database that correspond to new rows in the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        ISqlCommand InsertCommand { get; set; }

        /// <summary>
        ///     Determines the action to take when incoming data does not have a matching table or column.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="T:System.Data.MissingMappingAction"/> values. The default is Passthrough.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The value set is not one of the <see cref="T:System.Data.MissingMappingAction"/> values.
        /// </exception>
        MissingMappingAction MissingMappingAction { get; set; }

        /// <summary>
        ///     Determines the action to take when existing <see cref="T:System.Data.DataSet"/> schema does not match incoming data.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="T:System.Data.MissingSchemaAction"/> values. The default is Add.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     The value set is not one of the <see cref="T:System.Data.MissingSchemaAction"/> values.
        /// </exception>
        MissingSchemaAction MissingSchemaAction { get; set; }

        /// <summary>
        ///     Gets or sets whether the Fill method should return provider-specific values or common CLS-compliant values.
        /// </summary>
        /// <returns>
        ///     true if the Fill method should return provider-specific values; otherwise false to return common CLS-compliant values.
        /// </returns>
        bool ReturnProviderSpecificTypes { get; set; }

        /// <summary>
        ///     Gets or sets a Transact-SQL statement or stored procedure used to select records in the data source.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.SqlClient.SqlCommand"/> used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)"/> to select records from the database for placement in the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        ISqlCommand SelectCommand { get; set; }

        /// <summary>
        ///     Gets or sets the site of the <see cref='System.ComponentModel.Component'/>.
        /// </summary>
        ISite Site { get; set; }

        /// <summary>
        ///     Gets a collection that provides the master mapping between a source table and a <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet"/>. 
        ///     The default value is an empty collection.
        /// </returns>
        DataTableMappingCollection TableMappings { get; }

        /// <summary>
        ///     Gets or sets the number of rows that are processed in each round-trip to the server.
        /// </summary>
        /// <returns>
        ///     The number of rows to process per-batch. Value isEffect0There is no limit on the batch size..1Disables batch updating.&gt;1Changes are sent using batches of <see cref="P:System.Data.SqlClient.SqlDataAdapter.UpdateBatchSize"/> operations at a time.When setting this to a value other than 1, all the commands associated with the <see cref="T:System.Data.SqlClient.SqlDataAdapter"/> have to have their UpdatedRowSource property set to None or OutputParameters. An exception is thrown otherwise.
        /// </returns>
        int UpdateBatchSize { get; set; }

        /// <summary>
        ///     Gets or sets a Transact-SQL statement or stored procedure used to update records in the data source.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.SqlClient.SqlCommand"/> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)"/> to update records in the database that correspond to modified rows in the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        ISqlCommand UpdateCommand { get; set; }

        #endregion

        // event SqlRowUpdatedEventHandler RowUpdated
        // event SqlRowUpdatingEventHandler RowUpdating

        #region Public Methods and Operators

        /// <summary>
        ///     Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.
        /// </summary>
        /// <param name="requestedType">
        ///     The requested type.
        /// </param>
        /// <returns>
        ///     The <see cref="ObjRef"/>.
        /// </returns>
        ObjRef CreateObjRef(Type requestedType);

        /// <summary>
        ///     Disposes of the <see cref='System.ComponentModel.Component'/>.
        /// </summary>
        void Dispose();

        /// <summary>
        ///     Returns a boolean indicating if the passed in object obj is Equal to this.  Equality is defined as object equality for reference
        ///     types and bitwise equality for value types using a loader trick to replace Equals with EqualsValue for value types).
        /// </summary>
        /// <param name="obj">
        ///     The obj.
        /// </param>
        /// <returns>
        ///     The <see cref="bool"/>.
        /// </returns>
        bool Equals(Object obj);

        /// <summary>
        ///     Adds or refreshes rows in the <see cref="T:System.Data.DataSet"/>.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet"/>. This does not include rows 
        ///     affected by statements that do not return rows.
        /// </returns>
        /// <param name="dataSet">
        ///     A <see cref="T:System.Data.DataSet"/> to fill with records and, if necessary, schema.
        /// </param>
        int Fill(DataSet dataSet);

        /// <summary>
        ///     Adds or refreshes rows in the <see cref="T:System.Data.DataSet"/> to match those in the data source using the 
        ///     <see cref="T:System.Data.DataSet"/> and <see cref="T:System.Data.DataTable"/> names.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet"/>. This does not include rows 
        ///     affected by statements that do not return rows.
        /// </returns>
        /// <param name="dataSet">
        ///     A <see cref="T:System.Data.DataSet"/> to fill with records and, if necessary, schema.
        /// </param>
        /// <param name="srcTable">
        ///     The name of the source table to use for table mapping.
        /// </param>
        /// <exception cref="T:System.SystemException">
        ///     The source table is invalid.
        /// </exception>
        int Fill(DataSet dataSet,
                 string srcTable);

        /// <summary>
        ///     Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet"/> to match those in the data source using the 
        ///     <see cref="T:System.Data.DataSet"/> and <see cref="T:System.Data.DataTable"/> names.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet"/>. This does not include rows 
        ///     affected by statements that do not return rows.
        /// </returns>
        /// <param name="dataSet">
        ///     A <see cref="T:System.Data.DataSet"/> to fill with records and, if necessary, schema.
        /// </param>
        /// <param name="startRecord">
        ///     The zero-based record number to start with.
        /// </param>
        /// <param name="maxRecords">
        ///     The maximum number of records to retrieve.
        /// </param>
        /// <param name="srcTable">
        ///     The name of the source table to use for table mapping.
        /// </param>
        /// <exception cref="T:System.SystemException">
        ///     The <see cref="T:System.Data.DataSet"/> is invalid.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The source table is invalid.-or- The connection is invalid.
        /// </exception>
        /// <exception cref="T:System.InvalidCastException">
        ///     The connection could not be found.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The <paramref name="startRecord"/> parameter is less than 0.-or- The <paramref name="maxRecords"/> parameter is less than 0.
        /// </exception>
        int Fill(DataSet dataSet,
                 int startRecord,
                 int maxRecords,
                 string srcTable);

        /// <summary>
        ///     Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet"/> to match those in the data source using the 
        ///     <see cref="T:System.Data.DataTable"/> name.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet"/>. This does not include rows affected 
        ///     by statements that do not return rows.
        /// </returns>
        /// <param name="dataTable">
        ///     The name of the <see cref="T:System.Data.DataTable"/> to use for table mapping.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The source table is invalid.
        /// </exception>
        int Fill(DataTable dataTable);

        /// <summary>
        ///     Adds or refreshes rows in a <see cref="T:System.Data.DataTable"/> to match those in the data source starting at the specified record 
        ///     and retrieving up to the specified maximum number of records.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable"/>. This value does not include rows 
        ///     affected by statements that do not return rows.
        /// </returns>
        /// <param name="startRecord">
        ///     The zero-based record number to start with.
        /// </param>
        /// <param name="maxRecords">
        ///     The maximum number of records to retrieve.
        /// </param>
        /// <param name="dataTables">
        ///     The <see cref="T:System.Data.DataTable"/> objects to fill from the data source.
        /// </param>
        int Fill(int startRecord,
                 int maxRecords,
                 params DataTable[] dataTables);

        /// <summary>
        ///     Configures the schema of the specified <see cref="T:System.Data.DataTable"/> based on the specified <see cref="T:System.Data.SchemaType"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataTable"/> that contains schema information returned from the data source.
        /// </returns>
        /// <param name="dataTable">
        ///     The <see cref="T:System.Data.DataTable"/> to be filled with the schema from the data source.
        /// </param>
        /// <param name="schemaType">
        ///     One of the <see cref="T:System.Data.SchemaType"/> values.
        /// </param>
        DataTable FillSchema(DataTable dataTable,
                             SchemaType schemaType);

        /// <summary>
        ///     Adds a <see cref="T:System.Data.DataTable"/> named "Table" to the specified <see cref="T:System.Data.DataSet"/> and configures the schema 
        ///     to match that in the data source based on the specified <see cref="T:System.Data.SchemaType"/>.
        /// </summary>
        /// <returns>
        ///     A reference to a collection of <see cref="T:System.Data.DataTable"/> objects that were added to the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        /// <param name="dataSet">
        ///     A <see cref="T:System.Data.DataSet"/> to insert the schema in.
        /// </param>
        /// <param name="schemaType">
        ///     One of the <see cref="T:System.Data.SchemaType"/> values that specify how to insert the schema.
        /// </param>
        DataTable[] FillSchema(DataSet dataSet,
                               SchemaType schemaType);

        /// <summary>
        ///     Adds a <see cref="T:System.Data.DataTable"/> to the specified <see cref="T:System.Data.DataSet"/> and configures the schema to match 
        ///     that in the data source based upon the specified <see cref="T:System.Data.SchemaType"/> and <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A reference to a collection of <see cref="T:System.Data.DataTable"/> objects that were added to the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        /// <param name="dataSet">
        ///     A <see cref="T:System.Data.DataSet"/> to insert the schema in.
        /// </param>
        /// <param name="schemaType">
        ///     One of the <see cref="T:System.Data.SchemaType"/> values that specify how to insert the schema.
        /// </param>
        /// <param name="srcTable">
        ///     The name of the source table to use for table mapping.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     A source table from which to get the schema could not be found.
        /// </exception>
        DataTable[] FillSchema(DataSet dataSet,
                               SchemaType schemaType,
                               string srcTable);

        /// <summary>
        ///     Gets the parameters set by the user when executing an SQL SELECT statement.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.IDataParameter"/> objects that contains the parameters set by the user.
        /// </returns>
        IDataParameter[] GetFillParameters();

        /// <summary>
        ///     GetHashCode is intended to serve as a hash function for this object. Based on the contents of the object, the hash function will 
        ///     return a suitable value with a relatively random distribution over the various inputs.
        /// </summary>
        /// <returns>
        ///     The <see cref="int"/>.
        /// </returns>
        int GetHashCode();

        /// <summary>
        ///     Retrieves the current lifetime service object that controls the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="Object"/>.
        /// </returns>
        Object GetLifetimeService();

        /// <summary>
        ///     Returns a Type object which represent this object instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="Type"/>.
        /// </returns>
        Type GetType();

        /// <summary>
        ///     Obtains a lifetime service object to control the lifetime policy for this instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="object"/>.
        /// </returns>
        object InitializeLifetimeService();

        /// <summary>
        ///     Resets <see cref="P:System.Data.Common.DataAdapter.FillLoadOption"/> to its default state and causes 
        ///     <see cref="M:System.Data.Common.DataAdapter.Fill(System.Data.DataSet)"/> to honor 
        ///     <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill"/>.
        /// </summary>
        void ResetFillLoadOption();

        /// <summary>
        ///     Determines whether the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill"/> property should be persisted.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill"/> property is persisted; otherwise false.
        /// </returns>
        bool ShouldSerializeAcceptChangesDuringFill();

        /// <summary>
        ///     Determines whether the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption"/> property should be persisted.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption"/> property is persisted; otherwise false.
        /// </returns>
        bool ShouldSerializeFillLoadOption();

        /// <summary>
        ///     Returns a <see cref='System.String'/> containing the name of the <see cref='System.ComponentModel.Component'/> , if any. 
        ///     This method should not be overridden. For internal use only.
        /// </summary>
        /// <returns>
        ///     The <see cref="String"/>.
        /// </returns>
        String ToString();

        /// <summary>
        ///     Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or 
        ///     deleted row in the specified <see cref="T:System.Data.DataSet"/>.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully updated from the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        /// <param name="dataSet">
        ///     The <see cref="T:System.Data.DataSet"/> used to update the data source.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The source table is invalid.
        /// </exception>
        /// <exception cref="T:System.Data.DBConcurrencyException">
        ///     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected.
        /// </exception>
        int Update(DataSet dataSet);

        /// <summary>
        ///     Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or 
        ///     deleted row in the specified array in the <see cref="T:System.Data.DataSet"/>.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully updated from the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        /// <param name="dataRows">
        ///     An array of <see cref="T:System.Data.DataRow"/> objects used to update the data source.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <see cref="T:System.Data.DataSet"/> is invalid.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The source table is invalid.
        /// </exception>
        /// <exception cref="T:System.SystemException">
        ///     No <see cref="T:System.Data.DataRow"/> exists to update.-or- No <see cref="T:System.Data.DataTable"/> exists to update.-or- 
        ///     No <see cref="T:System.Data.DataSet"/> exists to use as a source.
        /// </exception>
        /// <exception cref="T:System.Data.DBConcurrencyException">
        ///     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected.
        /// </exception>
        int Update(DataRow[] dataRows);

        /// <summary>
        ///     Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or 
        ///     deleted row in the specified <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully updated from the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        /// <param name="dataTable">
        ///     The <see cref="T:System.Data.DataTable"/> used to update the data source.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <see cref="T:System.Data.DataSet"/> is invalid.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The source table is invalid.
        /// </exception>
        /// <exception cref="T:System.SystemException">
        ///     No <see cref="T:System.Data.DataRow"/> exists to update.-or- No <see cref="T:System.Data.DataTable"/> exists to update.-or- 
        ///     No <see cref="T:System.Data.DataSet"/> exists to use as a source.
        /// </exception>
        /// <exception cref="T:System.Data.DBConcurrencyException">
        ///     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected.
        /// </exception>
        int Update(DataTable dataTable);

        /// <summary>
        ///     Updates the values in the database by executing the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or 
        ///     deleted row in the <see cref="T:System.Data.DataSet"/>  with the specified <see cref="T:System.Data.DataTable"/> name.
        /// </summary>
        /// <returns>
        ///     The number of rows successfully updated from the <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        /// <param name="dataSet">
        ///     The <see cref="T:System.Data.DataSet"/> to use to update the data source.
        /// </param>
        /// <param name="srcTable">
        ///     The name of the source table to use for table mapping.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <see cref="T:System.Data.DataSet"/> is invalid.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The source table is invalid.
        /// </exception>
        /// <exception cref="T:System.Data.DBConcurrencyException">
        ///     An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected.
        /// </exception>
        int Update(DataSet dataSet,
                   string srcTable);

        #endregion
    }
}