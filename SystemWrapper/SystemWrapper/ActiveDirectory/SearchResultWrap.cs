namespace SystemWrapper.ActiveDirectory
{
    using System.DirectoryServices;

    using SystemInterface.ActiveDirectory;

    /// <summary>
    /// 
    /// </summary>
    public class SearchResultWrap : ISearchResult
    {
        #region Fields

        private readonly SearchResult searchResult;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchResult">
        /// </param>
        public SearchResultWrap(SearchResult searchResult)
        {
            this.searchResult = searchResult;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public IResultPropertyCollection Properties
        {
            get
            {
                var p = new ResultPropertyCollectionWrap(this.searchResult.Properties);
                return p;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDirectoryEntry GetDirectoryEntry()
        {
            return new DirectoryEntryWrap(this.searchResult.GetDirectoryEntry());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SearchResult GetSearchResult()
        {
            return this.searchResult;
        }

        #endregion
    }
}