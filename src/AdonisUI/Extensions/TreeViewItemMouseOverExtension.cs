using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdonisUI.Extensions
{
    /// <summary>
    /// See https://blogs.msdn.microsoft.com/mikehillberg/2006/09/21/a-trigger-for-the-treeviewitem-directly-under-the-mouse/
    /// </summary>
    public class TreeViewItemMouseOverExtension
    {
        /// <summary>
        /// The TreeViewItem that the mouse is currently directly over (or null).
        /// </summary>
        private static TreeViewItem _currentItem;

        static TreeViewItemMouseOverExtension()
        {
            // Get all Mouse enter/leave events for TreeViewItem.
            EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.MouseEnterEvent, new MouseEventHandler(OnMouseTransition), true);
            EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.MouseLeaveEvent, new MouseEventHandler(OnMouseTransition), true);

            // Listen for the UpdateOverItemEvent on all TreeViewItem's.
            EventManager.RegisterClassHandler(typeof(TreeViewItem), UpdateOverItemEvent, new RoutedEventHandler(OnUpdateOverItem));
        }

        /// <summary>
        /// A DependencyProperty that will be true only on the TreeViewItem that the mouse is directly over.
        /// I.e., this won't be set on that parent item.
        /// </summary>
        private static readonly DependencyPropertyKey IsMouseDirectlyOverItemKey = DependencyProperty.RegisterAttachedReadOnly("IsMouseDirectlyOverItem", typeof(bool), typeof(TreeViewItemMouseOverExtension), new FrameworkPropertyMetadata(null, CalculateIsMouseDirectlyOverItem));

        public static readonly DependencyProperty IsMouseDirectlyOverItemProperty = IsMouseDirectlyOverItemKey.DependencyProperty;

        /// <summary>
        /// A RoutedEvent used to find the nearest encapsulating TreeViewItem to the mouse's current position.
        /// </summary>
        private static readonly RoutedEvent UpdateOverItemEvent = EventManager.RegisterRoutedEvent("UpdateOverItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TreeViewItemMouseOverExtension));

        public static bool GetIsMouseDirectlyOverItem(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseDirectlyOverItemProperty);
        }

        private static object CalculateIsMouseDirectlyOverItem(DependencyObject item, object value)
        {
            return item == _currentItem;
        }

        /// <summary>
        /// This method is a listener for the UpdateOverItemEvent. When it is received, it means that the sender is the closest TreeViewItem to the mouse
        /// (closest in the sense of the tree,  not geographically).
        /// </summary>
        private static void OnUpdateOverItem(object sender, RoutedEventArgs args)
        {
            // Mark this object as the tree view item over which the mouse is currently positioned.
            _currentItem = sender as TreeViewItem;

            // Tell that item to re-calculate the IsMouseDirectlyOverItem property
            _currentItem.InvalidateProperty(IsMouseDirectlyOverItemProperty);

            // Prevent this event from notifying other tree view items higher in the tree.
            args.Handled = true;
        }

        /// <summary>
        /// This method is a listener for both the MouseEnter event and the MouseLeave event on TreeViewItems.
        /// It updates the _currentItem, and updates the IsMouseDirectlyOverItem property on the previous TreeViewItem and the new TreeViewItem.
        /// </summary>
        private static void OnMouseTransition(object sender, MouseEventArgs args)
        {
            lock (IsMouseDirectlyOverItemProperty)
            {
                if (_currentItem != null)
                {
                    // Tell the item that previously had the mouse that it no longer does.
                    DependencyObject oldItem = _currentItem;
                    _currentItem = null;
                    oldItem.InvalidateProperty(IsMouseDirectlyOverItemProperty);
                }

                // Get the element that is currently under the mouse.
                IInputElement currentPosition = Mouse.DirectlyOver;

                // See if the mouse is still over something (any element, not just a tree view item).
                if (currentPosition != null)
                {
                    // Raise an event from that point.  If a TreeViewItem is anywhere above this point
                    // in the tree, it will receive this event and update _currentItem.
                    RoutedEventArgs newItemArgs = new RoutedEventArgs(UpdateOverItemEvent);
                    currentPosition.RaiseEvent(newItemArgs);
                }
            }
        }
    }
}
