namespace System.Collections.Generic
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.Contracts;

    using Veritaware.Toolkits.LightVM.Common;

    public class NotifyingList<T> : NotifyingObject, IList<T>, IList, IReadOnlyList<T>, INotifyCollectionChanged
    {
        // This must agree with Binding.IndexerName.  It is declared separately
        // here so as to avoid a dependency on PresentationFramework.dll.
        private const string IndexerName = "Item[]";
        private readonly List<T> _internalList;
        private object _syncRoot;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private static bool IsCompatibleObject(object value) 
            => ((value is T) || (value == null && default(T) == null));

        private void CheckIndexes(List<T> oldList, int index, int count)
        {
            for(var i = index; i < index + count; ++i)
            {
                var oldIndex = oldList.IndexOf(_internalList[i]);
                if(i != oldIndex)
                    OnSwap(i, oldIndex);
            }
        }

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

        protected virtual void OnSwap(int indexLeft, int indexRight)
        {
            CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Move, _internalList[indexLeft], indexLeft, indexRight
            ));
            CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Move, _internalList[indexRight], indexRight, indexLeft
            ));
        }

        public NotifyingList()
        {
            _internalList = new List<T>();
        }

        public NotifyingList(int capacity)
        {
            _internalList = new List<T>(capacity);
        }

        public NotifyingList(IEnumerable<T> collection)
        {
            _internalList = new List<T>(collection);
        }

        public int Capacity => _internalList.Capacity;
        public int Count => _internalList.Count;
        public bool IsFixedSize => false;
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;

        public object SyncRoot
        {
            get
            {
                if(_syncRoot == null)
                {
                    Threading.Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
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
        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                if(value == null && !(default(T) == null))
                    throw new ArgumentNullException(nameof(value));

                try
                {
                    this[index] = (T)value;
                }
                catch(InvalidCastException)
                {
                    throw new ArgumentException($"Argument is not of type {typeof(T)}.", nameof(value));
                }
            }
        }

        public void Add(T item)
        {
            _internalList.Add(item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnAdd(item);
        }
        public int Add(object item)
        {
            if(item == null && !(default(T) == null))
                throw new ArgumentNullException(nameof(item));

            try
            {
                Add((T)item);
            }
            catch(InvalidCastException)
            {
                throw new ArgumentException($"Argument is not of type {typeof(T)}.", nameof(item));
            }

            return Count - 1;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach(T item in collection)
            {
                Add(item);
            }
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            Contract.Ensures(Contract.Result<ReadOnlyCollection<T>>() != null);
            return new ReadOnlyCollection<T>(this);
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer) => _internalList.BinarySearch(index, count, item, comparer);
        public int BinarySearch(T item) => _internalList.BinarySearch(item);
        public int BinarySearch(T item, IComparer<T> comparer) => _internalList.BinarySearch(item, comparer);

        public void Clear()
        {
            _internalList.Clear();
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnClear();
        }

        public bool Contains(T item) => _internalList.Contains(item);
        public bool Contains(object item)
        {
            if(IsCompatibleObject(item))
            {
                return Contains((T)item);
            }
            return false;
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) => _internalList.ConvertAll(converter);

        public void CopyTo(T[] array) => CopyTo(array, 0);
        public void CopyTo(T[] array, int arrayIndex) => _internalList.CopyTo(array, arrayIndex);
        public void CopyTo(int index, T[] array, int arrayIndex, int count) => _internalList.CopyTo(index, array, arrayIndex, count);
        public void CopyTo(Array array, int arrayIndex)
        {
            if((array != null) && (array.Rank != 1))
            {
                throw new ArgumentException("Multi-range array is not supported.");
            }
            Contract.EndContractBlock();

            try
            {
                _internalList.CopyTo((T[])array, arrayIndex);
            }
            catch(ArrayTypeMismatchException)
            {
                throw new ArgumentException($"Invalid array type: {typeof(T)}.");
            }
        }

        public bool Exists(Predicate<T> match) => _internalList.Exists(match);

        public T Find(Predicate<T> match) => _internalList.Find(match);

        public List<T> FindAll(Predicate<T> match) => _internalList.FindAll(match);

        public int FindIndex(Predicate<T> match) => _internalList.FindIndex(match);
        public int FindIndex(int startIndex, Predicate<T> match) => _internalList.FindIndex(startIndex, match);
        public int FindIndex(int startIndex, int count, Predicate<T> match) => _internalList.FindIndex(startIndex, count, match);

        public T FindLast(Predicate<T> match) => _internalList.FindLast(match);

        public int FindLastIndex(Predicate<T> match) => _internalList.FindLastIndex(match);
        public int FindLastIndex(int startIndex, Predicate<T> match) => _internalList.FindLastIndex(startIndex, match);
        public int FindLastIndex(int startIndex, int count, Predicate<T> match) => _internalList.FindLastIndex(startIndex, count, match);

        public void ForEach(Action<T> action) => _internalList.ForEach(action);

        public IEnumerator<T> GetEnumerator() => _internalList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _internalList.GetEnumerator();

        public List<T> GetInternalList() => _internalList;

        public NotifyingList<T> GetRange(int index, int count) => new NotifyingList<T>(_internalList.GetRange(index, count));

        public int IndexOf(T item) => _internalList.IndexOf(item);
        public int IndexOf(T item, int index) => _internalList.IndexOf(item, index);
        public int IndexOf(T item, int index, int count) => _internalList.IndexOf(item, index, count);
        public int IndexOf(object item)
        {
            if(IsCompatibleObject(item))
            {
                return IndexOf((T)item);
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            _internalList.Insert(index, item);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnInsert(item, index);
        }
        public void Insert(int index, object item)
        {
            if(item == null && !(default(T) == null))
                throw new ArgumentNullException(nameof(item));

            try
            {
                Insert(index, (T)item);
            }
            catch(InvalidCastException)
            {
                throw new ArgumentException($"Argument is not of type {typeof(T)}.", nameof(item));
            }
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            foreach(T item in collection)
            {
                Insert(index++, item);
            }
        }

        public int LastIndexOf(T item) => _internalList.LastIndexOf(item);
        public int LastIndexOf(T item, int index) => _internalList.LastIndexOf(item, index);
        public int LastIndexOf(T item, int index, int count) => _internalList.LastIndexOf(item, index, count);

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
        public void Remove(object item)
        {
            if(IsCompatibleObject(item))
            {
                Remove((T)item);
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            var matches = _internalList.FindAll(match);

            foreach(T item in matches)
                Remove(item);

            return matches.Count;
        }

        public void RemoveAt(int index)
        {
            var item = _internalList[index];
            _internalList.RemoveAt(index);
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(IndexerName);
            OnRemove(item, index);
        }

        public void RemoveRange(int index, int count)
        {
            var range = _internalList.GetRange(index, count);

            foreach(T item in range)
            {
                Remove(item);
            }
        }

        public void Reverse() => Reverse(0, Count);
        public void Reverse(int index, int count)
        {
            if(index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Argument can't be a negative number.");
            if(count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Argument can't be a negative number.");
            if(Count - index < count)
                throw new IndexOutOfRangeException();
            Contract.EndContractBlock();

            var i = index;
            var j = index + count - 1;

            while(i < j)
            {
                T temp = _internalList[i];
                _internalList[i] = _internalList[j];
                _internalList[j] = temp;
                OnSwap(i, j);
                i++;
                j--;
            }
        }

        public void Sort() => Sort(0, Count, null);
        public void Sort(IComparer<T> comparer) => Sort(0, Count, comparer);
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            var oldList = new List<T>(_internalList);
            _internalList.Sort(index, count, comparer);
            CheckIndexes(oldList, index, count);
        }
        public void Sort(Comparison<T> comparison)
        {
            var oldList = new List<T>(_internalList);
            _internalList.Sort(comparison);
            CheckIndexes(oldList, 0, Count);
        }

        public T[] ToArray() => _internalList.ToArray();

        public void TrimExcess() => _internalList.TrimExcess();

        public bool TrueForAll(Predicate<T> match) => _internalList.TrueForAll(match);
    }
}
