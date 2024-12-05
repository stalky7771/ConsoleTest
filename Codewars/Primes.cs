//https://www.codewars.com/kata/5519a584a73e70fa570005f5/train/csharp

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Main.Codewars
{
	public class Primes
	{
		//const long MAX = 1_000_000_000_000;
		const long MAX = 1000000;

		private static readonly List<int> _result = new List<int>();

		public static IEnumerable<int> Stream()
		{
			if (_result.Count == 0)
			{
				Init();
			}
			return _result;
		}

		private static void Init()
		{
			var sieve = new OptimizedSegmentedWheel235(MAX);

			Action<long> action = (p) => { _result.Add((int)p); };
			sieve.ListPrimes(action);
		}

		public static void TestAll()
		{
			var globalTimer = Stopwatch.StartNew();

			Test(0, 10, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29);
			Test(10, 10, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71);
			Test(100, 10, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601);
			Test(1000, 10, 7927, 7933, 7937, 7949, 7951, 7963, 7993, 8009, 8011, 8017);

			Console.WriteLine($"Time: {globalTimer.Elapsed}");
			Console.ReadLine();
		}

		private static void Test(int skip, int limit, params int[] expect)
		{
			var found = Stream().Skip(skip).Take(limit).ToArray();
			var res = found.SequenceEqual(expect);
			if (res)
				Console.WriteLine("OK");
			else
				Console.WriteLine("ERROR");
		}
	}

	public interface ISieve
	{
		void ListPrimes(Action<long> callback);
	}

	public class SieveOfEratosthenes : ISieve
	{
		private BitArray Data;
		public int Length => Data.Length;

		public SieveOfEratosthenes(int length)
		{
			Data = new BitArray(length);
			Data.SetAll(true);

			for (int p = 2; p * p < length; p++)
			{
				if (Data[p])
				{
					for (int i = p * p; i < Length; i += p)
					{
						Data[i] = false;
					}
				}
			}
		}

		public void ListPrimes(Action<long> callback)
		{
			for (int i = 2; i < Length; i++)
			{
				if (Data[i]) callback.Invoke(i);
			}
		}
	}

	public class OptimizedSegmentedWheel235 : ISieve
	{
		const int BUFFER_LENGTH = 200 * 1024;
		const int WHEEL = 30;
		const int WHEEL_PRIMES_COUNT = 3;

		private static long[] WheelRemainders = { 1, 7, 11, 13, 17, 19, 23, 29 };
		private static long[] SkipPrimes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
		private static byte[] Masks = { 1, 2, 4, 8, 16, 32, 64, 128 };
		private static int[][] OffsetsPerByte;

		private long Length;
		private long[] FirstPrimes;
		private long[][] PrimeMultiples;

		public bool IsFinished { get; set; }

		static OptimizedSegmentedWheel235()
		{
			OffsetsPerByte = new int[256][];
			List<int> offsets = new List<int>();
			for (int b = 0; b < 256; b++)
			{
				offsets.Clear();
				for (int i = 0; i < WheelRemainders.Length; i++)
				{
					if ((b & Masks[i]) != 0) offsets.Add((int)WheelRemainders[i]);
				}
				OffsetsPerByte[b] = offsets.ToArray();
			}
		}

		public OptimizedSegmentedWheel235(long length)
		{
			Length = length;
			int firstChunkLength = (int)System.Math.Sqrt(length) + 1;
			SieveOfEratosthenes sieve = new SieveOfEratosthenes(firstChunkLength);
			List<long> firstPrimes = new List<long>();
			sieve.ListPrimes(firstPrimes.Add);
			FirstPrimes = firstPrimes.Skip(WHEEL_PRIMES_COUNT).ToArray();
			PrimeMultiples = new long[WheelRemainders.Length][];
			for (int i = 0; i < WheelRemainders.Length; i++)
			{
				PrimeMultiples[i] = new long[FirstPrimes.Length];
				for (int j = 0; j < FirstPrimes.Length; j++)
				{
					long prime = FirstPrimes[j];
					long val = prime * prime;
					while (val % WHEEL != WheelRemainders[i]) val += 2 * prime;
					PrimeMultiples[i][j] = (val - WheelRemainders[i]) / WHEEL;
				}
			}
		}

		private void SieveSegment(byte[] segmentData, long segmentStart, long segmentEnd)
		{
			for (int i = 0; i < segmentData.Length; i++) segmentData[i] = 255;
			long segmentLength = segmentEnd - segmentStart;

			for (int i = 0; i < WheelRemainders.Length; i++)
			{
				byte mask = (byte)~Masks[i];
				for (int j = 0; j < PrimeMultiples[i].Length; j++)
				{
					long current = PrimeMultiples[i][j] - segmentStart;
					if (current >= segmentLength) continue;
					long prime = FirstPrimes[j];

					while (current < segmentLength)
					{
						segmentData[current] &= mask;
						current += prime;
					}

					PrimeMultiples[i][j] = segmentStart + current;
				}
			}
		}

		public void ListPrimes(Action<long> callback)
		{
			foreach (long prime in SkipPrimes) if (prime < Length) callback.Invoke(prime);

			long max = (Length + WHEEL - 1) / WHEEL;
			byte[] segmentData = new byte[BUFFER_LENGTH];
			long segmentStart = 1;
			long segmentEnd = System.Math.Min(segmentStart + BUFFER_LENGTH, max);
			while (segmentStart < max)
			{
				if (IsFinished)
					break;

				SieveSegment(segmentData, segmentStart, segmentEnd);
				for (int i = 0; i < segmentData.Length; i++)
				{
					long offset = (segmentStart + i) * WHEEL;
					byte data = segmentData[i];
					int[] offsets = OffsetsPerByte[data];
					for (int j = 0; j < offsets.Length; j++)
					{
						long p = offset + offsets[j];
						if (p >= Length)
							break;

						if (IsFinished)
							break;

						callback.Invoke(p);
					}
				}
				segmentStart = segmentEnd;
				segmentEnd = System.Math.Min(segmentStart + BUFFER_LENGTH, max);
			}
		}
	}
}
