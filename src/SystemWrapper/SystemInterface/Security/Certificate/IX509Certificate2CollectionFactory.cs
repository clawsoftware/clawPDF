namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// This interface wraps the X509Chain creation in order to be mock-able.
    /// </summary>
    public interface IX509Certificate2CollectionFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509Certificate2Collection creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509Certificate2Collection"/>.
        /// </returns>
        IX509Certificate2Collection Create();

        /// <summary>
        /// Wraps the X509Certificate2Collection creation method.
        /// </summary>
        /// <param name="collection">
        /// The collection of certificates
        /// </param>
        /// <returns>
        /// The <see cref="IX509Certificate2Collection"/>.
        /// </returns>
        IX509Certificate2Collection Create(X509Certificate2Collection collection);

        #endregion
    }
}