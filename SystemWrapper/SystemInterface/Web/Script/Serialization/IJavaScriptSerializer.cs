namespace SystemInterface.Web.Script.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Script.Serialization;

    /// <summary>
    ///     Wrapper for <see cref="T:System.Web.Script.Serialization.JavaScriptSerializer"/> class.
    /// </summary>
    public interface IJavaScriptSerializer
    {
        /// <summary>
        ///     Gets or sets the maximum length of JSON strings that are accepted by the 
        ///     <see cref="T:System.Web.Script.Serialization.JavaScriptSerializer"/> class.
        /// </summary>
        /// <returns>
        ///     The maximum length of JSON strings. The default is 2097152 characters, which is equivalent to 4 MB of Unicode string data.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The property is set to a value that is less than one.
        /// </exception>
        int MaxJsonLength { get; set; }

        /// <summary>
        ///     Gets or sets the limit for constraining the number of object levels to process.
        /// </summary>
        /// <returns>
        ///     The number of object levels. The default is 100.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The property is set to a value that is less than one.
        /// </exception>
        int RecursionLimit { get; set; }

        /// <summary>
        ///     Converts the given object to the specified type.
        /// </summary>
        /// <param name="obj">
        ///     The object to convert.
        /// </param>
        /// <typeparam name="T">
        ///     The type to which <paramref name="obj"/> will be converted.
        /// </typeparam>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="obj"/> (or a nested member of <paramref name="obj"/>) contains a "__type" property that indicates a custom type, 
        ///     but the type resolver that is associated with the serializer cannot find a corresponding managed type.
        ///     -or- <paramref name="obj"/> (or a nested member of <paramref name="obj"/>) contains a "__type" property that indicates a custom type, 
        ///     but the result of deserializing the corresponding JSON string cannot be assigned to the expected target type.
        ///     -or- <paramref name="obj"/> (or a nested member of <paramref name="obj"/>) contains a "__type" property that indicates either 
        ///     <see cref="T:System.Object"/> or a non-instantiable type (for example, an abstract type or an interface).
        ///     -or- An attempt was made to convert <paramref name="obj"/> to an array-like managed type, which is not supported for use as a 
        ///     deserialization target. -or- It is not possible to convert <paramref name="obj"/> to <paramref name="T"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="obj"/> is a dictionary type and a non-string key value was encountered.
        ///     -or- <paramref name="obj"/> includes member definitions that are not available on type <paramref name="T"/>.
        /// </exception>
        /// <returns>
        ///     The object that has been converted to the target type.
        /// </returns>
        T ConvertToType<T>(object obj);

        /// <summary>
        ///     Converts the specified object to the specified type.
        /// </summary>
        /// <param name="obj">
        ///     The object to convert.
        /// </param>
        /// <param name="targetType">
        ///     The type to convert the object to.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The resulting JSON-formatted string exceeds the value of <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.MaxJsonLength"/>. 
        ///     -or- <paramref name="obj"/> contains a circular reference. A circular reference occurs when a child object has a reference to a parent 
        ///     object, and the parent object has a reference to the child object.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The recursion limit defined by <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.RecursionLimit"/> was exceeded.
        /// </exception>
        /// <returns>
        ///     The serialized JSON string.
        /// </returns>
        object ConvertToType(object obj,
                             Type targetType);

        /// <summary>
        ///     Converts the specified JSON string to an object of type <paramref name="T"/>.
        /// </summary>
        /// <param name="input">
        ///     The JSON string to be deserialized.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the resulting object.
        /// </typeparam>
        /// <exception cref="T:System.ArgumentException">
        ///     The <paramref name="input"/> length exceeds the value of <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.MaxJsonLength"/>.
        ///     -or- The recursion limit defined by <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.RecursionLimit"/> was exceeded. 
        ///     -or- <paramref name="input"/> contains an unexpected character sequence. 
        ///     -or- <paramref name="input"/> is a dictionary type and a non-string key value was encountered. 
        ///     -or- <paramref name="input"/> includes member definitions that are not available on type <paramref name="T"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="input"/> is null.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="input"/> contains a "__type" property that indicates a custom type, but the type resolver associated with the serializer 
        ///     cannot find a corresponding managed type. 
        ///     -or- <paramref name="input"/> contains a "__type" property that indicates a custom type, but the result of deserializing the corresponding 
        ///     JSON string cannot be assigned to the expected target type. 
        ///     -or- <paramref name="input"/> contains a "__type" property that indicates either <see cref="T:System.Object"/> or a non-instantiable type 
        ///     (for example, an abstract types or an interface). 
        ///     -or- An attempt was made to convert a JSON array to an array-like managed type that is not supported for use as a JSON deserialization target. 
        ///     -or- It is not possible to convert <paramref name="input"/> to <paramref name="T"/>.
        /// </exception>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        T Deserialize<T>(string input);

        /// <summary>
        ///     Converts a JSON-formatted string to an object of the specified type.
        /// </summary>
        /// <param name="input">
        ///     The JSON string to deserialize.
        /// </param>
        /// <param name="targetType">
        ///     The type of the resulting object.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="input"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The length of <paramref name="input"/> exceeds the value of <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.MaxJsonLength"/>. 
        ///     -or- The recursion limit defined by <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.RecursionLimit"/> was exceeded. 
        ///     -or- <paramref name="input"/> contains an unexpected character sequence. 
        ///     -or- <paramref name="input"/> is a dictionary type and a non-string key value was encountered. 
        ///     -or- <paramref name="input"/> includes member definitions that are not available on the target type.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="input"/> contains a "__type" property that indicates a custom type, but the type resolver that is currently associated 
        ///     with the serializer cannot find a corresponding managed type. 
        ///     -or- <paramref name="input"/> contains a "__type" property that indicates a custom type, but the result of deserializing the 
        ///     corresponding JSON string cannot be assigned to the expected target type. 
        ///     -or- <paramref name="input"/> contains a "__type" property that indicates either <see cref="T:System.Object"/> or a non-instantiable 
        ///     type (for example, an abstract type or an interface).
        ///     -or- An attempt was made to convert a JSON array to an array-like managed type that is not supported for use as a JSON deserialization 
        ///     target. 
        ///     -or- It is not possible to convert <paramref name="input"/> to the target type.
        /// </exception>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        object Deserialize(string input,
                           Type targetType);

        /// <summary>
        ///     Converts the specified JSON string to an object graph.
        /// </summary>
        /// <param name="input">
        ///     The JSON string to be deserialized.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="input"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The <paramref name="input"/> length exceeds the value of <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.MaxJsonLength"/>. 
        ///     -or- The recursion limit defined by <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.RecursionLimit"/> was exceeded. 
        ///     -or- <paramref name="input"/> contains an unexpected character sequence. -or- <paramref name="input"/> is a dictionary type and a non-string 
        ///     key value was encountered. 
        ///     -or- <paramref name="input"/> includes member definitions that are not available on the target type.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        ///     <paramref name="input"/> contains a "__type" property that indicates a custom type, but the type resolver that is currently associated with 
        ///     the serializer cannot find a corresponding managed type. 
        ///     -or- <paramref name="input"/> contains a "__type" property that indicates a custom type, but the result of deserializing the corresponding 
        ///     JSON string cannot be assigned to the expected target type. 
        ///     -or- <paramref name="input"/> contains a "__type" property that indicates either <see cref="T:System.Object"/> or a non-instantiable type 
        ///     (for example, an abstract type or an interface).
        ///     -or- An attempt was made to convert a JSON array to an array-like managed type that is not supported for use as a JSON deserialization target. 
        ///     -or- It is not possible to convert <paramref name="input"/> to the target type.
        /// </exception>
        /// <returns>
        ///     The deserialized object.
        /// </returns>
        object DeserializeObject(string input);

        /// <summary>
        ///     Registers a custom converter with the <see cref="T:System.Web.Script.Serialization.JavaScriptSerializer"/> instance.
        /// </summary>
        /// <param name="converters">
        ///     An array that contains the custom converters to be registered.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="converters"/> is null.
        /// </exception>
        void RegisterConverters(IEnumerable<JavaScriptConverter> converters);

        /// <summary>
        ///     Converts an object to a JSON string.
        /// </summary>
        /// <param name="obj">
        ///     The object to serialize.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The resulting JSON string exceeds the value of <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.MaxJsonLength"/>. 
        ///     -or- <paramref name="obj"/> contains a circular reference. A circular reference occurs when a child object has a reference to a 
        ///     parent object, and the parent object has a reference to the child object.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The recursion limit defined by <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.RecursionLimit"/> was exceeded.
        /// </exception>
        /// <returns>
        ///     The serialized JSON string.
        /// </returns>
        string Serialize(object obj);

        /// <summary>
        ///     Serializes an object and writes the resulting JSON string to the specified <see cref="T:System.Text.StringBuilder"/> object.
        /// </summary>
        /// <param name="obj">
        ///     The object to serialize.
        /// </param>
        /// <param name="output">
        ///     The <see cref="T:System.Text.StringBuilder"/> object that is used to write the JSON string.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The resulting JSON string exceeds the value of <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.MaxJsonLength"/>. 
        ///     -or- <paramref name="obj"/> contains a circular reference. A circular reference occurs when a child object has a reference to a 
        ///     parent object, and the parent object has a reference to the child object.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The recursion limit defined by <see cref="P:System.Web.Script.Serialization.JavaScriptSerializer.RecursionLimit"/> was exceeded.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="output"/> is null.
        /// </exception>
        void Serialize(object obj,
                       StringBuilder output);
    }
}