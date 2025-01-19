using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands
{
	public class FunnyBreak : ModuleBase<SocketCommandContext>
	{
		[Command("break")]
		[Summary("Funny bad")]
		public async Task ExecuteAsync()
		{
			await ReplyAsync("Shut up funny :rolling_eyes:");
		}
	}
}
