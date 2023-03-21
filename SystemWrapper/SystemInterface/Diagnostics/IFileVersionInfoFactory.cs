using System;
using System.Diagnostics;

namespace SystemInterface.Diagnostics
{
    /// <summary>
    /// Interface for wrapping static methods of the <see cref="FileVersionInfo"/> class.
    /// </summary>
    public interface IFileVersionInfoFactory : IStaticWrapper
    {
        /// <summary>
        /// Returns a <see cref="IFileVersionInfo"/> representing the version information associated with the specified file.
        /// </summary>
        /// <param name="fileName">The fully qualified path and name of the file to retrieve the version information for.</param>
        /// <returns>
        /// A <see cref="IFileVersionInfo"/> containing information about the file. If the file did not contain version
        /// information, the <see cref="IFileVersionInfo"/> contains only the name of the file requested.
        /// </returns>
        IFileVersionInfo GetVersionInfo(string fileName);
    }
}
