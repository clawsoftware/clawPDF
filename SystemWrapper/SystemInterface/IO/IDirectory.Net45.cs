using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using SystemInterface.Security.AccessControl;

namespace SystemInterface.IO
{
    public partial interface IDirectory
    {
#if NET45
        IEnumerable<string> EnumerateFiles(string path);

        IEnumerable<string> EnumerateFiles(string path, string searchPattern);

        IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption);
#endif
    }
}