using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace clawSoft.clawPDF.WindowsApi
{
    /// <summary>
    ///     FlashWindow class from Chris Pietschmann
    ///     http://pietschsoft.com/post/2009/01/26/CSharp-Flash-Window-in-Taskbar-via-Win32-FlashWindowEx.aspx
    /// </summary>
    internal static class FlashWindow
    {
        /// <summary>
        ///     Stop flashing. The system restores the window to its original stae.
        /// </summary>
        //private const uint FlashwStop = 0;

        /// <summary>
        ///     Flash the window caption.
        /// </summary>
        //private const uint FlashwCaption = 1;

        /// <summary>
        ///     Flash the taskbar button.
        /// </summary>
        //private const uint FlashwTray = 2;

        /// <summary>
        ///     Flash both the window caption and taskbar button.
        ///     This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
        /// </summary>
        private const uint FlashwAll = 3;

        /// <summary>
        ///     Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        //private const uint FlashwTimer = 4;

        /// <summary>
        ///     Flash continuously until the window comes to the foreground.
        /// </summary>
        private const uint FlashwTimerNoForeground = 12;

        /// <summary>
        ///     A boolean value indicating whether the application is running on Windows 2000 or later.
        /// </summary>
        private static bool Win2000OrLater => Environment.OSVersion.Version.Major >= 5;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        private static FLASHWINFO Create_FLASHWINFO(IntPtr handle, uint flags, uint count, uint timeout)
        {
            var fi = new FLASHWINFO();
            fi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fi));
            fi.hwnd = handle;
            fi.dwFlags = flags;
            fi.uCount = count;
            fi.dwTimeout = timeout;
            return fi;
        }

        private static bool Flash(IntPtr hWnd, uint count)
        {
            if (Win2000OrLater)
            {
                var fi = Create_FLASHWINFO(hWnd, FlashwAll | FlashwTimerNoForeground, count, 0);
                return FlashWindowEx(ref fi);
            }

            return false;
        }

        /// <summary>
        ///     Flash the specified Window (form) for the specified number of times or till window has focus
        /// </summary>
        /// <param name="window">The Window (WPF) to Flash.</param>
        /// <param name="count">The number of times to Flash.</param>
        /// <returns></returns>
        public static bool Flash(Window window, uint count)
        {
            var source = (HwndSource)PresentationSource.FromVisual(window);
            if (source == null)
                return false;

            return Flash(source.Handle, count);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            /// <summary>
            ///     The size of the structure in bytes.
            /// </summary>
            public uint cbSize;

            /// <summary>
            ///     A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            ///     The Flash Status.
            /// </summary>
            public uint dwFlags;

            /// <summary>
            ///     The number of times to Flash the window.
            /// </summary>
            public uint uCount;

            /// <summary>
            ///     The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink
            ///     rate.
            /// </summary>
            public uint dwTimeout;
        }
    }
}