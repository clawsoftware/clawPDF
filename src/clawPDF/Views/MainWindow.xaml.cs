using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.Helper.Logging;

namespace clawSoft.clawPDF.Views
{
    internal partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AppSettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var settings = SettingsHelper.Settings;
            var currentLanguage = settings.ApplicationSettings.Language;

            var tmpSettings = settings.ApplicationSettings.Copy();
            var tmpProperties = settings.ApplicationProperties.Copy();
            var window = new ApplicationSettingsWindow(tmpSettings, tmpProperties,
                settings.ConversionProfiles);

            if (window.ShowDialog() == true)
            {
                settings.ApplicationSettings = tmpSettings;
                settings.ApplicationProperties = tmpProperties;
                if (!string.Equals(currentLanguage, settings.ApplicationSettings.Language,
                    StringComparison.OrdinalIgnoreCase))
                {
                    TranslationHelper.Instance.InitTranslator(settings.ApplicationSettings.Language);
                    TranslationHelper.Instance.TranslatorInstance.Translate(this);
                }

                SettingsHelper.SaveSettings();
            }

            //Translation of profiles are stored in their name property and could have been changed in the AppSettingsWindow
            //To include the current language it must be translated here
            TranslationHelper.Instance.TranslateProfileList(SettingsHelper.Settings.ConversionProfiles);

            LoggingHelper.ChangeLogLevel(settings.ApplicationSettings.LoggingLevel);
        }

        private void ProfileSettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new ProfileSettingsWindow(SettingsHelper.Settings);
            window.ShowDialog();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);

            ApplicationNameText.Text = "clawPDF " + VersionHelper.Instance.FormatWithThreeDigits();

            var welcomeSettingsHelper = new WelcomeSettingsHelper();
            if (welcomeSettingsHelper.IsFirstRun())
                welcomeSettingsHelper.SetCurrentApplicationVersionAsWelcomeVersionInRegistry();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new AboutWindow();
            w.ShowDialog();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            ShowUrlInBrowser(Urls.clawPDFWikiURL);
        }

        private void HeartButton_Click(object sender, RoutedEventArgs e)
        {
            ShowUrlInBrowser(Urls.clawSoftSponsorUrl);
        }

        private void ShowUrlInBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Win32Exception)
            {
            }
            catch (FileNotFoundException)
            {
            }
        }

        private void MainWindow_OnDragEnter(object sender, DragEventArgs e)
        {
            DragAndDropHelper.DragEnter(e);
        }

        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            DragAndDropHelper.Drop(e);
        }

        public static bool? ShowDialogTopMost()
        {
            var w = new MainWindow();
            return TopMostHelper.ShowDialogTopMost(w, true);
        }

        private void MainWindow_OnClose(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}