namespace SystemInterface.ActiveDirectory
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDirectoryEntryFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forestGc"></param>
        /// <returns></returns>
        IDirectoryEntry Create(string forestGc);

        #endregion
    }
}