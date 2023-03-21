#pragma warning disable 1591

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
    /// The implementation for the factory generating <see cref="SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap" /> instances.
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("DeveloperInTheFlow.FactoryGenerator", "1.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public partial class DataContractJsonSerializerWrapFactory : SystemInterface.Runtime.Serialization.Json.IDataContractJSonSerializerFactory
    {
        #region Public Factory Methods

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(System.Type type)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(type);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            string rootName)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                rootName);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            System.Xml.XmlDictionaryString rootName)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                rootName);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            System.Collections.Generic.IEnumerable<System.Type> knownTypes)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                knownTypes);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            string rootName,
            System.Collections.Generic.IEnumerable<System.Type> knownTypes)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                rootName, 
                knownTypes);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            System.Xml.XmlDictionaryString rootName,
            System.Collections.Generic.IEnumerable<System.Type> knownTypes)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                rootName, 
                knownTypes);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            System.Collections.Generic.IEnumerable<System.Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            System.Runtime.Serialization.IDataContractSurrogate dataContractSurrogate,
            bool alwaysEmitTypeInformation)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                knownTypes, 
                maxItemsInObjectGraph, 
                ignoreExtensionDataObject, 
                dataContractSurrogate, 
                alwaysEmitTypeInformation);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            string rootName,
            System.Collections.Generic.IEnumerable<System.Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            System.Runtime.Serialization.IDataContractSurrogate dataContractSurrogate,
            bool alwaysEmitTypeInformation)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                rootName, 
                knownTypes, 
                maxItemsInObjectGraph, 
                ignoreExtensionDataObject, 
                dataContractSurrogate, 
                alwaysEmitTypeInformation);
        }

        public SystemInterface.Runtime.Serialization.Json.IDataContractJsonSerializer Create(
            System.Type type,
            System.Xml.XmlDictionaryString rootName,
            System.Collections.Generic.IEnumerable<System.Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            System.Runtime.Serialization.IDataContractSurrogate dataContractSurrogate,
            bool alwaysEmitTypeInformation)
        {
            return new SystemWrapper.Runtime.Serialization.Json.DataContractJsonSerializerWrap(
                type, 
                rootName, 
                knownTypes, 
                maxItemsInObjectGraph, 
                ignoreExtensionDataObject, 
                dataContractSurrogate, 
                alwaysEmitTypeInformation);
        }

        #endregion
    }
}