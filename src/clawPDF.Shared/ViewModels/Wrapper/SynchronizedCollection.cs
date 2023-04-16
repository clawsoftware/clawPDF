using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace clawSoft.clawPDF.Shared.ViewModels.Wrapper
{
    /// <summary>
    ///     SynchronizedCollection is a wrapper that binds an IList and an ObserverableCollection. All changes that happen
    ///     inside the ObservableCollection will be replicated to the base collection.
    ///     Note: Changes in the base collection can't be detected. Do not modify it after starting the sync.
    /// </summary>
    /// <typeparam name="T">Type of the items in the List and the bound ObservableCollection</typeparam>
    public class SynchronizedCollection<T>
    {
        /// <summary>
        ///     Create a new SynchronizedCollection that reflects changes that take place in the ObservableCollection.
        /// </summary>
        /// <param name="collection">The collection that will be kept in sync when changes to the ObservableCollection take place</param>
        public SynchronizedCollection(IList<T> collection)
        {
            Collection = collection;
            ObservableCollection = new ObservableCollection<T>(collection);
            ObservableCollection.CollectionChanged += OnObservableCollectionChanged;
        }

        /// <summary>
        ///     The Collection that is kept in sync. Do not modify it! Use the ObservableCollection instead.
        /// </summary>
        public IList<T> Collection { get; }

        /// <summary>
        ///     All changes to this ObservableCollection will be reflected to the base collection
        /// </summary>
        public ObservableCollection<T> ObservableCollection { get; }

        private void OnObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Collection.Insert(e.NewStartingIndex, (T)e.NewItems[0]);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    Collection.RemoveAt(e.OldStartingIndex);
                    break;

                case NotifyCollectionChangedAction.Move:
                    Collection.RemoveAt(e.OldStartingIndex);
                    Collection.Insert(e.NewStartingIndex, (T)e.OldItems[0]);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Collection.Clear();
                    foreach (var itm in ObservableCollection)
                        Collection.Add(itm);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}