using Microsoft.Extensions.DependencyInjection;
using System;

namespace Main.NET_Research.Cookbook_Mayo
{
	public class IoC_Test
	{
		readonly IDeploymentService service;
		
		public IoC_Test(IDeploymentService service)
		{
			this.service = service;
		}

		public static void Func()
		{
			var services = new ServiceCollection();
			services.AddTransient<DeploymentArtifacts>();
			services.AddTransient<DeploymentRepository>();
			services.AddTransient<IDeploymentService, DeploymentService>();

			var serviceProvider = services.BuildServiceProvider();
			IDeploymentService deploymentService = serviceProvider.GetRequiredService<IDeploymentService>();
			IoC_Test program = new IoC_Test(deploymentService);
			program.StartDeployment();
		}

		public void StartDeployment()
		{
			service.PerformValidation();
			Console.WriteLine("Validation complete - continuing...");
		}
	}

	public class DeploymentArtifacts
	{
		public void Validate()
		{
			System.Console.WriteLine("Validating...");
		}
	}

	public class DeploymentRepository
	{
		public void SaveStatus(string status)
		{
			System.Console.WriteLine("Saving status...");
		}
	}

	public interface IDeploymentService
	{
		void PerformValidation();
	}

	public class DeploymentService : IDeploymentService
	{
		readonly DeploymentArtifacts artifacts;
		readonly DeploymentRepository repository;
		
		public DeploymentService(DeploymentArtifacts artifacts, DeploymentRepository repository)
		{
			this.artifacts = artifacts;
			this.repository = repository;
		}
		
		public void PerformValidation()
		{
			artifacts.Validate();
			repository.SaveStatus("status");
		}
	}
}
