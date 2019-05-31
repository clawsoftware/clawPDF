using System;
using System.Collections.Generic;
using System.Text;
using clawSoft.clawPDF.Core.Settings.Enums;
using pdfforge.DataStorage;

// Custom Code starts here
// START_CUSTOM_SECTION:INCLUDES

// END_CUSTOM_SECTION:INCLUDES
// Custom Code ends here. Do not edit below

// ! This file is generated automatically.
// ! Do not edit it outside the sections for custom code.
// ! These changes will be deleted during the next generation run

namespace clawSoft.clawPDF.Core.Settings
{
    /// <summary>
    ///     clawPDF application settings
    /// </summary>
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            Init();
        }

        public IList<ApiAccess> ApiAccess { get; set; }
        public IList<PrinterMapping> PrinterMappings { get; set; }
        public IList<TitleReplacement> TitleReplacement { get; set; }
        public bool AskSwitchDefaultPrinter { get; set; }

        public string Language { get; set; }

        public string LastUsedProfileGuid { get; set; }

        public LoggingLevel LoggingLevel { get; set; }

        public string PrimaryPrinter { get; set; }

        public UpdateInterval UpdateInterval { get; set; }

        private void Init()
        {
            ApiAccess = new List<ApiAccess>();
            PrinterMappings = new List<PrinterMapping>();
            TitleReplacement = new List<TitleReplacement>();
            AskSwitchDefaultPrinter = true;
            Language = "";
            LastUsedProfileGuid = "DefaultGuid";
            LoggingLevel = LoggingLevel.Error;
            PrimaryPrinter = "clawPDF";
            UpdateInterval = UpdateInterval.Weekly;
        }

        public void ReadValues(Data data, string path)
        {
            try
            {
                var numClasses = int.Parse(data.GetValue(@"" + path + @"ApiAccess\numClasses"));
                for (var i = 0; i < numClasses; i++)
                {
                    var tmp = new ApiAccess();
                    tmp.ReadValues(data, @"" + path + @"ApiAccess\" + i + @"\");
                    ApiAccess.Add(tmp);
                }
            }
            catch
            {
            }

            try
            {
                var numClasses = int.Parse(data.GetValue(@"" + path + @"PrinterMappings\numClasses"));
                for (var i = 0; i < numClasses; i++)
                {
                    var tmp = new PrinterMapping();
                    tmp.ReadValues(data, @"" + path + @"PrinterMappings\" + i + @"\");
                    PrinterMappings.Add(tmp);
                }
            }
            catch
            {
            }

            try
            {
                var numClasses = int.Parse(data.GetValue(@"" + path + @"TitleReplacement\numClasses"));
                for (var i = 0; i < numClasses; i++)
                {
                    var tmp = new TitleReplacement();
                    tmp.ReadValues(data, @"" + path + @"TitleReplacement\" + i + @"\");
                    TitleReplacement.Add(tmp);
                }
            }
            catch
            {
            }

            try
            {
                AskSwitchDefaultPrinter = bool.Parse(data.GetValue(@"" + path + @"AskSwitchDefaultPrinter"));
            }
            catch
            {
                AskSwitchDefaultPrinter = true;
            }

            try
            {
                Language = Data.UnescapeString(data.GetValue(@"" + path + @"Language"));
            }
            catch
            {
                Language = "";
            }

            try
            {
                LastUsedProfileGuid = Data.UnescapeString(data.GetValue(@"" + path + @"LastUsedProfileGuid"));
            }
            catch
            {
                LastUsedProfileGuid = "DefaultGuid";
            }

            try
            {
                LoggingLevel =
                    (LoggingLevel)Enum.Parse(typeof(LoggingLevel), data.GetValue(@"" + path + @"LoggingLevel"));
            }
            catch
            {
                LoggingLevel = LoggingLevel.Error;
            }

            try
            {
                PrimaryPrinter = Data.UnescapeString(data.GetValue(@"" + path + @"PrimaryPrinter"));
            }
            catch
            {
                PrimaryPrinter = "clawPDF";
            }

            try
            {
                UpdateInterval = (UpdateInterval)Enum.Parse(typeof(UpdateInterval),
                    data.GetValue(@"" + path + @"UpdateInterval"));
            }
            catch
            {
                UpdateInterval = UpdateInterval.Weekly;
            }
        }

        public void StoreValues(Data data, string path)
        {
            for (var i = 0; i < ApiAccess.Count; i++)
            {
                var tmp = ApiAccess[i];
                tmp.StoreValues(data, @"" + path + @"ApiAccess\" + i + @"\");
            }

            data.SetValue(@"" + path + @"ApiAccess\numClasses", ApiAccess.Count.ToString());

            for (var i = 0; i < PrinterMappings.Count; i++)
            {
                var tmp = PrinterMappings[i];
                tmp.StoreValues(data, @"" + path + @"PrinterMappings\" + i + @"\");
            }

            data.SetValue(@"" + path + @"PrinterMappings\numClasses", PrinterMappings.Count.ToString());

            for (var i = 0; i < TitleReplacement.Count; i++)
            {
                var tmp = TitleReplacement[i];
                tmp.StoreValues(data, @"" + path + @"TitleReplacement\" + i + @"\");
            }

            data.SetValue(@"" + path + @"TitleReplacement\numClasses", TitleReplacement.Count.ToString());

            data.SetValue(@"" + path + @"AskSwitchDefaultPrinter", AskSwitchDefaultPrinter.ToString());
            data.SetValue(@"" + path + @"Language", Data.EscapeString(Language));
            data.SetValue(@"" + path + @"LastUsedProfileGuid", Data.EscapeString(LastUsedProfileGuid));
            data.SetValue(@"" + path + @"LoggingLevel", LoggingLevel.ToString());
            data.SetValue(@"" + path + @"PrimaryPrinter", Data.EscapeString(PrimaryPrinter));
            data.SetValue(@"" + path + @"UpdateInterval", UpdateInterval.ToString());
        }

        public ApplicationSettings Copy()
        {
            var copy = new ApplicationSettings();

            copy.ApiAccess = new List<ApiAccess>();
            for (var i = 0; i < ApiAccess.Count; i++) copy.ApiAccess.Add(ApiAccess[i].Copy());

            copy.PrinterMappings = new List<PrinterMapping>();
            for (var i = 0; i < PrinterMappings.Count; i++) copy.PrinterMappings.Add(PrinterMappings[i].Copy());

            copy.TitleReplacement = new List<TitleReplacement>();
            for (var i = 0; i < TitleReplacement.Count; i++) copy.TitleReplacement.Add(TitleReplacement[i].Copy());

            copy.AskSwitchDefaultPrinter = AskSwitchDefaultPrinter;
            copy.Language = Language;
            copy.LastUsedProfileGuid = LastUsedProfileGuid;
            copy.LoggingLevel = LoggingLevel;
            copy.PrimaryPrinter = PrimaryPrinter;
            copy.UpdateInterval = UpdateInterval;

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is ApplicationSettings)) return false;
            var v = o as ApplicationSettings;

            if (ApiAccess.Count != v.ApiAccess.Count) return false;
            for (var i = 0; i < ApiAccess.Count; i++)
                if (!ApiAccess[i].Equals(v.ApiAccess[i]))
                    return false;

            if (PrinterMappings.Count != v.PrinterMappings.Count) return false;
            for (var i = 0; i < PrinterMappings.Count; i++)
                if (!PrinterMappings[i].Equals(v.PrinterMappings[i]))
                    return false;

            if (TitleReplacement.Count != v.TitleReplacement.Count) return false;
            for (var i = 0; i < TitleReplacement.Count; i++)
                if (!TitleReplacement[i].Equals(v.TitleReplacement[i]))
                    return false;

            if (!AskSwitchDefaultPrinter.Equals(v.AskSwitchDefaultPrinter)) return false;
            if (!Language.Equals(v.Language)) return false;
            if (!LastUsedProfileGuid.Equals(v.LastUsedProfileGuid)) return false;
            if (!LoggingLevel.Equals(v.LoggingLevel)) return false;
            if (!PrimaryPrinter.Equals(v.PrimaryPrinter)) return false;
            if (!UpdateInterval.Equals(v.UpdateInterval)) return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < ApiAccess.Count; i++) sb.AppendLine(ApiAccess.ToString());

            for (var i = 0; i < PrinterMappings.Count; i++) sb.AppendLine(PrinterMappings.ToString());

            for (var i = 0; i < TitleReplacement.Count; i++) sb.AppendLine(TitleReplacement.ToString());

            sb.AppendLine("AskSwitchDefaultPrinter=" + AskSwitchDefaultPrinter);
            sb.AppendLine("Language=" + Language);
            sb.AppendLine("LastUsedProfileGuid=" + LastUsedProfileGuid);
            sb.AppendLine("LoggingLevel=" + LoggingLevel);
            sb.AppendLine("PrimaryPrinter=" + PrimaryPrinter);
            sb.AppendLine("UpdateInterval=" + UpdateInterval);

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        // Custom Code starts here
        // START_CUSTOM_SECTION:GENERAL
        public ApiAccess GetApiAccess(string AccountName, ApiProvider ApiProvider)
        {
            foreach (var apiAccess in ApiAccess)
                if (apiAccess.AccountName == AccountName)
                    if (apiAccess.ProviderName == ApiProvider)
                        return apiAccess;

            return null;
        }

        public ApiAccess GetApiAccess(string AccountName)
        {
            foreach (var apiAccess in ApiAccess)
                if (apiAccess.AccountName == AccountName)
                    return apiAccess;
            return null;
        }

        public ApiAccess GetApiAccess(ApiProvider ApiProvider)
        {
            foreach (var apiAccess in ApiAccess)
                if (apiAccess.ProviderName == ApiProvider)
                    return apiAccess;

            return null;
        }

        // END_CUSTOM_SECTION:GENERAL
        // Custom Code ends here. Do not edit below
    }
}