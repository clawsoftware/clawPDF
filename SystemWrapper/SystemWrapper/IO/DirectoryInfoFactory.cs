namespace SystemWrapper.IO
{
    using System.IO;

    using SystemInterface.IO;

    /// <summary>
    ///     Implements the factory responsible for the creation of <see cref="IDirectoryInfo"/> instances.
    /// </summary>
    public class DirectoryInfoFactory : IDirectoryInfoFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes a new instance of the <see cref="IDirectoryInfo"/> class with the specified instance. 
        /// </summary>
        /// <param name="directoryInfo">
        ///     A <see cref="DirectoryInfo"/> object.
        /// </param>
        public IDirectoryInfo Create(DirectoryInfo directoryInfo)
        {
            return new DirectoryInfoWrap(directoryInfo);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="IDirectoryInfo"/> class on the specified path. 
        /// </summary>
        /// <param name="path">
        ///     A string specifying the path on which to create the <see cref="IDirectoryInfo"/>.
        /// </param>
        public IDirectoryInfo Create(string path)
        {
            return new DirectoryInfoWrap(path);
        }

        #endregion
    }
}