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
			//var counter = new UpsideDownNumbersCounter();
			////var result = counter.CalcTopUpsideDownNumber("12345678900000000");
			//var result = counter.CalcTopUpsideDownNumber("99999999900000001");
			//return;

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
			Test("100000", "12345678900000000", 718650);
			Test("100000", "11999999866666611", 718650);
			Test("1681816969181891", "8729534742494453", 161461);
			Test("900689689006", "133586245546987379633", 17946806);
			Test("160880916088091", "180910686989016081", 1664955);
			Test("93445505661", "9860889161916880986", 7433198);

			//Stopwatch stopWatch = new Stopwatch();
			//stopWatch.Start();
			//Test("8698", "64365925465732689127799", 119140593);
			////Test("1000", "8698", 13);
			//stopWatch.Stop();
			//Console.WriteLine($"time: {stopWatch.Elapsed.TotalSeconds}");
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

			var minN = x.Length;
			var right = BigInteger.Pow(10, minN - 1) == _min ? minN : minN + 1;

			var maxN = y.Length;
			var left = BigInteger.Pow(10, maxN - 1) == _max ? maxN : maxN - 1;

			right = System.Math.Min(left, right);
			right = System.Math.Max(right, 4);

			if (maxN < 5)
			{
				for (var n = maxN; n > 0; n--)
				{
					if (n % 2 == 0)
						CalculateEvenPow(n, n - 1, result);
					else
						CalculateOddPow(n + 1, n, result);
				}
				return (double)_count;
			}

			for (var n = maxN; n >= right - 1; n--)
			{
				if (left >= n && n > right)
				{
					_count += new BigInteger(Fast(n));
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
			return CalcFast(n) - CalcFast(n - 1);
		}

		public double CalcFast(int n)
		{
			switch (n)
			{
				case 0: return 1;
				case 1: return 3;
				case 2: return 7;
				case 3: return 19;
			}

			double x = 20;
			for (var i = 4; i <= n; i++)
				x = i % 2 == 0 ? 2 * x : 2.5 * x;

			return x - 1;
		}

		private bool IsUpsideDownNumber(int i, bool isMediumColumn = false)
		{
			return isMediumColumn
				? i == 0 || i == 1 || i == 8
				: i == 0 || i == 1 || i == 6 || i == 8 || i == 9;
		}

		private int GetNearMaxUpsideDownNumber(int i, bool isMediumColumn = false)
		{
			if (isMediumColumn)
			{
				if (i == 9)
					return 0;

				if (i == 7 || i == 6 || i == 5 || i == 4 || i == 3 || i == 2)
					return 8;

			}
			if (i == 7)
				return 9;

			if (i == 5 || i == 4 || i == 3 || i == 2)
				return 6;

			return 9;
		}

		private int Base5System(int i)
		{
			switch (i)
			{
				case 9: return 4;
				case 8: return 3;
				case 6: return 2;
				case 1: return 1;
				default: return 0;
			}
		}

		private int Base3System(int i)
		{
			switch (i)
			{
				case 8: return 3;
				case 1: return 1;
				default: return 0;
			}
		}

		private double GetAmount(int[] arr)
		{
			double res = 0;
			
			for (var i = 0; i < arr.Length; i++)
			{
				var x = Base5System(arr[i]);
				
				if(i == 0)
					x--;

				res += x * System.Math.Pow(5, arr.Length - i - 1);
			}
			return res;
		}

		private int GetNearMinUpsideDownNumber(int i, bool isMediumColumn = false)
		{
			if (isMediumColumn)
			{
				if (i == 9)
					return 8;

				if (i == 7 || i == 6 || i == 5 || i == 4 || i == 3 || i == 2)
					return 1;

			}
			if (i == 7)
				return 6;

			if (i == 5 || i == 4 || i == 3 || i == 2)
				return 1;

			return 1;
		}

		public Dictionary<char, char> dictToLowerUDN = new()
		{
			{ '9', '9' }, { '8', '8' }, { '7', '6' }, { '6', '6' }, { '5', '1' },
			{ '4', '1' }, { '3', '1' }, { '2', '1' }, { '1', '1' }, { '0', '0' },
		};

		public Dictionary<char, char> dictMediumNumberToLowerUDN = new()
		{
			{ '9', '8' }, { '8', '8' }, { '7', '1' }, { '6', '1' }, { '5', '1' },
			{ '4', '1' }, { '3', '1' }, { '2', '1' }, { '1', '1' }, { '0', '0' },
		};

		public Dictionary<char, char> dictNumberRotation = new()
		{
			{ '9', '6' }, { '8', '8' }, { '6', '9' }, { '1', '1' }, { '0', '0' }
		};

		public double CalcTopUpsideDownNumber(string str)
		{
			var size = str.Length / 2;

			var mediumNumber = char.MinValue;
			if (str.Length % 2 != 0)
			{
				mediumNumber = dictMediumNumberToLowerUDN[str[size]];
			}

			var leftNumber = str.Take(size).ToArray();
			var leftNumberOut = Enumerable.Repeat('9', leftNumber.Length).ToArray();

			var isNumberChanged = false;
			for (var i = 0; i < leftNumber.Length; i++)
			{
				leftNumberOut[i] = dictToLowerUDN[leftNumber[i]];
				if (leftNumber[i] != leftNumberOut[i])
				{
					if (mediumNumber != char.MinValue)
						mediumNumber = '8';

					isNumberChanged = true;
					break;
				}
			}

			var left = new string(leftNumberOut);

			if (isNumberChanged == false)
			{
				var leftRotatedNumber = "";
				foreach (var c in leftNumberOut)
				{
					leftRotatedNumber += dictNumberRotation[c];
				}
				//Convert.ToInt32(value, 16));

				var rightNumber = new string(str.Reverse().Take(size).Reverse().ToArray());

				if (double.Parse(leftRotatedNumber) > double.Parse(rightNumber))
				{
					int xxx = 0;
				}
				else
				{
					int xxx = 0;
				}
			}

			int a = 0;
			return 0;

			//for (var i = 0; i < inArray.Length; i++)
			//{
			//	var number = inArray[i];
			//	if (IsUpsideDownNumber(number))
			//	{
			//		outArray[i] = inArray[i];
			//	}
			//	else
			//	{
			//		outArray[i] = GetNearMinUpsideDownNumber(number);
			//		mediumNumber = 8;
			//		break;
			//	}
			//}

			//var amount = GetAmount(outArray);
			//amount++;

			//if (mediumNumber != -1)
			//{
			//	amount = (amount - 1) * 3 + Base3System(mediumNumber);
			//}

			//return amount;
		}

		public double CalcMin(string str)
		{
			var size = str.Length / 2;

			var mediumNumber = -1;
			if (str.Length % 2 != 0)
			{
				mediumNumber = (int)char.GetNumericValue(str[size]);
				if (IsUpsideDownNumber(mediumNumber, true) == false)
					mediumNumber = GetNearMaxUpsideDownNumber(mediumNumber, true);
			}

			var inArray = Array.ConvertAll(str.Take(size).ToArray(), c => (int)char.GetNumericValue(c));
			var outArray = Enumerable.Repeat(9, inArray.Length).ToArray();

			for (var i = 0; i < inArray.Length; i++)
			{
				var number = inArray[i];
				if (IsUpsideDownNumber(number))
				{
					outArray[i] = inArray[i];
				}
				else
				{
					outArray[i] = GetNearMaxUpsideDownNumber(number);
					mediumNumber = 0;
					break;
				}
			}

			var amount = GetAmount(outArray);
			amount++;

			if (mediumNumber != -1)
			{
				amount = (amount - 1) * 3 + Base3System(mediumNumber);
			}

			return amount;
		}
	}
}
