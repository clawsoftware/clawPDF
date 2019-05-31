using System.Windows;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.ViewModels;
using clawSoft.clawPDF.WindowsApi;

namespace clawSoft.clawPDF.Views
{
    internal partial class PrintJobWindow
    {
        private clawPDFSettings _settings = SettingsHelper.Settings;

        public PrintJobWindow()
        {
            InitializeComponent();
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = (PrintJobViewModel)DataContext;

            TopMostHelper.UndoTopMostWindow(this);
            _settings.ApplicationSettings.LastUsedProfileGuid = vm.SelectedProfile.Guid;

            var window = new ProfileSettingsWindow(_settings);
            if (window.ShowDialog() == true)
            {
                _settings = window.Settings;

                vm.Profiles = _settings.ConversionProfiles;
                vm.ApplicationSettings = _settings.ApplicationSettings;
                vm.SelectProfileByGuid(_settings.ApplicationSettings.LastUsedProfileGuid);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
            FlashWindow.Flash(this, 3);
        }

        private void CommandButtons_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            DragAndDropHelper.DragEnter(e);
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            DragAndDropHelper.Drop(e);
        }
    }
}