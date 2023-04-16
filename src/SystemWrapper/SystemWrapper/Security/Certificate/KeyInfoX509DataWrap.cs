namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Cryptography.Xml;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Wrapper for <see cref="X509Chain"/>
    /// </summary>
    public class KeyInfoX509DataWrap : IKeyInfoX509Data
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainWrap"/> class.
        /// </summary>
        public KeyInfoX509DataWrap(IX509Certificate certificate,
                                   X509IncludeOption option)
        {
            this.Initialize(certificate, option);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an instance of a <see cref="X509Chain"/>
        /// </summary>
        private KeyInfoX509Data KeyInfoX509DataInstance { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainWrap"/> class.
        /// </summary>
        private void Initialize(IX509Certificate certificate,
                                X509IncludeOption option)
        {
            this.KeyInfoX509DataInstance = new KeyInfoX509Data(certificate.GetCertificate(), option);
        }

        #endregion
    }
}