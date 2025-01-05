using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.BeansCommand
{
	public class BeansCommand : ModuleBase<SocketCommandContext>
	{
		[Command("beans")]
		[Summary("Reply's BEANS to a mention of beans")]
		public async Task ExecuteAsync([Remainder][Summary("beans?")] string phrase)
		{
			if (string.IsNullOrEmpty(phrase) || phrase != "beans")
			{
				await ReplyAsync("Usage: !beans beans");
				return;
			}

			await ReplyAsync("BEANS");
		}
	}
}
