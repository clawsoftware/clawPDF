namespace SystemInterface.Xml
{
    using System.Xml;

    /// <summary>
    ///     Factory to create a new <see cref="IXmlDocument"/> instance.
    /// </summary>
    public interface IXmlDocumentFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="IXmlDocument"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        /// The <see cref="IXmlDocument"/>.
        /// </returns>
        IXmlDocument Create();

        /// <summary>
        ///     Creates a new <see cref="IXmlDocument"/> instance passing the xml name table.
        /// </summary>
        /// <param name="nt">
        ///     The nt.
        /// </param>
        /// <returns>
        ///     The <see cref="IXmlDocument"/>.
        /// </returns>
        IXmlDocument Create(XmlNameTable nt);

        #endregion
    }
}