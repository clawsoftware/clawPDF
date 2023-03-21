namespace SystemWrapper.Xml
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    using SystemInterface.Xml;

    /// <summary>
    ///     Wrapper of the <see cref="XDocument"/> class.
    /// </summary>
    public class XDocumentWrap : IXDocument
    {
        #region Fields

        private readonly XDocument instance;

        #endregion

        #region Constructors and Destructors

        ///<overloads>
        /// Initializes a new instance of the <see cref="XDocument"/> class.
        /// Overloaded constructors are provided for creating a new empty 
        /// <see cref="XDocument"/>, creating an <see cref="XDocument"/> with
        /// a parameter list of initial content, and as a copy of another
        /// <see cref="XDocument"/> object.
        /// </overloads>
        /// <summary>
        /// Initializes a new instance of the <see cref="XDocument"/> class.
        /// </summary>
        public XDocumentWrap()
        {
            this.instance = new XDocument();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XDocument"/> class with the specified content.
        /// </summary>
        /// <param name="content">
        /// A parameter list of content objects to add to this document.
        /// </param>
        /// <remarks>
        /// Valid content includes:
        /// <list>
        /// <item>Zero or one <see cref="XDocumentType"/> objects</item>
        /// <item>Zero or one elements</item>
        /// <item>Zero or more comments</item>
        /// <item>Zero or more processing instructions</item>
        /// </list>
        /// See XContainer.Add(object content) for details about the content that can be added
        /// using this method.
        /// </remarks>
        public XDocumentWrap(params object[] content)
        {
            this.instance = new XDocument(content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XDocument"/> class
        /// with the specifed <see cref="XDeclaration"/> and content.
        /// </summary>
        /// <param name="declaration">
        /// The XML declaration for the document.
        /// </param>
        /// <param name="content">
        /// The contents of the document.
        /// </param>
        /// <remarks>
        /// Valid content includes:
        /// <list>
        /// <item>Zero or one <see cref="XDocumentType"/> objects</item>
        /// <item>Zero or one elements</item>
        /// <item>Zero or more comments</item>
        /// <item>Zero or more processing instructions</item>
        /// <item></item>
        /// </list>
        /// See XContainer.Add(object content) for details about the content that can be added
        /// using this method.
        /// </remarks>
        public XDocumentWrap(XDeclaration declaration,
                             params object[] content)
        {
            this.instance = new XDocument(declaration, content);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XDocument"/> class from an
        /// existing XDocument object.
        /// </summary>
        /// <param name="other">
        /// The <see cref="XDocument"/> object that will be copied.
        /// </param>
        public XDocumentWrap(XDocument other)
        {
            this.instance = new XDocument(other);
        }

        /// <overloads>
        /// The Load method provides multiple strategies for creating a new 
        /// <see cref="XDocument"/> and initializing it from a data source containing
        /// raw XML.  Load from a file (passing in a URI to the file), a
        /// <see cref="Stream"/>, a <see cref="TextReader"/>, or an
        /// <see cref="XmlReader"/>.  Note:  Use <see cref="XDocument.Parse(string)"/>
        /// to create an <see cref="XDocument"/> from a string containing XML.
        /// <seealso cref="XDocument.Parse(string)"/>
        /// </overloads>
        /// <summary>
        /// Create a new <see cref="XDocument"/> based on the contents of the file 
        /// referenced by the URI parameter passed in.  Note: Use 
        /// <see cref="XDocument.Parse(string)"/> to create an <see cref="XDocument"/> from
        /// a string containing XML.
        /// <seealso cref="XmlReader.Create(string)"/>
        /// <seealso cref="XDocument.Parse(string)"/>
        /// </summary>
        /// <remarks>
        /// This method uses the <see cref="XmlReader.Create(string)"/> method to create
        /// an <see cref="XmlReader"/> to read the raw XML into the underlying
        /// XML tree.
        /// </remarks>
        /// <param name="uri">
        /// A URI string referencing the file to load into a new <see cref="XDocument"/>.
        /// </param>
        /// <returns>
        /// An <see cref="XDocument"/> initialized with the contents of the file referenced
        /// in the passed in uri parameter.
        /// </returns>
        public XDocumentWrap(string uri)
        {
            this.instance = XDocument.Load(uri);
        }

        /// <summary>
        /// Create a new <see cref="XDocument"/> based on the contents of the file 
        /// referenced by the URI parameter passed in.  Optionally, whitespace can be preserved.  
        /// <see cref="XmlReader.Create(string)"/>
        /// </summary>
        /// <remarks>
        /// This method uses the <see cref="XmlReader.Create(string)"/> method to create
        /// an <see cref="XmlReader"/> to read the raw XML into an underlying
        /// XML tree.  If LoadOptions.PreserveWhitespace is enabled then
        /// the <see cref="XmlReaderSettings"/> property <see cref="XmlReaderSettings.IgnoreWhitespace"/>
        /// is set to false.
        /// </remarks>
        /// <param name="uri">
        /// A string representing the URI of the file to be loaded into a new <see cref="XDocument"/>.
        /// </param>
        /// <param name="options">
        /// A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        /// An <see cref="XDocument"/> initialized with the contents of the file referenced
        /// in the passed uri parameter.  If LoadOptions.PreserveWhitespace is enabled then
        /// all whitespace will be preserved.
        /// </returns>
        public XDocumentWrap(string uri,
                             LoadOptions options)
        {
            this.instance = XDocument.Load(uri, options);
        }

        /// <summary>
        /// Create a new <see cref="XDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="Stream"/> parameter.  
        /// </summary>
        /// <param name="stream">
        /// A <see cref="Stream"/> containing the raw XML to read into the newly
        /// created <see cref="XDocument"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="XDocument"/> containing the contents of the passed in
        /// <see cref="Stream"/>.
        /// </returns>
        public XDocumentWrap(Stream stream)
        {
            this.instance = XDocument.Load(stream);
        }

        /// <summary>
        /// Create a new <see cref="XDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="Stream"/> parameter.  Optionally whitespace handling
        /// can be preserved.
        /// </summary>
        /// <remarks>
        /// If LoadOptions.PreserveWhitespace is enabled then
        /// the underlying <see cref="XmlReaderSettings"/> property <see cref="XmlReaderSettings.IgnoreWhitespace"/>
        /// is set to false.
        /// </remarks>
        /// <param name="stream">
        /// A <see cref="Stream"/> containing the raw XML to read into the newly
        /// created <see cref="XDocument"/>.
        /// </param>
        /// <param name="options">
        /// A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="XDocument"/> containing the contents of the passed in
        /// <see cref="Stream"/>.
        /// </returns>
        public XDocumentWrap(Stream stream,
                             LoadOptions options)
        {
            this.instance = XDocument.Load(stream, options);
        }

        /// <summary>
        /// Create a new <see cref="XDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="TextReader"/> parameter.  
        /// </summary>
        /// <param name="textReader">
        /// A <see cref="TextReader"/> containing the raw XML to read into the newly
        /// created <see cref="XDocument"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="XDocument"/> containing the contents of the passed in
        /// <see cref="TextReader"/>.
        /// </returns>
        public XDocumentWrap(TextReader textReader)
        {
            this.instance = XDocument.Load(textReader);
        }

        /// <summary>
        /// Create a new <see cref="XDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="TextReader"/> parameter.  Optionally whitespace handling
        /// can be preserved.
        /// </summary>
        /// <remarks>
        /// If LoadOptions.PreserveWhitespace is enabled then
        /// the <see cref="XmlReaderSettings"/> property <see cref="XmlReaderSettings.IgnoreWhitespace"/>
        /// is set to false.
        /// </remarks>
        /// <param name="textReader">
        /// A <see cref="TextReader"/> containing the raw XML to read into the newly
        /// created <see cref="XDocument"/>.
        /// </param>
        /// <param name="options">
        /// A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="XDocument"/> containing the contents of the passed in
        /// <see cref="TextReader"/>.
        /// </returns>
        public XDocumentWrap(TextReader textReader,
                             LoadOptions options)
        {
            this.instance = XDocument.Load(textReader, options);
        }

        /// <summary>
        /// Create a new <see cref="XDocument"/> containing the contents of the
        /// passed in <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader">
        /// An <see cref="XmlReader"/> containing the XML to be read into the new
        /// <see cref="XDocument"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="XDocument"/> containing the contents of the passed
        /// in <see cref="XmlReader"/>.
        /// </returns>
        public XDocumentWrap(XmlReader reader)
        {
            this.instance = XDocument.Load(reader);
        }

        /// <summary>
        /// Create a new <see cref="XDocument"/> containing the contents of the
        /// passed in <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader">
        /// An <see cref="XmlReader"/> containing the XML to be read into the new
        /// <see cref="XDocument"/>.
        /// </param>
        /// <param name="options">
        /// A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        /// A new <see cref="XDocument"/> containing the contents of the passed
        /// in <see cref="XmlReader"/>.
        /// </returns>
        public XDocumentWrap(XmlReader reader,
                             LoadOptions options)
        {
            this.instance = XDocument.Load(reader, options);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the XML declaration for this document.
        /// </summary>
        public XDeclaration Declaration { get; set; }

        /// <summary>
        ///     Gets the original of <see cref="XDocument"/>.
        /// </summary>
        public XDocument Document
        {
            get
            {
                return this.instance;
            }
        }

        /// <summary>
        ///     Gets the Document Type Definition (DTD) for this document.
        /// </summary>
        public XDocumentType DocumentType { get; private set; }

        /// <summary>
        ///     Gets the node type for this node.
        /// </summary>
        /// <remarks>
        ///     This property will always return XmlNodeType.Document.
        /// </remarks>
        public XmlNodeType NodeType { get; private set; }

        /// <summary>
        ///     Gets the root element of the XML Tree for this document.
        /// </summary>
        public XElement Root { get; private set; }

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
        public void Save(string fileName)
        {
            this.instance.Save(fileName);
        }

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
        public void Save(string fileName,
                         SaveOptions options)
        {
            this.instance.Save(fileName, options);
        }

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
        public void Save(Stream stream)
        {
            this.instance.Save(stream);
        }

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
        public void Save(Stream stream,
                         SaveOptions options)
        {
            this.instance.Save(stream, options);
        }

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
        public void Save(TextWriter textWriter)
        {
            this.instance.Save(textWriter);
        }

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
        public void Save(TextWriter textWriter,
                         SaveOptions options)
        {
            this.instance.Save(textWriter, options);
        }

        /// <summary>
        ///     Output this <see cref="XDocument"/> to an <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"/> to output the XML to.
        /// </param>
        public void Save(XmlWriter writer)
        {
            this.instance.Save(writer);
        }

        /// <summary>
        ///     Output this <see cref="XDocument"/>'s underlying XML tree to the
        /// passed in <see cref="XmlWriter"/>.
        /// <seealso cref="XDocument.Save(XmlWriter)"/>
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="XmlWriter"/> to output the content of this 
        /// <see cref="XDocument"/>.
        /// </param>
        public void WriteTo(XmlWriter writer)
        {
            this.instance.WriteTo(writer);
        }

        #endregion
    }
}