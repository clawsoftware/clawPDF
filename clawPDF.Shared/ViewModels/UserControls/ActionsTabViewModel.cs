using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using clawSoft.clawPDF.Shared.Views.ActionControls;

namespace clawSoft.clawPDF.Shared.ViewModels.UserControls
{
    public class ActionsTabViewModel : CurrentProfileViewModel
    {
        public ActionsTabViewModel()
        {
            ProfileChanged += OnProfileChanged;
            ActionCollectionView = CollectionViewSource.GetDefaultView(Actions);
        }

        public ObservableCollection<ActionBundle> Actions { get; } = new ObservableCollection<ActionBundle>();

        public ICollectionView ActionCollectionView { get; }

        private void OnProfileChanged(object sender, EventArgs eventArgs)
        {
            foreach (var actionBundle in Actions)
            {
                actionBundle.ActionControl.CurrentProfile = CurrentProfile;
                actionBundle.RaiseIsEnabledChanged();
            }
        }

        public void SelectFirstEnabledOrFirstAction()
        {
            if (Actions == null)
                return;

            foreach (var action in Actions)
                if (action.IsEnabled)
                {
                    ActionCollectionView.MoveCurrentTo(action);
                    return;
                }

            ActionCollectionView.MoveCurrentToFirst();
        }

        public void AddAction(ActionControl action)
        {
            var actionBundle = new ActionBundle(action);
            Actions.Add(actionBundle);
        }
    }
}