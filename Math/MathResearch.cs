namespace Main.Math
{
	public class MathResearch
	{
		public static void PermutationsExample()
		{
			char[] a = "ABCD".ToCharArray();
			MathResearch.PrintPermutations(a, 0, a.Length - 1);
		}

		public static void PrintPermutations(char[] a, int i, int n)
		{
			int j;

			if (i == n)
			{
				Console.WriteLine(new string(a));
			}
			else
			{
				char temp;
				for (j = i; j <= n; j++)
				{
					// swap(a[i], a[j]);
					temp = a[i];
					a[i] = a[j];
					a[j] = temp;

					PrintPermutations(a, i + 1, n);

					// swap(a[i], a[j]);
					temp = a[i];
					a[i] = a[j];
					a[j] = temp;
				}
			}
		}
	}
}
