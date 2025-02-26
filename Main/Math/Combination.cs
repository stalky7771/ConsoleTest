namespace Main.Math
{
	public class Combination
	{
		static void CombinationUtil(int[] arr, int[] data, int start, int end, int index, int r)
		{
			// Current combination is 
			// ready to be printed, 
			// print it
			if (index == r)
			{
				//if (data[0] + data[1] + data[2] == 21)
				{
					for (int j = 0; j < r; j++)
					{
						Console.Write(data[j] + " ");
					}
					Console.WriteLine("");
				}
				return;
			}

			// replace index with all
			// possible elements. The 
			// condition "end-i+1 >= 
			// r-index" makes sure that 
			// including one element
			// at index will make a 
			// combination with remaining 
			// elements at remaining positions
			for (int i = start; i <= end && end - i + 1 >= r - index; i++)
			{
				data[index] = arr[i];
				CombinationUtil(arr, data, i + 1,
					end, index + 1, r);
			}
		}

		// The main function that prints
		// all combinations of size r
		// in arr[] of size n. This 
		// function mainly uses combinationUtil()
		static void PrintCombination(int[] arr, int n, int r)
		{
			// A temporary array to store 
			// all combination one by one
			int[] data = new int[r];

			// Print all combination 
			// using temporary array 'data[]'
			CombinationUtil(arr, data, 0, n - 1, 0, r);
		}


		public static void СombinationsExample()
		{
			//int[] arr = { 1, 2, 3, 4, 5 };
			//int[] arr = { 1, 2, 3, 4, 5 };
			//int[] arr = { 0, 1, 6, 8, 9};
			int[] arr = { 0, 1, 6, 8, 9, 0};
			//var a = "01689".ToCharArray();
			//int[] arr = { 3, 4, 5, 5, 6, 6, 7, 7, 7, 8, 8, 9, 9, 10, 11 };
			int r = 2;
			int n = arr.Length;
			PrintCombination(arr, n, r);
		}
	}
}
