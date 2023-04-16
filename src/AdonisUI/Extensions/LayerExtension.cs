using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AdonisUI.Extensions
{
    public class LayerExtension
    {
        public static int? GetLayer(DependencyObject obj)
        {
            return (int?)obj.GetValue(LayerProperty);
        }

        public static void SetLayer(DependencyObject obj, int? value)
        {
            obj.SetValue(LayerProperty, value);
        }

        public static bool GetIncreaseLayer(DependencyObject obj)
        {
            return (bool)obj.GetValue(IncreaseLayerProperty);
        }

        public static void SetIncreaseLayer(DependencyObject obj, bool value)
        {
            obj.SetValue(IncreaseLayerProperty, value);
        }

        public static int GetComputedLayer(DependencyObject obj)
        {
            return (int)obj.GetValue(ComputedLayerProperty);
        }

        private static void SetComputedLayer(DependencyObject obj, int value)
        {
            obj.SetValue(ComputedLayerPropertyKey, value);
        }

        public static readonly DependencyProperty LayerProperty = DependencyProperty.RegisterAttached("Layer", typeof(int?), typeof(LayerExtension), new PropertyMetadata(null, OnLayerPropertyChanged));

        public static readonly DependencyProperty IncreaseLayerProperty = DependencyProperty.RegisterAttached("IncreaseLayer", typeof(bool), typeof(LayerExtension), new PropertyMetadata(false, OnIncreaseLayerPropertyChanged));

        private static readonly DependencyPropertyKey ComputedLayerPropertyKey = DependencyProperty.RegisterAttachedReadOnly("ComputedLayer", typeof(int), typeof(LayerExtension), new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ComputedLayerProperty = ComputedLayerPropertyKey.DependencyProperty;

        private static void OnLayerPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (eventArgs.NewValue == null)
                return;

            SetComputedLayer(depObj, (int)eventArgs.NewValue);

            int increasedLayer = (int)eventArgs.NewValue + 1;

            if (!(depObj is FrameworkElement targetElement))
                return;

            if (targetElement.IsLoaded)
                SetComputedLayerOfChildren(targetElement, increasedLayer);
            else
                targetElement.Loaded += (sender, args) => SetComputedLayerOfChildren(targetElement, increasedLayer);
        }

        private static void OnIncreaseLayerPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(depObj is FrameworkElement targetElement))
                return;

            if (targetElement.IsLoaded)
                SetComputedLayerOfChildren(targetElement, GetComputedLayer(targetElement) + 1);
            else
                targetElement.Loaded += (sender, args) => SetComputedLayerOfChildren(targetElement, GetComputedLayer(targetElement) + 1);
        }

        private static void SetComputedLayerOfChildren(FrameworkElement element, int value)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(element))
            {
                if (!(child is DependencyObject childObject))
                    continue;

                if (GetLayer(childObject) == null)
                    SetComputedLayer(childObject, value);
            }
        }
    }
}
