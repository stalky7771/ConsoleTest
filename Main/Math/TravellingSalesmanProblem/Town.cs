using System.Drawing;

namespace Main.Math.TravellingSalesmanProblem
{
	public class Town
	{
		private const float MAX_X = 10f;
		private const float MAX_Y = 10f;
		public Point Pos { get; set; }
		public int Id { get; set; }

		public Town()
		{

		}

		public Town(int id, Random random)
		{
			Id = id;
			Pos = new Point((int)(random.NextDouble() * MAX_X), (int)(random.NextDouble() * MAX_Y));
		}

		public override string ToString()
		{
			return $"{Id}, X={Pos.X}, Y={Pos.Y}";
		}
	}
}
