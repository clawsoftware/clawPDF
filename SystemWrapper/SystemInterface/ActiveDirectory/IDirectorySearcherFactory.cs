namespace SystemInterface.ActiveDirectory
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDirectorySearcherFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryEntry">
        /// </param>
        /// <param name="pageSize">
        /// </param>
        /// <param name="sizeLimit">
        /// </param>
        /// <returns>
        /// </returns>
        IDirectorySearcher Create(IDirectoryEntry directoryEntry,
                                  int sizeLimit = 20,
                                  int? pageSize = null);

        #endregion
    }
}