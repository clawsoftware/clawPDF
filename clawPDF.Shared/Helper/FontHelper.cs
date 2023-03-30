using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Win32;

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
            var strFontsFolder = Path.Combine(Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).FullName, "Fonts");

            string[] files = Directory.GetFiles(strFontsFolder);

            // Iterieren Sie über die Liste der Dateien und geben Sie den Dateinamen aus
            foreach (string file in files)
            {
                if (Path.GetExtension(file).Equals(".ttf", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        fontDictionary[GetFontFamilieName(file).ToString()] = GetPostScriptName(file).ToString();
                    }
                    catch { }
                }
            }

            return fontDictionary;
        }

        public static string GetFontFamilieName(string fileName)
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile(fileName);
            string fontFamilyName = privateFonts.Families[0].Name;
            return fontFamilyName;
        }

        public static string GetPostScriptName(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new BinaryReader(fileStream))
            {
                var sfntVersion = SwapEndian(reader.ReadUInt32());
                var numTables = SwapEndian(reader.ReadUInt16());
                var searchRange = SwapEndian(reader.ReadUInt16());
                var entrySelector = SwapEndian(reader.ReadUInt16());
                var rangeShift = SwapEndian(reader.ReadUInt16());

                for (int i = 0; i < numTables; i++)
                {
                    var tag = new string(reader.ReadChars(4));
                    var checkSum = SwapEndian(reader.ReadUInt32());
                    var offset = SwapEndian(reader.ReadUInt32());
                    var length = SwapEndian(reader.ReadUInt32());

                    if (tag.ToLower() == "name")
                    {
                        reader.BaseStream.Seek(offset, SeekOrigin.Begin);

                        var format = SwapEndian(reader.ReadUInt16());
                        var count = SwapEndian(reader.ReadUInt16());
                        var stringOffset = SwapEndian(reader.ReadUInt16());

                        for (int j = 0; j < count; j++)
                        {
                            var platformID = SwapEndian(reader.ReadUInt16());
                            var encodingID = SwapEndian(reader.ReadUInt16());
                            var languageID = SwapEndian(reader.ReadUInt16());
                            var nameID = SwapEndian(reader.ReadUInt16());
                            var length2 = SwapEndian(reader.ReadUInt16());
                            var offset2 = SwapEndian(reader.ReadUInt16());

                            if (platformID == 3 && encodingID == 1 && nameID == 6)
                            {
                                reader.BaseStream.Seek(offset + stringOffset + offset2, SeekOrigin.Begin);
                                var bytes = reader.ReadBytes(length2);
                                return System.Text.Encoding.ASCII.GetString(bytes).Trim().Replace("\0", "");
                            }
                        }

                        break;
                    }
                }

                return null;
            }
        }

        private static uint SwapEndian(uint value)
        {
            return (uint)IPAddress.HostToNetworkOrder((int)value);
        }

        private static ushort SwapEndian(ushort value)
        {
            return (ushort)IPAddress.HostToNetworkOrder((short)value);
        }
    }
}