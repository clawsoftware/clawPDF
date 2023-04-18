using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;
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
        private static System.Windows.Media.Brush TabColor;

        public PrintJobWindow()
        {
            InitializeComponent();
            TabColor = PasswordTab.Background;
            vm = new PrintJobViewModel();
            DataContext = vm;
            PageOrientationComboBox.ItemsSource = PageOrientationValues;
            ColorModelComboBox.ItemsSource = ColorModelValues;
            EncryptionLevelComboBox.ItemsSource = EncryptionLevelValues;
        }

        public static IEnumerable<EnumValue<PageOrientation>> PageOrientationValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<PageOrientation>();

        public static IEnumerable<EnumValue<ColorModel>> ColorModelValues =>
            TranslationHelper.TranslatorInstance.GetEnumTranslation<ColorModel>();

        public static List<EncryptionLevel> EncryptionLevelValues = Enum.GetValues(typeof(EncryptionLevel)).Cast<EncryptionLevel>().ToList();

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
            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            ThemeHelper.ChangeTitleBar(hWnd);

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
                PasswordTab.Background = (System.Windows.Media.Brush)FindResource("ControlPrimaryColourBackground");
            }
            else
            {
                PasswordTab.Background = TabColor;
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
            if (vm.SelectedProfile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Rc40Bit)
            {
                EncryptionLevelComboBox.SelectedIndex = 0;
            }
            else if (vm.SelectedProfile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Rc128Bit)
            {
                EncryptionLevelComboBox.SelectedIndex = 1;
            }
            else if (vm.SelectedProfile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes128Bit)
            {
                EncryptionLevelComboBox.SelectedIndex = 2;
            }
            else if (vm.SelectedProfile.PdfSettings.Security.EncryptionLevel == EncryptionLevel.Aes256Bit)
            {
                EncryptionLevelComboBox.SelectedIndex = 3;
            }
        }

        private void SecurityCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.Enabled = true;
            vm.SelectedProfile.PdfSettings.Security.RestrictPrintingToLowQuality = false;
            PasswordTab.Background = (System.Windows.Media.Brush)FindResource("ControlPrimaryColourBackground");
        }

        private void SecurityCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            vm.SelectedProfile.PdfSettings.Security.Enabled = false;
            PasswordTab.Background = TabColor;
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
            if (PageOrientationComboBox.SelectedIndex == 0)
            {
                vm.SelectedProfile.PdfSettings.PageOrientation = PageOrientation.Automatic;
            }
            else if (PageOrientationComboBox.SelectedIndex == 1)
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
            if (ColorModelComboBox.SelectedIndex == 0)
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

        private void EncryptionLevelComboBoxChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (EncryptionLevelComboBox.SelectedIndex == 0)
            {
                vm.SelectedProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Rc40Bit;
            }
            else if (EncryptionLevelComboBox.SelectedIndex == 1)
            {
                vm.SelectedProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Rc128Bit;
            }
            else if (EncryptionLevelComboBox.SelectedIndex == 2)
            {
                vm.SelectedProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Aes128Bit;
            }
            else if (EncryptionLevelComboBox.SelectedIndex == 3)
            {
                vm.SelectedProfile.PdfSettings.Security.EncryptionLevel = EncryptionLevel.Aes256Bit;
            }
        }

        private void openTessdataSource_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\tessdata");
        }
    }
}