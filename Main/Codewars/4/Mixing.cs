// https://www.codewars.com/kata/5629db57620258aa9d000014/train/csharp

using System.Globalization;
namespace Main.Codewars._4
{
	public class Mixing
	{
		public static string Mix(string s1, string s2)
		{
			var line1 = s1.Where(char.IsLower).Select(c => c).OrderBy(c => c).ToList();
			var line2 = s2.Where(char.IsLower).Select(c => c).OrderBy(c => c).ToList();
			var allChars = line1.Union(line2).Distinct().ToList();
			var strList = new List<string>();
			foreach (var c in allChars)
			{
				var maxLength1 = line1.Count(x => x == c);
				var maxLength2 = line2.Count(x => x == c);
				if (maxLength1 < 2 && maxLength2 < 2)
					continue;

				if (maxLength1 > maxLength2)
					strList.Add($"1:{new string(c, maxLength1)}"); 
				else if (maxLength2 > maxLength1)
					strList.Add($"2:{new string(c, maxLength2)}");
				else
					strList.Add($"=:{new string(c, maxLength1)}");
			}

			return string.Join("/", strList.OrderByDescending(s => s.Length).ThenBy(s => s, StringComparer.Ordinal));
		}

		public static void Test(string s1, string s2, string res)
		{
			Console.WriteLine(Mix(s1, s2) == res ? "Pass" : "FAIL");
		}

		public static void TestAll()
		{
			Test("Are they here", "yes, they are here", "2:eeeee/2:yy/=:hh/=:rr");
			Test("looping is fun but dangerous", "less dangerous than coding", "1:ooo/1:uuu/2:sss/=:nnn/1:ii/2:aa/2:dd/2:ee/=:gg");
			Test(" In many languages", " there's a pair of functions", "1:aaa/1:nnn/1:gg/2:ee/2:ff/2:ii/2:oo/2:rr/2:ss/2:tt");
			Test("Lords of the Fallen", "gamekult", "1:ee/1:ll/1:oo");
			Test("codewars", "codewars", "");
			Test("A generation must confront the looming ", "codewarrs", "1:nnnnn/1:ooooo/1:tttt/1:eee/1:gg/1:ii/1:mm/=:rr");
		}
	}
}
