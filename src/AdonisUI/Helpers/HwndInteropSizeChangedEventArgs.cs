using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Helpers
{
    public class HwndInteropSizeChangedEventArgs
        : EventArgs
    {
        /// <summary>
        /// The type of resizing requested.
        /// </summary>
        public enum ResizeRequestType
        {
            /// <summary>
            /// The window has been resized, but neither the <see cref="Minimized"/> nor <see cref="Maximized"/> value applies.
            /// </summary>
            Restored = 0,

            /// <summary>
            /// The window has been minimized.

            /// </summary>
            Minimized = 1,
            
            /// <summary>
            /// The window has been maximized.
            /// </summary>
            Maximized = 2,

            /// <summary>
            /// Message is sent to all pop-up windows when some other window has been restored to its former size.
            /// </summary>
            MaxShow = 3,

            /// <summary>
            /// Message is sent to all pop-up windows when some other window is maximized.
            /// </summary>
            MaxHide = 4,
        }

        /// <summary>
        /// The type of resizing requested.
        /// </summary>
        public ResizeRequestType Type { get; private set; }

        public HwndInteropSizeChangedEventArgs(ResizeRequestType resizeRequestType)
        {
            Type = resizeRequestType;
        }
    }
}
