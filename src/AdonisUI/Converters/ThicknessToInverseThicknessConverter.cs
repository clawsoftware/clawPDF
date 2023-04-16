using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    public class ThicknessToInverseThicknessConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new ThicknessToInverseThicknessConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness valueAsThickness = (Thickness)value;

            return new Thickness(
                -valueAsThickness.Left,
                -valueAsThickness.Top,
                -valueAsThickness.Right,
                -valueAsThickness.Bottom);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
