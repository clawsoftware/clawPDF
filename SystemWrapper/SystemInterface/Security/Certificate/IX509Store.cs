namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interface wrapping a X509Store.
    /// </summary>
    public interface IX509Store
    {
        #region Public Properties

        /// <summary>
        /// Gets a collection of certificates.
        /// </summary>
        IX509Certificate2Collection Certificates { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Initializes a new instance of the <see cref="X509Store"/> class.
        /// </summary>
        /// <param name="storeLocation">
        /// One of the enumeration values that specifies the location of the X.509 certificate store. 
        /// </param>
        void Initialize(StoreLocation storeLocation);

        /// <summary>
        /// Opens an X.509 certificate store or creates a new store, depending on OpenFlags flag settings.
        /// </summary>
        /// <param name="flags">
        /// A bitwise combination of enumeration values that specifies the way to open the X.509 certificate store. 
        /// </param>
        void Open(OpenFlags flags);

        #endregion
    }
}