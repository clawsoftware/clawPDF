namespace SystemInterface.Security.Certificate
{
    using System;
    using System.Collections;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interface wrapping a X509ChainElementCollection.
    /// </summary>
    public interface IX509ChainElementCollection
    {
        #region Public Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        int Count { get; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets the IPamsX509ChainElement object at the specified index.
        /// </summary>
        /// <param name="index">
        /// An integer value.
        /// </param>
        /// <returns>
        /// The <see cref="IX509ChainElement"/>.
        /// </returns>
        IX509ChainElement this[int index] { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        void CopyTo(Array array,
                    int index);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator GetEnumerator();

        /// <summary>
        /// Initializes a new instance of the <see cref="IX509ChainElementCollection"/> class.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        void Initialize(X509ChainElementCollection collection);

        #endregion
    }
}