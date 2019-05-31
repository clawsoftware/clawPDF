using System;
using System.ComponentModel;

namespace clawSoft.clawPDF.Shared.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public Action<bool?> CloseViewAction { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged Members

        public void RaiseCloseView(bool? result)
        {
            if (CloseViewAction != null)
                CloseViewAction(result);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}