using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Displays a message box.
    /// </summary>
    public static class MessageBox
    {
        /// <summary>
        /// Displays a message box that has a message and that returns a result.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that specifies the text to display.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text)
        {
            var messageBoxModel = new MessageBoxModel
            {
                Text = text,
            };

            return Show(messageBoxModel);
        }

        /// <summary>
        /// Displays a message box that is configured like specified in the <see cref="IMessageBoxModel"/> and that returns a result.
        /// </summary>
        /// <param name="messageBoxModel">An <see cref="IMessageBoxModel"/> that configures the appearance and behavior of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(IMessageBoxModel messageBoxModel)
        {
            Window activeWindow = Application.Current?.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            return Show(activeWindow, messageBoxModel);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box is configured like specified in the <see cref="IMessageBoxModel"/> and returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="messageBoxModel">An <see cref="IMessageBoxModel"/> that configures the appearance and behavior of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, IMessageBoxModel messageBoxModel)
        {
            var messageBox = new MessageBoxWindow
            {
                Owner = owner,
                ViewModel = messageBoxModel,
            };

            messageBox.ShowDialog();
            return messageBoxModel.Result;
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon; and that accepts a default message box result and returns a result.
        /// </summary>
        /// <param name="text">A <see cref="String"/> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="String"/> that specifies the title bar caption to display.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <param name="labels">A <see cref="MessageBoxResult"/> value that specifies the button label</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(string text, string caption = null, string[] labels = null, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            var messageBoxModel = new MessageBoxModel
            {
                Text = text,
                Caption = caption,
                Buttons = MessageBoxButtons.Create(buttons, labels),
                Icon = icon,
            };

            messageBoxModel.SetDefaultButton(defaultResult);

            return Show(messageBoxModel);
        }

        /// <summary>
        /// Displays a message box in front of the specified window. The message box displays a message, title bar caption, button, and icon; and accepts a default message box result and returns a result.
        /// </summary>
        /// <param name="owner">A <see cref="Window"/> that represents the owner window of the message box.</param>
        /// <param name="text">A <see cref="String"/> that specifies the text to display.</param>
        /// <param name="caption">A <see cref="String"/> that specifies the title bar caption to display.</param>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies which button or buttons to display.</param>
        /// <param name="icon">A <see cref="MessageBoxImage"/> value that specifies the icon to display.</param>
        /// <param name="defaultResult">A <see cref="MessageBoxResult"/> value that specifies the default result of the message box.</param>
        /// <returns>A <see cref="MessageBoxResult"/> value that specifies which message box button is clicked by the user.</returns>
        public static MessageBoxResult Show(Window owner, string text, string caption = null, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            var messageBoxModel = new MessageBoxModel
            {
                Text = text,
                Caption = caption,
                Buttons = MessageBoxButtons.Create(buttons),
                Icon = icon,
            };

            messageBoxModel.SetDefaultButton(defaultResult);

            return Show(owner, messageBoxModel);
        }
    }
}