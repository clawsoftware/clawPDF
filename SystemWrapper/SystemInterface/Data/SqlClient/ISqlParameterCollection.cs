namespace SystemInterface.Data.SqlClient
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    ///     Wrapper for <see cref="SqlParameterCollection"/> class.
    /// </summary>
    public interface ISqlParameterCollection
    {
        #region Public Properties

        /// <summary>
        ///     Returns an Integer that contains the number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>. Read-only.
        /// </summary>
        /// <returns>
        ///     The number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> as an Integer.
        /// </returns>
        int Count { get; }

        /// <summary>
        ///     Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> has a fixed size.
        /// </summary>
        /// <returns>
        ///     Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> has a fixed size; otherwise false.
        /// </returns>
        bool IsFixedSize { get; }

        /// <summary>
        ///     Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> is read-only.
        /// </summary>
        /// <returns>
        ///     Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> is read only; otherwise false.
        /// </returns>
        bool IsReadOnly { get; }

        /// <summary>
        ///     Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> is synchronized.
        /// </summary>
        /// <returns>
        ///     Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> is synchronized; otherwise false.
        /// </returns>
        bool IsSynchronized { get; }

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     An object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </returns>
        object SyncRoot { get; }

        #endregion

        #region Public Indexers

        /// <summary>
        ///     Gets the <see cref="T:System.Data.SqlClient.SqlParameter"/> with the specified name.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Data.SqlClient.SqlParameter"/> with the specified name.
        /// </returns>
        /// <param name="parameterName">
        ///     The name of the parameter to retrieve.
        /// </param>
        /// <exception cref="T:System.IndexOutOfRangeException">
        ///     The specified <paramref name="parameterName"/> is not valid.
        /// </exception>
        SqlParameter this[string parameterName] { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="T:System.Data.SqlClient.SqlParameter"/> with the specified index.
        /// </summary>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <returns>
        ///     The <see cref="SqlParameter"/>.
        /// </returns>
        SqlParameter this[int index] { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.SqlClient.SqlParameter"/> object.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="T:System.Data.SqlClient.SqlParameter"/> to add to the collection.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="T:System.Data.SqlClient.SqlParameter"/> specified in the <paramref name="value"/> parameter is already added to this or another 
        ///     <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </exception>
        /// <exception cref="T:System.InvalidCastException">
        ///     The parameter passed was not a <see cref="T:System.Data.SqlClient.SqlParameter"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <paramref name="value"/> parameter is null.
        /// </exception>
        SqlParameter Add(SqlParameter value);

        /// <summary>
        ///     Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     The index of the new <see cref="T:System.Data.SqlClient.SqlParameter"/> object.
        /// </returns>
        /// <param name="value">
        ///     An <see cref="T:System.Object"/>.
        /// </param>
        int Add(object value);

        /// <summary>
        ///     Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.SqlClient.SqlParameter"/> object.Use caution when you are using this overload of the SqlParameterCollection.Add method to specify integer parameter values. Because this overload takes a <paramref name="value"/> of type <see cref="T:System.Object"/>, you must convert the integral value to an <see cref="T:System.Object"/> type when the value is zero, as the following C# example demonstrates. Copy Codeparameters.Add("@pname", Convert.ToInt32(0));If you do not perform this conversion, the compiler assumes that you are trying to call the SqlParameterCollection.Add (string, SqlDbType) overload.
        /// </returns>
        /// <param name="parameterName">
        ///     The name of the <see cref="T:System.Data.SqlClient.SqlParameter"/> to add to the collection.
        /// </param>
        /// <param name="value">
        ///     A <see cref="T:System.Object"/>.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     The <see cref="T:System.Data.SqlClient.SqlParameter"/> specified in the <paramref name="value"/> parameter is already added to this or another 
        ///     <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>. 
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     The <paramref name="value"/> parameter is null.
        /// </exception>
        SqlParameter Add(string parameterName,
                         object value);

        /// <summary>
        ///     Adds a <see cref="T:System.Data.SqlClient.SqlParameter"/> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> given the 
        ///     parameter name and the data type.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.SqlClient.SqlParameter"/> object.
        /// </returns>
        /// <param name="parameterName">
        ///     The name of the parameter. </param><param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType"/> values. 
        /// </param>
        SqlParameter Add(string parameterName,
                         SqlDbType sqlDbType);

        /// <summary>
        ///     Adds a <see cref="T:System.Data.SqlClient.SqlParameter"/> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>, given the 
        ///     specified parameter name, <see cref="T:System.Data.SqlDbType"/> and size.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.SqlClient.SqlParameter"/> object.
        /// </returns>
        /// <param name="parameterName">
        ///     The name of the parameter.
        /// </param>
        /// <param name="sqlDbType">
        ///     The <see cref="T:System.Data.SqlDbType"/> of the <see cref="T:System.Data.SqlClient.SqlParameter"/> to add to the collection.
        /// </param>
        /// <param name="size">
        ///     The size as an <see cref="T:System.Int32"/>.
        /// </param>
        SqlParameter Add(string parameterName,
                         SqlDbType sqlDbType,
                         int size);

        /// <summary>
        ///     Adds a <see cref="T:System.Data.SqlClient.SqlParameter"/> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> with the 
        ///     parameter name, the data type, and the column length.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:System.Data.SqlClient.SqlParameter"/> object.
        /// </returns>
        /// <param name="parameterName">
        ///     The name of the parameter. </param><param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType"/> values.
        /// </param>
        /// <param name="size">
        ///     The column length.</param><param name="sourceColumn">The name of the source column 
        ///     (<see cref="P:System.Data.SqlClient.SqlParameter.SourceColumn"/>) if this <see cref="T:System.Data.SqlClient.SqlParameter"/> is used in a call 
        ///     to <see cref="Overload:System.Data.Common.DbDataAdapter.Update"/>.
        /// </param>
        SqlParameter Add(string parameterName,
                         SqlDbType sqlDbType,
                         int size,
                         string sourceColumn);

        /// <summary>
        ///     Adds an array of <see cref="T:System.Data.SqlClient.SqlParameter"/> values to the end of the 
        ///     <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <param name="values">
        ///     The <see cref="T:System.Data.SqlClient.SqlParameter"/> values to add.
        /// </param>
        void AddRange(SqlParameter[] values);

        /// <summary>
        ///     Adds an array of values to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <param name="values">
        ///     The <see cref="T:System.Array"/> values to add.
        /// </param>
        void AddRange(Array values);

        /// <summary>
        ///     Adds a value to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Data.SqlClient.SqlParameter"/> object.
        /// </returns>
        /// <param name="parameterName">
        ///     The name of the parameter.</param><param name="value">The value to be added. Use <see cref="F:System.DBNull.Value"/> instead of null, 
        ///     to indicate a null value.
        /// </param>
        SqlParameter AddWithValue(string parameterName,
                                  object value);

        /// <summary>
        ///     Removes all the <see cref="T:System.Data.SqlClient.SqlParameter"/> objects from the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Determines whether the specified parameter name is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> contains the value; otherwise false.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="T:System.String"/> value.
        /// </param>
        bool Contains(string value);

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> contains the value; otherwise false.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="T:System.Data.SqlClient.SqlParameter"/> value.
        /// </param>
        bool Contains(SqlParameter value);

        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Object"/> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> contains the value; otherwise false.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="T:System.Object"/> value.
        /// </param>
        bool Contains(object value);

        /// <summary>
        ///     Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> to the specified <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> starting at the specified destination index.
        /// </summary>
        /// <param name="array">
        ///     The <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> that is the destination of the elements copied from the current 
        ///     <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </param>
        /// <param name="index">
        ///     A 32-bit integer that represents the index in the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> at which copying starts.
        /// </param>
        void CopyTo(SqlParameter[] array,
                    int index);

        /// <summary>
        ///     Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> to the specified one-dimensional 
        ///     <see cref="T:System.Array"/> starting at the specified destination <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from the current 
        ///     <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </param>
        /// <param name="index">
        ///     A 32-bit integer that represents the index in the <see cref="T:System.Array"/> at which copying starts.
        /// </param>
        void CopyTo(Array array,
                    int index);

        /// <summary>
        ///     Returns an enumerator that iterates through the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"/> for the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </returns>
        IEnumerator GetEnumerator();

        /// <summary>
        ///     Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> within the collection.
        /// </summary>
        /// <returns>
        ///     The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> that is a 
        ///     <see cref="T:System.Data.SqlClient.SqlParameter"/> within the collection. Returns -1 when the object does not exist in the 
        ///     <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="T:System.Data.SqlClient.SqlParameter"/> to find.
        /// </param>
        int IndexOf(SqlParameter value);

        /// <summary>
        ///     Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> with the specified name.
        /// </summary>
        /// <returns>
        ///     The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> with the specified case-sensitive name. 
        ///     Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </returns>
        /// <param name="parameterName">
        ///     The case-sensitive name of the <see cref="T:System.Data.SqlClient.SqlParameter"/> to find.
        /// </param>
        int IndexOf(string parameterName);

        /// <summary>
        ///     Gets the location of the specified <see cref="T:System.Object"/> within the collection.
        /// </summary>
        /// <returns>
        ///     The zero-based location of the specified <see cref="T:System.Object"/> that is a <see cref="T:System.Data.SqlClient.SqlParameter"/> within 
        ///     the collection. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </returns>
        /// <param name="value">
        ///     The <see cref="T:System.Object"/> to find.
        /// </param>
        int IndexOf(object value);

        /// <summary>
        ///     Inserts a <see cref="T:System.Data.SqlClient.SqlParameter"/> object into the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> 
        ///     at the specified index.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index at which value should be inserted.</param><param name="value">A <see cref="T:System.Data.SqlClient.SqlParameter"/> object 
        ///     to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </param>
        void Insert(int index,
                    SqlParameter value);

        /// <summary>
        ///     Inserts an <see cref="T:System.Object"/> into the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> at the specified index.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index at which value should be inserted.
        /// </param>
        /// <param name="value">
        ///     An <see cref="T:System.Object"/> to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/>.
        /// </param>
        void Insert(int index,
                    object value);

        /// <summary>
        ///     Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> from the collection.
        /// </summary>
        /// <param name="value">
        ///     A <see cref="T:System.Data.SqlClient.SqlParameter"/> object to remove from the collection.
        /// </param>
        /// <exception cref="T:System.InvalidCastException">
        ///     The parameter is not a <see cref="T:System.Data.SqlClient.SqlParameter"/>.
        /// </exception>
        /// <exception cref="T:System.SystemException">
        ///     The parameter does not exist in the collection.
        /// </exception>
        void Remove(SqlParameter value);

        /// <summary>
        ///     Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter"/> from the collection.
        /// </summary>
        /// <param name="value">
        ///     The object to remove from the collection.
        /// </param>
        void Remove(object value);

        /// <summary>
        ///     Removes the <see cref="T:System.Data.SqlClient.SqlParameter"/> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> 
        ///     at the specified index.
        /// </summary>
        /// <param name="index">
        ///     The zero-based index of the <see cref="T:System.Data.SqlClient.SqlParameter"/> object to remove.
        /// </param>
        void RemoveAt(int index);

        /// <summary>
        ///     Removes the <see cref="T:System.Data.SqlClient.SqlParameter"/> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection"/> 
        ///     at the specified parameter name.
        /// </summary>
        /// <param name="parameterName">
        ///     The name of the <see cref="T:System.Data.SqlClient.SqlParameter"/> to remove.
        /// </param>
        void RemoveAt(string parameterName);

        #endregion
    }
}