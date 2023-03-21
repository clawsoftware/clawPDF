namespace SystemInterface.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Interface wrapping a X509Chain.
    /// </summary>
    public interface IX509Chain
    {
        #region Public Properties

        /// <summary>
        /// Gets a collection of IPamsX509ChainElement objects.
        /// </summary>
        IX509ChainElementCollection ChainElements { get; }

        /// <summary>
        /// Gets the X509ChainPolicy to use when building an X.509 certificate chain.
        /// </summary>
        X509ChainPolicy ChainPolicy { get; }

        /// <summary>
        /// Gets the status of each element in an X509Chain object.
        /// </summary>
        X509ChainStatus[] ChainStatus { get; }

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
        bool Build(IX509Certificate certificate);

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        void Initialize();

        #endregion
    }
}