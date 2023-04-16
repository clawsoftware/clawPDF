using System.Security.AccessControl;
using SystemInterface.Security.AccessControl;

namespace SystemWrapper.Security.AccessControl
{
    /// <summary>
    /// Wrapper for <see cref="T:System.Security.AccessControl.FileSecurity"/> class.
    /// </summary>
    public class FileSecurityWrap : IFileSecurity
    {
        private FileSecurity _fileSecurity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileSecurity">A FileSecurity object.</param>
        public FileSecurityWrap(FileSecurity fileSecurity)
        {
            Initialize(fileSecurity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        /// <param name="fileSecurity">A FileSecurity object.</param>
        public void Initialize(FileSecurity fileSecurity)
        {
            _fileSecurity = fileSecurity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        public FileSecurityWrap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SystemWrapper.Security.AccessControl.FileSecurityWrap"/> class on the specified path.
        /// </summary>
        public void Initialize()
        {
            _fileSecurity = new FileSecurity();
        }

        /// <inheritdoc />
        public FileSecurity FileSecurityInstance
        {
            get { return _fileSecurity; }
        }
    }
}