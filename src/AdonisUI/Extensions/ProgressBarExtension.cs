using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace AdonisUI.Extensions
{
    public class ProgressBarExtension
    {
        public static object GetContent(DependencyObject obj)
        {
            return (object)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, object value)
        {
            obj.SetValue(ContentProperty, value);
        }

        public static DataTemplate GetContentTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(ContentTemplateProperty);
        }

        public static void SetContentTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(ContentTemplateProperty, value);
        }

        public static Brush GetForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ForegroundProperty);
        }

        public static void SetForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ForegroundProperty, value);
        }

        public static Brush GetProgressAnimationForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ProgressAnimationForegroundProperty);
        }

        public static void SetProgressAnimationForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ProgressAnimationForegroundProperty, value);
        }

        public static bool GetIsProgressAnimationEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsProgressAnimationEnabledProperty);
        }

        public static void SetIsProgressAnimationEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsProgressAnimationEnabledProperty, value);
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(object), typeof(ProgressBarExtension), new PropertyMetadata(null));

        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.RegisterAttached("ContentTemplate", typeof(DataTemplate), typeof(ProgressBarExtension), new PropertyMetadata(null));

        public static readonly DependencyProperty ProgressAnimationForegroundProperty = DependencyProperty.RegisterAttached("ProgressAnimationForeground", typeof(Brush), typeof(ProgressBarExtension), new PropertyMetadata(System.Windows.Media.Brushes.Black));

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(ProgressBarExtension), new PropertyMetadata(System.Windows.Media.Brushes.Black));

        public static readonly DependencyProperty IsProgressAnimationEnabledProperty = DependencyProperty.RegisterAttached("IsProgressAnimationEnabled", typeof(bool), typeof(ProgressBarExtension), new PropertyMetadata(false));
    }
}
