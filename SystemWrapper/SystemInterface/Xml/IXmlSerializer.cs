namespace SystemInterface.Xml
{
    using System;
    using System.CodeDom.Compiler;
    using System.IO;
    using System.Reflection;
    using System.Security.Policy;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    ///   Defines the contract for the wrapper of the <see cref="XmlSerializer"/> class.
    /// </summary>
    public interface IXmlSerializer
    {
        #region Public Events

        /// <summary>
        ///     Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters an XML attribute of unknown type during deserialization.
        /// </summary>
        event XmlAttributeEventHandler UnknownAttribute;

        /// <summary>
        ///     Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters an XML element of unknown type during deserialization.
        /// </summary>
        event XmlElementEventHandler UnknownElement;

        /// <summary>
        ///     Occurs when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters an XML node of unknown type during deserialization.
        /// </summary>
        event XmlNodeEventHandler UnknownNode;

        /// <summary>
        ///     Occurs during deserialization of a SOAP-encoded XML stream, when the <see cref="T:System.Xml.Serialization.XmlSerializer"/> encounters a recognized type that is not used or is unreferenced.
        /// </summary>
        event UnreferencedObjectEventHandler UnreferencedObject;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Gets a value that indicates whether this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can deserialize a specified XML document.
        /// </summary>
        /// <returns>
        ///     true if this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can deserialize the object that the <see cref="T:System.Xml.XmlReader"/> points to; otherwise, false.
        /// </returns>
        /// <param name="xmlReader">An <see cref="T:System.Xml.XmlReader"/> that points to the document to deserialize. </param>
        bool CanDeserialize(XmlReader xmlReader);

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> that contains the XML document to deserialize. </param>
        object Deserialize(Stream stream);

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.IO.TextReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="textReader">The <see cref="T:System.IO.TextReader"/> that contains the XML document to deserialize. </param><exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        object Deserialize(TextReader textReader);

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader"/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="xmlReader">The <see cref="T:System.xml.XmlReader"/> that contains the XML document to deserialize. </param><exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        object Deserialize(XmlReader xmlReader);

        /// <summary>
        ///     Deserializes an XML document contained by the specified <see cref="T:System.Xml.XmlReader"/> and allows the overriding of events that occur during deserialization.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Object"/> being deserialized.
        /// </returns>
        /// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader"/> that contains the document to deserialize.</param><param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents"/> class. </param>
        object Deserialize(XmlReader xmlReader,
                           XmlDeserializationEvents events);

        /// <summary>
        ///     Deserializes the XML document contained by the specified <see cref="T:System.xml.XmlReader"/> and encoding style.
        /// </summary>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        /// <param name="xmlReader">The <see cref="T:System.xml.XmlReader"/> that contains the XML document to deserialize. </param><param name="encodingStyle">The encoding style of the serialized XML. </param><exception cref="T:System.InvalidOperationException">An error occurred during deserialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        object Deserialize(XmlReader xmlReader,
                           string encodingStyle);

        /// <summary>
        ///     Deserializes the object using the data contained by the specified <see cref="T:System.Xml.XmlReader"/>.
        /// </summary>
        /// <returns>
        ///     The object being deserialized.
        /// </returns>
        /// <param name="xmlReader">An instance of the <see cref="T:System.Xml.XmlReader"/> class used to read the document.</param><param name="encodingStyle">The encoding used.</param><param name="events">An instance of the <see cref="T:System.Xml.Serialization.XmlDeserializationEvents"/> class. </param>
        object Deserialize(XmlReader xmlReader,
                           string encodingStyle,
                           XmlDeserializationEvents events);

        /// <summary>
        ///     Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects created from an array of <see cref="T:System.Xml.Serialization.XmlTypeMapping"/> objects.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects.
        /// </returns>
        /// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlTypeMapping"/> that maps one type to another. </param>
        XmlSerializer[] FromMappings(XmlMapping[] mappings);

        /// <summary>
        ///     Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class from the specified mappings.
        /// </summary>
        /// <returns>
        ///     An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class.
        /// </returns>
        /// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping"/> objects.</param><param name="type">The <see cref="T:System.Type"/> of the deserialized object.</param>
        XmlSerializer[] FromMappings(XmlMapping[] mappings,
                                     Type type);

        /// <summary>
        ///     Returns an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class created from mappings of one XML type to another.
        /// </summary>
        /// <returns>
        ///     An instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class.
        /// </returns>
        /// <param name="mappings">An array of <see cref="T:System.Xml.Serialization.XmlMapping"/> objects used to map one type to another.</param><param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence"/> class that contains host and assembly data presented to the common language runtime policy system.</param>
        [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use an overload of FromMappings which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
        XmlSerializer[] FromMappings(XmlMapping[] mappings,
                                     Evidence evidence);

        /// <summary>
        ///     Returns an array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects created from an array of types.
        /// </summary>
        /// <returns>
        ///     An array of <see cref="T:System.Xml.Serialization.XmlSerializer"/> objects.
        /// </returns>
        /// <param name="types">An array of <see cref="T:System.Type"/> objects. </param>
        XmlSerializer[] FromTypes(Type[] types);

        /// <summary>
        ///     Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Reflection.Assembly"/> object that contains serializers for the supplied types and mappings.
        /// </returns>
        /// <param name="types">A collection of types.</param><param name="mappings">A collection of <see cref="T:System.Xml.Serialization.XmlMapping"/> objects used to convert one type to another.</param>
        Assembly GenerateSerializer(Type[] types,
                                    XmlMapping[] mappings);

        /// <summary>
        ///     Returns an assembly that contains custom-made serializers used to serialize or deserialize the specified type or types, using the specified mappings and compiler settings and options.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Reflection.Assembly"/> that contains special versions of the <see cref="T:System.Xml.Serialization.XmlSerializer"/>.
        /// </returns>
        /// <param name="types">An array of type <see cref="T:System.Type"/> that contains objects used to serialize and deserialize data.</param><param name="mappings">An array of type <see cref="T:System.Xml.Serialization.XmlMapping"/> that maps the XML data to the type data.</param><param name="parameters">An instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters"/> class that represents the parameters used to invoke a compiler.</param>
        Assembly GenerateSerializer(Type[] types,
                                    XmlMapping[] mappings,
                                    CompilerParameters parameters);

        /// <summary>
        ///     Returns the name of the assembly that contains one or more versions of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> especially created to serialize or deserialize the specified type.
        /// </summary>
        /// <returns>
        ///     The name of the assembly that contains an <see cref="T:System.Xml.Serialization.XmlSerializer"/> for the type.
        /// </returns>
        /// <param name="type">The <see cref="T:System.Type"/> you are deserializing.</param>
        string GetXmlSerializerAssemblyName(Type type);

        /// <summary>
        ///     Returns the name of the assembly that contains the serializer for the specified type in the specified namespace.
        /// </summary>
        /// <returns>
        ///     The name of the assembly that contains specially built serializers.
        /// </returns>
        /// <param name="type">The <see cref="T:System.Type"/> you are interested in.</param><param name="defaultNamespace">The namespace of the type.</param>
        string GetXmlSerializerAssemblyName(Type type,
                                            string defaultNamespace);

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter"/>.
        /// </summary>
        /// <param name="textWriter">The <see cref="T:System.IO.TextWriter"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param>
        void Serialize(TextWriter textWriter,
                       object o);

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.TextWriter"/> and references the specified namespaces.
        /// </summary>
        /// <param name="textWriter">The <see cref="T:System.IO.TextWriter"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param><param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> that contains namespaces for the generated XML document. </param><exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        void Serialize(TextWriter textWriter,
                       object o,
                       XmlSerializerNamespaces namespaces);

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param><exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        void Serialize(Stream stream,
                       object o);

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.IO.Stream"/>that references the specified namespaces.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> used to write the XML document. </param><param name="o">The <see cref="T:System.Object"/> to serialize. </param><param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> referenced by the object. </param><exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        void Serialize(Stream stream,
                       object o,
                       XmlSerializerNamespaces namespaces);

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter"/> used to write the XML document. </param>
        /// <param name="o">The <see cref="T:System.Object"/> to serialize. </param>
        /// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        void Serialize(IXmlWriter xmlWriter,
                       object o);

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter"/> and references the specified namespaces.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter"/> used to write the XML document. </param>
        /// <param name="o">The <see cref="T:System.Object"/> to serialize. </param>
        /// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> referenced by the object. </param>
        /// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        void Serialize(IXmlWriter xmlWriter,
                       object o,
                       XmlSerializerNamespaces namespaces);

        /// <summary>
        ///     Serializes the specified object and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter"/> and references the specified namespaces and encoding style.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="T:System.xml.XmlWriter"/> used to write the XML document. </param>
        /// <param name="o">The object to serialize. </param>
        /// <param name="namespaces">The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces"/> referenced by the object. </param>
        /// <param name="encodingStyle">The encoding style of the serialized XML. </param>
        /// <exception cref="T:System.InvalidOperationException">An error occurred during serialization. The original exception is available using the <see cref="P:System.Exception.InnerException"/> property. </exception>
        void Serialize(IXmlWriter xmlWriter,
                       object o,
                       XmlSerializerNamespaces namespaces,
                       string encodingStyle);

        /// <summary>
        ///     Serializes the specified <see cref="T:System.Object"/> and writes the XML document to a file using the specified <see cref="T:System.Xml.XmlWriter"/>, XML namespaces, and encoding.
        /// </summary>
        /// <param name="xmlWriter">The <see cref="T:System.Xml.XmlWriter"/> used to write the XML document.</param>
        /// <param name="o">The object to serialize.</param>
        /// <param name="namespaces">An instance of the XmlSerializaerNamespaces that contains namespaces and prefixes to use.</param>
        /// <param name="encodingStyle">The encoding used in the document.</param>
        /// <param name="id">For SOAP encoded messages, the base used to generate id attributes. </param>
        void Serialize(IXmlWriter xmlWriter,
                       object o,
                       XmlSerializerNamespaces namespaces,
                       string encodingStyle,
                       string id);

        #endregion
    }
}