namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.Security.Certificate;

    /// <summary>
    /// This class wraps a X509Store in order to be mock-able.
    /// </summary>
    public class X509StoreFactory : IX509StoreFactory
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
        public IX509Store Create(StoreLocation storeLocation)
        {
            return new X509StoreWrap(storeLocation);
        }

        #endregion
    }
}