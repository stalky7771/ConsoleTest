//https://www.codewars.com/kata/534e01fbbb17187c7e0000c6/train/csharp

using System.Text;

namespace Main.Codewars
{
	public class Spiralizor
	{
		enum Direction { R, D, L, U };

		private static (int x, int y, int farX, int farY) NextPosition(Direction d, int x, int y)
		{
			var res = (x, y, farX: x, farY: y);
			switch (d)
			{
				case Direction.R: res.x = x + 1; res.farX = x + 2; break;
				case Direction.D: res.y = y + 1; res.farY = y + 2; break;
				case Direction.L: res.x = x - 1; res.farX = x - 2; break;
				case Direction.U: res.y = y - 1; res.farY = y - 2; break;
			}
			return res;
		}

		private static bool IsEnablePosition(int[,] g, Direction d, int x, int y)
		{
			switch (d)
			{
				case Direction.R: return g[y - 1, x    ] == 0 && g[y + 1, x    ] == 0 && g[y,     x + 1] == 0;
				case Direction.D: return g[y    , x + 1] == 0 && g[y    , x - 1] == 0 && g[y + 1, x    ] == 0;
				case Direction.L: return g[y    , x - 1] == 0 && g[y - 1, x    ] == 0 && g[y + 1, x    ] == 0;
				case Direction.U: return g[y    , x - 1] == 0 && g[y    , x + 1] == 0 && g[y - 1, x    ] == 0;
			}
			return false;
		}

		public static int[,] Spiralize(int size)
		{
			var direction = Direction.R;
			var pos = (x: -1, y: 0);
			var visited = new HashSet<(int hashX, int hashY)>();
			var grid = new int[size, size];

			var nextDirection = new Func<Direction, Direction>(d => (Direction)(((int)d + 1) % 4));

			for (int row = 0; row < grid.GetLength(0); row++)
				for (int col = 0; col < grid.GetLength(1); col++)
					grid[row, col] = 0;

			while (true)
			{
				var nextPos = NextPosition(direction, pos.x, pos.y);

				if (visited.Contains((nextPos.farY, nextPos.farX)))
				{
					direction = nextDirection(direction);
					nextPos = NextPosition(direction, pos.x, pos.y);
					if (   visited.Contains((nextPos.farY, nextPos.farX)) 
					    || !IsEnablePosition(grid, direction, nextPos.x, nextPos.y))
					{
						break; // finish
					}
					continue;
				}

				if (   direction == Direction.R && nextPos.x == size
				    || direction == Direction.D && nextPos.y == size
				    || direction == Direction.L && nextPos.x == -1)
				{
					direction = nextDirection(direction);
					continue;
				}

				pos.x = nextPos.x;
				pos.y = nextPos.y;

				grid[pos.y, pos.x] = 1;
				visited.Add((pos.y, pos.x));
			}

			return grid;
		}

		private static string ArrayToStr(int[,] grid)
		{
			int size = grid.GetLength(0);
			var result = new StringBuilder();
			for (int i = 0; i < size; i++)
			{
				var sb = new StringBuilder();
				for (int j = 0; j < size; j++)
				{
					sb.Append(grid[i, j] + " ");
				}

				result.AppendLine(sb.ToString());
			}

			return result.ToString();
		}

		public static void TestAll()
		{
			Test_5();
			Test_8();
			Test_10();
		}

		public static void Test_5()
		{
			int[,] expected = {
				{1, 1, 1, 1, 1},
				{0, 0, 0, 0, 1},
				{1, 1, 1, 0, 1},
				{1, 0, 0, 0, 1},
				{1, 1, 1, 1, 1}
			};
			int size = expected.GetLength(0);
			Console.WriteLine(CompareArrays(expected, Spiralize(size), size) ? "Ok" : "Error");
		}

		public static void Test_8()
		{
			int[,] expected = {
				{1, 1, 1, 1, 1, 1, 1, 1},
				{0, 0, 0, 0, 0, 0, 0, 1},
				{1, 1, 1, 1, 1, 1, 0, 1},
				{1, 0, 0, 0, 0, 1, 0, 1},
				{1, 0, 1, 0, 0, 1, 0, 1},
				{1, 0, 1, 1, 1, 1, 0, 1},
				{1, 0, 0, 0, 0, 0, 0, 1},
				{1, 1, 1, 1, 1, 1, 1, 1},
			};
			int size = expected.GetLength(0);
			Console.WriteLine(CompareArrays(expected, Spiralize(size), size) ? "Ok" : "Error");
		}

		public static void Test_10()
		{
			int[,] expected = {
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
				{0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
				{1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
				{1, 0, 0, 0, 0, 0, 0, 1, 0, 1},
				{1, 0, 1, 1, 1, 1, 0, 1, 0, 1},
				{1, 0, 1, 0, 0, 1, 0, 1, 0, 1},
				{1, 0, 1, 0, 0, 0, 0, 1, 0, 1},
				{1, 0, 1, 1, 1, 1, 1, 1, 0, 1},
				{1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
				{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			};
			int size = expected.GetLength(0);
			Console.WriteLine(CompareArrays(expected, Spiralize(size), size) ? "Ok" : "Error");
		}

		private static bool CompareArrays(int[,] a1, int[,] a2, int size)
		{
			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if (a1[i, j] != a2[i, j])
						return false;
				}
			}
			return true;
		}
	}
}
