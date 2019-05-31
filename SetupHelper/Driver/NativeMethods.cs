using System;
using System.Runtime.InteropServices;
using System.Text;

namespace clawSoft.clawPDF.SetupHelper.Driver
{
    #region Native Method Structures

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MONITOR_INFO_2
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pName;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pEnvironment;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDLLName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct FILETIME
    {
        public UInt32 dwLowDateTime;
        public UInt32 dwHighDateTime;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DRIVER_INFO_2
    {
        public uint cVersion;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pName;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pEnvironment;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDriverPath;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDataFile;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pConfigFile;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DRIVER_INFO_6
    {
        public UInt32 cVersion;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pName;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pEnvironment;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDriverPath;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDataFile;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pConfigFile;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pHelpFile;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDependentFiles;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pMonitorName;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pDefaultDataType;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszzPreviousNames;

        public FILETIME ftDriverDate;
        public UInt64 dwlDriverVersion;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszMfgName;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszOEMUrl;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszHardwareID;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszProvider;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class PRINTER_DEFAULTS
    {
        public string pDatatype;
        public IntPtr pDevMode;
        public int DesiredAccess;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct PRINTER_INFO_2
    {
        public string pServerName;
        public string pPrinterName;
        public string pShareName;
        public string pPortName;
        public string pDriverName;
        public string pComment;
        public string pLocation;
        public IntPtr pDevMode;
        public string pSepFile;
        public string pPrintProcessor;
        public string pDatatype;
        public string pParameters;
        public IntPtr pSecurityDescriptor;
        public uint Attributes;
        public uint Priority;
        public uint DefaultPriority;
        public uint StartTime;
        public uint UntilTime;
        public uint Status;
        public uint cJobs;
        public uint AveragePPM;
    }

    #endregion Native Method Structures

    internal static class NativeMethods
    {
        #region winspool

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool EnumMonitors(string pName, uint level, IntPtr pMonitors, uint cbBuf, ref uint pcbNeeded, ref uint pcReturned);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern Int32 AddMonitor(String pName, UInt32 Level, ref MONITOR_INFO_2 pMonitors);

        [DllImport("winspool.drv", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern Int32 DeleteMonitor(String pName, String pEnvironment, String pMonitorName);

        [DllImport("winspool.drv", EntryPoint = "XcvDataW", SetLastError = true)]
        internal static extern bool XcvData(IntPtr hXcv,
                                        [MarshalAs(UnmanagedType.LPWStr)] string pszDataName,
                                        IntPtr pInputData,
                                        uint cbInputData,
                                        IntPtr pOutputData,
                                        uint cbOutputData,
                                        out uint pcbOutputNeeded,
                                        out uint pwdStatus);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int AddPrinter(string pName, uint Level, [In] ref PRINTER_INFO_2 pPrinter);

        [DllImport("winspool.drv", EntryPoint = "OpenPrinterA", SetLastError = true)]
        internal static extern int OpenPrinter(string pPrinterName,
                                               ref IntPtr phPrinter,
                                               PRINTER_DEFAULTS pDefault);

        [DllImport("winspool.drv", SetLastError = true)]
        internal static extern int ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern bool DeletePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeletePrinterConnection(String pName);

        [DllImport("winspool.drv", EntryPoint = "AddPrinterDriver", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool AddPrinterDriver(String pName,
                                                   int Level,
                                                   ref DRIVER_INFO_6 pDriverInfo);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool EnumPrinterDrivers(String pName, String pEnvironment, uint level, IntPtr pDriverInfo, uint cdBuf, ref uint pcbNeeded, ref uint pcReturned);

        [DllImport("winspool.drv")]
        internal static extern bool GetPrinterDriverDirectory(StringBuilder pName,
                                  StringBuilder pEnv,
                                  int Level,
                                  [Out] StringBuilder outPath,
                                  int bufferSize,
                                  ref int Bytes);

        [DllImport("winspool.drv", EntryPoint = "DeletePrinterDriverEx", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool DeletePrinterDriverEx(String pName,
                                                        String pEnvironment,
                                                        String pDriverName,
                                                        uint dwDeleteFlag,
                                                        uint dwVersionFlag);

        #endregion winspool

        #region Kernel32

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        #endregion Kernel32
    }
}