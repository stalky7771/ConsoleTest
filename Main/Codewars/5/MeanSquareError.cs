//https://www.codewars.com/kata/51edd51599a189fe7f000015/train/csharp

namespace Main.Codewars._5
{
	public class MeanSquareError
	{
		public static double Solution(int[] firstArray, int[] secondArray)
		{
			double GetSquare(int x, int y)
			{
				return System.Math.Pow(System.Math.Max(x, y) - System.Math.Min(x, y), 2);
			}

			return firstArray.Select((x, index) => GetSquare(x, secondArray[index])).Sum() / firstArray.Length;
		}

		public static void TestAll()
		{
			Test(new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, 9);
			Test(new int[] { 10, 20, 10, 2 }, new int[] { 10, 25, 5, -2 }, 16.5);
			Test(new int[] { 0, -1 }, new int[] { -1, 0 }, 1);
		}

		public static void Test(int[] left, int [] right, double result)
		{
			if (System.Math.Abs(Solution(left, right) - result) < 0.001)
				Console.WriteLine("OK");
			else
				Console.WriteLine("Error");
		}
	}
}
