namespace SystemInterface.ActiveDirectory
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// 
    /// </summary>
    public interface IDirectorySearcher : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        bool CacheResults { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        StringCollection PropertiesToLoad { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        int SizeLimit { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        ISearchResultCollection FindAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        ISearchResult FindOne();

        #endregion
    }
}