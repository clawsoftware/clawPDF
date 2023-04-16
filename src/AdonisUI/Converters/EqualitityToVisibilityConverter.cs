using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    public class EqualitityToVisibilityConverter
        : IValueConverter
        , IMultiValueConverter
    {
        public static EqualitityToVisibilityConverter Instance = new EqualitityToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool equals = (bool)EqualityToBoolConverter.Instance.Convert(value, targetType, parameter, culture);

            return equals ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool equals = (bool)EqualityToBoolConverter.Instance.Convert(values, targetType, parameter, culture);

            if (parameter.ToString().ToLower() == "hidden")
                return equals ? Visibility.Visible : Visibility.Hidden;

            return equals ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
