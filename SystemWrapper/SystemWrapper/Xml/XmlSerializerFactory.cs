namespace SystemWrapper.Xml
{
    using System;
    using System.Security.Policy;
    using System.Xml.Serialization;

    using SystemInterface.Xml;

    /// <summary>
    ///     Factory responsible for the creation of the <see cref="XmlSerializer"/> wrapped by an instance of <see cref="IXmlSerializer"/>.
    /// </summary>
    public class XmlSerializerFactory : IXmlSerializerFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of type <see cref="T:System.Object"/> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object"/>. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize.</param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/> that extends or overrides the behavior of the class specified in the <paramref name="type"/> parameter.</param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize.</param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that defines the XML root element properties.</param><param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param><param name="location">The location of the types.</param>
        public IXmlSerializer Create(Type type,
                                     XmlAttributeOverrides overrides,
                                     Type[] extraTypes,
                                     XmlRootAttribute root,
                                     string defaultNamespace,
                                     string location)
        {
            return new XmlSerializerWrap(type, overrides, extraTypes, root, defaultNamespace, location);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML document instances, and deserialize XML document instances into objects of the specified type. This overload allows you to supply other types that can be encountered during a serialization or deserialization operation, as well as a default namespace for all XML elements, the class to use as the XML root element, its location, and credentials required for access.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize.</param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/> that extends or overrides the behavior of the class specified in the <paramref name="type"/> parameter.</param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize.</param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that defines the XML root element properties.</param><param name="defaultNamespace">The default namespace of all XML elements in the XML document.</param><param name="location">The location of the types.</param><param name="evidence">An instance of the <see cref="T:System.Security.Policy.Evidence"/> class that contains credentials required to access types.</param>
        [Obsolete("This method is obsolete and will be removed in a future release of the .NET Framework. Please use a XmlSerializer constructor overload which does not take an Evidence parameter. See http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
        public IXmlSerializer Create(Type type,
                                     XmlAttributeOverrides overrides,
                                     Type[] extraTypes,
                                     XmlRootAttribute root,
                                     string defaultNamespace,
                                     string location,
                                     Evidence evidence)
        {
            return new XmlSerializerWrap(type, overrides, extraTypes, root, defaultNamespace, location, evidence);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of type <see cref="T:System.Object"/> into XML document instances, and deserialize XML document instances into objects of type <see cref="T:System.Object"/>. Each object to be serialized can itself contain instances of classes, which this overload overrides with other classes. This overload also specifies the default namespace for all the XML elements and the class to use as the XML root element.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/> that extends or overrides the behavior of the class specified in the <paramref name="type"/> parameter. </param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize. </param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that defines the XML root element properties. </param><param name="defaultNamespace">The default namespace of all XML elements in the XML document. </param>
        public IXmlSerializer Create(Type type,
                                     XmlAttributeOverrides overrides,
                                     Type[] extraTypes,
                                     XmlRootAttribute root,
                                     string defaultNamespace)
        {
            return new XmlSerializerWrap(type, overrides, extraTypes, root, defaultNamespace);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize an XML document into object of the specified type. It also specifies the class to use as the XML root element.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="root">An <see cref="T:System.Xml.Serialization.XmlRootAttribute"/> that represents the XML root element. </param>
        public IXmlSerializer Create(Type type,
                                     XmlRootAttribute root)
        {
            return new XmlSerializerWrap(type, root);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into object of a specified type. If a property or field returns an array, the <paramref name="extraTypes"/> parameter specifies objects that can be inserted into the array.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="extraTypes">A <see cref="T:System.Type"/> array of additional object types to serialize. </param>
        public IXmlSerializer Create(Type type,
                                     Type[] extraTypes)
        {
            return new XmlSerializerWrap(type, extraTypes);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Each object to be serialized can itself contain instances of classes, which this overload can override with other classes.
        /// </summary>
        /// <param name="type">The type of the object to serialize. </param><param name="overrides">An <see cref="T:System.Xml.Serialization.XmlAttributeOverrides"/>. </param>
        public IXmlSerializer Create(Type type,
                                     XmlAttributeOverrides overrides)
        {
            return new XmlSerializerWrap(type, overrides);
        }

        /// <summary>
        ///     Initializes an instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class using an object that maps one type to another.
        /// </summary>
        /// <param name="xmlTypeMapping">An <see cref="T:System.Xml.Serialization.XmlTypeMapping"/> that maps one type to another. </param>
        public IXmlSerializer Create(XmlTypeMapping xmlTypeMapping)
        {
            return new XmlSerializerWrap(xmlTypeMapping);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param>
        public IXmlSerializer Create(Type type)
        {
            return new XmlSerializerWrap(type);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSerializer"/> class that can serialize objects of the specified type into XML documents, and deserialize XML documents into objects of the specified type. Specifies the default namespace for all the XML elements.
        /// </summary>
        /// <param name="type">The type of the object that this <see cref="T:System.Xml.Serialization.XmlSerializer"/> can serialize. </param><param name="defaultNamespace">The default namespace to use for all the XML elements. </param>
        public IXmlSerializer Create(Type type,
                                     string defaultNamespace)
        {
            return new XmlSerializerWrap(type, defaultNamespace);
        }

        #endregion
    }
}