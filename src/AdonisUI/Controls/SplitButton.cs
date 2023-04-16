using AdonisUI.Helpers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace AdonisUI.Controls
{
    [TemplatePart(Name = "PART_MenuExpander", Type = typeof(Button))]
    public class SplitButton : Button
    {
        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
        }

        public static readonly DependencyProperty SplitMenuProperty = DependencyProperty.Register("SplitMenu", typeof(ContextMenu), typeof(SplitButton), new PropertyMetadata(null));

        public ContextMenu SplitMenu
        {
            get => (ContextMenu)GetValue(SplitMenuProperty);
            set => SetValue(SplitMenuProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PART_MenuExpander") is Button menuExpanderButton)
                menuExpanderButton.Click += OnMenuExpanderClick;
        }

        private void OnMenuExpanderClick(object sender, RoutedEventArgs e)
        {
            OpenSplitMenu();
        }

        private void OpenSplitMenu()
        {
            if (SplitMenu == null)
                return;

            SplitMenu.IsEnabled = true;
            SplitMenu.PlacementTarget = this;
            SplitMenu.Placement = PlacementMode.Bottom;
            SplitMenu.IsOpen = true;
            SplitMenu.Closed += SplitMenu_Closed;
        }

        private void SplitMenu_Closed(object sender, RoutedEventArgs e)
        {
            ResetRippleEffects((FrameworkElement)sender);
        }

        private void ResetRippleEffects(FrameworkElement rootElement)
        {
            foreach (RippleHost rippleHost in UINavigator.FindVisualChildren<RippleHost>(rootElement))
            {
                rippleHost.Reset();
            }
        }
    }
}
