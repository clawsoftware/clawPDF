using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Properties;

namespace clawSoft.clawPDF.ViewModels.Wrapper
{
    internal class PrinterMappingWrapper : INotifyPropertyChanged
    {
        private ConversionProfile _conversionProfile;
        private string _primaryPrinter;
        private string _printerName;

        public PrinterMappingWrapper(PrinterMapping printerMapping, IEnumerable<ConversionProfile> profiles)
        {
            PrinterMapping = printerMapping;
            PrinterName = printerMapping.PrinterName;
            Profile = profiles.FirstOrDefault(p => p.Guid == printerMapping.ProfileGuid);
        }

        public PrinterMapping PrinterMapping { get; }

        public string PrinterName
        {
            get => _printerName;
            set
            {
                _printerName = value;
                PrinterMapping.PrinterName = _printerName;
                OnPropertyChanged("PrinterName");
                OnPropertyChanged("IsPrimaryPrinter");
            }
        }

        public ConversionProfile Profile
        {
            get => _conversionProfile;
            set
            {
                _conversionProfile = value;
                PrinterMapping.ProfileGuid = _conversionProfile == null ? "" : _conversionProfile.Guid;
                OnPropertyChanged("Profile");
            }
        }

        public bool IsPrimaryPrinter => PrinterName != null && PrinterName == PrimaryPrinter;

        public string PrimaryPrinter
        {
            get => _primaryPrinter;
            set
            {
                if (value == _primaryPrinter) return;
                _primaryPrinter = value;
                OnPropertyChanged("PrimaryPrinter");
                OnPropertyChanged("IsPrimaryPrinter");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}