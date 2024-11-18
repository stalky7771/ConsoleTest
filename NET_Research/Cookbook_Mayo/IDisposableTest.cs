namespace Main.NET_Research.Cookbook_Mayo
{
	public class DeploymentProcess : IDisposable
	{
		bool _isDisposed;
		readonly StreamWriter report = new StreamWriter("DeploymentReport.txt");

		public static void Test()
		{
			using (var deployer = new DeploymentProcess())
			{
				deployer.CheckStatus();
			}
		}
		
		public bool CheckStatus()
		{
			report.WriteLine($"{DateTime.Now} Application Deployed.");
			return true;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					Console.WriteLine("disposal of purely managed resources goes here");
				}
				report?.Close();
				_isDisposed = true;
			}
		}
		
		~DeploymentProcess()
		{
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
