using System.Collections;

namespace Yield
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			IEnumerator e1 = GetEnumerator1();
			IEnumerator e2 = GetEnumerator2();


			while(e1.MoveNext())
			{
				Console.WriteLine(e1.Current);
			}

			while (e2.MoveNext())
			{
				Console.WriteLine(e2.Current);
			}

			foreach(var item in GetEnumerable())
			{
				Console.WriteLine(item);
			}
		}

		static IEnumerator GetEnumerator1()
		{
			return new MakingToastRoutine();
		}

		static IEnumerator GetEnumerator2()
		{
			yield return "인덕션 켜기";
			yield return "팬 준비";
			yield return "버터를 팬에 투하";
			yield return "빵을 팬에 투하";
			yield return "빵이 구워질 때 까지 기달리기";
			yield return "빵에 잼 올리기";
			yield return "인덕션 끄기";
			yield return "토스트 준비됨";
		}

		static IEnumerable GetEnumerable()
		{
			yield return "인덕션 켜기";
			yield return "팬 준비";
			yield return "버터를 팬에 투하";
			yield return "빵을 팬에 투하";
			yield return "빵이 구워질 때 까지 기달리기";
			yield return "빵에 잼 올리기";
			yield return "인덕션 끄기";
			yield return "토스트 준비됨";
		}


		struct MakingToastRoutine : IEnumerator
		{
			public object Current => _routine[_step];

			private string[] _routine =
			{
				"인덕션 켜기",
				"팬 준비",
				"버터를 팬에 투하",
				"빵을 팬에 투하",
				"빵이 구워질 때 까지 기달리기",
				"빵에 잼 올리기",
				"인덕션 끄기",
				"토스트 준비됨"
			};
			private int _step = -1;

			public MakingToastRoutine() { }

			public bool MoveNext()
			{
				if (_step >= _routine.Length)
					return false;
				_step++;
				return _step < _routine.Length;
			}

			public void Reset()
			{
				
			}
		}
	}
}