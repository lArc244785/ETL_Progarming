using System.Collections;

namespace Collections
{
	internal class Program
	{
		static void ShowLinkedList<T>(MyLinkedList<T> linkedList) where T : IComparable<T>
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

			int removeValue = 235;

			list.AddFirst(1);
			list.AddFront(list.First , 2);
			list.AddLast(3);
			list.AddBack(list.Last, 4);
			list.AddFront(list.Last, removeValue);

			ShowLinkedList<int>(list);

			Console.WriteLine(list.Find((x) => x == 2).value);
			Console.WriteLine(list.FindLast((x) => x > 3).value);

			Console.WriteLine();


			list.Remove(removeValue);


			ShowLinkedList<int>(list);

			Console.WriteLine(list.Count + "\n");

			foreach(var item in list)
			{
				Console.WriteLine(item);
			}

		}
	}
}