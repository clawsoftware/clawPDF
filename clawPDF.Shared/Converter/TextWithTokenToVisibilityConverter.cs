using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.Shared.Converter
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public sealed class TextWithTokenToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return null;
            if (TokenHelper.ContainsInsecureTokens((string)value))
                return Visibility.Visible;
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}