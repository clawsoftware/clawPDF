namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Wrapper for <see cref="X509ChainElement"/>
    /// </summary>
    public class X509ChainElementWrap : IX509ChainElement
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainElementWrap"/> class.
        /// </summary>
        public X509ChainElementWrap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainElementWrap"/> class.
        /// </summary>
        /// <param name="certificate">
        /// The certificate.
        /// </param>
        /// <param name="chainElementStatus">
        /// The error status of the current X.509 certificate in a chain.
        /// </param>
        public X509ChainElementWrap(IX509Certificate certificate,
                                    X509ChainStatus[] chainElementStatus)
        {
            this.Certificate = certificate;
            this.ChainElementStatus = chainElementStatus;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the X.509 certificate at a particular chain element.
        /// </summary>
        public IX509Certificate Certificate { get; private set; }

        /// <summary>
        /// Gets the error status of the current X.509 certificate in a chain.
        /// </summary>
        public X509ChainStatus[] ChainElementStatus { get; private set; }

        #endregion
    }
}