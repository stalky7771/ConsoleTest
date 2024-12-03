using System.Linq.Expressions;

namespace Main.Codewars
{
	public class Parser
	{
		private readonly Dictionary<string, Func<double, double>> _functions = new();
		enum LexType { None, Operator, Number, Func }
		private LexType _tok_type;
		private string _token;
		private string _expression;
		private int _index = 0;

		private string _exp_ptr => _index < _expression.Length ? _expression[_index].ToString() : string.Empty;

		public Parser()
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

		public double eval_exp(string str)
		{
			double result = 0;
			_expression = str.Replace(" ", "").Replace("---", "-").Replace("&-", "^").ToLower();
			get_token();
			eval_exp2(ref result);
			return result;
		}

		void eval_exp2(ref double result)
		{
			string op;
			double temp = 0;

			eval_exp3(ref result);
			while ((op = _token) == "+" || op == "-")
			{
				get_token();
				eval_exp3(ref temp);
				switch (op)
				{
					case "-": result -= temp; break;
					case "+": result += temp; break;
				}
			}
		}

		void eval_exp3(ref double result)
		{
			string op;
			double temp = 0;

			eval_exp4(ref result);
			while ((op = _token) == "*" || op == "/")
			{
				get_token();
				eval_exp4(ref temp);
				switch (op)
				{
					case "*": result *= temp; break;
					case "/": result /= temp; break;
				}
			}
		}

		void eval_exp4(ref double result)
		{
			string op = string.Empty;

			if (_tok_type == LexType.Operator && _token == "+" || _token == "-")
			{
				op = _token;
				get_token();
			}
			eval_exp5(ref result);
			if (op == "-")
				result = -result;
		}

		void eval_exp5(ref double result)
		{
			double temp = 0;

			eval_exp6(ref result);
			if (_token == "&" || _token == "^")
			{
				var t = _token;
				get_token();
				eval_exp5(ref temp);
				result = t == "&" ? System.Math.Pow(result, temp) : 1 / System.Math.Pow(result, temp);
			}
		}

		void eval_exp6(ref double result)
		{
			if (_token == "(")
			{
				get_token();
				eval_exp2(ref result);
				if (_token != ")")
				{
					int aaa = 0; // error
				}
				get_token();
			}
			else
			{
				eval_exp7(ref result);
			}
		}

		void eval_exp7(ref double result)
		{
			if (_tok_type == LexType.Func)
			{
				var funcName = _token;
				get_token();
				eval_exp6(ref result);
				result = _functions[funcName](result);
				//get_token();
				eval_exp2(ref result);
			}
			else
			{
				atom(ref result);
			}
		}

		void atom(ref double result)
		{
			switch (_tok_type)
			{
				case LexType.Number:
					result = double.Parse(_token);
					get_token();
					return;
			}
		}

		void get_token()
		{
			_token = string.Empty;
			_tok_type = LexType.None;

			if (_index == _expression.Length)
				return;

			if (IsOperator(_exp_ptr))
			{
				_tok_type = LexType.Operator;
				_token = _exp_ptr;
				_index++;
			}
			else if ("0123456789,e".Contains(_exp_ptr))
			{
				_tok_type = LexType.Number;
				bool isExponent = false;
				while ((isExponent && (_exp_ptr == "-" || _exp_ptr == "+")) || !IsOperator(_exp_ptr))
				{
					isExponent = _exp_ptr == "e";

					_token += _exp_ptr;
					_index++;
					if (_index == _expression.Length)
						break;
				}
			}
			else
			{
				_tok_type = LexType.Func;
				while (!IsOperator(_exp_ptr))
				{
					_token += _exp_ptr;
					_index++;
					if (_index == _expression.Length)
						break;
				}
			}
		}

		bool IsOperator(string c)
		{
			return "+-/*&^()".Contains(c);
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
			
			//Test("abs(-2 * 1e-3)", "0,002");
		}

		public static void Test(string expression, string expected)
		{
			Parser p = new Parser();
			var result = p.eval_exp(expression).ToString();
			Console.WriteLine(result == expected ? "OK" : $"FAIL {expression}");
		}
	}
}
