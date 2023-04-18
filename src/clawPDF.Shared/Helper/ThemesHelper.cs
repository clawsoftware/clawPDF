using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace clawSoft.clawPDF.Shared.Helper
{
    public class ThemeHelper
    {
        public static Core.Settings.Enums.Theme CurrentTheme { get; set; } = Core.Settings.Enums.Theme.Light;

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

        public static void ChangeTheme()
        {
            ResourceDictionary newTheme = new ResourceDictionary();

            if (CurrentTheme == Core.Settings.Enums.Theme.Light)
            {
                newTheme.Source = new Uri("/Themes/ColourfulLightTheme.xaml", UriKind.Relative);
            }
            else if (CurrentTheme == Core.Settings.Enums.Theme.Dark)
            {
                newTheme.Source = new Uri("/Themes/ColourfulDarkTheme.xaml", UriKind.Relative);
            }
            else if (CurrentTheme == Core.Settings.Enums.Theme.System)
            {
                if (GetCurrentWindowsTheme() == WindowsTheme.Unknown)
                {
                    newTheme.Source = new Uri("/Themes/ColourfulLightTheme.xaml", UriKind.Relative);
                }
                else if (GetCurrentWindowsTheme() == WindowsTheme.Dark)
                {
                    newTheme.Source = new Uri("/Themes/ColourfulDarkTheme.xaml", UriKind.Relative);
                }
                else
                {
                    newTheme.Source = new Uri("/Themes/ColourfulLightTheme.xaml", UriKind.Relative);
                }
            }
            else
            {
                newTheme.Source = new Uri("/Themes/ColourfulLightTheme.xaml", UriKind.Relative);
            }

            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }

        public static void ChangeTitleBar(IntPtr hwnd)
        {
            if (CurrentTheme == Core.Settings.Enums.Theme.Light)
            {
                DwmAPI.SetWindowTheme(hwnd, false);
            }
            else if (CurrentTheme == Core.Settings.Enums.Theme.Dark)
            {
                DwmAPI.SetWindowTheme(hwnd, true);
            }
            else if (CurrentTheme == Core.Settings.Enums.Theme.System)
            {
                if (GetCurrentWindowsTheme() == WindowsTheme.Unknown)
                {
                    DwmAPI.SetWindowTheme(hwnd, false);
                }
                else if (GetCurrentWindowsTheme() == WindowsTheme.Dark)
                {
                    DwmAPI.SetWindowTheme(hwnd, true);
                }
                else
                {
                    DwmAPI.SetWindowTheme(hwnd, false);
                }
            }
            else
            {
                DwmAPI.SetWindowTheme(hwnd, false);
            }
        }
    }
}

public static class DwmAPI
{
    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;

    public static void SetWindowTheme(IntPtr hwnd, bool darkMode)
    {
        try
        {
            int darkModeValue = darkMode ? 1 : 0;
            int attribute = Environment.OSVersion.Version.Build >= 19041 ? DWMWA_USE_IMMERSIVE_DARK_MODE : DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;

            int result = DwmSetWindowAttribute(hwnd, attribute, ref darkModeValue, sizeof(int));

            if (result != 0)
            {
            }
        }
        catch { }
    }
}