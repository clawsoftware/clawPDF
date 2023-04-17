namespace SystemWrapper.ActiveDirectory.Contracts
{
    using System.DirectoryServices.AccountManagement;

    /// <summary>
    /// 
    /// </summary>
    public interface IPrincipalCollection
    {
        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityType"></param>
        /// <param name="identityValue"></param>
        void Add(IPrincipalContext context,
                 IdentityType identityType,
                 string identityValue);

        #endregion
    }
}