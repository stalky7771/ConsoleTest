//https://www.codewars.com/kata/561c20edc71c01139000017c/train/csharp

using System;
using System.Collections;
using System.Collections.Generic;

namespace Main.Codewars._4
{
	public class Tournament
	{
		// round-robin tournament
		// A   B   C
		// D   E   F

		// A   B ->  C
		//	  ^      |
		//	 /		 v
		// D < -E < -F

		public static (int, int)[][] BuildMatchesTable(int numberOfTeams)
		{
			var nOfT = numberOfTeams;
			var h = nOfT / 2; // half of team numbers;

			var teamsL = Enumerable.Range(2, h - 1).ToList();
			teamsL.AddRange(Enumerable.Range(h + 1, h).Reverse());

			var teamsQ = new Queue<int>(teamsL);

			(int, int)[][] res = new (int, int)[nOfT - 1][];

			for (var i = 0; i < nOfT - 1; i++)
			{
				var teamsInDay =  new List<int>(teamsQ);
				teamsInDay.Insert(0, 1);

				res[i] = new (int, int)[h];
				for (var j = 0; j < res[i].Length; j++)
					res[i][j] = (teamsInDay[j], teamsInDay[nOfT - 1 - j]);

				teamsQ.Enqueue(teamsQ.Dequeue());
			}

			for (var i = 0; i < nOfT - 1; i++)
			{
				for (var j = 0; j < res[i].Length; j++)
				{
					Console.Write($"{res[i][j]} ");
				}
				Console.WriteLine("");
			}


			return res;
		}
	}
}
