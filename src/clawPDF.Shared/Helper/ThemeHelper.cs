using AdonisUI;
using clawSoft.clawPDF.Core.Settings;
using Microsoft.Win32;
using System.Windows;

namespace clawSoft.clawPDF.Shared.Helper
{
    public class ThemeHelper
    {
        private static bool light = true;

        public enum WindowsTheme
        {
            Light,
            Dark,
            Unknown
        }

        public static WindowsTheme GetCurrentWindowsTheme()
        {
            var theme = WindowsTheme.Unknown;
            var registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            if (registryKey != null)
            {
                var value = registryKey.GetValue("AppsUseLightTheme");
                if (value != null && (int)value == 0)
                {
                    theme = WindowsTheme.Dark;
                }
                else if (value != null && (int)value == 1)
                {
                    theme = WindowsTheme.Light;
                }
            }
            return theme;
        }

        public static void ChangeTheme(Core.Settings.Enums.Theme theme)
        {
            if (theme == Core.Settings.Enums.Theme.Light)
            {
                ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.LightColorScheme);
            }
            else if (theme == Core.Settings.Enums.Theme.Dark)
            {
                ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);
            }
            else if (theme == Core.Settings.Enums.Theme.System)
            {
                if (GetCurrentWindowsTheme() == WindowsTheme.Unknown)
                {
                    ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.LightColorScheme);
                }
                else if (GetCurrentWindowsTheme() == WindowsTheme.Dark)
                {
                    ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.DarkColorScheme);
                }
                else
                {
                    ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.LightColorScheme);
                }
            }
            else
            {
                ResourceLocator.SetColorScheme(Application.Current.Resources, ResourceLocator.LightColorScheme);
            }
        }
    }
}