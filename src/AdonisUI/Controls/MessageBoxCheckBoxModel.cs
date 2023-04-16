using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdonisUI.ViewModels;

namespace AdonisUI.Controls
{
    /// <summary>
    /// The default implementation of <see cref="IMessageBoxCheckBoxModel"/> used to configure the appearance and behavior of a single check box of some <see cref="MessageBoxWindow"/>.
    /// </summary>
    public class MessageBoxCheckBoxModel
        : PropertyChangedBase
        , IMessageBoxCheckBoxModel
    {
        private object _id;

        /// <summary>
        /// An <see cref="Object"/> that can be used to identify the check box.
        /// </summary>
        public object Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _label;

        /// <inheritdoc/>
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private bool _isChecked;

        /// <inheritdoc/>
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private MessageBoxCheckBoxPlacement _placement;

        /// <inheritdoc/>
        public MessageBoxCheckBoxPlacement Placement
        {
            get => _placement;
            set => SetProperty(ref _placement, value);
        }

        /// <summary>
        /// Creates an instance of <see cref="MessageBoxCheckBoxModel"/>.
        /// </summary>
        /// <param name="label">A <see cref="String"/> that specifies the content of the check box.</param>
        public MessageBoxCheckBoxModel(string label)
        {
            _label = label;
        }
    }
}
