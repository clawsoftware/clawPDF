using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace clawSoft.clawPDF.Shared.Helper
{
    public static class TopMostHelper
    {
        private const int GwlExstyle = -20;
        private const int WsExTopmost = 0x8;

        private static readonly IntPtr NoTopMost = new IntPtr(-2);
        private static readonly IntPtr TopMost = new IntPtr(-1);
        private static readonly IntPtr Top = new IntPtr(0);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
            int uFlags);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        public static IntPtr GetWindowHandle(Window window)
        {
            return new WindowInteropHelper(window).Handle;
        }

        #region Forms definitions

        private static void MakeTopMostForm(Form f, bool revertWhenActive = false)
        {
            SetForegroundWindow(f.Handle);
            BringWindowToTop(f.Handle);
            SetWindowPos(f.Handle, TopMost, f.Left, f.Top, f.Width, f.Height, 0);
            SetWindowLong(f.Handle, GwlExstyle, GetWindowLong(f.Handle, GwlExstyle) | WsExTopmost);

            f.TopLevel = true;
            f.BringToFront();
            f.Activate();
            f.TopMost = true;
            f.Focus();

            if (revertWhenActive)
                f.Activated += FormActivated;
        }

        public static void UndoTopMostForm(Form f)
        {
            f.TopMost = false;

            SetWindowPos(f.Handle, NoTopMost, f.Left, f.Top, f.Width, f.Height, 0);
            SetWindowLong(f.Handle, GwlExstyle, GetWindowLong(f.Handle, GwlExstyle) ^ WsExTopmost);
        }

        public static void ShowTopMost(Form f, bool revertWhenActive = false)
        {
            MakeTopMostForm(f, revertWhenActive);
            f.Show();
        }

        public static DialogResult ShowDialogTopMost(Form f, bool revertWhenActive = false)
        {
            MakeTopMostForm(f, revertWhenActive);
            return f.ShowDialog();
        }

        private static void FormActivated(object sender, EventArgs e)
        {
            var f = sender as Form;
            if (f != null)
            {
                UndoTopMostForm(f);
                f.Activated -= FormActivated;
            }
        }

        public static DialogResult ShowDialogTopMost(SaveFileDialog saveFileDialog, bool revertWhenActive = false)
        {
            var form = new Form();
            MakeTopMostForm(form, revertWhenActive);
            form.TopMost =
                !revertWhenActive; //directly deactivate topmost -> dialog should stay in foreground but is not locked there

            var result = saveFileDialog.ShowDialog(form);
            return result;
        }

        #endregion Forms definitions

        #region WPF definitions

        private static void MakeTopMostWindow(Window window, bool revertWhenActive)
        {
            window.Topmost = true;
            window.Activate();

            if (revertWhenActive)
                window.Activated += WindowActivated;

            window.Loaded += WindowLoad;

            /*
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;

            if (windowHandle != IntPtr.Zero)
            {
                SetForegroundWindow(windowHandle);
                BringWindowToTop(windowHandle);
                //SetWindowPos(f.Handle, TopMost, f.Left, f.Top, f.Width, f.Height, 0);
                SetWindowLong(windowHandle, GwlExstyle, GetWindowLong(windowHandle, GwlExstyle) | WsExTopmost);
            }
            //*/
        }

        public static void UndoTopMostWindow(Window window)
        {
            window.Topmost = false;
            /*
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;

            //SetWindowPos(f.Handle, NoTopMost, f.Left, f.Top, f.Width, f.Height, 0);
            SetWindowLong(windowHandle, GwlExstyle, GetWindowLong(windowHandle, GwlExstyle) ^ WsExTopmost);
            */
        }

        public static void ShowTopMost(Window window, bool revertWhenActive)
        {
            MakeTopMostWindow(window, revertWhenActive);
            window.Show();
        }

        public static bool? ShowDialogTopMost(Window window, bool revertWhenActive)
        {
            MakeTopMostWindow(window, revertWhenActive);
            return window.ShowDialog();
        }

        private static void WindowActivated(object sender, EventArgs eventArgs)
        {
            var window = sender as Window;

            if (window != null)
            {
                UndoTopMostWindow(window);
                window.Activated -= WindowActivated;
            }
        }

        private static void WindowLoad(object sender, EventArgs eventArgs)
        {
            var window = sender as Window;

            if (window != null)
            {
                var windowHandle = GetWindowHandle(window);
                SetForegroundWindow(windowHandle);
            }
        }

        #endregion WPF definitions
    }
}