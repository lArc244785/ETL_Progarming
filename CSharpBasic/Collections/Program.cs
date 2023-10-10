using System.Collections;

namespace Collections
{
	internal class Program
	{
		static void Main(string[] args)
		{
			MyDynamicArray<string> data1 = new();
			data1.Add("철수");
			data1.Add("영희");
			data1.Add("재욱");
			data1.Add("나성");
			data1.Add("김도");

			MyDynamicArray<string> data2 = new();
			data2.Add("수철");
			data2.Add("희영");
			data2.Add("도김");

			using(IEnumerator<string>e1 = data1.GetEnumerator())
			using(IEnumerator<string>e2 = data2.GetEnumerator())
			{
				while(e1.MoveNext() && e2.MoveNext())
				{
					Console.WriteLine($"e1 {e1.Current}");
					Console.WriteLine($"e2 {e2.Current}");
				}
				e1.Reset();
				e2.Reset();
			}

		}
	}
}