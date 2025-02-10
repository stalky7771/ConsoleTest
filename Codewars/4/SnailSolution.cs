// https://www.codewars.com/kata/521c2db8ddc89b9b7a0000c1

namespace Main.Codewars
{
	public class SnailSolution
	{
		public static int[] Snail(int[][] array)
		{
			int l = array[0].Length;
			int[] sorted = new int[l * l];
			Snail(array, -1, 0, 1, 0, l, 0, sorted);
			return sorted;
		}

		public static void Snail(int[][] array, int x, int y, int dx, int dy, int l, int i, int[] sorted)
		{
			if (l == 0)
				return;
			for (int j = 0; j < l; j++)
			{
				x += dx;
				y += dy;
				sorted[i++] = array[y][x];
			}
			Snail(array, x, y, -dy, dx, dy == 0 ? l - 1 : l, i, sorted);
		}
	}
}
