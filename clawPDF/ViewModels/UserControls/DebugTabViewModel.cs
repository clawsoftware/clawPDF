using System;
using System.Collections.Generic;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.ViewModels.UserControls
{
    internal class DebugTabViewModel : ApplicationSettingsViewModel
    {
        public IEnumerable<EnumValue<LoggingLevel>> LoggingValues =>
            TranslationHelper.Instance.TranslatorInstance.GetEnumTranslation<LoggingLevel>();

        public bool ProfileManagementIsEnabled => true;

        protected override void OnSettingsChanged(EventArgs e)
        {
            base.OnSettingsChanged(e);

            RaisePropertyChanged("ProfileManagementIsEnabled");
        }
    }
}