using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
	//T의 자료형이 IComparable로 변환을 할 수 있어야된다.
	internal class MyDynamicArray<T> where T : IComparable<T>
	{
		public T this[uint index]
		{
			get
			{
				if (index >= Count)
					throw new IndexOutOfRangeException();

				return _items[index];
			}
			set
			{
				if (index >= Count)
					throw new IndexOutOfRangeException();

				_items[index] = value;
			}
		}

		public int Count => _count;
		public int Capacity => _items.Length;

		private int _count;
		private T[] _items = new T[DEFAULT_SIZE];
		private const int DEFAULT_SIZE = 1;

		public void Add(T item)
		{
			if(_count >= _items.Length)
			{
				T[] tmp = new T[_count * 2];
				Array.Copy(_items, tmp, _items.Length);
				_items = tmp;
			}
			_items[_count++] = item;
		}

		public object Find(Predicate<T> match)
		{
			for(int i = 0; i < _count; i++)
			{
				if (match(_items[i]))
					return _items[i];
			}
			return default;
		}

		public int FindIndex(Predicate<T> match)
		{
			for (int i = 0; i < _count; i++)
			{
				if (match(_items[i]))
					return i;
			}
			return -1;
		}

		public bool Contains(T item)
		{
			for(int i = 0; i< _count; i++)
			{
				
				if (Comparer<T>.Default.Compare(_items[i], item) == 0) 
					return true;
			}

			return false;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();

			for(int i = index; i < _count -1; i++)
			{
				_items[i] = _items[i + 1];
			}
			_count--;
		}

		public bool Remove(T item)
		{
			int index = FindIndex(x => x.CompareTo(item) == 0);

			if (index < 0)
				return false;

			RemoveAt(index);
			return true;
		}

	}
}
