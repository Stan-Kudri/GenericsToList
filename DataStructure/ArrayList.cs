﻿using System.Collections;

namespace DataStructure
{
    public class ArrayList<T> : IEnumerable<T>
    {
        private const int GrowFactor = 2;

        private T[] ListItem = Array.Empty<T>();

        private int _size = 0;

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < _size)
                {
                    return ListItem[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            set
            {
                if (index >= 0 && index < _size)
                {
                    ListItem[index] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int Count => _size;

        public int Capacity
        {
            get => ListItem.Length;
            set
            {
                if (value < _size)
                {
                    throw new ArgumentException("Size more value", nameof(value));
                }
                if (value != _size)
                {
                    var array = new T[value];
                    if (_size >= 1)
                    {
                        ListItem.CopyTo(array, 0);
                    }
                    ListItem = array;
                }
                if (value == 0)
                {
                    ListItem = Array.Empty<T>();
                }
            }
        }

        private void DoubleCapacity() => Capacity = GrowFactor * _size;

        private void DefaultValue()
        {
            Capacity = 10;
        }

        private void ResizeList()
        {
            DoubleCapacity();
        }

        public ArrayList()
        {
            DefaultValue();
        }

        public ArrayList(T[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (array.Length > 0)
            {
                _size = array.Length;

                if (_size >= Capacity)
                {
                    DoubleCapacity();
                }
                array.CopyTo(ListItem, 0);
            }
            else
            {
                ListItem = Array.Empty<T>();
            }
        }

        public ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            if (capacity == 0)
            {
                ListItem = Array.Empty<T>();
            }
            if (Capacity != capacity)
            {
                Capacity = capacity;
            }
        }

        public void Add(T item)
        {
            if (Capacity == 0)
            {
                DefaultValue();
            }

            _size++;

            if (_size >= Capacity)
            {
                ResizeList();
            }

            ListItem[_size - 1] = item;
        }

        public bool Contain(T item) => _size != 0 && IndexOf(item) >= 0;

        public int IndexOf(T item) => Array.IndexOf(ListItem, item, 0, _size);

        public bool Remove(T item)
        {
            var index = IndexOf(item);

            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < _size)
            {
                _size--;
                Array.Copy(ListItem, index + 1, ListItem, index, _size - index);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public void Clear()
        {
            if (Capacity > 0)
            {
                if (_size > 0)
                {
                    Array.Clear(ListItem, 0, _size);
                    _size = 0;
                }
            }
        }

        public IEnumerator<T> GetEnumerator() => new ArrayEnumerator<T>(ListItem);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class ArrayEnumerator<T> : IEnumerator<T>
        {
            private readonly T[] _itemArray;

            private int _index;

            public int Index => _index;

            public ArrayEnumerator(T[]? array)
            {
                if (array == null)
                {
                    throw new ArgumentNullException(nameof(array), "Array not null.");
                }
                _itemArray = array;
                _index = -1;
            }

            object IEnumerator.Current => Index;

            T IEnumerator<T>.Current => _itemArray[Index];


            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_index + 1 >= _itemArray.Length)
                {
                    return false;
                }
                _index++;
                return true;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
