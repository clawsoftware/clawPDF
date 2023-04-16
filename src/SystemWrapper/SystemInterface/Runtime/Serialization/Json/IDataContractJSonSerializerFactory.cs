namespace SystemInterface.Runtime.Serialization.Json
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml;

    /// <summary>
    ///     Factory to create a new <see cref="IDataContractJsonSerializer"/> instance.
    /// </summary>
    public interface IDataContractJSonSerializerFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that is serialized or deserialized.
        /// </param>
        IDataContractJsonSerializer Create(Type type);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        ///     The name of the XML element that encloses the content to serialize or deserialize.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           string rootName);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter of type 
        ///     <see cref="T:System.Xml.XmlDictionaryString"/>.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        ///     An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           XmlDictionaryString rootName);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type, with a collection of known types that may be present in the object graph.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that are serialized or deserialized.
        /// </param>
        /// <param name="knownTypes">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1"/>  of <see cref="T:System.Type"/> that contains the types that may be present 
        ///     in the object graph.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter, with a collection of known types that may 
        ///     be present in the object graph.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        ///     The name of the XML element that encloses the content to serialize or deserialize. The default is "root".
        /// </param>
        /// <param name="knownTypes">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1"/>  of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           string rootName,
                                           IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of a specified type using the XML root element specified by a parameter of type 
        ///     <see cref="T:System.Xml.XmlDictionaryString"/>, with a collection of known types that may be present in the object graph.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        ///     An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        /// <param name="knownTypes">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           XmlDictionaryString rootName,
                                           IEnumerable<Type> knownTypes);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, 
        ///     the maximum number of graph items to serialize or deserialize, whether to ignore unexpected data or emit type information, and a surrogate 
        ///     for custom serialization.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="knownTypes">
        ///     An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        /// <param name="maxItemsInObjectGraph">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        /// <param name="ignoreExtensionDataObject">
        ///     true to ignore the <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface upon serialization and ignore unexpected 
        ///     data upon deserialization; otherwise, false. The default is false.
        /// </param>
        /// <param name="dataContractSurrogate">
        ///     An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> to customize the serialization process.
        /// </param>
        /// <param name="alwaysEmitTypeInformation">
        ///     true to emit type information; otherwise, false. The default is false.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           IEnumerable<Type> knownTypes,
                                           int maxItemsInObjectGraph,
                                           bool ignoreExtensionDataObject,
                                           IDataContractSurrogate dataContractSurrogate,
                                           bool alwaysEmitTypeInformation);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type. This method also specifies the root name of the XML element, a list of known types that may 
        ///     be present in the object graph, the maximum number of graph items to serialize or deserialize, whether to ignore unexpected data or emit 
        ///     type information, and a surrogate for custom serialization.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that is serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        ///     The name of the XML element that encloses the content to serialize or deserialize. The default is "root".
        /// </param>
        /// <param name="knownTypes">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the types that may be present in 
        ///     the object graph.
        /// </param>
        /// <param name="maxItemsInObjectGraph">
        ///     The maximum number of items in the graph to serialize or deserialize. The default is the value returned by the 
        ///     <see cref="F:System.Int32.MaxValue"/> property.
        /// </param>
        /// <param name="ignoreExtensionDataObject">
        ///     true to ignore the <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface upon serialization and ignore unexpected 
        ///     data upon deserialization; otherwise, false. The default is false.
        /// </param>
        /// <param name="dataContractSurrogate">
        ///     An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> to customize the serialization process.
        /// </param>
        /// <param name="alwaysEmitTypeInformation">
        ///     true to emit type information; otherwise, false. The default is false.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           string rootName,
                                           IEnumerable<Type> knownTypes,
                                           int maxItemsInObjectGraph,
                                           bool ignoreExtensionDataObject,
                                           IDataContractSurrogate dataContractSurrogate,
                                           bool alwaysEmitTypeInformation);

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class to serialize or 
        ///     deserialize an object of the specified type. This method also specifies the root name of the XML element, a list of known types that may 
        ///     be present in the object graph, the maximum number of graph items to serialize or deserialize, whether to ignore unexpected data or emit 
        ///     type information, and a surrogate for custom serialization.
        /// </summary>
        /// <param name="type">
        ///     The type of the instances that are serialized or deserialized.
        /// </param>
        /// <param name="rootName">
        ///     An <see cref="T:System.Xml.XmlDictionaryString"/> that contains the root element name of the content.
        /// </param>
        /// <param name="knownTypes">
        ///     An <see cref="T:System.Collections.Generic.IEnumerable`1"/> of <see cref="T:System.Type"/> that contains the known types that may be present 
        ///     in the object graph.
        /// </param>
        /// <param name="maxItemsInObjectGraph">
        ///     The maximum number of items in the graph to serialize or deserialize. The default is the value returned by the 
        ///     <see cref="F:System.Int32.MaxValue"/> property.
        /// </param>
        /// <param name="ignoreExtensionDataObject">
        ///     true to ignore the <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface upon serialization and ignore unexpected 
        ///     data upon deserialization; otherwise, false. The default is false.
        /// </param>
        /// <param name="dataContractSurrogate">
        ///     An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> to customize the serialization process.
        /// </param>
        /// <param name="alwaysEmitTypeInformation">
        ///     true to emit type information; otherwise, false. The default is false.
        /// </param>
        IDataContractJsonSerializer Create(Type type,
                                           XmlDictionaryString rootName,
                                           IEnumerable<Type> knownTypes,
                                           int maxItemsInObjectGraph,
                                           bool ignoreExtensionDataObject,
                                           IDataContractSurrogate dataContractSurrogate,
                                           bool alwaysEmitTypeInformation);

        #endregion
    }
}