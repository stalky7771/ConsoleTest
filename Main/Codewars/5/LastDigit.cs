//https://www.codewars.com/kata/5511b2f550906349a70004e1/train/csharp

using System;
using System.Numerics;

namespace Main.Codewars._5
{
	public class LastDigit
	{
		public static int GetLastDigit(BigInteger n1, BigInteger n2)
		{
			if (n2 > 4)
			{
				n2 %= 4;
				if (n2 == 0)
					n2 = 4;
			}
			return (int)(BigInteger.Pow(n1, (int)n2) % 10);
		}

		public static void TestAll()
		{
			Test(4, 1, 4);
			Test(4, 2, 6);
			Test(9, 7, 9);
			Test(10, BigInteger.Pow(10, 10), 0);
			Test(9, 17, 9);
			Test(9, 31, 9);
			Test(9, 37, 9);
			Test(9, 41, 9);
			Test(9, 43, 9);

			Test(7, 10, 9);
			Test(7, 13, 7);
			Test(7, 17, 7);
			Test(7, 19, 3);

			Test(BigInteger.Pow(2, 200), BigInteger.Pow(2, 300), 6);
			Test(BigInteger.Parse("3715290469715693021198967285016729344580685479654510946723"), BigInteger.Parse("68819615221552997273737174557165657483427362207517952651"), 7);
			Test(BigInteger.Parse("48014552517170042110681969563172306160120430708"), BigInteger.Parse("48014552517170042110681969563172306160120430708"), 6);
			Test(BigInteger.Parse("954174927796609904"), BigInteger.Parse("954174927796609904"), 6);
		}

		public static void Test(BigInteger n1, BigInteger n2, int expected)
		{
			if (GetLastDigit(n1, n2) == expected)
				Console.WriteLine("OK");
			else
				Console.WriteLine("Error");
		}
	}
}
