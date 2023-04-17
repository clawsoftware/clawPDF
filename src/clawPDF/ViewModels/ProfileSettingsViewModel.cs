using System;
using System.ComponentModel;
using System.Windows.Data;
using clawSoft.clawPDF.Core.Actions;
using clawSoft.clawPDF.Core.Helper;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.Shared.ViewModels.Wrapper;
using clawSoft.clawPDF.Shared.Views;

namespace clawSoft.clawPDF.ViewModels
{
    internal class ProfileSettingsViewModel : ViewModelBase
    {
        private ICollectionView _conversionProfilesView;
        private SynchronizedCollection<ConversionProfile> _profileCollection;
        private clawPDFSettings _settings;
        private clawPDFSettings _unchangedOriginalSettings;
        public ButtonClickedToClose ButtonClickedToClose;
        public string InvalidProfileMessage;

        public ProfileSettingsViewModel()
        {
            ButtonClickedToClose = ButtonClickedToClose.None;

            AddProfileCommand = new DelegateCommand(AddProfileExcecute);
            DeleteProfileCommand = new DelegateCommand(DeleteProfileExcecute, DeleteProfileCanExcecute);
            RenameProfileCommand = new DelegateCommand(RenameProfileExcecute, RenameProfileCanExcecute);

            SaveButtonCommand = new DelegateCommand(SaveExcecute);
        }

        public ProfileSettingsViewModel(clawPDFSettings settings,
            TranslationHelper translationHelper)
            : this()
        {
            Settings = settings;
            _unchangedOriginalSettings = _settings.Copy();
        }

        public ProfileSettingsViewModel(clawPDFSettings settings)
            : this(settings, TranslationHelper.Instance)
        {
        }

        public DelegateCommand SaveButtonCommand { get; }
        public Func<bool> QueryDeleteProfile { private get; set; }
        public Func<string, string, bool> QueryDeleteProfileWithPrinterMapping { private get; set; }
        public Func<bool> QueryDiscardChanges { private get; set; }
        public Func<ActionResultDict, bool> QueryIgnoreDefectiveProfiles { private get; set; }
        public Func<string, string> QueryProfileName { private get; set; }
        public Action UpdateLayoutProfilesBoxAction { private get; set; }
        public DelegateCommand AddProfileCommand { get; }
        public DelegateCommand RenameProfileCommand { get; }
        public DelegateCommand DeleteProfileCommand { get; }

        public ICollectionView ConversionProfilesView
        {
            get => _conversionProfilesView;
            set
            {
                _conversionProfilesView = value;
                RaisePropertyChanged("ConversionProfilesView");
            }
        }

        public clawPDFSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                if (_unchangedOriginalSettings == null)
                    _unchangedOriginalSettings = _settings.Copy();

                _profileCollection = new SynchronizedCollection<ConversionProfile>(_settings.ConversionProfiles);

                ConversionProfilesView = CollectionViewSource.GetDefaultView(_profileCollection.ObservableCollection);
                ConversionProfilesView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                ConversionProfilesView.CurrentChanged += (sender, args) => CurrentProfilePropertyChanged();

                ConversionProfilesView.MoveCurrentTo(_settings.GetLastUsedOrFirstProfile());
                CurrentProfilePropertyChanged();
            }
        }

        public ConversionProfile CurrentProfile
        {
            get
            {
                if (_conversionProfilesView == null)
                    return null;
                return _conversionProfilesView.CurrentItem as ConversionProfile;
            }
        }

        public void CurrentProfilePropertyChanged()
        {
            RaisePropertyChanged("CurrentProfile");
            RaisePropertyChanged("Settings");
            DeleteProfileCommand.RaiseCanExecuteChanged();
            RenameProfileCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged("LowEncryptionEnabled");
            RaisePropertyChanged("MediumEncryptionEnabled");
            RaisePropertyChanged("HighEncryptionEnabled");
            RaisePropertyChanged("ExtendedPermissonsEnabled");
            RaisePropertyChanged("RestrictLowQualityPrintingEnabled");
            RaisePropertyChanged("AllowFillFormsEnabled");
            RaisePropertyChanged("AllowScreenReadersEnabled");
            RaisePropertyChanged("AllowEditingAssemblyEnabled");
        }

        public bool CheckForClosingWindowAndRestoreTheSettings()
        {
            if (CheckForChangesInProfiles())
                if (!QueryDiscardChanges())
                    return false;

            Settings = _unchangedOriginalSettings;
            return true;
        }

        private bool CheckForChangesInProfiles()
        {
            //check for added or deleted profiles
            if (!Settings.ConversionProfiles.Count.Equals(_unchangedOriginalSettings.ConversionProfiles.Count))
                return true;

            for (var i = 0; i < Settings.ConversionProfiles.Count; i++)
                //check for changes
                if (!Settings.ConversionProfiles[i].Equals(_unchangedOriginalSettings.ConversionProfiles[i]))
                    return true;
            return false;
        }

        private void SaveExcecute(object obj)
        {
            ButtonClickedToClose = ButtonClickedToClose.Save;

            var actionResultDict = ProfileChecker.ProfileCheckDict(Settings.ConversionProfiles);
            if (!actionResultDict)
                if (!QueryIgnoreDefectiveProfiles(actionResultDict))
                {
                    ButtonClickedToClose = ButtonClickedToClose.None;
                    return; //Cancel if user wants to edit defective profiles
                }

            Settings.ApplicationSettings.LastUsedProfileGuid = CurrentProfile.Guid;

            RaiseCloseView(true);
        }

        private void RenameProfileExcecute(object obj)
        {
            var newProfileName = QueryProfileName(CurrentProfile.Name);
            if (newProfileName == null)
                return;

            CurrentProfile.Name = newProfileName;
            ConversionProfilesView.Refresh();
            UpdateLayoutProfilesBoxAction();
        }

        private bool RenameProfileCanExcecute(object obj)
        {
            if (CurrentProfile == null)
                return false;

            return CurrentProfile.Properties.Renamable;
        }

        private void DeleteProfileExcecute(object obj)
        {
            var mapping = Settings.GetPrinterByProfile(CurrentProfile);
            if (mapping != null)
            {
                if (!QueryDeleteProfileWithPrinterMapping(CurrentProfile.Name, mapping.PrinterName))
                    return;
            }
            else
            {
                if (!QueryDeleteProfile())
                    return;
            }

            _profileCollection.ObservableCollection.Remove(CurrentProfile);
        }

        private bool DeleteProfileCanExcecute(object obj)
        {
            if (CurrentProfile == null)
                return false;
            if (!CurrentProfile.Properties.Deletable)
                return false;
            if (Settings.ConversionProfiles.Count < 2)
                return false;
            return true;
        }

        private void AddProfileExcecute(object obj)
        {
            var name = QueryProfileName(null);
            if (name == null)
                return;

            var newProfile = CurrentProfile.Copy();
            newProfile.Guid = Guid.NewGuid().ToString();
            newProfile.Name = name;
            newProfile.Properties.Deletable = true;
            newProfile.Properties.Editable = true;
            newProfile.Properties.Renamable = true;
            _profileCollection.ObservableCollection.Add(newProfile);
            ConversionProfilesView.MoveCurrentTo(newProfile);
        }

        public InputBoxValidation ProfilenameIsValid(string profileName)
        {
            if (profileName == null)
                return new InputBoxValidation(false, InvalidProfileMessage);

            if (profileName.Length <= 0)
                return new InputBoxValidation(false, InvalidProfileMessage);

            var profileNameDoesNotExist = Settings.GetProfileByName(profileName) == null;

            return new InputBoxValidation(profileNameDoesNotExist,
                profileNameDoesNotExist ? null : InvalidProfileMessage);
        }
    }

    internal enum ButtonClickedToClose
    {
        None,
        Save,
        Ok,
        Close
    }
}