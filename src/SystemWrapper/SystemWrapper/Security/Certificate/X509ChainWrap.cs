namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Wrapper for <see cref="X509Chain"/>
    /// </summary>
    public class X509ChainWrap : IX509Chain
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainWrap"/> class.
        /// </summary>
        public X509ChainWrap()
        {
            this.Initialize();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a collection of IPamsX509ChainElement objects.
        /// </summary>
        public IX509ChainElementCollection ChainElements
        {
            get
            {
                return new X509ChainElementCollectionFactory().Create(this.X509ChainInstance.ChainElements);
            }
        }

        /// <summary>
        /// Gets the X509ChainPolicy to use when building an X.509 certificate chain.
        /// </summary>
        public X509ChainPolicy ChainPolicy
        {
            get
            {
                return this.X509ChainInstance.ChainPolicy;
            }
        }

        /// <summary>
        /// Gets the status of each element in an X509Chain object.
        /// </summary>
        public X509ChainStatus[] ChainStatus
        {
            get
            {
                return this.X509ChainInstance.ChainStatus;
            }
        }

        /// <summary>
        /// Gets an instance of a <see cref="X509Chain"/>
        /// </summary>
        public X509Chain X509ChainInstance { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="certificate">
        /// The certificate.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Build(IX509Certificate certificate)
        {
            return this.X509ChainInstance.Build(certificate.GetCertificate());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainWrap"/> class.
        /// </summary>
        public void Initialize()
        {
            this.X509ChainInstance = new X509Chain();
        }

        #endregion
    }
}