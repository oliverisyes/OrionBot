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
			var embed = new EmbedBuilder
			{
				Title = "OrionBot Commands",
				//Description = "Use !help [command] to see more information about each command\n",
				Color = Color.Red
			};

			embed.AddField("Profile",
				//View own profile
				"View your own profile\n" +
				"```!profile```\n" +
				//View another profile
				"View someone else's profile\n" +
				"```!profile [name]```");

			embed.AddField("Timezones:",
				//Add Timezone
				"Add yourself and your timezone to the bot\n" +
				"For Australia, continent is Australia\n" +
				"For islands, continent is the ocean they're in\n" +
				"If unsure check the list of IANA timezones: https://en.wikipedia.org/wiki/List_of_tz_database_time_zones\n" +
				"```!time add [name] [Continent/Major_City]```\n" +
				//Find timezone
				"Find out what time it is for someone else \n" +
				"```!time for [name]```\n" +
				//Remove timezone
				"Remove your timezone from the bot\n" +
				"```!time remove```");

			await ReplyAsync(embed: embed.Build());
		}
	}
}
