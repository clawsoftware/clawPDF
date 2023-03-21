namespace SystemInterface.ActiveDirectory
{
    using System.Collections;

    /// <summary>
    /// 
    /// </summary>
    public interface IResultPropertyValueCollection : ICollection
    {
        #region Public Indexers

        /// <summary>
        /// The System.DirectoryServices.ResultPropertyValueCollectionWrap.this[System.Int32]
        /// property gets the property value that is located at a specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the property value to retrieve.
        /// </param>
        object this[int index] { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The System.DirectoryServices.ResultPropertyValueCollectionWrap.Contains(System.Object)
        /// method determines whether a specified property value is in this collection.
        /// </summary>
        /// <param name="value">
        /// The property value to find.
        /// </param>
        /// <returns>
        /// The return value is true if the specified property belongs to this collection;
        /// otherwise, false.
        /// </returns>
        bool Contains(object value);

        /// <summary>
        /// The System.DirectoryServices.ResultPropertyValueCollectionWrap.CopyTo(System.Object[],System.Int32)
        /// method copies the property values from this collection to an array, starting
        /// at a particular index of the array.
        /// </summary>
        /// <param name="values">
        /// An array of type System.Object that receives this collection's property values.
        /// </param>
        /// <param name="index">
        /// The zero-based array index at which to begin copying the property values.
        /// </param>
        void CopyTo(object[] values,
                    int index);

        /// <summary>
        /// The System.DirectoryServices.ResultPropertyValueCollectionWrap.IndexOf(System.Object)
        /// method retrieves the index of a specified property value in this collection.
        /// </summary>
        /// <param name="value">
        /// The property value to find.
        /// </param>
        /// <returns>
        /// The zero-based index of the specified property value. If the object is not
        /// found, the return value is -1.
        /// </returns>
        int IndexOf(object value);

        #endregion
    }
}