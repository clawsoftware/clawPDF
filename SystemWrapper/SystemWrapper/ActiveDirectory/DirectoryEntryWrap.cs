namespace SystemWrapper.ActiveDirectory
{
    using System;
    using System.DirectoryServices;

    using SystemInterface.ActiveDirectory;

    /// <summary>
    /// 
    /// </summary>
    public class DirectoryEntryWrap : IDirectoryEntry
    {
        #region Fields

        private readonly DirectoryEntry directoryEntry;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forestGc">
        /// </param>
        public DirectoryEntryWrap(string forestGc)
        {
            this.directoryEntry = new DirectoryEntry(forestGc);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryEntry">
        /// </param>
        public DirectoryEntryWrap(DirectoryEntry directoryEntry)
        {
            this.directoryEntry = directoryEntry;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public AuthenticationTypes AuthenticationType
        {
            get
            {
                return this.directoryEntry.AuthenticationType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public Guid Guid
        {
            get
            {
                return this.directoryEntry.Guid;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return this.directoryEntry.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string NativeGuid
        {
            get
            {
                return this.directoryEntry.NativeGuid;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set
            {
                this.directoryEntry.Password = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            get
            {
                return this.directoryEntry.Path;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SchemaClassName
        {
            get
            {
                return this.directoryEntry.SchemaClassName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Username
        {
            get
            {
                return this.directoryEntry.Username;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            this.directoryEntry.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public void CommitChanges()
        {
            this.directoryEntry.CommitChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public void DeleteTree()
        {
            this.directoryEntry.DeleteTree();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates whether or not unmanaged resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            this.directoryEntry.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        public DirectoryEntry GetDirectoryEntry()
        {
            return this.directoryEntry;
        }

        #endregion
    }
}
