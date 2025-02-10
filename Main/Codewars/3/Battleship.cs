using System.Text;

namespace Main.Codewars
{
	public class Battleship
	{
		public static bool ValidateBattlefield(int[,] field)
		{
			var count = 0;

			var size = field.GetLength(0);
			for (var i = 0; i < size; i++)
				for (var j = 0; j < size; j++)
					if (field[i, j] == 1)
						count++;

			if (count != 20)
				return false;

			var grid = new int[size + 2, size + 2];
			size = grid.GetLength(0); // 12
			for (var i = 0; i < size; i++)
				for (var j = 0; j < size; j++)
					if (i == 0 || j == 0 || i == size - 1 || j == size - 1)
					{
						if (grid[i, j] != 3)
							grid[i, j] = 2;
					}
					else
					{
						var tmp = field[i - 1, j - 1];
						if (tmp == 0 && (grid[i, j] == 2 || grid[i, j] == 3))
							continue;

						grid[i, j] = tmp;
						SetBorder(grid, i, j);
					}

			count = 0;
			for (var i = 0; i < size; i++)
				for (var j = 0; j < size; j++)
					if (grid[i, j] == 3)
						count++;

			var str = PringGrid(grid);

			return count == 68;
		}

		public static void SetBorder(int[,] grid, int i, int j)
		{
			var cell = grid[i, j];
			if (cell == 0)
				return;

			for (var x = i - 1; x <= i + 1; x++)
				for (var y = j - 1; y <= j + 1; y++)
					if (grid[x, y] != 1)
						grid[x, y] = 3;
		}

		public static string PringGrid(int[,] grid)
		{
			var sb = new StringBuilder();
			var size = grid.GetLength(0);
			for (var i = 0; i < size; i++)
			{
				for (var j = 0; j < size; j++)
				{
					sb.Append($"{grid[i, j]} ");
				}

				sb.AppendLine("");
			}
			return sb.ToString();
		}

		public static void Test()
		{
			int[,] field = new int[,]
			{
				{ 1, 0, 0, 0, 0, 1, 1, 0, 0, 0 },
				{ 1, 0, 1, 0, 0, 0, 0, 0, 1, 0 },
				{ 1, 0, 1, 0, 1, 1, 1, 0, 1, 0 },
				{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
				{ 0, 0, 0, 0, 1, 1, 1, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
				{ 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
				{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			};

			//int[,] field = new int[,]
			//{
			//	{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0  },
			//	{ 1, 0, 1, 0, 0, 0, 0, 0, 1, 0  },
			//	{ 1, 0, 1, 0, 1, 1, 1, 0, 1, 0  },
			//	{ 1, 0, 1, 0, 0, 0, 0, 0, 0, 0  },
			//	{ 0, 0, 1, 0, 0, 0, 0, 0, 1, 0  },
			//	{ 0, 0, 0, 0, 1, 1, 1, 0, 0, 0  },
			//	{ 0, 0, 0, 0, 0, 0, 0, 0, 1, 0  },
			//	{ 0, 0, 0, 1, 0, 0, 0, 0, 0, 0  },
			//	{ 0, 0, 0, 0, 0, 0, 0, 1, 0, 0  },
			//	{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			//};
			var res = ValidateBattlefield(field);
		}
	}
}
