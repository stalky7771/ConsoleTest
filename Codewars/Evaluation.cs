//https://www.codewars.com/kata/564d9ebde30917684f000048/train/csharp

using System;
using System.Data.SqlTypes;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using static Main.Codewars.Lexeme;

namespace Main.Codewars
{
	public class Lexeme
	{
		public enum LexType
		{
			None,
			Number,
			Operation,
			Function
		}

		public LexType Type { get; private set; }
		public string Value { get; private set; }

		public Lexeme(LexType type, string value)
		{
			Type = type;
			Value = value;
		}

		public override string ToString()
		{
			return $"{Type} - {Value} ";
		}
	}

	public class Evaluation
	{
		private readonly Dictionary<string, Func<double, double>> _functions = new();
		private readonly List<(string name, Func<double, double, double> func)> _operations = new();
		private Dictionary<string, string> _numbers = new();
		private readonly List<string> _funcList;
		private readonly List<char> _operList = new();

		public Evaluation()
		{
			_operations.Add(("^", (x, y) => x - y)); // binary minus
			_operations.Add(("+", (x, y) => x + y));
			_operations.Add(("*", (x, y) => x * y));
			_operations.Add(("/", (x, y) => x / y));
			_operations.Add(("&", System.Math.Pow));

			_functions.Add("log",	System.Math.Log10);
			_functions.Add("ln",	System.Math.Log);
			_functions.Add("exp",	System.Math.Exp);
			_functions.Add("sqrt",	System.Math.Sqrt);
			_functions.Add("abs",	System.Math.Abs);
			_functions.Add("atan",	System.Math.Atan);
			_functions.Add("acos",	System.Math.Acos);
			_functions.Add("asin",	System.Math.Asin);
			_functions.Add("sinh",	System.Math.Sinh);
			_functions.Add("cosh",	System.Math.Cosh);
			_functions.Add("tanh",	System.Math.Tanh);
			_functions.Add("sin",	System.Math.Sin);
			_functions.Add("cos",	System.Math.Cos);
			_functions.Add("tan",	System.Math.Tan);
			_functions.Add("umin",	x => -1 * x);

			_funcList = _functions.Keys.OrderBy(x => x.Length).Reverse().ToList();
			_operations.ForEach(x => _operList.Add(x.name[0]));
			_operList.Add('(');
			_operList.Add(')');
		}

		public string eval(string expression)
		{
			//var bbb = System.Math.Sin(20 * System.Math.PI / 180);
			expression = expression.Replace(" ", "");
			expression = SeparateUnaryAndBinaryMinus(expression);
			(_numbers, expression) = ExtraceNumbers(expression);

			int index = 0;
			try
			{
				double result = CalcSubExpression(expression, ref index);
				return result.ToString();
			}
			catch (Exception e)
			{
				return "ERROR";
			}
		}

		private List<Lexeme> Parser(string expression)
		{
			var result = new List<Lexeme>();
			var buf = string.Empty;
			foreach (var e in expression)
			{
				if (_operList.Contains(e))
				{
					if (buf != string.Empty && _funcList.Contains(buf))
					{
						result.Add(new Lexeme(LexType.Function, buf));
						buf = string.Empty;
					}

					result.Add(new Lexeme(LexType.Operation, e.ToString()));
					continue;
				}

				if ("0123456789,".Contains(e))
				{
					if (buf != string.Empty && _funcList.Contains(buf))
					{
						result.Add(new Lexeme(LexType.Function, buf));
						buf = string.Empty;
					}

					result.Add(new Lexeme(LexType.Number, e.ToString()));
					continue;
				}

				buf += e;
			}
			return result;
		}

		private List<Lexeme> Simplify(List<Lexeme> list)
		{
			var result = new List<Lexeme>();
			var accum = string.Empty;
			foreach (var lexeme in list)
			{
				if (lexeme.Type == LexType.Number)
				{
					accum += lexeme.Value;
				}
				else
				{
					if (accum != string.Empty)
					{
						result.Add(new Lexeme(LexType.Number, accum));
						accum = string.Empty;
					}
					result.Add(lexeme);
				}
			}

			return result;
		}

		private bool IsOperator(char c)
		{
			var operators = new List<char>();
			operators.Add('^');
			operators.Add('+');
			operators.Add('*');
			operators.Add('/');
			operators.Add('&');
			return operators.Contains(c);
		}

		private (Dictionary<string, string> dict, string strOut) ExtraceNumbers(string str)
		{
			var tmpDict = new Dictionary<string, string>();
			var result = str;
			var regex = new Regex(@"[-+]?[0-9]*[.,]?[0-9]+(?:[eE][-+]?[0-9]+)?");
			var matches = regex.Matches(str);
			if (matches.Count > 0)
			{
				foreach (Match match in matches)
				{
					var number = match.Value;
					if (number.Contains("e") || number.Contains("E"))
					{
						var guid = Guid.NewGuid();
						var key = guid.ToString().Substring(0, 5).ToUpper();
						tmpDict.Add(key, number);
						result = result.Replace(number, key);
					}
				}
			}

			return (tmpDict, result);
		}

		private double CalcSubExpression(string str, ref int index, string funcName = "")
		{
			string subExpr = string.Empty;

			for (; index < str.Length; index++)
			{
				if (string.IsNullOrEmpty(funcName))
				{
					var subExpression = str.Substring(index);
					foreach (var func in _funcList)
					{
						if (subExpression.StartsWith(func, StringComparison.OrdinalIgnoreCase))
						{
							index += func.Length;
							subExpr += CalcSubExpression(str, ref index, func);
							break;
						}
					}
				}

				if (index >= str.Length)
					break;

				if (str[index] == ')')
					break;

				if (str[index] == '(')
				{
					index++;
					subExpr += CalcSubExpression(str, ref index, funcName);
					funcName = string.Empty;
				}
				else
				{
					subExpr += str[index];
				}
			}

			var result = solveExpression(subExpr);

			if (string.IsNullOrEmpty(funcName) == false)
			{
				if (funcName == "sqrt" && result < 0)
					throw new Exception("message");

				return _functions[funcName](result);
			}

			return result;
		}

		private double solveExpression(string expStr)
		{
			foreach (var op in _operations)
			{
				if (expStr.Contains(op.name))
				{
					var i = expStr.IndexOf(op.name, StringComparison.Ordinal);
					var left = expStr.Substring(0, i);
					var right = expStr.Substring(i + 1);
					var expLeft = solveExpression(left);
					var expRight = solveExpression(right);
					if (op.name == "/" && expRight == 0)
						throw new Exception("message");

					return op.func(expLeft, expRight);
				}
			}

			if (_numbers.ContainsKey(expStr))
			{
				var str = _numbers[expStr];
				return double.Parse(str);
			}
			
			return double.Parse(expStr);
		}

		private string SeparateUnaryAndBinaryMinus(string str)
		{
			var res = FindUnaryMinus(str).Replace("-", "^");
			res = SetUnaryMinusArgument(res);
			return res;
		}

		private string SetUnaryMinusArgument(string str)
		{
			var separator = "^+)";

			var il = str.IndexOf("M");
			if (il == -1)
				return str;

			il++;
			var ir = il;
			for (; ir < str.Length; ir++)
			{
				var symbol = str[ir];
				if (separator.Contains(symbol))
					break;
			}

			var left = il - 1 == 0 ? "" : str.Substring(il - 1);
			var arg = str.Substring(il, ir - il);
			var right = str.Substring(ir, str.Length - ir);

			var result = $"{left}umin({arg}){right}";

			return SetUnaryMinusArgument(result);
		}

		private string FindUnaryMinus(string str)
		{
			var result = str;
			var i = str.IndexOf("-", StringComparison.Ordinal);
			if (i == -1)
				return result;

			if (i == 0 || IsOperator(str[i - 1]) || str[i - 1] == '(' || str[i - 1] == ')') // unary minus
			{
				StringBuilder sb = new StringBuilder(str);
				sb[i] = 'M';
				result = sb.ToString();
			}

			if (result == str)
				return result;

			return FindUnaryMinus(result);
		}

		private int GetAgrumentSize(string str)
		{
			var brackets = 0;
			var index = 0;
			for (; index < str.Length; index++)
			{
				var symbol = str[index];
				if (IsOperator(symbol) && brackets <= 0)
				{
					break;
				}

				if (symbol == '(')
					brackets++;

				if (symbol == ')')
					brackets--;
			}
			return index;
		}

		public static void TestAll()
		{
			//Evaluation e = new Evaluation();
			//var tmp = e.Parser("4*(21+32,59)*sin(37)");
			//tmp = e.Simplify(tmp);
			//int aaa = 0;



			//var result = e.FindUnaryMinus("-5&3&2*2-1");
			//var result = e.FindUnaryMinus("(2+3)*(-5)&3&2*2-1");
			//var result = e.FindUnaryMinus("(2+3)*(-5)&3&2*2+-1");
			//var result = e.FindUnaryMinus("2+-1");
			//return;

			//e.Test("sin(0,349)", "0,342");
			//e.Test("20 + 10 + 5 * 8&2 / 2", "190");
			//e.Test("-6+5", "-1");
			//e.Test("6-5", "1");
			//e.Test("-2+2", "0");
			//e.Test("-2&2", "-4");
			//e.Test("---5*2", "-10");
			//e.Test("0,01*4", "0,04");
			//e.Test("10 * 5 + 2&3", "58");
			//e.Test("sqrt(90 - 9) * 2", "18");
			//e.Test("2 * 3 + 2 & 2 * 4", "22");
			//e.Test("5-- 6", "11");
			//e.Test("((2 + 3) * (1 + 2)) * 4 & 2", "240");
			//e.Test("((2 + 3) * (1 + 2)) * 4 & -2", "0,9375");
			//e.Test("4&-2", "0,9375");
			//e.Test("2*3&2", "18");
			//e.Test("abs(-(-1+(2*(4--3)))&2)", "169");
			//e.Test("sqrt(-2)*2", "ERROR");
			//e.Test("2*5/0", "ERROR");
			//e.Test("4 & 3 & 2", "262144");
			//e.Test("abs(-2 * 1e-3)", "0,002");
			//e.Test("((2+3)*(1+2))*4&2", "240");
			//e.Test("(-(2+3)*(1+2))*4&2", "-240");
			//e.Test("-5&3&2*2-1", "-3906251");

			// umin(5)&3&2*2-1

			//e.Test("sqrt(sin(2 + 3) * cos(1 + 2)) * 4 & 2", "15,5893529757165");
		}

		public void Test(string expression, string expected)
		{
			var res = eval(expression) == expected ? "OK" : $"FAIL {expression}";
			Console.WriteLine(res);
		}
	}
}
