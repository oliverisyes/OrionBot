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
			ulong id = Context.Guild.Id;

			if (command == "time")
			{
				if (Servers.TimeEnabled(id))
				{
					await ReplyAsync("This command is already enabled");
				}
				else
				{
					Servers.EnableTime(id);

					await ReplyAsync("Timezone command has been enabled");
				}
			}
			else if (command == "qotd")
			{
				if (Servers.QotdEnabled(id))
				{
					await ReplyAsync("This command is already enabled");
				}
				else
				{
					Servers.EnableQotd(id);

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
