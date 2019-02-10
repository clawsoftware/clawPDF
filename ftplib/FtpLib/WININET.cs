using System;
using System.Runtime.InteropServices;
using System.Text;

namespace clawSoft.clawPDF.ftplib.FtpLib
{
    public static class WININET
    {
        public const int INTERNET_SERVICE_FTP = 1;

        public const int INTERNET_OPEN_TYPE_PRECONFIG = 0;

        public const int INTERNET_OPEN_TYPE_DIRECT = 1;

        public const int INTERNET_DEFAULT_FTP_PORT = 21;

        public const int INTERNET_NO_CALLBACK = 0;

        public const int FTP_TRANSFER_TYPE_UNKNOWN = 0;

        public const int FTP_TRANSFER_TYPE_ASCII = 1;

        public const int FTP_TRANSFER_TYPE_BINARY = 2;

        public const int INTERNET_FLAG_HYPERLINK = 1024;

        public const int INTERNET_FLAG_NEED_FILE = 16;

        public const int INTERNET_FLAG_NO_CACHE_WRITE = 67108864;

        public const int INTERNET_FLAG_RELOAD = 8;

        public const int INTERNET_FLAG_RESYNCHRONIZE = 2048;

        public const int INTERNET_FLAG_ASYNC = 268435456;

        public const int INTERNET_FLAG_SYNC = 4;

        public const int INTERNET_FLAG_FROM_CACHE = 16777216;

        public const int INTERNET_FLAG_OFFLINE = 16777216;

        public const int INTERNET_FLAG_PASSIVE = 134217728;

        public const int INTERNET_ERROR_BASE = 12000;

        public const int ERROR_INTERNET_EXTENDED_ERROR = 12003;

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr InternetOpen([In] string agent, [In] int dwAccessType, [In] string proxyName,
            [In] string proxyBypass, [In] int dwFlags);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr InternetConnect([In] IntPtr hInternet, [In] string serverName, [In] int serverPort,
            [In] string userName, [In] string password, [In] int dwService, [In] int dwFlags, [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int InternetCloseHandle([In] IntPtr hInternet);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpCommand([In] IntPtr hConnect, [In] bool fExpectResponse, [In] int dwFlags,
            [In] string command, [In] IntPtr dwContext, [In] [Out] ref IntPtr ftpCommand);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpCreateDirectory([In] IntPtr hConnect, [In] string directory);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpDeleteFile([In] IntPtr hConnect, [In] string fileName);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FtpFindFirstFile([In] IntPtr hConnect, [In] string searchFile,
            [In] [Out] ref WINAPI.WIN32_FIND_DATA findFileData, [In] int dwFlags, [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpGetCurrentDirectory([In] IntPtr hConnect, [In] [Out] StringBuilder currentDirectory,
            [In] [Out] ref int dwCurrentDirectory);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpGetFile([In] IntPtr hConnect, [In] string remoteFile, [In] string newFile,
            [In] bool failIfExists, [In] int dwFlagsAndAttributes, [In] int dwFlags, [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpGetFileSize([In] IntPtr hConnect, [In] [Out] ref int dwFileSizeHigh);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpOpenFile([In] IntPtr hConnect, [In] string fileName, [In] uint dwAccess,
            [In] int dwFlags, [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpPutFile([In] IntPtr hConnect, [In] string localFile, [In] string newRemoteFile,
            [In] int dwFlags, [In] IntPtr dwContext);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpRemoveDirectory([In] IntPtr hConnect, [In] string directory);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpRenameFile([In] IntPtr hConnect, [In] string existingName, [In] string newName);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int FtpSetCurrentDirectory([In] IntPtr hConnect, [In] string directory);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int InternetFindNextFile([In] IntPtr hInternet,
            [In] [Out] ref WINAPI.WIN32_FIND_DATA findData);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int InternetGetLastResponseInfo([In] [Out] ref int dwError,
            [Out] [MarshalAs(UnmanagedType.LPTStr)]
            StringBuilder buffer, [In] [Out] ref int bufferLength);

        [DllImport("wininet.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int InternetReadFile([In] IntPtr hConnect, [In] [Out] [MarshalAs(UnmanagedType.LPTStr)]
            StringBuilder buffer, [In] int buffCount, [In] [Out] ref int bytesRead);

        [DllImport("wininet.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int InternetReadFileEx([In] IntPtr hFile,
            [In] [Out] ref WINAPI.INTERNET_BUFFERS lpBuffersOut, [In] int dwFlags, [In] [Out] int dwContext);
    }
}