using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Exposes properties used to configure the appearance and behavior of a single button of some <see cref="MessageBoxWindow"/>.
    /// </summary>
    public interface IMessageBoxButtonModel
    {
        /// <summary>
        /// An <see cref="Object"/> that can be used to identify the button.
        /// </summary>
        object Id { get; }

        /// <summary>
        /// A <see cref="String"/> that specifies the content of the button.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// A <see cref="MessageBoxResult"/> that will be used as <see cref="IMessageBoxModel.Result"/> of the parent message box if the button is pressed.
        /// </summary>
        MessageBoxResult CausedResult { get; }

        /// <summary>
        /// A <see cref="bool"/> that specifies if the button should be preselected when opening the message box.
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        /// A <see cref="bool"/> that specifies if the button should be pressed when the user presses the escape key.
        /// If at least one button in <see cref="IMessageBoxModel.Buttons"/> has this property set to <see langword="true"/> the <see cref="MessageBoxWindow"/> displays the window close button.
        /// </summary>
        bool IsCancel { get; }
    }
}
