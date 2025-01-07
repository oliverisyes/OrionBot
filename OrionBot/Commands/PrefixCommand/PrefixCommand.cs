using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.PrefixCommand
{
	public class PrefixCommand : ModuleBase<SocketCommandContext>
	{
		[Command("prefix")]
		[Summary("Allows changing the bot's command prefix")]
		public async Task ExecuteAsync([Remainder][Summary("prefix")] string phrase)
		{
			ulong id = Context.Guild.Id;
			phrase.Replace("prefix ", "");

			if (phrase.Length == 1)
			{
				char prefix = phrase[0];

				Servers.ChangePrefix(id, prefix);

				await ReplyAsync("Command prefix changed!");
			}
			else
			{
				await ReplyAsync("Please enter a prefix with just one character");
			}
		}
	}
}
