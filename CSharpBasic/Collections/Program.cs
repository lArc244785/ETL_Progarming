using System.Collections;

namespace Collections
{
	internal class Program
	{

		static void Main(string[] args)
		{
			MyHashTable<string, int> hashTable = new MyHashTable<string, int>();
			hashTable.Add("A", 1);
			hashTable.Add("B", 2);
			hashTable.Add("C", 3);
			hashTable.Add("D", 4);
			hashTable.Add("E", 5);
			hashTable.Add("F", 6);

			if (!hashTable.TryAdd("F", 7))
				Console.WriteLine("실패");

			Console.WriteLine("--------------");

			foreach (var item in hashTable.Values)
				Console.WriteLine(item);

			Console.WriteLine("--------------");

			foreach (var item in hashTable.Keys)
				Console.WriteLine(item);

			Console.WriteLine("--------------");
			foreach (var item in hashTable)
			{
				Console.WriteLine($"{item.Key} {item.Value}");
			}
		}
	}
}