namespace SystemInterface.ActiveDirectory
{
    using System.DirectoryServices;

    /// <summary>
    /// 
    /// </summary>
    public interface ISearchResult
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        IResultPropertyCollection Properties { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDirectoryEntry GetDirectoryEntry();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        SearchResult GetSearchResult();

        #endregion
    };
}