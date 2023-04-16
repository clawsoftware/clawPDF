using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    public class StringIsNullOrEmptyToVisibilityConverter
        : IValueConverter
    {
        public static StringIsNullOrEmptyToVisibilityConverter Instance = new StringIsNullOrEmptyToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hidden = parameter != null && parameter.ToString() == "hidden";

            if (value == null || value.ToString() == String.Empty)
                return hidden ? Visibility.Hidden : Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
