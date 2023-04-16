using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AdonisUI.Helpers;
using Brush = System.Windows.Media.Brush;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Window with custom chrome supporting theming of non-client areas
    /// </summary>
    [TemplatePart(Name = PART_DragMoveThumb, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_IconPresenter, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PART_MinimizeButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_MaximizeRestoreButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    public class AdonisWindow
        : Window
    {
        private const string PART_DragMoveThumb = "PART_DragMoveThumb";
        private const string PART_IconPresenter = "PART_IconPresenter";
        private const string PART_MinimizeButton = "PART_MinimizeButton";
        private const string PART_MaximizeRestoreButton = "PART_MaximizeRestoreButton";
        private const string PART_CloseButton = "PART_CloseButton";

        protected HwndInterop HwndInterop { get; private set; }

        public FrameworkElement DragMoveThumb { get; protected set; }

        public FrameworkElement IconPresenter { get; protected set; }

        public Button MinimizeButton { get; protected set; }

        public Button MaximizeRestoreButton { get; protected set; }

        public Button CloseButton { get; protected set; }

        /// <summary>
        /// Gets or sets the visibility of the icon component of the window.
        /// </summary>
        public Visibility IconVisibility
        {
            get => (Visibility)GetValue(IconVisibilityProperty);
            set => SetValue(IconVisibilityProperty, value);
        }

        /// <summary>
        /// Gets or sets the window's icon as <see cref="ImageSource">ImageSource</see>.
        /// When the <see cref="Window.IconProperty">IconProperty</see> property changes, this is updated accordingly.
        /// </summary>
        protected internal ImageSource IconSource
        {
            get => (ImageSource)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        /// <summary>
        /// Gets the title bar actual height.
        /// </summary>
        public double TitleBarActualHeight
        {
            get => (double)GetValue(TitleBarActualHeightProperty);
            private set => SetValue(TitleBarActualHeightPropertyKey, value);
        }

        /// <summary>
        /// Gets or sets the content of the window's title bar
        /// between the title and the window buttons.
        /// </summary>
        public object TitleBarContent
        {
            get => GetValue(TitleBarContentProperty);
            set => SetValue(TitleBarContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the foreground brush of the window's title bar.
        /// </summary>
        public Brush TitleBarForeground
        {
            get => (Brush)GetValue(TitleBarForegroundProperty);
            set => SetValue(TitleBarForegroundProperty, value);
        }

        /// <summary>
        /// Gets or sets the background brush of the window's title bar.
        /// </summary>
        public Brush TitleBarBackground
        {
            get => (Brush)GetValue(TitleBarBackgroundProperty);
            set => SetValue(TitleBarBackgroundProperty, value);
        }

        /// <summary>
        /// Gets or sets the visibility of the title component of the window.
        /// </summary>
        public Visibility TitleVisibility
        {
            get => (Visibility)GetValue(TitleVisibilityProperty);
            set => SetValue(TitleVisibilityProperty, value);
        }

        /// <summary>
        /// Gets or sets the background brush of the minimize, maximize and restore
        /// buttons when they are hovered.
        /// </summary>
        public Brush WindowButtonHighlightBrush
        {
            get => (Brush)GetValue(WindowButtonHighlightBrushProperty);
            set => SetValue(WindowButtonHighlightBrushProperty, value);
        }

        /// <summary>
        /// Gets the size of the display overlapping area when the window is maximized.
        /// </summary>
        protected internal Thickness MaximizeBorderThickness
        {
            get => (Thickness)GetValue(MaximizeBorderThicknessProperty);
            private set => SetValue(MaximizeBorderThicknessPropertyKey, value);
        }

        /// <summary>
        /// Controls whether to shrink the title bar height a little when the window is maximized.
        /// The default is <see langword="true"/> as this is how native windows behave.
        /// </summary>
        public bool ShrinkTitleBarWhenMaximized
        {
            get => (bool)GetValue(ShrinkTitleBarWhenMaximizedProperty);
            set => SetValue(ShrinkTitleBarWhenMaximizedProperty, value);
        }

        /// <summary>
        /// Controls whether the title bar should be drawn over the window content instead of being stacked on top of it.
        /// </summary>
        public bool PlaceTitleBarOverContent
        {
            get => (bool)GetValue(PlaceTitleBarOverContentProperty);
            set => SetValue(PlaceTitleBarOverContentProperty, value);
        }

        public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(AdonisWindow), new PropertyMetadata(Visibility.Visible));

        protected internal static readonly DependencyProperty IconSourceProperty = DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(AdonisWindow), new PropertyMetadata(null));

        protected internal static readonly DependencyPropertyKey TitleBarActualHeightPropertyKey = DependencyProperty.RegisterReadOnly("TitleBarActualHeight", typeof(double), typeof(AdonisWindow), new PropertyMetadata(0.0d));

        protected internal static readonly DependencyProperty TitleBarActualHeightProperty = TitleBarActualHeightPropertyKey.DependencyProperty;

        public static readonly DependencyProperty TitleBarContentProperty = DependencyProperty.Register("TitleBarContent", typeof(object), typeof(AdonisWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarForegroundProperty = DependencyProperty.Register("TitleBarForeground", typeof(Brush), typeof(AdonisWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleBarBackgroundProperty = DependencyProperty.Register("TitleBarBackground", typeof(Brush), typeof(AdonisWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleVisibilityProperty = DependencyProperty.Register("TitleVisibility", typeof(Visibility), typeof(AdonisWindow), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty WindowButtonHighlightBrushProperty = DependencyProperty.Register("WindowButtonHighlightBrush", typeof(Brush), typeof(AdonisWindow), new PropertyMetadata(null));

        protected internal static readonly DependencyPropertyKey MaximizeBorderThicknessPropertyKey = DependencyProperty.RegisterReadOnly("MaximizeBorderThickness", typeof(Thickness), typeof(AdonisWindow), new PropertyMetadata(new Thickness()));

        protected internal static readonly DependencyProperty MaximizeBorderThicknessProperty = MaximizeBorderThicknessPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShrinkTitleBarWhenMaximizedProperty = DependencyProperty.Register("ShrinkTitleBarWhenMaximized", typeof(bool), typeof(AdonisWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty PlaceTitleBarOverContentProperty = DependencyProperty.Register("PlaceTitleBarOverContent", typeof(bool), typeof(AdonisWindow), new PropertyMetadata(false));

        static AdonisWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdonisWindow), new FrameworkPropertyMetadata(typeof(AdonisWindow)));
            IconProperty.OverrideMetadata(typeof(AdonisWindow), new FrameworkPropertyMetadata(OnIconPropertyChanged));
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is AdonisWindow sourceWindow))
                return;

            if (e.NewValue is ImageSource image)
            {
                sourceWindow.IconSource = image;
                return;
            }

            string newIcon = e.NewValue.ToString();

            sourceWindow.IconSource = String.IsNullOrEmpty(newIcon) ? null : new BitmapImage(new Uri(newIcon));
        }

        /// <inheritdoc/>
        public AdonisWindow()
        {
            IconSource = GetApplicationIcon();
            MaximizeBorderThickness = GetSystemMaximizeBorderThickness();
        }

        private BitmapSource GetApplicationIcon()
        {
            string appFilePath = Process.GetCurrentProcess().MainModule.FileName;
            if (!File.Exists(appFilePath))
                return null;

            Icon appIcon = System.Drawing.Icon.ExtractAssociatedIcon(appFilePath);

            if (appIcon == null)
                return null;

            return Imaging.CreateBitmapSourceFromHIcon(appIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private Thickness GetSystemMaximizeBorderThickness()
        {
            Thickness frameThickness = SystemParameters.WindowNonClientFrameThickness;
            Thickness resizeBorderThickness = SystemParameters.WindowResizeBorderThickness;

            return new Thickness(
                frameThickness.Left + resizeBorderThickness.Left - 1,
                frameThickness.Top + resizeBorderThickness.Top - SystemParameters.CaptionHeight - 1,
                frameThickness.Right + resizeBorderThickness.Right - 1,
                frameThickness.Bottom + resizeBorderThickness.Bottom - 1);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            HwndInterop = new HwndInterop(this);

            DragMoveThumb = GetTemplateChild(PART_DragMoveThumb) as FrameworkElement;
            IconPresenter = GetTemplateChild(PART_IconPresenter) as FrameworkElement;
            MinimizeButton = GetTemplateChild(PART_MinimizeButton) as Button;
            MaximizeRestoreButton = GetTemplateChild(PART_MaximizeRestoreButton) as Button;
            CloseButton = GetTemplateChild(PART_CloseButton) as Button;

            if (DragMoveThumb != null)
                InitDragMoveThumb(DragMoveThumb);
            if (IconPresenter != null)
                InitIconPresenter(IconPresenter);
            if (MinimizeButton != null)
                InitMinimizeButton(MinimizeButton);
            if (MaximizeRestoreButton != null)
                InitMaximizeRestoreButton(MaximizeRestoreButton);
            if (CloseButton != null)
                InitCloseButton(CloseButton);

            UpdateLayoutForSizeToContent();
            HwndInterop.PositionChanging += DisableSizeToContentWhenMaximizing;
            HandleTitleBarActualHeightChanged();
        }

        /// <summary>
        /// Initializes functionality of the drag/move thumb component of the window's title bar.
        /// </summary>
        /// <param name="dragMoveThumb">The drag/move thumb component of the window</param>
        protected virtual void InitDragMoveThumb(FrameworkElement dragMoveThumb)
        {
            dragMoveThumb.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ChangedButton == MouseButton.Left && e.ClickCount == 1)
                {
                    if (WindowState == WindowState.Maximized)
                        dragMoveThumb.MouseMove += RestoreOnMouseMove;

                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                        DragMove();
                }

                if (e.ClickCount == 2 &&
                    (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip))
                {
                    ToggleWindowState();
                }
            };

            dragMoveThumb.MouseLeftButtonUp += (s, e) => dragMoveThumb.MouseMove -= RestoreOnMouseMove;

            dragMoveThumb.MouseRightButtonUp += (s, e) => OpenSystemContextMenu(e.GetPosition(this));
        }

        /// <summary>
        /// Initializes functionality of the minimize button of the window's title bar.
        /// </summary>
        /// <param name="minimizeButton">The minimize button of the window</param>
        protected virtual void InitMinimizeButton(Button minimizeButton)
        {
            minimizeButton.Click += MinimizeClick;
        }

        /// <summary>
        /// Initializes functionality of the maximize/restore button of the window's title bar.
        /// </summary>
        /// <param name="maximizeRestoreButton">The maximize/restore button of the window</param>
        protected virtual void InitMaximizeRestoreButton(Button maximizeRestoreButton)
        {
            maximizeRestoreButton.Click += MaximizeRestoreClick;
        }

        /// <summary>
        /// Initializes functionality of the close button of the window's title bar.
        /// </summary>
        /// <param name="closeButton">The close button of the window</param>
        protected virtual void InitCloseButton(Button closeButton)
        {
            closeButton.Click += CloseClick;
        }

        /// <summary>
        /// Initializes functionality of the icon presenter component of the window's title bar.
        /// </summary>
        /// <param name="iconPresenter">The icon presenter component of the window</param>
        protected virtual void InitIconPresenter(FrameworkElement iconPresenter)
        {
            iconPresenter.MouseLeftButtonDown += (s, e) =>
            {
                if (e.ClickCount == 2)
                {
                    Close();
                    return;
                }

                var anchorElement = DragMoveThumb ?? IconPresenter;
                var menuPosition = anchorElement.TranslatePoint(new Point(0, anchorElement.ActualHeight), this);
                OpenSystemContextMenu(menuPosition);
            };
        }

        /// <summary>
        /// Handles the close button's click event.
        /// </summary>
        protected virtual void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the maximize/restore button's click event.
        /// </summary>
        protected virtual void MaximizeRestoreClick(object sender, RoutedEventArgs e)
        {
            ToggleWindowState();
        }

        /// <summary>
        /// Handles the minimize button's click event.
        /// </summary>
        protected virtual void MinimizeClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Sets the <see cref="Window.WindowState"/> to <see cref="WindowState.Maximized"/>
        /// if it is currently at <see cref="WindowState.Normal"/> or else to <see cref="WindowState.Normal"/>.
        /// </summary>
        protected virtual void ToggleWindowState()
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void RestoreOnMouseMove(object sender, MouseEventArgs e)
        {
            var dragMoveThumb = sender as FrameworkElement;
            if (dragMoveThumb == null)
                return;

            // detach event handler to ensure it is called only once per mouse down
            dragMoveThumb.MouseMove -= RestoreOnMouseMove;

            // collect given window and screen data
            Point positionInWindow = e.MouseDevice.GetPosition(this);
            Point positionOnScreen = PointToScreen(positionInWindow);
            ScreenInterop currentScreen = ScreenInterop.FromPoint(positionOnScreen);
            Size restoreSizeOnScreen = TransformToScreenCoordinates(new Size(RestoreBounds.Width, RestoreBounds.Height));

            // calculate window's new top left coordinate
            double restoreLeft = positionOnScreen.X - (restoreSizeOnScreen.Width * 0.5);
            double restoreTop = positionOnScreen.Y - MaximizeBorderThickness.Top;

            // make sure the restore bounds are within the current screen bounds
            if (restoreLeft < currentScreen.Bounds.Left)
                restoreLeft = currentScreen.Bounds.Left;
            else if (restoreLeft + restoreSizeOnScreen.Width > currentScreen.Bounds.Right)
                restoreLeft = currentScreen.Bounds.Right - restoreSizeOnScreen.Width;

            // since we calculated with screen values, we need to convert back to window values
            Point restoreTopLeftOnScreen = new Point(restoreLeft, restoreTop);
            Point restoreTopLeft = TransformToWindowCoordinates(restoreTopLeftOnScreen);

            // restore window to calculated position
            Left = restoreTopLeft.X;
            Top = restoreTopLeft.Y;
            WindowState = WindowState.Normal;

            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        /// <summary>
        /// Converts a Size that represents the current coordinate system of the window
        /// into a Size in screen coordinates.
        /// </summary>
        protected Size TransformToScreenCoordinates(Size size)
        {
            PresentationSource presentationSource = PresentationSource.FromVisual(this);

            if (presentationSource?.CompositionTarget == null)
                return size;

            Matrix transformToDevice = presentationSource.CompositionTarget.TransformToDevice;
            return (Size)transformToDevice.Transform(new Vector(size.Width, size.Height));
        }

        /// <summary>
        /// Converts a <see cref="Size"/> that represents the native coordinate system of the screen
        /// into a <see cref="Size"/> in device independent coordinates.
        /// </summary>
        protected Size TransformToWindowCoordinates(Size size)
        {
            Point transformedCoordinates = TransformToWindowCoordinates(new Point(size.Width, size.Height));
            return new Size(transformedCoordinates.X, transformedCoordinates.Y);
        }

        /// <summary>
        /// Converts a Point that represents the native coordinate system of the screen
        /// into a Point in device independent coordinates.
        /// </summary>
        protected Point TransformToWindowCoordinates(Point point)
        {
            PresentationSource presentationSource = PresentationSource.FromVisual(this);

            if (presentationSource?.CompositionTarget == null)
                return point;

            Matrix transformFromDevice = presentationSource.CompositionTarget.TransformFromDevice;
            return transformFromDevice.Transform(point);
        }

        /// <summary>
        /// Displays the system's native window context menu at the given position.
        /// </summary>
        /// <param name="positionInWindow">Coordinate of top left corner of the context menu relative to the window</param>
        protected virtual void OpenSystemContextMenu(Point positionInWindow)
        {
            SystemContextMenuInterop.OpenSystemContextMenu(this, positionInWindow);
        }

        /// <summary>
        /// When using <see cref="SizeToContent.WidthAndHeight"/> the layout might not be calculated correctly
        /// which can result in the window being too large and having large black borders filling the remaining space.
        /// This method can be used to force a layout update again to recalculate the window size correctly.
        /// See https://social.msdn.microsoft.com/Forums/vstudio/en-US/89fe6959-ce1a-4064-bdde-94151df7dc01/gradient-style-issue-when-sizetocontentheightandwidth-with-customchrome?forum=wpf
        /// </summary>
        private void UpdateLayoutForSizeToContent()
        {
            if (SizeToContent == SizeToContent.WidthAndHeight)
            {
                var previousSizeToContent = SizeToContent;
                SizeToContent = SizeToContent.Manual;

                Dispatcher?.BeginInvoke(DispatcherPriority.Loaded, (Action)(() =>
                {
                    SizeToContent = previousSizeToContent;
                }));
            }
        }

        /// <summary>
        /// In order to maximize the window correctly, <see cref="SizeToContent.WidthAndHeight"/> must not be set.
        /// This method ensures that <see cref="SizeToContent.Manual"/> is set when the window is about to be maximized.
        /// </summary>
        private void DisableSizeToContentWhenMaximizing(object sender, HwndInteropPositionChangingEventArgs e)
        {
            if (e.Type == HwndInteropPositionChangingEventArgs.PositionChangeType.MAXIMIZERESTORE)
            {
                SizeToContent = SizeToContent.Manual;
            }
        }

        private void HandleTitleBarActualHeightChanged()
        {
            if (!(GetTemplateChild("TitleBar") is Border titleBar))
            {
                return;
            };

            var titleBarHeightPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(ActualHeightProperty, typeof(Border));

            titleBarHeightPropertyDescriptor.AddValueChanged(titleBar, (sender, e) =>
            {
                TitleBarActualHeight = PlaceTitleBarOverContent
                    ? titleBar.ActualHeight
                    : 0.0d;
            });
        }
    }
}
