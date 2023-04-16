using AdonisUI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;

namespace AdonisUI.Controls
{
    /// <summary>
    /// Displays a message box.
    /// </summary>
    [TemplatePart(Name = PART_MessageBoxButtonContainer, Type = typeof(DependencyObject))]
    public class MessageBoxWindow
        : AdonisWindow
    {
        private const string PART_MessageBoxButtonContainer = "PART_MessageBoxButtonContainer";

        public IMessageBoxModel ViewModel
        {
            get => DataContext as IMessageBoxModel;
            set => DataContext = value;
        }

        /// <summary>
        /// A <see cref="Style"/> targeting <see cref="Button"/> that represents the style of all buttons inside the message box.
        /// </summary>
        public Style ButtonStyle
        {
            get => GetValue(ButtonStyleProperty) as Style;
            set => SetValue(ButtonStyleProperty, value);
        }

        /// <summary>
        /// A <see cref="Style"/> targeting <see cref="CheckBox"/> that represents the style of all check boxes inside the message box.
        /// </summary>
        public Style CheckBoxStyle
        {
            get => GetValue(CheckBoxStyleProperty) as Style;
            set => SetValue(CheckBoxStyleProperty, value);
        }

        /// <summary>
        /// A <see cref="Style"/> targeting <see cref="Border"/> that represents the style of the button container row.
        /// </summary>
        public Style ButtonContainerStyle
        {
            get => GetValue(ButtonContainerStyleProperty) as Style;
            set => SetValue(ButtonContainerStyleProperty, value);
        }

        /// <summary>
        /// A <see cref="double"/> that represents the maximum width of the message box relative to the current screen width.
        /// </summary>
        public double MaxRelativeScreenWidth
        {
            get => (double)GetValue(MaxRelativeScreenWidthProperty);
            set => SetValue(MaxRelativeScreenWidthProperty, value);
        }

        /// <summary>
        /// A <see cref="double"/> that represents the maximum height of the message box relative to the current screen height.
        /// </summary>
        public double MaxRelativeScreenHeight
        {
            get => (double)GetValue(MaxRelativeScreenHeightProperty);
            set => SetValue(MaxRelativeScreenHeightProperty, value);
        }

        /// <summary>
        /// A <see cref="double"/> that represents the amount of which the maximum width is increased in case it does not fit the screen.
        /// The message box will choose the smallest multiple of this value where its height does not become larger than specified via <see cref="MaxRelativeScreenHeightProperty"/>.
        /// </summary>
        public double MaxWidthStep
        {
            get => (double)GetValue(MaxWidthStepProperty);
            set => SetValue(MaxWidthStepProperty, value);
        }

        /// <summary>
        /// A <see cref="Style"/> targeting <see cref="Button"/> that represents the style of all buttons inside the message box.
        /// </summary>
        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(nameof(ButtonStyle), typeof(Style), typeof(MessageBoxWindow), new PropertyMetadata(null));

        /// <summary>
        /// A <see cref="Style"/> targeting <see cref="CheckBox"/> that represents the style of all check boxes inside the message box.
        /// </summary>
        public static readonly DependencyProperty CheckBoxStyleProperty = DependencyProperty.Register(nameof(CheckBoxStyle), typeof(Style), typeof(MessageBoxWindow), new PropertyMetadata(null));

        /// <summary>
        /// A <see cref="Style"/> targeting <see cref="Border"/> that represents the style of the button container row.
        /// </summary>
        public static readonly DependencyProperty ButtonContainerStyleProperty = DependencyProperty.Register(nameof(ButtonContainerStyle), typeof(Style), typeof(MessageBoxWindow), new PropertyMetadata(null));

        /// <summary>
        /// A <see cref="double"/> that represents the maximum width of the message box relative to the current screen width.
        /// </summary>
        public static readonly DependencyProperty MaxRelativeScreenWidthProperty = DependencyProperty.Register("MaxRelativeScreenWidth", typeof(double), typeof(MessageBoxWindow), new PropertyMetadata(0.9));

        /// <summary>
        /// A <see cref="double"/> that represents the maximum height of the message box relative to the current screen height.
        /// </summary>
        public static readonly DependencyProperty MaxRelativeScreenHeightProperty = DependencyProperty.Register("MaxRelativeScreenHeight", typeof(double), typeof(MessageBoxWindow), new PropertyMetadata(0.9));

        /// <summary>
        /// A <see cref="double"/> that represents the amount of which the maximum width is increased in case it does not fit the screen.
        /// The message box will choose the smallest multiple of this value where its height does not become larger than specified via <see cref="MaxRelativeScreenHeightProperty"/>.
        /// </summary>
        public static readonly DependencyProperty MaxWidthStepProperty = DependencyProperty.Register("MaxWidthStep", typeof(double), typeof(MessageBoxWindow), new PropertyMetadata(200.0));

        /// <summary>
        /// A <see cref="ICollectionView"/> that represents the check boxes that are displayed below the message box text.
        /// </summary>
        public ICollectionView CheckBoxesBelowTextView { get; private set; }

        /// <summary>
        /// A <see cref="ICollectionView"/> that represents the check boxes that are displayed next to the message box buttons.
        /// </summary>
        public ICollectionView CheckBoxesNextToButtonsView { get; private set; }

        static MessageBoxWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxWindow), new FrameworkPropertyMetadata(typeof(MessageBoxWindow)));
        }

        /// <summary>
        /// Creates a new instance of <see cref="MessageBoxWindow"/>.
        /// </summary>
        public MessageBoxWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Topmost = true;
            Loaded += OnLoaded;
            Closing += OnClosing;
            DataContextChanged += OnDataContextChanged;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!CanCancel())
                CloseButton.IsEnabled = false;

            Rect screenBounds = GetCurrentScreenBounds();
            Size transformedScreenBounds = TransformToWindowCoordinates(new Size(screenBounds.Width, screenBounds.Height));
            MaxHeight = transformedScreenBounds.Height * MaxRelativeScreenHeight;
            MaxWidth = CalcMaxWidth(transformedScreenBounds);

            AttachButtonClickHandlers();

            if (ViewModel != null && ViewModel.IsSoundEnabled)
                PlayOpeningSound();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            InitCheckBoxCollectionViews();
        }

        /// <summary>
        /// Determines if the window can be cancelled via close button or keyboard shortcuts.
        /// </summary>
        /// <returns><see langword="true"/> if the window can be cancelled.</returns>
        protected virtual bool CanCancel()
        {
            if (ViewModel == null)
                return true;

            if (!ViewModel.Buttons.Any())
            {
                return true;
            }

            if (ViewModel.Buttons.Any(btn =>
                btn.CausedResult == MessageBoxResult.OK ||
                btn.CausedResult == MessageBoxResult.Cancel))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the screen bounds of the screen the window is currently in.
        /// </summary>
        /// <returns>A <see cref="Rect"/> representing the current screen bounds.</returns>
        protected Rect GetCurrentScreenBounds()
        {
            return ScreenInterop.FromHandle(new WindowInteropHelper(this).Handle).Bounds;
        }

        /// <summary>
        /// Calculates the maximum width of the window depending on its content and the available screen space.
        /// </summary>
        /// <param name="availableSize">A <see cref="Size"/> that reflects the available size that the window can give to the child.</param>
        /// <returns>A <see cref="double"/> that is used as the window's MaxWidth.</returns>
        protected virtual double CalcMaxWidth(Size availableSize)
        {
            int maxWidthStepCounter = 0;
            double maxAbsoluteScreenWidth = availableSize.Width * MaxRelativeScreenWidth;

            // save current max size
            double maxWidth = MaxWidth;
            double maxHeight = MaxHeight;

            // remove max height for calculation and test different max widths
            MaxHeight = double.PositiveInfinity;
            MaxWidth = Math.Min(MinWidth + MaxWidthStep * maxWidthStepCounter, maxAbsoluteScreenWidth);

            Size measuredSize = base.MeasureOverride(availableSize);

            // measure with different max widths until the window fits the screen
            while (measuredSize.Height > availableSize.Height * MaxRelativeScreenHeight && MaxWidth < maxAbsoluteScreenWidth)
            {
                maxWidthStepCounter++;
                MaxWidth = Math.Min(MinWidth + MaxWidthStep * maxWidthStepCounter, maxAbsoluteScreenWidth);
                measuredSize = base.MeasureOverride(availableSize);
            }

            // save result
            double fittingMaxWidth = MaxWidth;

            // reset max size
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;

            // increase width in case buttons are clipped
            FrameworkElement buttonContainer = UINavigator.FindVisualChild<FrameworkElement>(this, PART_MessageBoxButtonContainer);
            double additionalWidthRequiredForButtons = Math.Max(CalcDesiredActualSizeDiff(buttonContainer).X, 0);
            fittingMaxWidth = Math.Min(fittingMaxWidth + additionalWidthRequiredForButtons, maxAbsoluteScreenWidth);

            return fittingMaxWidth;
        }

        /// <summary>
        /// Calculates the difference between the desired size and actual size of a <see cref="FrameworkElement"/> measured against positive infinity.
        /// </summary>
        /// <param name="element">A <see cref="FrameworkElement"/> whose size difference is about to be measured.</param>
        /// <returns>A <see cref="Size"/> that represents the calculated difference in width and height.</returns>
        protected Vector CalcDesiredActualSizeDiff(FrameworkElement element)
        {
            if (element == null)
                return new Vector(0, 0);

            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            return new Vector(element.DesiredSize.Width - element.ActualWidth, element.DesiredSize.Height - element.ActualHeight);
        }

        /// <summary>
        /// Finds all child buttons in <see cref="PART_MessageBoxButtonContainer"/> and attaches a click handler to of them.
        /// </summary>
        private void AttachButtonClickHandlers()
        {
            var buttonContainer = UINavigator.FindVisualChild<DependencyObject>(this, PART_MessageBoxButtonContainer);

            if (buttonContainer == null)
                throw new InvalidOperationException($"An element named ${PART_MessageBoxButtonContainer} must be present in the message box control template.");

            var buttons = UINavigator.FindVisualChildren<Button>(buttonContainer);

            foreach (Button button in buttons)
            {
                button.Click += MessageBoxButton_Click;

                if (button.IsDefault)
                    button.Focus();
            }
        }

        /// <summary>
        /// Handles a click on one of the displayed message box buttons.
        /// </summary>
        /// <param name="sender">The button that was clicked on.</param>
        /// <param name="e">Event arguments.</param>
        protected virtual void MessageBoxButton_Click(object sender, RoutedEventArgs e)
        {
            IMessageBoxButtonModel buttonModel = (IMessageBoxButtonModel)((FrameworkElement)sender).DataContext;

            if (ViewModel != null)
            {
                ViewModel.Result = buttonModel.CausedResult;
                ViewModel.ButtonPressed = buttonModel;
            }

            DialogResult = buttonModel.CausedResult != MessageBoxResult.No && buttonModel.CausedResult != MessageBoxResult.Cancel;
        }

        /// <summary>
        /// Plays a system sound depending on the associated <see cref="MessageBoxImage"/>.
        /// </summary>
        protected virtual void PlayOpeningSound()
        {
            switch (ViewModel.Icon)
            {
                case MessageBoxImage.Asterisk:
                case MessageBoxImage.Information:
                    SystemSounds.Asterisk.Play();
                    break;

                case MessageBoxImage.Error:
                case MessageBoxImage.Hand:
                case MessageBoxImage.Stop:
                    SystemSounds.Hand.Play();
                    break;

                case MessageBoxImage.Exclamation:
                case MessageBoxImage.Warning:
                    SystemSounds.Exclamation.Play();
                    break;

                case MessageBoxImage.Question:
                    SystemSounds.Question.Play();
                    break;
            }
        }

        private void InitCheckBoxCollectionViews()
        {
            if (ViewModel == null)
            {
                CheckBoxesBelowTextView = null;
                CheckBoxesNextToButtonsView = null;
                return;
            }

            CheckBoxesBelowTextView = new CollectionViewSource { Source = ViewModel.CheckBoxes }.View;
            CheckBoxesBelowTextView.Filter = checkBox => ((IMessageBoxCheckBoxModel)checkBox).Placement == MessageBoxCheckBoxPlacement.BelowText;

            CheckBoxesNextToButtonsView = new CollectionViewSource { Source = ViewModel.CheckBoxes }.View;
            CheckBoxesNextToButtonsView.Filter = checkBox => ((IMessageBoxCheckBoxModel)checkBox).Placement == MessageBoxCheckBoxPlacement.NextToButtons;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (ViewModel == null)
                return;

            if (ViewModel.Result == MessageBoxResult.None)
            {
                if (!CanCancel())
                    e.Cancel = true;
                else
                    ViewModel.Result = GetDefaultCancelResult();
            }
        }

        /// <summary>
        /// Gets the message box result used when the window is closed but no message box button has been clicked.
        /// </summary>
        /// <returns>A <see cref="MessageBoxResult"/> that represents the message box window's result.</returns>
        protected virtual MessageBoxResult GetDefaultCancelResult()
        {
            if (ViewModel == null)
                return MessageBoxResult.Cancel;

            if (ViewModel.Buttons.Any(btn => btn.CausedResult == MessageBoxResult.Cancel))
                return MessageBoxResult.Cancel;

            if (ViewModel.Buttons.Any(btn => btn.CausedResult == MessageBoxResult.OK))
                return MessageBoxResult.OK;

            return MessageBoxResult.Cancel;
        }
    }
}