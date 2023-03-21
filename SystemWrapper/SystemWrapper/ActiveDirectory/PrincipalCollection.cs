namespace SystemWrapper.ActiveDirectory
{
    using System.DirectoryServices.AccountManagement;

    using SystemWrapper.ActiveDirectory.Contracts;

    /// <summary>
    /// 
    /// </summary>
    public class PrincipalCollectionWrap : IPrincipalCollection
    {
        #region Fields

        private readonly PrincipalCollection principalCollection;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        public PrincipalCollectionWrap(PrincipalCollection principalCollection)
        {
            this.principalCollection = principalCollection;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identityType"></param>
        /// <param name="identityValue"></param>
        public void Add(IPrincipalContext context,
                        IdentityType identityType,
                        string identityValue)
        {
            this.principalCollection.Add(context.PrincipalContextInstance, identityType, identityValue);
        }

        #endregion
    }
}