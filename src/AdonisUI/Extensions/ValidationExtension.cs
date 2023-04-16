using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AdonisUI.Controls;

namespace AdonisUI.Extensions
{
    public class ValidationExtension
    {
        public static bool GetIsErrorMessageVisibleOnFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsErrorMessageVisibleOnFocusProperty);
        }

        public static void SetIsErrorMessageVisibleOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(IsErrorMessageVisibleOnFocusProperty, value);
        }

        public static bool GetIsErrorMessageVisibleOnMouseOver(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsErrorMessageVisibleOnMouseOverProperty);
        }

        public static void SetIsErrorMessageVisibleOnMouseOver(DependencyObject obj, bool value)
        {
            obj.SetValue(IsErrorMessageVisibleOnMouseOverProperty, value);
        }

        public static ValidationErrorIndicatorPlacement GetErrorMessagePlacement(DependencyObject obj)
        {
            return (ValidationErrorIndicatorPlacement)obj.GetValue(ErrorMessagePlacementProperty);
        }

        public static void SetErrorMessagePlacement(DependencyObject obj, ValidationErrorIndicatorPlacement value)
        {
            obj.SetValue(ErrorMessagePlacementProperty, value);
        }

        public static readonly DependencyProperty IsErrorMessageVisibleOnFocusProperty = DependencyProperty.RegisterAttached("IsErrorMessageVisibleOnFocus", typeof(bool), typeof(ValidationExtension), new PropertyMetadata(true));

        public static readonly DependencyProperty IsErrorMessageVisibleOnMouseOverProperty = DependencyProperty.RegisterAttached("IsErrorMessageVisibleOnMouseOver", typeof(bool), typeof(ValidationExtension), new PropertyMetadata(true));

        public static readonly DependencyProperty ErrorMessagePlacementProperty = DependencyProperty.RegisterAttached("ErrorMessagePlacement", typeof(ValidationErrorIndicatorPlacement), typeof(ValidationExtension), new PropertyMetadata(ValidationErrorIndicatorPlacement.Top));
    }
}
