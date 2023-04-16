using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace AdonisUI.Extensions
{
    public class CursorSpotlightExtension
    {
        public static FrameworkElement GetMouseEventSource(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(MouseEventSourceProperty);
        }

        public static void SetMouseEventSource(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(MouseEventSourceProperty, value);
        }

        public static Brush GetBackgroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BackgroundBrushProperty);
        }

        public static void SetBackgroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BackgroundBrushProperty, value);
        }

        public static Brush GetBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(BorderBrushProperty);
        }

        public static void SetBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(BorderBrushProperty, value);
        }

        public static double GetMaxBlurRadius(DependencyObject obj)
        {
            return (double)obj.GetValue(MaxBlurRadiusProperty);
        }

        public static void SetMaxBlurRadius(DependencyObject obj, double value)
        {
            obj.SetValue(MaxBlurRadiusProperty, value);
        }

        public static double GetRelativeSpotlightSize(DependencyObject obj)
        {
            return (double)obj.GetValue(RelativeSpotlightSizeProperty);
        }

        public static void SetRelativeSpotlightSize(DependencyObject obj, double value)
        {
            obj.SetValue(RelativeSpotlightSizeProperty, value);
        }

        public static readonly DependencyProperty MouseEventSourceProperty = DependencyProperty.RegisterAttached("MouseEventSource", typeof(FrameworkElement), typeof(CursorSpotlightExtension), new PropertyMetadata(null, MouseEventTargetPropertyChanged));

        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.RegisterAttached("BackgroundBrush", typeof(Brush), typeof(CursorSpotlightExtension), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(CursorSpotlightExtension), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MaxBlurRadiusProperty = DependencyProperty.RegisterAttached("MaxBlurRadius", typeof(double), typeof(CursorSpotlightExtension), new FrameworkPropertyMetadata(double.MaxValue, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty RelativeSpotlightSizeProperty = DependencyProperty.RegisterAttached("RelativeSpotlightSize", typeof(double), typeof(CursorSpotlightExtension), new FrameworkPropertyMetadata(0.8, FrameworkPropertyMetadataOptions.Inherits));

        private static void MouseEventTargetPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.OldValue != null)
                ClearSpotlight(obj as FrameworkElement, args.OldValue as FrameworkElement);

            if (args.NewValue != null)
                InitSpotlight(obj as FrameworkElement, args.NewValue as FrameworkElement);
        }

        private static void InitSpotlight(FrameworkElement spotlightTarget, FrameworkElement mouseEventSource)
        {
            // binding required to enable bindings to spotlightTarget's size in visual brush
            // see https://social.msdn.microsoft.com/Forums/vstudio/en-US/21580413-6f42-429c-b9e0-17331bae87cc/binding-width-and-height-of-a-border-in-a-visualbrush-resource?forum=wpf
            BindingOperations.SetBinding(spotlightTarget, FrameworkElement.DataContextProperty, new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
            });

            var opacityMask = new VisualBrush();
            var canvas = new Canvas();

            BindingOperations.SetBinding(canvas, FrameworkElement.WidthProperty, new Binding("ActualWidth"));
            BindingOperations.SetBinding(canvas, FrameworkElement.HeightProperty, new Binding("ActualHeight"));

            canvas.ClipToBounds = true;
            canvas.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            opacityMask.Visual = canvas;
            spotlightTarget.OpacityMask = opacityMask;

            mouseEventSource.MouseEnter += TargetElementOnMouseEnter(spotlightTarget);
            mouseEventSource.PreviewMouseMove += TargetElementOnMouseMove(spotlightTarget);
            mouseEventSource.MouseLeave += TargetElementOnMouseLeave(spotlightTarget);
        }

        private static void ClearSpotlight(FrameworkElement spotlightTarget, FrameworkElement mouseEventSource)
        {
            spotlightTarget.OpacityMask = null;

            mouseEventSource.MouseEnter -= TargetElementOnMouseEnter(spotlightTarget);
            mouseEventSource.PreviewMouseMove -= TargetElementOnMouseMove(spotlightTarget);
            mouseEventSource.MouseLeave -= TargetElementOnMouseLeave(spotlightTarget);
        }

        private static MouseEventHandler TargetElementOnMouseEnter(FrameworkElement spotlightTarget)
        {
            return (sender, args) => {
                if (!((spotlightTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                canvas.Background = CreateSpotlight(spotlightTarget);
            };
        }

        private static MouseEventHandler TargetElementOnMouseMove(FrameworkElement spotlightTarget)
        {
            return (sender, args) =>
            {
                if (!((spotlightTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                if (!(canvas.Background is RadialGradientBrush spotlight))
                {
                    spotlight = CreateSpotlight(spotlightTarget);
                    canvas.Background = spotlight;
                }

                Point cursorLocation = args.MouseDevice.GetPosition(spotlightTarget);
                Point relativeCursorLocation = new Point(cursorLocation.X / canvas.ActualWidth, cursorLocation.Y / canvas.ActualHeight);

                spotlight.Center = relativeCursorLocation;
                spotlight.GradientOrigin = relativeCursorLocation;
            };
        }

        private static MouseEventHandler TargetElementOnMouseLeave(FrameworkElement spotlightTarget)
        {
            return (sender, args) =>
            {
                if (!((spotlightTarget.OpacityMask as VisualBrush)?.Visual is Canvas canvas))
                    return;

                canvas.Background = null;
            };
        }

        private static RadialGradientBrush CreateSpotlight(FrameworkElement targetElement)
        {
            double maxSize = Math.Max(targetElement.ActualWidth, targetElement.ActualHeight);
            double relativeWidth = maxSize / targetElement.ActualWidth;
            double relativeHeight = maxSize / targetElement.ActualHeight;

            double relativeSpotlightSize = GetRelativeSpotlightSize(targetElement);
            double blurRadius = Math.Min(relativeSpotlightSize * 0.75, GetMaxBlurRadius(targetElement) / maxSize);

            return new RadialGradientBrush
            {
                RadiusX = relativeWidth,
                RadiusY = relativeHeight,
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb(255, 0, 0, 0), 0),
                    new GradientStop(Color.FromArgb(255, 0, 0, 0), relativeSpotlightSize - blurRadius / 2),
                    new GradientStop(Color.FromArgb(0, 0, 0, 0), relativeSpotlightSize + blurRadius / 2),
                },
            };
        }
    }
}
