using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.Views;
using clawSoft.clawPDF.ViewModels;
using clawSoft.clawPDF.WindowsApi;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Views
{
    internal partial class PrintJobWindow
    {
        private clawPDFSettings _settings = SettingsHelper.Settings;
        private PrintJobViewModel vm;
        private static readonly TranslationHelper TranslationHelper = TranslationHelper.Instance;

        public PrintJobWindow()
        {
            InitializeComponent();
            vm = new PrintJobViewModel();
            DataContext = vm;
            PageOrientationComboBox.ItemsSource = PageOrientationValues;
            ColorModelComboBox.ItemsSource = ColorModelValues;
        }

        public static IEnumerable<EnumValue<PageOrientation>> PageOrientationValues => 
            TranslationHelper.TranslatorInstance.GetEnumTranslation<PageOrientation>();

        public static IEnumerable<EnumValue<ColorModel>> ColorModelValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<ColorModel>();

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
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
            if (vm.SelectedProfile.PdfSettings.Security.Enabled)
            {
                PasswordTab.Background = new SolidColorBrush(Colors.LightBlue);
            }
            else
            {
                PasswordTab.Background = null;
            }
            SecurityCheckBox.IsChecked = vm.SelectedProfile.PdfSettings.Security.Enabled;
            UserPasswordCheckBox.IsChecked = vm.SelectedProfile.PdfSettings.Security.RequireUserPassword;
            OCRLanguage.Text = vm.SelectedProfile.OCRSettings.OCRLanguage;
            PageOrientationComboBox.SelectedValue = vm.SelectedProfile.PdfSettings.PageOrientation;
            PageOrientationComboBox.SelectedValuePath = "Value";
            PageOrientationComboBox.DisplayMemberPath = "Name";
            ColorModelComboBox.SelectedValue = vm.SelectedProfile.PdfSettings.ColorModel;
            ColorModelComboBox.SelectedValuePath = "Value";
            ColorModelComboBox.DisplayMemberPath = "Name";
        }

        private void SecurityCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.Enabled = true;
            vm.SelectedProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Aes128Bit;
            PasswordTab.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void SecurityCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.Enabled = false;
            PasswordTab.Background = null;
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

        private void PageOrientationComboBoxChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(PageOrientationComboBox.SelectedIndex == 0)
            {
                vm.SelectedProfile.PdfSettings.PageOrientation = PageOrientation.Automatic;
            }
            else if(PageOrientationComboBox.SelectedIndex == 1)
            {
                vm.SelectedProfile.PdfSettings.PageOrientation = PageOrientation.Portrait;
            }
            else if (PageOrientationComboBox.SelectedIndex == 2)
            {
                vm.SelectedProfile.PdfSettings.PageOrientation = PageOrientation.Landscape;
            }
        }

        private void ColorModelComboBoxChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(ColorModelComboBox.SelectedIndex == 0)
            {
                vm.SelectedProfile.PdfSettings.ColorModel = ColorModel.Cmyk;
            }
            else if (ColorModelComboBox.SelectedIndex == 1)
            {
                vm.SelectedProfile.PdfSettings.ColorModel = ColorModel.Rgb;
            }
            else if (ColorModelComboBox.SelectedIndex == 2)
            {
                vm.SelectedProfile.PdfSettings.ColorModel = ColorModel.Gray;
            }
        }

        private void openTessdataSource_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\tessdata");
        }
    }
}