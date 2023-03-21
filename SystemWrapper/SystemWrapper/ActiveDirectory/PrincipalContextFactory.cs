namespace SystemWrapper.ActiveDirectory
{
    using System.DirectoryServices.AccountManagement;

    using SystemWrapper.ActiveDirectory.Contracts;

    /// <summary>
    /// 
    /// </summary>
    public class PrincipalContextFactory : IPrincipalContextFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public IPrincipalContext Create(ContextType contextType,
                                        string name)
        {
            return new PrincipalContextWrap(contextType, name);
        }

        #endregion
    }
}