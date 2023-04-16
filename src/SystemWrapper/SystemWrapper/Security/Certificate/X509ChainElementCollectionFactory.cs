namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// This class wraps a X509ChainElementCollection in order to be mock-able.
    /// </summary>
    public class X509ChainElementCollectionFactory : IX509ChainElementCollectionFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509ChainElementCollection creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509ChainElementCollection"/>.
        /// </returns>
        public IX509ChainElementCollection Create()
        {
            return new X509ChainElementCollectionWrap();
        }

        /// <summary>
        /// Wraps the X509ChainElementCollection creation method.
        /// </summary>
        /// <param name="collection">
        /// The collection of X509ChainElement objects.
        /// </param>
        /// <returns>
        /// The <see cref="IX509ChainElementCollection"/>.
        /// </returns>
        public IX509ChainElementCollection Create(X509ChainElementCollection collection)
        {
            return new X509ChainElementCollectionWrap(collection);
        }

        #endregion
    }
}