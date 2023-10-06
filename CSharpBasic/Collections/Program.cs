namespace Collections
{
	internal class Program
	{
		static void Main(string[] args)
		{
			MyDynamicArray<string> da = new();
			da.Add("철수");
			Console.WriteLine($"동적 배열의 0번째 인덱스 값: {da[0]}");
		}
	}
}