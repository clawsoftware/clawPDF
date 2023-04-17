namespace SystemWrapper.Security.Certificate
{
    using System;
    using System.Collections;
    using System.Security.Cryptography.X509Certificates;

    using SystemInterface.IO;
    using SystemInterface.Security.Certificate;

    /// <summary>
    /// Wrapper for <see cref="X509ChainElementCollection"/>
    /// </summary>
    public class X509ChainElementCollectionWrap : IX509ChainElementCollection,
                                                  ICollection
    {
        #region Fields

        private readonly IFile file;

        private readonly IPath path;

        /// <summary>
        /// The elements.
        /// </summary>
        private IX509ChainElement[] elements;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        /// <param name="file">
        /// </param>
        /// <param name="path">
        /// </param>
        public X509ChainElementCollectionWrap(IFile file,
                                              IPath path)
        {
            this.file = file;
            this.path = path;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainElementCollectionWrap"/> class.
        /// </summary>
        internal X509ChainElementCollectionWrap()
        {
            this.elements = new IX509ChainElement[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainElementCollectionWrap"/> class.
        /// </summary>
        /// <param name="collection">
        /// The collection of X509ChainElement objects.
        /// </param>
        internal X509ChainElementCollectionWrap(X509ChainElementCollection collection)
        {
            this.Initialize(collection);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public int Count
        {
            get
            {
                return this.elements.Length;
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <returns>
        /// true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
        /// </returns>
        public bool IsSynchronized { get; private set; }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public object SyncRoot { get; private set; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets the IPamsX509ChainElement object at the specified index.
        /// </summary>
        /// <param name="index">
        /// An integer value.
        /// </param>
        /// <returns>
        /// The <see cref="IX509ChainElement"/>.
        /// </returns>
        public IX509ChainElement this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new InvalidOperationException("Index must be greater or equal to zero");
                }

                if (index >= this.elements.Length)
                {
                    throw new ArgumentOutOfRangeException("index", "Out of range.");
                }

                return this.elements[index];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing. </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins. </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.-or-The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
        public void CopyTo(Array array,
                           int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Rank != 1)
            {
                throw new ArgumentException("Rank");
            }

            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (index + this.Count > array.Length)
            {
                throw new ArgumentException("length");
            }

            for (var i = 0; i < this.Count; ++i)
            {
                array.SetValue(this[i], index);
                ++i;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return new X509ChainElementEnumeratorWrap(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X509ChainElementCollectionWrap"/> class.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        public void Initialize(X509ChainElementCollection collection)
        {
            this.elements = new IX509ChainElement[collection.Count];
            var elementFactory = new X509ChainElementFactory(this.file, this.path);
            for (var i = 0; i < this.elements.Length; i++)
            {
                this.elements[i] = elementFactory.Create(collection[i]);
            }
        }

        #endregion
    }
}