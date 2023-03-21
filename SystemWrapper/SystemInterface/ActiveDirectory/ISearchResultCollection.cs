namespace SystemInterface.ActiveDirectory
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public interface ISearchResultCollection : ICollection<ISearchResult>,
                                               IDisposable
    {
        #region Public Indexers

        /// <summary>
        /// Gets the <see cref="T:System.DirectoryServices.SearchResult"/> object that is located at a specified index in this collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.DirectoryServices.SearchResult"/> object that is located at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.SearchResult"/> object to retrieve.</param>
        ISearchResult this[int index] { get; }

        #endregion
    }
}