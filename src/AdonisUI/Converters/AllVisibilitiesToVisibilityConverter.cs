using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    /// <summary>
    /// Takes multiple Visibilities as input and returns a single Visibility of the same value if they are all equal
    /// or returns Visibility.Visible.
    /// </summary>
    class AllVisibilitiesToVisibilityConverter
        : IMultiValueConverter
    {
        public static AllVisibilitiesToVisibilityConverter Instance = new AllVisibilitiesToVisibilityConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.OfType<Visibility>().All(x => x == Visibility.Collapsed))
            {
                return Visibility.Collapsed;
            }

            if (values.OfType<Visibility>().All(x => x == Visibility.Hidden))
            {
                return Visibility.Hidden;
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
