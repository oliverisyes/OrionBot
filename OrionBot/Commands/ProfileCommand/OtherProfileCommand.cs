using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.ProfileCommand
{
	public class OtherProfileCommand : ModuleBase<SocketCommandContext>
	{
		[Command("profile")]
		[Summary("Displays another user's profile")]
		public async Task ExecuteAsync([Remainder][Summary("profile")] string phrase)
		{
			string name = GetWord(phrase);
			ulong id = Players.GetDiscordIDName(name);
			string cap = name[0].ToString();
			string space = "᲼᲼᲼᲼";

			name = name.Substring(1, name.Length - 1);

			var embed = new EmbedBuilder
			{
				Title = cap.ToUpper() + name + "'s Profile",
				Color = Color.DarkPurple
			};
			embed.AddField("Profile",
				//UserID
				"User ID: " + Players.GetUserIDID(id) + space +
				//DiscordID
				"Discord ID: " + id);

			embed.AddField("Hunger Games", 
				//Wins
				"Wins: " + Players.GetWinsID(id) + space +
				//Loses
				"Loses: " + Players.GetLosesID(id) + space +
				//Kills
				"Kills: " + Players.GetKillsID(id));

			embed.AddField("Timezone",
				//Timezone
				Players.GetZoneID(id));

			await ReplyAsync(embed: embed.Build());
		}

		public string GetWord(string phrase)
		{
			string[] splited = phrase.Split(' ');

			return splited[splited.Length - 1];
		}
	}
}
