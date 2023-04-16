using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AdonisUI.Controls
{
    public class ValidationErrorIndicator
        : Control
    {
        static ValidationErrorIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValidationErrorIndicator), new FrameworkPropertyMetadata(typeof(ValidationErrorIndicator)));
        }

        public FrameworkElement ValidatedElement
        {
            get { return (FrameworkElement)GetValue(ValidatedElementProperty); }
            set { SetValue(ValidatedElementProperty, value); }
        }

        public bool IsValidatedElementFocused
        {
            get { return (bool)GetValue(IsValidatedElementFocusedProperty); }
            set { SetValue(IsValidatedElementFocusedProperty, value); }
        }

        public bool IsErrorMessageVisibleOnFocus
        {
            get { return (bool)GetValue(IsErrorMessageVisibleOnFocusProperty); }
            internal set { SetValue(IsErrorMessageVisibleOnFocusProperty, value); }
        }

        public bool IsErrorMessageVisibleOnMouseOver
        {
            get { return (bool)GetValue(IsErrorMessageVisibleOnMouseOverProperty); }
            internal set { SetValue(IsErrorMessageVisibleOnMouseOverProperty, value); }
        }

        public bool IsErrorMessageDisplayOnFocusEnabled
        {
            get { return (bool)GetValue(IsErrorMessageDisplayOnFocusEnabledProperty); }
            set { SetValue(IsErrorMessageDisplayOnFocusEnabledProperty, value); }
        }

        public bool IsErrorMessageDisplayOnMouseOverEnabled
        {
            get { return (bool)GetValue(IsErrorMessageDisplayOnMouseOverEnabledProperty); }
            set { SetValue(IsErrorMessageDisplayOnMouseOverEnabledProperty, value); }
        }

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public ValidationErrorIndicatorPlacement ErrorMessagePlacement
        {
            get { return (ValidationErrorIndicatorPlacement)GetValue(ErrorMessagePlacementProperty); }
            set { SetValue(ErrorMessagePlacementProperty, value); }
        }

        public static readonly DependencyProperty ValidatedElementProperty = DependencyProperty.Register("ValidatedElement", typeof(FrameworkElement), typeof(ValidationErrorIndicator), new PropertyMetadata(null));

        public static readonly DependencyProperty IsValidatedElementFocusedProperty = DependencyProperty.Register("IsValidatedElementFocused", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(false));

        internal static readonly DependencyProperty IsErrorMessageVisibleOnFocusProperty = DependencyProperty.Register("IsErrorMessageVisibleOnFocus", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(true));

        internal static readonly DependencyProperty IsErrorMessageVisibleOnMouseOverProperty = DependencyProperty.Register("IsErrorMessageVisibleOnMouseOver", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(true));

        public static readonly DependencyProperty IsErrorMessageDisplayOnFocusEnabledProperty = DependencyProperty.Register("IsErrorMessageDisplayOnFocusEnabled", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(true));

        public static readonly DependencyProperty IsErrorMessageDisplayOnMouseOverEnabledProperty = DependencyProperty.Register("IsErrorMessageDisplayOnMouseOverEnabled", typeof(bool), typeof(ValidationErrorIndicator), new PropertyMetadata(true));

        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(ValidationErrorIndicator), new PropertyMetadata(20.0));

        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(ValidationErrorIndicator), new PropertyMetadata(20.0));

        public static readonly DependencyProperty ErrorMessagePlacementProperty = DependencyProperty.Register("ErrorMessagePlacement", typeof(ValidationErrorIndicatorPlacement), typeof(ValidationErrorIndicator), new PropertyMetadata(ValidationErrorIndicatorPlacement.Top));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ToggleButton alertToggleButton = GetTemplateChild("AlertToggleButton") as ToggleButton;
            alertToggleButton.Unchecked += AlertToggleButton_OnUnchecked;

            FrameworkElement adornerContent = GetTemplateChild("AdornerContent") as FrameworkElement;
            adornerContent.MouseDown += (s, args) => IsErrorMessageVisibleOnFocus = false;
        }

        private void AlertToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            bool wasErrorMessageVisibleValue = IsErrorMessageVisibleOnMouseOver;

            IsErrorMessageVisibleOnMouseOver = false;

            (sender as ToggleButton).Checked += AlertToggleButton_OnChecked(wasErrorMessageVisibleValue);
            (sender as ToggleButton).MouseLeave += AlertToggleButton_OnMouseLeave(wasErrorMessageVisibleValue);
        }

        private MouseEventHandler AlertToggleButton_OnMouseLeave(bool wasErrorMessageVisibleValue)
        {
            return (s, args) => ResetErrorMessageVisibility(s as ToggleButton, wasErrorMessageVisibleValue);
        }

        private RoutedEventHandler AlertToggleButton_OnChecked(bool wasErrorMessageVisibleValue)
        {
            return (s, args) => ResetErrorMessageVisibility(s as ToggleButton, wasErrorMessageVisibleValue);
        }

        private void ResetErrorMessageVisibility(ToggleButton alertToggleButton, bool wasErrorMessageVisibleValue)
        {
            IsErrorMessageVisibleOnMouseOver = wasErrorMessageVisibleValue;

            (alertToggleButton as ToggleButton).Unchecked -= AlertToggleButton_OnChecked(wasErrorMessageVisibleValue);
            (alertToggleButton as ToggleButton).MouseLeave -= AlertToggleButton_OnMouseLeave(wasErrorMessageVisibleValue);
        }
    }
}
