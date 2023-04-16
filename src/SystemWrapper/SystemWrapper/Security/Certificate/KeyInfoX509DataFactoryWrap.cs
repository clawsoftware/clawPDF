namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.IO;
    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Implementation of the KeyInfoX509Data factory interface.
    /// </summary>
    public class KeyInfoX509DataFactoryWrap : IKeyInfoX509DataFactory
    {
        #region Fields

        private readonly IFile file;

        private readonly IPath path;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">
        /// </param>
        /// <param name="path">
        /// </param>
        public KeyInfoX509DataFactoryWrap(IFile file,
                                          IPath path)
        {
            this.file = file;
            this.path = path;
        }

        #endregion

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
        public IKeyInfoX509Data Create(X509Certificate certificate)
        {
            return new KeyInfoX509DataWrap(new X509CertificateWrap(this.file, this.path, certificate), X509IncludeOption.WholeChain);
        }

        /// <summary>
        /// BuildTrustedChainFromEndCertificate a KeyInfoX509Data using the certificates located in stores.
        /// </summary>
        /// <param name="certificate">
        /// The certificate to create a KeyInfoX509Data object from.
        /// </param>
        /// <returns>
        /// KeyInfoX509Data object of the given certificate.
        /// </returns>
        public IKeyInfoX509Data Create(IX509Certificate certificate)
        {
            return new KeyInfoX509DataWrap(certificate, X509IncludeOption.WholeChain);
        }

        #endregion
    }
}