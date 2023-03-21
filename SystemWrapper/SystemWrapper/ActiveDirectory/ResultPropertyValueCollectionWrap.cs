namespace SystemWrapper.ActiveDirectory
{
    using System;
    using System.Collections;
    using System.DirectoryServices;
    using System.Linq;

    using SystemInterface.ActiveDirectory;

    /// <summary>
    /// 
    /// </summary>
    public class ResultPropertyValueCollectionWrap : IResultPropertyValueCollection
    {
        #region Fields

        private readonly ResultPropertyValueCollection resultPropertyValueCollection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultPropertyValueCollection">
        /// </param>
        public ResultPropertyValueCollectionWrap(ResultPropertyValueCollection resultPropertyValueCollection)
        {
            this.resultPropertyValueCollection = resultPropertyValueCollection;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public int Count
        {
            get
            {
                return this.resultPropertyValueCollection.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <returns>
        /// true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
        /// </returns>
        public bool IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// The System.DirectoryServices.ResultPropertyValueCollectionWrap.this[System.Int32]
        /// property gets the property value that is located at a specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the property value to retrieve.
        /// </param>
        public object this[int index]
        {
            get
            {
                return this.resultPropertyValueCollection[index];
            }
        }

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
        public bool Contains(object value)
        {
            return this.resultPropertyValueCollection.Contains(value);
        }

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
        public void CopyTo(object[] values,
                           int index)
        {
            this.resultPropertyValueCollection.CopyTo(values, index);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing. </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins. </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>. </exception><exception cref="T:System.ArgumentException">The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>. </exception>
        public void CopyTo(Array array,
                           int index)
        {
            this.resultPropertyValueCollection.CopyTo(array.Cast<object>().ToArray(), index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.resultPropertyValueCollection.GetEnumerator();
        }

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
        public int IndexOf(object value)
        {
            return this.resultPropertyValueCollection.IndexOf(value);
        }

        #endregion
    }
}