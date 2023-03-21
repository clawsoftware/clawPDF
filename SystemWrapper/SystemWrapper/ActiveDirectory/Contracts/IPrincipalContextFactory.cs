namespace SystemWrapper.ActiveDirectory.Contracts
{
    using System.DirectoryServices.AccountManagement;

    /// <summary>
    /// 
    /// </summary>
    public interface IPrincipalContextFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IPrincipalContext Create(ContextType contextType,
                                 string name);

        #endregion
    }
}