//https://www.codewars.com/kata/564d9ebde30917684f000048/train/csharp

using System.Diagnostics;

namespace Main.Codewars
{
	public class Evaluate
	{
		private readonly Dictionary<string, Func<double, double>> _functions = new();
		enum LexType { None, Operator, Number, Func }
		private LexType _type;
		private string _token;
		private string _expression;
		private int _index = 0;

		private string Symbol => _index < _expression.Length ? _expression[_index].ToString() : string.Empty;

		public Evaluate()
		{
			_functions.Add("log", System.Math.Log10);
			_functions.Add("ln", System.Math.Log);
			_functions.Add("exp", System.Math.Exp);
			_functions.Add("sqrt", System.Math.Sqrt);
			_functions.Add("abs", System.Math.Abs);
			_functions.Add("atan", System.Math.Atan);
			_functions.Add("acos", System.Math.Acos);
			_functions.Add("asin", System.Math.Asin);
			_functions.Add("sinh", System.Math.Sinh);
			_functions.Add("cosh", System.Math.Cosh);
			_functions.Add("tanh", System.Math.Tanh);
			_functions.Add("sin", System.Math.Sin);
			_functions.Add("cos", System.Math.Cos);
			_functions.Add("tan", System.Math.Tan);
		}

		private static bool IsOperator(string c) => "+-/*&^()".Contains(c);

		public string eval(string expression)
		{
			//Console.WriteLine(expression);
			_index = 0;
			double result = 0;
			_type = LexType.None;
			_token = string.Empty;

			if (CheckCorrectFormat(expression.ToLower()) == false)
			{
				return "ERROR Input string was not in a correct format";
			}

			_expression = expression.Replace(" ", "").Replace("---", "-").Replace("&-", "^").Replace("--", "+").ToLower();

			try
			{
				GetNextToken();
				EvaluationExp2(ref result);
			}
			catch (Exception e)
			{
				return $"ERROR {e.Message}";
			}
			return result.ToString();
		}

		private bool CheckCorrectFormat(string str)
		{
			if (str.Count(c => c == '(') != str.Count(c => c == ')'))
				return false;

			foreach (var symbol in "0123456789,.e+-/*&^()")
			{
				str = str.Replace(symbol.ToString(), " ");
			}

			foreach (var name in str.Split(" ", StringSplitOptions.RemoveEmptyEntries))
			{
				if (_functions.ContainsKey(name) == false)
					return false;
			}

			return true;
		}

		private void EvaluationExp2(ref double result, bool flag = true)
		{
			string op;
			double temp = 0;

			if (flag)
			{
				EvaluationExp3(ref result);
			}
			
			while ((op = _token) == "+" || op == "-")
			{
				GetNextToken();
				EvaluationExp3(ref temp);
				switch (op)
				{
					case "-": result -= temp; break;
					case "+": result += temp; break;
				}
			}
		}

		private void EvaluationExp3(ref double result)
		{
			string op;
			double temp = 0;

			EvaluationExp4(ref result);
			while ((op = _token) == "*" || op == "/")
			{
				GetNextToken();
				EvaluationExp4(ref temp);
				switch (op)
				{
					case "*": result *= temp; break;
					case "/":
					{
						if (temp == 0)
							throw new Exception("Division by zero");
						
						result /= temp;
					} break;
				}
			}
		}

		private void EvaluationExp4(ref double result)
		{
			var op = string.Empty;

			if (_type == LexType.Operator && _token == "+" || _token == "-")
			{
				op = _token;
				GetNextToken();
			}

			var tmp = result;
			EvaluationExp5(ref result);
			if (op == "-")
			{
				result = -result;
			}
		}

		private void EvaluationExp5(ref double result)
		{
			double temp = 0;

			EvaluationExp6(ref result);
			if (_token == "&" || _token == "^")
			{
				var t = _token;
				GetNextToken();
				EvaluationExp5(ref temp);
				result = t == "&" ? System.Math.Pow(result, temp) : 1 / System.Math.Pow(result, temp);
			}
		}

		private void EvaluationExp6(ref double result)
		{
			if (_token == "(")
			{
				GetNextToken();
				EvaluationExp2(ref result);
				if (_token != ")")
				{
					throw new Exception("Formula / Calculation");
				}
				GetNextToken();
			}
			else
			{
				EvaluationExp7(ref result);
			}
		}

		private void EvaluationExp7(ref double result)
		{
			if (_type == LexType.Func)
			{
				var funcName = _token;
				GetNextToken();
				EvaluationExp6(ref result);
				if (funcName == "sqrt" && result < 0)
				{
					throw new Exception("Formula / Calculation");
				}
				result = _functions[funcName](result);
				EvaluationExp2(ref result, false);
			}
			else
			{
				EvaluationExp8(ref result);
			}
		}

		private void EvaluationExp8(ref double result)
		{
			switch (_type)
			{
				case LexType.Number:
					result = double.Parse(_token);
					GetNextToken();
					return;
			}
		}

		private void GetNextToken()
		{
			_token = string.Empty;
			_type = LexType.None;

			if (_index == _expression.Length)
				return;

			if (IsOperator(Symbol))
			{
				_type = LexType.Operator;
				_token = Symbol;
				_index++;
			}
			else if ("0123456789,e".Contains(Symbol))
			{
				_type = LexType.Number;
				bool isExponent = false;
				while ((isExponent && (Symbol == "-" || Symbol == "+")) || !IsOperator(Symbol))
				{
					isExponent = Symbol == "e";

					_token += Symbol;
					_index++;
					if (_index == _expression.Length)
						break;
				}
			}
			else
			{
				_type = LexType.Func;
				while (!IsOperator(Symbol))
				{
					_token += Symbol;
					_index++;
					if (_index == _expression.Length)
						break;
				}
			}
		}

		public static void TestAll()
		{
			//Test("10-2*3", "4");
			//Test("(10-2)*3", "24");
			//Test("((2 + 3) * (1 + 2)) * 4 & 2", "240");
			//Test("(-(2+3)*(1+2))*4&2", "-240");
			//Test("20 + 10 + 5 * 8&2 / 2", "190");
			//Test("-6+5", "-1");
			//Test("6-5", "1");
			//Test("-2+2", "0");
			//Test("-2&2", "-4");
			//Test("2&0", "1");
			//Test("---5*2", "-10");
			//Test("0,01*4", "0,04");
			//Test("((2 + 3) * (1 + 2)) * 4 & -2", "0,9375");
			//Test("4 & -2", "0,0625");
			//Test("10 * 5 + 2&3", "58");
			//Test("2 * 3 + 2 & 2 * 4", "22");
			//Test("5-- 6", "11");
			//Test("4&2", "16");
			//Test("2*3&2", "18");
			//Test("4 & 3 & 2", "262144");
			//Test("-5&3&2*2-1", "-3906251");
			//Test("1E-2&2", "0,0001");
			//Test("1e+2 - 20", "80");
			//Test("-1e-1 * 5", "-0,5");
			//Test("sin(cos(1))", "0,5143952585235492");
			//Test("sqrt (sin(2 + 3)*cos (1+2)) * 4 & 2", "15,589352975716475");
			//Test("sin(2 + 3) * cos(1 + 2)", "0,9493278367245317");
			//Test("sin(20) * cos(30)", "0,1408231285927205");
			//Test("sin(2 + 3)", "-0,9589242746631385");
			//Test("cos(1+2)", "-0,9899924966004454");
			//Test("abs(-2 * 1e-3)", "0,002");
			//Test("1/0", "ERROR");
			//Test("sqrt(-2)", "ERROR");
			//Test("sin(cos(--14--2*1e+2))", "0,8026419077374775");
			//Test("si  n(1)*3", "ERROR");
			//Test("(5+2))", "ERROR");
			//Test("sqrt (1e-1*1e1)-15", "-14");
			//Test("sin(1)-15", "-14,158529015192103");
			//Test("10&(1+1--1+-1)", "100");
			//Test("sqrt(10&(1+1--1+-1))", "10");
			//Test("sqrt(15&(1+1--1+-1))", "15");
			//Test("abs(1+(2-5)--1)", "1");
			//Test("abs(1+(2-5)--1)* sin(3 + -1)", "0,9092974268256817");
			//Test("sin(cos(tan(1+2+-1-3-2-1--1)))", "0,8359477452180156");
			//Test("abs(1+(2-5)--1)* sin(3 + -1) / 2,1 * sin(cos(tan(1+2+-1-3-2-1--1)))", "0,3619643493749867");
			//Test("0,9092974268256817 / 2,1 * 0,8359477452180156", "0,3619643493749867");
			//Test("sqrt (1e-1*1e1)--4", "5");
			//Test("abs(1+(2-5)-10)* sin(3 + 10) / 2,1", "2,4009544961522336");
			//Test("abs(1+(2-5)--16)* sin(3 + -16) / 2,1", "-2,8011135788442725");
			//Test("abs(1+(2-5)-0)*sin(3 + 0) / 2,1", "0,134400007676064");
		}

		public static void Test(string expression, string expected)
		{
			Evaluate p = new Evaluate();
			var result = p.eval(expression);
			if (result.Contains("ERROR"))
				result = "ERROR";

			Console.WriteLine(result == expected ? "OK" : $"FAIL {expression}");
		}
	}

	// using Python
	public class Evaluate2
	{
		public static string eval(string expression)
		{
			try
			{
				expression = expression.ToLower().Replace("&", "**");
				ProcessStartInfo procStartInfo = new ProcessStartInfo("python", "-c \"from math import *; r=" + expression + "; print(r if r<1e14 else '{0:.12e}'.format(r))\"");
				procStartInfo.RedirectStandardOutput = true;
				procStartInfo.RedirectStandardError = true;
				procStartInfo.UseShellExecute = false;
				procStartInfo.CreateNoWindow = true;
				Process proc = new Process();
				proc.StartInfo = procStartInfo;
				proc.Start();
				string result = proc.StandardOutput.ReadToEnd();
				return result == "" ? "ERROR" : result.Trim();
			}
			catch (Exception e) { Console.WriteLine(e.ToString()); return "ERROR"; }
		}
	}
}
