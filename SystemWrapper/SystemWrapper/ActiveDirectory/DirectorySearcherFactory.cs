namespace SystemWrapper.ActiveDirectory
{
    using SystemInterface.ActiveDirectory;

    /// <summary>
    /// 
    /// </summary>
    public class DirectorySearcherFactory : IDirectorySearcherFactory
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
        public IDirectorySearcher Create(IDirectoryEntry directoryEntry,
                                         int sizeLimit = 20,
                                         int? pageSize = null)
        {
            return new DirectorySearcherWrap(directoryEntry, sizeLimit, pageSize);
        }

        #endregion
    }
}