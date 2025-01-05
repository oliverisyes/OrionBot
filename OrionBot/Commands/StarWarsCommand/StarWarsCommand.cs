using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.StarWarsCommand
{
	public class StarWarsCommand : ModuleBase<SocketCommandContext>
	{
		[Command("star wars")]
		[Summary("Replys and pings me whenever star wars is mentioned")]
		public async Task ExecuteAsync([Remainder][Summary("star wars")] string phrase)
		{
			if (string.IsNullOrEmpty(phrase))
			{
				await ReplyAsync($"Usage: !beans beans");
				return;
			}

			await ReplyAsync("STAR WARS MENTION STAR WARS MENTION <@503907258640367616>");
		}
	}
}
