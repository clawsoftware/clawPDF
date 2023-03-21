namespace SystemWrapper.ActiveDirectory
{
    using System;
    using System.DirectoryServices.AccountManagement;

    using SystemWrapper.ActiveDirectory.Contracts;

    /// <summary>
    /// 
    /// </summary>
    public class PrincipalContextWrap : IPrincipalContext
    {
        #region Fields

        private readonly PrincipalContext principalContext;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextType"></param>
        /// <param name="name"></param>
        public PrincipalContextWrap(ContextType contextType,
                                    string name)
        {
            this.principalContext = new PrincipalContext(contextType, name);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public PrincipalContext PrincipalContextInstance
        {
            get
            {
                return this.principalContext;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.principalContext.Dispose();
        }

        #endregion
    }
}
