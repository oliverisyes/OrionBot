using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using OrionBot.Commands.QotdCommand;
using System.Threading.Channels;
using Quartz;
using Quartz.Impl;
using Discord.Rest;

namespace OrionBot
{
	public class Bot : IBot
	{
		private IServiceProvider? _serviceProvider;

		private readonly ILogger<Bot> _logger;
		private readonly IConfiguration _configuration;
		private readonly DiscordSocketClient _client;
		private readonly CommandService _commands;

		public Bot(ILogger<Bot> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;

			DiscordSocketConfig config = new()
			{
				GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
			};

			_client = new DiscordSocketClient(config);
			_commands = new CommandService();
		}

		public async Task StartAsync(IServiceProvider services)
		{
			Console.WriteLine("Bot (0 = Prototype, 1 = Full)");
			int device = Convert.ToInt16(Console.ReadLine());
			string discordToken = "0";

			if (device == 0) //PC
			{
				discordToken = _configuration["discordToken"] ?? throw new Exception("Missing Discord Token");
			}
			else if (device == 1) //Raspberry pi
			{
				//Load variables from .env file
				EnvReader.Load("token.env");

				//Access token
				discordToken = Environment.GetEnvironmentVariable("DISCORD_TOKEN") ?? throw new Exception("Missing Discord Token");
			}

			_logger.LogInformation($"Starting up with token {discordToken}");

			_serviceProvider = services;

			await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), _serviceProvider);

			await _client.LoginAsync(TokenType.Bot, discordToken);
			await _client.StartAsync();
			await _client.SetCustomStatusAsync("!help for help");

			StdSchedulerFactory factory = new StdSchedulerFactory();
			IScheduler scheduler = await factory.GetScheduler();

			await scheduler.Start();

			IJobDetail qotdJob = JobBuilder.Create<QotdSending>()
				.WithIdentity("job1", "group1")
				.SetJobData(new JobDataMap
				{
					["_client"] = _client,
				})
				.Build();

			ITrigger qotdTrigger = TriggerBuilder.Create()
				.WithIdentity("trigger1", "group1")
				.StartNow()
				//.WithCronSchedule("0 * * * * ?")
				.WithCronSchedule("0 0 15 * * ?")
				.ForJob("job1", "group1")
				.Build();

			await scheduler.ScheduleJob(qotdJob, qotdTrigger);

			//await scheduler.Shutdown();

			_client.JoinedGuild += JoinedGuildAsync;
			_client.MessageReceived += HandleCommandAsync;
		}

		public async Task StopAsync()
		{
			_logger.LogInformation("Shutting Down");

			if (_client != null)
			{
				await _client.LogoutAsync();
				await _client.StopAsync();
			}
		}

		private async Task JoinedGuildAsync(SocketGuild arg)
		{
			ulong guildID = arg.Id;
			string guildName = arg.Name;

			SetUpServer(guildID, guildName);
		}

		static bool swCooldown = false;

		private async Task HandleCommandAsync(SocketMessage arg)
		{
			//Ignore messages from bots 
			if (arg is not SocketUserMessage message || message.Author.IsBot)
			{
				return;
			}

			//Log the recieved message
			//_logger.LogInformation($"{DateTime.Now.ToShortTimeString()} - {message.Author}: {message.Content}");

			int position = 0;
			SocketCommandContext command = new SocketCommandContext(_client, message);
			ulong userID = command.User.Id;
			ulong serverID = command.Guild.Id;
			string serverName = command.Guild.Name;

			if (!Servers.ServerExists(serverID))
			{
				Servers.AddServer(serverID, serverName);
			}
			if (!Server_Users.ServerUserExists(userID, serverID) && Players.PlayerExistsID(userID))
			{
				Server_Users.AddServerUser(userID, serverID);
			}

			if (message.HasCharPrefix(Servers.GetPrefix(command.Guild.Id), ref position))
			{
				//Execute command if it exists in ServiceCollection
				await _commands.ExecuteAsync(command, position, _serviceProvider);
			}
			else if (message.ToString().ToLower().Contains("star wars") && userID != 503907258640367616 && Server_Users.ServerUserExists(503907258640367616, command.Guild.Id) == true)
			{
				if (swCooldown == false)
				{
					StarWars.SWActivate(command);

					StdSchedulerFactory factory = new StdSchedulerFactory();
					IScheduler scheduler = await factory.GetScheduler();

					IJobDetail swJob = JobBuilder.Create<StarWars>()
						.WithIdentity("job2", "group1")
						.Build();

					ITrigger swTrigger = TriggerBuilder.Create()
						.WithIdentity("trigger2", "group2")
						.WithSimpleSchedule(x => x
						.WithIntervalInMinutes(10)
						.WithRepeatCount(1))
						.Build();

					await scheduler.ScheduleJob(swJob, swTrigger);
				}
			}
		}

		public static void SetUpServer(ulong id, string name)
		{
			Servers.AddServer(id, name);
		}

		public bool GetswCooldown()
		{
			return swCooldown;
		}

		public static void SetswCooldown()
		{
			swCooldown = !swCooldown;
		}
	}
}