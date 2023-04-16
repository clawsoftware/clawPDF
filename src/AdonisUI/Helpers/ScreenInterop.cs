using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdonisUI.Helpers
{
    /// <summary>
    /// Represents a display device or multiple display devices on a single system.
    /// See https://github.com/micdenny/WpfScreenHelper/
    /// </summary>
    internal class ScreenInterop
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool EnumDisplayMonitors(HandleRef hdc, COMRECT rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref RECT rc, int nUpdate);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr MonitorFromPoint(POINTSTRUCT pt, int flags);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos([In, Out] POINT pt);

        public static HandleRef NullHandleRef;

        public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rect r)
            {
                this.left = (int)r.Left;
                this.top = (int)r.Top;
                this.right = (int)r.Right;
                this.bottom = (int)r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public Size Size
            {
                get { return new Size(this.right - this.left, this.bottom - this.top); }
            }
        }

        // use this in cases where the Native API takes a POINT not a POINT*
        // classes marshal by ref.
        [StructLayout(LayoutKind.Sequential)]
        public struct POINTSTRUCT
        {
            public int x;
            public int y;
            public POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;

            public POINT()
            {
            }

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

#if DEBUG
            public override string ToString()
            {
                return "{x=" + x + ", y=" + y + "}";
            }
#endif
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public class MONITORINFOEX
        {
            internal int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
            internal RECT rcMonitor = new RECT();
            internal RECT rcWork = new RECT();
            internal int dwFlags = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)] internal char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public COMRECT()
            {
            }

            public COMRECT(Rect r)
            {
                this.left = (int)r.X;
                this.top = (int)r.Y;
                this.right = (int)r.Right;
                this.bottom = (int)r.Bottom;
            }

            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return "Left = " + left + " Top " + top + " Right = " + right + " Bottom = " + bottom;
            }
        }

        public const int SM_CMONITORS = 80;

        private readonly IntPtr hmonitor;

        // This identifier is just for us, so that we don't try to call the multimon
        // functions if we just need the primary monitor... this is safer for
        // non-multimon OSes.
        private const int PRIMARY_MONITOR = unchecked((int)0xBAADF00D);

        private const int MONITORINFOF_PRIMARY = 0x00000001;

        private const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        private static readonly bool MultiMonitorSupport;

        static ScreenInterop()
        {
            MultiMonitorSupport = GetSystemMetrics(SM_CMONITORS) != 0;
        }

        private ScreenInterop(IntPtr monitor)
            : this(monitor, IntPtr.Zero)
        {
        }

        private ScreenInterop(IntPtr monitor, IntPtr hdc)
        {
            if (!MultiMonitorSupport || monitor == (IntPtr)PRIMARY_MONITOR)
            {
                this.Bounds = new Rect(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop, SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight);
                this.Primary = true;
                this.DeviceName = "DISPLAY";
            }
            else
            {
                var info = new MONITORINFOEX();

                GetMonitorInfo(new HandleRef(null, monitor), info);

                this.Bounds = new Rect(
                    info.rcMonitor.left, info.rcMonitor.top,
                    info.rcMonitor.right - info.rcMonitor.left,
                    info.rcMonitor.bottom - info.rcMonitor.top);

                this.Primary = ((info.dwFlags & MONITORINFOF_PRIMARY) != 0);

                this.DeviceName = new string(info.szDevice).TrimEnd((char)0);
            }
            hmonitor = monitor;
        }

        /// <summary>
        /// Gets an array of all displays on the system.
        /// </summary>
        /// <returns>An enumerable of type Screen, containing all displays on the system.</returns>
        public static IEnumerable<ScreenInterop> AllScreens
        {
            get
            {
                if (MultiMonitorSupport)
                {
                    var closure = new MonitorEnumCallback();
                    var proc = new MonitorEnumProc(closure.Callback);

                    EnumDisplayMonitors(NullHandleRef, null, proc, IntPtr.Zero);

                    if (closure.Screens.Count > 0)
                        return closure.Screens.Cast<ScreenInterop>();
                }

                return new[] { new ScreenInterop((IntPtr)PRIMARY_MONITOR) };
            }
        }

        /// <summary>
        /// Gets the bounds of the display.
        /// </summary>
        /// <returns>A <see cref="T:System.Windows.Rect" />, representing the bounds of the display.</returns>
        public Rect Bounds { get; private set; }

        /// <summary>
        /// Gets the device name associated with a display.
        /// </summary>
        /// <returns>The device name associated with a display.</returns>
        public string DeviceName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a particular display is the primary device.
        /// </summary>
        /// <returns>true if this display is primary; otherwise, false.</returns>
        public bool Primary { get; private set; }

        /// <summary>
        /// Gets the primary display.
        /// </summary>
        /// <returns>The primary display.</returns>
        public static ScreenInterop PrimaryScreen
        {
            get
            {
                if (MultiMonitorSupport)
                {
                    return AllScreens.FirstOrDefault(t => t.Primary);
                }
                return new ScreenInterop((IntPtr)PRIMARY_MONITOR);
            }
        }

        /// <summary>
        /// Gets the working area of the display. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.
        /// </summary>
        /// <returns>A <see cref="T:System.Windows.Rect" />, representing the working area of the display.</returns>
        public Rect WorkingArea
        {
            get
            {
                if (!MultiMonitorSupport || hmonitor == (IntPtr)PRIMARY_MONITOR)
                {
                    return SystemParameters.WorkArea;
                }
                var info = new MONITORINFOEX();
                GetMonitorInfo(new HandleRef(null, hmonitor), info);
                return new Rect(
                    info.rcWork.left, info.rcWork.top,
                    info.rcWork.right - info.rcWork.left,
                    info.rcWork.bottom - info.rcWork.top);
            }
        }

        /// <summary>
        /// Retrieves a Screen for the display that contains the largest portion of the specified control.
        /// </summary>
        /// <param name="hwnd">The window handle for which to retrieve the Screen.</param>
        /// <returns>A Screen for the display that contains the largest region of the object. In multiple display environments where no display contains any portion of the specified window, the display closest to the object is returned.</returns>
        public static ScreenInterop FromHandle(IntPtr hwnd)
        {
            if (MultiMonitorSupport)
            {
                return new ScreenInterop(MonitorFromWindow(new HandleRef(null, hwnd), 2));
            }
            return new ScreenInterop((IntPtr)PRIMARY_MONITOR);
        }

        /// <summary>
        /// Retrieves a Screen for the display that contains the specified point.
        /// </summary>
        /// <param name="point">A <see cref="T:System.Windows.Point" /> that specifies the location for which to retrieve a Screen.</param>
        /// <returns>A Screen for the display that contains the point. In multiple display environments where no display contains the point, the display closest to the specified point is returned.</returns>
        public static ScreenInterop FromPoint(Point point)
        {
            if (MultiMonitorSupport)
            {
                var pt = new POINTSTRUCT((int)point.X, (int)point.Y);
                return new ScreenInterop(MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST));
            }
            return new ScreenInterop((IntPtr)PRIMARY_MONITOR);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the specified object is equal to this Screen.
        /// </summary>
        /// <param name="obj">The object to compare to this Screen.</param>
        /// <returns>true if the specified object is equal to this Screen; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is ScreenInterop monitor && hmonitor == monitor.hmonitor;
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return (int)hmonitor;
        }

        private class MonitorEnumCallback
        {
            public ArrayList Screens { get; private set; }

            public MonitorEnumCallback()
            {
                this.Screens = new ArrayList();
            }

            public bool Callback(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lparam)
            {
                this.Screens.Add(new ScreenInterop(monitor, hdc));
                return true;
            }
        }
    }
}
