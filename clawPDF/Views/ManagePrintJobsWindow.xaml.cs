using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using clawSoft.clawPDF.Helper;
using clawSoft.clawPDF.Shared.Helper;
using clawSoft.clawPDF.ViewModels;

namespace clawSoft.clawPDF.Views
{
    internal partial class ManagePrintJobsWindow : Window
    {
        public ManagePrintJobsWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            TranslationHelper.Instance.TranslatorInstance.Translate(this);
            var view = (GridView)JobList.View;
            view.Columns[0].Header =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("ManagePrintJobsWindow", "TitleColoumn",
                    "Title");
            view.Columns[1].Header =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("ManagePrintJobsWindow", "FilesColoumn",
                    "Files");
            view.Columns[2].Header =
                TranslationHelper.Instance.TranslatorInstance.GetTranslation("ManagePrintJobsWindow", "PagesColoumn",
                    "Pages");
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            DragAndDropHelper.DragEnter(e);
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            DragAndDropHelper.Drop(e);
        }

        private void JobList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (ManagePrintJobsViewModel)DataContext;
            vm.DeleteJobCommand.RaiseCanExecuteChanged();
            vm.MergeJobsCommand.RaiseCanExecuteChanged();
        }

        private void OnActivated(object sender, EventArgs e)
        {
            ((ManagePrintJobsViewModel)DataContext).RaiseRefreshView();
        }

        private void ManagePrintJobsWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}