using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Helpers
{
    public class HwndInteropPositionChangingEventArgs
        : EventArgs
    {
        public enum PositionChangeType
        {
            /// <summary>
            /// Draws a frame (defined in the window's class description) around the window. Same as the <see cref="FRAMECHANGED"/> flag.
            /// </summary>
            DRAWFRAME = 0x0020,

            /// <summary>
            /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
            /// </summary>
            FRAMECHANGED = DRAWFRAME,

            /// <summary>
            /// Hides the window.
            /// </summary>
            HIDEWINDOW = 0x0080,

            /// <summary>
            /// Does not activate the window.
            /// </summary>
            NOACTIVATE = 0x0010,

            /// <summary>
            /// Discards the entire contents of the client area.
            /// </summary>
            NOCOPYBITS = 0x0100,

            /// <summary>
            /// Retains the current position (ignores the x and y members).
            /// </summary>
            NOMOVE = 0x0002,

            /// <summary>
            /// Does not change the owner window's position in the Z order.
            /// </summary>
            NOOWNERZORDER = 0x0200,

            /// <summary>
            /// Does not redraw changes.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            /// Does not change the owner window's position in the Z order. Same as the <see cref="NOOWNERZORDER"/> flag.
            /// </summary>
            NOREPOSITION = NOOWNERZORDER,

            /// <summary>
            /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            NOSENDCHANGING = 0x0400,

            /// <summary>
            /// Retains the current size (ignores the cx and cy members).
            /// </summary>
            NOSIZE = 0x0001,

            /// <summary>
            /// Retains the current Z order (ignores the hwndInsertAfter member).
            /// </summary>
            NOZORDER = 0x0004,

            /// <summary>
            /// Displays the window.
            /// </summary>
            SHOWWINDOW = 0x0040,

            /// <summary>
            /// No official documentation found. Seems to occur whe maximizing or restoring a window.
            /// </summary>
            MAXIMIZERESTORE = 0x8020,
        }

        public PositionChangeType Type { get; private set; }

        public HwndInteropPositionChangingEventArgs(PositionChangeType positionChangeType)
        {
            Type = positionChangeType;
        }
    }
}
