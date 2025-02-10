using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Main.Codewars
{
	public class BinomialExpansion
	{	
		public static string Expand(string expr) //(ax + b)^n
		{
			var splitResult = expr.Split("^", StringSplitOptions.RemoveEmptyEntries);
			var baseExpr = splitResult[0].Replace("(", "").Replace(")", "");
			var n = int.Parse(splitResult[1]);
			if (n == 0)
				return "1";

			var x = new Regex("[A-Za-z]").Matches(baseExpr)[0];
			var tmp = baseExpr.Split(x.ToString());
			var a = tmp[0] == "-" ? -1
				: tmp[0] != "" 
					? int.Parse(tmp[0])
					: 1;

			var b = tmp[1] != "" ? int.Parse(tmp[1]) : 0;

			if (b == 0)
			{
				return a == 1
					? $"{x}^{n}"
					: a == -1
						? $"-{x}^{n}"
						: $"{System.Math.Pow(a, n)}{x}^{n}";
			}

			var sb = new StringBuilder();

			for (var k = 0; k <= n; k++)
			{
				var c_n_k = BinomRate(k, n, a);
				var powA = n - k;
				var powB = k;

				var strA = powA == 0
						? ""
						: powA == 1
							? x.ToString()
							: $"{x}^{powA}";

				c_n_k *= (long)System.Math.Pow(b, powB);
				var result = k == 0 && c_n_k == -1
					?	"-"
					:	k > 0 && c_n_k > 0
						? $"+{c_n_k}"
						: $"{c_n_k}";


				result = (result == "1" && strA != "1") ? strA : result + strA;

				sb.Append(result);
			}

			return sb.ToString();
		}

		private static long BinomRate(int k, int n, int a) => Factorial(n) / (Factorial(k) * Factorial(n - k)) * (long)System.Math.Pow(a, n - k);

		private static long Factorial(long num) => num == 0 ? 1 : num * Factorial(num - 1);

		public static void TestAll()
		{
			Test("(x+1)^2", "x^2+2x+1");
			Test("(p-1)^3", "p^3-3p^2+3p-1");
			Test("(2f+4)^6", "64f^6+768f^5+3840f^4+10240f^3+15360f^2+12288f+4096");
			Test("(-2a-4)^0", "1");
			Test("(-12t+43)^2", "144t^2-1032t+1849");
			Test("(r+0)^203", "r^203");
			Test("(-x-1)^2", "x^2+2x+1");
			Test("(9t-0)^2", "81t^2");
			Test("(-h+0)^9", "-h^9");
			Test("(u+0)^8", "u^8");
		}

		public static void Test(string expr, string excepted)
		{
			var res = Expand(expr);
			Console.WriteLine(res == excepted ? "OK" : $"Error {expr}");
		}
	}
}
