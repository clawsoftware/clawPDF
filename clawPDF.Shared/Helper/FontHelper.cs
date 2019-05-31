using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using TrueTypeFontInfo;

namespace clawSoft.clawPDF.Shared.Helper
{
    internal static class FontHelper
    {
        private static Dictionary<string, string> _psFontDictionary;

        public static string FindPostScriptName(string fontFamilyName)
        {
            if (_psFontDictionary == null)
                _psFontDictionary = BuildFontDictionary();

            if (_psFontDictionary.ContainsKey(fontFamilyName))
                return _psFontDictionary[fontFamilyName];

            return null;
        }

        public static bool PostscriptFontExists(string postscriptFontName)
        {
            if (_psFontDictionary == null)
                _psFontDictionary = BuildFontDictionary();

            return _psFontDictionary.ContainsValue(postscriptFontName);
        }

        private static Dictionary<string, string> BuildFontDictionary()
        {
            var fontDictionary = new Dictionary<string, string>();

            var fontsKey =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", false);

            if (fontsKey == null)
                return fontDictionary;

            var valueNames = fontsKey.GetValueNames();
            var strFontsFolder =
                Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName,
                    "Fonts");

            foreach (var fontKey in valueNames)
            {
                var registryFontFileName = (string)fontsKey.GetValue(fontKey);

                if (registryFontFileName != null && Path.GetExtension(registryFontFileName)
                        .Equals(".ttf", StringComparison.InvariantCultureIgnoreCase))
                {
                    var trueType = new TrueType();
                    trueType.Load(Path.Combine(strFontsFolder, registryFontFileName));

                    fontDictionary[trueType.FontFamilyName] = trueType.PostScriptFontName;
                }
            }

            return fontDictionary;
        }
    }
}