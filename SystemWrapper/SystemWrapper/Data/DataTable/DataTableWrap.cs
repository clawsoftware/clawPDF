namespace SystemWrapper.Data.DataTable
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    using SystemInterface.Data.DataTable;

    /// <summary>
    ///     Wrapper for the <see cref="T:System.Data.DataTable.DataTable"/> class.
    /// </summary>
    public class DataTableWrap : IDataTable
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTableWrap"/> class.
        /// </summary>
        public DataTableWrap()
        {
            this.Initialize();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTableWrap"/> class.
        /// </summary>
        /// <param name="tableName">
        ///     The table name.
        /// </param>
        public DataTableWrap(string tableName)
        {
            this.Initialize(tableName);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTableWrap"/> class.
        /// </summary>
        /// <param name="tableName">
        ///     The table name.
        /// </param>
        /// <param name="tableNamespace">
        ///     The table namespace.
        /// </param>
        public DataTableWrap(string tableName,
                             string tableNamespace)
        {
            this.Initialize(tableName, tableNamespace);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataTableWrap"/> class.
        /// </summary>
        /// <param name="dataTable">
        ///     The data table.
        /// </param>
        public DataTableWrap(DataTable dataTable)
        {
            this.Initialize(dataTable);
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Occurs after a value has been changed for the specified <see cref="T:System.Data.DataColumn"/> in a <see cref="T:System.Data.DataRow"/>.
        /// </summary>
        public event DataColumnChangeEventHandler ColumnChanged
        {
            add
            {
                this.DataTableInstance.ColumnChanged += value;
            }

            remove
            {
                this.DataTableInstance.ColumnChanged -= value;
            }
        }

        /// <summary>
        ///     Occurs when a value is being changed for the specified <see cref="T:System.Data.DataColumn"/> in a <see cref="T:System.Data.DataRow"/>.
        /// </summary>
        public event DataColumnChangeEventHandler ColumnChanging
        {
            add
            {
                this.DataTableInstance.ColumnChanging += value;
            }

            remove
            {
                this.DataTableInstance.ColumnChanging -= value;
            }
        }

        /// <summary>
        ///     Occurs after the <see cref="T:System.Data.DataTable"/> is initialized.
        /// </summary>
        public event EventHandler Initialized
        {
            add
            {
                this.DataTableInstance.Initialized += value;
            }

            remove
            {
                this.DataTableInstance.Initialized -= value;
            }
        }

        /// <summary>
        ///     Occurs after a <see cref="T:System.Data.DataRow"/> has been changed successfully.
        /// </summary>
        public event DataRowChangeEventHandler RowChanged
        {
            add
            {
                this.DataTableInstance.RowChanged += value;
            }

            remove
            {
                this.DataTableInstance.RowChanged -= value;
            }
        }

        /// <summary>
        ///     Occurs when a <see cref="T:System.Data.DataRow"/> is changing.
        /// </summary>
        public event DataRowChangeEventHandler RowChanging
        {
            add
            {
                this.DataTableInstance.RowChanging += value;
            }

            remove
            {
                this.DataTableInstance.RowChanging -= value;
            }
        }

        /// <summary>
        ///     Occurs after a row in the table has been deleted.
        /// </summary>
        public event DataRowChangeEventHandler RowDeleted
        {
            add
            {
                this.DataTableInstance.RowDeleted += value;
            }

            remove
            {
                this.DataTableInstance.RowDeleted -= value;
            }
        }

        /// <summary>
        ///     Occurs before a row in the table is about to be deleted.
        /// </summary>
        public event DataRowChangeEventHandler RowDeleting
        {
            add
            {
                this.DataTableInstance.RowDeleting += value;
            }

            remove
            {
                this.DataTableInstance.RowDeleting -= value;
            }
        }

        /// <summary>
        ///     Occurs after a <see cref="T:System.Data.DataTable"/> is cleared.
        /// </summary>
        public event DataTableClearEventHandler TableCleared
        {
            add
            {
                this.DataTableInstance.TableCleared += value;
            }

            remove
            {
                this.DataTableInstance.TableCleared -= value;
            }
        }

        /// <summary>
        ///     Occurs when a <see cref="T:System.Data.DataTable"/> is cleared.
        /// </summary>
        public event DataTableClearEventHandler TableClearing
        {
            add
            {
                this.DataTableInstance.TableClearing += value;
            }

            remove
            {
                this.DataTableInstance.TableClearing -= value;
            }
        }

        /// <summary>
        ///     Occurs when a new <see cref="T:System.Data.DataRow"/> is inserted.
        /// </summary>
        public event DataTableNewRowEventHandler TableNewRow
        {
            add
            {
                this.DataTableInstance.TableNewRow += value;
            }

            remove
            {
                this.DataTableInstance.TableNewRow -= value;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether string comparisons within the table are case-sensitive.
        /// </summary>
        /// <returns>
        ///     true if the comparison is case-sensitive; otherwise false. The default is set to the parent <see cref="T:System.Data.DataSet"/> 
        ///     object's <see cref="P:System.Data.DataSet.CaseSensitive"/> property, or false if the <see cref="T:System.Data.DataTable"/> was 
        ///     created independently of a <see cref="T:System.Data.DataSet"/>.
        /// </returns>
        public bool CaseSensitive
        {
            get
            {
                return this.DataTableInstance.CaseSensitive;
            }

            set
            {
                this.DataTableInstance.CaseSensitive = value;
            }
        }

        /// <summary>
        ///     Gets the collection of child relations for this <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRelationCollection"/> that contains the child relations for the table. An empty collection is 
        ///     returned if no <see cref="T:System.Data.DataRelation"/> objects exist.
        /// </returns>
        public DataRelationCollection ChildRelations
        {
            get
            {
                return this.DataTableInstance.ChildRelations;
            }
        }

        /// <summary>
        ///     Gets the collection of columns that belong to this table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataColumnCollection"/> that contains the collection of <see cref="T:System.Data.DataColumn"/> 
        ///     objects for the table. An empty collection is returned if no <see cref="T:System.Data.DataColumn"/> objects exist.
        /// </returns>
        public DataColumnCollection Columns
        {
            get
            {
                return this.DataTableInstance.Columns;
            }
        }

        /// <summary>
        ///     Gets the collection of constraints maintained by this table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.ConstraintCollection"/> that contains the collection of <see cref="T:System.Data.Constraint"/> 
        ///     objects for the table. An empty collection is returned if no <see cref="T:System.Data.Constraint"/> objects exist.
        /// </returns>
        public ConstraintCollection Constraints
        {
            get
            {
                return this.DataTableInstance.Constraints;
            }
        }

        /// <summary>
        ///     Gets the <see cref="T:System.Data.DataSet"/> to which this table belongs.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.DataSet"/> to which this table belongs.
        /// </returns>
        public DataSet DataSet
        {
            get
            {
                return this.DataTableInstance.DataSet;
            }
        }

        /// <summary>
        ///     Gets the data table instance.
        /// </summary>
        public DataTable DataTableInstance { get; private set; }

        /// <summary>
        ///     Gets a customized view of the table that may include a filtered view, or a cursor position.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.DataView"/> associated with the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public DataView DefaultView
        {
            get
            {
                return this.DataTableInstance.DefaultView;
            }
        }

        /// <summary>
        ///     Gets or sets the expression that returns a value used to represent this table in the user interface. The DisplayExpression property 
        ///     lets you display the name of this table in a user interface.
        /// </summary>
        /// <returns>
        ///     A display string.
        /// </returns>
        public string DisplayExpression
        {
            get
            {
                return this.DataTableInstance.DisplayExpression;
            }

            set
            {
                this.DataTableInstance.DisplayExpression = value;
            }
        }

        /// <summary>
        ///     Gets the collection of customized user information.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.PropertyCollection"/> that contains custom user information.
        /// </returns>
        public PropertyCollection ExtendedProperties
        {
            get
            {
                return this.DataTableInstance.ExtendedProperties;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether there are errors in any of the rows in any of the tables of the <see cref="T:System.Data.DataSet"/> to which the table belongs.
        /// </summary>
        /// <returns>
        ///     true if errors exist; otherwise false.
        /// </returns>
        public bool HasErrors
        {
            get
            {
                return this.DataTableInstance.HasErrors;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Data.DataTable"/> is initialized.
        /// </summary>
        /// <returns>
        ///     true to indicate the component has completed initialization; otherwise false.
        /// </returns>
        public bool IsInitialized
        {
            get
            {
                return this.DataTableInstance.IsInitialized;
            }
        }

        /// <summary>
        ///     Gets or sets the locale information used to compare strings within the table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Globalization.CultureInfo"/> that contains data about the user's machine locale. The default is the 
        ///     <see cref="T:System.Data.DataSet"/> object's <see cref="T:System.Globalization.CultureInfo"/> (returned by the 
        ///     <see cref="P:System.Data.DataSet.Locale"/> property) to which the <see cref="T:System.Data.DataTable"/> belongs; if the table 
        ///     doesn't belong to a <see cref="T:System.Data.DataSet"/>, the default is the current system <see cref="T:System.Globalization.CultureInfo"/>.
        /// </returns>
        public CultureInfo Locale
        {
            get
            {
                return this.DataTableInstance.Locale;
            }

            set
            {
                this.DataTableInstance.Locale = value;
            }
        }

        /// <summary>
        ///     Gets or sets the initial starting size for this table.
        /// </summary>
        /// <returns>
        ///     The initial starting size in rows of this table. The default is 50.
        /// </returns>
        public int MinimumCapacity
        {
            get
            {
                return this.DataTableInstance.MinimumCapacity;
            }

            set
            {
                this.DataTableInstance.MinimumCapacity = value;
            }
        }

        /// <summary>
        ///     Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     The namespace of the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public string Namespace
        {
            get
            {
                return this.DataTableInstance.Namespace;
            }

            set
            {
                this.DataTableInstance.Namespace = value;
            }
        }

        /// <summary>
        ///     Gets the collection of parent relations for this <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRelationCollection"/> that contains the parent relations for the table. An empty collection is returned 
        ///     if no <see cref="T:System.Data.DataRelation"/> objects exist.
        /// </returns>
        public DataRelationCollection ParentRelations
        {
            get
            {
                return this.DataTableInstance.ParentRelations;
            }
        }

        /// <summary>
        ///     Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     The prefix of the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public string Prefix
        {
            get
            {
                return this.DataTableInstance.Prefix;
            }

            set
            {
                this.DataTableInstance.Prefix = value;
            }
        }

        /// <summary>
        ///     Gets or sets an array of columns that function as primary keys for the data table.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataColumn"/> objects.
        /// </returns>
        /// <exception cref="T:System.Data.DataException">
        ///     The key is a foreign key.
        /// </exception>
        public DataColumn[] PrimaryKey
        {
            get
            {
                return this.DataTableInstance.PrimaryKey;
            }

            set
            {
                this.DataTableInstance.PrimaryKey = value;
            }
        }

        /// <summary>
        ///     Gets or sets the serialization format.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.SerializationFormat"/> enumeration specifying either Binary or Xml serialization.
        /// </returns>
        public SerializationFormat RemotingFormat
        {
            get
            {
                return this.DataTableInstance.RemotingFormat;
            }

            set
            {
                this.DataTableInstance.RemotingFormat = value;
            }
        }

        /// <summary>
        ///     Gets the collection of rows that belong to this table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRowCollection"/> that contains <see cref="T:System.Data.DataRow"/> objects; otherwise a null value if 
        ///     no <see cref="T:System.Data.DataRow"/> objects exist.
        /// </returns>
        public DataRowCollection Rows
        {
            get
            {
                return this.DataTableInstance.Rows;
            }
        }

        /// <summary>
        ///     Gets or sets an <see cref="T:System.ComponentModel.ISite"/> for the <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.ComponentModel.ISite"/> for the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public ISite Site
        {
            get
            {
                return this.DataTableInstance.Site;
            }

            set
            {
                this.DataTableInstance.Site = value;
            }
        }

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
        public string TableName
        {
            get
            {
                return this.DataTableInstance.TableName;
            }

            set
            {
                this.DataTableInstance.TableName = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Commits all the changes made to this table since the last time <see cref="M:System.Data.DataTable.AcceptChanges"/> was called.
        /// </summary>
        public void AcceptChanges()
        {
            this.DataTableInstance.AcceptChanges();
        }

        /// <summary>
        ///     Begins the initialization of a <see cref="T:System.Data.DataTable"/> that is used on a form or used by another component. 
        ///     The initialization occurs at run time.
        /// </summary>
        public void BeginInit()
        {
            this.DataTableInstance.BeginInit();
        }

        /// <summary>
        ///     Turns off notifications, index maintenance, and constraints while loading data.
        /// </summary>
        public void BeginLoadData()
        {
            this.DataTableInstance.BeginLoadData();
        }

        /// <summary>
        ///     Clears the <see cref="T:System.Data.DataTable"/> of all data.
        /// </summary>
        public void Clear()
        {
            this.DataTableInstance.Clear();
        }

        /// <summary>
        ///     Clones the structure of the <see cref="T:System.Data.DataTable"/>, including all <see cref="T:System.Data.DataTable"/> schemas and constraints.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.DataTable"/> with the same schema as the current <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public IDataTable Clone()
        {
            return new DataTableWrap(this.DataTableInstance.Clone());
        }

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
        public object Compute(string expression,
                              string filter)
        {
            return this.DataTableInstance.Compute(expression, filter);
        }

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
        public IDataTable Copy()
        {
            return new DataTableWrap(this.DataTableInstance.Copy());
        }

        /// <summary>
        ///     Returns a <see cref="T:System.Data.DataTableReader"/> corresponding to the data within this <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataTableReader"/> containing one result set, corresponding to the source <see cref="T:System.Data.DataTable"/> 
        ///     instance.
        /// </returns>
        public DataTableReader CreateDataReader()
        {
            return this.DataTableInstance.CreateDataReader();
        }

        /// <summary>
        ///     Ends the initialization of a <see cref="T:System.Data.DataTable"/> that is used on a form or used by another component. The initialization 
        ///     occurs at run time.
        /// </summary>
        public void EndInit()
        {
            this.DataTableInstance.EndInit();
        }

        /// <summary>
        ///     Turns on notifications, index maintenance, and constraints after loading data.
        /// </summary>
        public void EndLoadData()
        {
            this.DataTableInstance.EndLoadData();
        }

        /// <summary>
        ///     Gets a copy of the <see cref="T:System.Data.DataTable"/> that contains all changes made to it since it was loaded or 
        ///     <see cref="M:System.Data.DataTable.AcceptChanges"/> was last called.
        /// </summary>
        /// <returns>
        ///     A copy of the changes from this <see cref="T:System.Data.DataTable"/>, or null if no changes are found.
        /// </returns>
        public IDataTable GetChanges()
        {
            return new DataTableWrap(this.DataTableInstance.GetChanges());
        }

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
        public IDataTable GetChanges(DataRowState rowStates)
        {
            return new DataTableWrap(this.DataTableInstance.GetChanges(rowStates));
        }

        /// <summary>
        ///     Gets an array of <see cref="T:System.Data.DataRow"/> objects that contain errors.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects that have errors.
        /// </returns>
        public DataRow[] GetErrors()
        {
            return this.DataTableInstance.GetErrors();
        }

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
        public void GetObjectData(SerializationInfo info,
                                  StreamingContext context)
        {
            this.DataTableInstance.GetObjectData(info, context);
        }

        /// <summary>
        ///     Copies a <see cref="T:System.Data.DataRow"/> into a <see cref="T:System.Data.DataTable"/>, preserving any property settings, as well as original and current values.
        /// </summary>
        /// <param name="row">
        ///     The <see cref="T:System.Data.DataRow"/> to be imported.
        /// </param>
        public void ImportRow(DataRow row)
        {
            this.DataTableInstance.ImportRow(row);
        }

        /// <summary>
        ///     Fills a <see cref="T:System.Data.DataTable"/> with values from a data source using the supplied <see cref="T:System.Data.IDataReader"/>. 
        ///     If the <see cref="T:System.Data.DataTable"/> already contains rows, the incoming data from the data source is merged with the existing rows.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="T:System.Data.IDataReader"/> that provides a result set.
        /// </param>
        public void Load(IDataReader reader)
        {
            this.DataTableInstance.Load(reader);
        }

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
        public void Load(IDataReader reader,
                         LoadOption loadOption)
        {
            this.DataTableInstance.Load(reader, loadOption);
        }

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
        public void Load(IDataReader reader,
                         LoadOption loadOption,
                         FillErrorEventHandler errorHandler)
        {
            this.DataTableInstance.Load(reader, loadOption, errorHandler);
        }

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
        public DataRow LoadDataRow(object[] values,
                                   bool acceptChanges)
        {
            return this.DataTableInstance.LoadDataRow(values, acceptChanges);
        }

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
        public DataRow LoadDataRow(object[] values,
                                   LoadOption loadOption)
        {
            return this.DataTableInstance.LoadDataRow(values, loadOption);
        }

        /// <summary>
        ///     Merge the specified <see cref="T:System.Data.DataTable"/> with the current <see cref="T:System.Data.DataTable"/>.
        /// </summary>
        /// <param name="table">
        ///     The <see cref="T:System.Data.DataTable"/> to be merged with the current <see cref="T:System.Data.DataTable"/>.
        /// </param>
        public void Merge(DataTable table)
        {
            this.DataTableInstance.Merge(table);
        }

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
        public void Merge(DataTable table,
                          bool preserveChanges)
        {
            this.DataTableInstance.Merge(table, preserveChanges);
        }

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
        public void Merge(DataTable table,
                          bool preserveChanges,
                          MissingSchemaAction missingSchemaAction)
        {
            this.DataTableInstance.Merge(table, preserveChanges, missingSchemaAction);
        }

        /// <summary>
        ///     Creates a new <see cref="T:System.Data.DataRow"/> with the same schema as the table.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.DataRow"/> with the same schema as the <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public DataRow NewRow()
        {
            return this.DataTableInstance.NewRow();
        }

        /// <summary>
        ///     Reads XML schema and data into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="stream">
        ///     An object that derives from <see cref="T:System.IO.Stream"/>
        /// </param>
        public XmlReadMode ReadXml(Stream stream)
        {
            return this.DataTableInstance.ReadXml(stream);
        }

        /// <summary>
        ///     Reads XML schema and data into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.IO.TextReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="reader">
        ///     The <see cref="T:System.IO.TextReader"/> that will be used to read the data.
        /// </param>
        public XmlReadMode ReadXml(TextReader reader)
        {
            return this.DataTableInstance.ReadXml(reader);
        }

        /// <summary>
        ///     Reads XML schema and data into the <see cref="T:System.Data.DataTable"/> from the specified file.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="fileName">
        ///     The name of the file from which to read the data.
        /// </param>
        public XmlReadMode ReadXml(string fileName)
        {
            return this.DataTableInstance.ReadXml(fileName);
        }

        /// <summary>
        ///     Reads XML Schema and Data into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.Xml.XmlReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.XmlReadMode"/> used to read the data.
        /// </returns>
        /// <param name="reader">
        ///     The <see cref="T:System.Xml.XmlReader"/> that will be used to read the data.
        /// </param>
        public XmlReadMode ReadXml(XmlReader reader)
        {
            return this.DataTableInstance.ReadXml(reader);
        }

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> using the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream used to read the schema.
        /// </param>
        public void ReadXmlSchema(Stream stream)
        {
            this.DataTableInstance.ReadXmlSchema(stream);
        }

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.IO.TextReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="T:System.IO.TextReader"/> used to read the schema information.
        /// </param>
        public void ReadXmlSchema(TextReader reader)
        {
            this.DataTableInstance.ReadXmlSchema(reader);
        }

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> from the specified file.
        /// </summary>
        /// <param name="fileName">
        ///     The name of the file from which to read the schema information.
        /// </param>
        public void ReadXmlSchema(string fileName)
        {
            this.DataTableInstance.ReadXmlSchema(fileName);
        }

        /// <summary>
        ///     Reads an XML schema into the <see cref="T:System.Data.DataTable"/> using the specified <see cref="T:System.Xml.XmlReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="T:System.Xml.XmlReader"/> used to read the schema information.
        /// </param>
        public void ReadXmlSchema(XmlReader reader)
        {
            this.DataTableInstance.ReadXmlSchema(reader);
        }

        /// <summary>
        ///     Rolls back all changes that have been made to the table since it was loaded, or the last time 
        ///     <see cref="M:System.Data.DataTable.AcceptChanges"/> was called.
        /// </summary>
        public void RejectChanges()
        {
            this.DataTableInstance.RejectChanges();
        }

        /// <summary>
        ///     Resets the <see cref="T:System.Data.DataTable"/> to its original state. Reset removes all data, indexes, relations, and columns of the table. 
        ///     If a DataSet includes a DataTable, the table will still be part of the DataSet after the table is reset.
        /// </summary>
        public void Reset()
        {
            this.DataTableInstance.Reset();
        }

        /// <summary>
        ///     Gets an array of all <see cref="T:System.Data.DataRow"/> objects.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects.
        /// </returns>
        public DataRow[] Select()
        {
            return this.DataTableInstance.Select();
        }

        /// <summary>
        ///     Gets an array of all <see cref="T:System.Data.DataRow"/> objects that match the filter criteria.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Data.DataRow"/> objects.
        /// </returns>
        /// <param name="filterExpression">
        ///     The criteria to use to filter the rows. For examples on how to filter rows, see DataView RowFilter Syntax [C#].
        /// </param>
        public DataRow[] Select(string filterExpression)
        {
            return this.DataTableInstance.Select(filterExpression);
        }

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
        public DataRow[] Select(string filterExpression,
                                string sort)
        {
            return this.DataTableInstance.Select(filterExpression, sort);
        }

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
        public DataRow[] Select(string filterExpression,
                                string sort,
                                DataViewRowState recordStates)
        {
            return this.DataTableInstance.Select(filterExpression, sort, recordStates);
        }

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the data will be written.
        /// </param>
        public void WriteXml(Stream stream)
        {
            this.DataTableInstance.WriteXml(stream);
        }

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
        public void WriteXml(Stream stream,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(stream, writeHierarchy);
        }

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> with which to write the content.
        /// </param>
        public void WriteXml(TextWriter writer)
        {
            this.DataTableInstance.WriteXml(writer);
        }

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
        public void WriteXml(TextWriter writer,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(writer, writeHierarchy);
        }

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> with which to write the contents.
        /// </param>
        public void WriteXml(XmlWriter writer)
        {
            this.DataTableInstance.WriteXml(writer);
        }

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> with which to write the contents.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        public void WriteXml(XmlWriter writer,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(writer, writeHierarchy);
        }

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified file.
        /// </summary>
        /// <param name="fileName">
        ///     The file to which to write the XML data.
        /// </param>
        public void WriteXml(string fileName)
        {
            this.DataTableInstance.WriteXml(fileName);
        }

        /// <summary>
        ///     Writes the current contents of the <see cref="T:System.Data.DataTable"/> as XML using the specified file. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy"/> parameter to true.
        /// </summary>
        /// <param name="fileName">
        ///     The file to which to write the XML data.
        /// </param>
        /// <param name="writeHierarchy">
        ///     If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.
        /// </param>
        public void WriteXml(string fileName,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(fileName, writeHierarchy);
        }

        /// <summary>
        ///     Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable"/> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode"/>. To write the schema, set the value for the <paramref name="mode"/> parameter to WriteSchema.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the data will be written.
        /// </param>
        /// <param name="mode">
        ///     One of the <see cref="T:System.Data.XmlWriteMode"/> values.
        /// </param>
        public void WriteXml(Stream stream,
                             XmlWriteMode mode)
        {
            this.DataTableInstance.WriteXml(stream, mode);
        }

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
        public void WriteXml(Stream stream,
                             XmlWriteMode mode,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(stream, mode, writeHierarchy);
        }

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
        public void WriteXml(TextWriter writer,
                             XmlWriteMode mode)
        {
            this.DataTableInstance.WriteXml(writer, mode);
        }

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
        public void WriteXml(TextWriter writer,
                             XmlWriteMode mode,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(writer, mode, writeHierarchy);
        }

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
        public void WriteXml(XmlWriter writer,
                             XmlWriteMode mode)
        {
            this.DataTableInstance.WriteXml(writer, mode);
        }

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
        public void WriteXml(XmlWriter writer,
                             XmlWriteMode mode,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(writer, mode, writeHierarchy);
        }

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
        public void WriteXml(string fileName,
                             XmlWriteMode mode)
        {
            this.DataTableInstance.WriteXml(fileName, mode);
        }

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
        public void WriteXml(string fileName,
                             XmlWriteMode mode,
                             bool writeHierarchy)
        {
            this.DataTableInstance.WriteXml(fileName, mode, writeHierarchy);
        }

        /// <summary>
        ///     Writes the current data structure of the <see cref="T:System.Data.DataTable"/> as an XML schema to the specified stream.
        /// </summary>
        /// <param name="stream">
        ///     The stream to which the XML schema will be written.
        /// </param>
        public void WriteXmlSchema(Stream stream)
        {
            this.DataTableInstance.WriteXmlSchema(stream);
        }

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
        public void WriteXmlSchema(Stream stream,
                                   bool writeHierarchy)
        {
            this.DataTableInstance.WriteXmlSchema(stream, writeHierarchy);
        }

        /// <summary>
        ///     Writes the current data structure of the <see cref="T:System.Data.DataTable"/> as an XML schema using the specified 
        ///     <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.IO.TextWriter"/> with which to write.
        /// </param>
        public void WriteXmlSchema(TextWriter writer)
        {
            this.DataTableInstance.WriteXmlSchema(writer);
        }

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
        public void WriteXmlSchema(TextWriter writer,
                                   bool writeHierarchy)
        {
            this.DataTableInstance.WriteXmlSchema(writer, writeHierarchy);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            this.DataTableInstance = new DataTable();
        }

        private void Initialize(string tableName)
        {
            this.DataTableInstance = new DataTable(tableName);
        }

        private void Initialize(string tableName,
                                string tableNamespace)
        {
            this.DataTableInstance = new DataTable(tableName, tableNamespace);
        }

        private void Initialize(DataTable dataTable)
        {
            this.DataTableInstance = dataTable;
        }

        #endregion
    }
}