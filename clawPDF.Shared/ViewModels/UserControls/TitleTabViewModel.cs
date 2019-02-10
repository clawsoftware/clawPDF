using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using clawSoft.clawPDF.Core.Settings;
using clawSoft.clawPDF.Core.Settings.Enums;
using clawSoft.clawPDF.Shared.Helper;
using pdfforge.DynamicTranslator;

namespace clawSoft.clawPDF.Shared.ViewModels.UserControls
{
    public class TitleTabViewModel : ViewModelBase
    {
        private Wrapper.SynchronizedCollection<TitleReplacement> _titleReplacements;

        public TitleTabViewModel()
        {
            TitleAddCommand = new DelegateCommand(TitleAddCommandExecute);
            TitleDeleteCommand = new DelegateCommand(TitleDeleteCommandExecute, TitleDeleteCommandCanExecute);

            ReplacementValues = TranslationHelper.Instance.IsInitialized
                ? TranslationHelper.Instance.TranslatorInstance.GetEnumTranslation<ReplacementType>().ToList()
                : new List<EnumValue<ReplacementType>>();
        }

        public TitleTabViewModel(Translator translator)
            : this()
        {
            TitleAddCommand = new DelegateCommand(TitleAddCommandExecute);
            TitleDeleteCommand = new DelegateCommand(TitleDeleteCommandExecute, TitleDeleteCommandCanExecute);
            ReplacementValues = translator.GetEnumTranslation<ReplacementType>().ToList();
        }

        public ICollectionView TitleReplacementView { get; private set; }

        public DelegateCommand TitleAddCommand { get; set; }
        public DelegateCommand TitleDeleteCommand { get; set; }

        public IList<EnumValue<ReplacementType>> ReplacementValues { get; }

        public ObservableCollection<TitleReplacement> TitleReplacements
        {
            get
            {
                if (_titleReplacements == null)
                    return null;
                return _titleReplacements.ObservableCollection;
            }
        }

        public void ApplyTitleReplacements(IList<TitleReplacement> titleReplacements)
        {
            if (TitleReplacements != null) TitleReplacements.CollectionChanged -= TitleReplacementsOnCollectionChanged;

            if (titleReplacements != null)
            {
                var replacements = titleReplacements;

                _titleReplacements = new Wrapper.SynchronizedCollection<TitleReplacement>(replacements);
                _titleReplacements.ObservableCollection.CollectionChanged += TitleReplacementsOnCollectionChanged;
                TitleReplacementView = CollectionViewSource.GetDefaultView(_titleReplacements.ObservableCollection);
                TitleReplacementView.SortDescriptions.Add(new SortDescription(nameof(TitleReplacement.ReplacementType),
                    ListSortDirection.Descending));
                TitleReplacementView.SortDescriptions.Add(new SortDescription(nameof(TitleReplacement.Search),
                    ListSortDirection.Descending));
                TitleReplacementView.CurrentChanged += TitleReplacement_OnCurrentChanged;
            }

            RaisePropertyChanged("TitleReplacements");
            RaiseTitleCommandsCanExecuteChanged();
        }

        private void RaiseTitleCommandsCanExecuteChanged()
        {
            TitleDeleteCommand.RaiseCanExecuteChanged();
        }

        private void TitleReplacement_OnCurrentChanged(object sender, EventArgs eventArgs)
        {
            RaiseTitleCommandsCanExecuteChanged();
        }

        private bool TitleDeleteCommandCanExecute(object obj)
        {
            if (TitleReplacementView == null || TitleReplacementView.CurrentItem == null ||
                TitleReplacementView.CurrentItem == CollectionView.NewItemPlaceholder)
                return false;

            return true;
        }

        private void TitleDeleteCommandExecute(object obj)
        {
            TitleReplacements.Remove(TitleReplacementView.CurrentItem as TitleReplacement);
        }

        private void TitleAddCommandExecute(object obj)
        {
            var newItem = new TitleReplacement();
            TitleReplacements.Add(newItem);
            TitleReplacementView.MoveCurrentTo(newItem);
        }

        private void TitleReplacementsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaiseTitleCommandsCanExecuteChanged();
        }
    }
}