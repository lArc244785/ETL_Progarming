using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collections
{
	public class Node <T>
	{
		public T? value;
		public Node<T>? prev = null;
		public Node<T>? next = null;
		public Node(T value)
		{
			this.value = value;
		}
	}



	internal class MyLinkedList<T>:IEnumerable where T : IComparable<T>
	{
		public struct Enumerator : IEnumerator<T>
		{
			private MyLinkedList<T> _linkedList;
			private Node<T>? _node;
			private Node<T>? _error;

			public T Current => _node.value;

			object IEnumerator.Current => _node.value;

			public Enumerator(MyLinkedList<T> linkedList)
			{
				_error = new Node<T>(default);
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

		private Node<T> _first;
		private Node<T> _last;
		private Node<T> _tmp;
		private int _count;

		public Node<T> First => _first;
		public Node<T> Last => _last;
		public int Count => _count;

		#region Add
		private bool InitNode(T value)
		{
			if(_first == null || _last == null)
			{
				_tmp = new Node<T>(value);
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

			_tmp = new Node<T>(value);

			_first.prev = _tmp;
			_tmp.next = _first;
			_first = _tmp;

			_count++;
		}

		public void AddLast(T value)
		{
			if (InitNode(value))
				return;

			_tmp = new Node<T>(value);

			_last.next = _tmp;
			_tmp.prev = _last;
			_last = _tmp;

			_count++;
		}

		public void AddFront(Node<T> target,T value)
		{
			if (InitNode(value))
				return;
			
			if(target == _first)
			{
				AddFirst(value);
				return;
			}

			_tmp = new Node<T>(value);

			Node<T> targetPrev = target.prev;
			targetPrev.next = _tmp;
			_tmp.prev = targetPrev;
			_tmp.next = target;
			target.prev = _tmp;

			_count++;
		}

		public void AddBack(Node<T> target, T value)
		{
			if (InitNode(value))
				return;

			if(target == _last)
			{
				AddLast(value);
				return;
			}

			_tmp = new Node<T>(value);
			Node<T> targetNext = target.next;
			
			target.next = _tmp;
			_tmp.prev = target;
			_tmp.next = targetNext;
			targetNext.prev = _tmp;

			_count++;
		}
		#endregion

		#region Find
		public Node<T> Find(Predicate<T> match)
		{
			Node<T> result = null;
			_tmp = _first;
			while(_tmp != null && result == null)
			{
				if(match(_tmp.value))
					result = _tmp;

				_tmp = _tmp.next;
			}

			return result;
		}

		public Node<T> FindLast(Predicate<T> match)
		{
			Node<T> result = null;
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

		private bool Remove(Node<T> node)
		{
			if (node == null)
				return false;

			Node<T> leftNode = node.prev;
			Node<T> rightNode = node.next;

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
