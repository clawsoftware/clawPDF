namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interface wrapping a X509Certificate2Collection.
    /// </summary>
    public interface IX509Certificate2Collection
    {
        #region Public Methods and Operators

        /// <summary>
        /// Adds a certificate to an X.509 certificate store.
        /// </summary>
        /// <param name="certificate">
        /// The IPamsCertificate to add to the current X509CertificateCollection. 
        /// </param>
        /// <returns>
        /// The index into the current X509CertificateCollection at which the new IPamsCertificate was inserted.
        /// </returns>
        int Add(IX509Certificate certificate);

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate2Collection"/> class.
        /// </summary>
        /// <param name="collection">
        /// The collection of certificates.
        /// </param>
        void Initialize(X509Certificate2Collection collection);

        #endregion
    }
}