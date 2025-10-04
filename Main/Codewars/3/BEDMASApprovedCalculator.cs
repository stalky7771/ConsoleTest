// https://www.codewars.com/kata/56a14b6b56e5917073000022

using System.Globalization;

namespace Main.Codewars._3
{
	internal class BEDMASApprovedCalculator
	{
		private static readonly Dictionary<string, int> Precedence = new()
		{
			{ "+", 1 },
			{ "-", 1 },
			{ "*", 2 },
			{ "/", 2 },
			{ "^", 3 }
		};

		private static bool IsOperator(string token) => Precedence.ContainsKey(token);

		public static double Calculate(string s)
		{
			var tokens = Tokenize(s);
			var postfix = ToPostfix(tokens);
			return EvaluatePostfix(postfix);
		}

		private static List<string> Tokenize(string expr)
		{
			var tokens = new List<string>();
			string number = "";

			foreach (char c in expr)
			{
				if (char.IsDigit(c) || c == '.')
				{
					number += c;
				}
				else if ("+-*/()^".Contains(c))
				{
					if (number != "")
					{
						tokens.Add(number);
						number = "";
					}
					tokens.Add(c.ToString());
				}
				else if (char.IsWhiteSpace(c))
				{
					if (number != "")
					{
						tokens.Add(number);
						number = "";
					}
				}
			}

			if (number != "")
				tokens.Add(number);

			return tokens;
		}

		private static List<string> ToPostfix(List<string> tokens)
		{
			var output = new List<string>();
			var stack = new Stack<string>();

			foreach (var token in tokens)
			{
				if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
				{
					output.Add(token);
				}
				else if (IsOperator(token))
				{
					while (stack.Count > 0 && IsOperator(stack.Peek()) &&
						   Precedence[token] <= Precedence[stack.Peek()])
					{
						output.Add(stack.Pop());
					}
					stack.Push(token);
				}
				else if (token == "(")
				{
					stack.Push(token);
				}
				else if (token == ")")
				{
					while (stack.Count > 0 && stack.Peek() != "(")
						output.Add(stack.Pop());
					stack.Pop(); // remove "("
				}
			}

			while (stack.Count > 0)
				output.Add(stack.Pop());

			return output;
		}

		private static double EvaluatePostfix(List<string> postfix)
		{
			var stack = new Stack<double>();

			foreach (var token in postfix)
			{
				if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
				{
					stack.Push(num);
				}
				else if (IsOperator(token))
				{
					double b = stack.Pop();
					double a = stack.Pop();

					double result = token switch
					{
						"+" => a + b,
						"-" => a - b,
						"*" => a * b,
						"/" => a / b,
						"^" => System.Math.Pow(a,  b)
					};

					stack.Push(result);
				}
			}

			return stack.Pop();
		}

		public static bool IsEqual(double a, double b)
		{
			return System.Math.Abs(a - b) < 0.000000001;
		}
		
		public static void TestAll()
		{
			//var res = Calculate("(2 / (2 + 3.33) * 4) - -6");

			Test("3 + 5 * (2 - 1)", 8);
			Test("1 + 2", 3);
			Test("2*2", 4);
		}

		private static void Test(string s, double expected)
		{
			if (IsEqual(Calculate(s), expected))
				Console.WriteLine("Pass");
			else
				Console.WriteLine("FAIL");
		}
	}
}
