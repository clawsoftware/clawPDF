namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// This class wraps a X509Chain in order to be mock-able.
    /// </summary>
    public class X509Certificate2CollectionFactory : IX509Certificate2CollectionFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509Certificate2Collection creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509Certificate2Collection"/>.
        /// </returns>
        public IX509Certificate2Collection Create()
        {
            return new X509Certificate2CollectionWrap();
        }

        /// <summary>
        /// Wraps the X509Certificate2Collection creation method.
        /// </summary>
        /// <param name="collection">
        /// The collection of certificates
        /// </param>
        /// <returns>
        /// The <see cref="IX509Certificate2Collection"/>.
        /// </returns>
        public IX509Certificate2Collection Create(X509Certificate2Collection collection)
        {
            return new X509Certificate2CollectionWrap(collection);
        }

        #endregion
    }
}