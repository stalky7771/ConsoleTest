//https://www.codewars.com/kata/53d40c1e2f13e331fc000c26/train/csharp

using System.Numerics;

namespace Main.Codewars._3
{
	public class TheMillionthFibonacciKata
	{
		private static readonly Mtx2X2 fibMtx = new Mtx2X2 { _11 = 1, _12 = 1, _21 = 1 };
		private static readonly Mtx2X2 identity = new Mtx2X2 { _11 = 1, _22 = 1 };

		public static BigInteger fib(int n)
		{
			if (n == 0)
				return 0;

			var isNegative = n < 0;
			if (isNegative)
				n = -n;

			if (n == 1 || n == 2)
				return isNegative ? -1 : 1;

			var res = IntPower(fibMtx, n - 1)._11;

			var sign = isNegative ? System.Math.Pow(-1, n + 1) : 1;

			return sign > 0 ? res : -res;
		}

		public struct Mtx2X2
		{
			public BigInteger _11, _12, _21, _22;
			public static Mtx2X2 operator *(Mtx2X2 lhs, Mtx2X2 rhs)
			{
				return new Mtx2X2
				{
					_11 = lhs._11 * rhs._11 + lhs._12 * rhs._21,
					_12 = lhs._11 * rhs._12 + lhs._12 * rhs._22,
					_21 = lhs._21 * rhs._11 + lhs._22 * rhs._21,
					_22 = lhs._21 * rhs._12 + lhs._22 * rhs._22
				};
			}
		}

		public static Mtx2X2 IntPower(Mtx2X2 x, int power)
		{
			if (power == 0) return identity;
			if (power == 1) return x;
			int n = 31;
			while ((power <<= 1) >= 0)
				n--;

			var tmp = x;

			while (--n > 0)
				tmp = tmp * tmp * ((power <<= 1) < 0 ? x : identity);

			return tmp;
		}

		private static void Test(BigInteger expected, int input)
		{
			BigInteger res = fib(input);
			Console.WriteLine($"{input} - " + (res == expected ? "OK" : $"ERR {res}"));
		}

		public static void TestAll()
		{
			Test(0, 0);
			Test(1, 1);
			Test(1, 2);
			Test(2, 3);
			Test(3, 4);
			Test(5, 5);
			Test(BigInteger.Parse("-51680708854858323072"), -96);
			Test(BigInteger.Parse("806515533049393"), -73);
		}
	}
}
