using System;
using System.Collections.Generic;
using System.IO;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Utilities;
using pdfforge.DataStorage;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.Helper
{
    /// <summary>
    ///     TranslationUtil provides functionality that is used in conjunction with the DynamicTranslator classes.
    /// </summary>
    public class TranslationHelper
    {
        private static TranslationHelper _instance;
        private IAssemblyHelper _assemblyHelper = new AssemblyHelper();
        private LanguageLoader _languageLoader;
        private Translator _tmpTranslator;
        private Translator _translator;

        public static TranslationHelper Instance => _instance ?? (_instance = new TranslationHelper());

        /// <summary>
        ///     The Translator Singleton that will be used throughout the application
        /// </summary>
        public Translator TranslatorInstance
        {
            get
            {
                if (_translator == null)
                    throw new ArgumentException("Translator has not been initialized and thus cannot be used");

                return _translator;
            }
        }

        /// <summary>
        ///     Path to where the translation files are located. The default takes into account, that there might be different
        ///     folders to look at, i.e. when run from Visual Studio
        /// </summary>
        public string TranslationPath => LanguageLoader.TranslationFolder ?? "";

        public bool IsInitialized => _translator != null;

        public LanguageLoader LanguageLoader
        {
            get
            {
                if (_languageLoader == null)
                    _languageLoader = BuildLanguageLoader();

                return _languageLoader;
            }
            private set => _languageLoader = value;
        }

        /// <summary>
        ///     Initialize the Translator for later use in the application
        /// </summary>
        /// <param name="assemblyHelper">AssemblyHelper that is used to determine the application folder</param>
        /// <param name="languageName">Language to use for initialization</param>
        public void InitTranslator(string languageName, IAssemblyHelper assemblyHelper = null)
        {
            if (assemblyHelper != null)
                _assemblyHelper = assemblyHelper;
            LanguageLoader = BuildLanguageLoader();

            if (string.IsNullOrEmpty(languageName))
                languageName = "english";

            var translationFile = LanguageLoader.GetTranslationFile(languageName);

            _translator = BuildLanguageTranslator(translationFile);
            _translator.FallbackTranslator = BuildLanguageTranslator(Path.Combine(TranslationPath, "english.ini"));
        }

        /// <summary>
        ///     Initialize an empty translator (i.e. for tests)
        /// </summary>
        public void InitEmptyTranslator()
        {
            _translator = new BasicTranslator("empty", Data.CreateDataStorage());
            _languageLoader = BuildLanguageLoader();
        }

        private Translator BuildLanguageTranslator(string translationFile)
        {
            if (translationFile == null || !File.Exists(translationFile))
                return new BasicTranslator("empty", Data.CreateDataStorage());

            return new BasicTranslator(translationFile);
        }

        private LanguageLoader BuildLanguageLoader()
        {
            var appDir = _assemblyHelper.GetCurrentAssemblyDirectory();

            var translationPathCandidates = new[]
                {Path.Combine(appDir, @"Languages"), Path.Combine(appDir, @"..\..\Languages")};

            return new LanguageLoader(translationPathCandidates);
        }

        /// <summary>
        ///     Temporarily sets a translation while storing the old translator for later use. Use RevertTemporaryTranslation to
        ///     revert to the initial translator.
        /// </summary>
        /// <param name="language">The language definition to use</param>
        /// <returns>true, if the translation was successfully loaded</returns>
        public bool SetTemporaryTranslation(Language language)
        {
            var languageFile = Path.Combine(TranslationPath, language.FileName);

            if (!File.Exists(languageFile))
                return false;

            if (_tmpTranslator == null) _tmpTranslator = _translator;

            _translator = new BasicTranslator(languageFile);

            return true;
        }

        /// <summary>
        ///     Reverts a temporarily set translation to it's original. If no temporary translation has been set, nothing will be
        ///     reverted.
        /// </summary>
        public void RevertTemporaryTranslation()
        {
            if (_tmpTranslator != null)
            {
                _translator = _tmpTranslator;
                _tmpTranslator = null;
            }
        }

        public bool HasTranslation(string language)
        {
            return LanguageLoader.GetTranslationFileIfExists(language) != null;
        }

        /// <summary>
        ///     Translates a profile list by searching for predefined translations based on their GUID and apply the translated
        ///     name to them
        /// </summary>
        /// <param name="profiles">The profile list</param>
        public void TranslateProfileList(IList<ConversionProfile> profiles)
        {
            foreach (var p in profiles)
                try
                {
                    var translation = _translator.GetTranslation("ProfileNameByGuid", p.Guid);
                    if (!string.IsNullOrEmpty(translation))
                        p.Name = translation;
                }
                catch (ArgumentException)
                {
                    //do nothing, profile must not be renamed
                }
        }
    }
}