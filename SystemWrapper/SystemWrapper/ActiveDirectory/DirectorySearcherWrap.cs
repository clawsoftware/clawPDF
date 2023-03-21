namespace SystemWrapper.ActiveDirectory
{
    using System;
    using System.Collections.Specialized;
    using System.DirectoryServices;

    using SystemInterface.ActiveDirectory;

    /// <summary>
    /// 
    /// </summary>
    public class DirectorySearcherWrap : IDirectorySearcher
    {
        #region Fields

        private readonly DirectorySearcher directorySearcher;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryEntry">
        /// </param>
        /// <param name="sizeLimit">
        /// </param>
        /// <param name="pageSize">
        /// </param>
        public DirectorySearcherWrap(IDirectoryEntry directoryEntry,
                                     int sizeLimit,
                                     int? pageSize)
        {
            this.directorySearcher = new DirectorySearcher(directoryEntry.GetDirectoryEntry())
                                         {
                                             SizeLimit = sizeLimit,
                                         };
            if (pageSize.HasValue)
            {
                this.directorySearcher.PageSize = pageSize.Value;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public bool CacheResults
        {
            get
            {
                return this.directorySearcher.CacheResults;
            }
            set
            {
                this.directorySearcher.CacheResults = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Filter
        {
            get
            {
                return this.directorySearcher.Filter;
            }
            set
            {
                this.directorySearcher.Filter = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public int PageSize
        {
            get
            {
                return this.directorySearcher.PageSize;
            }

            set
            {
                this.directorySearcher.PageSize = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public StringCollection PropertiesToLoad
        {
            get
            {
                return this.directorySearcher.PropertiesToLoad;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SizeLimit
        {
            get
            {
                return this.directorySearcher.SizeLimit;
            }
            set
            {
                this.directorySearcher.SizeLimit = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.directorySearcher.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public ISearchResultCollection FindAll()
        {
            var searchResultCollection = this.directorySearcher.FindAll();
            return new SearchResultCollectionWrap(searchResultCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public ISearchResult FindOne()
        {
            var searchResult = this.directorySearcher.FindOne();
            if (searchResult != null)
            {
                var searchResultWrap = new SearchResultWrap(searchResult);
                return searchResultWrap;
            }
            return null;
        }

        #endregion
    }
}
