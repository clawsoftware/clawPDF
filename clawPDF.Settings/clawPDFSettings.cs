using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pdfforge.DataStorage;
using pdfforge.DataStorage.Storage;

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
    ///     Container class for clawPDF settings and profiles
    /// </summary>
    public class clawPDFSettings
    {
        private readonly Data data = Data.CreateDataStorage();
        private readonly IStorage storage;

        public clawPDFSettings(IStorage storage)
        {
            Init();

            this.storage = storage;
            data = Data.CreateDataStorage();
        }

        public ApplicationProperties ApplicationProperties { get; set; }

        /// <summary>
        ///     clawPDF application settings
        /// </summary>
        public ApplicationSettings ApplicationSettings { get; set; }

        public IList<ConversionProfile> ConversionProfiles { get; set; }

        private void Init()
        {
            ApplicationProperties = new ApplicationProperties();
            ApplicationSettings = new ApplicationSettings();
            ConversionProfiles = new List<ConversionProfile>();
        }

        public bool LoadData(IStorage storage, string path)
        {
            try
            {
                data.Clear();
                storage.SetData(data);
                storage.ReadData(path);
                ReadValues(data, "");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoadData(string path)
        {
            return LoadData(storage, path);
        }

        public bool SaveData(IStorage storage, string path)
        {
            try
            {
                data.Clear();
                StoreValues(data, "");
                storage.SetData(data);
                storage.WriteData(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveData(string path)
        {
            return SaveData(storage, path);
        }

        public void ReadValues(Data data, string path)
        {
            ApplicationProperties.ReadValues(data, path + @"ApplicationProperties\");
            ApplicationSettings.ReadValues(data, path + @"ApplicationSettings\");

            try
            {
                var numClasses = int.Parse(data.GetValue(@"" + path + @"ConversionProfiles\numClasses"));
                for (var i = 0; i < numClasses; i++)
                {
                    var tmp = new ConversionProfile();
                    tmp.ReadValues(data, @"" + path + @"ConversionProfiles\" + i + @"\");
                    ConversionProfiles.Add(tmp);
                }
            }
            catch
            {
            }
        }

        public void StoreValues(Data data, string path)
        {
            ApplicationProperties.StoreValues(data, path + @"ApplicationProperties\");
            ApplicationSettings.StoreValues(data, path + @"ApplicationSettings\");

            for (var i = 0; i < ConversionProfiles.Count; i++)
            {
                var tmp = ConversionProfiles[i];
                tmp.StoreValues(data, @"" + path + @"ConversionProfiles\" + i + @"\");
            }

            data.SetValue(@"" + path + @"ConversionProfiles\numClasses", ConversionProfiles.Count.ToString());
        }

        public clawPDFSettings Copy()
        {
            var copy = new clawPDFSettings(storage);

            copy.ApplicationProperties = ApplicationProperties.Copy();
            copy.ApplicationSettings = ApplicationSettings.Copy();

            copy.ConversionProfiles = new List<ConversionProfile>();
            for (var i = 0; i < ConversionProfiles.Count; i++)
                copy.ConversionProfiles.Add(ConversionProfiles[i].Copy());

            return copy;
        }

        public override bool Equals(object o)
        {
            if (!(o is clawPDFSettings)) return false;
            var v = o as clawPDFSettings;

            if (!ApplicationProperties.Equals(v.ApplicationProperties)) return false;
            if (!ApplicationSettings.Equals(v.ApplicationSettings)) return false;

            if (ConversionProfiles.Count != v.ConversionProfiles.Count) return false;
            for (var i = 0; i < ConversionProfiles.Count; i++)
                if (!ConversionProfiles[i].Equals(v.ConversionProfiles[i]))
                    return false;

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("[ApplicationProperties]");
            sb.AppendLine(ApplicationProperties.ToString());
            sb.AppendLine("[ApplicationSettings]");
            sb.AppendLine(ApplicationSettings.ToString());

            for (var i = 0; i < ConversionProfiles.Count; i++) sb.AppendLine(ConversionProfiles.ToString());

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        // Custom Code starts here
        // START_CUSTOM_SECTION:GENERAL

        /// <summary>
        ///     Function that returns a profile from the inner Conversionprofiles(list) by a given guid.
        /// </summary>
        /// <param name="guid">Guid to look for</param>
        /// <returns>(First) Conversionprofile with the given guid. Returns null, if no profile with given guid exists.</returns>
        public ConversionProfile GetProfileByGuid(string guid)
        {
            if (ConversionProfiles.Count <= 0)
                return null;

            return ConversionProfiles.FirstOrDefault(p => p.Guid == guid);
        }

        /// <summary>
        ///     Function that returns a profile from the inner Conversionprofiles(list) by a given name.
        /// </summary>
        /// <param name="name">Profilename</param>
        /// <returns>(First) Conversionprofile with the given name. Returns null, if no profile with given name exists.</returns>
        public ConversionProfile GetProfileByName(string name)
        {
            if (ConversionProfiles.Count <= 0)
                return null;

            return ConversionProfiles.FirstOrDefault(p => p.Name == name);
        }

        /// <summary>
        ///     Function that returns the last used profile, according to the LastUsedProfileGuid of the ApplicationSettings.
        ///     If the Conversionprofiles(list) does not contain a profile with the LastUsedProfileGuid (because it was deleted)
        ///     or the last guid is null the function will null.
        /// </summary>
        /// <returns>Returns last used profile. Returns null if ConversionProfiles is empty or no last profile is known.</returns>
        public ConversionProfile GetLastUsedProfile()
        {
            if (ConversionProfiles.Count <= 0)
                return null;

            if (ApplicationSettings.LastUsedProfileGuid == null)
                return null;

            return GetProfileByGuid(ApplicationSettings.LastUsedProfileGuid);
        }

        /// <summary>
        ///     Function that returns the last used profile, according to the LastUsedProfileGuid of the ApplicationSettings.
        ///     If the Conversionprofiles(list) does not contain a profile with the LastUsedProfileGuid (because it was deleted)
        ///     or the last guid is null the function will return the first profile.
        /// </summary>
        /// <returns>Returns last used or first profile. Returns null if ConversionProfiles is empty.</returns>
        public ConversionProfile GetLastUsedOrFirstProfile()
        {
            if (ConversionProfiles.Count <= 0)
                return null;

            if (ApplicationSettings.LastUsedProfileGuid == null)
                return ConversionProfiles[0];

            var p = GetProfileByGuid(ApplicationSettings.LastUsedProfileGuid);
            if (p == null)
                return ConversionProfiles[0];

            return p;
        }

        /// <summary>
        ///     Sorts the inner list "ConversionProfiles", firstly considering their properties and then the alphabetical order
        ///     temporary > default > other
        /// </summary>
        public void SortConversionProfiles()
        {
            //((List<ConversionProfile>)ConversionProfiles).Sort(CompareTemporaryFirstDefaultSecond);
            ((List<ConversionProfile>)ConversionProfiles).Sort(new ProfileSorter().Compare);
        }

        /// <summary>
        ///     Find the first printer mapping for this profile
        /// </summary>
        /// <param name="profile">The profile to look for</param>
        /// <returns>The first printer mapping that is found or null.</returns>
        public PrinterMapping GetPrinterByProfile(ConversionProfile profile)
        {
            foreach (var pm in ApplicationSettings.PrinterMappings)
                if (pm.ProfileGuid.Equals(profile.Guid))
                    return pm;

            return null;
        }

        public IStorage GetStorage()
        {
            return storage;
        }

        public bool LoadData(IStorage storage, string path, Action<Data> dataValidation)
        {
            try
            {
                data.Clear();
                storage.SetData(data);
                storage.ReadData(path);
                dataValidation(data);
                ReadValues(data, "");
                return true;
            }
            catch
            {
                return false;
            }
        }

        // END_CUSTOM_SECTION:GENERAL
        // Custom Code ends here. Do not edit below
    }
}