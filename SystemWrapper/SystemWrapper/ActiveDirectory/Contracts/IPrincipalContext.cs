namespace SystemWrapper.ActiveDirectory.Contracts
{
    using System;
    using System.DirectoryServices.AccountManagement;

    /// <summary>
    /// 
    /// </summary>
    public interface IPrincipalContext : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        PrincipalContext PrincipalContextInstance { get; }

        #endregion
    }
}