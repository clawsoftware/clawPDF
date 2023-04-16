using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    /// <summary>
    /// Takes multiple bools as input and returns true if all bools are true.
    /// </summary>
    public class AllBoolsToBoolConverter
        : IMultiValueConverter
    {
        public static AllBoolsToBoolConverter Instance = new AllBoolsToBoolConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.OfType<bool>().All(x => x);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
