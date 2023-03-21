namespace SystemWrapper.Security.Certificate
{
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.IO;
    using SystemInterface.Security.Certificate;

    /// <summary>
    /// This class wraps a X509ChainElement in order to be mock-able.
    /// </summary>
    public class X509ChainElementFactory : IX509ChainElementFactory
    {
        #region Fields

        private readonly IFile file;

        private readonly IPath path;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="file">
        /// </param>
        /// <param name="path">
        /// </param>
        public X509ChainElementFactory(IFile file,
                                       IPath path)
        {
            this.file = file;
            this.path = path;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Wraps the X509ChainElement creation method.
        /// </summary>
        /// <returns>
        /// The <see cref="IX509ChainElement"/>.
        /// </returns>
        public IX509ChainElement Create()
        {
            return new X509ChainElementWrap();
        }

        /// <summary>
        /// Wraps the X509ChainElement creation method.
        /// </summary>
        /// <param name="element">
        /// The X.509v3 chain element.
        /// </param>
        /// <returns>
        /// The <see cref="IX509ChainElement"/>.
        /// </returns>
        public IX509ChainElement Create(X509ChainElement element)
        {
            return new X509ChainElementWrap(new X509CertificateWrap(this.file, this.path, element.Certificate), element.ChainElementStatus);
        }

        #endregion
    }
}