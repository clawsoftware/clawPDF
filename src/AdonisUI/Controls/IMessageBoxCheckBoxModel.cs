using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Exposes properties used to configure the appearance and behavior of a single check box of some <see cref="MessageBoxWindow"/>.
    /// </summary>
    public interface IMessageBoxCheckBoxModel
    {
        /// <summary>
        /// A <see cref="String"/> that specifies the content of the check box.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// A <see cref="bool"/> that specifies if the check box is checked.
        /// </summary>
        bool IsChecked { get; }

        /// <summary>
        /// A <see cref="MessageBoxCheckBoxPlacement"/> that specifies where the check box is placed inside the message box.
        /// </summary>
        MessageBoxCheckBoxPlacement Placement { get; }
    }
}
