using System;
using System.Drawing;

namespace Main.Codewars
{
	public class BefungeInterpreter
	{
		public enum Direction { Up, Right, Down, Left };

		private Point _pos = Point.Empty;
		private Direction _direction = Direction.Right;
		private readonly Stack<long> _stack = new();
		private char[][] _map;
		private string _output;
		private bool _isTextMode;

		public Point GetNextPos(Direction direction, Point pos)
		{
			switch (direction)
			{
				case Direction.Up: pos.Y--; break;
				case Direction.Right: pos.X++; break;
				case Direction.Down: pos.Y++; break;
				case Direction.Left: pos.X--; break;
			}
			return pos;
		}

		public string Interpret(string code)
		{
			var lines = code.Split('\n');
			_map = lines.Select((x, i) => lines[i].ToCharArray()).ToArray();

			while (true)
			{
				var c = _map[_pos.Y][_pos.X];
				if (c == '@')
					break; 

				Check(c);
				_pos = GetNextPos(_direction, _pos);
			}

			return _output;
		}

		public void Check(char c)
		{
			if (c == '"')
			{
				_isTextMode = !_isTextMode;
				return;
			}

			if (_isTextMode)
			{
				_stack.Push(Convert.ToInt32(c));
				return;
			}

			if ("0123456789".Contains(c))
			{
				_stack.Push(Convert.ToInt32(c) - Convert.ToInt32('0'));
				return;
			}

			switch (c)
			{
				case ' ': break;
				case '>': _direction = Direction.Right; break;
				case '^': _direction = Direction.Up; break;
				case '<': _direction = Direction.Left; break;
				case 'v': _direction = Direction.Down; break;
				case ':': _stack.Push(_stack.Count != 0 ? _stack.Peek() : 0); break;
				case '_': _direction = _stack.Pop() == 0 ? Direction.Right : Direction.Left; break;
				case '.': _output += _stack.Pop(); break;
				case ',': _output += Convert.ToChar(_stack.Pop()); break;
				case '+': _stack.Push(_stack.Pop() + _stack.Pop()); break;
				case '-':
				{
					var a = _stack.Pop();
					var b = _stack.Pop();
					_stack.Push(b - a);
				} break;

				case '*': _stack.Push(_stack.Pop() * _stack.Pop()); break;

				case '/':
				{
					var a = _stack.Pop();
					var b = _stack.Pop();
					_stack.Push(a == 0 ? 0 : b / a);
				} break;

				case '%':
				{
					var a = _stack.Pop();
					var b = _stack.Pop();
					_stack.Push(a == 0 ? 0 : b % a);
				} break;

				case '!': _stack.Push(_stack.Pop() == 0 ? 1 : 0); break;
				case '`': _stack.Push(_stack.Pop() < _stack.Pop() ? 1 : 0); break;

				case '?':
				{
					var directions = ((Direction[])Enum.GetValues(typeof(Direction)));
					_direction = directions[new Random().Next(0, directions.Length)];
				} break;

				case '|': _direction = _stack.Pop() == 0 ? Direction.Down : Direction.Up; break;

				case '\\':
				{
					var a = _stack.Pop();
					if (_stack.Count == 0) 
						_stack.Push(0);
					var b = _stack.Pop();
					_stack.Push(a);
					_stack.Push(b);
				} break;

				case '$': _stack.Pop(); break;
				case '#': _pos = GetNextPos(_direction, _pos); break;
				case 'p': _map[_stack.Pop()][_stack.Pop()] = Convert.ToChar(_stack.Pop()); break;
				case 'g': _stack.Push(Convert.ToInt32(_map[_stack.Pop()][_stack.Pop()])); break;
			}
		}

		public static void Test()
		{
			/*
			[>] [9] [8] [7] [v] [>] [.] [v]
			[v] [4] [5] [6] [<] [ ] [ ] [:]
			[>] [3] [2] [1] [ ] [^] [ ] [_] [@]
			*/
			//			var obj = new BefungeInterpreter();
			//			Console.WriteLine(obj.Interpret(">987v>.v\nv456<  :\n>321 ^ _@"));

			/*for (int i = 0; i < 100; i++)
			{
				var tmpArray = new Direction[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left };
				Console.WriteLine(tmpArray[new Random().Next(0, tmpArray.Length)]);
			}*/

			//var obj = new BefungeInterpreter();
			//var res = obj.Interpret(">25*\"!dlroW olleH\":v\n                v:,_@\n                >  ^");
			//Console.WriteLine(res);
			/*
			[>][2][5][*]["][!][d][l][r][o][W][ ][o][l][l][e][H]["][:][v]
			[ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][v][:][,][_][@]
			[ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][ ][>][ ][ ][^]
			 */

			//char c1 = '!';
			//int a0 = Convert.ToInt32(c1);
			//int a = Convert.ToInt32(c1) - Convert.ToInt32('!');
			//char c2 = (char)(a + Convert.ToInt32('!'));
			//int b = 0;
			//return;

			//var obj = new BefungeInterpreter();
			//var res = obj.Interpret("44* >:1-:v    v ,*25 .:* ,,,,\"! = \".:_ @\n    ^    _ $1 > \\:                   ^");
			//Console.WriteLine(res);
		}
	}
}
