using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections
{
	public class MyLinkedListNode <T>
	{
		public T? value;
		public MyLinkedListNode<T>? prev = null;
		public MyLinkedListNode<T>? next = null;
		public MyLinkedListNode(T value)
		{
			this.value = value;
		}
	}



	internal class MyLinkedList<T>:IEnumerable where T : IComparable<T>
	{
		public struct Enumerator : IEnumerator<T>
		{
			private MyLinkedList<T> _linkedList;
			private MyLinkedListNode<T>? _node;
			private MyLinkedListNode<T>? _error;

			public T Current => _node.value;

			object IEnumerator.Current => _node.value;

			public Enumerator(MyLinkedList<T> linkedList)
			{
				_error = new MyLinkedListNode<T>(default);
				_linkedList = linkedList;
				_node = _error;
			}

			public void Dispose()
			{
				
			}

			public bool MoveNext()
			{
				if (_node == null)
				{
					return false;
				}

				_node = (_node == _error) ? _linkedList.First : _node.next;

				return _node != null;
			}

			public void Reset()
			{
				_node = _error;
			}
		}

		private MyLinkedListNode<T> _first;
		private MyLinkedListNode<T> _last;
		private MyLinkedListNode<T> _tmp;
		private int _count;

		public MyLinkedListNode<T> First => _first;
		public MyLinkedListNode<T> Last => _last;
		public int Count => _count;

		#region Add
		private bool InitNode(T value)
		{
			if(_first == null || _last == null)
			{
				_tmp = new MyLinkedListNode<T>(value);
				_first = _tmp;
				_last = _tmp;
				_count++;
				return true;
			}
			return false;
		}
		public void AddFirst(T value)
		{
			if (InitNode(value))
				return;

			_tmp = new MyLinkedListNode<T>(value);

			_first.prev = _tmp;
			_tmp.next = _first;
			_first = _tmp;

			_count++;
		}

		public void AddLast(T value)
		{
			if (InitNode(value))
				return;

			_tmp = new MyLinkedListNode<T>(value);

			_last.next = _tmp;
			_tmp.prev = _last;
			_last = _tmp;

			_count++;
		}

		public void AddFront(MyLinkedListNode<T> target,T value)
		{
			if (InitNode(value))
				return;
			
			if(target == _first)
			{
				AddFirst(value);
				return;
			}

			_tmp = new MyLinkedListNode<T>(value);

			MyLinkedListNode<T> targetPrev = target.prev;
			targetPrev.next = _tmp;
			_tmp.prev = targetPrev;
			_tmp.next = target;
			target.prev = _tmp;

			_count++;
		}

		public void AddBack(MyLinkedListNode<T> target, T value)
		{
			if (InitNode(value))
				return;

			if(target == _last)
			{
				AddLast(value);
				return;
			}

			_tmp = new MyLinkedListNode<T>(value);
			MyLinkedListNode<T> targetNext = target.next;
			
			target.next = _tmp;
			_tmp.prev = target;
			_tmp.next = targetNext;
			targetNext.prev = _tmp;

			_count++;
		}
		#endregion

		#region Find
		public MyLinkedListNode<T> Find(Predicate<T> match)
		{
			MyLinkedListNode<T> result = null;
			_tmp = _first;
			while(_tmp != null && result == null)
			{
				if(match(_tmp.value))
					result = _tmp;

				_tmp = _tmp.next;
			}

			return result;
		}

		public MyLinkedListNode<T> FindLast(Predicate<T> match)
		{
			MyLinkedListNode<T> result = null;
			_tmp = _last;
			while (_tmp != null && result == null)
			{
				if (match(_tmp.value))
					result = _tmp;

				_tmp = _tmp.prev;
			}

			return result;
		}
		#endregion

		private bool Remove(MyLinkedListNode<T> node)
		{
			if (node == null)
				return false;

			MyLinkedListNode<T> leftNode = node.prev;
			MyLinkedListNode<T> rightNode = node.next;

			if (leftNode != null)
			{
				leftNode.next = rightNode;
			}
			else
			{
				rightNode.prev = null;
				_first = rightNode;
			}

			if(rightNode != null)
			{
				rightNode.prev = leftNode;
			}
			else
			{
				leftNode.next = null;
				_last = leftNode;
			}
			_count--;
			return true;
		}

		public bool Remove(T value)
		{
			return Remove(Find((x) => x.CompareTo(value) == 0));
		}

		public bool RemoveLast(T value)
		{
			return Remove(FindLast((x) => x.CompareTo(value) == 0));
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}
	}
}
