namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Wrapper for <see cref="X509Store"/>.
    /// </summary>
    public class X509StoreWrap : IX509Store
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="X509StoreWrap"/> class.
        /// </summary>
        /// <param name="storeLocation">
        /// The store location.
        /// </param>
        public X509StoreWrap(StoreLocation storeLocation)
        {
            this.Initialize(storeLocation);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a collection of certificates.
        /// </summary>
        public IX509Certificate2Collection Certificates
        {
            get
            {
                return new X509Certificate2CollectionFactory().Create(this.X509StoreInstance.Certificates);
            }
        }

        /// <summary>
        /// Gets an instance of a <see cref="X509Store"/>.
        /// </summary>
        public X509Store X509StoreInstance { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Store"/> class.
        /// </summary>
        /// <param name="storeLocation">
        /// One of the enumeration values that specifies the location of the X.509 certificate store. 
        /// </param>
        public void Initialize(StoreLocation storeLocation)
        {
            this.X509StoreInstance = new X509Store(storeLocation);
        }

        /// <summary>
        /// Opens an X.509 certificate store or creates a new store, depending on OpenFlags flag settings.
        /// </summary>
        /// <param name="flags">
        /// A bitwise combination of enumeration values that specifies the way to open the X.509 certificate store. 
        /// </param>
        public void Open(OpenFlags flags)
        {
            this.X509StoreInstance.Open(flags);
        }

        #endregion
    }
}