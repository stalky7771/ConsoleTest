//https://www.codewars.com/kata/59f98052120be4abfa000304/train/csharp

using System.Numerics;

namespace Main.Codewars._3
{
	public class UpsideDownNumbers
	{
		public static double upsideDown(string x, string y)
		{
			var counter = new UpsideDownNumbersCounter();
			var res = counter.Calculate(x, y);
			return res;
		}

		public static void Test(string x, string y, double expected)
		{
			var res = upsideDown(x, y);
			if (res == expected)
				Console.WriteLine("OK");
			else
				Console.WriteLine($"Error {x}, {y}");
		}

		public static void TestAll()
		{
			Test("0", "9", 3);
			Test("0", "10", 3);
			Test("6", "25", 2);
			Test("10", "100", 4);
			Test("100", "1000", 12);
			Test("100000", "12345678900000000", 718650);
		}
	}

	public class UpsideDownNumbersCounter
	{
		private static int[] arrLong = new[] { 9, 8, 6, 1, 0 };
		private static int[] arrShort = new[] { 8, 1, 0 };

		private BigInteger min;
		private BigInteger max;
		private double count;
		private bool isFinished;

		public double Calculate(string x, string y)
		{
			count = 0;
			min = BigInteger.Parse(x);
			max = BigInteger.Parse(y);
			BigInteger result = 0;

			var maxN = (int)(System.Math.Ceiling(BigInteger.Log10(max)));

			for (var n = maxN; n > 0; n--)
			{
				if (n % 2 == 0)
					CalculateEvenPow(n, n - 1, result);
				else
					CalculateOddPow(n + 1, n, result);
			}
			return count;
		}

		private void CalculateEvenPow(int maxN, int n, BigInteger result) // четный
		{
			for (var i = 0; i < arrLong.Length; i++)
			{
				var number = arrLong[i];
				var left = number * BigInteger.Pow(10, n);
				var right = InvertNumber(number) * BigInteger.Pow(10, maxN - (n + 1));
				var x = result + left + right;

				if (n - 1 >= maxN / 2)
				{
					CalculateEvenPow(maxN, n - 1, x);
				}
				else
				{
					if (x > BigInteger.Pow(10, maxN - 1))
						IncrementCounter(x);
				}

				if (isFinished)
					break;
			}
		}

		private void CalculateOddPow(int maxN, int n, BigInteger result) // нечетный
		{
			if (n == (maxN + 1) / 2)
			{
				for (var i = 0; i < arrShort.Length; i++)
				{
					var x = result + arrShort[i] * BigInteger.Pow(10, n - 1);
					if (x >= BigInteger.Pow(10, maxN - 2) - 1)
						IncrementCounter(x);

					if (isFinished)
						break;
				}
			}
			else
			{
				for (var i = 0; i < arrLong.Length; i++)
				{
					var number = arrLong[i];
					var left = number * BigInteger.Pow(10, n - 1);
					var right = InvertNumber(number) * BigInteger.Pow(10, maxN - n - 1);
					var x = result + left + right;

					if (n - 1 >= maxN / 2)
						CalculateOddPow(maxN, n - 1, x);

					if (isFinished)
						break;
				}
			}
		}

		private void IncrementCounter(BigInteger x)
		{
			if (max >= x && x >= min)
			{
				count++;
				//Console.WriteLine(x);
			}
			else
			{
				if (x < min)
					isFinished = true;
			}
		}

		private static int InvertNumber(int i)
		{
			return i == 0 || i == 1 || i == 8
				? i
				: i == 6
					? 9
					: 6;
		}
	}
}
