using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AdonisUI
{
    public class SpaceExtension
        : MarkupExtension
    {
        private const double DesignModeHorizontalSpace = 8;

        private const double DesignModeVerticalSpace = 8;

        private static readonly bool IsInDesignMode = DesignerProperties.GetIsInDesignMode(new DependencyObject());

        private static double? _cachedHorizontalSpace;

        private static double? _cachedVerticalSpace;

        private static FrameworkElement _resourceOwnerFallback;

        public double? Factor { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        public double Bottom { get; set; }

        public double? Offset { get; set; }

        public double OffsetLeft { get; set; }

        public double OffsetTop { get; set; }

        public double OffsetRight { get; set; }

        public double OffsetBottom { get; set; }

        public Orientation? Orientation { get; set; }

        public double HorizontalOffset
        {
            set
            {
                OffsetLeft = value;
                OffsetRight = value;
            }
        }

        public double VerticalOffset
        {
            set
            {
                OffsetTop = value;
                OffsetBottom = value;
            }
        }

        public SpaceExtension()
        {
            Factor = 1;
            Left = 1;
            Top = 1;
            Right = 1;
            Bottom = 1;
        }

        public SpaceExtension(string expression)
        {
            if (!TryParseExpression(expression, out double factor, out double offset))
                throw new NotSupportedException("Expression cannot be parsed.");

            Factor = factor;
            Left = factor;
            Top = factor;
            Right = factor;
            Bottom = factor;
            Offset = offset;
            OffsetLeft = offset;
            OffsetTop = offset;
            OffsetRight = offset;
            OffsetBottom = offset;
        }

        public SpaceExtension(string horizontalExpression, string verticalExpression)
        {
            bool canParseHorizontal = TryParseExpression(horizontalExpression, out double horizontalFactor, out double horizontalOffset);
            bool canParseVertical = TryParseExpression(verticalExpression, out double verticalFactor, out double verticalOffset);

            if (!canParseHorizontal || !canParseVertical)
                throw new NotSupportedException("Expression cannot be parsed.");

            Left = horizontalFactor;
            Top = verticalFactor;
            Right = horizontalFactor;
            Bottom = verticalFactor;

            OffsetLeft = horizontalOffset;
            OffsetTop = verticalOffset;
            OffsetRight = horizontalOffset;
            OffsetBottom = verticalOffset;
        }

        public SpaceExtension(string leftExpression, string topExpression, string rightExpression, string bottomExpression)
        {
            bool canParseLeft = TryParseExpression(leftExpression, out double leftFactor, out double leftOffset);
            bool canParseTop = TryParseExpression(topExpression, out double topFactor, out double topOffset);
            bool canParseRight = TryParseExpression(rightExpression, out double rightFactor, out double rightOffset);
            bool canParseBottom = TryParseExpression(bottomExpression, out double bottomFactor, out double bottomOffset);

            if (!canParseLeft || !canParseTop || !canParseRight || !canParseBottom)
                throw new NotSupportedException("Expression cannot be parsed.");

            Left = leftFactor;
            Top = topFactor;
            Right = rightFactor;
            Bottom = bottomFactor;

            OffsetLeft = leftOffset;
            OffsetTop = topOffset;
            OffsetRight = rightOffset;
            OffsetBottom = bottomOffset;
        }

        public static void SetSpace(double horizontalSpace, double verticalSpace)
        {
            _cachedHorizontalSpace = horizontalSpace;
            _cachedVerticalSpace = verticalSpace;
        }

        public static void SetSpaceResourceOwnerFallback(FrameworkElement resourceOwner)
        {
            _resourceOwnerFallback = resourceOwner;
        }

        private static void ReleaseSpaceResourceOwnerFallback()
        {
            _resourceOwnerFallback = null;
        }

        /// <summary>
        /// Expects an expression in the form of [x+y] or [x-y] where x is parsed as factor and y as offset
        /// </summary>
        protected virtual bool TryParseExpression(string expression, out double factor, out double offset)
        {
            factor = 0;
            offset = 0;

            if (String.IsNullOrEmpty(expression))
                return false;

            char sign;

            if (expression.Substring(1).Contains('+'))
                sign = '+';
            else if (expression.Substring(1).Contains('-'))
                sign = '-';
            else
                return double.TryParse(expression, NumberStyles.Any, CultureInfo.InvariantCulture, out factor);

            string[] expressionParts = expression.Split(sign);

            if (expressionParts[0] == String.Empty) // first char was <sign>
                expressionParts = new[] { '-' + expressionParts[1], expressionParts[2] };

            if (expressionParts.Length != 2)
                return false;

            bool canParseFactor = double.TryParse(expressionParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out factor);
            bool canParseOffset = double.TryParse(expressionParts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out offset);

            if (sign == '-')
                offset *= -1;

            return canParseFactor && canParseOffset;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget service = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            DependencyProperty targetProperty = GetTargetProperty(service);
            double horizontalSpace = GetHorizontalSpace(service);
            double verticalSpace = GetVerticalSpace(service);

            if (targetProperty != null && targetProperty.PropertyType == typeof(GridLength) && Factor.HasValue && Offset.HasValue)
            {
                if (Orientation.HasValue)
                    return Factor * (Orientation.Value == System.Windows.Controls.Orientation.Horizontal ? horizontalSpace : verticalSpace) + Offset;

                if (targetProperty.OwnerType == typeof(RowDefinition))
                    return new GridLength(Factor.Value * verticalSpace + Offset.Value);

                return new GridLength(Factor.Value * horizontalSpace + Offset.Value);
            }

            if (targetProperty != null && targetProperty.PropertyType == typeof(Thickness) || !Factor.HasValue)
                return new Thickness(
                    Left * horizontalSpace + OffsetLeft,
                    Top * verticalSpace + OffsetTop,
                    Right * horizontalSpace + OffsetRight,
                    Bottom * verticalSpace + OffsetBottom);

            if (Orientation.HasValue)
                return Factor * (Orientation.Value == System.Windows.Controls.Orientation.Horizontal ? horizontalSpace : verticalSpace) + Offset;

            Orientation? guessedOrientation = GuessPreferredOrientation(targetProperty);
            if (guessedOrientation != null)
                return Factor * (guessedOrientation.Value == System.Windows.Controls.Orientation.Horizontal ? horizontalSpace : verticalSpace) + Offset;

            throw new InvalidOperationException($"Cannot determine target orientation for property ${service.TargetProperty} on type ${service.TargetObject.GetType().FullName}. Orientation must be specified manually.");
        }

        protected virtual DependencyProperty GetTargetProperty(IProvideValueTarget service)
        {
            DependencyProperty targetProperty = service.TargetProperty as DependencyProperty;

            if (service.TargetObject is Setter setter)
                targetProperty = setter.Property;

            return targetProperty;
        }

        protected virtual double GetHorizontalSpace(IProvideValueTarget service)
        {
            if (_cachedHorizontalSpace.HasValue)
                return _cachedHorizontalSpace.Value;

            try
            {
                FindAndCacheSpaceResources(service);
            }
            catch
            {
                if (IsInDesignMode)
                    return DesignModeHorizontalSpace;

                throw;
            }

            if (!_cachedHorizontalSpace.HasValue)
                throw new Exception("Dimensions.HorizontalSpace could not be retrieved.");

            return _cachedHorizontalSpace.Value;
        }

        protected virtual double GetVerticalSpace(IProvideValueTarget service)
        {
            if (_cachedVerticalSpace.HasValue)
                return _cachedVerticalSpace.Value;

            try
            {
                FindAndCacheSpaceResources(service);
            }
            catch
            {
                if (IsInDesignMode)
                    return DesignModeVerticalSpace;

                throw;
            }

            if (!_cachedVerticalSpace.HasValue)
                throw new Exception("Dimensions.VerticalSpace could not be retrieved.");

            return _cachedVerticalSpace.Value;
        }

        private void FindAndCacheSpaceResources(IProvideValueTarget service)
        {
            object horizontalSpace = TryFindResource(service, Dimensions.HorizontalSpace);
            object verticalSpace = TryFindResource(service, Dimensions.VerticalSpace);

            if (horizontalSpace == null)
                throw new ResourceReferenceKeyNotFoundException("Cannot find Dimensions.HorizontalSpace resource.", Dimensions.HorizontalSpace);
            if (verticalSpace == null)
                throw new ResourceReferenceKeyNotFoundException("Cannot find Dimensions.VerticalSpace resource.", Dimensions.VerticalSpace);

            _cachedHorizontalSpace = (double) horizontalSpace;
            _cachedVerticalSpace = (double) verticalSpace;

            ReleaseSpaceResourceOwnerFallback();
        }

        protected virtual object TryFindResource(IProvideValueTarget service, object resourceKey)
        {
            if (service.TargetObject is FrameworkElement element)
                return element.TryFindResource(resourceKey);

            if (_resourceOwnerFallback != null)
                return _resourceOwnerFallback.TryFindResource(resourceKey);

            if (Application.Current != null)
                return Application.Current.TryFindResource(resourceKey);

            return null;
        }

        protected virtual Orientation? GuessPreferredOrientation(DependencyProperty targetProperty)
        {
            if (targetProperty == null)
                return null;

            string[] horizontalPropertyNames = { "width", "horizontal" };
            string[] verticalPropertyNames = { "height", "vertical" };

            if (horizontalPropertyNames.Any(name => targetProperty.Name.ToLower().Contains(name)))
                return System.Windows.Controls.Orientation.Horizontal;

            if (verticalPropertyNames.Any(name => targetProperty.Name.ToLower().Contains(name)))
                return System.Windows.Controls.Orientation.Vertical;

            return null;
        }
    }
}
