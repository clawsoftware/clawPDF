using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using AdonisUI.Controls;
using AdonisUI.Converters;

namespace AdonisUI.Extensions
{
    public static class WatermarkExtension
    {
        public static object GetWatermark(DependencyObject d)
        {
            return (object)d.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject d, object value)
        {
            d.SetValue(WatermarkProperty, value);
        }

        public static bool GetIsWatermarkVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsWatermarkVisibleProperty);
        }

        public static void SetIsWatermarkVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWatermarkVisibleProperty, value);
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached("Watermark", typeof(object), typeof(WatermarkExtension), new PropertyMetadata(null, OnWatermarkChanged));

        public static readonly DependencyProperty IsWatermarkVisibleProperty = DependencyProperty.RegisterAttached("IsWatermarkVisible", typeof(bool), typeof(WatermarkExtension), new PropertyMetadata(false));

        private static void OnWatermarkChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is TextBox textBox)
            {
                UpdateIsWatermarkVisible(textBox);

                Binding textBinding = new Binding
                {
                    Path = new PropertyPath("Text"),
                    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                    Converter = new StringIsNullOrEmptyToBoolConverter(),
                };

                BindingOperations.SetBinding(textBox, IsWatermarkVisibleProperty, textBinding);
            }
            else if (obj is ComboBox comboBox)
            {
                UpdateIsWatermarkVisible(comboBox);

                Binding textBinding = new Binding
                {
                    Path = new PropertyPath("Text"),
                    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                    Converter = new StringIsNullOrEmptyToBoolConverter(),
                };

                BindingOperations.SetBinding(comboBox, IsWatermarkVisibleProperty, textBinding);
            }
            else if (obj is PasswordBox passwordBox)
            {
                UpdateIsWatermarkVisible(passwordBox);

                passwordBox.PasswordChanged -= OnPasswordBoxPasswordChanged;
                passwordBox.PasswordChanged += OnPasswordBoxPasswordChanged;
                passwordBox.Unloaded -= OnPasswordBoxUnloaded;
                passwordBox.Unloaded += OnPasswordBoxUnloaded;
            }
            else if (obj is DatePicker datePicker)
            {
                UpdateIsWatermarkVisible(datePicker);

                Binding textBinding = new Binding
                {
                    Path = new PropertyPath("Text"),
                    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                    Converter = new StringIsNullOrEmptyToBoolConverter(),
                };

                BindingOperations.SetBinding(datePicker, IsWatermarkVisibleProperty, textBinding);
            }
        }

        private static void UpdateIsWatermarkVisible(TextBox textBox)
        {
            SetIsWatermarkVisible(textBox, String.IsNullOrEmpty(textBox.Text));
        }

        private static void UpdateIsWatermarkVisible(ComboBox comboBox)
        {
            SetIsWatermarkVisible(comboBox, String.IsNullOrEmpty(comboBox.Text));
        }

        private static void UpdateIsWatermarkVisible(PasswordBox passwordBox)
        {
            SetIsWatermarkVisible(passwordBox, String.IsNullOrEmpty(passwordBox.Password));
        }

        private static void UpdateIsWatermarkVisible(DatePicker datePicker)
        {
            SetIsWatermarkVisible(datePicker, String.IsNullOrEmpty(datePicker.Text));
        }

        private static void OnTextBoxTextChanged(object sender, RoutedEventArgs e)
        {
            UpdateIsWatermarkVisible(sender as TextBox);
        }

        private static void OnComboBoxTextChanged(object sender, RoutedEventArgs e)
        {
            UpdateIsWatermarkVisible(sender as ComboBox);
        }

        private static void OnPasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdateIsWatermarkVisible(sender as PasswordBox);
        }

        private static void OnPasswordBoxUnloaded(object sender, RoutedEventArgs e)
        {
            ((PasswordBox) sender).PasswordChanged -= OnPasswordBoxPasswordChanged;
        }
    }
}
