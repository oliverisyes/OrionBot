using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;

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
			Console.WriteLine("Device");
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

		private async Task HandleCommandAsync(SocketMessage arg)
		{
			//Ignore messages from bots 
			if (arg is not SocketUserMessage message || message.Author.IsBot)
			{
				return;
			}

			//Log the recieved message
			//_logger.LogInformation($"{DateTime.Now.ToShortTimeString()} - {message.Author}: {message.Content}");

			//Check if message is a command
			int position = 0;
			SocketCommandContext command = new SocketCommandContext(_client, message);

			if (message.HasCharPrefix(Servers.GetPrefix(command.Guild.Id), ref position))
			{
				//Execute command if it exists in ServiceCollection
				await _commands.ExecuteAsync(new SocketCommandContext(_client, message), position, _serviceProvider);
			}
			else if (message.ToString() == "star wars")
			{
				position = 0;

				//Execute command if it exists in ServiceCollection
				await _commands.ExecuteAsync(command, position, _serviceProvider);
			}
		}

		public static void SetUpServer(ulong id, string name)
		{
			Servers.AddServer(id, name);
		}
	}
}