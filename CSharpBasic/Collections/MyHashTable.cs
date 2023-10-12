using System.Collections;

namespace Collections
{
	public struct KeyValuePair<T, K> : IEquatable<KeyValuePair<T, K>>
		where T : IEquatable<T>
		where K : IEquatable<K>
	{
		public T? Key;
		public K? Value;
		public KeyValuePair(T? key, K? value)
		{
			Key = key;
			Value = value;
		}

		public bool Equals(KeyValuePair<T, K> other)
		{
			return other.Key.Equals(Key) && other.Value.Equals(Value);
		}
	}

	internal class MyHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
		where TKey : IEquatable<TKey>
		where TValue : IEquatable<TValue>
	{
		public TValue this[TKey key]
		{
			get
			{
				var bucket = _bukets[Hash(key)];

				if (bucket == null)
					throw new Exception($"[MyHashtable<{nameof(TKey)} , {nameof(TValue)}>] : Key{key} doesn't exist");

				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
						return bucket[i].Value;
				}

				throw new Exception($"[MyHashtable<{nameof(TKey)} , {nameof(TValue)}>] : Key{key} doesn't exist");
			}
			set
			{
				var bucket = _bukets[Hash(key)];

				if (bucket == null)
					throw new Exception($"[MyHashtable<{nameof(TKey)} , {nameof(TValue)}>] : Key{key} doesn't exist");

				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
					{
						bucket[i] = new(key, value);
					}
				}

				throw new Exception($"[MyHashtable<{nameof(TKey)} , {nameof(TValue)}>] : Key{key} doesn't exist");
			}
		}

		public IEnumerable<TKey> Keys
		{
			get
			{
				List<TKey> keys = new();
				for (int i = 0; i < _valideIndexList.Count; i++)
				{
					for (int j = 0; j < _bukets[_valideIndexList[i]].Count; j++)
					{
						keys.Add(_bukets[_valideIndexList[i]][j].Key);
					}
				}
				return keys;
			}
		}

		public IEnumerable<TValue> Values
		{
			get
			{
				List<TValue> values = new();
				for (int i = 0; i < _valideIndexList.Count; i++)
				{
					for (int j = 0; j < _bukets[_valideIndexList[i]].Count; j++)
					{
						values.Add(_bukets[_valideIndexList[i]][j].Value);
					}
				}
				return values;
			}
		}

		private const int DEFAULT_SIZE = 100;
		private List<KeyValuePair<TKey, TValue>>[] _bukets =
			new List<KeyValuePair<TKey, TValue>>[DEFAULT_SIZE];

		//유효한 인덱스들을 저장하는곳
		private List<int> _valideIndexList = new();

		public int Hash(TKey key)
		{
			string keyName = key.ToString();
			int result = 0;
			for (int i = 0; i < keyName.Length; i++)
			{
				result += keyName[i];
			}

			result %= DEFAULT_SIZE;
			return result;
		}
		public void Add(TKey key, TValue value)
		{
			int index = Hash(key);
			var bucket = _bukets[index];

			if (bucket == null)
			{
				_bukets[index] = new();
				bucket = _bukets[index];

				_valideIndexList.Add(index);
			}
			else
			{
				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
						throw new Exception($"[MyHashtable<{nameof(TKey)} , {nameof(TValue)}>] : Key{key} doesn't exist");
				}
			}
			bucket.Add(new(key, value));
		}
		public bool TryAdd(TKey key, TValue value)
		{
			int index = Hash(key);
			var bucket = _bukets[index];

			if (bucket == null)
			{
				_bukets[index] = new();
				_bukets[index].Add(new(key, value));
				_valideIndexList.Add(index);
			}
			else
			{
				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
						return false;
				}
			}

				bucket.Add(new(key, value));
			return true;
		}
		public bool TryGetValue(TKey key, out TValue value)
		{
			int index = Hash(key);
			var bucket = _bukets[index];

			if (bucket != null)
			{
				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
					{
						value = bucket[i].Value;
						return true;
					}
				}
			}

			value = default;
			return false;
		}

		public bool Remove(TKey key)
		{
			int index = Hash(key);
			var bucket = _bukets[index];
			if (bucket != null)
			{
				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
					{
						bucket.RemoveAt(i);
						if (bucket.Count == 0)
							_valideIndexList.Remove(index);
						return true;
					}
				}
			}
			return false;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		struct Enumerator : IEnumerator<KeyValuePair<TKey,TValue>>
		{
			private MyHashTable <TKey, TValue>? _hashTable;

			private List<KeyValuePair<TKey, TValue>>? _buket;
			private int _valideIndexListIndex;
			private int _buketIndex;

			public Enumerator(MyHashTable<TKey, TValue>? hashTable)
			{
				_hashTable = hashTable;

				_valideIndexListIndex = 0;
				_buketIndex = -1;
				_buket = _hashTable._bukets[_hashTable._valideIndexList[_valideIndexListIndex]];
			}

			public KeyValuePair<TKey, TValue> Current => _buket != null ? _buket[_buketIndex] : default;

			object IEnumerator.Current => _buket != null ? _buket[_buketIndex] : default;

			public void Dispose()
			{
				
			}

			public bool MoveNext()
			{
				if (_valideIndexListIndex == -1)
				{
					_valideIndexListIndex++;
					_buket = _hashTable._bukets[_hashTable._valideIndexList[_valideIndexListIndex]];
					return true;
				}
				else
				{
					_buketIndex++;
					if(_buketIndex == _buket.Count)
					{
						_buketIndex = 0;
						_valideIndexListIndex++;
						
						if (_valideIndexListIndex == _hashTable._valideIndexList.Count)
							return false;
						_buket = _hashTable._bukets[_hashTable._valideIndexList[_valideIndexListIndex]];
					}	
				}

				return true;
			}

			public void Reset()
			{
				_buket = null;
				_valideIndexListIndex = -1;
				_buketIndex = 0;
			}
		}

	}
}
