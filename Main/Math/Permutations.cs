namespace Main.Math
{
	public class Permutations
	{
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

		public static void PermutationsExample()
		{
			char[] a = "123".ToCharArray();
			PrintPermutations(a, 0, a.Length - 1);
		}
	}
}
