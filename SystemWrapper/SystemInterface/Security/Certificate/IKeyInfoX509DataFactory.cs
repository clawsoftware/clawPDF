namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interface of the KeyInfoX509Data factory.
    /// </summary>
    public interface IKeyInfoX509DataFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// BuildTrustedChainFromEndCertificate a KeyInfoX509Data object.
        /// </summary>
        /// <param name="certificate">
        /// The certificate to create a KeyInfoX509Data object from.
        /// </param>
        /// <returns>
        /// KeyInfoX509Data object of the given certificate.
        /// </returns>
        IKeyInfoX509Data Create(X509Certificate certificate);

        /// <summary>
        /// BuildTrustedChainFromEndCertificate a KeyInfoX509Data object.
        /// </summary>
        /// <param name="certificate">
        /// The certificate to create a KeyInfoX509Data object from.
        /// </param>
        /// <returns>
        /// KeyInfoX509Data object of the given certificate.
        /// </returns>
        IKeyInfoX509Data Create(IX509Certificate certificate);

        #endregion
    }
}