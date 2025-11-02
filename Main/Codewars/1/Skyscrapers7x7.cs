// https://www.codewars.com/kata/5917a2205ffc30ec3a0000a8/train/csharp

namespace Main.Codewars._1;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Skyscrapers7x7_Parallel
{
	const int N = 4;
	static int?[] top, bottom, left, right;
	static int[,] solution = new int[N, N];
	static volatile bool solved = false; // общий флаг, чтобы остановить другие потоки

	public static void TestAll()
	{
		// Пример подсказок (можно заменить своими)
		top = new int?[] { 3, null, 2, null, 4, null, 1 };
		bottom = new int?[] { null, 2, null, 3, null, 1, null };
		left = new int?[] { 2, null, null, 3, null, 2, null };
		right = new int?[] { null, 3, null, 1, null, null, 4 };

		// Генерируем все возможные перестановки строк
		var allRows = GetAllPermutations(N);

		// Предварительно фильтруем строки по боковым подсказкам
		var validRows = new List<int[]>[N];
		for (int i = 0; i < N; i++)
		{
			validRows[i] = allRows.Where(r =>
				(!left[i].HasValue || CountVisible(r, true) == left[i].Value) &&
				(!right[i].HasValue || CountVisible(r, false) == right[i].Value)
			).ToList();
		}

		Console.WriteLine("Начинаем поиск решения...");

		// Используем параллельные потоки для разных первых строк
		Parallel.ForEach(validRows[0], (firstRow, state) =>
		{
			if (solved) state.Stop(); // если решение уже найдено, выходим

			int[,] grid = new int[N, N];
			for (int c = 0; c < N; c++)
				grid[0, c] = firstRow[c];

			if (SolveParallel(1, grid, validRows))
			{
				solved = true;
				lock (solution)
				{
					Array.Copy(grid, solution, grid.Length);
				}
				state.Stop(); // останавливаем остальные потоки
			}
		});

		if (solved)
		{
			Console.WriteLine("\n✅ Решение найдено:");
			PrintSolution();
		}
		else
		{
			Console.WriteLine("❌ Решение не найдено.");
		}
	}

	// Перебор строк с сохранением текущего состояния сетки
	static bool SolveParallel(int rowIndex, int[,] grid, List<int[]>[] validRows)
	{
		if (rowIndex == N)
			return CheckColumns(grid);

		foreach (var row in validRows[rowIndex])
		{
			if (CanPlaceRow(rowIndex, row, grid))
			{
				for (int c = 0; c < N; c++)
					grid[rowIndex, c] = row[c];

				if (SolveParallel(rowIndex + 1, grid, validRows))
					return true;
			}
		}
		return false;
	}

	// Проверка — можно ли поставить строку (без повторов в столбцах)
	static bool CanPlaceRow(int rowIndex, int[] row, int[,] grid)
	{
		for (int col = 0; col < N; col++)
		{
			for (int prevRow = 0; prevRow < rowIndex; prevRow++)
				if (grid[prevRow, col] == row[col])
					return false;
		}
		return true;
	}

	// Проверка всех подсказок сверху и снизу
	static bool CheckColumns(int[,] grid)
	{
		for (int col = 0; col < N; col++)
		{
			int[] colVals = new int[N];
			for (int row = 0; row < N; row++)
				colVals[row] = grid[row, col];

			if (top[col].HasValue && CountVisible(colVals, true) != top[col].Value)
				return false;
			if (bottom[col].HasValue && CountVisible(colVals, false) != bottom[col].Value)
				return false;
		}
		return true;
	}

	// Подсчёт видимых небоскрёбов
	static int CountVisible(int[] row, bool fromLeft)
	{
		int max = 0, count = 0;
		if (fromLeft)
		{
			foreach (var x in row)
				if (x > max) { max = x; count++; }
		}
		else
		{
			for (int i = row.Length - 1; i >= 0; i--)
				if (row[i] > max) { max = row[i]; count++; }
		}
		return count;
	}

	// Генерация всех перестановок чисел 1..N
	static List<int[]> GetAllPermutations(int n)
	{
		int[] arr = Enumerable.Range(1, n).ToArray();
		var result = new List<int[]>();
		Permute(arr, 0, result);
		return result;
	}

	static void Permute(int[] arr, int l, List<int[]> result)
	{
		if (l == arr.Length)
		{
			result.Add((int[])arr.Clone());
		}
		else
		{
			for (int i = l; i < arr.Length; i++)
			{
				Swap(arr, i, l);
				Permute(arr, l + 1, result);
				Swap(arr, i, l);
			}
		}
	}

	static void Swap(int[] arr, int i, int j)
	{
		int t = arr[i]; arr[i] = arr[j]; arr[j] = t;
	}

	static void PrintSolution()
	{
		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < N; j++)
				Console.Write(solution[i, j] + " ");
			Console.WriteLine();
		}
	}
}

public class Skyscrapers7x7
{
	const int N = 7;
	static int?[] top, bottom, left, right;
	static int[,] solution = new int[N, N];
	private static int counter = 0;

	static List<int[]> GetAllPermutations(int n)
	{
		int[] arr = Enumerable.Range(1, n).ToArray();
		var result = new List<int[]>();
		Permute(arr, 0, result);
		return result;
	}

	static void Permute(int[] arr, int l, List<int[]> result)
	{
		if (l == arr.Length)
		{
			result.Add((int[])arr.Clone());
		}
		else
		{
			for (int i = l; i < arr.Length; i++)
			{
				Swap(arr, i, l);
				Permute(arr, l + 1, result);
				Swap(arr, i, l);
			}
		}
	}

	static void Swap(int[] arr, int i, int j)
	{
		int t = arr[i]; arr[i] = arr[j]; arr[j] = t;
	}

	static int CountVisible(int[] row, bool fromLeft)
	{
		int max = 0, count = 0;
		if (fromLeft)
		{
			foreach (var x in row)
				if (x > max) { max = x; count++; }
		}
		else
		{
			for (int i = row.Length - 1; i >= 0; i--)
				if (row[i] > max) { max = row[i]; count++; }
		}
		return count;
	}

	static bool Solve(int rowIndex, List<int[]>[] validRows)
	{
		if (rowIndex == N) return CheckColumns();

		foreach (var row in validRows[rowIndex])
		{
			if (CanPlaceRow(rowIndex, row))
			{
				for (int col = 0; col < N; col++)
					solution[rowIndex, col] = row[col];

				if (Solve(rowIndex + 1, validRows))
				{
					Console.WriteLine($"{counter++} TRUE");
					return true;
				}
			}
		}
		Console.WriteLine($"{counter++} FALSE");
		return false;
	}

	static bool CanPlaceRow(int rowIndex, int[] row)
	{
		for (int col = 0; col < N; col++)
		{
			for (int prevRow = 0; prevRow < rowIndex; prevRow++)
				if (solution[prevRow, col] == row[col])
					return false;
		}
		return true;
	}

	static bool CheckColumns()
	{
		for (int col = 0; col < N; col++)
		{
			int[] colVals = new int[N];
			for (int row = 0; row < N; row++)
				colVals[row] = solution[row, col];

			if (top[col].HasValue && CountVisible(colVals, true) != top[col].Value)
				return false;
			if (bottom[col].HasValue && CountVisible(colVals, false) != bottom[col].Value)
				return false;
		}
		return true;
	}

	static void PrintSolution()
	{
		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < N; j++)
				Console.Write(solution[i, j] + " ");
			Console.WriteLine();
		}
	}

	public static int[][] SolvePuzzle(int[] clues)
	{
		// Start your coding here...
		return null;
	}

	public static void TestAll()
	{
		// Example clues
		top = new int?[] { 3, null, 2, null, 4, null, 1 };
		bottom = new int?[] { null, 2, null, 3, null, 1, null };
		left = new int?[] { 2, null, null, 3, null, 2, null };
		right = new int?[] { null, 3, null, 1, null, null, 4 };

		// Precompute all permutations of [1..7]
		var allRows = GetAllPermutations(N);

		// Filter by left/right clues
		var validRows = new List<int[]>[N];
		for (int i = 0; i < N; i++)
		{
			validRows[i] = allRows.Where(r =>
				(!left[i].HasValue || CountVisible(r, true) == left[i].Value) &&
				(!right[i].HasValue || CountVisible(r, false) == right[i].Value)
			).ToList();
		}

		if (Solve(0, validRows))
			PrintSolution();
		else
			Console.WriteLine("No solution found.");
	}

	static int[][] clues = new[]
	{
		new [] { 7, 0, 0, 0, 2, 2, 3,
			0, 0, 3, 0, 0, 0, 0,
			3, 0, 3, 0, 0, 5, 0,
			0, 0, 0, 0, 5, 0, 4 },
		new [] { 0, 2, 3, 0, 2, 0, 0,
			5, 0, 4, 5, 0, 4, 0,
			0, 4, 2, 0, 0, 0, 6,
			5, 2, 2, 2, 2, 4, 1 }
	};

	static int[][][] expected = new[]
	{
		new[] { new[] { 1, 5, 6, 7, 4, 3, 2 },
			new[] { 2, 7, 4, 5, 3, 1, 6 },
			new[] { 3, 4, 5, 6, 7, 2, 1 },
			new[] { 4, 6, 3, 1, 2, 7, 5 },
			new[] { 5, 3, 1, 2, 6, 4, 7 },
			new[] { 6, 2, 7, 3, 1, 5, 4 },
			new[] { 7, 1, 2, 4, 5, 6, 3 } },
		new[] { new[] { 7, 6, 2, 1, 5, 4, 3 },
			new[] { 1, 3, 5, 4, 2, 7, 6 },
			new[] { 6, 5, 4, 7, 3, 2, 1 },
			new[] { 5, 1, 7, 6, 4, 3, 2 },
			new[] { 4, 2, 1, 3, 7, 6, 5 },
			new[] { 3, 7, 6, 2, 1, 5, 4 },
			new[] { 2, 4, 3, 5, 6, 1, 7 } }
	};

	//public void Test_1_Medium()
	//{
	//	var actual = SolvePuzzle(clues[0]);
	//	Assert.That(actual, Is.EqualTo(expected[0]));
	//}

	//public void Test_2_VeryHard()
	//{
	//	var actual = SolvePuzzle(clues[1]);
	//	Assert.That(actual, Is.EqualTo(expected[1]));
	//}
}