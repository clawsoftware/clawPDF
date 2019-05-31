using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace clawSoft.clawPDF.Shared.Helper
{
    public class TerminalServerDetection
    {
        [Flags]
        public enum SuiteMask : ushort
        {
            VER_SUITE_BACKOFFICE = 0x00000004,
            VER_SUITE_BLADE = 0x00000400,
            VER_SUITE_COMPUTE_SERVER = 0x00004000,
            VER_SUITE_DATACENTER = 0x00000080,
            VER_SUITE_ENTERPRISE = 0x00000002,
            VER_SUITE_EMBEDDEDNT = 0x00000040,
            VER_SUITE_PERSONAL = 0x00000200,
            VER_SUITE_SINGLEUSERTS = 0x00000100,
            VER_SUITE_SMALLBUSINESS = 0x00000001,
            VER_SUITE_SMALLBUSINESS_RESTRICTED = 0x00000020,
            VER_SUITE_STORAGE_SERVER = 0x00002000,
            VER_SUITE_TERMINAL = 0x00000010,
            VER_SUITE_WH_SERVER = 0x00008000
        }

        private readonly Func<OSVERSIONINFOEX> _queryFunc;

        public TerminalServerDetection(Func<OSVERSIONINFOEX> queryFunc)
        {
            _queryFunc = queryFunc;
        }

        public TerminalServerDetection()
        {
            _queryFunc = QueryWindowsVersion;
        }

        [DllImport("kernel32.dll")]
        public static extern bool GetVersionEx(ref OSVERSIONINFOEX osvi);

        private static OSVERSIONINFOEX QueryWindowsVersion()
        {
            var lWinVer = new OSVERSIONINFOEX();
            lWinVer.dwOSVersionInfoSize = Marshal.SizeOf(lWinVer);

            if (!GetVersionEx(ref lWinVer))
                throw new Win32Exception();

            return lWinVer;
        }

        public bool IsTerminalServer()
        {
            var lWinVer = _queryFunc();

            var isTerminal = (lWinVer.wSuiteMask & SuiteMask.VER_SUITE_TERMINAL) == SuiteMask.VER_SUITE_TERMINAL;
            var isSingleUserTs = (lWinVer.wSuiteMask & SuiteMask.VER_SUITE_SINGLEUSERTS) ==
                                 SuiteMask.VER_SUITE_SINGLEUSERTS;

            return isTerminal && !isSingleUserTs;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;

            //public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;

            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;

            public short wServicePackMajor;
            public short wServicePackMinor;
            public SuiteMask wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
    }
}