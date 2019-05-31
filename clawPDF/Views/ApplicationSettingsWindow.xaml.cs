using System;
using System.Collections.Generic;
using System.Windows;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.ViewModels;

namespace clawSoft.clawPDF.Views
{
    internal partial class ApplicationSettingsWindow : Window
    {
        public ApplicationSettingsWindow()
        {
            InitializeComponent();
        }

        public ApplicationSettingsWindow(ApplicationSettings applicationSettings,
            ApplicationProperties applicationProperties, IEnumerable<ConversionProfile> conversionProfiles)
            : this()
        {
            GeneralTabUserControl.ViewModel.ApplicationSettings = applicationSettings;
            GeneralTabUserControl.ViewModel.ApplicationProperties = applicationProperties;
            GeneralTabUserControl.PreviewLanguageAction = PreviewLanguageAction;
            TitleTabUserControl.ViewModel.ApplyTitleReplacements(applicationSettings.TitleReplacement);
            DebugTabUserControl.ViewModel.ApplicationSettings = applicationSettings;
            DebugTabUserControl.UpdateSettings = UpdateSettingsAction;
            PrinterTabUserControl.ViewModel.ConversionProfiles = conversionProfiles;
            PrinterTabUserControl.ViewModel.ApplicationSettings = applicationSettings;
        }

        private ApplicationSettingsViewModel ViewModel => (ApplicationSettingsViewModel)DataContext;

        private void UpdateSettingsAction(clawPDFSettings settings)
        {
            GeneralTabUserControl.ViewModel.ApplicationSettings = settings.ApplicationSettings;
            GeneralTabUserControl.ViewModel.ApplicationProperties = settings.ApplicationProperties;
            TitleTabUserControl.ViewModel.ApplyTitleReplacements(settings.ApplicationSettings.TitleReplacement);
            PrinterTabUserControl.ViewModel.ConversionProfiles = settings.ConversionProfiles;
            PrinterTabUserControl.ViewModel.ApplicationSettings = settings.ApplicationSettings;
        }

        private void PreviewLanguageAction()
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
            TranslationHelper.Instance.TranslatorInstance.Translate(PrinterTabUserControl);
            PrinterTabUserControl.UpdateProfilesList();
            TranslationHelper.Instance.TranslatorInstance.Translate(TitleTabUserControl);
            TranslationHelper.Instance.TranslatorInstance.Translate(DebugTabUserControl);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ApplicationSettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
        }

        private void ApplicationSettingsWindow_OnClosed(object sender, EventArgs e)
        {
            TranslationHelper.Instance.RevertTemporaryTranslation();
        }
    }
}