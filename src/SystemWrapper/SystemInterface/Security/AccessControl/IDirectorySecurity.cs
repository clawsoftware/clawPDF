using System.Security.AccessControl;

namespace SystemInterface.Security.AccessControl
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Security.AccessControl.DirectorySecurity"/> class.
    /// </summary>
    public interface IDirectorySecurity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemInterface.Security.AccessControl.DirectorySecurityWrap"/> class on the specified path.
        /// </summary>
        /// <param name="directorySecurity">A <see cref="T:System.Security.AccessControl.DirectorySecurity"/> object.</param>
        void Initialize(DirectorySecurity directorySecurity);

        /// <summary>
        /// Gets <see cref="T:System.Security.AccessControl.DirectorySecurity"/> object.
        /// </summary>
        DirectorySecurity DirectorySecurityInstance { get; }
    }
}
