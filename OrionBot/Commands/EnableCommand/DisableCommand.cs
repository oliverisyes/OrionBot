using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.EnableCommand
{
	public class DisableCommand : ModuleBase<SocketCommandContext>
	{
		[Command("disable")]
		[Summary("Disables a command")]
		public async Task ExecuteAsync([Remainder][Summary("disable")] string command)
		{
			ulong serverID = Context.Guild.Id;

			if (command == "time")
			{
				if (!Servers.TimeEnabled(serverID))
				{
					await ReplyAsync("This command is already disabled");
				}
				else
				{
					Servers.EnableTime(serverID);

					await ReplyAsync("Timezone command has been disabled");
				}
			}
			else if (command == "qotd")
			{
				if (!Servers.QotdEnabled(serverID))
				{
					await ReplyAsync("This command is already disabled");
				}
				else
				{
					Servers.EnableQotd(serverID);

					await ReplyAsync("Qotd command has been disabled");
				}
			}
			else
			{
				await ReplyAsync("This isn't a command");
			}
		}
	}
}
