using System.ComponentModel;
using System.Windows.Controls;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Shared.Properties;

namespace clawSoft.clawPDF.Shared.Views.ActionControls
{
    public class ActionControl : UserControl, INotifyPropertyChanged
    {
        private ConversionProfile _conversionProfile;
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public virtual bool IsActionEnabled { get; set; }

        public ConversionProfile CurrentProfile
        {
            get => _conversionProfile;
            set
            {
                _conversionProfile = value;
                OnPropertyChanged("CurrentProfile");
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