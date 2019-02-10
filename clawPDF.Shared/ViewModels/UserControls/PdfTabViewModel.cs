using System;
using System.Collections.Generic;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.PDFProcessing;
using clawSoft.clawPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.ViewModels.UserControls
{
    public class PdfTabViewModel : CurrentProfileViewModel
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public PdfTabViewModel()
        {
            ProfileChanged += OnProfileChanged;
        }

        public static IEnumerable<EnumValue<PageOrientation>> PageOrientationValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<PageOrientation>();

        public static IEnumerable<EnumValue<ColorModel>> ColorModelValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<ColorModel>();

        public static IEnumerable<EnumValue<PageView>> PageViewValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<PageView>();

        public static IEnumerable<EnumValue<DocumentView>> DocumentViewValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<DocumentView>();

        public static IEnumerable<EnumValue<CompressionColorAndGray>> CompressionColorAndGrayValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<CompressionColorAndGray>();

        public static IEnumerable<EnumValue<CompressionMonochrome>> CompressionMonochromeValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<CompressionMonochrome>();

        public static IEnumerable<EnumValue<SignaturePage>> SignaturePageValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<SignaturePage>();

        public bool EncryptionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.Enabled;
            }
            set
            {
                CurrentProfile.PdfSettings.Security.Enabled = value;
                RaisePropertyChangedForEncryptionProperties();
            }
        }

        public bool LowEncryptionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Rc40Bit;
            }
            set
            {
                if (value) //== true
                    CurrentProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Rc40Bit;
                RaisePropertyChangedForEncryptionProperties();
            }
        }

        public bool MediumEncryptionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Rc128Bit;
            }
            set
            {
                if (value)
                    CurrentProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Rc128Bit;
                RaisePropertyChangedForEncryptionProperties();
            }
        }

        public bool HighEncryptionEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes128Bit;
            }
            set
            {
                if (value) //== true
                    CurrentProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Aes128Bit;
                RaisePropertyChangedForEncryptionProperties();
            }
        }

        public bool ExtendedPermissonsEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.EncryptionLevel != EncryptionLevel.Rc40Bit;
            }
        }

        public bool RestrictLowQualityPrintingEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.RestrictPrintingToLowQuality && ExtendedPermissonsEnabled;
            }
            set => CurrentProfile.PdfSettings.Security.RestrictPrintingToLowQuality = value;
        }

        public bool AllowFillFormsEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.AllowToFillForms || !ExtendedPermissonsEnabled;
            }
            set => CurrentProfile.PdfSettings.Security.AllowToFillForms = value;
        }

        public bool AllowScreenReadersEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.AllowScreenReader || !ExtendedPermissonsEnabled;
            }
            set => CurrentProfile.PdfSettings.Security.AllowScreenReader = value;
        }

        public bool AllowEditingAssemblyEnabled
        {
            get
            {
                if (CurrentProfile == null)
                    return false;
                return CurrentProfile.PdfSettings.Security.AllowToEditAssembly || !ExtendedPermissonsEnabled;
            }

            set => CurrentProfile.PdfSettings.Security.AllowToEditAssembly = value;
        }

        public string PdfVersion
        {
            get
            {
                if (CurrentProfile == null)
                    return "1.4";
                return PDFProcessor.DeterminePdfVersion(CurrentProfile);
            }
        }

        private void OnProfileChanged(object sender, EventArgs eventArgs)
        {
            RaisePropertyChangedForEncryptionProperties();
        }

        private void RaisePropertyChangedForEncryptionProperties()
        {
            RaisePropertyChanged("EncryptionEnabled");
            RaisePropertyChanged("LowEncryptionEnabled");
            RaisePropertyChanged("MediumEncryptionEnabled");
            RaisePropertyChanged("HighEncryptionEnabled");
            RaisePropertyChanged("ExtendedPermissonsEnabled");
            RaisePropertyChanged("RestrictLowQualityPrintingEnabled");
            RaisePropertyChanged("AllowFillFormsEnabled");
            RaisePropertyChanged("AllowScreenReadersEnabled");
            RaisePropertyChanged("AllowEditingAssemblyEnabled");
            RaisePropertyChanged("PdfVersion");
        }
    }
}