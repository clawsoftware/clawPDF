using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AdonisUI.Helpers
{
    /// <summary>
    /// Helper class for interactions with the system's native context menu
    /// </summary>
    internal static class SystemContextMenuInterop
    {
        public static uint TPM_LEFTALIGN = 0;

        public static uint TPM_RETURNCMD = 256;

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = false, SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        public static void OpenSystemContextMenu(Window window, Point positionInWindow)
        {
            Point screenCoordinate = window.PointToScreen(positionInWindow);
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;
            IntPtr systemMenu = GetSystemMenu(windowHandle, false);

            int track = TrackPopupMenuEx(
                systemMenu,
                TPM_LEFTALIGN | TPM_RETURNCMD,
                Convert.ToInt32(screenCoordinate.X),
                Convert.ToInt32(screenCoordinate.Y),
                windowHandle,
                IntPtr.Zero);

            if (track == 0)
                return;

            PostMessage(windowHandle, 274, new IntPtr(track), IntPtr.Zero);
        }
    }
}
