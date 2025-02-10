// https://www.codewars.com/kata/5550d638a99ddb113e0000a2

namespace Main.Codewars
{
	public class JosephusPermutationSolution
	{
		public static List<object> JosephusPermutation(List<object> items, int k)
		{
			List<object> res = new List<object>();
			int pos = 0;
			while (items.Count > 0)
			{
				pos = (pos + k - 1) % items.Count;
				res.Add(items[pos]);
				items.RemoveAt(pos);
			}
			return res;
		}
	}
}
