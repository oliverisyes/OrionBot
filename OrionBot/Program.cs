using System.Reflection;
using System.Runtime.InteropServices;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace OrionBot
{
	class Program
	{
		private static void Main(string[] args) =>
			MainAsync(args).GetAwaiter().GetResult();

		private static async Task MainAsync(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.AddUserSecrets(Assembly.GetExecutingAssembly())
				.Build();

			var serviceProvider = new ServiceCollection()
				.AddLogging(options =>
				{
					options.ClearProviders();
					options.AddConsole();
				})
				.AddSingleton<IConfiguration>(configuration)
				.AddScoped<IBot, Bot>()
				.BuildServiceProvider();

			await using var db = new OrionContext();
			Console.WriteLine($"Database path: {db.DbPath}");
			Console.WriteLine("Checking database connects: ");
			var playercheck = db.Players.OrderBy(b => b.userID).First();
			Console.WriteLine(playercheck);

			try
			{
				IBot bot = serviceProvider.GetRequiredService<IBot>();

				await bot.StartAsync(serviceProvider);

				do
				{
					var keyinfo = Console.ReadKey();

					if (keyinfo.Key == ConsoleKey.Q)
					{
						await bot.StopAsync();
						return;
					}
				} while (true);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Environment.Exit(-1);
			}
		}
	}
}