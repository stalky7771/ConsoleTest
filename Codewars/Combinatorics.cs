//https://www.codewars.com/kata/541af676b589989aed0009e7/train/csharp

using System.Text;

namespace Main.Codewars
{
	public class Combinatorics
	{
		public static int CountCombinations(int money, int[] coins)
		{
			return Counter(money, new Queue<int>(new List<int>(coins).OrderBy(n => n).Reverse().ToList()));
		}

		private static int Counter(int money, Queue<int> coins)
		{
			var res = 0;

			var coin = coins.Dequeue();
			var rate = money / coin;

			for (var i = rate; i >= 0; i--)
			{
				var newMoney = money - coin * i;

				if (newMoney == 0)
				{
					res++;
				}
				else if (coins.Count > 0)
				{
					res += Counter(newMoney, new Queue<int>(coins));
				}
			}

			return res;
		}

		public static void TestAll()
		{
			Test(4, new[] { 1, 2 }, 3);
			Test(10, new[] { 5, 2, 3 }, 4);
			Test(11, new[] { 5, 7 }, 0);
			Test(199, new[] { 3, 5, 9, 15 }, 760);

			Test(300, new[] { 5, 10, 20, 50, 100, 200, 500, }, 1022);

			Test(419, new[] { 2, 5, 10, 20, 50 }, 18515);
		}

		public static void Test(int money, int[] coins, int expected)
		{
			Console.WriteLine(ConditionToStr(money, coins));
			var result = CountCombinations(money, coins);
			var str = result == expected ? "Ok" : "Error";
			Console.WriteLine(str + $"\tResult: {result}, expected {expected}");
		}

		private static void Print(List<int> coinList)
		{
			var sb = new StringBuilder();
			foreach (var i in coinList)
			{
				sb.Append($"{i} ");
			}
			var res = 0;
			foreach (var c in coinList)
			{
				res += c;
			}
			Console.WriteLine($"{sb.ToString()} sum: {res}");
		}

		private static string ConditionToStr(int money, int[] coins)
		{
			var sb = new StringBuilder();
			sb.Append($"Money: {money}, ");
			sb.Append($"Coins: ");
			foreach (var c in coins)
			{
				sb.Append($" {c}");
			}
			
			return sb.ToString();
		}
	}
}
