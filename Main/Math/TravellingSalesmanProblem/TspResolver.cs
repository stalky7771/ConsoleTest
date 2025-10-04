namespace Main.Math.TravellingSalesmanProblem
{
    public class TspResolver
    {
	    public void Execute()
	    {
		    var map = new Map();
			map.Init(50, "Map2", 77);
			map.Process();
			//map.Save();
	    }
    }
}
