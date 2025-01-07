using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.EnableCommand
{
	public class EnableCommand : ModuleBase<SocketCommandContext>
	{
		[Command("enable")]
		[Summary("Enables a command")]
		public async Task ExecuteAsync([Remainder][Summary("enable")] string command)
		{
			ulong serverID = Context.Guild.Id;

			if (command == "time")
			{
				if (Servers.TimeEnabled(serverID))
				{
					await ReplyAsync("This command is already enabled");
				}
				else
				{
					Servers.EnableTime(serverID);

					await ReplyAsync("Timezone command has been enabled");
				}
			}
			else if (command == "qotd")
			{
				if (Servers.QotdEnabled(serverID))
				{
					await ReplyAsync("This command is already enabled");
				}
				else
				{
					Servers.EnableQotd(serverID);

					await ReplyAsync("Qotd command has been enabled");
				}
			}
			else
			{
				await ReplyAsync("This isn't a command");
			}
		}
	}
}
