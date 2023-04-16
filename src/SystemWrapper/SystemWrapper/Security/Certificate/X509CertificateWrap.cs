namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.IO;
    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Wrapper for <see cref="X509Certificate2"/> class.
    /// </summary>
    public class X509CertificateWrap : IX509Certificate
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrap"/> class.
        /// </summary>
        public X509CertificateWrap(IFile file,
                                   IPath path)
        {
            this.FileWrap = file;
            this.PathWrap = path;

            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrap"/> class.
        /// </summary>
        /// <param name="rawData">
        /// Byte array containing the X.509 certificate data
        /// </param>
        /// <param name="password">
        /// The password required to access the X.509 certificate data.
        /// </param>
        public X509CertificateWrap(IFile file,
                                   IPath path,
                                   byte[] rawData,
                                   string password = null)
        {
            this.FileWrap = file;
            this.PathWrap = path;

            if (password == null)
            {
                this.Initialize(rawData);
            }
            else
            {
                this.Initialize(rawData, password);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrap"/> class.
        /// </summary>
        /// <param name="certificate">
        /// The certificate.
        /// </param>
        public X509CertificateWrap(IFile file,
                                   IPath path,
                                   X509Certificate certificate)
        {
            this.FileWrap = file;
            this.PathWrap = path;

            this.Initialize(certificate);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a collection of X509Extension objects.
        /// </summary>
        public X509ExtensionCollection Extensions
        {
            get
            {
                return this.X509CertificateInstance.Extensions;
            }
        }

        /// <summary>
        /// Gets a value indicating whether an IPamsCertificate object contains a private key. 
        /// </summary>
        public bool HasPrivateKey
        {
            get
            {
                return this.X509CertificateInstance.HasPrivateKey;
            }
        }

        /// <summary>
        /// Gets the name of the certificate authority that issued the X509v3 certificate.
        /// </summary>
        public string Issuer
        {
            get
            {
                return this.X509CertificateInstance.Issuer;
            }
        }

        /// <summary>
        /// Gets the AsymmetricAlgorithm object that represents the private key associated with a certificate.
        /// </summary>
        public AsymmetricAlgorithm PrivateKey
        {
            get
            {
                return this.X509CertificateInstance.PrivateKey;
            }
        }

        /// <summary>
        /// Gets a PublicKey object associated with a certificate.
        /// </summary>
        public PublicKey PublicKey
        {
            get
            {
                return this.X509CertificateInstance.PublicKey;
            }
        }

        /// <summary>
        /// Gets the subject distinguished name from the certificate.
        /// </summary>
        public string Subject
        {
            get
            {
                return this.X509CertificateInstance.Subject;
            }
        }

        /// <summary>
        /// Gets an instance of a <see cref="X509Certificate2"/>.
        /// </summary>
        public X509Certificate2 X509CertificateInstance { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// The wrapper to a file.
        /// </summary>
        private IFile FileWrap { get; set; }

        /// <summary>
        /// The wrapper to the path.
        /// </summary>
        private IPath PathWrap { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the SHA1 hash value for the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The hexadecimal string representation of the X.509 certificate hash value.
        /// </returns>
        public string GetCertHashString()
        {
            return this.X509CertificateInstance.GetCertHashString();
        }

        /// <summary>
        /// Gets the <see cref="X509Certificate2"/> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="X509Certificate2"/>.
        /// </returns>
        public X509Certificate2 GetCertificate()
        {
            return this.X509CertificateInstance;
        }

        /// <summary>
        /// Gets the effective date of this X509v3 certificate.
        /// </summary>
        /// <returns>
        /// The effective date for this X.509 certificate.
        /// </returns>
        public string GetEffectiveDateString()
        {
            return this.X509CertificateInstance.GetEffectiveDateString();
        }

        /// <summary>
        /// Gets the expiration date of this X509v3 certificate.
        /// </summary>
        /// <returns>
        /// The expiration date for this X.509 certificate.
        /// </returns>
        public string GetExpirationDateString()
        {
            return this.X509CertificateInstance.GetExpirationDateString();
        }

        /// <summary>
        /// Gets the key algorithm parameters for the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The key algorithm parameters for the X.509 certificate as a hexadecimal string.
        /// </returns>
        public string GetKeyAlgorithmParametersString()
        {
            return this.X509CertificateInstance.GetKeyAlgorithmParametersString();
        }

        /// <summary>
        /// Gets the raw data for the entire X.509v3 certificate as an array of bytes.
        /// </summary>
        /// <returns>
        /// Byte array containing the X.509 certificate data
        /// </returns>
        public byte[] GetRawCertData()
        {
            return this.X509CertificateInstance.GetRawCertData();
        }

        /// <summary>
        /// Gets the raw data for the entire X.509v3 certificate as a hexadecimal string.
        /// </summary>
        /// <returns>
        /// The X.509 certificate data as a hexadecimal string.
        /// </returns>
        public string GetRawCertDataString()
        {
            return this.X509CertificateInstance.GetRawCertDataString();
        }

        /// <summary>
        /// Gets the serial number of the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The serial number of the X.509 certificate as an array of bytes.
        /// </returns>
        public byte[] GetSerialNumber()
        {
            return this.X509CertificateInstance.GetSerialNumber();
        }

        /// <summary>
        /// Gets the serial number of the X.509v3 certificate.
        /// </summary>
        /// <returns>
        /// The serial number of the X.509 certificate as a hexadecimal string.
        /// </returns>
        public string GetSerialNumberString()
        {
            return this.X509CertificateInstance.GetSerialNumberString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrap"/> class.
        /// </summary>
        public void Initialize()
        {
            this.X509CertificateInstance = new X509Certificate2();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrap"/> class.
        /// </summary>
        /// <param name="rawData">
        /// Byte array containing the X.509 certificate data
        /// </param>
        public void Initialize(byte[] rawData)
        {
            this.Initialize(rawData, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrap"/> class.
        /// </summary>
        /// <param name="rawData">
        /// Byte array containing the X.509 certificate data
        /// </param>
        /// <param name="password">
        /// The password required to access the X.509 certificate data.
        /// </param>
        public void Initialize(byte[] rawData,
                               string password)
        {
            var filename = this.PathWrap.GetTempFileName();
            try
            {
                this.FileWrap.WriteAllBytes(filename, rawData);

                if (password == null)
                {
                    this.Initialize(filename);
                }

                this.Initialize(filename, password);
            }
            finally
            {
                this.FileWrap.Delete(filename);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        /// <param name="fileName">
        /// the file name of a X.509 certificate data
        /// </param>        
        public void Initialize(string fileName)
        {
            this.X509CertificateInstance = new X509Certificate2(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate"/> wrapped class.
        /// </summary>
        /// <param name="fileName">
        /// the file name of a X.509 certificate data
        /// </param>
        /// <param name="password">
        /// The password required to access the X.509 certificate data.
        /// </param>
        public void Initialize(string fileName,
                               string password)
        {
            this.X509CertificateInstance = new X509Certificate2(fileName, password);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509CertificateWrap"/> class.
        /// </summary>
        /// <param name="certificate">
        /// The certificate.
        /// </param>
        public void Initialize(X509Certificate certificate)
        {
            this.X509CertificateInstance = new X509Certificate2(certificate);
        }

        #endregion
    }
}