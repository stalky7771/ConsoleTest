//https://www.codewars.com/kata/546d15cebed2e10334000ed9

using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using static Main.Codewars.BefungeInterpreter;

namespace Main.Codewars
{
	public class Runes
	{
		private static readonly Dictionary<string, Func<int, int, int>> Commands =
			new() { { "+", (x, y) => x + y }, { "-", (x, y) => x - y }, { "*", (x, y) => x * y } };

		public static int solveExpressionMySolution(string expression)
		{
			int firstNumberSign = 1;
			if (expression.StartsWith("-"))
			{
				firstNumberSign = -1;
				expression = expression.Remove(0, 1);
			}

			foreach (var s in "0123456789".Except(expression).ToArray())
			{
				var tmpExpression = expression.Replace("?", s.ToString());

				var numbers = tmpExpression.Split("=");
				var leftPart = numbers[0];

				var signIndex = Array.FindIndex(leftPart.ToArray(), c => c == '+' || c == '-' || c == '*');
				var sign = leftPart[signIndex].ToString();
				var strA = new string(leftPart.Take(signIndex).ToArray());
				var strB = new string(leftPart.Skip(signIndex+1).ToArray());
				var strC = numbers[1];

				if (IsStartedBy0(strA) || IsStartedBy0(strB) || IsStartedBy0(strC))
					continue;

				if (Commands[sign](int.Parse(strA) * firstNumberSign, int.Parse(strB)) == int.Parse(strC))
					return int.Parse(s.ToString());
			}
			return -1;
		}

		static bool IsStartedBy0(string s)
		{
			s = s.Replace("-", "");
			return s.Length > 1 && s.StartsWith("0");
		}

		public static int solveExpression(string expression)
		{
			string[] sections = expression.Split('=');
			DataTable dt = new DataTable();
			for (int i = 0; i <= 9; i++)
			{
				if (expression.Contains(i.ToString()))
					continue;
				if (dt.Compute(sections[0].Replace('?', i.ToString()[0]), "").ToString() == sections[1].Replace('?', i.ToString()[0]))
					return i;
			}
			return -1;
		}

		public static void Test(int result, string expression)
		{
			var res = solveExpression(expression) == result ? "Ok" : "Error";
			Console.WriteLine($"{res} - {expression}");
		}

		public static void TestAll()
		{
			Test(2, "1+1=?");
			Test(6, "123*45?=5?088");
			Test(0, "-5?*-1=5?");
			Test(-1, "19--45=5?");
			Test(5, "??*??=302?");
			Test(2, "?*11=??");
			Test(2, "??*1=??");
			Test(-1, "??+??=??");
			Test(8, "-?56373--9216=-?47157");
		}
	}
}
