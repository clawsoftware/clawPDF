namespace SystemWrapper.Security.Certificate
{
    using System.Collections;
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Wrapper for <see cref="X509Certificate2Collection"/>.
    /// </summary>
    public class X509Certificate2CollectionWrap : CollectionBase,
                                                  IX509Certificate2Collection
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate2CollectionWrap"/> class.
        /// </summary>
        public X509Certificate2CollectionWrap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate2CollectionWrap"/> class.
        /// </summary>
        /// <param name="collection">
        /// The collection of certificates.
        /// </param>
        public X509Certificate2CollectionWrap(X509Certificate2Collection collection)
        {
            this.Initialize(collection);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds a certificate to an X.509 certificate store.
        /// </summary>
        /// <param name="certificate">
        /// The certificate to add.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Add(IX509Certificate certificate)
        {
            return this.List.Add(certificate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Certificate2Collection"/> class.
        /// </summary>
        /// <param name="collection">
        /// The collection of certificates.
        /// </param>
        public void Initialize(X509Certificate2Collection collection)
        {
            foreach (var certificate in collection)
            {
                this.List.Add(certificate);
            }
        }

        #endregion
    }
}