namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interface wrapping a X509ChainElement.
    /// </summary>
    public interface IX509ChainElement
    {
        #region Public Properties

        /// <summary>
        /// Gets the X.509 certificate at a particular chain element.
        /// </summary>
        IX509Certificate Certificate { get; }

        /// <summary>
        /// Gets the error status of the current X.509 certificate in a chain.
        /// </summary>
        X509ChainStatus[] ChainElementStatus { get; }

        #endregion
    }
}