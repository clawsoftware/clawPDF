using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.Shared.ViewModels;
using clawSoft.clawPDF.ViewModels.Wrapper;

namespace clawSoft.clawPDF.ViewModels.UserControls
{
    internal class PrinterTabViewModel : ApplicationSettingsViewModel
    {
        private readonly ConversionProfile _dummyLastUsedProfile = new ConversionProfile
        {
            Name = "<Last used profile>",
            Guid = ""
        };

        private readonly TranslationHelper _translationHelper;
        private ICollection<string> _clawPdfPrinters;

        private IEnumerable<ConversionProfile> _conversionProfiles;
        private ConversionProfile _defaultProfile;
        private Shared.ViewModels.Wrapper.SynchronizedCollection<PrinterMappingWrapper> _printerMappings;
        private ICollectionView _printerMappingView;

        public PrinterTabViewModel(ApplicationSettings applicationSettings, IEnumerable<ConversionProfile> profiles,
            Func<ICollection<string>> fetchPrintersFunc, TranslationHelper translationHelper)
        {
            SettingsChanged += OnSettingsChanged;

            _translationHelper = translationHelper;

            var helper = new PrinterHelper();
            GetPrinterListAction = fetchPrintersFunc ?? helper.GetclawPDFPrinters;

            ConversionProfiles = profiles;
            ApplicationSettings = applicationSettings;

            AddPrinterCommand = new DelegateCommand(AddPrintercommandExecute);
            DeletePrinterCommand = new DelegateCommand(DeletePrinterCommandExecute, ModifyPrinterCommandCanExecute);
        }

        public PrinterTabViewModel()
            : this(null, new List<ConversionProfile>(), null, TranslationHelper.Instance)
        {
        }

        public IEnumerable<ConversionProfile> ConversionProfiles
        {
            get => _conversionProfiles;
            set
            {
                _conversionProfiles = value;
                RaisePropertyChanged("ConversionProfiles");

                _defaultProfile = ConversionProfiles.FirstOrDefault(x => x.Guid == ProfileGuids.DEFAULT_PROFILE_GUID);
                if (_defaultProfile == null)
                    _defaultProfile = _dummyLastUsedProfile;
            }
        }

        public IEnumerable<ConversionProfile> PrinterMappingProfiles
        {
            get
            {
                var profiles = _conversionProfiles.ToList();
                _dummyLastUsedProfile.Name =
                    _translationHelper.TranslatorInstance.GetTranslation("ApplicationSettingsWindow",
                        "LastUsedProfileMapping", "<Last used profile>");
                profiles.Insert(0, _dummyLastUsedProfile);
                return profiles;
            }
        }

        public ICollection<string> ClawPdfPrinters
        {
            get => _clawPdfPrinters;
            set
            {
                _clawPdfPrinters = value;
                RaisePropertyChanged("clawPDFPrinters");
            }
        }

        public ObservableCollection<PrinterMappingWrapper> PrinterMappings
        {
            get
            {
                if (_printerMappings == null)
                    return null;
                return _printerMappings.ObservableCollection;
            }
        }

        public Func<string> AddPrinterAction { private get; set; }
        public Action<PrinterMappingWrapper> DeletePrinterAction { private get; set; }
        public Func<ICollection<string>> GetPrinterListAction { get; set; }
        public DelegateCommand AddPrinterCommand { get; }
        public DelegateCommand DeletePrinterCommand { get; }

        public string PrimaryPrinter
        {
            get
            {
                if (ApplicationSettings == null)
                    return null;
                if (string.IsNullOrEmpty(ApplicationSettings.PrimaryPrinter) ||
                    PrinterMappings.All(o => o.PrinterName != ApplicationSettings.PrimaryPrinter))
                {
                    var printerHelper = new PrinterHelper();
                    ApplicationSettings.PrimaryPrinter =
                        printerHelper.GetApplicableclawPDFPrinter("clawPDF", "clawPDF");
                }

                return ApplicationSettings.PrimaryPrinter;
            }
            set
            {
                ApplicationSettings.PrimaryPrinter = value;
                UpdatePrimaryPrinter(ApplicationSettings.PrimaryPrinter);
                RaisePropertyChanged("PrimaryPrinter");
            }
        }

        private void OnSettingsChanged(object sender, EventArgs eventArgs)
        {
            if (ApplicationSettings == null)
                return;

            UpdatePrinterList();
            ApplyPrinterMappings();
            UpdatePrinterCollectionViews();
            RaisePropertyChanged("PrimaryPrinter");
        }

        private void PrinterMappings_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ApplicationSettings.PrinterMappings.Clear();

            foreach (var printerMappingWrapper in PrinterMappings)
            {
                ApplicationSettings.PrinterMappings.Add(printerMappingWrapper.PrinterMapping);
                if (printerMappingWrapper.Profile == null)
                    printerMappingWrapper.Profile = _defaultProfile;
            }

            RaisePrinterCommandsCanExecuteChanged();
        }

        private void PrinterMappingView_OnCurrentChanged(object sender, EventArgs eventArgs)
        {
            RaisePrinterCommandsCanExecuteChanged();
        }

        private void RaisePrinterCommandsCanExecuteChanged()
        {
            DeletePrinterCommand.RaiseCanExecuteChanged();
        }

        private void UpdatePrinterList()
        {
            if (GetPrinterListAction == null)
                return;

            if (_clawPdfPrinters == null)
                _clawPdfPrinters = new List<string>();

            _clawPdfPrinters.Clear();
            foreach (var printer in GetPrinterListAction()) _clawPdfPrinters.Add(printer);

            RaisePropertyChanged("clawPDFPrinters");
        }

        public void UpdatePrimaryPrinter(string printerName)
        {
            foreach (var printerMappingWrapper in _printerMappings.ObservableCollection)
                printerMappingWrapper.PrimaryPrinter = printerName;
        }

        private void ApplyPrinterMappings()
        {
            if (ApplicationSettings != null && ApplicationSettings.PrinterMappings != null)
            {
                var mappingWrappers = new List<PrinterMappingWrapper>();

                foreach (var printerMapping in ApplicationSettings.PrinterMappings)
                {
                    var mappingWrapper = new PrinterMappingWrapper(printerMapping, ConversionProfiles);
                    if (printerMapping.ProfileGuid == _dummyLastUsedProfile.Guid)
                        mappingWrapper.Profile = _dummyLastUsedProfile;
                    else if (mappingWrapper.Profile == null) mappingWrapper.Profile = _defaultProfile;
                    mappingWrappers.Add(mappingWrapper);
                }

                _printerMappings =
                    new Shared.ViewModels.Wrapper.SynchronizedCollection<PrinterMappingWrapper>(mappingWrappers);

                _printerMappings.ObservableCollection.CollectionChanged += PrinterMappings_OnCollectionChanged;
                _printerMappingView = CollectionViewSource.GetDefaultView(_printerMappings.ObservableCollection);
                _printerMappingView.CurrentChanged += PrinterMappingView_OnCurrentChanged;
            }

            RaisePropertyChanged("PrinterMappings");
        }

        private bool ModifyPrinterCommandCanExecute(object o)
        {
            if (_printerMappingView == null)
                return false;

            var currentMapping = _printerMappingView.CurrentItem as PrinterMappingWrapper;

            if (currentMapping == null)
                return false;

            return ClawPdfPrinters.Contains(currentMapping.PrinterName);
        }

        private void AddPrintercommandExecute(object o)
        {
            var printerName = AddPrinterAction();

            if (!string.IsNullOrWhiteSpace(printerName))
                PrinterMappings.Add(
                    new PrinterMappingWrapper(new PrinterMapping(printerName, ProfileGuids.DEFAULT_PROFILE_GUID),
                        ConversionProfiles));

            UpdatePrinterCollectionViews();
        }

        private void DeletePrinterCommandExecute(object obj)
        {
            var currentMapping = _printerMappingView.CurrentItem as PrinterMappingWrapper;

            if (currentMapping == null)
                return;

            DeletePrinterAction(currentMapping);
        }

        private void UpdatePrinterCollectionViews()
        {
            UpdatePrinterList();
            CollectionViewSource.GetDefaultView(ApplicationSettings.PrinterMappings).Refresh();
            CollectionViewSource.GetDefaultView(ClawPdfPrinters).Refresh();
        }

        public void RefreshPrinterMappings()
        {
            // We need to force the UI to read the translated names.
            // This is the hard way to implement "RaisePropertyChanged"
            var tmp = _printerMappings;
            _printerMappings = null;
            RaisePropertyChanged("PrinterMappings");
            _printerMappings = tmp;
            RaisePropertyChanged("PrinterMappings");
        }
    }
}