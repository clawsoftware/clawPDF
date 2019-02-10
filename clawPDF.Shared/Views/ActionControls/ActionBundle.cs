using System.ComponentModel;
using clawSoft.clawPDF.Shared.Properties;

namespace clawSoft.clawPDF.Shared.Views.ActionControls
{
    public class ActionBundle : INotifyPropertyChanged
    {
        private readonly ActionControl _actionControl;

        public ActionBundle(ActionControl actionControl)
        {
            _actionControl = actionControl;
            Name = actionControl.DisplayName;
            Description = actionControl.Description;
            ActionControl = actionControl;
        }

        public bool IsEnabled
        {
            get => _actionControl.IsActionEnabled;
            set
            {
                _actionControl.IsActionEnabled = value;
                RaiseIsEnabledChanged();
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ActionControl ActionControl { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaiseIsEnabledChanged()
        {
            OnPropertyChanged("IsEnabled");
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}