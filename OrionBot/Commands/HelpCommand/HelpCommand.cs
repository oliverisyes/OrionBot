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
			embed.AddField("Timezones:",
				"Add yourself and your timezone to the bot \n```!time add [name] [Continent/Major_City]```\n" +
				"Find out what time it is for someone else \n```!time for [name]```\n" +
				"Remove your timezone from the bot\n```!time remove```\n");

			await ReplyAsync(embed: embed.Build());
		}
	}
}
