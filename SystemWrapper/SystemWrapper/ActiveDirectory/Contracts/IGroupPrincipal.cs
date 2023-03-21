namespace SystemWrapper.ActiveDirectory.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGroupPrincipal
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        IPrincipalCollection Members { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        void Save();

        #endregion
    }
}