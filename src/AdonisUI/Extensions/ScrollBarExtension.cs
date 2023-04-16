using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace AdonisUI.Extensions
{
    /// <summary>
    /// Provides attached behaviors related to the ScrollBar control.
    /// </summary>
    public class ScrollBarExtension
    {
        /// <summary>
        /// Gets the value of the <see cref="ExpansionModeProperty"/> attached property of the specified ScrollBar.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(ScrollBar))]
        public static ScrollBarExpansionMode GetExpansionMode(DependencyObject obj)
        {
            return (ScrollBarExpansionMode)obj.GetValue(ExpansionModeProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="ExpansionModeProperty"/> attached property of the specified ScrollBar.
        /// </summary>
        public static void SetExpansionMode(DependencyObject obj, ScrollBarExpansionMode value)
        {
            obj.SetValue(ExpansionModeProperty, value);
        }

        /// <summary>
        /// A DependencyProperty that controls when to expand and collapse the scroll bar.
        /// </summary>
        public static readonly DependencyProperty ExpansionModeProperty = DependencyProperty.RegisterAttached("ExpansionMode", typeof(ScrollBarExpansionMode), typeof(ScrollBarExtension), new PropertyMetadata(ScrollBarExpansionMode.ExpandOnHover));
    }
}
