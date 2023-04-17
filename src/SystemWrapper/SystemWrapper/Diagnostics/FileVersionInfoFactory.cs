using System;
using System.Diagnostics;
using SystemInterface.Diagnostics;

namespace SystemWrapper.Diagnostics
{
    /// <summary>
    /// Wrapper implementation of static methods from the <see cref="FileVersionInfo"/> class.
    /// </summary>
    public class FileVersionInfoFactory : IFileVersionInfoFactory
    {
        /// <inheritdoc />
        public IFileVersionInfo GetVersionInfo(string fileName)
        {
            return new FileVersionInfoWrap(FileVersionInfo.GetVersionInfo(fileName));
        }
    }
}
