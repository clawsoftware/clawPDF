using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AdonisUI.Controls;

namespace AdonisUI.Extensions
{
    /// <summary>
    /// Based on the approach of Thomas Levesque
    /// See http://www.thomaslevesque.com/2009/03/27/wpf-automatically-sort-a-gridview-when-a-column-header-is-clicked/
    /// </summary>
    public class GridViewSortExtension
    {
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static bool GetAutoSort(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoSortProperty);
        }

        public static void SetAutoSort(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoSortProperty, value);
        }

        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        public static bool GetShowSortGlyph(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowSortGlyphProperty);
        }

        public static void SetShowSortGlyph(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowSortGlyphProperty, value);
        }

        private static GridViewColumnHeader GetSortedColumnHeader(DependencyObject obj)
        {
            return (GridViewColumnHeader)obj.GetValue(SortedColumnHeaderProperty);
        }

        private static void SetSortedColumnHeader(DependencyObject obj, GridViewColumnHeader value)
        {
            obj.SetValue(SortedColumnHeaderProperty, value);
        }

        public static DataTemplate GetSortGlyphAscending(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(SortGlyphAscendingProperty);
        }

        public static void SetSortGlyphAscending(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(SortGlyphAscendingProperty, value);
        }

        public static DataTemplate GetSortGlyphDescending(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(SortGlyphDescendingProperty);
        }

        public static void SetSortGlyphDescending(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(SortGlyphDescendingProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GridViewSortExtension), new UIPropertyMetadata(null, OnCommandPropertyChanged));

        public static readonly DependencyProperty AutoSortProperty = DependencyProperty.RegisterAttached("AutoSort", typeof(bool), typeof(GridViewSortExtension), new UIPropertyMetadata(false, OnAutoSortPropertyChanged));

        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(GridViewSortExtension), new UIPropertyMetadata(null));

        public static readonly DependencyProperty ShowSortGlyphProperty = DependencyProperty.RegisterAttached("ShowSortGlyph", typeof(bool), typeof(GridViewSortExtension), new UIPropertyMetadata(true));

        public static readonly DependencyProperty SortGlyphAscendingProperty = DependencyProperty.RegisterAttached("SortGlyphAscending", typeof(DataTemplate), typeof(GridViewSortExtension), new UIPropertyMetadata(null));

        public static readonly DependencyProperty SortGlyphDescendingProperty = DependencyProperty.RegisterAttached("SortGlyphDescending", typeof(DataTemplate), typeof(GridViewSortExtension), new UIPropertyMetadata(null));

        private static readonly DependencyProperty SortedColumnHeaderProperty = DependencyProperty.RegisterAttached("SortedColumnHeader", typeof(GridViewColumnHeader), typeof(GridViewSortExtension), new UIPropertyMetadata(null));

        private static void OnCommandPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ItemsControl listView = obj as ItemsControl;
            if (listView != null)
            {
                if (!GetAutoSort(listView)) // Don't change click handler if AutoSort enabled
                {
                    if (args.OldValue != null && args.NewValue == null)
                    {
                        listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                    }
                    if (args.OldValue == null && args.NewValue != null)
                    {
                        listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                    }
                }
            }
        }

        private static void OnAutoSortPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ListView listView = obj as ListView;
            if (listView != null)
            {
                if (GetCommand(listView) == null) // Don't change click handler if a command is set
                {
                    bool oldValue = (bool)args.OldValue;
                    bool newValue = (bool)args.NewValue;
                    if (oldValue && !newValue)
                    {
                        listView.RemoveHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                    }
                    if (!oldValue && newValue)
                    {
                        listView.AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
                    }
                }
            }
        }

        private static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked?.Column != null)
            {
                string propertyName = GetPropertyName(headerClicked.Column);
                if (string.IsNullOrEmpty(propertyName))
                {
                    // use DisplayMemberBinding if no property name has been specified
                    propertyName = (headerClicked.Column?.DisplayMemberBinding as Binding)?.Path?.Path;
                }

                if (!string.IsNullOrEmpty(propertyName))
                {
                    ListView listView = GetAncestor<ListView>(headerClicked);
                    if (listView != null)
                    {
                        ICommand command = GetCommand(listView);
                        if (command != null)
                        {
                            if (command.CanExecute(propertyName))
                            {
                                command.Execute(propertyName);
                            }
                        }
                        else if (GetAutoSort(listView))
                        {
                            ApplySort(listView.Items, propertyName, listView, headerClicked);
                        }
                    }
                }
            }
        }

        public static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(reference);
            while (!(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent != null)
                return (T)parent;
            else
                return null;
        }

        public static void ApplySort(ICollectionView view, string propertyName, ListView listView, GridViewColumnHeader sortedColumnHeader)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                SortDescription currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    if (currentSort.Direction == ListSortDirection.Ascending)
                        direction = ListSortDirection.Descending;
                    else
                        direction = ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();

                GridViewColumnHeader currentSortedColumnHeader = GetSortedColumnHeader(listView);
                if (currentSortedColumnHeader != null)
                {
                    RemoveSortGlyph(currentSortedColumnHeader);
                }
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                view.SortDescriptions.Add(new SortDescription(propertyName, direction));
                if (GetShowSortGlyph(listView))
                    AddSortGlyph(
                        sortedColumnHeader,
                        direction,
                        direction == ListSortDirection.Ascending ? GetSortGlyphAscending(listView) : GetSortGlyphDescending(listView));
                SetSortedColumnHeader(listView, sortedColumnHeader);
            }
        }

        private static void AddSortGlyph(GridViewColumnHeader columnHeader, ListSortDirection direction, DataTemplate sortGlyphTemplate)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);

            SortGlyphAdorner sortGlyphAdorner;
            if (sortGlyphTemplate != null)
            {
                var sortGlyph = new ContentPresenter
                {
                    ContentTemplate = sortGlyphTemplate,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(8, 0, 8, 0),
                    IsHitTestVisible = false,
                };

                sortGlyphAdorner = new SortGlyphAdorner(columnHeader, sortGlyph);
            }
            else
            {
                sortGlyphAdorner = new SortGlyphAdorner(columnHeader, direction);
            }

            adornerLayer.Add(sortGlyphAdorner);
        }

        private static void RemoveSortGlyph(GridViewColumnHeader columnHeader)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            Adorner[] adorners = adornerLayer.GetAdorners(columnHeader);
            if (adorners != null)
            {
                foreach (SortGlyphAdorner adorner in adorners.OfType<SortGlyphAdorner>())
                {
                    adornerLayer.Remove(adorner);
                }
            }
        }
    }
}
