using System;
using System.Runtime.InteropServices;
using System.Text;

namespace clawSoft.clawPDF.ftplib.FtpLib
{
    public static class WINAPI
    {
        public const int MAX_PATH = 260;

        public const int NO_ERROR = 0;

        public const int FILE_ATTRIBUTE_NORMAL = 128;

        public const int FILE_ATTRIBUTE_DIRECTORY = 16;

        public const int ERROR_NO_MORE_FILES = 18;

        public const uint GENERIC_READ = 2147483648u;

        private const uint FORMAT_MESSAGE_IGNORE_INSERTS = 512u;

        private const uint FORMAT_MESSAGE_FROM_HMODULE = 2048u;

        private const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096u;

        public static string TranslateInternetError(uint errorCode)
        {
            var intPtr = IntPtr.Zero;
            try
            {
                var stringBuilder = new StringBuilder(255);
                intPtr = LoadLibrary("wininet.dll");
                if (FormatMessage(4608u, intPtr, errorCode, 0u, stringBuilder, (uint)(stringBuilder.Capacity + 1),
                        IntPtr.Zero) != 0) return stringBuilder.ToString();
                return string.Empty;
            }
            catch
            {
                return "Unknown Error";
            }
            finally
            {
                FreeLibrary(intPtr);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpBuffer, uint nSize, IntPtr Arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary([In] [MarshalAs(UnmanagedType.LPTStr)] string lpLibFileName);

        public struct FILETIME
        {
            public int dwLowDateTime;

            public int dwHighDateTime;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WIN32_FIND_DATA
        {
            public int dfFileAttributes;

            public FILETIME ftCreationTime;

            public FILETIME ftLastAccessTime;

            public FILETIME ftLastWriteTime;

            public int nFileSizeHigh;

            public int nFileSizeLow;

            public int dwReserved0;

            public int dwReserved1;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
            public char[] fileName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public char[] alternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct INTERNET_BUFFERS
        {
            public int dwStructSize;

            public IntPtr Next;

            public string Header;

            public int dwHeadersLength;

            public int dwHeadersTotal;

            public IntPtr lpvBuffer;

            public int dwBufferLength;

            public int dwBufferTotal;

            public int dwOffsetLow;

            public int dwOffsetHigh;
        }
    }
}