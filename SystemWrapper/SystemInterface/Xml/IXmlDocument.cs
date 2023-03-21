namespace SystemInterface.Xml
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.XPath;

    /// <summary>
    ///     Wrapper for the <see cref="XmlDocument"/> class.
    /// </summary>
    public interface IXmlDocument
    {
        #region Public Events

        event XmlNodeChangedEventHandler NodeChanged;

        event XmlNodeChangedEventHandler NodeChanging;

        event XmlNodeChangedEventHandler NodeInserted;

        event XmlNodeChangedEventHandler NodeInserting;

        event XmlNodeChangedEventHandler NodeRemoved;

        event XmlNodeChangedEventHandler NodeRemoving;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets an XmlAttributeCollection containing the attributes of this node.
        /// </summary>
        XmlAttributeCollection Attributes { get; }

        /// <summary>
        ///     Gets the base URI of the current node.
        /// </summary>
        String BaseURI { get; }

        /// <summary>
        ///     Gets all the child nodes of the node.
        /// </summary>
        XmlNodeList ChildNodes { get; }

        /// <summary>
        ///     Gets the root XmlElement for the document.
        /// </summary>
        XmlElement DocumentElement { get; }

        /// <summary>
        ///     Gets the node for the DOCTYPE declaration.
        /// </summary>
        XmlDocumentType DocumentType { get; }

        /// <summary>
        ///     Gets the first child of this node.
        /// </summary>
        XmlNode FirstChild { get; }

        /// <summary>
        ///     Gets a value indicating whether this node has any child nodes.
        /// </summary>
        bool HasChildNodes { get; }

        /// <summary>
        ///     Gets the XmlImplementation object for this document.
        /// </summary>
        XmlImplementation Implementation { get; }

        string InnerText { set; }

        string InnerXml { get; set; }

        /// <summary>
        ///     Gets a value indicating whether the node is read-only.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        ///     Gets the last child of this node.
        /// </summary>
        XmlNode LastChild { get; }

        /// <summary>
        ///     Gets the name of the current node without the namespace prefix.
        /// </summary>
        String LocalName { get; }

        /// <summary>
        ///     Gets the name of the node.
        /// </summary>
        String Name { get; }

        /// <summary>
        ///     Gets the XmlNameTable associated with this implementation.
        /// </summary>
        XmlNameTable NameTable { get; }

        /// <summary>
        ///     Gets the namespace URI of this node.
        /// </summary>
        string NamespaceURI { get; }

        /// <summary>
        ///     Gets the node immediately following this node.
        /// </summary>
        XmlNode NextSibling { get; }

        /// <summary>
        ///     Gets the type of the current node.
        /// </summary>
        XmlNodeType NodeType { get; }

        /// <summary>
        ///     Gets the markup representing this node and all its children.
        /// </summary>
        string OuterXml { get; }

        /// <summary>
        ///     Gets the XmlDocument that contains this node.
        /// </summary>
        XmlDocument OwnerDocument { get; }

        XmlNode ParentNode { get; }

        /// <summary>
        ///     Gets or sets the namespace prefix of this node.
        /// </summary>
        string Prefix { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to preserve whitespace.
        /// </summary>
        bool PreserveWhitespace { get; set; }

        /// <summary>
        ///     Gets the node immediately preceding this node.
        /// </summary>
        XmlNode PreviousSibling { get; }

        IXmlSchemaInfo SchemaInfo { get; }

        XmlSchemaSet Schemas { get; set; }

        /// <summary>
        ///     Gets or sets the value of the node.
        /// </summary>
        string Value { get; set; }

        XmlResolver XmlResolver { set; }

        #endregion

        #region Public Indexers

        /// <summary>
        ///     Retrieves the first child element with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        XmlElement this[string name] { get; }

        /// <summary>
        ///     Retrieves the first child element with the specified LocalName and NamespaceURI.
        /// </summary>
        /// <param name="localname">
        ///     The localname.
        /// </param>
        /// <param name="ns">
        ///     The ns.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        XmlElement this[string localname,
                        string ns] { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Adds the specified node to the end of the list of children of this node.
        /// </summary>
        /// <param name="newChild">
        ///     The new child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode AppendChild(XmlNode newChild);

        /// <summary>
        ///     Creates a duplicate of this node.
        /// </summary>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode Clone();

        /// <summary>
        ///     Creates a duplicate of this node.
        /// </summary>
        /// <param name="deep">
        ///     The deep.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode CloneNode(bool deep);

        /// <summary>
        ///     Creates an XmlAttribute with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlAttribute"/>.
        /// </returns>
        XmlAttribute CreateAttribute(String name);

        /// <summary>
        ///     Creates an XmlAttribute with the specified LocalName and NamespaceURI.
        /// </summary>
        /// <param name="qualifiedName">
        ///     The qualified name.
        /// </param>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlAttribute"/>.
        /// </returns>
        XmlAttribute CreateAttribute(String qualifiedName,
                                     String namespaceURI);

        /// <summary>
        ///     Creates a XmlAttribute with the specified Prefix, LocalName, and NamespaceURI.
        /// </summary>
        /// <param name="prefix">
        ///     The prefix.
        /// </param>
        /// <param name="localName">
        ///     The local name.
        /// </param>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlAttribute"/>.
        /// </returns>
        XmlAttribute CreateAttribute(string prefix,
                                     string localName,
                                     string namespaceURI);

        /// <summary>
        ///     Creates a XmlCDataSection containing the specified data.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlCDataSection"/>.
        /// </returns>
        XmlCDataSection CreateCDataSection(String data);

        /// <summary>
        ///     Creates an XmlComment containing the specified data.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlComment"/>.
        /// </returns>
        XmlComment CreateComment(String data);

        /// <summary>
        ///     Creates an XmlDocumentFragment.
        /// </summary>
        /// <returns>
        ///     The <see cref="XmlDocumentFragment"/>.
        /// </returns>
        XmlDocumentFragment CreateDocumentFragment();

        /// <summary>
        ///     Returns a new XmlDocumentType object.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="publicId">
        ///     The public id.
        /// </param>
        /// <param name="systemId">
        ///     The system id.
        /// </param>
        /// <param name="internalSubset">
        ///     The internal subset.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlDocumentType"/>.
        /// </returns>
        XmlDocumentType CreateDocumentType(string name,
                                           string publicId,
                                           string systemId,
                                           string internalSubset);

        /// <summary>
        ///     Creates an element with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        XmlElement CreateElement(String name);

        /// <summary>
        ///     Creates an XmlElement with the specified LocalName and NamespaceURI.
        /// </summary>
        /// <param name="qualifiedName">
        ///     The qualified name.
        /// </param>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        XmlElement CreateElement(String qualifiedName,
                                 String namespaceURI);

        XmlElement CreateElement(string prefix,
                                 string localName,
                                 string namespaceURI);

        /// <summary>
        ///     Creates an XmlEntityReference with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlEntityReference"/>.
        /// </returns>
        XmlEntityReference CreateEntityReference(String name);

        XPathNavigator CreateNavigator();

        /// <summary>
        ///     Creates a XmlNode with the specified XmlNodeType, Prefix, Name, and NamespaceURI.
        /// </summary>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="prefix">
        ///     The prefix.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode CreateNode(XmlNodeType type,
                           string prefix,
                           string name,
                           string namespaceURI);

        /// <summary>
        ///     Creates an XmlNode with the specified node type, Name, and NamespaceURI.
        /// </summary>
        /// <param name="nodeTypeString">
        ///     The node type string.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode CreateNode(string nodeTypeString,
                           string name,
                           string namespaceURI);

        /// <summary>
        ///     Creates an XmlNode with the specified XmlNodeType, Name, and NamespaceURI.
        /// </summary>
        /// <param name="type">
        ///     The type.
        /// </param>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode CreateNode(XmlNodeType type,
                           string name,
                           string namespaceURI);

        /// <summary>
        ///     Creates a XmlProcessingInstruction with the specified name and data strings.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlProcessingInstruction"/>.
        /// </returns>
        XmlProcessingInstruction CreateProcessingInstruction(String target,
                                                             String data);

        /// <summary>
        ///     Creates a XmlSignificantWhitespace node.
        /// </summary>
        /// <param name="text">
        ///     The text.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlSignificantWhitespace"/>.
        /// </returns>
        XmlSignificantWhitespace CreateSignificantWhitespace(string text);

        /// <summary>
        ///     Creates an XmlText with the specified text.
        /// </summary>
        /// <param name="text">
        ///     The text.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlText"/>.
        /// </returns>
        XmlText CreateTextNode(String text);

        /// <summary>
        ///      Creates a XmlWhitespace node.
        /// </summary>
        /// <param name="text">
        ///     The text.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlWhitespace"/>.
        /// </returns>
        XmlWhitespace CreateWhitespace(string text);

        /// <summary>
        ///     Creates a XmlDeclaration node with the specified values.
        /// </summary>
        /// <param name="version">
        ///     The version.
        /// </param>
        /// <param name="encoding">
        ///     The encoding.
        /// </param>
        /// <param name="standalone">
        ///     The standalone.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlDeclaration"/>.
        /// </returns>
        XmlDeclaration CreateXmlDeclaration(String version,
                                            string encoding,
                                            string standalone);

        bool Equals(Object obj);

        /// <summary>
        ///     Returns the XmlElement with the specified ID.
        /// </summary>
        /// <param name="elementId">
        ///     The element id.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        XmlElement GetElementById(string elementId);

        /// <summary>
        ///     Returns an XmlNodeList containing a list of all descendant elements that match the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNodeList"/>.
        /// </returns>
        XmlNodeList GetElementsByTagName(String name);

        /// <summary>
        ///     Returns a XmlNodeList containing a list of all descendant elements that match the specified name.
        /// </summary>
        /// <param name="localName">
        ///     The local name.
        /// </param>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNodeList"/>.
        /// </returns>
        XmlNodeList GetElementsByTagName(String localName,
                                         String namespaceURI);

        IEnumerator GetEnumerator();

        int GetHashCode();

        /// <summary>
        ///     Looks up the closest xmlns declaration for the given prefix that is in scope for the current node and returns the namespace URI in the declaration.
        /// </summary>
        /// <param name="prefix">
        ///     The prefix.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        string GetNamespaceOfPrefix(string prefix);

        /// <summary>
        ///     Looks up the closest xmlns declaration for the given namespace URI that is in scope for the current node and returns the prefix defined in 
        ///     that declaration.
        /// </summary>
        /// <param name="namespaceURI">
        ///     The namespace uri.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        string GetPrefixOfNamespace(string namespaceURI);

        /// <summary>
        ///     Returns a Type object which represent this object instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="Type"/>.
        /// </returns>
        Type GetType();

        /// <summary>
        ///     Imports a node from another document to this document.
        /// </summary>
        /// <param name="node">
        ///     The node.
        /// </param>
        /// <param name="deep">
        ///     The deep.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode ImportNode(XmlNode node,
                           bool deep);

        /// <summary>
        ///     Inserts the specified node immediately after the specified reference node.
        /// </summary>
        /// <param name="newChild">
        ///     The new child.
        /// </param>
        /// <param name="refChild">
        ///     The ref child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode InsertAfter(XmlNode newChild,
                            XmlNode refChild);

        /// <summary>
        ///     Inserts the specified node immediately before the specified reference node.
        /// </summary>
        /// <param name="newChild">
        ///     The new child.
        /// </param>
        /// <param name="refChild">
        ///     The ref child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode InsertBefore(XmlNode newChild,
                             XmlNode refChild);

        /// <summary>
        ///     Loads the XML document from the specified URL.
        /// </summary>
        /// <param name="filename">
        ///     The filename.
        /// </param>
        void Load(string filename);

        void Load(Stream inStream);

        /// <summary>
        ///     Loads the XML document from the specified TextReader.
        /// </summary>
        /// <param name="txtReader">
        ///     The txt reader.
        /// </param>
        void Load(TextReader txtReader);

        /// <summary>
        ///     Loads the XML document from the specified XmlReader.
        /// </summary>
        /// <param name="reader">
        ///     The reader.
        /// </param>
        void Load(XmlReader reader);

        /// <summary>
        ///     Loads the XML document from the specified string.
        /// </summary>
        /// <param name="xml">
        ///     The xml.
        /// </param>
        void LoadXml(string xml);

        /// <summary>
        ///     Puts all XmlText nodes in the full depth of the sub-tree underneath this XmlNode into a "normal" form where only markup (e.g., tags, comments, 
        ///     processing instructions, CDATA sections, and entity references) separates XmlText nodes, that is, there are no adjacent XmlText nodes.
        /// </summary>
        void Normalize();

        /// <summary>
        ///     Adds the specified node to the beginning of the list of children of this node.
        /// </summary>
        /// <param name="newChild">
        ///     The new child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode PrependChild(XmlNode newChild);

        /// <summary>
        ///     Creates an XmlNode object based on the information in the XmlReader. The reader must be positioned on a node or attribute.
        /// </summary>
        /// <param name="reader">
        ///     The reader.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode ReadNode(XmlReader reader);

        /// <summary>
        ///     Removes all the children and/or attributes of the current node.
        /// </summary>
        void RemoveAll();

        /// <summary>
        ///     Removes specified child node.
        /// </summary>
        /// <param name="oldChild">
        ///     The old child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode RemoveChild(XmlNode oldChild);

        /// <summary>
        ///     Replaces the child node oldChild with newChild node.
        /// </summary>
        /// <param name="newChild">
        ///     The new child.
        /// </param>
        /// <param name="oldChild">
        ///     The old child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode ReplaceChild(XmlNode newChild,
                             XmlNode oldChild);

        /// <summary>
        ///     Saves out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="filename">
        ///     The filename.
        /// </param>
        void Save(string filename);

        /// <summary>
        ///     Saves out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="outStream">
        ///     The out stream.
        /// </param>
        void Save(Stream outStream);

        /// <summary>
        ///     Saves out the file with xmldeclaration which has encoding value equal to that of textwriter's encoding
        /// </summary>
        /// <param name="writer">
        ///     The writer.
        /// </param>
        void Save(TextWriter writer);

        /// <summary>
        ///     Saves out the file with xmldeclaration which has encoding value equal to that of textwriter's encoding
        /// </summary>
        /// <param name="w">
        ///     The w.
        /// </param>
        void Save(XmlWriter w);

        /// <summary>
        ///     Selects all nodes that match the xpath expression
        /// </summary>
        /// <param name="xpath">
        ///     The xpath.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNodeList"/>.
        /// </returns>
        XmlNodeList SelectNodes(string xpath);

        /// <summary>
        ///     Selects all nodes that match the xpath expression and given namespace context.
        /// </summary>
        /// <param name="xpath">
        ///     The xpath.
        /// </param>
        /// <param name="nsmgr">
        ///     The nsmgr.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNodeList"/>.
        /// </returns>
        XmlNodeList SelectNodes(string xpath,
                                XmlNamespaceManager nsmgr);

        /// <summary>
        ///     Selects the first node that matches the xpath expression
        /// </summary>
        /// <param name="xpath">
        ///     The xpath.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode SelectSingleNode(string xpath);

        /// <summary>
        ///     Selects the first node that matches the xpath expression and given namespace context.
        /// </summary>
        /// <param name="xpath">
        ///     The xpath.
        /// </param>
        /// <param name="nsmgr">
        ///     The nsmgr.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        XmlNode SelectSingleNode(string xpath,
                                 XmlNamespaceManager nsmgr);

        /// <summary>
        ///     Test if the DOM implementation implements a specific feature.
        /// </summary>
        /// <param name="feature">
        ///     The feature.
        /// </param>
        /// <param name="version">
        ///     The version.
        /// </param>
        /// <returns>
        ///     The <see cref="bool"/>.
        /// </returns>
        bool Supports(string feature,
                      string version);

        /// <summary>
        ///     Returns a String which represents the object instance.  The default for an object is to return the fully qualified name of the class.
        /// </summary>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        String ToString();

        void Validate(ValidationEventHandler validationEventHandler);

        void Validate(ValidationEventHandler validationEventHandler,
                      XmlNode nodeToValidate);

        /// <summary>
        ///     Writes out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="xw">
        ///     The xw.
        /// </param>
        void WriteContentTo(XmlWriter xw);

        /// <summary>
        ///     Writes out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="w">
        ///     The w.
        /// </param>
        void WriteTo(XmlWriter w);

        #endregion
    }
}