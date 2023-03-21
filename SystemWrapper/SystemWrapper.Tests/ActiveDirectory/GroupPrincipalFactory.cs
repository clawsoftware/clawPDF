namespace SystemWrapper.Tests.ActiveDirectory
{
    using System.DirectoryServices.AccountManagement;

    using SystemWrapper.ActiveDirectory;
    using SystemWrapper.ActiveDirectory.Contracts;

    public class GroupPrincipalFactory : IGroupPrincipalFactory
    {
        #region Public Methods and Operators

        public IGroupPrincipal Create(IPrincipalContext principalContext,
                                      string groupName)
        {
            var groupPrincipal = GroupPrincipal.FindByIdentity(principalContext.PrincipalContextInstance, groupName);
            return new GroupPrincipalWrap(groupPrincipal);
        }

        #endregion
    }
}