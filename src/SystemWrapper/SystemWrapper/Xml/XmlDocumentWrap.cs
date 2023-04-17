namespace SystemWrapper.Xml
{
    using System.Collections;
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.XPath;

    using SystemInterface.Xml;

    /// <summary>
    ///     Wrapper for the <see cref="XmlDocument"/> class.
    /// </summary>
    public class XmlDocumentWrap : IXmlDocument
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlDocumentWrap"/> class.
        /// </summary>
        public XmlDocumentWrap()
        {
            this.Initialize();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlDocumentWrap"/> class.
        /// </summary>
        /// <param name="nt">
        ///     The nt.
        /// </param>
        public XmlDocumentWrap(XmlNameTable nt)
        {
            this.Initialize(nt);
        }

        #endregion

        #region Public Events

        public event XmlNodeChangedEventHandler NodeChanged;

        public event XmlNodeChangedEventHandler NodeChanging;

        public event XmlNodeChangedEventHandler NodeInserted;

        public event XmlNodeChangedEventHandler NodeInserting;

        public event XmlNodeChangedEventHandler NodeRemoved;

        public event XmlNodeChangedEventHandler NodeRemoving;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets an XmlAttributeCollection containing the attributes of this node.
        /// </summary>
        public XmlAttributeCollection Attributes
        {
            get
            {
                return this.XmlDocumentInstance.Attributes;
            }
        }

        /// <summary>
        ///     Gets the base URI of the current node.
        /// </summary>
        public string BaseURI
        {
            get
            {
                return this.XmlDocumentInstance.BaseURI;
            }
        }

        /// <summary>
        ///     Gets all the child nodes of the node.
        /// </summary>
        public XmlNodeList ChildNodes
        {
            get
            {
                return this.XmlDocumentInstance.ChildNodes;
            }
        }

        /// <summary>
        ///     Gets the root XmlElement for the document.
        /// </summary>
        public XmlElement DocumentElement
        {
            get
            {
                return this.XmlDocumentInstance.DocumentElement;
            }
        }

        /// <summary>
        ///     Gets the node for the DOCTYPE declaration.
        /// </summary>
        public XmlDocumentType DocumentType
        {
            get
            {
                return this.XmlDocumentInstance.DocumentType;
            }
        }

        /// <summary>
        ///     Gets the first child of this node.
        /// </summary>
        public XmlNode FirstChild
        {
            get
            {
                return this.XmlDocumentInstance.FirstChild;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this node has any child nodes.
        /// </summary>
        public bool HasChildNodes
        {
            get
            {
                return this.XmlDocumentInstance.HasChildNodes;
            }
        }

        /// <summary>
        ///     Gets the XmlImplementation object for this document.
        /// </summary>
        public XmlImplementation Implementation
        {
            get
            {
                return this.XmlDocumentInstance.Implementation;
            }
        }

        public string InnerText
        {
            set
            {
                this.XmlDocumentInstance.InnerText = value;
            }
        }

        public string InnerXml
        {
            get
            {
                return this.XmlDocumentInstance.InnerXml;
            }

            set
            {
                this.XmlDocumentInstance.InnerXml = value;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the node is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return this.XmlDocumentInstance.IsReadOnly;
            }
        }

        /// <summary>
        ///     Gets the last child of this node.
        /// </summary>
        public XmlNode LastChild
        {
            get
            {
                return this.XmlDocumentInstance.LastChild;
            }
        }

        /// <summary>
        ///     Gets the name of the current node without the namespace prefix.
        /// </summary>
        public string LocalName
        {
            get
            {
                return this.XmlDocumentInstance.LocalName;
            }
        }

        /// <summary>
        ///     Gets the name of the node.
        /// </summary>
        public string Name
        {
            get
            {
                return this.XmlDocumentInstance.Name;
            }
        }

        /// <summary>
        ///     Gets the XmlNameTable associated with this implementation.
        /// </summary>
        public XmlNameTable NameTable
        {
            get
            {
                return this.XmlDocumentInstance.NameTable;
            }
        }

        /// <summary>
        ///     Gets the namespace URI of this node.
        /// </summary>
        public string NamespaceURI
        {
            get
            {
                return this.XmlDocumentInstance.NamespaceURI;
            }
        }

        /// <summary>
        ///     Gets the node immediately following this node.
        /// </summary>
        public XmlNode NextSibling
        {
            get
            {
                return this.XmlDocumentInstance.NextSibling;
            }
        }

        /// <summary>
        ///     Gets the type of the current node.
        /// </summary>
        public XmlNodeType NodeType
        {
            get
            {
                return this.XmlDocumentInstance.NodeType;
            }
        }

        /// <summary>
        ///     Gets the markup representing this node and all its children.
        /// </summary>
        public string OuterXml
        {
            get
            {
                return this.XmlDocumentInstance.OuterXml;
            }
        }

        /// <summary>
        ///     Gets the XmlDocument that contains this node.
        /// </summary>
        public XmlDocument OwnerDocument
        {
            get
            {
                return this.XmlDocumentInstance.OwnerDocument;
            }
        }

        public XmlNode ParentNode
        {
            get
            {
                return this.XmlDocumentInstance.ParentNode;
            }
        }

        /// <summary>
        ///     Gets or sets the namespace prefix of this node.
        /// </summary>
        public string Prefix
        {
            get
            {
                return this.XmlDocumentInstance.Prefix;
            }

            set
            {
                this.XmlDocumentInstance.Prefix = value;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to preserve whitespace.
        /// </summary>
        public bool PreserveWhitespace
        {
            get
            {
                return this.XmlDocumentInstance.PreserveWhitespace;
            }

            set
            {
                this.XmlDocumentInstance.PreserveWhitespace = value;
            }
        }

        /// <summary>
        ///     Gets the node immediately preceding this node.
        /// </summary>
        public XmlNode PreviousSibling
        {
            get
            {
                return this.XmlDocumentInstance.PreviousSibling;
            }
        }

        public IXmlSchemaInfo SchemaInfo
        {
            get
            {
                return this.XmlDocumentInstance.SchemaInfo;
            }
        }

        public XmlSchemaSet Schemas
        {
            get
            {
                return this.XmlDocumentInstance.Schemas;
            }

            set
            {
                this.XmlDocumentInstance.Schemas = value;
            }
        }

        /// <summary>
        ///     Gets or sets the value of the node.
        /// </summary>
        public string Value
        {
            get
            {
                return this.XmlDocumentInstance.Value;
            }

            set
            {
                this.XmlDocumentInstance.Value = value;
            }
        }

        /// <summary>
        ///     Gets the xml document instance.
        /// </summary>
        public XmlDocument XmlDocumentInstance { get; private set; }

        public XmlResolver XmlResolver
        {
            set
            {
                this.XmlDocumentInstance.XmlResolver = value;
            }
        }

        #endregion

        #region Explicit Interface Indexers

        /// <summary>
        ///     Retrieves the first child element with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        XmlElement IXmlDocument.this[string name]
        {
            get
            {
                return this.XmlDocumentInstance[name];
            }
        }

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
        XmlElement IXmlDocument.this[string localname,
                                     string ns]
        {
            get
            {
                return this.XmlDocumentInstance[localname, ns];
            }
        }

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
        public XmlNode AppendChild(XmlNode newChild)
        {
            return this.XmlDocumentInstance.AppendChild(newChild);
        }

        /// <summary>
        ///     Creates a duplicate of this node.
        /// </summary>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode Clone()
        {
            return this.XmlDocumentInstance.Clone();
        }

        /// <summary>
        ///     Creates a duplicate of this node.
        /// </summary>
        /// <param name="deep">
        ///     The deep.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode CloneNode(bool deep)
        {
            return this.XmlDocumentInstance.CloneNode(deep);
        }

        /// <summary>
        ///     Creates an XmlAttribute with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlAttribute"/>.
        /// </returns>
        public XmlAttribute CreateAttribute(string name)
        {
            return this.XmlDocumentInstance.CreateAttribute(name);
        }

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
        public XmlAttribute CreateAttribute(string qualifiedName,
                                            string namespaceURI)
        {
            return this.XmlDocumentInstance.CreateAttribute(qualifiedName, namespaceURI);
        }

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
        public XmlAttribute CreateAttribute(string prefix,
                                            string localName,
                                            string namespaceURI)
        {
            return this.XmlDocumentInstance.CreateAttribute(prefix, localName, namespaceURI);
        }

        /// <summary>
        ///     Creates a XmlCDataSection containing the specified data.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlCDataSection"/>.
        /// </returns>
        public XmlCDataSection CreateCDataSection(string data)
        {
            return this.XmlDocumentInstance.CreateCDataSection(data);
        }

        /// <summary>
        ///     Creates an XmlComment containing the specified data.
        /// </summary>
        /// <param name="data">
        ///     The data.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlComment"/>.
        /// </returns>
        public XmlComment CreateComment(string data)
        {
            return this.XmlDocumentInstance.CreateComment(data);
        }

        /// <summary>
        ///     Creates an XmlDocumentFragment.
        /// </summary>
        /// <returns>
        ///     The <see cref="XmlDocumentFragment"/>.
        /// </returns>
        public XmlDocumentFragment CreateDocumentFragment()
        {
            return this.XmlDocumentInstance.CreateDocumentFragment();
        }

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
        public XmlDocumentType CreateDocumentType(string name,
                                                  string publicId,
                                                  string systemId,
                                                  string internalSubset)
        {
            return this.XmlDocumentInstance.CreateDocumentType(name, publicId, systemId, internalSubset);
        }

        /// <summary>
        ///     Creates an element with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        public XmlElement CreateElement(string name)
        {
            return this.XmlDocumentInstance.CreateElement(name);
        }

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
        public XmlElement CreateElement(string qualifiedName,
                                        string namespaceURI)
        {
            return this.XmlDocumentInstance.CreateElement(qualifiedName, namespaceURI);
        }

        public XmlElement CreateElement(string prefix,
                                        string localName,
                                        string namespaceURI)
        {
            return this.XmlDocumentInstance.CreateElement(prefix, localName, namespaceURI);
        }

        /// <summary>
        ///     Creates an XmlEntityReference with the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlEntityReference"/>.
        /// </returns>
        public XmlEntityReference CreateEntityReference(string name)
        {
            return this.XmlDocumentInstance.CreateEntityReference(name);
        }

        public XPathNavigator CreateNavigator()
        {
            return this.XmlDocumentInstance.CreateNavigator();
        }

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
        public XmlNode CreateNode(XmlNodeType type,
                                  string prefix,
                                  string name,
                                  string namespaceURI)
        {
            return this.XmlDocumentInstance.CreateNode(type, prefix, name, namespaceURI);
        }

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
        public XmlNode CreateNode(string nodeTypeString,
                                  string name,
                                  string namespaceURI)
        {
            return this.XmlDocumentInstance.CreateNode(nodeTypeString, name, namespaceURI);
        }

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
        public XmlNode CreateNode(XmlNodeType type,
                                  string name,
                                  string namespaceURI)
        {
            return this.XmlDocumentInstance.CreateNode(type, name, namespaceURI);
        }

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
        public XmlProcessingInstruction CreateProcessingInstruction(string target,
                                                                    string data)
        {
            return this.XmlDocumentInstance.CreateProcessingInstruction(target, data);
        }

        /// <summary>
        ///     Creates a XmlSignificantWhitespace node.
        /// </summary>
        /// <param name="text">
        ///     The text.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlSignificantWhitespace"/>.
        /// </returns>
        public XmlSignificantWhitespace CreateSignificantWhitespace(string text)
        {
            return this.XmlDocumentInstance.CreateSignificantWhitespace(text);
        }

        /// <summary>
        ///     Creates an XmlText with the specified text.
        /// </summary>
        /// <param name="text">
        ///     The text.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlText"/>.
        /// </returns>
        public XmlText CreateTextNode(string text)
        {
            return this.XmlDocumentInstance.CreateTextNode(text);
        }

        /// <summary>
        ///      Creates a XmlWhitespace node.
        /// </summary>
        /// <param name="text">
        ///     The text.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlWhitespace"/>.
        /// </returns>
        public XmlWhitespace CreateWhitespace(string text)
        {
            return this.XmlDocumentInstance.CreateWhitespace(text);
        }

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
        public XmlDeclaration CreateXmlDeclaration(string version,
                                                   string encoding,
                                                   string standalone)
        {
            return this.XmlDocumentInstance.CreateXmlDeclaration(version, encoding, standalone);
        }

        /// <summary>
        ///     Returns the XmlElement with the specified ID.
        /// </summary>
        /// <param name="elementId">
        ///     The element id.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlElement"/>.
        /// </returns>
        public XmlElement GetElementById(string elementId)
        {
            return this.XmlDocumentInstance.GetElementById(elementId);
        }

        /// <summary>
        ///     Returns an XmlNodeList containing a list of all descendant elements that match the specified name.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNodeList"/>.
        /// </returns>
        public XmlNodeList GetElementsByTagName(string name)
        {
            return this.XmlDocumentInstance.GetElementsByTagName(name);
        }

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
        public XmlNodeList GetElementsByTagName(string localName,
                                                string namespaceURI)
        {
            return this.XmlDocumentInstance.GetElementsByTagName(localName, namespaceURI);
        }

        public IEnumerator GetEnumerator()
        {
            return this.XmlDocumentInstance.GetEnumerator();
        }

        /// <summary>
        ///     Looks up the closest xmlns declaration for the given prefix that is in scope for the current node and returns the namespace URI in the declaration.
        /// </summary>
        /// <param name="prefix">
        ///     The prefix.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        public string GetNamespaceOfPrefix(string prefix)
        {
            return this.XmlDocumentInstance.GetNamespaceOfPrefix(prefix);
        }

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
        public string GetPrefixOfNamespace(string namespaceURI)
        {
            return this.XmlDocumentInstance.GetPrefixOfNamespace(namespaceURI);
        }

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
        public XmlNode ImportNode(XmlNode node,
                                  bool deep)
        {
            return this.XmlDocumentInstance.ImportNode(node, deep);
        }

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
        public XmlNode InsertAfter(XmlNode newChild,
                                   XmlNode refChild)
        {
            return this.XmlDocumentInstance.InsertAfter(newChild, refChild);
        }

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
        public XmlNode InsertBefore(XmlNode newChild,
                                    XmlNode refChild)
        {
            return this.XmlDocumentInstance.InsertBefore(newChild, refChild);
        }

        /// <summary>
        ///     Loads the XML document from the specified URL.
        /// </summary>
        /// <param name="filename">
        ///     The filename.
        /// </param>
        public void Load(string filename)
        {
            this.XmlDocumentInstance.Load(filename);
        }

        public void Load(Stream inStream)
        {
            this.XmlDocumentInstance.Load(inStream);
        }

        /// <summary>
        ///     Loads the XML document from the specified TextReader.
        /// </summary>
        /// <param name="txtReader">
        ///     The txt reader.
        /// </param>
        public void Load(TextReader txtReader)
        {
            this.XmlDocumentInstance.Load(txtReader);
        }

        /// <summary>
        ///     Loads the XML document from the specified XmlReader.
        /// </summary>
        /// <param name="reader">
        ///     The reader.
        /// </param>
        public void Load(XmlReader reader)
        {
            this.XmlDocumentInstance.Load(reader);
        }

        /// <summary>
        ///     Loads the XML document from the specified string.
        /// </summary>
        /// <param name="xml">
        ///     The xml.
        /// </param>
        public void LoadXml(string xml)
        {
            this.XmlDocumentInstance.LoadXml(xml);
        }

        /// <summary>
        ///     Puts all XmlText nodes in the full depth of the sub-tree underneath this XmlNode into a "normal" form where only markup (e.g., tags, comments, 
        ///     processing instructions, CDATA sections, and entity references) separates XmlText nodes, that is, there are no adjacent XmlText nodes.
        /// </summary>
        public void Normalize()
        {
            this.XmlDocumentInstance.Normalize();
        }

        /// <summary>
        ///     Adds the specified node to the beginning of the list of children of this node.
        /// </summary>
        /// <param name="newChild">
        ///     The new child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode PrependChild(XmlNode newChild)
        {
            return this.XmlDocumentInstance.PrependChild(newChild);
        }

        /// <summary>
        ///     Creates an XmlNode object based on the information in the XmlReader. The reader must be positioned on a node or attribute.
        /// </summary>
        /// <param name="reader">
        ///     The reader.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode ReadNode(XmlReader reader)
        {
            return this.XmlDocumentInstance.ReadNode(reader);
        }

        /// <summary>
        ///     Removes all the children and/or attributes of the current node.
        /// </summary>
        public void RemoveAll()
        {
            this.XmlDocumentInstance.RemoveAll();
        }

        /// <summary>
        ///     Removes specified child node.
        /// </summary>
        /// <param name="oldChild">
        ///     The old child.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode RemoveChild(XmlNode oldChild)
        {
            return this.XmlDocumentInstance.RemoveChild(oldChild);
        }

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
        public XmlNode ReplaceChild(XmlNode newChild,
                                    XmlNode oldChild)
        {
            return this.XmlDocumentInstance.ReplaceChild(newChild, oldChild);
        }

        /// <summary>
        ///     Saves out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="filename">
        ///     The filename.
        /// </param>
        public void Save(string filename)
        {
            this.XmlDocumentInstance.Save(filename);
        }

        /// <summary>
        ///     Saves out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="outStream">
        ///     The out stream.
        /// </param>
        public void Save(Stream outStream)
        {
            this.XmlDocumentInstance.Save(outStream);
        }

        /// <summary>
        ///     Saves out the file with xmldeclaration which has encoding value equal to that of textwriter's encoding
        /// </summary>
        /// <param name="writer">
        ///     The writer.
        /// </param>
        public void Save(TextWriter writer)
        {
            this.XmlDocumentInstance.Save(writer);
        }

        /// <summary>
        ///     Saves out the file with xmldeclaration which has encoding value equal to that of textwriter's encoding
        /// </summary>
        /// <param name="w">
        ///     The w.
        /// </param>
        public void Save(XmlWriter w)
        {
            this.XmlDocumentInstance.Save(w);
        }

        /// <summary>
        ///     Selects all nodes that match the xpath expression
        /// </summary>
        /// <param name="xpath">
        ///     The xpath.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNodeList"/>.
        /// </returns>
        public XmlNodeList SelectNodes(string xpath)
        {
            return this.XmlDocumentInstance.SelectNodes(xpath);
        }

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
        public XmlNodeList SelectNodes(string xpath,
                                       XmlNamespaceManager nsmgr)
        {
            return this.XmlDocumentInstance.SelectNodes(xpath, nsmgr);
        }

        /// <summary>
        ///     Selects the first node that matches the xpath expression
        /// </summary>
        /// <param name="xpath">
        ///     The xpath.
        /// </param>
        /// <returns>
        ///     The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode SelectSingleNode(string xpath)
        {
            return this.XmlDocumentInstance.SelectSingleNode(xpath);
        }

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
        public XmlNode SelectSingleNode(string xpath,
                                        XmlNamespaceManager nsmgr)
        {
            return this.XmlDocumentInstance.SelectSingleNode(xpath, nsmgr);
        }

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
        public bool Supports(string feature,
                             string version)
        {
            return this.XmlDocumentInstance.Supports(feature, version);
        }

        public void Validate(ValidationEventHandler validationEventHandler)
        {
            this.XmlDocumentInstance.Validate(validationEventHandler);
        }

        public void Validate(ValidationEventHandler validationEventHandler,
                             XmlNode nodeToValidate)
        {
            this.XmlDocumentInstance.Validate(validationEventHandler, nodeToValidate);
        }

        /// <summary>
        ///     Writes out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="xw">
        ///     The xw.
        /// </param>
        public void WriteContentTo(XmlWriter xw)
        {
            this.XmlDocumentInstance.WriteContentTo(xw);
        }

        /// <summary>
        ///     Writes out the to the file with exact content in the XmlDocument.
        /// </summary>
        /// <param name="w">
        ///     The w.
        /// </param>
        public void WriteTo(XmlWriter w)
        {
            this.XmlDocumentInstance.WriteTo(w);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            this.XmlDocumentInstance = new XmlDocument();
        }

        private void Initialize(XmlNameTable nt)
        {
            this.XmlDocumentInstance = new XmlDocument(nt);
        }

        #endregion
    }
}