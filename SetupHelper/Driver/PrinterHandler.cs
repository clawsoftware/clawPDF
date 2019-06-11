using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using clawSoft.clawPDF.SetupHelper.Helper;
using Microsoft.Win32;

namespace clawSoft.clawPDF.SetupHelper.Driver
{
    public class clawPDFInstaller
    {
        #region Printer Driver Win32 API Constants

        private const uint DRIVER_KERNELMODE = 0x00000001;
        private const uint DRIVER_USERMODE = 0x00000002;

        private const uint APD_STRICT_UPGRADE = 0x00000001;
        private const uint APD_STRICT_DOWNGRADE = 0x00000002;
        private const uint APD_COPY_ALL_FILES = 0x00000004;
        private const uint APD_COPY_NEW_FILES = 0x00000008;
        private const uint APD_COPY_FROM_DIRECTORY = 0x00000010;

        private const uint DPD_DELETE_UNUSED_FILES = 0x00000001;
        private const uint DPD_DELETE_SPECIFIC_VERSION = 0x00000002;
        private const uint DPD_DELETE_ALL_FILES = 0x00000004;

        private const int WIN32_FILE_ALREADY_EXISTS = 183; // Returned by XcvData "AddPort" if the port already exists

        #endregion Printer Driver Win32 API Constants

        private const string ENVIRONMENT = null;
        private const string PRINTERNAME = "clawPDF";
        private const string DRIVERNAME = "clawPDF Virtual Printer";
        private const string HARDWAREID = "clawPDF_Driver";
        private const string PORTMONITOR = "CLAWMON";
        private const string MONITORDLL = "clawmon.dll";
        private const string MONITORUIDLL = "clawmonui.dll";
        private const string PORTNAME = "CLAWMON:";
        private const string PRINTPROCESOR = "winprint";

        private const string DRIVERMANUFACTURER = "Andrew Hess // clawSoft";

        private const string DRIVERFILE = "PSCRIPT5.DLL";
        private const string DRIVERUIFILE = "PS5UI.DLL";
        private const string DRIVERHELPFILE = "PSCRIPT.HLP";
        private const string DRIVERDATAFILE = "SCPDFPRN.PPD";

        private enum DriverFileIndex
        {
            Min = 0,
            DriverFile = Min,
            UIFile,
            HelpFile,
            DataFile,
            Max = DataFile
        };

        private readonly String[] printerDriverFiles = new String[] { DRIVERFILE, DRIVERUIFILE, DRIVERHELPFILE, DRIVERDATAFILE };
        private readonly String[] printerDriverDependentFiles = new String[] { "PSCRIPT.NTF" };

        #region Error messages for Trace/Debug

        private const string FILENOTDELETED_INUSE = "{0} is being used by another process. File was not deleted.";
        private const string FILENOTDELETED_UNAUTHORIZED = "{0} is read-only, or its file permissions do not allow for deletion.";

        private const string FILENOTCOPIED_PRINTERDRIVER = "Printer driver file was not copied. Exception message: {0}";
        private const string FILENOTCOPIED_ALREADYEXISTS = "Destination file {0} was not copied/created - it already exists.";

        private const string WIN32ERROR = "Win32 error code {0}.";

        private const string NATIVE_COULDNOTENABLE64REDIRECTION = "Could not enable 64-bit file system redirection.";
        private const string NATIVE_COULDNOTREVERT64REDIRECTION = "Could not revert 64-bit file system redirection.";

        private const string INSTALL_ROLLBACK_FAILURE_AT_FUNCTION = "Partial uninstallation failure. Function {0} returned false.";

        private const string REGISTRYCONFIG_NOT_ADDED = "Could not add port configuration to registry. Exception message: {0}";
        private const string REGISTRYCONFIG_NOT_DELETED = "Could not delete port configuration from registry. Exception message: {0}";

        private const String INFO_INSTALLPORTMONITOR_FAILED = "Port monitor installation failed.";
        private const String INFO_INSTALLCOPYDRIVER_FAILED = "Could not copy printer driver files.";
        private const String INFO_INSTALLPORT_FAILED = "Could not add redirected port.";
        private const String INFO_INSTALLPRINTERDRIVER_FAILED = "Printer driver installation failed.";
        private const String INFO_INSTALLPRINTER_FAILED = "Could not add printer.";
        private const String INFO_INSTALLCONFIGPORT_FAILED = "Port configuration failed.";

        #endregion Error messages for Trace/Debug

        #region Constructors

        public clawPDFInstaller()
        {
        }

        #endregion Constructors

        #region Port operations

#if DEBUG
        public bool AddclawPDFPort_Test()
        {
            return AddclawPDFPort();
        }
#endif

        private bool AddclawPDFPort()
        {
            bool portAdded = false;

            int portAddResult = DoXcvDataPortOperation(PORTNAME, PORTMONITOR, "AddPort");
            switch (portAddResult)
            {
                case 0:
                case WIN32_FILE_ALREADY_EXISTS: // Port already exists - this is OK, we'll just keep using it
                    portAdded = true;
                    break;
            }
            return portAdded;
        }

        public bool DeleteclawPDFPort()
        {
            bool portDeleted = false;

            int portDeleteResult = DoXcvDataPortOperation(PORTNAME, PORTMONITOR, "DeletePort");
            switch (portDeleteResult)
            {
                case 0:
                    portDeleted = true;
                    break;
            }
            return portDeleted;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="xcvDataOperation"></param>
        /// <returns></returns>
        /// <remarks>I can't remember the name/link of the developer who wrote this code originally,
        /// so I can't provide a link or credit.</remarks>
        private int DoXcvDataPortOperation(string portName, string portMonitor, string xcvDataOperation)
        {
            int win32ErrorCode;

            PRINTER_DEFAULTS def = new PRINTER_DEFAULTS();

            def.pDatatype = null;
            def.pDevMode = IntPtr.Zero;
            def.DesiredAccess = 1; //Server Access Administer

            IntPtr hPrinter = IntPtr.Zero;

            if (NativeMethods.OpenPrinter(",XcvMonitor " + portMonitor, ref hPrinter, def) != 0)
            {
                if (!portName.EndsWith("\0"))
                    portName += "\0"; // Must be a null terminated string

                // Must get the size in bytes. Rememeber .NET strings are formed by 2-byte characters
                uint size = (uint)(portName.Length * 2);

                // Alloc memory in HGlobal to set the portName
                IntPtr portPtr = Marshal.AllocHGlobal((int)size);
                Marshal.Copy(portName.ToCharArray(), 0, portPtr, portName.Length);

                uint needed; // Not that needed in fact...
                uint xcvResult; // Will receive de result here

                NativeMethods.XcvData(hPrinter, xcvDataOperation, portPtr, size, IntPtr.Zero, 0, out needed, out xcvResult);

                NativeMethods.ClosePrinter(hPrinter);
                Marshal.FreeHGlobal(portPtr);
                win32ErrorCode = (int)xcvResult;
            }
            else
            {
                win32ErrorCode = Marshal.GetLastWin32Error();
            }
            return win32ErrorCode;
        }

        #endregion Port operations

        #region Port Monitor

        /// <summary>
        /// Adds the clawPDF port monitor
        /// </summary>
        /// <param name="monitorFilePath">Directory where the uninstalled monitor dll is located</param>
        /// <returns>true if the monitor is installed, false if install failed</returns>
        public bool AddclawPDFPortMonitor(String monitorFilePath)
        {
            bool monitorAdded = false;

            IntPtr oldRedirectValue = IntPtr.Zero;

            try
            {
                oldRedirectValue = DisableWow64Redirection();
                //if (!DoesMonitorExist(PORTMONITOR))
                //{
                // Copy the monitor DLL to
                // the system directory
                String monitorfileSourcePath = Path.Combine(monitorFilePath, MONITORDLL);
                String monitorfileDestinationPath = Path.Combine(Environment.SystemDirectory, MONITORDLL);
                String monitoruifileSourcePath = Path.Combine(monitorFilePath, MONITORUIDLL);
                String monitoruifileDestinationPath = Path.Combine(Environment.SystemDirectory, MONITORUIDLL);

                Spooler.stop();

                try
                {
                    File.Copy(monitoruifileSourcePath, monitoruifileDestinationPath, true);
                }
                catch { }

                try
                {
                    File.Copy(monitorfileSourcePath, monitorfileDestinationPath, true);
                }
                catch { }

                Spooler.start();

                MONITOR_INFO_2 newMonitor = new MONITOR_INFO_2();
                newMonitor.pName = PORTMONITOR;
                newMonitor.pEnvironment = ENVIRONMENT;
                newMonitor.pDLLName = MONITORDLL;
                if (!AddPortMonitor(newMonitor))
                    Console.WriteLine(String.Format("Could not add port monitor {0}", PORTMONITOR) + Environment.NewLine +
                                              String.Format(WIN32ERROR, Marshal.GetLastWin32Error().ToString()));
                else
                    monitorAdded = true;
                //}
                //else
                //{
                // Monitor already installed -
                // log it, and keep going
                //    Console.WriteLine(String.Format("Port monitor {0} already installed.", PORTMONITOR));
                //    monitorAdded = true;
                // }
            }
            finally
            {
                if (oldRedirectValue != IntPtr.Zero) RevertWow64Redirection(oldRedirectValue);
            }

            return monitorAdded;
        }

        /// <summary>
        /// Disables WOW64 system directory file redirection
        /// if the current process is both
        /// 32-bit, and running on a 64-bit OS -
        /// Compiling for 64-bit OS, and setting the install dir to "ProgramFiles64"
        /// should ensure this code never runs in production
        /// </summary>
        /// <returns>A Handle, which should be retained to reenable redirection</returns>
        private IntPtr DisableWow64Redirection()
        {
            IntPtr oldValue = IntPtr.Zero;
            if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess)
                if (!NativeMethods.Wow64DisableWow64FsRedirection(ref oldValue))
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not disable Wow64 file system redirection.");
            return oldValue;
        }

        /// <summary>
        /// Reenables WOW64 system directory file redirection
        /// if the current process is both
        /// 32-bit, and running on a 64-bit OS -
        /// Compiling for 64-bit OS, and setting the install dir to "ProgramFiles64"
        /// should ensure this code never runs in production
        /// </summary>
        /// <param name="oldValue">A Handle value - should be retained from call to <see cref="DisableWow64Redirection"/></param>
        private void RevertWow64Redirection(IntPtr oldValue)
        {
            if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess)
            {
                if (!NativeMethods.Wow64RevertWow64FsRedirection(oldValue))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not reenable Wow64 file system redirection.");
                }
            }
        }

        /// <summary>
        /// Removes the clawPDF port monitor
        /// </summary>
        /// <returns>true if monitor successfully removed, false if removal failed</returns>
        public bool RemoveclawPDFPortMonitor()
        {
            bool monitorRemoved = false;
            if ((NativeMethods.DeleteMonitor(null, ENVIRONMENT, PORTMONITOR)) != 0)
            {
                monitorRemoved = true;
                // Try to remove the monitor DLL now
                if (!DeleteclawPDFPortMonitorDll())
                {
                    Console.WriteLine("Could not remove port monitor dll.");
                }
            }
            return monitorRemoved;
        }

        private bool DeleteclawPDFPortMonitorDll()
        {
            return DeletePortMonitorDll(MONITORDLL, MONITORUIDLL);
        }

        private bool DeletePortMonitorDll(String monitorDll, string monitoruiDLL)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "net.exe";
            processStartInfo.Arguments = "stop spooler";
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process.Start(processStartInfo).WaitForExit(1000 * 60);

            bool monitorDllRemoved = false;

            String monitorDllFullPathname = String.Empty;
            IntPtr oldRedirectValue = IntPtr.Zero;
            try
            {
                oldRedirectValue = DisableWow64Redirection();

                monitorDllFullPathname = Path.Combine(Environment.SystemDirectory, monitorDll);

                File.Delete(monitorDllFullPathname);
                monitorDllRemoved = true;
                File.Delete(Path.Combine(Environment.SystemDirectory, monitoruiDLL));
            }
            catch (Win32Exception windows32Ex)
            {
                // This one is likely very bad -
                // log and rethrow so we don't continue
                // to try to uninstall
                Console.WriteLine(NATIVE_COULDNOTENABLE64REDIRECTION + String.Format(WIN32ERROR, windows32Ex.NativeErrorCode.ToString()));
                throw;
            }
            catch (IOException)
            {
                // File still in use
                Console.WriteLine(String.Format(FILENOTDELETED_INUSE, monitorDllFullPathname));
            }
            catch (UnauthorizedAccessException)
            {
                // File is readonly, or file permissions do not allow delete
                Console.WriteLine(String.Format(FILENOTDELETED_INUSE, monitorDllFullPathname));
            }
            finally
            {
                try
                {
                    if (oldRedirectValue != IntPtr.Zero) RevertWow64Redirection(oldRedirectValue);
                }
                catch (Win32Exception windows32Ex)
                {
                    // Couldn't turn file redirection back on -
                    // this is not good
                    Console.WriteLine(NATIVE_COULDNOTREVERT64REDIRECTION + String.Format(WIN32ERROR, windows32Ex.NativeErrorCode.ToString()));
                    throw;
                }
            }

            processStartInfo.Arguments = "start spooler";
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            Process.Start(processStartInfo).WaitForExit(1000 * 60);

            return monitorDllRemoved;
        }

        private bool AddPortMonitor(MONITOR_INFO_2 newMonitor)
        {
            bool monitorAdded = false;
            if ((NativeMethods.AddMonitor(null, 2, ref newMonitor) != 0))
            {
                monitorAdded = true;
            }
            return monitorAdded;
        }

        private bool DeletePortMonitor(String monitorName)
        {
            bool monitorDeleted = false;
            if ((NativeMethods.DeleteMonitor(null, ENVIRONMENT, monitorName)) != 0)
            {
                monitorDeleted = true;
            }
            return monitorDeleted;
        }

        private bool DoesMonitorExist(String monitorName)
        {
            bool monitorExists = false;
            List<MONITOR_INFO_2> portMonitors = EnumerateMonitors();
            foreach (MONITOR_INFO_2 portMonitor in portMonitors)
            {
                if (portMonitor.pName == monitorName)
                {
                    monitorExists = true;
                    break;
                }
            }
            return monitorExists;
        }

        public List<MONITOR_INFO_2> EnumerateMonitors()
        {
            List<MONITOR_INFO_2> portMonitors = new List<MONITOR_INFO_2>();

            uint pcbNeeded = 0;
            uint pcReturned = 0;

            if (!NativeMethods.EnumMonitors(null, 2, IntPtr.Zero, 0, ref pcbNeeded, ref pcReturned))
            {
                IntPtr pMonitors = Marshal.AllocHGlobal((int)pcbNeeded);
                if (NativeMethods.EnumMonitors(null, 2, pMonitors, pcbNeeded, ref pcbNeeded, ref pcReturned))
                {
                    IntPtr currentMonitor = pMonitors;

                    for (int i = 0; i < pcReturned; i++)
                    {
                        portMonitors.Add((MONITOR_INFO_2)Marshal.PtrToStructure(currentMonitor, typeof(MONITOR_INFO_2)));
                        currentMonitor = IntPtr.Add(currentMonitor, Marshal.SizeOf(typeof(MONITOR_INFO_2)));
                    }
                    Marshal.FreeHGlobal(pMonitors);
                }
                else
                {
                    // Failed to retrieve enumerated monitors
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not enumerate port monitors.");
                }
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Call to EnumMonitors in winspool.drv succeeded with a zero size buffer - unexpected error.");
            }

            return portMonitors;
        }

        #endregion Port Monitor

        #region Printer Install

        public String RetrievePrinterDriverDirectory()
        {
            StringBuilder driverDirectory = new StringBuilder(1024);
            int dirSizeInBytes = 0;
            if (!NativeMethods.GetPrinterDriverDirectory(null,
                                                         null,
                                                         1,
                                                         driverDirectory,
                                                         1024,
                                                         ref dirSizeInBytes))
                throw new DirectoryNotFoundException("Could not retrieve printer driver directory.");
            return driverDirectory.ToString();
        }

        private delegate bool undoInstall();

        /// <summary>
        /// Installs the port monitor, port,
        /// printer drivers, and clawPDF virtual printer
        /// </summary>
        /// <param name="driverSourceDirectory">Directory where the uninstalled printer driver files are located</param>
        /// <param name="driverFilesToCopy">An array containing the printer driver's filenames</param>
        /// <param name="dependentFilesToCopy">An array containing dependent filenames</param>
        /// <returns>true if installation suceeds, false if failed</returns>
        public bool InstallclawPDFPrinter(String driverSourceDirectory,
                                            String outputHandlerCommand)
        {
            bool printerInstalled = false;

            Stack<undoInstall> undoInstallActions = new Stack<undoInstall>();

            String driverDirectory = RetrievePrinterDriverDirectory();
            undoInstallActions.Push(this.DeleteclawPDFPortMonitorDll);
            ConfigureclawPDFPort(outputHandlerCommand);
            if (AddclawPDFPortMonitor(driverSourceDirectory))
            {
                Console.WriteLine("Port monitor successfully installed.");
                undoInstallActions.Push(this.RemoveclawPDFPortMonitor);
                if (CopyPrinterDriverFiles(driverSourceDirectory, printerDriverFiles.Concat(printerDriverDependentFiles).ToArray()))
                {
                    Console.WriteLine("Printer drivers copied or already exist.");
                    undoInstallActions.Push(this.RemoveclawPDFPortMonitor);
                    if (AddclawPDFPort())
                    {
                        Console.WriteLine("Redirection port added.");
                        undoInstallActions.Push(this.RemoveclawPDFPrinterDriver);
                        if (InstallclawPDFPrinterDriver())
                        {
                            Console.WriteLine("Printer driver installed.");
                            undoInstallActions.Push(this.DeleteclawPDFPrinter);
                            if (AddclawPDFPrinter())
                            {
                                Console.WriteLine("Virtual printer installed.");
                                undoInstallActions.Push(this.RemoveclawPDFPortConfig);
                                if (ConfigureclawPDFPort(outputHandlerCommand))
                                {
                                    Console.WriteLine("Printer configured.");
                                    printerInstalled = true;
                                }
                                else
                                    // Failed to configure port
                                    Console.WriteLine(INFO_INSTALLCONFIGPORT_FAILED);
                            }
                            else
                                // Failed to install printer
                                Console.WriteLine(INFO_INSTALLPRINTER_FAILED);
                        }
                        else
                            // Failed to install printer driver
                            Console.WriteLine(INFO_INSTALLPRINTERDRIVER_FAILED);
                    }
                    else
                        // Failed to add printer port
                        Console.WriteLine(INFO_INSTALLPORT_FAILED);
                }
                else
                    //Failed to copy printer driver files
                    Console.WriteLine(INFO_INSTALLCOPYDRIVER_FAILED);
            }
            else
                //Failed to add port monitor
                Console.WriteLine(INFO_INSTALLPORTMONITOR_FAILED);

            //if (printerInstalled == false)
            //{
            //    // Printer installation failed -
            //    // undo all the install steps
            //    while (undoInstallActions.Count > 0)
            //    {
            //        undoInstall undoAction = undoInstallActions.Pop();
            //        try
            //        {
            //            if (!undoAction())
            //            {
            //                Console.WriteLine(String.Format(INSTALL_ROLLBACK_FAILURE_AT_FUNCTION, undoAction.Method.Name));
            //            }
            //        }
            //        catch (Win32Exception win32Ex)
            //        {
            //            Console.WriteLine(String.Format(INSTALL_ROLLBACK_FAILURE_AT_FUNCTION, undoAction.Method.Name) +
            //                                            String.Format(WIN32ERROR, win32Ex.ErrorCode.ToString()));
            //        }
            //    }
            //}
            return printerInstalled;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool UninstallclawPDFPrinter()
        {
            bool printerUninstalledCleanly = true;

            if (!DeleteclawPDFPrinter())
                printerUninstalledCleanly = false;
            if (!RemoveclawPDFPrinterDriver())
                printerUninstalledCleanly = false;
            if (!DeleteclawPDFPort())
                printerUninstalledCleanly = false;
            if (!RemoveclawPDFPortMonitor())
                printerUninstalledCleanly = false;
            if (!RemoveclawPDFPortConfig())
                printerUninstalledCleanly = false;
            DeleteclawPDFPortMonitorDll();
            return printerUninstalledCleanly;
        }

        private bool CopyPrinterDriverFiles(String driverSourceDirectory,
                                            String[] filesToCopy)
        {
            bool filesCopied = false;
            String driverDestinationDirectory = RetrievePrinterDriverDirectory();
            try
            {
                for (int loop = 0; loop < filesToCopy.Length; loop++)
                {
                    String fileSourcePath = Path.Combine(driverSourceDirectory, filesToCopy[loop]);
                    String fileDestinationPath = Path.Combine(driverDestinationDirectory, filesToCopy[loop]);
                    try
                    {
                        File.Copy(fileSourcePath, fileDestinationPath, true);
                    }
                    catch (PathTooLongException)
                    {
                        // Will be caught by outer
                        // IOException catch block
                        throw;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        // Will be caught by outer
                        // IOException catch block
                        throw;
                    }
                    catch (FileNotFoundException)
                    {
                        // Will be caught by outer
                        // IOException catch block
                        throw;
                    }
                    catch (IOException)
                    {
                        // Just keep going - file was already there
                        // Not really a problem
                        Console.WriteLine(String.Format(FILENOTCOPIED_ALREADYEXISTS, fileDestinationPath));
                        continue;
                    }
                }
                filesCopied = true;
            }
            catch (IOException ioEx)
            {
                Console.WriteLine(String.Format(FILENOTCOPIED_PRINTERDRIVER, ioEx.Message));
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                Console.WriteLine(String.Format(FILENOTCOPIED_PRINTERDRIVER, unauthorizedEx.Message));
            }
            catch (NotSupportedException notSupportedEx)
            {
                Console.WriteLine(String.Format(FILENOTCOPIED_PRINTERDRIVER, notSupportedEx.Message));
            }

            return filesCopied;
        }

        private bool DeletePrinterDriverFiles(String driverSourceDirectory,
                                              String[] filesToDelete)
        {
            bool allFilesDeleted = true;
            for (int loop = 0; loop < filesToDelete.Length; loop++)
            {
                try
                {
                    File.Delete(Path.Combine(driverSourceDirectory, filesToDelete[loop]));
                }
                catch
                {
                    allFilesDeleted = false;
                }
            }
            return allFilesDeleted;
        }

#if DEBUG
        public bool IsPrinterDriverInstalled_Test(String driverName)
        {
            return IsPrinterDriverInstalled(driverName);
        }
#endif

        private bool IsPrinterDriverInstalled(String driverName)
        {
            bool driverInstalled = false;
            List<DRIVER_INFO_6> installedDrivers = EnumeratePrinterDrivers();
            foreach (DRIVER_INFO_6 printerDriver in installedDrivers)
            {
                if (printerDriver.pName == driverName)
                {
                    driverInstalled = true;
                    break;
                }
            }
            return driverInstalled;
        }

        public List<DRIVER_INFO_6> EnumeratePrinterDrivers()
        {
            List<DRIVER_INFO_6> installedPrinterDrivers = new List<DRIVER_INFO_6>();

            uint pcbNeeded = 0;
            uint pcReturned = 0;

            if (!NativeMethods.EnumPrinterDrivers(null, ENVIRONMENT, 6, IntPtr.Zero, 0, ref pcbNeeded, ref pcReturned))
            {
                IntPtr pDrivers = Marshal.AllocHGlobal((int)pcbNeeded);
                if (NativeMethods.EnumPrinterDrivers(null, ENVIRONMENT, 6, pDrivers, pcbNeeded, ref pcbNeeded, ref pcReturned))
                {
                    IntPtr currentDriver = pDrivers;
                    for (int loop = 0; loop < pcReturned; loop++)
                    {
                        installedPrinterDrivers.Add((DRIVER_INFO_6)Marshal.PtrToStructure(currentDriver, typeof(DRIVER_INFO_6)));
                        //currentDriver = (IntPtr)(currentDriver.ToInt32() + Marshal.SizeOf(typeof(DRIVER_INFO_6)));
                        currentDriver = IntPtr.Add(currentDriver, Marshal.SizeOf(typeof(DRIVER_INFO_6)));
                    }
                    Marshal.FreeHGlobal(pDrivers);
                }
                else
                {
                    // Failed to enumerate printer drivers
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Could not enumerate printer drivers.");
                }
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Call to EnumPrinterDrivers in winspool.drv succeeded with a zero size buffer - unexpected error.");
            }

            return installedPrinterDrivers;
        }

        private bool InstallclawPDFPrinterDriver()
        {
            bool clawPDFPrinterDriverInstalled = false;

            if (!IsPrinterDriverInstalled(DRIVERNAME))
            {
                String driverSourceDirectory = RetrievePrinterDriverDirectory();

                StringBuilder nullTerminatedDependentFiles = new StringBuilder();
                if (printerDriverDependentFiles.Length > 0)
                {
                    for (int loop = 0; loop <= printerDriverDependentFiles.GetUpperBound(0); loop++)
                    {
                        nullTerminatedDependentFiles.Append(printerDriverDependentFiles[loop]);
                        nullTerminatedDependentFiles.Append("\0");
                    }
                    nullTerminatedDependentFiles.Append("\0");
                }
                else
                {
                    nullTerminatedDependentFiles.Append("\0\0");
                }

                DRIVER_INFO_6 printerDriverInfo = new DRIVER_INFO_6();

                printerDriverInfo.cVersion = 3;
                printerDriverInfo.pName = DRIVERNAME;
                printerDriverInfo.pEnvironment = ENVIRONMENT;
                printerDriverInfo.pDriverPath = Path.Combine(driverSourceDirectory, DRIVERFILE);
                printerDriverInfo.pConfigFile = Path.Combine(driverSourceDirectory, DRIVERUIFILE);
                printerDriverInfo.pHelpFile = Path.Combine(driverSourceDirectory, DRIVERHELPFILE);
                printerDriverInfo.pDataFile = Path.Combine(driverSourceDirectory, DRIVERDATAFILE);
                printerDriverInfo.pDependentFiles = nullTerminatedDependentFiles.ToString();

                printerDriverInfo.pMonitorName = PORTMONITOR;
                printerDriverInfo.pDefaultDataType = String.Empty;
                printerDriverInfo.dwlDriverVersion = 0x0001000000000000U;
                printerDriverInfo.pszMfgName = DRIVERMANUFACTURER;
                printerDriverInfo.pszHardwareID = HARDWAREID;
                printerDriverInfo.pszProvider = DRIVERMANUFACTURER;

                clawPDFPrinterDriverInstalled = InstallPrinterDriver(ref printerDriverInfo);
            }
            else
            {
                clawPDFPrinterDriverInstalled = true; // Driver is already installed, we'll just use the installed driver
            }

            return clawPDFPrinterDriverInstalled;
        }

        private bool InstallPrinterDriver(ref DRIVER_INFO_6 printerDriverInfo)
        {
            bool printerDriverInstalled = false;

            printerDriverInstalled = NativeMethods.AddPrinterDriver(null, 6, ref printerDriverInfo);
            if (printerDriverInstalled == false)
            {
                Console.WriteLine("Could not add clawPDF printer driver. " +
                                          String.Format(WIN32ERROR, Marshal.GetLastWin32Error().ToString()));
            }
            return printerDriverInstalled;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool RemoveclawPDFPrinterDriver()
        {
            bool driverRemoved = NativeMethods.DeletePrinterDriverEx(null, ENVIRONMENT, DRIVERNAME, DPD_DELETE_UNUSED_FILES, 3);
            if (!driverRemoved)
            {
                Console.WriteLine("Could not remove clawPDF printer driver. " +
                                          String.Format(WIN32ERROR, Marshal.GetLastWin32Error().ToString()));
            }
            return driverRemoved;
        }

        private bool AddclawPDFPrinter()
        {
            bool printerAdded = false;
            PRINTER_INFO_2 clawPDFPrinter = new PRINTER_INFO_2();

            clawPDFPrinter.pServerName = null;
            clawPDFPrinter.pPrinterName = PRINTERNAME;
            clawPDFPrinter.pPortName = PORTNAME;
            clawPDFPrinter.pDriverName = DRIVERNAME;
            clawPDFPrinter.pPrintProcessor = PRINTPROCESOR;
            clawPDFPrinter.pDatatype = "RAW";
            clawPDFPrinter.Attributes = 0x00000002;

            int clawPDFPrinterHandle = NativeMethods.AddPrinter(null, 2, ref clawPDFPrinter);
            if (clawPDFPrinterHandle != 0)
            {
                // Added ok
                int closeCode = NativeMethods.ClosePrinter((IntPtr)clawPDFPrinterHandle);
                printerAdded = true;
            }
            else
            {
                Console.WriteLine("Could not add clawPDF virtual printer. " +
                                          String.Format(WIN32ERROR, Marshal.GetLastWin32Error().ToString()));
            }
            return printerAdded;
        }

        public bool AddCustomclawPDFPrinter(string name)
        {
            bool printerAdded = false;
            PRINTER_INFO_2 clawPDFPrinter = new PRINTER_INFO_2();

            clawPDFPrinter.pServerName = null;
            clawPDFPrinter.pPrinterName = name;
            clawPDFPrinter.pPortName = PORTNAME;
            clawPDFPrinter.pDriverName = DRIVERNAME;
            clawPDFPrinter.pPrintProcessor = PRINTPROCESOR;
            clawPDFPrinter.pDatatype = "RAW";
            clawPDFPrinter.Attributes = 0x00000002;

            int clawPDFPrinterHandle = NativeMethods.AddPrinter(null, 2, ref clawPDFPrinter);
            if (clawPDFPrinterHandle != 0)
            {
                // Added ok
                int closeCode = NativeMethods.ClosePrinter((IntPtr)clawPDFPrinterHandle);
                printerAdded = true;
            }
            else
            {
                Console.WriteLine("Could not add clawPDF virtual printer. " +
                                  String.Format(WIN32ERROR, Marshal.GetLastWin32Error().ToString()));
            }
            return printerAdded;
        }

        private bool DeleteclawPDFPrinter()
        {
            bool printerDeleted = false;

            PRINTER_DEFAULTS clawPDFDefaults = new PRINTER_DEFAULTS();
            clawPDFDefaults.DesiredAccess = 0x000F000C; // All access
            clawPDFDefaults.pDatatype = null;
            clawPDFDefaults.pDevMode = IntPtr.Zero;

            IntPtr clawPDFHandle = IntPtr.Zero;
            try
            {
                if (NativeMethods.OpenPrinter(PRINTERNAME, ref clawPDFHandle, clawPDFDefaults) != 0)
                {
                    if (NativeMethods.DeletePrinter(clawPDFHandle))
                        printerDeleted = true;
                }
                else
                {
                    Console.WriteLine("Could not delete clawPDF virtual printer. " +
                                              String.Format(WIN32ERROR, Marshal.GetLastWin32Error().ToString()));
                }
            }
            finally
            {
                if (clawPDFHandle != IntPtr.Zero) NativeMethods.ClosePrinter(clawPDFHandle);
            }
            return printerDeleted;
        }

        public bool DeleteCustomclawPDFPrinter(string name)
        {
            bool printerDeleted = false;

            PRINTER_DEFAULTS clawPDFDefaults = new PRINTER_DEFAULTS();
            clawPDFDefaults.DesiredAccess = 0x000F000C; // All access
            clawPDFDefaults.pDatatype = null;
            clawPDFDefaults.pDevMode = IntPtr.Zero;

            IntPtr clawPDFHandle = IntPtr.Zero;
            try
            {
                if (NativeMethods.OpenPrinter(name, ref clawPDFHandle, clawPDFDefaults) != 0)
                {
                    if (NativeMethods.DeletePrinter(clawPDFHandle))
                        printerDeleted = true;
                }
                else
                {
                    Console.WriteLine("Could not delete clawPDF virtual printer. " +
                                      String.Format(WIN32ERROR, Marshal.GetLastWin32Error().ToString()));
                }
            }
            finally
            {
                if (clawPDFHandle != IntPtr.Zero) NativeMethods.ClosePrinter(clawPDFHandle);
            }
            return printerDeleted;
        }

        public bool IsclawPDFPrinterInstalled()
        {
            bool clawPDFInstalled = false;

            PRINTER_DEFAULTS clawPDFDefaults = new PRINTER_DEFAULTS();
            clawPDFDefaults.DesiredAccess = 0x00008; // Use access
            clawPDFDefaults.pDatatype = null;
            clawPDFDefaults.pDevMode = IntPtr.Zero;

            IntPtr clawPDFHandle = IntPtr.Zero;
            if (NativeMethods.OpenPrinter(PRINTERNAME, ref clawPDFHandle, clawPDFDefaults) != 0)
            {
                clawPDFInstalled = true;
            }
            else
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (errorCode == 0x5) clawPDFInstalled = true; // Printer is installed, but user
                                                               // has no privileges to use it
            }

            return clawPDFInstalled;
        }

        #endregion Printer Install

        #region Configuration and Registry changes

#if DEBUG
        public bool ConfigureclawPDFPort_Test()
        {
            return ConfigureclawPDFPort();
        }
#endif

        private bool ConfigureclawPDFPort()
        {
            return ConfigureclawPDFPort(String.Empty);
        }

        private bool ConfigureclawPDFPort(String commandValue)
        {
            bool registryChangesMade = false;
            // Add all the registry info
            // for the port and monitor
            RegistryKey portConfiguration;
            try
            {
                portConfiguration = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\" + PORTMONITOR);
                portConfiguration.SetValue("LogLevel", 0, RegistryValueKind.DWord);
                portConfiguration = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\" + PORTMONITOR + "\\" + PORTMONITOR + ":");
                portConfiguration.SetValue("Domain", ".", RegistryValueKind.String);
                portConfiguration.SetValue("ExecPath", Path.GetDirectoryName(Application.ExecutablePath), RegistryValueKind.String);
                portConfiguration.SetValue("FilePattern", "", RegistryValueKind.String);
                portConfiguration.SetValue("HideProcess", 0, RegistryValueKind.DWord);
                portConfiguration.SetValue("OutputPath", "", RegistryValueKind.String);
                portConfiguration.SetValue("Overwrite", 1, RegistryValueKind.DWord);
                portConfiguration.SetValue("Password", new byte[] { 0000, 00, 00, 00, 00, 00 }, RegistryValueKind.Binary);
                portConfiguration.SetValue("PipeData", 0, RegistryValueKind.DWord);
                portConfiguration.SetValue("RunAsPUser", 1, RegistryValueKind.DWord);
                portConfiguration.SetValue("User", "", RegistryValueKind.String);
                portConfiguration.SetValue("WaitTermination", 0, RegistryValueKind.DWord);
                portConfiguration.SetValue("WaitTimeout", 0, RegistryValueKind.DWord);
                portConfiguration.SetValue("Description", "clawPDF", RegistryValueKind.String);
                portConfiguration.SetValue("UserCommand", Path.GetDirectoryName(Application.ExecutablePath) + @"\clawPDF.Bridge.exe", RegistryValueKind.String);
                portConfiguration.SetValue("Printer", PRINTERNAME, RegistryValueKind.String);
                registryChangesMade = true;
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                Console.WriteLine(String.Format(REGISTRYCONFIG_NOT_ADDED, unauthorizedEx.Message));
            }
            catch (SecurityException securityEx)
            {
                Console.WriteLine(String.Format(REGISTRYCONFIG_NOT_ADDED, securityEx.Message));
            }

            return registryChangesMade;
        }

        private bool RemoveclawPDFPortConfig()
        {
            bool registryEntriesRemoved = false;

            try
            {
                Registry.LocalMachine.DeleteSubKey("SYSTEM\\CurrentControlSet\\Control\\Print\\Monitors\\" +
                                                    PORTMONITOR + "\\Ports\\" + PORTNAME, false);
                registryEntriesRemoved = true;
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                Console.WriteLine(String.Format(REGISTRYCONFIG_NOT_DELETED, unauthorizedEx.Message));
            }

            return registryEntriesRemoved;
        }

        #endregion Configuration and Registry changes
    }
}