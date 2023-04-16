namespace SystemWrapper.Xml
{
    using System;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Reflection;
    using System.Security.Policy;
    using System.Xml;
    using System.Xml.Serialization;

    using SystemInterface.Xml;

    /// <summary>
    ///     The wrapper of the <see cref="XmlSerializer"/> class.
    /// </summary>
    public class XmlSerializerWrap : IXmlSerializer
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of type <see cref="T:System.Object"/> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object"/>. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize.</param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/> that extends or overrides the behavior of the class specified in the <paramref name="type"/> parameter.</param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize.</param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that defines the XML root element properties.</param><param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param><param name="location">The location of the types.</param>
        public XmlSerializerWrap(Type type,
                                 XmlAttributeOverrides overrides,
                                 Type[] extraTypes,
                                 XmlRootAttribute root,
                                 string defaultNamespace,
                                 string location)
        {
            this.XmlSerializerInstance = new XmlSerializer(type, overrides, extraTypes, root, defaultNamespace, location);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML document instances, and deserialize XML document instances into objects of the specified type. This overload allows you to supply other types that can be encountered during a serialization or deserialization operation, as well as a default namespace for all XML elements, the class to use as the XML root element, its location, and credentials required for access.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize.</param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/> that extends or overrides the behavior of the class specified in the <paramref name="type"/> parameter.</param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize.</param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that defines the XML root element properties.</param><param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param><param name="location">The location of the types.</param><param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence"/> class that contains credentials required to access types.</param>
        [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use a XmlSerializer constructor overload which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
        public XmlSerializerWrap(Type type,
                                 XmlAttributeOverrides overrides,
                                 Type[] extraTypes,
                                 XmlRootAttribute root,
                                 string defaultNamespace,
                                 string location,
                                 Evidence evidence)
        {
            this.XmlSerializerInstance = new XmlSerializer(type, overrides, extraTypes, root, defaultNamespace, location, evidence);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of type <see cref="T:System.Object"/> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object"/>. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/> that extends or overrides the behavior of the class specified in the <paramref name="type"/> parameter. </param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize. </param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that defines the XML root element properties. </param><param name="defaultNamespace">The default namespace of all XML elements in the XML document. </param>
        public XmlSerializerWrap(Type type,
                                 XmlAttributeOverrides overrides,
                                 Type[] extraTypes,
                                 XmlRootAttribute root,
                                 string defaultNamespace)
        {
            this.XmlSerializerInstance = new XmlSerializer(type, overrides, extraTypes, root, defaultNamespace);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize an XML document into object of the specified type. It also specifies the class to use as the XML root element.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that represents the XML root element. </param>
        public XmlSerializerWrap(Type type,
                                 XmlRootAttribute root)
        {
            this.XmlSerializerInstance = new XmlSerializer(type, root);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into object of a specified type. If a property or field returns an array, the <paramref name="extraTypes"/> parameter specifies objects that can be inserted into the array.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize. </param>
        public XmlSerializerWrap(Type type,
                                 Type[] extraTypes)
        {
            this.XmlSerializerInstance = new XmlSerializer(type, extraTypes);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes.
        /// </summary>
        /// <param name="type">The type of the object to serialize. </param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/>. </param>
        public XmlSerializerWrap(Type type,
                                 XmlAttributeOverrides overrides)
        {
            this.XmlSerializerInstance = new XmlSerializer(type, overrides);
        }

        /// <summary>
        ///     Initializes an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class using an object that maps one type to another.
        /// </summary>
        /// <param name="xmlTypeMapping">An <see cref="T:System.Xml.Serialization.XmlTypeMapping"/> that maps one type to another. </param>
        public XmlSerializerWrap(XmlTypeMapping xmlTypeMapping)
        {
            this.XmlSerializerInstance = new XmlSerializer(xmlTypeMapping);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param>
        public XmlSerializerWrap(Type type)
        {
            this.XmlSerializerInstance = new XmlSerializer(type);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Specifies the default namespace for all the XML elements.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="defaultNamespace">The default namespace to use for all the XML elements. </param>
        public XmlSerializerWrap(Type type,
                                 string defaultNamespace)
        {
            this.XmlSerializerInstance = new XmlSerializer(type, defaultNamespace);
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters an XML attribute of unknown type during deserialization.
        /// </summary>
        public event XmlAttributeEventHandler UnknownAttribute
        {
            add
            {
                this.XmlSerializerInstance.UnknownAttribute += value;
            }
            remove
            {
                this.XmlSerializerInstance.UnknownAttribute -= value;
            }
        }

        /// <summary>
        ///     Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters an XML element of unknown type during deserialization.
        /// </summary>
        public event XmlElementEventHandler UnknownElement
        {
            add
            {
                this.XmlSerializerInstance.UnknownElement += value;
            }
            remove
            {
                this.XmlSerializerInstance.UnknownElement -= value;
            }
        }

        /// <summary>
        ///     Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters an XML node of unknown type during deserialization.
        /// </summary>
        public event XmlNodeEventHandler UnknownNode
        {
            add
            {
                this.XmlSerializerInstance.UnknownNode += value;
            }
            remove
            {
                this.XmlSerializerInstance.UnknownNode -= value;
            }
        }

        /// <summary>
        ///     Occurs during deserialization of a SOAP-encoded XML stream, when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters a recognized type that is not used or is unreferenced.
        /// </summary>
        public event UnreferencedObjectEventHandler UnreferencedObject
        {
            add
            {
                this.XmlSerializerInstance.UnreferencedObject += value;
            }
            remove
            {
                this.XmlSerializerInstance.UnreferencedObject -= value;
            }
        }

        #endregion

        #region Properties

        private XmlSerializer XmlSerializerInstance { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Gets a value that indicates whether this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can deserialize a specified XML document.
        /// </summary>
        /// <returns>
        ///     true if this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can deserialize the object that the <see cref="T:System.Xml.XmlReader"/> points to; otherwise, false.
        /// </returns>
        /// <param name="xmlReader">An <see cref="T:System.Xml.XmlReader"/> that points to the document to deserialize. </param>
        public bool CanDeserialize(XmlReader xmlReader)
        {
            return this.XmlSerializerInstance.CanDeserialize(xmlReader);
        }

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> that contains the XML document to deserialize. </param>
        public object Deserialize(Stream stream)
        {
            return this.XmlSerializerInstance.Deserialize(stream);
        }

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.IO.TextReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="textReader">The <see cref="T:System.IO.TextReader"/> that contains the XML document to deserialize. </param><exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public object Deserialize(TextReader textReader)
        {
            return this.XmlSerializerInstance.Deserialize(textReader);
        }

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="xmlReader">The <see cref="T:System.xml.XmlReader"/> that contains the XML document to deserialize. </param><exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public object Deserialize(XmlReader xmlReader)
        {
            return this.XmlSerializerInstance.Deserialize(xmlReader, null);
        }

        /// <summary>
        ///     Deserializes an XML document contained by the specified <see cref="T:System.Xml.XmlReader"/> and allows the overriding of events that occur during deserialization.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader"/> that contains the document to deserialize.</param><param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents"/> class. </param>
        public object Deserialize(XmlReader xmlReader,
                                  XmlDeserializationEvents events)
        {
            return this.XmlSerializerInstance.Deserialize(xmlReader, null, events);
        }

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader"/> and encoding style.
        /// </summary>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        /// <param name="xmlReader">The <see cref="T:System.xml.XmlReader"/> that contains the XML document to deserialize. </param><param name="encodingStyle">The encoding style of the serialized XML. </param><exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public object Deserialize(XmlReader xmlReader,
                                  string encodingStyle)
        {
            return this.XmlSerializerInstance.Deserialize(xmlReader, encodingStyle);
        }

        /// <summary>
        ///     Deserializes the object using the data contained by the specified <see cref="T:System.Xml.XmlReader"/>.
        /// </summary>
        /// <returns>
        ///     The object being deserialized.
        /// </returns>
        /// <param name="xmlReader">An instance of the <see cref="T:System.Xml.XmlReader"/> class used to read the document.</param><param name="encodingStyle">The encoding used.</param><param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents"/> class. </param>
        public object Deserialize(XmlReader xmlReader,
                                  string encodingStyle,
                                  XmlDeserializationEvents events)
        {
            return this.XmlSerializerInstance.Deserialize(xmlReader, encodingStyle, events);
        }

        /// <summary>
        ///     Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects created from an array of <see cref="T:System.Xml.Serialization.XmlTypeMapping"/> objects.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects.
        /// </returns>
        /// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlTypeMapping"/> that maps one type to another. </param>
        public XmlSerializer[] FromMappings(XmlMapping[] mappings)
        {
            return XmlSerializer.FromMappings(mappings);
        }

        /// <summary>
        ///     Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class from the specified mappings.
        /// </summary>
        /// <returns>
        ///     An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class.
        /// </returns>
        /// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping"/> objects.</param><param name="type">The <see cref="T:System.Type"/> of the deserialized object.</param>
        public XmlSerializer[] FromMappings(XmlMapping[] mappings,
                                            Type type)
        {
            return XmlSerializer.FromMappings(mappings, type);
        }

        /// <summary>
        ///     Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class created from mappings of one XML type to another.
        /// </summary>
        /// <returns>
        ///     An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class.
        /// </returns>
        /// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping"/> objects used to map one type to another.</param><param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence"/> class that contains host and assembly data presented to the common language runtime policy system.</param>
        [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of FromMappings which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
        public XmlSerializer[] FromMappings(XmlMapping[] mappings,
                                            Evidence evidence)
        {
            return XmlSerializer.FromMappings(mappings, evidence);
        }

        /// <summary>
        ///     Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects created from an array of types.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects.
        /// </returns>
        /// <param name="types">An array of <see cref="T:System.Type"/> objects. </param>
        public XmlSerializer[] FromTypes(Type[] types)
        {
            return XmlSerializer.FromTypes(types);
        }

        /// <summary>
        ///     Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Reflection.Assembly"/> object that contains serializers for the supplied types and mappings.
        /// </returns>
        /// <param name="types">A collection of types.</param><param name="mappings">A collection of <see cref="T:System.Xml.Serialization.XmlMapping"/> objects used to convert one type to another.</param>
        public Assembly GenerateSerializer(Type[] types,
                                           XmlMapping[] mappings)
        {
            return XmlSerializer.GenerateSerializer(types, mappings);
        }

        /// <summary>
        ///     Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings and compiler settings and options.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Reflection.Assembly"/> that contains special versions of the <see cref="T:System.Xml.Serialization.XmlSerializer"/>.
        /// </returns>
        /// <param name="types">An array of type <see cref="T:System.Type"/> that contains objects used to serialize and deserialize data.</param><param name="mappings">An array of type <see cref="T:System.Xml.Serialization.XmlMapping"/> that maps the XML data to the type data.</param><param name="parameters">An instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters"/> class that represents the parameters used to invoke a compiler.</param>
        public Assembly GenerateSerializer(Type[] types,
                                           XmlMapping[] mappings,
                                           CompilerParameters parameters)
        {
            return XmlSerializer.GenerateSerializer(types, mappings, parameters);
        }

        /// <summary>
        ///     Returns the name of the assembly that contains one or more versions of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> especially created to serialize or deserialize the specified type.
        /// </summary>
        /// <returns>
        ///     The name of the assembly that contains an <see cref="T:System.Xml.Serialization.XmlSerializer"/> for the type.
        /// </returns>
        /// <param name="type">The <see cref="T:System.Type"/> you are deserializing.</param>
        public string GetXmlSerializerAssemblyName(Type type)
        {
            return XmlSerializer.GetXmlSerializerAssemblyName(type);
        }

        /// <summary>
        ///     Returns the name of the assembly that contains the serializer for the specified type in the specified namespace.
        /// </summary>
        /// <returns>
        ///     The name of the assembly that contains specially built serializers.
        /// </returns>
        /// <param name="type">The <see cref="T:System.Type"/> you are interested in.</param><param name="defaultNamespace">The namespace of the type.</param>
        public string GetXmlSerializerAssemblyName(Type type,
                                                   string defaultNamespace)
        {
            return XmlSerializer.GetXmlSerializerAssemblyName(type, defaultNamespace);
        }

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <param name="textWriter">The <see cref="T:System.IO.TextWriter"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param>
        public void Serialize(TextWriter textWriter,
                              object o)
        {
            this.XmlSerializerInstance.Serialize(textWriter, o, null);
        }

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter"/> and references the specified namespaces.
        /// </summary>
        /// <param name="textWriter">The <see cref="T:System.IO.TextWriter"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param><param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces for the generated XML document. </param><exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public void Serialize(TextWriter textWriter,
                              object o,
                              XmlSerializerNamespaces namespaces)
        {
            this.XmlSerializerInstance.Serialize(textWriter, o, namespaces);
        }

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param><exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public void Serialize(Stream stream,
                              object o)
        {
            this.XmlSerializerInstance.Serialize(stream, o, null);
        }

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream"/>that references the specified namespaces.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param><param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> referenced by the object. </param><exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public void Serialize(Stream stream,
                              object o,
                              XmlSerializerNamespaces namespaces)
        {
            this.XmlSerializerInstance.Serialize(stream, o, namespaces);
        }

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="IXmlWriter"/>.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="IXmlWriter"/> used to write the XML document. </param>
        /// <param name="o">The <see cref="T:System.Object"/> to serialize. </param>
        /// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public void Serialize(IXmlWriter xmlWriter,
                              object o)
        {
            this.XmlSerializerInstance.Serialize(xmlWriter.Writer, o, null);
        }

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="IXmlWriter"/> and references the specified namespaces.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="IXmlWriter"/> used to write the XML document. </param>
        /// <param name="o">The <see cref="T:System.Object"/> to serialize. </param>
        /// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> referenced by the object. </param>
        /// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public void Serialize(IXmlWriter xmlWriter,
                              object o,
                              XmlSerializerNamespaces namespaces)
        {
            this.XmlSerializerInstance.Serialize(xmlWriter.Writer, o, namespaces, null);
        }

        /// <summary>
        ///     Serializes the specified object and writes the XML document to a file using the specified <see cref="IXmlWriter"/> and references the specified namespaces and encoding style.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="IXmlWriter"/> used to write the XML document. </param>
        /// <param name="o">The object to serialize. </param>
        /// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> referenced by the object. </param>
        /// <param name="encodingStyle">The encoding style of the serialized XML. </param>
        /// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        public void Serialize(IXmlWriter xmlWriter,
                              object o,
                              XmlSerializerNamespaces namespaces,
                              string encodingStyle)
        {
            this.XmlSerializerInstance.Serialize(xmlWriter.Writer, o, namespaces, encodingStyle, null);
        }

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="IXmlWriter"/>, XML namespaces, and encoding.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="IXmlWriter"/> used to write the XML document.</param>
        /// <param name="o">The object to serialize.</param>
        /// <param name="namespaces">An instance of the XmlSerializaerNamespaces that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding used in the document.</param>
        /// <param name="id">For SOAP encoded messages, the base used to generate id attributes. </param>
        public void Serialize(IXmlWriter xmlWriter,
                              object o,
                              XmlSerializerNamespaces namespaces,
                              string encodingStyle,
                              string id)
        {
            this.XmlSerializerInstance.Serialize(xmlWriter.Writer, o, namespaces, encodingStyle, id);
        }

        #endregion
    }
}