namespace SystemWrapper.Runtime.Serialization.Json
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Xml;

    using SystemInterface.Attributes;
    using SystemInterface.Runtime.Serialization.Json;

    /// <summary>
    ///     Wrapper for <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class.
    /// </summary>
    [GenerateFactory(typeof(IDataContractJSonSerializerFactory))]
    public class DataContractJsonSerializerWrap : IDataContractJsonSerializer
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that is serialized or deserialized.
        /// </param>
        public DataContractJsonSerializerWrap(Type type)
        {
            this.Initialize(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        /// The name of the XML element that encloses the content to serialize or deserialize.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              string rootName)
        {
            this.Initialize(type, rootName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter of type 
        ///     <see cref="T:System.Xml.XmlDictionaryString"/>.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        /// An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              XmlDictionaryString rootName)
        {
            this.Initialize(type, rootName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type, with a collection of known types that may be present in the object graph.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that are serialized or deserialized.
        /// </param>
        /// <param name="knownTypes">
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/>  of <see cref="T:System.Type"/> that contains the types that may be present 
        ///     in the object graph.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              IEnumerable<Type> knownTypes)
        {
            this.Initialize(type, knownTypes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter, with a collection of known types that may 
        ///     be present in the object graph.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        /// The name of the XML element that encloses the content to serialize or deserialize. The default is "root".
        /// </param>
        /// <param name="knownTypes">
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/>  of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              string rootName,
                                              IEnumerable<Type> knownTypes)
        {
            this.Initialize(type, rootName, knownTypes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter of type 
        ///     <see cref="T:System.Xml.XmlDictionaryString"/>, with a collection of known types that may be present in the object graph.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        /// An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        /// <param name="knownTypes">
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              XmlDictionaryString rootName,
                                              IEnumerable<Type> knownTypes)
        {
            this.Initialize(type, rootName, knownTypes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, 
        ///     the maximum number of graph items to serialize or deserialize, whether to ignore unexpected data or emit type information, and a surrogate 
        ///     for custom serialization.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="knownTypes">
        /// An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        /// <param name="maxItemsInObjectGraph">
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        /// <param name="ignoreExtensionDataObject">
        /// true to ignore the <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface upon serialization and ignore unexpected 
        ///     data upon deserialization; otherwise, false. The default is false.
        /// </param>
        /// <param name="dataContractSurrogate">
        /// An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> to customize the serialization process.
        /// </param>
        /// <param name="alwaysEmitTypeInformation">
        /// true to emit type information; otherwise, false. The default is false.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              IEnumerable<Type> knownTypes,
                                              int maxItemsInObjectGraph,
                                              bool ignoreExtensionDataObject,
                                              IDataContractSurrogate dataContractSurrogate,
                                              bool alwaysEmitTypeInformation)
        {
            this.Initialize(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type. This method also specifies the root name of the XML element, a list of known types that may 
        ///     be present in the object graph, the maximum number of graph items to serialize or deserialize, whether to ignore unexpected data or emit 
        ///     type information, and a surrogate for custom serialization.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        /// The name of the XML element that encloses the content to serialize or deserialize. The default is "root".
        /// </param>
        /// <param name="knownTypes">
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        /// <param name="maxItemsInObjectGraph">
        /// The maximum number of items in the graph to serialize or deserialize. The default is the value returned by the 
        ///     <see cref="F:System.Int32.MaxValue"/> property.
        /// </param>
        /// <param name="ignoreExtensionDataObject">
        /// true to ignore the <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface upon serialization and ignore unexpected 
        ///     data upon deserialization; otherwise, false. The default is false.
        /// </param>
        /// <param name="dataContractSurrogate">
        /// An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> to customize the serialization process.
        /// </param>
        /// <param name="alwaysEmitTypeInformation">
        /// true to emit type information; otherwise, false. The default is false.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              string rootName,
                                              IEnumerable<Type> knownTypes,
                                              int maxItemsInObjectGraph,
                                              bool ignoreExtensionDataObject,
                                              IDataContractSurrogate dataContractSurrogate,
                                              bool alwaysEmitTypeInformation)
        {
            this.Initialize(type, rootName, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractJsonSerializerWrap"/> class. 
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type. This method also specifies the root name of the XML element, a list of known types that may 
        ///     be present in the object graph, the maximum number of graph items to serialize or deserialize, whether to ignore unexpected data or emit 
        ///     type information, and a surrogate for custom serialization.
        /// </summary>
        /// <param name="type">
        /// The type of the instances that are serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        /// An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        /// <param name="knownTypes">
        /// An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the known types that may be present 
        ///     in the object graph.
        /// </param>
        /// <param name="maxItemsInObjectGraph">
        /// The maximum number of items in the graph to serialize or deserialize. The default is the value returned by the 
        ///     <see cref="F:System.Int32.MaxValue"/> property.
        /// </param>
        /// <param name="ignoreExtensionDataObject">
        /// true to ignore the <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface upon serialization and ignore unexpected 
        ///     data upon deserialization; otherwise, false. The default is false.
        /// </param>
        /// <param name="dataContractSurrogate">
        /// An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> to customize the serialization process.
        /// </param>
        /// <param name="alwaysEmitTypeInformation">
        /// true to emit type information; otherwise, false. The default is false.
        /// </param>
        public DataContractJsonSerializerWrap(Type type,
                                              XmlDictionaryString rootName,
                                              IEnumerable<Type> knownTypes,
                                              int maxItemsInObjectGraph,
                                              bool ignoreExtensionDataObject,
                                              IDataContractSurrogate dataContractSurrogate,
                                              bool alwaysEmitTypeInformation)
        {
            this.Initialize(type, rootName, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the data contract json serializer instance.
        /// </summary>
        public DataContractJsonSerializer DataContractJsonSerializerInstance { get; private set; }

        /// <summary>
        ///     Gets a surrogate type that is currently active for a given <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> instance. 
        ///     Surrogates can extend the serialization or deserialization process.
        /// </summary>
        public IDataContractSurrogate DataContractSurrogate
        {
            get
            {
                return this.DataContractJsonSerializerInstance.DataContractSurrogate;
            }
        }

        /// <summary>
        ///     Gets a value that specifies whether unknown data is ignored on deserialization and whether the 
        ///     <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface is ignored on serialization.
        /// </summary>
        /// <returns>
        ///     true to ignore unknown data and <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/>; otherwise, false.
        /// </returns>
        public bool IgnoreExtensionDataObject
        {
            get
            {
                return this.DataContractJsonSerializerInstance.IgnoreExtensionDataObject;
            }
        }

        /// <summary>
        ///     Gets a collection of types that may be present in the object graph serialized using this instance of the 
        ///     <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"/> that contains the expected types passed in as known types to 
        ///     the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> constructor.
        /// </returns>
        public ReadOnlyCollection<Type> KnownTypes
        {
            get
            {
                return this.DataContractJsonSerializerInstance.KnownTypes;
            }
        }

        /// <summary>
        ///     Gets the maximum number of items in an object graph that the serializer serializes or deserializes in one read or write call.
        /// </summary>
        /// <returns>
        ///     The maximum number of items to serialize or deserialize.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The number of items exceeds the maximum value.
        /// </exception>
        public int MaxItemsInObjectGraph
        {
            get
            {
                return this.DataContractJsonSerializerInstance.MaxItemsInObjectGraph;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Determines whether the <see cref="T:System.Xml.XmlReader"/> is positioned on an object that can be deserialized.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="T:System.Xml.XmlReader"/> used to read the XML stream.
        /// </param>
        /// <returns>
        ///     true if the reader is positioned correctly; otherwise, false.
        /// </returns>
        public bool IsStartObject(XmlReader reader)
        {
            return this.DataContractJsonSerializerInstance.IsStartObject(reader);
        }

        /// <summary>
        ///     Gets a value that specifies whether the <see cref="T:System.Xml.XmlDictionaryReader"/> is positioned over an XML element that represents 
        ///     an object the serializer can deserialize from.
        /// </summary>
        /// <param name="reader">
        ///     The <see cref="T:System.Xml.XmlDictionaryReader"/> used to read the XML stream mapped from JSON.
        /// </param>
        /// <returns>
        ///     true if the reader is positioned correctly; otherwise, false.
        /// </returns>
        public bool IsStartObject(XmlDictionaryReader reader)
        {
            return this.DataContractJsonSerializerInstance.IsStartObject(reader);
        }

        /// <summary>
        ///     Reads a document stream in the JSON (JavaScript Object Notation) format and returns the deserialized object.
        /// </summary>
        /// <param name="stream">
        ///     The <see cref="T:System.IO.Stream"/> to be read.
        /// </param>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        public object ReadObject(Stream stream)
        {
            return this.DataContractJsonSerializerInstance.ReadObject(stream);
        }

        /// <summary>
        ///     Reads the XML document mapped from JSON (JavaScript Object Notation) with an <see cref="T:System.Xml.XmlReader"/> and returns the 
        ///     deserialized object.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="T:System.Xml.XmlReader"/> used to read the XML document mapped from JSON.
        /// </param>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        public object ReadObject(XmlReader reader)
        {
            return this.DataContractJsonSerializerInstance.ReadObject(reader);
        }

        /// <summary>
        ///     Reads an XML document mapped from JSON with an <see cref="T:System.Xml.XmlReader"/> and returns the deserialized object; it also enables 
        ///     you to specify whether the serializer should verify that it is positioned on an appropriate element before attempting to deserialize.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="T:System.Xml.XmlReader"/> used to read the XML document mapped from JSON.
        /// </param>
        /// <param name="verifyObjectName">
        ///     true to check whether the enclosing XML element name and namespace correspond to the expected name and namespace; otherwise, false, 
        ///     which skips the verification. The default is true.
        /// </param>
        /// <returns>
        /// The deserialized object.
        /// </returns>
        public object ReadObject(XmlReader reader,
                                 bool verifyObjectName)
        {
            return this.DataContractJsonSerializerInstance.ReadObject(reader, verifyObjectName);
        }

        /// <summary>
        ///     Reads the XML document mapped from JSON with an <see cref="T:System.Xml.XmlDictionaryReader"/> and returns the deserialized object; it 
        ///     also enables you to specify whether the serializer should verify that it is positioned on an appropriate element before attempting to 
        ///     deserialize.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="T:System.Xml.XmlDictionaryReader"/> used to read the XML document mapped from JSON.
        /// </param>
        /// <param name="verifyObjectName">
        ///     true to check whether the enclosing XML element name and namespace correspond to the expected name and namespace; otherwise, false to skip the 
        ///     verification. The default is true.
        /// </param>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        public object ReadObject(XmlDictionaryReader reader,
                                 bool verifyObjectName)
        {
            return this.DataContractJsonSerializerInstance.ReadObject(reader, verifyObjectName);
        }

        /// <summary>
        ///     Reads the XML document mapped from JSON (JavaScript Object Notation) with an <see cref="T:System.Xml.XmlDictionaryReader"/> and returns 
        ///     the deserialized object.
        /// </summary>
        /// <param name="reader">
        ///     An <see cref="T:System.Xml.XmlDictionaryReader"/> used to read the XML document mapped from JSON.
        /// </param>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        public object ReadObject(XmlDictionaryReader reader)
        {
            return this.DataContractJsonSerializerInstance.ReadObject(reader);
        }

        /// <summary>
        ///     Writes the closing XML element to an XML document, using an <see cref="T:System.Xml.XmlWriter"/>, which can be mapped to JavaScript Object 
        ///     Notation (JSON).
        /// </summary>
        /// <param name="writer">
        ///     An <see cref="T:System.Xml.XmlWriter"/> used to write the XML document mapped to JSON.
        /// </param>
        public void WriteEndObject(XmlWriter writer)
        {
            this.DataContractJsonSerializerInstance.WriteEndObject(writer);
        }

        /// <summary>
        ///     Writes the closing XML element to an XML document, using an <see cref="T:System.Xml.XmlDictionaryWriter"/>, which can be mapped to 
        ///     JavaScript Object Notation (JSON).
        /// </summary>
        /// <param name="writer">
        ///     An <see cref="T:System.Xml.XmlDictionaryWriter"/> used to write the XML document to map to JSON.
        /// </param>
        public void WriteEndObject(XmlDictionaryWriter writer)
        {
            this.DataContractJsonSerializerInstance.WriteEndObject(writer);
        }

        /// <summary>
        ///     Serializes a specified object to JavaScript Object Notation (JSON) data and writes the resulting JSON to a stream.
        /// </summary>
        /// <param name="stream">
        ///     The <see cref="T:System.IO.Stream"/> that is written to.
        /// </param>
        /// <param name="graph">
        ///     The object that contains the data to write to the stream.
        /// </param>
        /// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules. For example, the 
        ///     <see cref="T:System.Runtime.Serialization.DataContractAttribute"/> attribute has not been applied to the type.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     There is a problem with the instance being written.
        /// </exception>
        /// <exception cref="T:System.ServiceModel.QuotaExceededException">
        ///     The maximum number of objects to serialize has been exceeded. Check the 
        ///     <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph"/> property.
        /// </exception>
        public void WriteObject(Stream stream,
                                object graph)
        {
            this.DataContractJsonSerializerInstance.WriteObject(stream, graph);
        }

        /// <summary>
        ///     Serializes an object to XML that may be mapped to JavaScript Object Notation (JSON). Writes all the object data, including the starting XML 
        ///     element, content, and closing element, with an <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> used to write the XML document to map to JSON.
        /// </param>
        /// <param name="graph">
        ///     The object that contains the data to write.
        /// </param>
        /// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules. For example, the 
        ///     <see cref="T:System.Runtime.Serialization.DataContractAttribute"/> attribute has not been applied to the type.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     There is a problem with the instance being written.
        /// </exception>
        /// <exception cref="T:System.ServiceModel.QuotaExceededException">
        ///     The maximum number of objects to serialize has been exceeded. Check the 
        ///     <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph"/> property.
        /// </exception>
        public void WriteObject(XmlWriter writer,
                                object graph)
        {
            this.DataContractJsonSerializerInstance.WriteObject(writer, graph);
        }

        /// <summary>
        ///     Serializes an object to XML that may be mapped to JavaScript Object Notation (JSON). Writes all the object data, including the starting XML 
        ///     element, content, and closing element, with an <see cref="T:System.Xml.XmlDictionaryWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlDictionaryWriter"/> used to write the XML document or stream to map to JSON.
        /// </param>
        /// <param name="graph">
        ///     The object that contains the data to write.
        /// </param>
        /// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules. For example, the 
        ///     <see cref="T:System.Runtime.Serialization.DataContractAttribute"/> attribute has not been applied to the type.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     There is a problem with the instance being written.
        /// </exception>
        /// <exception cref="T:System.ServiceModel.QuotaExceededException">
        ///     The maximum number of objects to serialize has been exceeded. Check the 
        ///     <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph"/> property.
        /// </exception>
        public void WriteObject(XmlDictionaryWriter writer,
                                object graph)
        {
            this.DataContractJsonSerializerInstance.WriteObject(writer, graph);
        }

        /// <summary>
        ///     Writes the XML content that can be mapped to JavaScript Object Notation (JSON) using an <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> used to write to.
        /// </param>
        /// <param name="graph">
        ///     The object to write.
        /// </param>
        /// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">
        ///     The type being serialized does not conform to data contract rules. For example, the 
        ///     <see cref="T:System.Runtime.Serialization.DataContractAttribute"/> attribute has not been applied to the type.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     There is a problem with the instance being written.
        /// </exception>
        /// <exception cref="T:System.ServiceModel.QuotaExceededException">
        ///     The maximum number of objects to serialize has been exceeded. Check the 
        ///     <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph"/> property.
        /// </exception>
        public void WriteObjectContent(XmlWriter writer,
                                       object graph)
        {
            this.DataContractJsonSerializerInstance.WriteObjectContent(writer, graph);
        }

        /// <summary>
        ///     Writes the XML content that can be mapped to JavaScript Object Notation (JSON) using an <see cref="T:System.Xml.XmlDictionaryWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlDictionaryWriter"/> to write to.
        /// </param>
        /// <param name="graph">
        ///     The object to write.</param><exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does 
        ///     not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute"/> attribute has not 
        ///     been applied to the type.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        ///     There is a problem with the instance being written.
        /// </exception>
        /// <exception cref="T:System.ServiceModel.QuotaExceededException">
        ///     The maximum number of objects to serialize has been exceeded. Check the 
        ///     <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph"/> property.
        /// </exception>
        public void WriteObjectContent(XmlDictionaryWriter writer,
                                       object graph)
        {
            this.DataContractJsonSerializerInstance.WriteObjectContent(writer, graph);
        }

        /// <summary>
        ///     Writes the opening XML element for serializing an object to XML that can be mapped to JavaScript Object Notation (JSON) using an 
        ///     <see cref="T:System.Xml.XmlWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlWriter"/> used to write the XML start element.
        /// </param>
        /// <param name="graph">
        ///     The object to write.
        /// </param>
        public void WriteStartObject(XmlWriter writer,
                                     object graph)
        {
            this.DataContractJsonSerializerInstance.WriteStartObject(writer, graph);
        }

        /// <summary>
        ///     Writes the opening XML element for serializing an object to XML that can be mapped to JavaScript Object Notation (JSON) using an 
        ///     <see cref="T:System.Xml.XmlDictionaryWriter"/>.
        /// </summary>
        /// <param name="writer">
        ///     The <see cref="T:System.Xml.XmlDictionaryWriter"/> used to write the XML start element.
        /// </param>
        /// <param name="graph">
        ///     The object to write.
        /// </param>
        public void WriteStartObject(XmlDictionaryWriter writer,
                                     object graph)
        {
            this.DataContractJsonSerializerInstance.WriteStartObject(writer, graph);
        }

        #endregion

        #region Methods

        private void Initialize(Type type)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type);
        }

        private void Initialize(Type type,
                                string rootName)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, rootName);
        }

        private void Initialize(Type type,
                                XmlDictionaryString rootName)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, rootName);
        }

        private void Initialize(Type type,
                                IEnumerable<Type> knownTypes)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, knownTypes);
        }

        private void Initialize(Type type,
                                string rootName,
                                IEnumerable<Type> knownTypes)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, rootName, knownTypes);
        }

        private void Initialize(Type type,
                                XmlDictionaryString rootName,
                                IEnumerable<Type> knownTypes)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, rootName, knownTypes);
        }

        private void Initialize(Type type,
                                IEnumerable<Type> knownTypes,
                                int maxItemsInObjectGraph,
                                bool ignoreExtensionDataObject,
                                IDataContractSurrogate dataContractSurrogate,
                                bool alwaysEmitTypeInformation)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation);
        }

        private void Initialize(Type type,
                                string rootName,
                                IEnumerable<Type> knownTypes,
                                int maxItemsInObjectGraph,
                                bool ignoreExtensionDataObject,
                                IDataContractSurrogate dataContractSurrogate,
                                bool alwaysEmitTypeInformation)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, rootName, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation);
        }

        private void Initialize(Type type,
                                XmlDictionaryString rootName,
                                IEnumerable<Type> knownTypes,
                                int maxItemsInObjectGraph,
                                bool ignoreExtensionDataObject,
                                IDataContractSurrogate dataContractSurrogate,
                                bool alwaysEmitTypeInformation)
        {
            this.DataContractJsonSerializerInstance = new DataContractJsonSerializer(type, rootName, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation);
        }

        #endregion
    }
}