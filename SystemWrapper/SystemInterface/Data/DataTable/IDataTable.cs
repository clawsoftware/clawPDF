namespace SystemInterface.Data.DataTable
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    /// <summary>
    ///     Wrapper for the <see cref="T:System.Data.DataTable.DataTable"/> class.
    /// </summary>
    public interface IDataTable
    {
        #region Public Events

        /// <summary>
        ///     Occurs after a value has been changed for the specified <see cref="T:System.Data.DataColumn"/> in a <see cref="T:System.Data.DataRow"/>.
        /// </summary>
        event DataColumnChangeEventHandler ColumnChanged;

        /// <summary>
        ///     Occurs when a value is being changed for the specified <see cref="T:System.Data.DataColumn"/> in a <see cref="T:System.Data.DataRow"/>.
        /// </summary>
        event DataColumnChangeEventHandler ColumnChanging;

        /// <summary>
        ///     Occurs after the <see cref="T:System.Data.DataTable"/> is initialized.
        /// </summary>
        event EventHandler Initialized;

        /// <summary>
        ///     Occurs after a <see cref="T:System.Data.DataRow"/> has been changed successfully.
        /// </summary>
        event DataRowChangeEventHandler RowChanged;

        /// <summary>
        ///     Occurs when a <see cref="T:System.Data.DataRow"/> is changing.
        /// </summary>
        event DataRowChangeEventHandler RowChanging;

        /// <summary>
        ///     Occurs after a row in the table has been deleted.
        /// </summary>
        event DataRowChangeEventHandler RowDeleted;

        /// <summary>
        ///     Occurs before a row in the table is about to be deleted.
        /// </summary>
        event DataRowChangeEventHandler RowDeleting;

        /// <summary>
        ///     Occurs after a <see cref="T:System.Data.DataTable"/> is cleared.
        /// </summary>
        event DataTableClearEventHandler TableCleared;

        /// <summary>
        ///     Occurs when a <see cref="T:System.Data.DataTable"/> is cleared.
        /// </summary>
        event DataTableClearEventHandler TableClearing;

        /// <summary>
        ///     Occurs when a new <see cref="T:System.Data.DataRow"/> is inserted.
        /// </summary>
        event DataTableNewRowEventHandler TableNewRow;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Indicates whether string comparisons within the table are case-sensitive.
        /// </summary>
        /// <returns>
        ///     true if the comparison is case-sensitive; otherwise false. The default is set to the parent <see cref="T:System.Data.DataSet"/> 
        ///     object's <see cref="P:System.Data.DataSet.CaseSensitive"/> property, or false if the <see cref="T:System.Data.DataTable"/> was 
        ///     created independently of a <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        bool CaseSensitive { get; set; }

        /// <summary>
        ///     Gets the collection of child relations for this <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRelationCollection"/> that contains the child relations for the table. An empty collection is 
        ///     returned if no <see cref="T:System.Data.DataRelation"/> objects exist.
        /// </returns>
        DataRelationCollection ChildRelations { get; }

        /// <summary>
        ///     Gets the collection of columns that belong to this table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataColumnCollection"/> that contains the collection of <see cref="T:System.Data.DataColumn"/> 
        ///     objects for the table. An empty collection is returned if no <see cref="T:System.Data.DataColumn"/> objects exist.
        /// </returns>
        DataColumnCollection Columns { get; }

        /// <summary>
        ///     Gets the collection of constraints maintained by this table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.ConstraintCollection"/> that contains the collection of <see cref="T:System.Data.Constraint"/> 
        ///     objects for the table. An empty collection is returned if no <see cref="T:System.Data.Constraint"/> objects exist.
        /// </returns>
        ConstraintCollection Constraints { get; }

        /// <summary>
        ///     Gets the <see cref="T:System.Data.DataSet"/> to which this table belongs.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.DataSet"/> to which this table belongs.
        /// </returns>
        DataSet DataSet { get; }

        /// <summary>
        ///     Gets the data table instance.
        /// </summary>
        DataTable DataTableInstance { get; }

        /// <summary>
        ///     Gets a customized view of the table that may include a filtered view, or a cursor position.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.DataView"/> associated with the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        DataView DefaultView { get; }

        /// <summary>
        ///     Gets or sets the expression that returns a value used to represent this table in the user interface. The DisplayExpression property 
        ///     lets you display the name of this table in a user interface.
        /// </summary>
        /// <returns>
        ///     A display string.
        /// </returns>
        string DisplayExpression { get; set; }

        /// <summary>
        ///     Gets the collection of customized user information.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.PropertyCollection"/> that contains custom user information.
        /// </returns>
        PropertyCollection ExtendedProperties { get; }

        /// <summary>
        ///     Gets a value indicating whether there are errors in any of the rows in any of the tables of the <see cref="T:System.Data.DataSet"/> to which the table belongs.
        /// </summary>
        /// <returns>
        ///     true if errors exist; otherwise false.
        /// </returns>
        bool HasErrors { get; }

        /// <summary>
        ///     Gets a value that indicates whether the <see cref="T:System.Data.DataTable"/> is initialized.
        /// </summary>
        /// <returns>
        ///     true to indicate the component has completed initialization; otherwise false.
        /// </returns>
        bool IsInitialized { get; }

        /// <summary>
        ///     Gets or sets the locale information used to compare strings within the table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Globalization.CultureInfo"/> that contains data about the user's machine locale. The default is the 
        ///     <see cref="T:System.Data.DataSet"/> object's <see cref="T:System.Globalization.CultureInfo"/> (returned by the 
        ///     <see cref="P:System.Data.DataSet.Locale"/> property) to which the <see cref="T:System.Data.DataTable"/> belongs; if the table 
        ///     doesn't belong to a <see cref="T:System.Data.DataSet"/>, the default is the current system <see cref="T:System.Globalization.CultureInfo"/>.
        /// </returns>
        CultureInfo Locale { get; set; }

        /// <summary>
        ///     Gets or sets the initial starting size for this table.
        /// </summary>
        /// <returns>
        ///     The initial starting size in rows of this table. The default is 50.
        /// </returns>
        int MinimumCapacity { get; set; }

        /// <summary>
        ///     Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     The namespace of the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        string Namespace { get; set; }

        /// <summary>
        ///     Gets the collection of parent relations for this <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRelationCollection"/> that contains the parent relations for the table. An empty collection is returned 
        ///     if no <see cref="T:System.Data.DataRelation"/> objects exist.
        /// </returns>
        DataRelationCollection ParentRelations { get; }

        /// <summary>
        ///     Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     The prefix of the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        string Prefix { get; set; }

        /// <summary>
        ///     Gets or sets an array of columns that function as primary keys for the data table.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataColumn"/> objects.
        /// </returns>
        /// <exception cref="T:System.Data.DataException">
        ///     The key is a foreign key.
        /// </exception>
        DataColumn[] PrimaryKey { get; set; }

        /// <summary>
        ///     Gets or sets the serialization format.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.SerializationFormat"/> enumeration specifying either Binary or Xml serialization.
        /// </returns>
        SerializationFormat RemotingFormat { get; set; }

        /// <summary>
        ///     Gets the collection of rows that belong to this table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRowCollection"/> that contains <see cref="T:System.Data.DataRow"/> objects; otherwise a null value if 
        ///     no <see cref="T:System.Data.DataRow"/> objects exist.
        /// </returns>
        DataRowCollection Rows { get; }

        /// <summary>
        ///     Gets or sets an <see cref="T:System.ComponentModel.ISite"/> for the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.ComponentModel.ISite"/> for the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        ISite Site { get; set; }

        /// <summary>
        ///     Gets or sets the name of the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     The name of the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        ///     null or empty string ("") is passed in and this table belongs to a collection.
        /// </exception>
        /// <exception cref="T:System.Data.DuplicateNameException">
        ///     The table belongs to a collection that already has a table with the same name. (Comparison is case-sensitive).
        /// </exception>
        string TableName { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Commits all the changes made to this table since the last time <see cref="M:System.Data.DataTable.AcceptChanges"/> was called.
        /// </summary>
        void AcceptChanges();

        /// <summary>
        ///     Begins the initialization of a <see cref="T:System.Data.DataTable"/> that is used on a form or used by another component. 
        ///     The initialization occurs at run time.
        /// </summary>
        void BeginInit();

        /// <summary>
        ///     Turns off notifications, index maintenance, and constraints while loading data.
        /// </summary>
        void BeginLoadData();

        /// <summary>
        ///     Clears the <see cref="T:System.Data.DataTable"/> of all data.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Clones the structure of the <see cref="T:System.Data.DataTable"/>, including all <see cref="T:System.Data.DataTable"/> schemas and constraints.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.DataTable"/> with the same schema as the current <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        IDataTable Clone();

        /// <summary>
        ///     Computes the given expression on the current rows that pass the filter criteria.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Object"/>, set to the result of the computation. If the expression evaluates to null, the return value will be <see cref="F:System.DBNull.Value"/>.
        /// </returns>
        /// <param name="expression">
        ///     The expression to compute.
        /// </param>
        /// <param name="filter">
        ///     The filter to limit the rows that evaluate in the expression.
        /// </param>
        object Compute(string expression,
                       string filter);

        /// <summary>
        ///     Copies both the structure and data for this <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.DataTable"/> with the same structure (table schemas and constraints) and data as this 
        ///     <see cref="T:System.Data.DataTable"/>.If these classes have been derived, the copy will also be of the same derived classes.
        ///     <see cref="M:System.Data.DataTable.Copy"/> creates a new <see cref="T:System.Data.DataTable"/> with the same structure and data as 
        ///     the original <see cref="T:System.Data.DataTable"/>. To copy the structure to a new <see cref="T:System.Data.DataTable"/>, but not the 
        ///     data, use <see cref="M:System.Data.DataTable.Clone"/>.
        /// </returns>
        IDataTable Copy();

        /// <summary>
        ///     Returns a <see cref="T:System.Data.DataTableReader"/> corresponding to the data within this <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataTableReader"/> containing one result set, corresponding to the source <see cref="T:System.Data.DataTable"/> 
        ///     instance.
        /// </returns>
        DataTableReader CreateDataReader();

        /// <summary>
        ///     Ends the initialization of a <see cref="T:System.Data.DataTable"/> that is used on a form or used by another component. The initialization 
        ///     occurs at run time.
        /// </summary>
        void EndInit();

        /// <summary>
        ///     Turns on notifications, index maintenance, and constraints after loading data.
        /// </summary>
        void EndLoadData();

        /// <summary>
        ///     Gets a copy of the <see cref="T:System.Data.DataTable"/> that contains all changes made to it since it was loaded or 
        ///     <see cref="M:System.Data.DataTable.AcceptChanges"/> was last called.
        /// </summary>
        /// <returns>
        ///     A copy of the changes from this <see cref="T:System.Data.DataTable"/>, or null if no changes are found.
        /// </returns>
        IDataTable GetChanges();

        /// <summary>
        ///     Gets a copy of the <see cref="T:System.Data.DataTable"/> containing all changes made to it since it was last loaded, or since 
        ///     <see cref="M:System.Data.DataTable.AcceptChanges"/> was called, filtered by <see cref="T:System.Data.DataRowState"/>.
        /// </summary>
        /// <returns>
        ///     A filtered copy of the <see cref="T:System.Data.DataTable"/> that can have actions performed on it, and later be merged back in the 
        ///     <see cref="T:System.Data.DataTable"/> using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)"/>. If no rows of the desired 
        ///     <see cref="T:System.Data.DataRowState"/> are found, the method returns null.
        /// </returns>
        /// <param name="rowStates">
        ///     One of the <see cref="T:System.Data.DataRowState"/> values.
        /// </param>
        IDataTable GetChanges(DataRowState rowStates);

        /// <summary>
        ///     Gets an array of <see cref="T:System.Data.DataRow"/> objects that contain errors.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects that have errors.
        /// </returns>
        DataRow[] GetErrors();

        /// <summary>
        ///     Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <param name="info">
        ///     A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object that holds the serialized data associated with the 
        ///     <see cref="T:System.Data.DataTable"/>.
        /// </param>
        /// <param name="context">
        ///     A <see cref="T:System.Runtime.Serialization.StreamingContext"/> object that contains the source and destination of the serialized 
        ///     stream associated with the <see cref="T:System.Data.DataTable"/>.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        void GetObjectData(SerializationInfo info,
                           StreamingContext context);

        /// <summary>
        ///     Copies a <see cref="T:System.Data.DataRow"/> into a <see cref="T:System.Data.DataTable"/>, preserving any property settings, as well as original and current values.
        /// </summary>
        /// <param name="row">
        ///     The <see cref="T:System.Data.DataRow"/> to be imported.
        /// </param>
        void ImportRow(DataRow row);

        /// <summary>
        ///     Fills a <see cref="T:System.Data.DataTable"/> with values from a data source using the supplied <see cref="T:System.Data.IDataReader"/>. 
        ///     If the <see cref="T:System.Data.DataTable"/> already contains rows, the incoming data from the data source is merged with the existing rows.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="T:System.Data.IDataReader"/> that provides a result set.
        /// </param>
        void Load(IDataReader reader);

        /// <summary>
        ///     Fills a <see cref="T:System.Data.DataTable"/> with values from a data source using the supplied <see cref="T:System.Data.IDataReader"/>. 
        ///     If the DataTable already contains rows, the incoming data from the data source is merged with the existing rows according to the value of 
        ///     the <paramref name="loadOption"/> parameter.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="T:System.Data.IDataReader"/> that provides one or more result sets.
        /// </param>
        /// <param name="loadOption">
        ///     A value from the <see cref="T:System.Data.LoadOption"/> enumeration that indicates how rows already in the 
        ///     <see cref="T:System.Data.DataTable"/> are combined with incoming rows that share the same primary key.
        /// </param>
        void Load(IDataReader reader,
                  LoadOption loadOption);

        /// <summary>
        ///     Fills a <see cref="T:System.Data.DataTable"/> with values from a data source using the supplied <see cref="T:System.Data.IDataReader"/> 
        ///     using an error-handling delegate.
        /// </summary>
        /// <param name="reader">
        ///     A <see cref="T:System.Data.IDataReader"/> that provides a result set.
        /// </param>
        /// <param name="loadOption">
        ///     A value from the <see cref="T:System.Data.LoadOption"/> enumeration that indicates how rows already in the 
        ///     <see cref="T:System.Data.DataTable"/> are combined with incoming rows that share the same primary key.
        /// </param>
        /// <param name="errorHandler">
        ///     A <see cref="T:System.Data.FillErrorEventHandler"/> delegate to call when an error occurs while loading data.
        /// </param>
        void Load(IDataReader reader,
                  LoadOption loadOption,
                  FillErrorEventHandler errorHandler);

        /// <summary>
        ///     Finds and updates a specific row. If no matching row is found, a new row is created using the given values.
        /// </summary>
        /// <returns>
        ///     The new <see cref="T:System.Data.DataRow"/>.
        /// </returns>
        /// <param name="values">
        ///     An array of values used to create the new row.
        /// </param>
        /// <param name="acceptChanges">
        ///     true to accept changes; otherwise false.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The array is larger than the number of columns in the table.
        /// </exception>
        /// <exception cref="T:System.InvalidCastException">
        ///     A value doesn't match its respective column type.
        /// </exception>
        /// <exception cref="T:System.Data.ConstraintException">
        ///     Adding the row invalidates a constraint.
        /// </exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">
        ///     Attempting to put a null in a column where <see cref="P:System.Data.DataColumn.AllowDBNull"/> is false.
        /// </exception>
        DataRow LoadDataRow(object[] values,
                            bool acceptChanges);

        /// <summary>
        ///     Finds and updates a specific row. If no matching row is found, a new row is created using the given values.
        /// </summary>
        /// <returns>
        ///     The new <see cref="T:System.Data.DataRow"/>.
        /// </returns>
        /// <param name="values">
        ///     An array of values used to create the new row.
        /// </param>
        /// <param name="loadOption">
        ///     Used to determine how the array values are applied to the corresponding values in an existing row.
        /// </param>
        DataRow LoadDataRow(object[] values,
                            LoadOption loadOption);

        /// <summary>
        ///     Merge the specified <see cref="T:System.Data.DataTable"/> with the current <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <param name="table">
        ///     The <see cref="T:System.Data.DataTable"/> to be merged with the current <see cref="T:System.Data.DataTable"/>.
        /// </param>
        void Merge(DataTable table);

        /// <summary>
        ///     Merge the specified <see cref="T:System.Data.DataTable"/> with the current DataTable, indicating whether to preserve changes in 
        ///     the current DataTable.
        /// </summary>
        /// <param name="table">
        ///     The DataTable to be merged with the current DataTable.
        /// </param>
        /// <param name="preserveChanges">
        ///     true, to preserve changes in the current DataTable; otherwise false.
        /// </param>
        void Merge(DataTable table,
                   bool preserveChanges);

        /// <summary>
        ///     Merge the specified <see cref="T:System.Data.DataTable"/> with the current DataTable, indicating whether to preserve changes and 
        ///     how to handle missing schema in the current DataTable.
        /// </summary>
        /// <param name="table">
        ///     The <see cref="T:System.Data.DataTable"/> to be merged with the current <see cref="T:System.Data.DataTable"/>.
        /// </param>
        /// <param name="preserveChanges">
        ///     true, to preserve changes in the current <see cref="T:System.Data.DataTable"/>; otherwise false.
        /// </param>
        /// <param name="missingSchemaAction">
        ///     One of the <see cref="T:System.Data.MissingSchemaAction"/> values.
        /// </param>
        void Merge(DataTable table,
                   bool preserveChanges,
                   MissingSchemaAction missingSchemaAction);

        /// <summary>
        ///     Creates a new <see cref="T:System.Data.DataRow"/> with the same schema as the table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRow"/> with the same schema as the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        DataRow NewRow();

        /// <summary>
        ///     Reads XML schema and data into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="stream">
        ///     An object that derives from <see cref="T:System.IO.Stream"/>
        /// </param>
        XmlReadMode ReadXml(Stream stream);

        /// <summary>
        ///     Reads XML schema and data into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.IO.TextReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="reader">
        ///     The <see cref="T:System.IO.TextReader"/> that will be used to read the data.
        /// </param>
        XmlReadMode ReadXml(TextReader reader);

        /// <summary>
        ///     Reads XML schema and data into the <see cref="T:System.Data.DataTable"/> from the specified file.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="fileName">
        ///     The name of the file from which to read the data.
        /// </param>
        XmlReadMode ReadXml(string fileName);

        /// <summary>
        ///     Reads XML Schema and Data into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.Xml.XmlReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="reader">
        ///     The <see cref="T:System.Xml.XmlReader"/> that will be used to read the data.
        /// </param>
        XmlReadMode ReadXml(XmlReader reader);

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> using the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream used to read the schema.
        /// </param>
        void ReadXmlSchema(Stream stream);

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.IO.TextReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="T:System.IO.TextReader"/> used to read the schema information.
        /// </param>
        void ReadXmlSchema(TextReader reader);

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> from the specified file.
        /// </summary>
        /// <param name="fileName">
        ///     The name of the file from which to read the schema information.
        /// </param>
        void ReadXmlSchema(string fileName);

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.Xml.XmlReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="T:System.Xml.XmlReader"/> used to read the schema information.
        /// </param>
        void ReadXmlSchema(XmlReader reader);

        /// <summary>
        ///     Rolls back all changes that have been made to the table since it was loaded, or the last time 
        ///     <see cref="M:System.Data.DataTable.AcceptChanges"/> was called.
        /// </summary>
        void RejectChanges();

        /// <summary>
        ///     Resets the <see cref="T:System.Data.DataTable"/> to its original state. Reset removes all data, indexes, relations, and columns of the table. 
        ///     If a DataSet includes a DataTable, the table will still be part of the DataSet after the table is reset.
        /// </summary>
        void Reset();

        /// <summary>
        ///     Gets an array of all <see cref="T:System.Data.DataRow"/> objects.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects.
        /// </returns>
        DataRow[] Select();

        /// <summary>
        ///     Gets an array of all <see cref="T:System.Data.DataRow"/> objects that match the filter criteria.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects.
        /// </returns>
        /// <param name="filterExpression">
        ///     The criteria to use to filter the rows. For examples on how to filter rows, see DataView RowFilter Syntax [C#].
        /// </param>
        DataRow[] Select(string filterExpression);

        /// <summary>
        ///     Gets an array of all <see cref="T:System.Data.DataRow"/> objects that match the filter criteria, in the specified sort order.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects matching the filter expression.
        /// </returns>
        /// <param name="filterExpression">
        ///     The criteria to use to filter the rows. For examples on how to filter rows, see DataView RowFilter Syntax [C#].
        /// </param>
        /// <param name="sort">
        ///     A string specifying the column and sort direction.
        /// </param>
        DataRow[] Select(string filterExpression,
                         string sort);

        /// <summary>
        ///     Gets an array of all <see cref="T:System.Data.DataRow"/> objects that match the filter in the order of the sort that match the specified state.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects.
        /// </returns>
        /// <param name="filterExpression">
        ///     The criteria to use to filter the rows. For examples on how to filter rows, see DataView RowFilter Syntax [C#].
        /// </param>
        /// <param name="sort">
        ///     A string specifying the column and sort direction.
        /// </param>
        /// <param name="recordStates">
        ///     One of the <see cref="T:System.Data.DataViewRowState"/> values.
        /// </param>
        DataRow[] Select(string filterExpression,
                         string sort,
                         DataViewRowState recordStates);

        /// <summary>
        ///     Gets the <see cref="P:System.Data.DataTable.TableName"/> and <see cref="P:System.Data.DataTable.DisplayExpression"/>, if there is one as a 
        ///     concatenated string.
        /// </summary>
        /// <returns>
        ///     A string consisting of the <see cref="P:System.Data.DataTable.TableName"/> and the <see cref="P:System.Data.DataTable.DisplayExpression"/> values.
        /// </returns>
        string ToString();

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the data will be written.
        /// </param>
        void WriteXml(Stream stream);

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.IO.Stream"/>. 
        ///     To save the data for the table and all its descendants, set the <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the data will be written.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(Stream stream,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> with which to write the content.
        /// </param>
        void WriteXml(TextWriter writer);

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.IO.TextWriter"/>. 
        ///     To save the data for the table and all its descendants, set the <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> with which to write the content.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(TextWriter writer,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> with which to write the contents.
        /// </param>
        void WriteXml(XmlWriter writer);

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> with which to write the contents.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(XmlWriter writer,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified file.
        /// </summary>
        /// <param name="fileName">
        ///     The file to which to write the XML data.
        /// </param>
        void WriteXml(string fileName);

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified file. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="fileName">
        ///     The file to which to write the XML data.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(string fileName,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the <paramref name="mode"/> parameter to WriteSchema.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the data will be written.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        void WriteXml(Stream stream,
                      XmlWriteMode mode);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the <paramref name="mode"/> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the data will be written.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values. 
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(Stream stream,
                      XmlWriteMode mode,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> using the specified 
        ///     <see cref="T:System.IO.TextWriter"/> and <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the 
        ///     <paramref name="mode"/> parameter to WriteSchema.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> used to write the document.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        void WriteXml(TextWriter writer,
                      XmlWriteMode mode);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> using the specified 
        ///     <see cref="T:System.IO.TextWriter"/> and <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the 
        ///     <paramref name="mode"/> parameter to WriteSchema. To save the data for the table and all its descendants, set the 
        ///     <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> used to write the document.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(TextWriter writer,
                      XmlWriteMode mode,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> using the specified 
        ///     <see cref="T:System.Xml.XmlWriter"/> and <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the 
        ///     <paramref name="mode"/> parameter to WriteSchema.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> used to write the document.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        void WriteXml(XmlWriter writer,
                      XmlWriteMode mode);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> using the specified 
        ///     <see cref="T:System.Xml.XmlWriter"/> and <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the 
        ///     <paramref name="mode"/> parameter to WriteSchema. To save the data for the table and all its descendants, set the 
        ///     <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> used to write the document.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(XmlWriter writer,
                      XmlWriteMode mode,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> using the specified file and 
        ///     <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the <paramref name="mode"/> parameter to WriteSchema.
        /// </summary>
        /// <param name="fileName">
        ///     The name of the file to which the data will be written.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        void WriteXml(string fileName,
                      XmlWriteMode mode);

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> using the specified file and 
        ///     <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the <paramref name="mode"/> parameter to WriteSchema. 
        ///     To save the data for the table and all its descendants, set the <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="fileName">
        ///     The name of the file to which the data will be written.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        void WriteXml(string fileName,
                      XmlWriteMode mode,
                      bool writeHierarchy);

        /// <summary>
        ///     Writes the current data structure of the <see cref="T:System.Data.DataTable"/> as an XML schema to the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the XML schema will be written.
        /// </param>
        void WriteXmlSchema(Stream stream);

        /// <summary>
        ///     Writes the current data structure of the <see cref="T:System.Data.DataTable"/> as an XML schema to the specified stream. To save the schema 
        ///     for the table and all its descendants, set the <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the XML schema will be written.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.
        /// </param>
        void WriteXmlSchema(Stream stream,
                            bool writeHierarchy);

        /// <summary>
        ///     Writes the current data structure of the <see cref="T:System.Data.DataTable"/> as an XML schema using the specified 
        ///     <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> with which to write.
        /// </param>
        void WriteXmlSchema(TextWriter writer);

        /// <summary>
        ///     Writes the current data structure of the <see cref="T:System.Data.DataTable"/> as an XML schema using the specified 
        ///     <see cref="T:System.IO.TextWriter"/>. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy"/> 
        ///     parameter to true.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> with which to write.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.
        /// </param>
        void WriteXmlSchema(TextWriter writer,
                            bool writeHierarchy);

        #endregion
    }
}