using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace clawSoft.clawPDF.Shared.Converter
{
    public class AreEqualConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty ReferenceProperty =
            DependencyProperty.Register("Reference", typeof(object), typeof(AreEqualConverter));

        public object Reference
        {
            get => GetValue(ReferenceProperty);
            set => SetValue(ReferenceProperty, value);
        }

        public object EqualValue { get; set; }
        public object NotEqualValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Reference == null)
                return NotEqualValue;

            return Reference.Equals(value) ? EqualValue : NotEqualValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}