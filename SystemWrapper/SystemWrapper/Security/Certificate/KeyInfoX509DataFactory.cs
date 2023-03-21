namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Cryptography.Xml;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Implementation of the KeyInfoX509Data factory interface.
    /// </summary>
    public class KeyInfoX509DataFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// BuildTrustedChainFromEndCertificate a KeyInfoX509Data using the certificates located in stores.
        /// </summary>
        /// <param name="certificate">
        /// The certificate to create a KeyInfoX509Data object from.
        /// </param>
        /// <returns>
        /// KeyInfoX509Data object of the given certificate.
        /// </returns>
        public KeyInfoX509Data Create(IX509Certificate certificate)
        {
            return new KeyInfoX509Data(certificate.GetCertificate(), X509IncludeOption.WholeChain);
        }

        #endregion
    }
}