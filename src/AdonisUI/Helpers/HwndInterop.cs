using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AdonisUI.Helpers
{
    /// <summary>
    /// Helper class for interactions with system window events
    /// </summary>
    public class HwndInterop
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hwndInsertAfter;
            public IntPtr hwnd;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        private const Int32 WM_SYSCOMMAND = 0x112;
        private const Int32 WM_SIZE = 0x0005;
        private const Int32 WM_WINDOWPOSCHANGING = 0x0046;

        private const Int32 SC_MAXIMIZE = 0xF030;
        private const Int32 SC_RESTORE = 0xF120;
        private const Int32 SC_MINIMIZE = 0xF020;

        private readonly IntPtr _handle;

        /// <summary>
        /// Is raised when the <see cref="WM_SIZE"/> is occuring.
        /// </summary>
        public event EventHandler<HwndInteropSizeChangedEventArgs> SizeChanged;

        /// <summary>
        /// Is raised when the <see cref="WM_WINDOWPOSCHANGING"/> is occuring.
        /// </summary>
        public event EventHandler<HwndInteropPositionChangingEventArgs> PositionChanging;

        /// <summary>
        /// Helper class for interactions with system window events
        /// </summary>
        public HwndInterop(Window window)
        {
            _handle = new WindowInteropHelper(window).Handle;

            HwndSource source = HwndSource.FromHwnd(_handle);
            source?.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_SIZE:
                    SizeChanged?.Invoke(this, new HwndInteropSizeChangedEventArgs((HwndInteropSizeChangedEventArgs.ResizeRequestType)wParam));
                    break;
                case WM_WINDOWPOSCHANGING:
                    WINDOWPOS windowPos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
                    PositionChanging?.Invoke(this, new HwndInteropPositionChangingEventArgs((HwndInteropPositionChangingEventArgs.PositionChangeType)windowPos.flags));
                    break;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Sends a system event to maximize the window.
        /// </summary>
        public void Maximize()
        {
            SendMessage(_handle, WM_SYSCOMMAND, (IntPtr)SC_MAXIMIZE, IntPtr.Zero);
        }

        /// <summary>
        /// Sends a system event to restore the window.
        /// </summary>
        public void Restore()
        {
            SendMessage(_handle, WM_SYSCOMMAND, (IntPtr)SC_RESTORE, IntPtr.Zero);
        }

        /// <summary>
        /// Sends a system event to minimize the window.
        /// </summary>
        public void Minimize()
        {
            SendMessage(_handle, WM_SYSCOMMAND, (IntPtr)SC_MINIMIZE, IntPtr.Zero);
        }
    }
}
