namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interface wrapping a X509Certificate.
    /// </summary>
    public interface IX509Certificate
    {
        #region Public Properties

        /// <summary>
        /// Gets a collection of X509Extension objects.
        /// </summary>
        X509ExtensionCollection Extensions { get; }

        /// <summary>
        /// Gets a value indicating whether an IPamsCertificate object contains a private key. 
        /// </summary>
        bool HasPrivateKey { get; }

        /// <summary>
        /// Gets the name of the certificate authority that issued the X509v3 certificate.
        /// </summary>
        string Issuer { get; }

        /// <summary>
        /// Gets the AsymmetricAlgorithm object that represents the private key associated with a certificate.
        /// </summary>
        AsymmetricAlgorithm PrivateKey { get; }

        /// <summary>
        /// Gets a PublicKey object associated with a certificate.
        /// </summary>
        PublicKey PublicKey { get; }

        /// <summary>
        /// Gets the subject distinguished name from the certificate.
        /// </summary>
        string Subject { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the SHA1 hash value for the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The hexadecimal string representation of the X.509 certificate hash value.
        /// </returns>
        string GetCertHashString();

        /// <summary>
        /// Gets the <see cref="X509Certificate2"/> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="X509Certificate2"/>.
        /// </returns>
        X509Certificate2 GetCertificate();

        /// <summary>
        /// Gets the effective date of this X509v3 certificate.
        /// </summary>
        /// <returns>
        /// The effective date for this X.509 certificate.
        /// </returns>
        string GetEffectiveDateString();

        /// <summary>
        /// Gets the expiration date of this X509v3 certificate.
        /// </summary>
        /// <returns>
        /// The expiration date for this X.509 certificate.
        /// </returns>
        string GetExpirationDateString();

        /// <summary>
        /// Gets the key algorithm parameters for the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The key algorithm parameters for the X.509 certificate as a hexadecimal string.
        /// </returns>
        string GetKeyAlgorithmParametersString();

        /// <summary>
        /// Gets the raw data for the entire X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// Byte array containing the X.509 certificate data
        /// </returns>
        byte[] GetRawCertData();

        /// <summary>
        /// Gets the raw data for the entire X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The X.509 certificate data as a hexadecimal string.
        /// </returns>
        string GetRawCertDataString();

        /// <summary>
        /// Gets the serial number of the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The serial number of the X.509 certificate as an array of bytes.
        /// </returns>
        byte[] GetSerialNumber();

        /// <summary>
        /// Gets the serial number of the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The serial number of the X.509 certificate as a hexadecimal string.
        /// </returns>
        string GetSerialNumberString();

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        /// <param name="rawData">
        /// Byte array containing the X.509 certificate data
        /// </param>
        void Initialize(byte[] rawData);

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        /// <param name="rawData">
        /// Byte array containing the X.509 certificate data
        /// </param>
        /// <param name="password">
        /// The password required to access the X.509 certificate data.
        /// </param>
        void Initialize(byte[] rawData,
                        string password);

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        /// <param name="fileName">
        /// the file name of a X.509 certificate data
        /// </param>        
        void Initialize(string fileName);

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        /// <param name="fileName">
        /// the file name of a X.509 certificate data
        /// </param>
        /// <param name="password">
        /// The password required to access the X.509 certificate data.
        /// </param>
        void Initialize(string fileName,
                        string password);

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        /// <param name="certificate">
        /// The certificate.
        /// </param>
        void Initialize(X509Certificate certificate);

        #endregion
    }
}