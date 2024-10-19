using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ECommerce.ConsoleCommands;
using ECommerce.Services;


namespace ECommerce
{

	class Program
	{
		static void Main(string[] args)
		{
			string inputFile = args[0];
			string outputFile = args[1];
			
			if (!File.Exists(inputFile))
			{
				Console.WriteLine($"Input file '{inputFile}' does not exist.");
				return;
			}

			string[] commands = File.ReadAllLines(inputFile);
			List<string> outputResults = new List<string>();


			var serviceProvider = ConfigureServices();
			var cart = serviceProvider.GetService<Cart>() ;
			

            foreach (var item in commands)
            {
				var commandObject = JsonConvert.DeserializeObject<CommandWrapper>(item);
				if (commandObject == null)
				{
					
					Console.WriteLine($"Failed to deserialize command: {item}");
					continue; 
				}
				var command = GetCommand(commandObject);
				var result = command.Execute(cart, commandObject.Payload);
				outputResults.Add(JsonConvert.SerializeObject(result));

			}

			File.WriteAllLines(outputFile, outputResults);

		}

		public static ServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();
			
			services.AddSingleton<IPromotionManager, PromotionManager>();
			services.AddSingleton<Cart>();
			return services.BuildServiceProvider();
		}

		private static ICommand GetCommand(CommandWrapper commandObject)
		{
			return commandObject.Command.ToLower() switch
			{
				"additem" => new AddItemCommand(),
				"addvasitemtoitem" => new AddVasItemToItemCommand(),
				"removeitem" => new RemoveItemCommand(),
				"resetcart" => new ResetCartCommand(),
				"displaycart" => new DisplayCartCommand(),
				_ => throw new InvalidOperationException("Unknown command"),
			};
		}

		public class CommandWrapper
		{
			public string Command { get; set; }
			public Dictionary<string, object> Payload { get; set; }
		}

		





	}
}
