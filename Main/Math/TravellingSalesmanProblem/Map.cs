using System.Text.Json;

namespace Main.Math.TravellingSalesmanProblem
{
	public class Map
	{
		private Town[] _towns;
		private double[,] _distance;
		private int[] _townIds;
		private double _passDistance = Double.MaxValue;
		private Random Rnd;
		public string Name { get; private set; }
		public string MapSavePath => Path.Combine(Directory.GetCurrentDirectory(), $"{Name}.json");
		public int RndSeed;

		public List<Town> GetTowns()
		{
			return _towns.ToList();
		}

		public void Init(int townCount, string name, int rndSeed)
		{
			Name = name;
			RndSeed = rndSeed;

			if (File.Exists(MapSavePath))
			{
				Load();
			}

			Rnd = new Random(RndSeed);

			if (_towns == null)
			{
				_towns = new Town[townCount];

				for (var i = 0; i < townCount; i++)
				{
					_towns[i] = new Town(i, Rnd);
				}

				foreach (var town in _towns)
				{
					Console.WriteLine(town.ToString());
				}
			}

			_distance = new Double[townCount, townCount];

			for (var i = 0; i < townCount; i++)
			{
				for (var j = 0; j < townCount; j++)
				{
					var townA = _towns[i];
					var townB = _towns[j];
					var x1 = townA.Pos.X;
					var y1 = townA.Pos.Y;

					var x2 = townB.Pos.X;
					var y2 = townB.Pos.Y;

					_distance[i, j] = System.Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
					//Console.Write($"{townA.Id}, {townB.Id} - {_distance[i, j]:0.0}\t");
				}
				//Console.WriteLine();
			}

			_townIds = new int[townCount];
			for (var i = 0; i < townCount; i++)
			{
				_townIds[i] = _towns[i].Id;
			}
		}

		public void Process()
		{
			_passDistance = GetResultDistance(_townIds);
			Console.WriteLine(_passDistance);

			for (var i = 0; i < 10000000; i++)
			{
				var tmpIdArray = new int[_townIds.Length]; 
				Array.Copy(_townIds, tmpIdArray, _townIds.Length);
				var id1 = Rnd.Next(0, _townIds.Length);
				var id2 = Rnd.Next(0, _townIds.Length);

				(tmpIdArray[id1], tmpIdArray[id2]) = (tmpIdArray[id2], tmpIdArray[id1]);

				var newDistance = GetResultDistance(tmpIdArray);
				//Console.WriteLine($"{i} - {_passDistance:0}, {newDistance:0}");

				if (newDistance < _passDistance)
				{
					_passDistance = newDistance;
					_townIds = tmpIdArray;
					//Console.WriteLine($"{_passDistance:0.00}");
				}

				if (i % 100000 == 0)
					Console.WriteLine($"{i} - {_passDistance:0.00}");
			}
		}

		public double GetResultDistance(int[] idArray)
		{
			if (idArray == null || idArray.Length < 2)
				return 0;
			
			double result = 0;
			for (var i = 0; i < idArray.Length - 1; i++)
			{
				result += _distance[idArray[i], idArray[i + 1]];
			}
			return result;
		}

		public void Save()
		{
			var dto = new MapDTO();
			dto.TownIds = _townIds;
			dto.Towns = _towns;
			dto.RndSeed = RndSeed;

			var jsonStrOut = JsonSerializer.Serialize(dto);
			using (StreamWriter outFile = new StreamWriter(MapSavePath))
			{
				outFile.WriteLine(jsonStrOut);
			}
		}

		public void Load()
		{
			string json = File.ReadAllText(MapSavePath);
			var dto = JsonSerializer.Deserialize<MapDTO>(json);
			_townIds = dto.TownIds;
			_towns = dto.Towns;
			RndSeed = dto.RndSeed;
		}
	}
}
