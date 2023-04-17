namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// This interface wraps the X509ChainElement creation in order to be mock-able.
    /// </summary>
    public interface IX509ChainElementFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509ChainElement creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509ChainElement"/>.
        /// </returns>
        IX509ChainElement Create();

        /// <summary>
        /// Wraps the X509ChainElement creation method.
        /// </summary>
        /// <param name="element">
        /// The X.509v3 chain element.
        /// </param>
        /// <returns>
        /// The <see cref="IX509ChainElement"/>.
        /// </returns>
        IX509ChainElement Create(X509ChainElement element);

        #endregion
    }
}