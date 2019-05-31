using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml;

namespace clawSoft.clawPDF.Converter
{
    internal class ColorWithFactorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var color = (Color)ColorConverter.ConvertFromString(value.ToString());
                var factor = XmlConvert.ToDouble(parameter.ToString());
                var changedColor = new Color();
                changedColor.R = MultiplyByte(color.R, factor);
                changedColor.G = MultiplyByte(color.G, factor);
                changedColor.B = MultiplyByte(color.B, factor);
                changedColor.A = 255;

                return new SolidColorBrush(changedColor);
            }
            catch
            {
                return SystemColors.ControlBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private byte MultiplyByte(byte byte1, double factor)
        {
            var result = byte1 * factor;
            if (result > 255)
                result = 255;
            else if (result < 0)
                result = 0;
            return (byte)result;
        }
    }
}