using Discord.Commands;
using Discord.WebSocket;
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
			ulong serverID = Context.Guild.Id;
			phrase.ToLower();

			if (phrase.StartsWith("add"))
			{
				string question = phrase.Replace("add ", "");
				Qotd.AddQuestion(question);

				await ReplyAsync("Added: " + question);
			}
			else if (phrase.StartsWith("channel") && Servers.QotdEnabled(serverID))
			{
				ulong channel = Context.Message.Channel.Id;

				Servers.SetQotdChannel(serverID, channel);

				await ReplyAsync("Qotd channel has been set");
			}
		}
	}
}
