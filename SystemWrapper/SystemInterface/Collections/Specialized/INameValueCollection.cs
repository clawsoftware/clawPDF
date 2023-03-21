namespace SystemInterface.Collections.Specialized
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    /// <summary>
    ///     Wrapper for <see cref="T:System.Configuration.ConfigurationManager"/> class.
    /// </summary>
    public interface INameValueCollection
    {
        #region Public Properties

        /// <summary>
        ///     Gets all the keys in the <see cref="T:System.Collections.Specialized.NameValueCollection"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> array that contains all the keys of the <see cref="T:System.Collections.Specialized.NameValueCollection"/>.
        /// </returns>
        string[] AllKeys { get; }

        /// <summary>
        ///     Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase"/> instance.
        /// </summary>
        /// <returns>
        ///     The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase"/> instance.
        /// </returns>
        int Count { get; }

        /// <summary>
        ///     Returns a <see cref='System.Collections.Specialized.NameObjectCollectionBase.KeysCollection'/> instance containing all the keys 
        ///     in the <see cref='System.Collections.Specialized.NameObjectCollectionBase'/> instance.
        /// </summary>
        NameObjectCollectionBase.KeysCollection Keys { get; }

        #endregion

        #region Public Indexers

        /// <summary>
        ///     Gets or sets the entry with the specified key in the <see cref="T:System.Collections.Specialized.NameValueCollection"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that contains the comma-separated list of values associated with the specified key, if found; otherwise, null.
        /// </returns>
        /// <param name="name">
        ///     The <see cref="T:System.String"/> key of the entry to locate. The key can be null.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The collection is read-only and the operation attempts to modify the collection.
        /// </exception>
        string this[string name] { get; set; }

        /// <summary>
        ///     Gets the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> that contains the comma-separated list of values at the specified index of the collection.
        /// </returns>
        /// <param name="index">
        ///     The zero-based index of the entry to locate in the collection.
        /// </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index"/> is outside the valid range of indexes for the collection.
        /// </exception>
        string this[int index] { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Copies the entries in the specified <see cref='System.Collections.Specialized.NameValueCollection'/> to the current <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="c">
        ///     The c.
        /// </param>
        void Add(NameValueCollection c);

        /// <summary>
        ///     Adds an entry with the specified name and value into the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        void Add(String name,
                 String value);

        /// <summary>
        ///     Invalidates the cached arrays and removes all entries from the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        void Clear();

        void CopyTo(Array dest,
                    int index);

        /// <summary>
        ///     Gets the values associated with the specified key from the <see cref='System.Collections.Specialized.NameValueCollection'/> combined into one comma-separated list.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        String Get(String name);

        /// <summary>
        ///     Gets the values at the specified index of the <see cref='System.Collections.Specialized.NameValueCollection'/> combined into one 
        ///     comma-separated list.
        /// </summary>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        String Get(int index);

        /// <summary>
        ///     Returns an enumerator that can iterate through the <see cref='System.Collections.Specialized.NameObjectCollectionBase'/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator GetEnumerator();

        /// <summary>
        ///     Gets the key at the specified index of the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        String GetKey(int index);

        /// <summary>
        ///     Gets the values associated with the specified key from the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="string[]"/>.
        /// </returns>
        String[] GetValues(String name);

        /// <summary>
        ///     Gets the values at the specified index of the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <returns>
        ///     The <see cref="string[]"/>.
        /// </returns>
        String[] GetValues(int index);

        /// <summary>
        ///     Gets a value indicating whether the <see cref='System.Collections.Specialized.NameValueCollection'/> contains entries whose keys are not <see langword='null'/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool"/>.
        /// </returns>
        bool HasKeys();

        /// <summary>
        ///     Removes the entries with the specified key from the <see cref='System.Collections.Specialized.NameObjectCollectionBase'/> instance.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        void Remove(String name);

        /// <summary>
        ///     Adds a value to an entry in the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        void Set(String name,
                 String value);

        /// <summary>
        ///     Returns a String which represents the object instance. The default for an object is to return the fully qualified name of the class.
        /// </summary>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        String ToString();

        #endregion
    }
}