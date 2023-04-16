namespace SystemInterface.IO
{
    using System.IO;

    /// <summary>
    ///     Defines the contract for the factory responsible for the creation of <see cref="IFileInfo"/> instances.
    /// </summary>
    public interface IFileInfoFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes a new instance of the <see cref="IFileInfo"/> class with the specified instance. 
        /// </summary>
        /// <param name="fileInfo">
        ///     A <see cref="FileInfo"/> object.
        /// </param>
        IFileInfo Create(FileInfo fileInfo);

        /// <summary>
        ///     Initializes a new instance of the <see cref="IFileInfo"/> class on the specified fileName. 
        /// </summary>
        /// <param name="fileName">
        ///     The fully qualified name of the new file, or the relative file name.
        /// </param>
        IFileInfo Create(string fileName);

        #endregion
    }
}