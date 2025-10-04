namespace Main.Codewars._4
{
	internal class GetNextSmaller
	{
		public static long NextSmaller(long n)
		{
			char[] digits = n.ToString().ToCharArray();

			// Find first pos i where digits[i] > digits[i + 1]
			int i = digits.Length - 2;
			while (i >= 0 && digits[i] <= digits[i + 1])
				i--;

			if (i < 0)
				return -1; // min (e.g., 1234)

			// Find number on the right smaller than digits[i]
			int j = digits.Length - 1;
			while (digits[j] >= digits[i])
				j--;

			// Exchange them
			(digits[i], digits[j]) = (digits[j], digits[i]);

			// Revert tail after pos i (reverse direction)
			Array.Reverse(digits, i + 1, digits.Length - (i + 1));
			
			if (digits[0] == '0')
				return -1;

			return long.Parse(new string(digits));
		}

		public static void Test(long n, long expect)
		{
			if (NextSmaller(n) == expect)
				Console.WriteLine("Pass");
			else
				Console.WriteLine("FAIL");
		}

		public static void TestAll()
		{
			Test(21, 12);
			Test(907, 790);
			Test(531, 513);
			Test(1027, -1);
			Test(1072, 1027);
			Test(441, 414);
			Test(123456798, 123456789);
		}
	}
}
