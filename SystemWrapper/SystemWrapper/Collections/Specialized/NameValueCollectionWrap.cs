namespace SystemWrapper.Collections.Specialized
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;

    using SystemInterface.Collections.Specialized;

    public class NameValueCollectionWrap : INameValueCollection
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameValueCollectionWrap"/> class.
        /// </summary>
        public NameValueCollectionWrap()
        {
            this.Initialize();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameValueCollectionWrap"/> class.
        /// </summary>
        /// <param name="equalityComparer">
        ///     The equality comparer.
        /// </param>
        public NameValueCollectionWrap(IEqualityComparer equalityComparer)
        {
            this.Initialize(equalityComparer);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameValueCollectionWrap"/> class.
        /// </summary>
        /// <param name="col">
        ///     The col.
        /// </param>
        public NameValueCollectionWrap(NameValueCollection col)
        {
            this.Initialize(col);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameValueCollectionWrap"/> class.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        public NameValueCollectionWrap(int capacity)
        {
            this.Initialize(capacity);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameValueCollectionWrap"/> class.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <param name="equalityComparer">
        ///     The equality comparer.
        /// </param>
        public NameValueCollectionWrap(int capacity,
                                       IEqualityComparer equalityComparer)
        {
            this.Initialize(capacity, equalityComparer);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NameValueCollectionWrap"/> class.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <param name="col">
        ///     The col.
        /// </param>
        public NameValueCollectionWrap(int capacity,
                                       NameValueCollection col)
        {
            this.Initialize(capacity, col);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets all the keys in the <see cref="T:System.Collections.Specialized.NameValueCollection"/>.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String"/> array that contains all the keys of the <see cref="T:System.Collections.Specialized.NameValueCollection"/>.
        /// </returns>
        public string[] AllKeys
        {
            get
            {
                return this.NameValueCollectionInstance.AllKeys;
            }
        }

        /// <summary>
        ///     Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase"/> instance.
        /// </summary>
        /// <returns>
        ///     The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase"/> instance.
        /// </returns>
        public int Count
        {
            get
            {
                return this.NameValueCollectionInstance.Count;
            }
        }

        /// <summary>
        ///     Returns a <see cref='System.Collections.Specialized.NameObjectCollectionBase.KeysCollection'/> instance containing all the keys 
        ///     in the <see cref='System.Collections.Specialized.NameObjectCollectionBase'/> instance.
        /// </summary>
        public NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                return this.NameValueCollectionInstance.Keys;
            }
        }

        /// <summary>
        ///     Gets the name value collection instance.
        /// </summary>
        public NameValueCollection NameValueCollectionInstance { get; private set; }

        #endregion

        #region Explicit Interface Indexers

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
        string INameValueCollection.this[string name]
        {
            get
            {
                return this.NameValueCollectionInstance[name];
            }

            set
            {
                this.NameValueCollectionInstance[name] = value;
            }
        }

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
        string INameValueCollection.this[int index]
        {
            get
            {
                return this.NameValueCollectionInstance[index];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Copies the entries in the specified <see cref='System.Collections.Specialized.NameValueCollection'/> to the current <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="c">
        ///     The c.
        /// </param>
        public void Add(NameValueCollection c)
        {
            this.NameValueCollectionInstance.Add(c);
        }

        /// <summary>
        ///     Adds an entry with the specified name and value into the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        public void Add(string name,
                        string value)
        {
            this.NameValueCollectionInstance.Add(name, value);
        }

        /// <summary>
        ///     Invalidates the cached arrays and removes all entries from the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        public void Clear()
        {
            this.NameValueCollectionInstance.Clear();
        }

        public void CopyTo(Array dest,
                           int index)
        {
            this.NameValueCollectionInstance.CopyTo(dest, index);
        }

        /// <summary>
        ///     Gets the values associated with the specified key from the <see cref='System.Collections.Specialized.NameValueCollection'/> combined into one comma-separated list.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        public string Get(string name)
        {
            return this.NameValueCollectionInstance.Get(name);
        }

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
        public string Get(int index)
        {
            return this.NameValueCollectionInstance.Get(index);
        }

        /// <summary>
        ///     Returns an enumerator that can iterate through the <see cref='System.Collections.Specialized.NameObjectCollectionBase'/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.NameValueCollectionInstance.GetEnumerator();
        }

        /// <summary>
        ///     Gets the key at the specified index of the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <returns>
        ///     The <see cref="string"/>.
        /// </returns>
        public string GetKey(int index)
        {
            return this.NameValueCollectionInstance.GetKey(index);
        }

        /// <summary>
        ///     Gets the values associated with the specified key from the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <returns>
        ///     The <see cref="string[]"/>.
        /// </returns>
        public string[] GetValues(string name)
        {
            return this.NameValueCollectionInstance.GetValues(name);
        }

        /// <summary>
        ///     Gets the values at the specified index of the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="index">
        ///     The index.
        /// </param>
        /// <returns>
        ///     The <see cref="string[]"/>.
        /// </returns>
        public string[] GetValues(int index)
        {
            return this.NameValueCollectionInstance.GetValues(index);
        }

        /// <summary>
        ///     Gets a value indicating whether the <see cref='System.Collections.Specialized.NameValueCollection'/> contains entries whose keys are not <see langword='null'/>.
        /// </summary>
        /// <returns>
        ///     The <see cref="bool"/>.
        /// </returns>
        public bool HasKeys()
        {
            return this.NameValueCollectionInstance.HasKeys();
        }

        /// <summary>
        ///     Removes the entries with the specified key from the <see cref='System.Collections.Specialized.NameObjectCollectionBase'/> instance.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        public void Remove(string name)
        {
            this.NameValueCollectionInstance.Remove(name);
        }

        /// <summary>
        ///     Adds a value to an entry in the <see cref='System.Collections.Specialized.NameValueCollection'/>.
        /// </summary>
        /// <param name="name">
        ///     The name.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        public void Set(string name,
                        string value)
        {
            this.NameValueCollectionInstance.Set(name, value);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            this.NameValueCollectionInstance = new NameValueCollection();
        }

        private void Initialize(IEqualityComparer equalityComparer)
        {
            this.NameValueCollectionInstance = new NameValueCollection(equalityComparer);
        }

        private void Initialize(NameValueCollection col)
        {
            this.NameValueCollectionInstance = new NameValueCollection(col);
        }

        private void Initialize(int capacity)
        {
            this.NameValueCollectionInstance = new NameValueCollection(capacity);
        }

        private void Initialize(int capacity,
                                IEqualityComparer equalityComparer)
        {
            this.NameValueCollectionInstance = new NameValueCollection(capacity, equalityComparer);
        }

        private void Initialize(int capacity,
                                NameValueCollection col)
        {
            this.NameValueCollectionInstance = new NameValueCollection(capacity, col);
        }

        #endregion
    }
}