// https://www.codewars.com/kata/5552101f47fc5178b1000050/csharp

using static System.Math;

namespace Main.Codewars
{
	public class PlayingWithDigits
	{
		public static long digPow_V1(int n, int p)
		{
			var sum = Convert.ToInt64(n.ToString().Select(s => Pow(int.Parse(s.ToString()), p++)).Sum());
			return sum % n == 0 ? sum / n : -1;
		}

		public static long digPow_V2(int n, int p)
		{
			var value = n.ToString()
				.ToCharArray()
				.Select(c => int.Parse(c.ToString()))
				.Select((d, idx) => Pow(d, idx + p))
				.Sum();

			if ((value % n) != 0)
				return -1;

			return (long)value / n;
		}
	}
}
