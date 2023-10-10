using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
	public class Node <T>
	{
		public T value;
		public Node<T> prev = null;
		public Node<T> next = null;
		public Node(T value)
		{
			this.value = value;
		}
	}


	internal class MyLinkedList <T>
	{
		private Node<T> _first;
		private Node<T> _last;

		public Node<T> First => _first;
		public Node<T> Last => _last;

		#region Add
		private bool InitNode(Node<T> node)
		{
			if(_first == null || _last == null)
			{
				_first = node;
				_last = node;
				return true;
			}
			return false;
		}
		public void FirstAdd(Node<T> node)
		{
			if (InitNode(node))
				return;

			_first.prev = node;
			node.next = _first;
			_first = node;
		}

		public void LastAdd(Node<T> node)
		{
			if (InitNode(node))
				return;

			_last.next = node;
			node.prev = _last;
			_last = node;
		}

		public void FrontAdd(Node<T> target,Node<T> node)
		{
			if (InitNode(node))
				return;
			
			if(target == _first)
			{
				FirstAdd(node);
				return;
			}

			Node<T> targetPrev = target.prev;
			targetPrev.next = node;
			node.prev = targetPrev;
			node.next = target;
			target.prev = node;
		}

		public void BackAdd(Node<T> target, Node<T> node)
		{
			if (InitNode(node))
				return;

			if(target == _last)
			{
				LastAdd(node);
				return;
			}

			Node<T> targetNext = target.next;
			target.next = node;
			node.prev = target;
			node.next = targetNext;
			targetNext.prev = node;
		}
		#endregion

		#region Find
		public Node<T> FirstFind(Predicate<T> match)
		{
			Node<T> result = null;
			Node<T> current = _first;
			while(current != null && result == null)
			{
				if(match(current.value))
				{
					result = current;
				}

				current = current.next;
			}

			return result;
		}

		public Node<T> LastFind(Predicate<T> match)
		{
			Node<T> result = null;
			Node<T> current = _last;
			while (current != null && result == null)
			{
				if (match(current.value))
				{
					result = current;
				}

				current = current.prev;
			}

			return result;
		}
		#endregion

		public void Remove(Node<T> node)
		{
			Node<T> prevNode = node.prev;
			Node<T> nextNode = node.next;

			if (prevNode != null)
				prevNode.next = nextNode;
			else
				_first = nextNode;

			if (nextNode != null)
				nextNode.prev = prevNode;
			else
				_last = prevNode;

		}
	}
}
