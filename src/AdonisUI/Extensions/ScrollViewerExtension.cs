using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdonisUI.Extensions
{
    /// <summary>
    /// Provides attached behaviors related to the ScrollViewer control.
    /// </summary>
    public class ScrollViewerExtension
    {
        /// <summary>
        /// Gets the value of the <see cref="VerticalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ScrollViewer))]
        public static ScrollBarExpansionMode GetVerticalScrollBarExpansionMode(DependencyObject obj)
        {
            return (ScrollBarExpansionMode)obj.GetValue(VerticalScrollBarExpansionModeProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="VerticalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static void SetVerticalScrollBarExpansionMode(DependencyObject obj, ScrollBarExpansionMode value)
        {
            obj.SetValue(VerticalScrollBarExpansionModeProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="HorizontalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ScrollViewer))]
        public static ScrollBarExpansionMode GetHorizontalScrollBarExpansionMode(DependencyObject obj)
        {
            return (ScrollBarExpansionMode)obj.GetValue(HorizontalScrollBarExpansionModeProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="HorizontalScrollBarExpansionModeProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static void SetHorizontalScrollBarExpansionMode(DependencyObject obj, ScrollBarExpansionMode value)
        {
            obj.SetValue(HorizontalScrollBarExpansionModeProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="VerticalScrollBarPlacementProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static ScrollBarPlacement GetVerticalScrollBarPlacement(DependencyObject obj)
        {
            return (ScrollBarPlacement)obj.GetValue(VerticalScrollBarPlacementProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="VerticalScrollBarPlacementProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static void SetVerticalScrollBarPlacement(DependencyObject obj, ScrollBarPlacement value)
        {
            obj.SetValue(VerticalScrollBarPlacementProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="HorizontalScrollBarPlacementProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static ScrollBarPlacement GetHorizontalScrollBarPlacement(DependencyObject obj)
        {
            return (ScrollBarPlacement)obj.GetValue(HorizontalScrollBarPlacementProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="HorizontalScrollBarPlacementProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static void SetHorizontalScrollBarPlacement(DependencyObject obj, ScrollBarPlacement value)
        {
            obj.SetValue(HorizontalScrollBarPlacementProperty, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="HideScrollBarsUntilMouseOverProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static bool GetHideScrollBarsUntilMouseOver(DependencyObject obj)
        {
            return (bool)obj.GetValue(HideScrollBarsUntilMouseOverProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="HideScrollBarsUntilMouseOverProperty"/> attached property of the specified ScrollViewer.
        /// </summary>
        public static void SetHideScrollBarsUntilMouseOver(DependencyObject obj, bool value)
        {
            obj.SetValue(HideScrollBarsUntilMouseOverProperty, value);
        }

        /// <summary>
        /// A DependencyProperty that controls when to expand and collapse the vertical scroll bar.
        /// </summary>
        public static readonly DependencyProperty VerticalScrollBarExpansionModeProperty = DependencyProperty.RegisterAttached("VerticalScrollBarExpansionMode", typeof(ScrollBarExpansionMode), typeof(ScrollViewerExtension), new PropertyMetadata(ScrollBarExpansionMode.ExpandOnHover));

        /// <summary>
        /// A DependencyProperty that controls when to expand and collapse the horizontal scroll bar.
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollBarExpansionModeProperty = DependencyProperty.RegisterAttached("HorizontalScrollBarExpansionMode", typeof(ScrollBarExpansionMode), typeof(ScrollViewerExtension), new PropertyMetadata(ScrollBarExpansionMode.ExpandOnHover));

        /// <summary>
        /// A DependencyProperty that controls the placement of the vertical scroll bar.
        /// </summary>
        public static readonly DependencyProperty VerticalScrollBarPlacementProperty = DependencyProperty.RegisterAttached("VerticalScrollBarPlacement", typeof(ScrollBarPlacement), typeof(ScrollViewerExtension), new PropertyMetadata(ScrollBarPlacement.Docked));

        /// <summary>
        /// A DependencyProperty that controls the placement of the horizontal scroll bar.
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollBarPlacementProperty = DependencyProperty.RegisterAttached("HorizontalScrollBarPlacement", typeof(ScrollBarPlacement), typeof(ScrollViewerExtension), new PropertyMetadata(ScrollBarPlacement.Docked));

        /// <summary>
        /// A DependencyProperty that controls whether to set up fade-in and fade-out animations for both scroll bars when entering or leaving the scroll viewer.
        /// </summary>
        public static readonly DependencyProperty HideScrollBarsUntilMouseOverProperty = DependencyProperty.RegisterAttached("HideScrollBarsUntilMouseOver", typeof(bool), typeof(ScrollViewerExtension), new PropertyMetadata(false));
    }
}
