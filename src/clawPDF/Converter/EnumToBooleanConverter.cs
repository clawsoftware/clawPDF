using System;
using System.Globalization;
using System.Windows.Data;

namespace clawSoft.clawPDF.Converter
{
    internal class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter.ToString();
            return Enum.Parse(targetType, parameterString);
        }
    }
}