using System.Collections.Specialized;
using Veritaware.Toolkits.LightVM;

namespace System.Collections.Generic
{
    public class NotifyingList<T> : ModelBase, INotifyCollectionChanged, IList<T>
    {
        // This must agree with Binding.IndexerName.  It is declared separately
        // here so as to avoid a dependency on PresentationFramework.dll.
        private const string IndexerName = "Item[]";

        private readonly List<T> _internalList;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnItemChanged(T newItem, T oldItem, int index)
            => CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Replace, newItem, oldItem, index
            ));

        protected virtual void OnAdd(T item)
            => CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, _internalList.Count
            ));

        protected virtual void OnClear()
            => CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset
            ));

        protected virtual void OnInsert(T item, int index)
        {
            CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, index
            ));

            for(var i = index + 1; i < _internalList.Count; ++i)
            {
                CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                    NotifyCollectionChangedAction.Move, _internalList[i], i, i -1
                ));
            }
        }

        protected virtual void OnRemove(T item, int index)
            => CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, item, index
            ));

        public NotifyingList()
        {
            _internalList = new List<T>();
        }

        public NotifyingList(IList<T> list)
        {
            _internalList = new List<T>(list);
        }

        public T this[int index]
        {
            get
            {
                return _internalList[index];
            }

            set
            {
                if(_internalList[index].Equals(value))
                    return;

                T oldValue = _internalList[index];
                _internalList[index] = value;

                OnItemChanged(value, oldValue, index);
            }
        }

        public int Count => _internalList.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _internalList.Add(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnAdd(item);
        }

        public void Clear()
        {
            _internalList.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnClear();
        }

        public bool Contains(T item) => _internalList.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _internalList.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => _internalList.GetEnumerator();
        public int IndexOf(T item) => _internalList.IndexOf(item);
        public void Insert(int index, T item)
        {
            _internalList.Insert(index, item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnInsert(item, index);
        }

        public bool Remove(T item)
        {
            var index = _internalList.IndexOf(item);
            if(_internalList.Remove(item))
            {
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(IndexerName);
                OnRemove(item, index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            var item = _internalList[index];
            _internalList.RemoveAt(index);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnRemove(item, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => _internalList.GetEnumerator();
    }
}
