namespace SystemInterface.IO
{
    using System.IO;

    /// <summary>
    ///     Defines the contract for the factory responsible for the creation of <see cref="IDirectoryInfo"/> instances.
    /// </summary>
    public interface IDirectoryInfoFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes a new instance of the <see cref="IDirectoryInfo"/> class with the specified instance. 
        /// </summary>
        /// <param name="directoryInfo">
        ///     A <see cref="DirectoryInfo"/> object.
        /// </param>
        IDirectoryInfo Create(DirectoryInfo directoryInfo);

        /// <summary>
        ///     Initializes a new instance of the <see cref="IDirectoryInfo"/> class on the specified path. 
        /// </summary>
        /// <param name="path">
        ///     A string specifying the path on which to create the <see cref="IDirectoryInfo"/>.
        /// </param>
        IDirectoryInfo Create(string path);

        #endregion
    }
}