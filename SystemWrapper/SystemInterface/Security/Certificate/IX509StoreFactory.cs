namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// This interface wraps the X509Store creation in order to be mock-able.
    /// </summary>
    public interface IX509StoreFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509Store creation method.
        /// </summary>
        /// <param name="storeLocation">
        /// One of the enumeration values that specifies the location of the X.509 certificate store. 
        /// </param>
        /// <returns>
        /// The <see cref="IX509Store"/>.
        /// </returns>
        IX509Store Create(StoreLocation storeLocation);

        #endregion
    }
}