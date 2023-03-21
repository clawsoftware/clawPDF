namespace SystemInterface.Xml
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    ///   Defines the contract for the factory responsible for the creation of an instance of <see cref="IXDocument"/> class.
    /// </summary>
    public interface IXDocumentFactory
    {
        #region Public Methods and Operators

        /// <overloads>
        ///      Initializes a new instance of the <see cref="IXDocument"/> class.  Overloaded constructors are provided for creating a new empty <see cref="IXDocument"/>, creating an <see cref="IXDocument"/> with a parameter list of initial content, and as a copy of another <see cref="IXDocument"/> object.
        ///  </overloads>
        ///  <summary>
        ///      Initializes a new instance of the <see cref="IXDocument"/> class.
        ///  </summary>
        IXDocument Create();

        /// <summary>
        ///     Initializes a new instance of the <see cref="IXDocument"/> class with the specified content.
        /// </summary>
        /// <param name="content">
        ///     A parameter list of content objects to add to this document.
        /// </param>
        /// <remarks>
        /// Valid content includes:
        /// <list>
        /// <item>Zero or one <see cref="XDocumentType"/> objects</item>
        /// <item>Zero or one elements</item>
        /// <item>Zero or more comments</item>
        /// <item>Zero or more processing instructions</item>
        /// </list>
        ///     See XContainer.Add(object content) for details about the content that can be added using this method.
        /// </remarks>
        IXDocument Create(params object[] content);

        /// <summary>
        ///     Initializes a new instance of the <see cref="IXDocument"/> class with the specifed <see cref="XDeclaration"/> and content.
        /// </summary>
        /// <param name="declaration">
        ///     The XML declaration for the document.
        /// </param>
        /// <param name="content">
        ///     The contents of the document.
        /// </param>
        /// <remarks>
        ///     Valid content includes:
        /// <list>
        /// <item>Zero or one <see cref="XDocumentType"/> objects</item>
        /// <item>Zero or one elements</item>
        /// <item>Zero or more comments</item>
        /// <item>Zero or more processing instructions</item>
        /// <item></item>
        /// </list>
        ///     See XContainer.Add(object content) for details about the content that can be added using this method.
        /// </remarks>
        IXDocument Create(XDeclaration declaration,
                          params object[] content);

        /// <summary>
        ///     Initializes a new instance of the <see cref="IXDocument"/> class from an existing XDocument object.
        /// </summary>
        /// <param name="other">
        ///     The <see cref="IXDocument"/> object that will be copied.
        /// </param>
        IXDocument Create(IXDocument other);

        /// <overloads>
        ///     The Load method provides multiple strategies for creating a new 
        /// <see cref="IXDocument"/> and initializing it from a data source containing
        /// raw XML.  Load from a file (passing in a URI to the file), a
        /// <see cref="Stream"/>, a <see cref="TextReader"/>, or an
        /// <see cref="XmlReader"/>.  Note:  Use <see cref="XDocument.Parse(string)"/>
        /// to create an <see cref="IXDocument"/> from a string containing XML.
        /// <seealso cref="XDocument.Parse(string)"/>
        /// </overloads>
        /// <summary>
        ///     Create a new <see cref="IXDocument"/> based on the contents of the file 
        /// referenced by the URI parameter passed in.  Note: Use 
        /// <see cref="XDocument.Parse(string)"/> to create an <see cref="IXDocument"/> from
        /// a string containing XML.
        /// <seealso cref="XmlReader.Create(string)"/>
        /// <seealso cref="XDocument.Parse(string)"/>
        /// </summary>
        /// <remarks>
        ///     This method uses the <see cref="XmlReader.Create(string)"/> method to create
        /// an <see cref="XmlReader"/> to read the raw XML into the underlying
        /// XML tree.
        /// </remarks>
        /// <param name="uri">
        ///     A URI string referencing the file to load into a new <see cref="IXDocument"/>.
        /// </param>
        /// <returns>
        ///     An <see cref="IXDocument"/> initialized with the contents of the file referenced
        /// in the passed in uri parameter.
        /// </returns>
        IXDocument Load(string uri);

        /// <summary>
        ///     Create a new <see cref="IXDocument"/> based on the contents of the file 
        /// referenced by the URI parameter passed in.  Optionally, whitespace can be preserved.  
        /// <see cref="XmlReader.Create(string)"/>
        /// </summary>
        /// <remarks>
        ///     This method uses the <see cref="XmlReader.Create(string)"/> method to create
        /// an <see cref="XmlReader"/> to read the raw XML into an underlying
        /// XML tree.  If LoadOptions.PreserveWhitespace is enabled then
        /// the <see cref="XmlReaderSettings"/> property <see cref="XmlReaderSettings.IgnoreWhitespace"/>
        /// is set to false.
        /// </remarks>
        /// <param name="uri">
        ///     A string representing the URI of the file to be loaded into a new <see cref="IXDocument"/>.
        /// </param>
        /// <param name="options">
        ///     A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        ///     An <see cref="IXDocument"/> initialized with the contents of the file referenced
        /// in the passed uri parameter.  If LoadOptions.PreserveWhitespace is enabled then
        /// all whitespace will be preserved.
        /// </returns>
        IXDocument Load(string uri,
                        LoadOptions options);

        /// <summary>
        ///     Create a new <see cref="IXDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="Stream"/> parameter.  
        /// </summary>
        /// <param name="stream">
        ///     A <see cref="Stream"/> containing the raw XML to read into the newly
        ///     created <see cref="IXDocument"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IXDocument"/> containing the contents of the passed in
        /// <see cref="Stream"/>.
        /// </returns>
        IXDocument Load(Stream stream);

        /// <summary>
        ///     Create a new <see cref="IXDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="Stream"/> parameter.  Optionally whitespace handling
        /// can be preserved.
        /// </summary>
        /// <remarks>
        ///     If LoadOptions.PreserveWhitespace is enabled then
        /// the underlying <see cref="XmlReaderSettings"/> property <see cref="XmlReaderSettings.IgnoreWhitespace"/>
        /// is set to false.
        /// </remarks>
        /// <param name="stream">
        ///     A <see cref="Stream"/> containing the raw XML to read into the newly
        ///     created <see cref="IXDocument"/>.
        /// </param>
        /// <param name="options">
        ///     A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IXDocument"/> containing the contents of the passed in
        /// <see cref="Stream"/>.
        /// </returns>
        IXDocument Load(Stream stream,
                        LoadOptions options);

        /// <summary>
        ///     Create a new <see cref="IXDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="TextReader"/> parameter.  
        /// </summary>
        /// <param name="textReader">
        ///     A <see cref="TextReader"/> containing the raw XML to read into the newly
        ///     created <see cref="IXDocument"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IXDocument"/> containing the contents of the passed in
        /// <see cref="TextReader"/>.
        /// </returns>
        IXDocument Load(TextReader textReader);

        /// <summary>
        ///     Create a new <see cref="IXDocument"/> and initialize its underlying XML tree using
        /// the passed <see cref="TextReader"/> parameter.  Optionally whitespace handling
        /// can be preserved.
        /// </summary>
        /// <remarks>
        ///     If LoadOptions.PreserveWhitespace is enabled then
        /// the <see cref="XmlReaderSettings"/> property <see cref="XmlReaderSettings.IgnoreWhitespace"/>
        /// is set to false.
        /// </remarks>
        /// <param name="textReader">
        ///     A <see cref="TextReader"/> containing the raw XML to read into the newly
        ///     created <see cref="IXDocument"/>.
        /// </param>
        /// <param name="options">
        ///     A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IXDocument"/> containing the contents of the passed in
        /// <see cref="TextReader"/>.
        /// </returns>
        IXDocument Load(TextReader textReader,
                        LoadOptions options);

        /// <summary>
        ///     Create a new <see cref="IXDocument"/> containing the contents of the
        /// passed in <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="XmlReader"/> containing the XML to be read into the new
        ///     <see cref="IXDocument"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IXDocument"/> containing the contents of the passed
        /// in <see cref="XmlReader"/>.
        /// </returns>
        IXDocument Load(XmlReader reader);

        /// <summary>
        ///     Create a new <see cref="IXDocument"/> containing the contents of the
        /// passed in <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="XmlReader"/> containing the XML to be read into the new
        ///     <see cref="IXDocument"/>.
        /// </param>
        /// <param name="options">
        ///     A set of <see cref="LoadOptions"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IXDocument"/> containing the contents of the passed
        /// in <see cref="XmlReader"/>.
        /// </returns>
        IXDocument Load(XmlReader reader,
                        LoadOptions options);

        #endregion
    }
}