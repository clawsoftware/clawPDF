using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    public class ValuesToThicknessConverter
        : IValueConverter
        , IMultiValueConverter
    {
        public static ValuesToThicknessConverter Instance = new ValuesToThicknessConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToThickness(value);
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
                return ConvertToThickness(values[0]);

            if (values.Length == 2)
                return ConvertToThickness(values[0], values[1]);

            if (values.Length == 4)
                return ConvertToThickness(values[0], values[1], values[2], values[3]);

            throw new ArgumentException("Invalid amount of values", nameof(values));
        }

        private Thickness ConvertToThickness(object uniformValue)
        {
            double uniformDouble = ToDouble(uniformValue);
            return new Thickness(uniformDouble);
        }

        private Thickness ConvertToThickness(object leftRight, object topBottom)
        {
            double leftRightDouble = ToDouble(leftRight);
            double topBottomDouble = ToDouble(topBottom);

            return new Thickness(leftRightDouble, topBottomDouble, leftRightDouble, topBottomDouble);
        }

        private Thickness ConvertToThickness(object left, object top, object right, object bottom)
        {
            double leftDouble = ToDouble(left);
            double topDouble = ToDouble(top);
            double rightDouble = ToDouble(right);
            double bottomDouble = ToDouble(bottom);

            return new Thickness(leftDouble, topDouble, rightDouble, bottomDouble);
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
