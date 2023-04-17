using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace clawSoft.clawPDF.Shared.Converter
{
    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Color)) return ConvertWpfColor(value);

            if (targetType == typeof(System.Drawing.Color)) return ConvertFormsColor(value);

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Color ConvertWpfColor(object value)
        {
            if (value is Color)
                return (Color)value;

            if (value is System.Drawing.Color)
            {
                var color = (System.Drawing.Color)value;
                return Color.FromArgb(color.A, color.R, color.G, color.B);
            }

            throw new NotImplementedException();
        }

        private System.Drawing.Color ConvertFormsColor(object value)
        {
            throw new NotImplementedException();
        }
    }
}