// https://www.codewars.com/kata/52c4dd683bfd3b434c000292

namespace Main.Codewars
{
	public class CatchingCarMileageNumbers
	{
		public static int IsInteresting(int number, List<int> awesomePhrases)
		{
			return Enumerable.Range(number, 3)
				.Where(x => Interesting(x, awesomePhrases))
				.Select(x => (number - x + 4) / 2)
				.FirstOrDefault();
		}

		private static bool Interesting(int num, List<int> awesome)
		{
			if (num < 100) return false;
			var s = num.ToString();
			return awesome.Contains(num)
			       || s.Skip(1).All(c => c == '0')
			       || s.Skip(1).All(c => c == s[0])
			       || "1234567890".Contains(s)
			       || "9876543210".Contains(s)
			       || s.SequenceEqual(s.Reverse());
		}
	}
}
