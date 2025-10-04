// https://www.codewars.com/kata/52a382ee44408cea2500074c/train/csharp

namespace Main.Codewars._4
{
	public class MatrixDeterminant
	{
		public static int Determinant(int[][] matrix)
		{
			return (int)DeterminantRecursive(matrix);
		}

		public static long DeterminantRecursive(int[][] matrix)
		{
			var n = matrix.GetLength(0);

			if (n == 1)
				return matrix[0][0];

			if (n == 2)
				return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];

			long det = 0;

			for (int p = 0; p < n; p++)
			{
				var sub = new int[n - 1][];

				for (int i = 0; i < sub.Length; i++)
					sub[i] = new int[n - 1];

				for (int i = 1; i < n; i++)
				{
					int colIndex = 0;
					for (int j = 0; j < n; j++)
					{
						if (j == p) continue;
						sub[i - 1][colIndex++] = matrix[i][j];
					}
				}

				det += (long)System.Math.Pow(-1, p) * matrix[0][p] * DeterminantRecursive(sub);
			}

			return det;
		}

		public static void Test()
		{
			Console.WriteLine(Determinant(new int[][] { new[] { 2, 5, 3 }, new[] { 1, -2, -1 }, new[] { 1, 3, 4 } }) == -20 ? "Pass" : "FAIL");
			Console.WriteLine(Determinant(new int[][] { new int[] { 1 } }) == 1 ? "Pass" : "FAIL");
			Console.WriteLine(Determinant(new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 } }) == -2 ? "Pass" : "FAIL");
			Console.WriteLine(Determinant(new int[][] { new int[] { 2, 5, 3 }, new int[] { 1, -2, -1 }, new int[] { 1, 3, 4 } }) == -20 ? "Pass" : "FAIL");
			Console.WriteLine(Determinant(new int[][] { new int[] { 1, 0, 2, -1 }, new int[] { 3, 0, 0, 5 }, new int[] { 2, 1, 4, -3 }, new int[] { 1, 0, 5, 0 } }) == 30 ? "Pass" : "FAIL");
		}
	}
}
