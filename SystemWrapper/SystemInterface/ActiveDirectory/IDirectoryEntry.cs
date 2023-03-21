namespace SystemInterface.ActiveDirectory
{
    using System;
    using System.DirectoryServices;

    /// <summary>
    /// 
    /// </summary>
    public interface IDirectoryEntry : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string NativeGuid { get; }

        /// <summary>
        /// 
        /// </summary>
        string Password { set; }

        /// <summary>
        /// 
        /// </summary>
        string Path { get; }

        /// <summary>
        /// 
        /// </summary>
        string SchemaClassName { get; }

        /// <summary>
        /// 
        /// </summary>
        string Username { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        void Close();

        /// <summary>
        /// 
        /// </summary>
        void CommitChanges();

        /// <summary>
        /// 
        /// </summary>
        void DeleteTree();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        DirectoryEntry GetDirectoryEntry();

        #endregion
    }
}