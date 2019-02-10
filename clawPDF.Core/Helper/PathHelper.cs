using System.Runtime.InteropServices;
using System.Text;

namespace clawSoft.clawPDF.Core.Helper
{
    public class PathHelper
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)] string path,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder shortPath,
            int bufferSize
        );

        //public static string GetShortPathName(string path)
        //{
        //    var buffer = new StringBuilder(256);
        //    var result = GetShortPathName(path, buffer, buffer.Capacity);

        //    if (result == 0)
        //        return path;

        //    return buffer.ToString();
        //}
    }
}