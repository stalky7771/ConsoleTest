//https://www.codewars.com/kata/59f98052120be4abfa000304/train/csharp

using System.Diagnostics;
using System.Numerics;

namespace Main.Codewars._3
{
	public class UpsideDownNumbers
	{
		public static double UpsideDown(string x, string y)
		{
			var counter = new UpsideDownNumbersCounter();
			var res = counter.Calculate(x, y);
			//Console.WriteLine(res);
			return res;
		}

		public static void Test(string x, string y, double expected)
		{
			var res = UpsideDown(x, y);
			if (res == expected)
				Console.WriteLine("OK");
			else
				Console.WriteLine($"Error {x}, {y} - {res}");
		}

		public static void TestAll()
		{
			//var max = "10";
			//var min = "1";
			//for (var i = 3; i < 12; i++)
			//{
			//	max += "0";
			//	//min += "0";
			//	var counter = new UpsideDownNumbersCounter();
			//	var res = counter.Calculate(min, max);
			//	Console.WriteLine($"{max} - {min} - {res}");
			//	//Console.WriteLine($"{i} - {counter.Fast(i)}");
			//}

			//return;

			//var counter = new UpsideDownNumbersCounter();
			//var res = counter.Calculate("0", "1000000000000000000000000");
			//Console.WriteLine($"{res}");

			//return;

			//Test("0", "9", 3);
			//Test("0", "10", 3);
			//Test("6", "25", 2);
			//Test("10", "100", 4);
			//Test("100", "1000", 12);
			//Test("100000", "12345678900000000", 718650);
			//Test("1681816969181891", "8729534742494453", 161461);
			//Test("10000000000", "10000000000000000000000", 78120000);
			//Test("900689689006", "133586245546987379633", 17946806);
			//Test("160880916088091", "180910686989016081", 1664955);
			//Test("93445505661", "9860889161916880986", 7433198);

			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			Test("8698", "64365925465732689127799", 119140593);
			stopWatch.Stop();
			Console.WriteLine($"time: {stopWatch.Elapsed.TotalSeconds}");
		}
	}

	public class UpsideDownNumbersCounter
	{
		private static readonly int[] arrLong = { 9, 8, 6, 1, 0 };
		private static readonly int[] arrShort = { 8, 1, 0 };

		private BigInteger _min;
		private BigInteger _max;
		private BigInteger _count;
		private bool _isFinished;

		private readonly Dictionary<int, double> _cache = new();
		private readonly Dictionary<double, BigInteger> _pows = new();

		BigInteger GetFastPow(int n)
		{
			if (_pows.ContainsKey(n) == false)
			{
				_pows.Add(n, BigInteger.Pow(10, n));
			}
			return _pows[n];
		}

		public double Calculate(string x, string y)
		{
			_count = 0;
			_min = BigInteger.Parse(x);
			_max = BigInteger.Parse(y);
			BigInteger result = 0;

			var maxN = (int)(System.Math.Ceiling(BigInteger.Log10(_max)));
			var left = BigInteger.Pow(10, maxN) == _max ? maxN : maxN - 1;

			var minN = _min > 10 ? (int)(System.Math.Floor(BigInteger.Log10(_min))) : 1;
			var right = BigInteger.Pow(10, minN) == _min ? minN : minN + 1;

			right = System.Math.Max(right, 4);

			for (var n = maxN; n > 0; n--)
			{
				if (left >= n && n > right)
				{
					_count += new BigInteger(Fast(n) - Fast(n - 1));
					continue;
				}

				if (n % 2 == 0)
					CalculateEvenPow(n, n - 1, result);
				else
					CalculateOddPow(n + 1, n, result);
			}

			return (double)_count;
		}

		private void CalculateEvenPow(int maxN, int n, BigInteger result) // четный
		{
			BigInteger pow1 = GetFastPow(n);
			BigInteger pow2 = GetFastPow(maxN - (n + 1));
			BigInteger pow3 = GetFastPow(maxN - 1);

			for (var i = 0; i < arrLong.Length; i++)
			{
				var number = arrLong[i];
				var left = number * pow1;
				var right = InvertNumber(number) * pow2;
				var x = result + left + right;

				if (x > _max)
					continue;

				if (n - 1 >= maxN / 2)
				{
					CalculateEvenPow(maxN, n - 1, x);
				}
				else
				{
					if (x > pow3)
						IncrementCounter(x);
				}

				if (_isFinished)
					break;
			}
		}

		private void CalculateOddPow(int maxN, int n, BigInteger result) // нечетный
		{
			BigInteger pow1 = GetFastPow(n - 1);
			BigInteger pow2 = GetFastPow(maxN - 2) - 1;
			BigInteger pow3 = GetFastPow(maxN - n - 1);

			if (n == (maxN + 1) / 2)
			{
				for (var i = 0; i < arrShort.Length; i++)
				{
					var x = result + arrShort[i] * pow1;
					if (x >= pow2)
						IncrementCounter(x);

					if (_isFinished)
						break;
				}
			}
			else
			{
				for (var i = 0; i < arrLong.Length; i++)
				{
					var number = arrLong[i];
					var left = number * pow1;
					var right = InvertNumber(number) * pow3;
					var x = result + left + right;

					if (x > _max)
						continue;

					if (n - 1 >= maxN / 2)
						CalculateOddPow(maxN, n - 1, x);

					if (_isFinished)
						break;
				}
			}
		}

		private void IncrementCounter(BigInteger x)
		{
			if (_max >= x && x >= _min)
			{
				_count++;
				var a = 0;
				//Console.WriteLine(x);
			}
			else
			{
				if (x <= _min)
					_isFinished = true;
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

		public double Fast(int n)
		{
			if (_cache.ContainsKey(n))
				return _cache[n];

			switch (n)
			{
				case 0: return 1;
				case 1: return 3;
				case 2: return 7;
				case 3: return 19;
			}

			double c = 20;
			for (var i = 4; i <= n; i++)
			{
				c = i % 2 == 0 ? 2 * c : 2.5 * c;
			}

			var result = c - 1;
			_cache.Add(n, result);
			return result;
		}
	}
}
