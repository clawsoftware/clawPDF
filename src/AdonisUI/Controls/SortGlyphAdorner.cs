using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AdonisUI.Controls
{
    public class SortGlyphAdorner
        : FrameworkElementAdorner
    {
        public SortGlyphAdorner(FrameworkElement adornedElement, FrameworkElement sortGlyph)
            : base(sortGlyph, adornedElement, AdornerPlacement.Inside, AdornerPlacement.Inside, 0, 0)
        {
        }

        public SortGlyphAdorner(FrameworkElement adornedElement, ListSortDirection direction)
            : base(CreateDefaultSortGlyph(direction), adornedElement, AdornerPlacement.Inside, AdornerPlacement.Inside, 0, 0)
        {
        }

        private static FrameworkElement CreateDefaultSortGlyph(ListSortDirection direction)
        {
            DataTemplate expanderTemplate = Application.Current.TryFindResource(AdonisUI.Templates.Expander) as DataTemplate;

            if (expanderTemplate == null)
                return null;

            var adornerChild = new ContentPresenter
            {
                ContentTemplate = expanderTemplate,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(8, 0, 8, 0),
                IsHitTestVisible = false,
            };

            if (direction == ListSortDirection.Ascending)
            {
                adornerChild.RenderTransformOrigin = new Point(0.5, 0.5);
                adornerChild.RenderTransform = new ScaleTransform
                {
                    ScaleY = -1,
                    CenterX = 0.5,
                    CenterY = 0.5,
                };
            }

            return adornerChild;
        }
    }
}
