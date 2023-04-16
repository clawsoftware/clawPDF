namespace SystemWrapper.Tests.ActiveDirectory
{
    using SystemWrapper.ActiveDirectory.Contracts;

    public interface IGroupPrincipalFactory
    {
        #region Public Methods and Operators

        IGroupPrincipal Create(IPrincipalContext principalContext,
                               string groupName);

        #endregion
    }
}