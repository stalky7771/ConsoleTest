//https://www.codewars.com/kata/525caa5c1bf619d28c000335/train/csharp

namespace Main.Codewars
{
	public class TicTacToe
	{
		public int IsSolved(int[,] board)
		{
			var b = board;
			for (var i = 0; i < 2; i++)
			{
				if (b[i, 0] != 0 && b[i, 0] == b[i, 1] && b[i, 0] == b[i, 2])
					return b[i, 0];
			}

			for (var i = 0; i < 2; i++)
			{
				if (b[0, i] != 0 && b[0, i] == b[1, i] && b[0, i] == b[2, i])
					return b[0, i];
			}

			if (b[0, 0] != 0 && b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
				return b[0, 0];

			if (b[0, 2] != 0 && b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
				return b[0, 2];



			return 0;
		}

		private static void Test(int[,] board, int expected)
		{
			var ttt = new TicTacToe();
			var res = ttt.IsSolved(board) == expected;
			Console.WriteLine(res ? "OK" : "Error");
		}

		public static void TestAll()
		{
			Test(new int[,] { { 1, 1, 1 }, { 0, 2, 2 }, { 0, 0, 0 } }, 1);
		}
	}
}
