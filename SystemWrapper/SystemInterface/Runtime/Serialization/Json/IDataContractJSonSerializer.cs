namespace SystemInterface.Runtime.Serialization.Json
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    /// <summary>
    ///     Wrapper for <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> class.
    /// </summary>
    public interface IDataContractJsonSerializer
    {
        #region Public Properties

        /// <summary>
        ///     Gets a surrogate type that is currently active for a given <see cref="T:System.Runtime.Serialization.IDataContractSurrogate"/> instance. 
        ///     Surrogates can extend the serialization or deserialization process.
        /// </summary>
        IDataContractSurrogate DataContractSurrogate { get; }

        /// <summary>
        ///     Gets a value that specifies whether unknown data is ignored on deserialization and whether the 
        ///     <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/> interface is ignored on serialization.
        /// </summary>
        /// <returns>
        ///     true to ignore unknown data and <see cref="T:System.Runtime.Serialization.IExtensibleDataObject"/>; otherwise, false.
        /// </returns>
        bool IgnoreExtensionDataObject { get; }

        /// <summary>
        ///     Gets a collection of types that may be present in the object graph serialized using this instance of the 
        ///     <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1"/> that contains the expected types passed in as known types to 
        ///     the <see cref="T:System.Runtime.Serialization.Json.DataContractJsonSerializer"/> constructor.
        /// </returns>
        ReadOnlyCollection<Type> KnownTypes { get; }

        /// <summary>
        ///     Gets the maximum number of items in an object graph that the serializer serializes or deserializes in one read or write call.
        /// </summary>
        /// <returns>
        ///     The maximum number of items to serialize or deserialize.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The number of items exceeds the maximum value.
        /// </exception>
        int MaxItemsInObjectGraph { get; }

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
        bool IsStartObject(XmlReader reader);

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
        bool IsStartObject(XmlDictionaryReader reader);

        /// <summary>
        ///     Reads a document stream in the JSON (JavaScript Object Notation) format and returns the deserialized object.
        /// </summary>
        /// <param name="stream">
        ///     The <see cref="T:System.IO.Stream"/> to be read.
        /// </param>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        object ReadObject(Stream stream);

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
        object ReadObject(XmlReader reader);

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
        object ReadObject(XmlReader reader,
                          bool verifyObjectName);

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
        object ReadObject(XmlDictionaryReader reader);

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
        object ReadObject(XmlDictionaryReader reader,
                          bool verifyObjectName);

        /// <summary>
        ///     Writes the closing XML element to an XML document, using an <see cref="T:System.Xml.XmlWriter"/>, which can be mapped to JavaScript Object 
        ///     Notation (JSON).
        /// </summary>
        /// <param name="writer">
        ///     An <see cref="T:System.Xml.XmlWriter"/> used to write the XML document mapped to JSON.
        /// </param>
        void WriteEndObject(XmlWriter writer);

        /// <summary>
        ///     Writes the closing XML element to an XML document, using an <see cref="T:System.Xml.XmlDictionaryWriter"/>, which can be mapped to 
        ///     JavaScript Object Notation (JSON).
        /// </summary>
        /// <param name="writer">
        ///     An <see cref="T:System.Xml.XmlDictionaryWriter"/> used to write the XML document to map to JSON.
        /// </param>
        void WriteEndObject(XmlDictionaryWriter writer);

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
        void WriteObject(Stream stream,
                         object graph);

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
        void WriteObject(XmlWriter writer,
                         object graph);

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
        void WriteObject(XmlDictionaryWriter writer,
                         object graph);

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
        void WriteObjectContent(XmlWriter writer,
                                object graph);

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
        void WriteObjectContent(XmlDictionaryWriter writer,
                                object graph);

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
        void WriteStartObject(XmlWriter writer,
                              object graph);

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
        void WriteStartObject(XmlDictionaryWriter writer,
                              object graph);

        #endregion
    }
}