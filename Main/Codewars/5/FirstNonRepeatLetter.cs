//https://www.codewars.com/kata/52bc74d4ac05d0945d00054e/csharp

namespace Main.Codewars._5
{
	public class FirstNonRepeatLetter
	{
		public static string FirstNonRepeatingLetter(string s)
		{
			if (string.IsNullOrEmpty(s))
				return "";

			var str = s.ToLower();

			var hsFirstEntry = new HashSet<char>();
			var hsRepeating = new HashSet<char>();
			
			foreach (var c in str)
			{
				if (hsFirstEntry.Contains(c))
				{
					if (hsRepeating.Contains(c) == false)
						hsRepeating.Add(c);
				}
				else
				{
					hsFirstEntry.Add(c);
				}
			}

			var result = hsFirstEntry.Except(hsRepeating).ToList();

			if (result.Count == 0)
				return "";

			int index = 0;
			foreach (var tmpChar in str)
			{
				if (result.Contains(tmpChar))
					break;

				index++;
			}

			return s[index].ToString();
		}

		public static void TestAll()
		{
			Test("stress", "t");
			Test("sTreSS", "T");
			Test("stre", "s");
			Test("ssss", "");
			Test("moonmen", "e");
		}

		public static void Test(string s, string expected)
		{
			if (FirstNonRepeatingLetter(s) == expected)
				Console.WriteLine("OK");
			else
				Console.WriteLine("Error");
		}

	}
}
