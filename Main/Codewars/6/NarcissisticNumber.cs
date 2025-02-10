//https://www.codewars.com/kata/5287e858c6b5a9678200083c/train/csharp

namespace Main.Codewars
{
	public class NarcissisticNumber
	{
		public static bool Narcissistic(int value)
		{
			var digits = value.ToString().ToArray();
			return (int)digits.Select(d => System.Math.Pow(d - '0', digits.Length)).Sum() == value;
		}

		public static void TestAll()
		{
			Test(1, true);
			Test(371, true);
		}

		public static void Test(int value, bool excepted)
		{
			Console.WriteLine(Narcissistic(value) == excepted ? "OK" : $"Error - {value}");
		}
	}
}
