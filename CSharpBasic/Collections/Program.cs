using System.Collections;

namespace Collections
{
	internal class Program
	{
		static void ShowLinkedList<T>(MyLinkedList<T> linkedList)
		{
			Node<T> node = linkedList.First;
			while(node != null)
			{
				Console.WriteLine(node.value);
				node = node.next;
			}

			Console.WriteLine();
		}

		static void Main(string[] args)
		{
			MyLinkedList<int> list = new MyLinkedList<int>();

			Node<int> removeNode = new Node<int>(235);

			list.FirstAdd(new Node<int>(1));
			list.FrontAdd(list.First , new Node<int>(2));
			list.LastAdd(new Node<int>(3));
			list.BackAdd(list.Last, new Node<int>(4));
			list.FrontAdd(list.Last, removeNode);

			ShowLinkedList<int>(list);

			Console.WriteLine(list.FirstFind((x) => x == 2).value);
			Console.WriteLine(list.LastFind((x) => x > 3).value);

			Console.WriteLine();

			list.Remove(removeNode);

			ShowLinkedList<int>(list);

		}
	}
}