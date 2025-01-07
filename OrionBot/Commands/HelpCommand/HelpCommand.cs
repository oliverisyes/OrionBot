using Discord;
using Discord.Commands;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OrionBot.Commands.HelpCommand
{
	public class HelpCommand : ModuleBase<SocketCommandContext>
	{
		[Command("help")]
		[Summary("Displays all the bot's commands")]
		public async Task ExecuteAsync()
		{
			ulong serverID = Context.Guild.Id;
			char prefix = Servers.GetPrefix(serverID);
			string time = "";
			string qotd = "";

			if (Servers.TimeEnabled(serverID))
			{
				time = ", time";
			}
			if (Servers.QotdEnabled(serverID))
			{
				qotd = ", qotd";
			}

			var embed = new EmbedBuilder
			{
				Title = "OrionBot Commands",
				Description = "Use " + prefix + "help [category] to see information about each command category\n" +
				"Command categories: profile" + time,
				Color = Color.Red
			};

			embed.AddField("Enable or Disable Commands",
				"Enable or disable a command, all categories are disabled by default\n" +
				"```" + prefix + "enable [category]\n" +
				prefix + "disable [category]```");

			embed.AddField("Prefix Command", 
				"Change the command prefix the bot uses (Default is !)\n" +
				"```" + prefix + "prefix [new prefix]```");

			await ReplyAsync(embed: embed.Build());
		}

		[Command("help")]
		[Summary("Display a specific command")]
		public async Task ExecuteAsync([Remainder][Summary("qotd")] string category)
		{
			ulong serverID = Context.Guild.Id;
			char prefix = Servers.GetPrefix(serverID);
			category.ToLower();

			var embed = new EmbedBuilder
			{
				Title = "OrionBot Commands",
				Color = Color.Red
			};

			if (category == "profile")
			{
				embed.AddField("Profile Commands",
				//View own profile
				"View your profile\n" +
				"```" + prefix + "profile```\n" +
				//Add profile
				"Add your profile\n" +
				"```" + prefix + "profile add [name]```\n" +
				//View another profile
				"View another user's profile\n" +
				"```" + prefix + "profile [name]```\n" +
				//Change name
				"Change your name\n" +
				"```" + prefix + "profile change [new name]```\n" +
				//Remove profile
				"Permanently remove your profile\n" +
				"```" + prefix + "profile remove```\n");
			}
			else if (category == "time" && Servers.TimeEnabled(serverID))
			{
				embed.AddField("Timezone Commands:",
				//View your time
				"View your own time\n" +
				"```" + prefix + "time```\n" +
				//Add Timezone
				"Add your timezone to the bot\n" +
				"If unsure check this list of timezones for your Continent/City: https://en.wikipedia.org/wiki/List_of_tz_database_time_zones\n" +
				"```" + prefix + "time add [Continent/Major_City]```\n" +
				//Find timezone
				"Find out what time it is for someone else \n" +
				"```" + prefix + "time for [name]```\n" +
				//Remove timezone
				"Remove your timezone from the bot\n" +
				"```" + prefix + "time remove```\n");
			}
			else if (category == "qotd" && Servers.QotdEnabled(serverID))
			{
				embed.AddField("Qotd Commands:",
					//Set qotd channel
					"Set a channel for qotd questions to be sent\n" +
					"Please use this command in the channel you want the qotd to be sent to\n" +
					"```" + prefix + "qotd channel```");
			}

			await ReplyAsync(embed: embed.Build());
		}
	}
}
