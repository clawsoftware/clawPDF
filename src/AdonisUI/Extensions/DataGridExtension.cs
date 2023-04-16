using AdonisUI.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace AdonisUI.Extensions
{
    /// <summary>
    /// Provides attached behaviors related to the DataGrid control.
    /// </summary>
    public class DataGridExtension
    {
        /// <summary>
        /// Gets the value of the <see cref="HasAnyRowErrorProperty"/> attached property of the specified DataGrid.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static bool GetHasAnyRowError(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasAnyRowErrorProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="HasAnyRowErrorProperty"/> attached property of the specified DataGrid.
        /// </summary>
        private static void SetHasAnyRowError(DependencyObject obj, bool value)
        {
            obj.SetValue(HasAnyRowErrorPropertyKey, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="HadAnyRowErrorProperty"/> attached property of the specified DataGrid.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static bool GetHadAnyRowError(DependencyObject obj)
        {
            return (bool)obj.GetValue(HadAnyRowErrorProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="HadAnyRowErrorProperty"/> attached property of the specified DataGrid.
        /// </summary>
        private static void SetHadAnyRowError(DependencyObject obj, bool value)
        {
            obj.SetValue(HadAnyRowErrorPropertyKey, value);
        }

        /// <summary>
        /// Gets the value of the <see cref="IsReportingErrorsToDataGridProperty"/> attached property of the specified DataGridRow.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(DataGridRow))]
        public static bool GetIsReportingErrorsToDataGrid(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsReportingErrorsToDataGridProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="IsReportingErrorsToDataGridProperty"/> attached property of the specified DataGridRow.
        /// </summary>
        public static void SetIsReportingErrorsToDataGrid(DependencyObject obj, bool value)
        {
            obj.SetValue(IsReportingErrorsToDataGridProperty, value);
        }

        private static readonly DependencyPropertyKey HasAnyRowErrorPropertyKey = DependencyProperty.RegisterAttachedReadOnly("HasAnyRowError", typeof(bool), typeof(DataGridExtension), new PropertyMetadata(false));

        /// <summary>
        /// A DependencyProperty that will be true if any DataGridRow in the DataGrid has <see cref="Validation.HasErrorProperty"/> set to true.
        /// </summary>
        public static readonly DependencyProperty HasAnyRowErrorProperty = HasAnyRowErrorPropertyKey.DependencyProperty;
        
        private static readonly DependencyPropertyKey HadAnyRowErrorPropertyKey = DependencyProperty.RegisterAttachedReadOnly("HadAnyRowError", typeof(bool), typeof(DataGridExtension), new PropertyMetadata(false));

        /// <summary>
        /// A DependencyProperty that will be true if any DataGridRow in the DataGrid has or had at least once <see cref="Validation.HasErrorProperty"/> set to true.
        /// </summary>
        public static readonly DependencyProperty HadAnyRowErrorProperty = HadAnyRowErrorPropertyKey.DependencyProperty;

        /// <summary>
        /// A DependencyProperty that needs to be true if the DataGridRow should update the <see cref="HasAnyRowErrorProperty"/> and <see cref="HadAnyRowErrorProperty"/> properties of its DataGrid.
        /// </summary>
        public static readonly DependencyProperty IsReportingErrorsToDataGridProperty = DependencyProperty.RegisterAttached("IsReportingErrorsToDataGrid", typeof(bool), typeof(DataGridExtension), new PropertyMetadata(false, OnIsReportingErrorsToDataGridPropertyChanged));

        private static void OnIsReportingErrorsToDataGridPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow) d;
            DataGrid parentDataGrid = UINavigator.FindVisualParent<DataGrid>(dataGridRow);
            DependencyPropertyDescriptor hasErrorProperty = DependencyPropertyDescriptor.FromProperty(Validation.HasErrorProperty, typeof(DataGridRow));

            if (parentDataGrid != null)
            {
                UpdateDataGridHasAnyRowError(parentDataGrid);
            }

            if ((bool) e.NewValue)
            {
                hasErrorProperty.AddValueChanged(dataGridRow, OnDataGridRowHasErrorChanged);
            }
            else
            {
                hasErrorProperty.RemoveValueChanged(dataGridRow, OnDataGridRowHasErrorChanged);
            }
        }

        private static void OnDataGridRowHasErrorChanged(object sender, EventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow) sender;
            DataGrid parentDataGrid = UINavigator.FindVisualParent<DataGrid>(dataGridRow);

            if (parentDataGrid != null)
            {
                UpdateDataGridHasAnyRowError(parentDataGrid);
                UpdateDataGridHadAnyRowError(parentDataGrid);
            }
        }

        private static void UpdateDataGridHasAnyRowError(DataGrid dataGrid)
        {
            SetHasAnyRowError(dataGrid, GetDataGridRows(dataGrid).Where(GetIsReportingErrorsToDataGrid).Any(Validation.GetHasError));
        }

        private static void UpdateDataGridHadAnyRowError(DataGrid dataGrid)
        {
            if (!GetHadAnyRowError(dataGrid))
                SetHadAnyRowError(dataGrid, GetDataGridRows(dataGrid).Where(GetIsReportingErrorsToDataGrid).Any(Validation.GetHasError));
        }

        private static IEnumerable<DataGridRow> GetDataGridRows(DataGrid dataGrid)
        {
            foreach (object item in dataGrid.ItemsSource)
            {
                if (dataGrid.ItemContainerGenerator.ContainerFromItem(item) is DataGridRow row)
                    yield return row;
            }
        }
    }
}
