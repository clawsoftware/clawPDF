namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.IO;
    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Factory Creating a X509Certificate to avoid bugg in X509Certificate that does not delete tmp files
    /// </summary>
    public class X509CertificateFactoryWrap : IX509CertificateFactoryWrap
    {
        #region Fields

        private readonly IFile fileWrap;

        private readonly IPath pathWrap;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="fileWrap">
        /// </param>
        /// <param name="pathWrap">
        /// </param>
        public X509CertificateFactoryWrap(IFile fileWrap,
                                          IPath pathWrap)
        {
            this.fileWrap = fileWrap;
            this.pathWrap = pathWrap;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creating an X509Certificate from a byte array
        /// </summary>
        /// <param name="certificateAsBytes">
        /// The certificate as bytes.
        /// </param>
        /// <param name="password">
        /// The password needed to access private key. Can be null if the certificate specified as bytes does not require password decryption.
        /// </param>
        /// <returns>
        /// X509Certificate of the given byte Array
        /// </returns>
        public IX509Certificate Create(byte[] certificateAsBytes,
                                       string password = null)
        {
            return new X509CertificateWrap(this.fileWrap, this.pathWrap, certificateAsBytes, password);
        }

        /// <summary>
        /// Creating an X509Certificate from a byte array
        /// </summary>
        /// <param name="certificate">
        /// The X509Certificate to create
        /// </param>
        /// <returns>
        /// X509Certificate of the given byte Array
        /// </returns>
        public IX509Certificate Create(X509Certificate certificate)
        {
            return new X509CertificateWrap(this.fileWrap, this.pathWrap, certificate);
        }

        #endregion
    }
}