namespace SystemInterface.Collections.Specialized
{
    using System.Collections;
    using System.Collections.Specialized;

    /// <summary>
    ///     Factory that creates a new <see cref="INameValueCollection"/> instance.
    /// </summary>
    public interface INameValueCollectionFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Creates a new <see cref="INameValueCollection"/> instance using the default constructor.
        /// </summary>
        /// <returns>
        ///     The <see cref="INameValueCollection"/>.
        /// </returns>
        INameValueCollection Create();

        /// <summary>
        ///     Creates a new <see cref="INameValueCollection"/> instance passing the equality comparer.
        /// </summary>
        /// <param name="equalityComparer">
        ///     The equality comparer.
        /// </param>
        /// <returns>
        ///     The <see cref="INameValueCollection"/>.
        /// </returns>
        INameValueCollection Create(IEqualityComparer equalityComparer);

        /// <summary>
        ///     Creates a new <see cref="INameValueCollection"/> instance passing the name value collection.
        /// </summary>
        /// <param name="col">
        ///     The col.
        /// </param>
        /// <returns>
        ///     The <see cref="INameValueCollection"/>.
        /// </returns>
        INameValueCollection Create(NameValueCollection col);

        /// <summary>
        ///     Creates a new <see cref="INameValueCollection"/> instance passing the capacity.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <returns>
        ///     The <see cref="INameValueCollection"/>.
        /// </returns>
        INameValueCollection Create(int capacity);

        /// <summary>
        ///     Creates a new <see cref="INameValueCollection"/> instance passing the capacity and the equality comparer.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <param name="equalityComparer">
        ///     The equality comparer.
        /// </param>
        /// <returns>
        ///     The <see cref="INameValueCollection"/>.
        /// </returns>
        INameValueCollection Create(int capacity,
                                    IEqualityComparer equalityComparer);

        /// <summary>
        ///     Creates a new <see cref="INameValueCollection"/> instance passing the capacity and the name value collection.
        /// </summary>
        /// <param name="capacity">
        ///     The capacity.
        /// </param>
        /// <param name="col">
        ///     The col.
        /// </param>
        /// <returns>
        ///     The <see cref="INameValueCollection"/>.
        /// </returns>
        INameValueCollection Create(int capacity,
                                    NameValueCollection col);

        #endregion
    }
}