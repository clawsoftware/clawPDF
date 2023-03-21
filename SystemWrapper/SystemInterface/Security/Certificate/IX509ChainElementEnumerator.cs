namespace SystemInterface.Security.Certificate
{
    /// <summary>
    /// Interface wrapping a X509ChainElementEnumerator.
    /// </summary>
    public interface IX509ChainElementEnumerator
    {
        #region Public Properties

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        IX509ChainElement Current { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        bool MoveNext();

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        void Reset();

        #endregion
    }
}