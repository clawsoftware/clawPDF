using System;
using System.Globalization;
using System.Windows.Data;

namespace clawSoft.clawPDF.Converter
{
    internal class IdentityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}