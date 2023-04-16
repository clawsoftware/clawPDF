using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    public class ValuesToCornerRadiusConverter
        : IValueConverter
        , IMultiValueConverter
    {
        public static ValuesToCornerRadiusConverter Instance = new ValuesToCornerRadiusConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToCornerRadius(value);
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
                return ConvertToCornerRadius(values[0]);

            if (values.Length == 2)
                return ConvertToCornerRadius(values[0], values[1]);

            if (values.Length == 4)
                return ConvertToCornerRadius(values[0], values[1], values[2], values[3]);

            throw new ArgumentException("Invalid amount of values", nameof(values));
        }

        private CornerRadius ConvertToCornerRadius(object uniformValue)
        {
            double uniformDouble = ToDouble(uniformValue);
            return new CornerRadius(uniformDouble);
        }

        private CornerRadius ConvertToCornerRadius(object topLeftAndBottomRight, object topRightAndBottomLeft)
        {
            double topLeftAndBottomRightDouble = ToDouble(topLeftAndBottomRight);
            double topRightAndBottomLeftDouble = ToDouble(topRightAndBottomLeft);

            return new CornerRadius(topLeftAndBottomRightDouble, topRightAndBottomLeftDouble, topLeftAndBottomRightDouble, topRightAndBottomLeftDouble);
        }

        private CornerRadius ConvertToCornerRadius(object topLeft, object topRight, object bottomRight, object bottomLeft)
        {
            double topLeftDouble = ToDouble(topLeft);
            double topRightDouble = ToDouble(topRight);
            double bottomRightDouble = ToDouble(bottomRight);
            double bottomLeftDouble = ToDouble(bottomLeft);

            return new CornerRadius(topLeftDouble, topRightDouble, bottomRightDouble, bottomLeftDouble);
        }

        private double ToDouble(object value)
        {
            return value != DependencyProperty.UnsetValue ? System.Convert.ToDouble(value) : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
