using System.Linq.Expressions;

namespace Main.Codewars
{
	public class Parser
	{
		enum LexType { None, Operator, Number }
		private LexType _tok_type;
		private string _token;
		private string _expression;
		private int _index = 0;

		private string _exp_ptr => _index < _expression.Length ? _expression[_index].ToString() : string.Empty;

		public double eval_exp(string str)
		{
			double result = 0;
			_expression = str.Replace(" ", "");
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

			eval_exp5(ref result);
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

		void eval_exp5(ref double result)
		{
			double temp = 0;

			eval_exp6(ref result);
			if (_token == "&")
			{
				get_token();
				eval_exp5(ref temp);
				result = System.Math.Pow(result, temp);
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

			if ("+-*/&()".Contains(_exp_ptr))
			{
				_tok_type = LexType.Operator;
				_token = _exp_ptr;
				_index++;
			}
			else if ("0123456789,".Contains(_exp_ptr))
			{
				_tok_type = LexType.Number;
				while (!isdelim(_exp_ptr))
				{
					_token += _exp_ptr;
					_index++;
					if (_index == _expression.Length)
						break;
				}
			}
			//Console.WriteLine($">>> GetToken {_token} - {_tok_type}");
		}

		bool isdelim(string c)
		{
			return "+-/*&()".Contains(c);
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
			Test("-2&2", "-4");
			//Test("2&0", "1");
			//Test("---5*2", "-10");
			//Test("0,01*4", "0,04");
			//Test("((2 + 3) * (1 + 2)) * 4 & -2", "0,9375");
			//Test("10 * 5 + 2&3", "58");
			//Test("2 * 3 + 2 & 2 * 4", "22");
			//Test("5-- 6", "11");
			//Test("4&2", "16");
			//Test("2*3&2", "18");
			//Test("4 & 3 & 2", "262144");
			//Test("-5&3&2*2-1", "-3906251");
		}

		public static void Test(string expression, string expected)
		{
			Parser p = new Parser();
			var result = p.eval_exp(expression).ToString();
			Console.WriteLine(result == expected ? "OK" : $"FAIL {expression}");
		}
	}
}
