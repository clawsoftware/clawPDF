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
    /// The default implementation of <see cref="IMessageBoxModel"/> used to configure the appearance and behavior of a <see cref="MessageBoxWindow"/>.
    /// </summary>
    public class MessageBoxModel
        : PropertyChangedBase
        , IMessageBoxModel
    {
        private string _text;

        /// <inheritdoc/>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _caption;

        /// <inheritdoc/>
        public string Caption
        {
            get => _caption;
            set => SetProperty(ref _caption, value);
        }

        private IEnumerable<IMessageBoxButtonModel> _buttons = new List<IMessageBoxButtonModel>();

        /// <inheritdoc/>
        public IEnumerable<IMessageBoxButtonModel> Buttons
        {
            get => _buttons;
            set => SetProperty(ref _buttons, value);
        }

        private IEnumerable<IMessageBoxCheckBoxModel> _checkBoxes = new List<IMessageBoxCheckBoxModel>();

        /// <inheritdoc/>
        public IEnumerable<IMessageBoxCheckBoxModel> CheckBoxes
        {
            get => _checkBoxes;
            set => SetProperty(ref _checkBoxes, value);
        }

        private MessageBoxImage _icon;

        /// <inheritdoc/>
        public MessageBoxImage Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        private MessageBoxResult _result;

        /// <inheritdoc/>
        public MessageBoxResult Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private IMessageBoxButtonModel _buttonPressed;

        /// <inheritdoc/>
        public IMessageBoxButtonModel ButtonPressed
        {
            get => _buttonPressed;
            set => SetProperty(ref _buttonPressed, value);
        }

        private bool _isSoundEnabled = true;

        /// <inheritdoc/>
        public bool IsSoundEnabled
        {
            get => _isSoundEnabled;
            set => SetProperty(ref _isSoundEnabled, value);
        }

        /// <summary>
        /// Sets <see cref="IMessageBoxButtonModel.IsDefault"/> to <see langword="true"/> on the first button that matches the given <paramref name="defaultResult"/>
        /// and to <see langword="false"/> on all other buttons.
        /// </summary>
        /// <param name="defaultResult">The result that matches the default button's <see cref="IMessageBoxButtonModel.CausedResult"/>.</param>
        public void SetDefaultButton(MessageBoxResult defaultResult)
        {
            IMessageBoxButtonModel defaultButton = _buttons.FirstOrDefault(btn => btn.CausedResult == defaultResult);

            SetDefaultButton(defaultButton);
        }

        /// <summary>
        /// Sets <see cref="IMessageBoxButtonModel.IsDefault"/> to <see langword="true"/> on the given button and to <see langword="false"/> on all other buttons.
        /// </summary>
        /// <param name="defaultButton">The button that is supposed to be the default button. It must be part of the <see cref="Buttons"/> collection.</param>
        public void SetDefaultButton(IMessageBoxButtonModel defaultButton)
        {
            foreach (IMessageBoxButtonModel button in _buttons)
            {
                button.IsDefault = false;
            }

            if (defaultButton != null && _buttons.Contains(defaultButton))
                defaultButton.IsDefault = true;
        }
    }
}
