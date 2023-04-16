using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Contains methods for creating lists of <see cref="IMessageBoxButtonModel"/>.
    /// </summary>
    public static class MessageBoxButtons
    {
        /// <summary>
        /// Creates one or more instances of <see cref="IMessageBoxButtonModel"/> depending on the given <see cref="MessageBoxButton"/>.
        /// </summary>
        /// <param name="buttons">A <see cref="MessageBoxButton"/> value that specifies the button or buttons that should be created.</param>
        /// <param name="labels">An array of <see cref="String"/> that allows specifying custom button labels. They are expected in the same order as the <see cref="MessageBoxButton"/> values are named.</param>
        /// <returns>A collection of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IEnumerable<IMessageBoxButtonModel> Create(MessageBoxButton buttons, params string[] labels)
        {
            labels ??= new string[0];

            switch (buttons)
            {
                case MessageBoxButton.OK:
                    return new[] { Ok(labels.FirstOrDefault()) };

                case MessageBoxButton.OKCancel:
                    return OkCancel(labels.FirstOrDefault(), labels.ElementAtOrDefault(1));

                case MessageBoxButton.YesNo:
                    return YesNo(labels.FirstOrDefault(), labels.ElementAtOrDefault(1));

                case MessageBoxButton.YesNoCancel:
                    return YesNoCancel(labels.FirstOrDefault(), labels.ElementAtOrDefault(1), labels.ElementAtOrDefault(2));
            }

            return Enumerable.Empty<IMessageBoxButtonModel>();
        }

        /// <summary>
        /// Creates two instances of <see cref="IMessageBoxButtonModel"/> where one is configured as an OK button the other one as a Cancel button.
        /// </summary>
        /// <param name="okLabel">A <see cref="String"/> that specifies the custom label of the OK button.</param>
        /// <param name="cancelLabel">A <see cref="String"/> that specifies the custom label of the Cancel button.</param>
        /// <returns>A collection of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IEnumerable<IMessageBoxButtonModel> OkCancel(string okLabel = null, string cancelLabel = null)
        {
            return new[]
            {
                Ok(okLabel),
                Cancel(cancelLabel),
            };
        }

        /// <summary>
        /// Creates two instances of <see cref="IMessageBoxButtonModel"/> where one is configured as a Yes button the other one as a No button.
        /// </summary>
        /// <param name="yesLabel">A <see cref="String"/> that specifies the custom label of the Yes button.</param>
        /// <param name="noLabel">A <see cref="String"/> that specifies the custom label of the No button.</param>
        /// <returns>A collection of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IEnumerable<IMessageBoxButtonModel> YesNo(string yesLabel = null, string noLabel = null)
        {
            return new[]
            {
                Yes(yesLabel),
                No(noLabel),
            };
        }

        /// <summary>
        /// Creates three instances of <see cref="IMessageBoxButtonModel"/> where one is configured as a Yes button, one as a No button and one as a Cancel button.
        /// </summary>
        /// <param name="yesLabel">A <see cref="String"/> that specifies the custom label of the Yes button.</param>
        /// <param name="noLabel">A <see cref="String"/> that specifies the custom label of the No button.</param>
        /// <param name="cancelLabel">A <see cref="String"/> that specifies the custom label of the Cancel button.</param>
        /// <returns>A collection of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IEnumerable<IMessageBoxButtonModel> YesNoCancel(string yesLabel = null, string noLabel = null, string cancelLabel = null)
        {
            return new[]
            {
                Yes(yesLabel),
                No(noLabel),
                Cancel(cancelLabel),
            };
        }

        /// <summary>
        /// Creates an instance of <see cref="IMessageBoxButtonModel"/> that is configured as an OK button.
        /// </summary>
        /// <param name="label">A <see cref="String"/> that specifies the custom label of the button.</param>
        /// <returns>An instance of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IMessageBoxButtonModel Ok(string label = null)
        {
            return new MessageBoxButtonModel(label ?? "OK", MessageBoxResult.OK) { IsDefault = true };
        }

        /// <summary>
        /// Creates an instance of <see cref="IMessageBoxButtonModel"/> that is configured as a Yes button.
        /// </summary>
        /// <param name="label">A <see cref="String"/> that specifies the custom label of the button.</param>
        /// <returns>An instance of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IMessageBoxButtonModel Yes(string label = null)
        {
            return new MessageBoxButtonModel(label ?? "Yes", MessageBoxResult.Yes) { IsDefault = true };
        }

        /// <summary>
        /// Creates an instance of <see cref="IMessageBoxButtonModel"/> that is configured as a No button.
        /// </summary>
        /// <param name="label">A <see cref="String"/> that specifies the custom label of the button.</param>
        /// <returns>An instance of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IMessageBoxButtonModel No(string label = null)
        {
            return new MessageBoxButtonModel(label ?? "No", MessageBoxResult.No);
        }

        /// <summary>
        /// Creates an instance of <see cref="IMessageBoxButtonModel"/> that is configured as a Cancel button.
        /// </summary>
        /// <param name="label">A <see cref="String"/> that specifies the custom label of the button.</param>
        /// <returns>An instance of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IMessageBoxButtonModel Cancel(string label = null)
        {
            return new MessageBoxButtonModel(label ?? "Cancel", MessageBoxResult.Cancel) { IsCancel = true };
        }

        /// <summary>
        /// Creates an instance of <see cref="IMessageBoxButtonModel"/> that is configured as a custom button.
        /// </summary>
        /// <param name="label">A <see cref="String"/> that specifies the label of the button.</param>
        /// <param name="id">An <see cref="Object"/> that specifies an optional ID that can be used to identify the button.</param>
        /// <returns>An instance of <see cref="IMessageBoxButtonModel"/>.</returns>
        public static IMessageBoxButtonModel Custom(string label, object id = null)
        {
            return new MessageBoxButtonModel(label, MessageBoxResult.Custom) { Id = id };
        }
    }
}