using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AdonisUI.Converters
{
    public class RectConverter
        : IMultiValueConverter
    {
        public static RectConverter Instance = new RectConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0)
                return new Rect();

            if (values.Length == 1 && values[0] is Size)
                return new Rect((Size)values[0]);

            if (values.Length == 2 && values[0] is double && values[1] is double)
                return new Rect(new Size((double) values[0], (double) values[1]));

            if (values.Length == 2 && values[0] is Point && values[1] is Point)
                return new Rect((Point)values[0], (Point)values[1]);

            if (values.Length == 2 && values[0] is Point && values[1] is Size)
                return new Rect((Point)values[0], (Size)values[1]);

            if (values.Length == 2 && values[0] is Point && values[1] is Vector)
                return new Rect((Point)values[0], (Vector)values[1]);

            if (values.Length == 4 && values[0] is double && values[1] is double && values[2] is double && values[3] is double)
                return new Rect((double)values[0], (double)values[1], (double)values[2], (double)values[3]);

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
