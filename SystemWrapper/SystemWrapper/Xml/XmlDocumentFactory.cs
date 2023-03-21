namespace SystemWrapper.Xml
{
    using System.Xml;

    using SystemInterface.Xml;

    /// <summary>
    ///     Factory to create a new <see cref="IXmlDocument"/> instance.
    /// </summary>
    public class XmlDocumentFactory : IXmlDocumentFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="IXmlDocument"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        /// The <see cref="IXmlDocument"/>.
        /// </returns>
        public IXmlDocument Create()
        {
            return new XmlDocumentWrap();
        }

        /// <summary>
        ///     Creates a new <see cref="IXmlDocument"/> instance passing the xml name table.
        /// </summary>
        /// <param name="nt">
        ///     The nt.
        /// </param>
        /// <returns>
        ///     The <see cref="IXmlDocument"/>.
        /// </returns>
        public IXmlDocument Create(XmlNameTable nt)
        {
            return new XmlDocumentWrap(nt);
        }

        #endregion
    }
}