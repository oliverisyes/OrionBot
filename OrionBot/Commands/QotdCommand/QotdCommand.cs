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

			if (phrase.StartsWith("add") && Context.User.Id == 503907258640367616)
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
			else if (phrase.StartsWith("role") && Servers.QotdEnabled(serverID))
			{
				ulong roleID = Convert.ToUInt64(phrase.Replace("role ", ""));
				Servers.SetQotdRole(serverID, roleID);

				await ReplyAsync("Qotd role has been set");
			}
			else
			{
				await ReplyAsync("This is not a command");
			}
		}
	}
}
