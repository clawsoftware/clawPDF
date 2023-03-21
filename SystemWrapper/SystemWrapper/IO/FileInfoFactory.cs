namespace SystemWrapper.IO
{
    using System.IO;

    using SystemInterface.IO;

    /// <summary>
    ///     Implements the factory responsible for the creation of <see cref="IFileInfo"/> instances.
    /// </summary>
    public class FileInfoFactory : IFileInfoFactory
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes a new instance of the <see cref="IFileInfo"/> class with the specified instance. 
        /// </summary>
        /// <param name="fileInfo">
        ///     A <see cref="FileInfo"/> object.
        /// </param>
        public IFileInfo Create(FileInfo fileInfo)
        {
            return new FileInfoWrap(fileInfo);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="IFileInfo"/> class on the specified fileName. 
        /// </summary>
        /// <param name="fileName">
        ///     The fully qualified name of the new file, or the relative file name.
        /// </param>
        public IFileInfo Create(string fileName)
        {
            return new FileInfoWrap(fileName);
        }

        #endregion
    }
}