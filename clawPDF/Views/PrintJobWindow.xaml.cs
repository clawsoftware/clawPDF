using System.ComponentModel;
using System.Windows;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.ViewModels;
using clawSoft.clawPDF.WindowsApi;

namespace clawSoft.clawPDF.Views
{
    internal partial class PrintJobWindow
    {
        private clawPDFSettings _settings = SettingsHelper.Settings;
        private PrintJobViewModel vm;

        public PrintJobWindow()
        {
            InitializeComponent();
            vm = new PrintJobViewModel();
            DataContext = vm;
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            //var vm = (PrintJobViewModel)DataContext;

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

        private void SecurityPasswordsButton_OnClick(object sender, RoutedEventArgs e)
        {
            //var vm = (PrintJobViewModel)DataContext;

            var askUserPassword = vm.SelectedProfile.PdfSettings.Security.RequireUserPassword;

            var pwWindow = new EncryptionPasswordsWindow(EncryptionPasswordMiddleButton.Remove, true, askUserPassword);
            pwWindow.OwnerPassword = vm.SelectedProfile.PdfSettings.Security.OwnerPassword;
            pwWindow.UserPassword = vm.SelectedProfile.PdfSettings.Security.UserPassword;

            pwWindow.ShowDialogTopMost();

            if (pwWindow.Response == EncryptionPasswordResponse.OK)
            {
                vm.SelectedProfile.PdfSettings.Security.OwnerPassword = pwWindow.OwnerPassword;
                vm.SelectedProfile.PdfSettings.Security.UserPassword = pwWindow.UserPassword;
            }
            else if (pwWindow.Response == EncryptionPasswordResponse.Remove)
            {
                vm.SelectedProfile.PdfSettings.Security.UserPassword = "";
                vm.SelectedProfile.PdfSettings.Security.OwnerPassword = "";
            }
        }

        private void profileChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SecurityCheckBox.IsChecked = vm.SelectedProfile.PdfSettings.Security.Enabled;
            UserPasswordCheckBox.IsChecked = vm.SelectedProfile.PdfSettings.Security.RequireUserPassword;
            OCRLanguage.Text = vm.SelectedProfile.OCRSettings.OCRLanguage;
        }

        private void SecurityCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.Enabled = true;
            vm.SelectedProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Aes128Bit;
        }

        private void SecurityCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.Enabled = false;
        }

        private void UserPasswordCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.RequireUserPassword = true;
        }

        private void UserPasswordCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.RequireUserPassword = true;
        }

        private void OCRLanguageChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            vm.SelectedProfile.OCRSettings.OCRLanguage = OCRLanguage.Text;
        }
    }
}