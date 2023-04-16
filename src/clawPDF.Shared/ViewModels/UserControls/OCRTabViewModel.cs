using System;
using System.Collections.Generic;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.PDFProcessing;
using clawSoft.clawPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.ViewModels.UserControls
{
    public class OCRTabViewModel : CurrentProfileViewModel
    {
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public OCRTabViewModel()
        {
            ProfileChanged += OnProfileChanged;
        }

        public string OCRLanguage
        {
            get
            {
                if (CurrentProfile == null)
                    return "eng+deu";
                return PDFProcessor.DeterminePdfVersion(CurrentProfile);
            }
            set => CurrentProfile.OCRSettings.OCRLanguage = value;
        }

        private void OnProfileChanged(object sender, EventArgs eventArgs)
        {
            RaisePropertyChangedForEncryptionProperties();
        }

        private void RaisePropertyChangedForEncryptionProperties()
        {
            RaisePropertyChanged("OCRLanguage");
        }
    }
}