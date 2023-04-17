using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace clawSoft.clawPDF.Shared.ViewModels.Wrapper
{
    public class DataGridComboBoxColumnWithBindingHack : DataGridComboBoxColumn
    {
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            var element = base.GenerateEditingElement(cell, dataItem);
            CopyItemsSource(element);
            return element;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var element = base.GenerateElement(cell, dataItem);
            CopyItemsSource(element);
            return element;
        }

        private void CopyItemsSource(FrameworkElement element)
        {
            BindingOperations.SetBinding(element, ItemsControl.ItemsSourceProperty,
                BindingOperations.GetBinding(this, ItemsControl.ItemsSourceProperty));
        }
    }
}