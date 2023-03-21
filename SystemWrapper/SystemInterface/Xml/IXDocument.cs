namespace SystemInterface.Xml
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    ///   Defines the contract for the wrapper of the <see cref="XDocument"/> class.
    /// </summary>
    public interface IXDocument
    {
        #region Public Properties

        /// <summary>
        ///     Gets the XML declaration for this document.
        /// </summary>
        XDeclaration Declaration { get; set; }

        /// <summary>
        ///     Gets the original of <see cref="XDocument"/>.
        /// </summary>
        XDocument Document { get; }

        /// <summary>
        ///     Gets the Document Type Definition (DTD) for this document.
        /// </summary>
        XDocumentType DocumentType { get; }

        /// <summary>
        ///     Gets the node type for this node.
        /// </summary>
        /// <remarks>
        ///     This property will always return XmlNodeType.Document.
        /// </remarks>
        XmlNodeType NodeType { get; }

        /// <summary>
        ///     Gets the root element of the XML Tree for this document.
        /// </summary>
        XElement Root { get; }

        #endregion

        #region Public Methods and Operators

        ///<overloads>
        ///     Outputs this <see cref="XDocument"/>'s underlying XML tree.  The output can
        /// be saved to a file, a <see cref="Stream"/>, a <see cref="TextWriter"/>,
        /// or an <see cref="XmlWriter"/>.  Optionally whitespace can be preserved.  
        /// </overloads>
        /// <summary>
        ///     Output this <see cref="XDocument"/> to a file.
        /// </summary>
        /// <remarks>
        ///     The format will be indented by default.  If you want
        /// no indenting then use the SaveOptions version of Save (see
        /// <see cref="XDocument.Save(string, SaveOptions)"/>) enabling 
        /// SaveOptions.DisableFormatting.
        /// There is also an option SaveOptions.OmitDuplicateNamespaces for removing duplicate namespace declarations. 
        /// Or instead use the SaveOptions as an annotation on this node or its ancestors, then this method will use those options.
        /// </remarks>
        /// <param name="fileName">
        ///     The name of the file to output the XML to.
        /// </param>
        void Save(string fileName);

        /// <summary>
        ///     Output this <see cref="XDocument"/> to a file.
        /// </summary>
        /// <param name="fileName">
        ///     The name of the file to output the XML to.  
        /// </param>
        /// <param name="options">
        ///     If SaveOptions.DisableFormatting is enabled the output is not indented.
        ///     If SaveOptions.OmitDuplicateNamespaces is enabled duplicate namespace declarations will be removed.
        /// </param>
        void Save(string fileName,
                  SaveOptions options);

        /// <summary>
        ///     Output this <see cref="XDocument"/> to the passed in <see cref="Stream"/>.
        /// </summary>
        /// <remarks>
        ///     The format will be indented by default.  If you want
        /// no indenting then use the SaveOptions version of Save (see
        /// <see cref="XDocument.Save(Stream, SaveOptions)"/>) enabling
        /// SaveOptions.DisableFormatting
        /// There is also an option SaveOptions.OmitDuplicateNamespaces for removing duplicate namespace declarations. 
        /// Or instead use the SaveOptions as an annotation on this node or its ancestors, then this method will use those options.
        /// </remarks>
        /// <param name="stream">
        ///     The <see cref="Stream"/> to output this <see cref="XDocument"/> to.
        /// </param>
        void Save(Stream stream);

        /// <summary>
        ///     Output this <see cref="XDocument"/> to a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">
        ///     The <see cref="Stream"/> to output the XML to.  
        /// </param>
        /// <param name="options">
        ///     If SaveOptions.DisableFormatting is enabled the output is not indented.
        ///     If SaveOptions.OmitDuplicateNamespaces is enabled duplicate namespace declarations will be removed.
        /// </param>
        void Save(Stream stream,
                  SaveOptions options);

        /// <summary>
        ///     Output this <see cref="XDocument"/> to the passed in <see cref="TextWriter"/>.
        /// </summary>
        /// <remarks>
        ///     The format will be indented by default.  If you want
        /// no indenting then use the SaveOptions version of Save (see
        /// <see cref="XDocument.Save(TextWriter, SaveOptions)"/>) enabling
        /// SaveOptions.DisableFormatting
        /// There is also an option SaveOptions.OmitDuplicateNamespaces for removing duplicate namespace declarations. 
        /// Or instead use the SaveOptions as an annotation on this node or its ancestors, then this method will use those options.
        /// </remarks>
        /// <param name="textWriter">
        ///     The <see cref="TextWriter"/> to output this <see cref="XDocument"/> to.
        /// </param>
        void Save(TextWriter textWriter);

        /// <summary>
        ///     Output this <see cref="XDocument"/> to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="textWriter">
        ///     The <see cref="TextWriter"/> to output the XML to.  
        /// </param>
        /// <param name="options">
        ///     If SaveOptions.DisableFormatting is enabled the output is not indented.
        ///     If SaveOptions.OmitDuplicateNamespaces is enabled duplicate namespace declarations will be removed.
        /// </param>
        void Save(TextWriter textWriter,
                  SaveOptions options);

        /// <summary>
        ///     Output this <see cref="XDocument"/> to an <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"/> to output the XML to.
        /// </param>
        void Save(XmlWriter writer);

        /// <summary>
        ///     Output this <see cref="XDocument"/>'s underlying XML tree to the
        /// passed in <see cref="XmlWriter"/>.
        /// <seealso cref="XDocument.Save(XmlWriter)"/>
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"/> to output the content of this 
        /// <see cref="XDocument"/>.
        /// </param>
        void WriteTo(XmlWriter writer);

        #endregion
    }
}