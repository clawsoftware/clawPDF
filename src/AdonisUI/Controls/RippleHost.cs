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
using System.Windows.Shapes;

namespace AdonisUI.Controls
{
    /// <summary>
    /// A control offering elliptical fade-in and fade-out animations for its content on mouse down.
    /// </summary>
    [TemplatePart(Name = "PART_ContentHost", Type = typeof(FrameworkElement))]
    public class RippleHost : ContentControl
    {
        private const string PART_ContentHost = "PART_ContentHost";

        public FrameworkElement ContentHost { get; protected set; }

        static RippleHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleHost), new FrameworkPropertyMetadata(typeof(RippleHost)));
        }

        public FrameworkElement MouseEventSource
        {
            get => (FrameworkElement)GetValue(MouseEventSourceProperty);
            set => SetValue(MouseEventSourceProperty, value);
        }

        public TimeSpan FadeInDuration
        {
            get => (TimeSpan)GetValue(FadeInDurationProperty);
            set => SetValue(FadeInDurationProperty, value);
        }

        public TimeSpan FadeOutDuration
        {
            get => (TimeSpan)GetValue(FadeOutDurationProperty);
            set => SetValue(FadeOutDurationProperty, value);
        }

        private Timeline AnimationToComplete { get; set; }

        private static bool GetIsAnimationComplete(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAnimationCompleteProperty);
        }

        private static void SetIsAnimationComplete(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAnimationCompleteProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly DependencyProperty MouseEventSourceProperty = DependencyProperty.Register("MouseEventSource", typeof(FrameworkElement), typeof(RippleHost), new FrameworkPropertyMetadata(MouseEventSourcePropertyChanged, null));

        public static readonly DependencyProperty FadeInDurationProperty = DependencyProperty.Register("FadeInDuration", typeof(TimeSpan), typeof(RippleHost), new PropertyMetadata(TimeSpan.FromMilliseconds(200)));

        public static readonly DependencyProperty FadeOutDurationProperty = DependencyProperty.Register("FadeOutDuration", typeof(TimeSpan), typeof(RippleHost), new PropertyMetadata(TimeSpan.FromMilliseconds(200)));

        private static readonly DependencyProperty IsAnimationCompleteProperty = DependencyProperty.RegisterAttached("IsAnimationComplete", typeof(bool), typeof(RippleHost), new PropertyMetadata(false));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RippleHost), new PropertyMetadata(new CornerRadius(0)));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ContentHost = GetTemplateChild(PART_ContentHost) as FrameworkElement;

            FrameworkElement mouseEventSource = MouseEventSource ?? this;

            InitRippleLayer(mouseEventSource);
        }

        private static void MouseEventSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
                ((RippleHost)d).ClearRippleLayer(e.OldValue as FrameworkElement);

            if (e.NewValue != null)
                ((RippleHost)d).InitRippleLayer(e.NewValue as FrameworkElement);
        }

        private void InitRippleLayer(FrameworkElement clickEventSource)
        {
            if (ContentHost != null)
            {
                BindDataContext(ContentHost);
            }

            InitRippleOpacityMask();

            clickEventSource.PreviewMouseLeftButtonDown += MouseEventSourceOnMouseDown();
            clickEventSource.PreviewMouseLeftButtonUp += MouseEventSourceOnMouseUp();

            Window parentWindow = Window.GetWindow(clickEventSource);
            if (parentWindow != null)
            {
                parentWindow.PreviewMouseLeftButtonUp += MouseEventSourceOnMouseUp();
                parentWindow.Deactivated += ParentWindowOnDeactivated;
            }

            IsVisibleChanged += (s, e) => Reset();
        }

        private void BindDataContext(DependencyObject elementToInheritDataContext)
        {
            BindingOperations.SetBinding(elementToInheritDataContext, FrameworkElement.DataContextProperty, new Binding
            {
                Path = new PropertyPath("DataContext"),
                Source = LogicalTreeHelper.GetParent(this),
                Mode = BindingMode.OneWay,
            });
        }

        private void InitRippleOpacityMask()
        {
            // binding required to enable bindings to target's size in visual brush
            // see https://social.msdn.microsoft.com/Forums/vstudio/en-US/21580413-6f42-429c-b9e0-17331bae87cc/binding-width-and-height-of-a-border-in-a-visualbrush-resource?forum=wpf
            BindingOperations.SetBinding(this, FrameworkElement.DataContextProperty, new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
            });

            var opacityMask = new VisualBrush();
            var rippleContainer = new Canvas();

            BindingOperations.SetBinding(rippleContainer, FrameworkElement.WidthProperty, new Binding("ActualWidth"));
            BindingOperations.SetBinding(rippleContainer, FrameworkElement.HeightProperty, new Binding("ActualHeight"));

            rippleContainer.ClipToBounds = true;
            rippleContainer.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            rippleContainer.Opacity = 0.25;

            opacityMask.Visual = rippleContainer;
            OpacityMask = opacityMask;
        }

        private void ClearRippleLayer(FrameworkElement clickEventSource)
        {
            OpacityMask = null;

            clickEventSource.PreviewMouseLeftButtonDown -= MouseEventSourceOnMouseDown();
            clickEventSource.PreviewMouseLeftButtonUp -= MouseEventSourceOnMouseUp();

            Window parentWindow = Window.GetWindow(clickEventSource);
            if (parentWindow != null)
            {
                parentWindow.PreviewMouseLeftButtonUp -= MouseEventSourceOnMouseUp();
                parentWindow.Deactivated -= ParentWindowOnDeactivated;
            }
        }

        private MouseButtonEventHandler MouseEventSourceOnMouseDown()
        {
            return (sender, args) => StartRipple(args.MouseDevice.GetPosition(this));
        }

        private void StartRipple(Point center)
        {
            if (!((OpacityMask as VisualBrush)?.Visual is Panel rippleContainer))
                return;

            if (!(rippleContainer.Background is RadialGradientBrush ripple))
            {
                ripple = CreateRipple(center);
                rippleContainer.Background = ripple;
            }

            Point relativeCenter = new Point(center.X / rippleContainer.ActualWidth, center.Y / rippleContainer.ActualHeight);

            ripple.Center = relativeCenter;
            ripple.GradientOrigin = relativeCenter;

            DoubleAnimation widthAnimation = CreateExpansionAnimation();
            DoubleAnimation heightAnimation = CreateExpansionAnimation();
            DoubleAnimation opacityAnimation = CreateFadeInAnimation();

            ScaleTransform rippleScaleTransform = (ScaleTransform)ripple.RelativeTransform;

            AnimationToComplete = widthAnimation;
            widthAnimation.Completed += (s, e) => SetIsAnimationComplete(widthAnimation, true);

            rippleScaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, widthAnimation);
            rippleScaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, heightAnimation);
            rippleContainer.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }

        private MouseButtonEventHandler MouseEventSourceOnMouseUp()
        {
            return (sender, args) => EndRipple();
        }

        private void ParentWindowOnDeactivated(object sender, EventArgs args)
        {
            EndRipple();
        }

        private void EndRipple()
        {
            if (!((OpacityMask as VisualBrush)?.Visual is Panel rippleContainer))
                return;

            Brush ripple = rippleContainer.Background;

            if (ripple == null)
                return;

            Duration animationDuration = new Duration(FadeOutDuration);
            DoubleAnimation removeAnimation = new DoubleAnimation(0, animationDuration);
            removeAnimation.Completed += (s, e) => rippleContainer.Background = null;

            if (AnimationToComplete == null || GetIsAnimationComplete(AnimationToComplete))
                rippleContainer.BeginAnimation(UIElement.OpacityProperty, removeAnimation);
            else
                AnimationToComplete.Completed += (s, e) => rippleContainer.BeginAnimation(UIElement.OpacityProperty, removeAnimation);
        }

        private RadialGradientBrush CreateRipple(Point center)
        {
            double distanceToTopLeftCoordinate = Point.Subtract(new Point(0, 0), center).Length;
            double distanceToTopRightCoordinate = Point.Subtract(new Point(ActualWidth, 0), center).Length;
            double distanceToButtomLeftCoordinate = Point.Subtract(new Point(0, ActualHeight), center).Length;
            double distanceToBottomRightCoordinate = Point.Subtract(new Point(ActualWidth, ActualHeight), center).Length;

            double maxSize = Math.Max(Math.Max(Math.Max(distanceToTopLeftCoordinate, distanceToTopRightCoordinate), distanceToButtomLeftCoordinate), distanceToBottomRightCoordinate);
            double relativeWidth = maxSize / ActualWidth;
            double relativeHeight = maxSize / ActualHeight;
            Point relativeCenter = new Point(center.X / ActualWidth, center.Y / ActualHeight);

            return new RadialGradientBrush
            {
                RadiusX = relativeWidth,
                RadiusY = relativeHeight,
                GradientOrigin = relativeCenter,
                Center = relativeCenter,
                RelativeTransform = new ScaleTransform(0, 0, relativeCenter.X, relativeCenter.Y),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb(255, 0, 0, 0), 0),
                    new GradientStop(Color.FromArgb(255, 0, 0, 0), 1),
                    new GradientStop(Color.FromArgb(0, 0, 0, 0), 1),
                },
            };
        }

        private DoubleAnimation CreateExpansionAnimation()
        {
            Duration animationDuration = new Duration(FadeInDuration);

            DoubleAnimation sizeAnimation = new DoubleAnimation(1, animationDuration)
            {
                EasingFunction = new CircleEase
                {
                    EasingMode = EasingMode.EaseInOut,
                },
            };

            Timeline.SetDesiredFrameRate(sizeAnimation, 60);

            return sizeAnimation;
        }

        private DoubleAnimation CreateFadeInAnimation()
        {
            Duration animationDuration = new Duration(new TimeSpan((long)(FadeInDuration.Ticks * 0.5)));

            DoubleAnimation sizeAnimation = new DoubleAnimation(1, animationDuration)
            {
                EasingFunction = new CircleEase
                {
                    EasingMode = EasingMode.EaseInOut,
                },
            };

            Timeline.SetDesiredFrameRate(sizeAnimation, 60);

            return sizeAnimation;
        }

        public void Reset()
        {
            if (!((OpacityMask as VisualBrush)?.Visual is Panel rippleContainer))
                return;

            rippleContainer.Background = null;
        }
    }
}
