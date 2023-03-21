namespace SystemWrapper.ActiveDirectory
{
    using System;
    using System.Collections;
    using System.DirectoryServices;

    using SystemInterface.ActiveDirectory;

    /// <summary>
    /// 
    /// </summary>
    public class ResultPropertyCollectionWrap : IResultPropertyCollection
    {
        #region Fields

        private readonly ResultPropertyCollection resultPropertyCollection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultPropertyCollection">
        /// </param>
        public ResultPropertyCollectionWrap(ResultPropertyCollection resultPropertyCollection)
        {
            this.resultPropertyCollection = resultPropertyCollection;
            this.PropertyNames = this.resultPropertyCollection.PropertyNames;
            this.Values = this.resultPropertyCollection.Values;
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
                return this.resultPropertyCollection.Count;
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
        /// Gets the names of the properties in this collection.
        /// </summary>
        public ICollection PropertyNames { get; private set; }

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

        /// <summary>
        /// Gets the values of the properties in this collection.
        /// </summary>
        public ICollection Values { get; private set; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Determines whether the property that has the specified name belongs to this
        /// collection.
        /// </summary>
        /// <param name="name">
        /// The name of the property to get.
        /// </param>
        public IResultPropertyValueCollection this[string name]
        {
            get
            {
                return new ResultPropertyValueCollectionWrap(this.resultPropertyCollection[name]);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines whether the property that has the specified name belongs to this
        /// collection.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to find.
        /// </param>
        /// <returns>
        /// The return value is true if the specified property belongs to this collection;
        /// otherwise, false.
        /// </returns>
        public bool Contains(string propertyName)
        {
            return this.resultPropertyCollection.Contains(propertyName);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing. </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins. </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>. </exception><exception cref="T:System.ArgumentException">The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>. </exception>
        public void CopyTo(Array array,
                           int index)
        {
            this.resultPropertyCollection.CopyTo(array, index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.resultPropertyCollection.GetEnumerator();
        }

        #endregion
    }
}