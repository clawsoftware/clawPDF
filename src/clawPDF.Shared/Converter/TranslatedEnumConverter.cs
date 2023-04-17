using System;
using System.Globalization;
using System.Windows.Data;
using clawSoft.clawPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.Converter
{
    internal class TranslatedEnumConverter : IValueConverter
    {
        private readonly Translator _translator;

        public TranslatedEnumConverter(Translator translator)
        {
            _translator = translator;
        }

        public TranslatedEnumConverter()
        {
            if (TranslationHelper.Instance.IsInitialized) _translator = TranslationHelper.Instance.TranslatorInstance;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sourceType = value.GetType();
            if (!sourceType.IsEnum)
                throw new InvalidOperationException("The target must be an enum");

            if (_translator == null)
                return value.ToString();

            var translation = _translator.GetTranslation("Enums", sourceType.Name + "." + value);

            if (string.IsNullOrEmpty(translation))
                translation = value.ToString();

            return translation;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}