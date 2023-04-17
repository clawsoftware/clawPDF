namespace SystemWrapper.ActiveDirectory
{
    using SystemInterface.ActiveDirectory;

    /// <summary>
    /// 
    /// </summary>
    public class DirectoryEntryFactory : IDirectoryEntryFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forestGc"></param>
        /// <returns></returns>
        public IDirectoryEntry Create(string forestGc)
        {
            return new DirectoryEntryWrap(forestGc);
        }

        #endregion
    }
}