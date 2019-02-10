using System;
using System.Globalization;
using System.Windows.Data;
using clawSoft.clawPDF.Shared.Helper;

namespace clawSoft.clawPDF.Shared.Converter
{
    internal class TranslatorConverter : IValueConverter
    {
        public string TranslationSection { get; set; }
        public string TranslationKey { get; set; }
        public string DefaultTranslation { get; set; }

        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a string");

            var translation =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation(TranslationSection, TranslationKey,
                    DefaultTranslation);

            return translation;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}