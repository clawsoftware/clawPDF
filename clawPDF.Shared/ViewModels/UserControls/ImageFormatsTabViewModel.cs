using System.Collections.Generic;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.ViewModels.UserControls
{
    public class ImageFormatsTabViewModel : CurrentProfileViewModel
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public static IEnumerable<EnumValue<JpegColor>> JpegColorValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<JpegColor>();

        public static IEnumerable<EnumValue<PngColor>> PngColorValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<PngColor>();

        public static IEnumerable<EnumValue<TiffColor>> TiffColorValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<TiffColor>();
    }
}