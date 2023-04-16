namespace SystemWrapper.ActiveDirectory
{
    using System.DirectoryServices.AccountManagement;

    using SystemWrapper.ActiveDirectory.Contracts;

    /// <summary>
    /// 
    /// </summary>
    public class GroupPrincipalWrap : IGroupPrincipal
    {
        #region Fields

        private readonly GroupPrincipal groupPrincipal;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        public GroupPrincipalWrap(GroupPrincipal groupPrincipal)
        {
            this.groupPrincipal = groupPrincipal;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public IPrincipalCollection Members
        {
            get
            {
                return new PrincipalCollectionWrap(this.groupPrincipal.Members);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="principalContext"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static IGroupPrincipal FindByIdentity(IPrincipalContext principalContext,
                                                     string groupName)
        {
            return new GroupPrincipalWrap(GroupPrincipal.FindByIdentity(principalContext.PrincipalContextInstance, groupName));
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            this.groupPrincipal.Save();
        }

        #endregion
    }
}