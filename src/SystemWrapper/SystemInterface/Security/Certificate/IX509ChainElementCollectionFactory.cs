namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// This interface wraps the X509ChainElementCollection creation in order to be mock-able.
    /// </summary>
    public interface IX509ChainElementCollectionFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509ChainElementCollection creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509ChainElementCollection"/>.
        /// </returns>
        IX509ChainElementCollection Create();

        /// <summary>
        /// Wraps the X509ChainElementCollection creation method.
        /// </summary>
        /// <param name="collection">
        /// The collection of X509ChainElement objects.
        /// </param>
        /// <returns>
        /// The <see cref="IX509ChainElementCollection"/>.
        /// </returns>
        IX509ChainElementCollection Create(X509ChainElementCollection collection);

        #endregion
    }
}