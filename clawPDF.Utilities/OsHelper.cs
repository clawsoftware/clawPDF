using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace clawSoft.clawPDF.Utilities
{
    public interface IOsHelper
    {
        /// <summary>
        ///     Detect if the current process is running in 64-Bit mode
        /// </summary>
        bool Is64BitProcess { get; }

        /// <summary>
        ///     Detect if the application is run on a 64-Bit Windows edition
        /// </summary>
        bool Is64BitOperatingSystem { get; }

        string WindowsFontsFolder { get; }

        bool UserIsAdministrator();
    }

    public class OsHelper : IOsHelper
    {
        /// <summary>
        ///     Detect if the current process is running in 64-Bit mode
        /// </summary>
        public bool Is64BitProcess => IntPtr.Size == 8;

        /// <summary>
        ///     Detect if the application is run on a 64-Bit Windows edition
        /// </summary>
        public bool Is64BitOperatingSystem => Is64BitProcess || InternalCheckIsWow64();

        public bool UserIsAdministrator()
        {
            try
            {
                //get the currently logged in user
                var user = WindowsIdentity.GetCurrent();
                if (user == null)
                    return false;

                var principal = new WindowsPrincipal(user);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string WindowsFontsFolder
        {
            get
            {
                var windir = Environment.GetEnvironmentVariable("windir") ?? @"C:\Windows";

                return Path.Combine(windir, "Fonts");
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        private bool InternalCheckIsWow64()
        {
            if (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1 ||
                Environment.OSVersion.Version.Major >= 6)
                using (var p = System.Diagnostics.Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal)) return false;
                    return retVal;
                }

            return false;
        }

        public string GetWindowsVersion()
        {
            var windowsVersion = Environment.OSVersion.ToString();

            try
            {
                var myKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                if (myKey != null)
                    windowsVersion = (string)myKey.GetValue("ProductName") + " (" + windowsVersion + ")";
            }
            catch
            {
            }

            return windowsVersion;
        }
    }
}