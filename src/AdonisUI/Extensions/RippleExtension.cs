using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace AdonisUI.Extensions
{
    public class RippleExtension
    {
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

        public static Brush GetForegroundBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ForegroundBrushProperty);
        }

        public static void SetForegroundBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(ForegroundBrushProperty, value);
        }

        public static TimeSpan GetFadeInDuration(DependencyObject obj)
        {
            return (TimeSpan)obj.GetValue(FadeInDurationProperty);
        }

        public static void SetFadeInDuration(DependencyObject obj, TimeSpan value)
        {
            obj.SetValue(FadeInDurationProperty, value);
        }

        public static TimeSpan GetFadeOutDuration(DependencyObject obj)
        {
            return (TimeSpan)obj.GetValue(FadeOutDurationProperty);
        }

        public static void SetFadeOutDuration(DependencyObject obj, TimeSpan value)
        {
            obj.SetValue(FadeOutDurationProperty, value);
        }

        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.RegisterAttached("BackgroundBrush", typeof(Brush), typeof(RippleExtension), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(RippleExtension), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ForegroundBrushProperty = DependencyProperty.RegisterAttached("ForegroundBrush", typeof(Brush), typeof(RippleExtension), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.Transparent, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FadeInDurationProperty = DependencyProperty.RegisterAttached("FadeInDuration", typeof(TimeSpan), typeof(RippleExtension), new FrameworkPropertyMetadata(TimeSpan.FromMilliseconds(200), FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FadeOutDurationProperty = DependencyProperty.RegisterAttached("FadeOutDuration", typeof(TimeSpan), typeof(RippleExtension), new FrameworkPropertyMetadata(TimeSpan.FromMilliseconds(200), FrameworkPropertyMetadataOptions.Inherits));
    }
}
