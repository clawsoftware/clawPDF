using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdonisUI.ViewModels
{
    /// <inheritdoc cref="INotifyPropertyChanged" />
    /// <summary>
    /// Notifies clients that a property value is changing or has changed.
    /// It includes methods to set the value of a property and automatically raise events on the appropriate event handlers.
    /// </summary>
    public class PropertyChangedBase
        : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise an event on <see cref="INotifyPropertyChanged.PropertyChanged"/> to indicate that a property value changed.
        /// </summary>
        /// <param name="propertyName">Name of the changed property value.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// <para>
        /// Set <paramref name="storage"/> to the given <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If the given <paramref name="value"/> is different than the current value,
        /// it raises an event on <see cref="INotifyPropertyChanged.PropertyChanged"/> after the storage was changed.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="storage">Reference to the storage field.</param>
        /// <param name="value">New value to set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns><see langword="true"/> if the value was different from the <paramref name="storage"/> variable and an event on <see cref="PropertyChanged"/> was raised; otherwise, <see langword="false"/>.</returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
