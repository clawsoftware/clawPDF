using System;
using System.Collections.Generic;
using System.Linq;
using clawSoft.clawPDF.Core.Settings.Enums;
using pdfforge.DataStorage;

namespace clawSoft.clawPDF.Helper
{
    /// <summary>
    ///     The SettingsUpgrader class performs updates to the clawPDF settings.
    ///     This is done after loading the raw data and before loading them into the DataStorage classes.
    ///     There is one update method from each version to the next (0 to 1, 1 to 2 etc.) and they are called subsequently, if
    ///     required.
    /// </summary>
    internal class SettingsUpgrader : DataUpgrader
    {
        public const string VersionSettingPath = @"ApplicationProperties\SettingsVersion";
        private readonly List<Action> _upgradeMethods = new List<Action>();

        public SettingsUpgrader(Data settingsData)
        {
            Data = settingsData;

            _upgradeMethods.Add(UpgradeV0ToV1);
            _upgradeMethods.Add(UpgradeV1ToV2);
            _upgradeMethods.Add(UpgradeV2ToV3);
            _upgradeMethods.Add(UpgradeV3ToV4);
            _upgradeMethods.Add(UpgradeV4ToV5);
        }

        public int SettingsVersion
        {
            get
            {
                var versionString = Data.GetValue(VersionSettingPath);
                return GetInt(versionString) ?? 0;
            }
        }

        public void Upgrade(int targetVersion)
        {
            for (var i = SettingsVersion; i < Math.Min(targetVersion, _upgradeMethods.Count); i++)
            {
                // Call upgrade methods subsequently, starting with the current version
                var upgradeMethod = _upgradeMethods[i];
                upgradeMethod();
            }
        }

        public bool RequiresUpgrade(int targetVersion)
        {
            return targetVersion > SettingsVersion;
        }

        private void UpgradeV0ToV1()
        {
            MoveSettingInAllProfiles("DefaultFormat", "OutputFormat");
            MapSettingInAllProfiles("PdfSettings\\Security\\" + "EncryptionLevel", MapEncryptionNamesV1);
            ApplyNewSettingInAllProfiles("TitleTemplate", "<PrintJobName>");
            ApplyNewSettingInAllProfiles("AuthorTemplate", "<PrintJobAuthor>");

            Data.SetValue(VersionSettingPath, "1");
        }

        private void UpgradeV1ToV2()
        {
            MoveSettingInAllProfiles(@"CoverPage\AddBackground", @"BackgroundPage\OnCover");
            MoveSettingInAllProfiles(@"AttachmentPage\AddBackground", @"BackgroundPage\OnAttachment");
            MoveValue(@"ApplicationSettings\LastUsedProfilGuid", @"ApplicationSettings\LastUsedProfileGuid");
            Data.SetValue(VersionSettingPath, "2");
        }

        private void UpgradeV2ToV3()
        {
            MapSettingInAllProfiles(@"OutputFormat", MapOutputformatPdfA_V3);
            Data.SetValue(VersionSettingPath, "3");
        }

        private void UpgradeV3ToV4()
        {
            Data.SetValue(VersionSettingPath, "4");
            MapSettingInAllProfiles(@"TiffSettings\Color", MapTiffColorBlackWhite_V4);
        }

        private void UpgradeV4ToV5()
        {
            Data.SetValue(VersionSettingPath, "5");

            string[] startReplacements =
            {
                "Microsoft Word - ",
                "Microsoft PowerPoint - ",
                "Microsoft Excel - "
            };

            string[] endReplacements =
            {
                ".doc",
                ".docx",
                ".xls",
                ".xlsx",
                ".ppt",
                ".pptx",
                ".png",
                ".jpg",
                ".jpeg",
                ".txt - Editor",
                " - Editor",
                ".txt",
                ".tif",
                ".tiff"
            };

            int replacementCount;
            if (int.TryParse(Data.GetValue(@"ApplicationSettings\TitleReplacement\numClasses"), out replacementCount))
                for (var i = 0; i < replacementCount; i++)
                {
                    var section = $"ApplicationSettings\\TitleReplacement\\{i}\\";

                    var type = ReplacementType.Replace;
                    var search = Data.GetValue(section + "Search");

                    if (startReplacements.Contains(search)) type = ReplacementType.Start;

                    if (endReplacements.Contains(search)) type = ReplacementType.End;

                    Data.SetValue(section + "ReplacementType", type.ToString());
                }
        }

        private string MapTiffColorBlackWhite_V4(string s)
        {
            if (s.Equals("BlackWhite", StringComparison.OrdinalIgnoreCase))
                return "BlackWhiteG4Fax";
            return s;
        }

        private string MapOutputformatPdfA_V3(string s)
        {
            if (s.Equals("PdfA", StringComparison.OrdinalIgnoreCase))
                return "PdfA2B";
            return s;
        }

        private string MapEncryptionNamesV1(string s)
        {
            switch (s)
            {
                case "Low40Bit":
                    return "Rc40Bit";

                case "Medium128Bit":
                    return "Rc128Bit";

                case "High128BitAes":
                    return "Aes128Bit";
            }

            return "Rc128Bit";
        }

        private void MoveSettingInAllProfiles(string oldPath, string newPath)
        {
            var numProfiles = GetInt(Data.GetValue(@"ConversionProfiles\numClasses"));

            if (numProfiles != null)
                for (var i = 0; i < numProfiles; i++)
                {
                    var path = string.Format(@"ConversionProfiles\{0}\", i);
                    MoveValue(path + oldPath, path + newPath);
                }
        }

        private void MapSettingInAllProfiles(string path, Func<string, string> mapFunction)
        {
            var numProfiles = GetInt(Data.GetValue(@"ConversionProfiles\numClasses"));

            if (numProfiles != null)
                for (var i = 0; i < numProfiles; i++)
                {
                    var p = string.Format(@"ConversionProfiles\{0}\" + path, i);
                    MapValue(p, mapFunction);
                }
        }

        private void ApplyNewSettingInAllProfiles(string path, string defaultValue)
        {
            var numProfiles = GetInt(Data.GetValue(@"ConversionProfiles\numClasses"));

            if (numProfiles != null)
                for (var i = 0; i < numProfiles; i++)
                {
                    var p = string.Format(@"ConversionProfiles\{0}\" + path, i);
                    Data.SetValue(p, defaultValue);
                }
        }

        private int? GetInt(string s)
        {
            int i;

            if (!int.TryParse(s, out i))
                return null;
            return i;
        }
    }
}