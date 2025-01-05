using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.HungerGamesCommand
{
	public class HungerGamesCommand : ModuleBase<SocketCommandContext>
	{
		[Command("game")]
		[Summary("Begins the hunger games")]
		public async Task ExecuteAsync([Remainder][Summary("begin")] string phrase)
		{
			if(phrase == "begin")
			{

			}
			else if(phrase.Contains("newplayer"))
			{
				
				return;
			}
			else if(phrase == "removeplayer")
			{

			}
			else //(string.IsNullOrEmpty(phrase) || phrase != "begin")
			{
				await ReplyAsync(phrase);
				return;
			}
		
			await ReplyAsync(phrase);
		}

		public bool NewPlayer()
		{
			
			return true;
		}
	}
}