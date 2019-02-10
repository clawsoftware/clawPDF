using System;
using System.Globalization;
using System.Windows.Data;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Utilities.Tokens;

namespace clawSoft.clawPDF.Shared.Converter
{
    [ValueConversion(typeof(string), typeof(string))]
    public class TokenReplacerConverter : IValueConverter
    {
        public TokenReplacerConverter()
        {
            TokenReplacer = TokenHelper.TokenReplacerWithPlaceHolders;
        }

        public TokenReplacer TokenReplacer { get; set; }

        public string Footer { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;

            if (stringValue == null)
                throw new NotImplementedException();

            stringValue = TokenReplacer.ReplaceTokens(stringValue);

            if (Footer != null)
                stringValue += Footer;

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}