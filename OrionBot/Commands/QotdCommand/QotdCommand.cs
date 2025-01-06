using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.QotdCommand
{
	public class QotdCommand : ModuleBase<SocketCommandContext>
	{
		[Command("qotd")]
		[Summary("Displays the time for someone else")]
		public async Task ExecuteAsync([Remainder][Summary("qotd")] string phrase)
		{
			if (phrase.StartsWith("add"))
			{

			}
			else if (phrase.StartsWith("channel"))
			{

			}

			await ReplyAsync(phrase);
		}

		public static string GetWord(string phrase)
		{
			string[] splited = phrase.Split(' ');

			return splited[^1];
		}
	}
}
