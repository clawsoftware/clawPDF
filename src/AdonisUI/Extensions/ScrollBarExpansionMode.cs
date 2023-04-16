using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Extensions
{
    /// <summary>
    /// Values for controlling when to expand scroll bars.
    /// </summary>
    public enum ScrollBarExpansionMode
    {
        /// <summary>
        /// Never expand the scroll bar. Keep it always collapsed.
        /// </summary>
        NeverExpand,

        /// <summary>
        /// Expand the scroll bar when the mouse hovers over it.
        /// </summary>
        ExpandOnHover,

        /// <summary>
        /// Always expand the scroll bar. Do not collapse it.
        /// </summary>
        AlwaysExpand,
    }
}
