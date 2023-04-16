using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AdonisUI.ViewModels;

namespace AdonisUI.Controls
{
    /// <summary>
    /// The default implementation of <see cref="IMessageBoxButtonModel"/> used to configure the appearance and behavior of a single button of some <see cref="MessageBoxWindow"/>.
    /// </summary>
    public class MessageBoxButtonModel
        : PropertyChangedBase
        , IMessageBoxButtonModel
    {
        private object _id;

        /// <inheritdoc/>
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

        private MessageBoxResult _causedResult;

        /// <inheritdoc/>
        public MessageBoxResult CausedResult
        {
            get => _causedResult;
            set => SetProperty(ref _causedResult, value);
        }

        private bool _isDefault;

        /// <inheritdoc/>
        public bool IsDefault
        {
            get => _isDefault;
            set => SetProperty(ref _isDefault, value);
        }

        private bool _isCancel;

        /// <inheritdoc/>
        public bool IsCancel
        {
            get => _isCancel;
            set => SetProperty(ref _isCancel, value);
        }

        /// <summary>
        /// Creates an instance of <see cref="MessageBoxButtonModel"/>.
        /// </summary>
        /// <param name="label">A <see cref="String"/> that specifies the content of the button.</param>
        /// <param name="causedResult">A <see cref="MessageBoxResult"/> that will be used as <see cref="IMessageBoxModel.Result"/> of the parent message box if the button is pressed.</param>
        public MessageBoxButtonModel(string label, MessageBoxResult causedResult)
        {
            Label = label;
            CausedResult = causedResult;
        }
    }
}
