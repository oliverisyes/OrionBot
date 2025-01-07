using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.ProfileCommand
{
	public class ProfileCommand : ModuleBase<SocketCommandContext>
	{
		[Command("profile")]
		[Summary("Displays the user's profile")]
		public async Task ExecuteAsync()
		{
			ulong id = Context.User.Id;
			string name = Players.GetNameID(id);
			string cap = name[0].ToString();
			string space = "᲼᲼᲼᲼";

			if (name == "0")
			{
				await ReplyAsync("You are not in the database");
			}
			else
			{
				name = name.Substring(1, name.Length - 1);

				var embed = new EmbedBuilder
				{
					Title = cap.ToUpper() + name + "'s Profile",
					Color = Color.Purple
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
		}

		[Command("profile")]
		[Summary("Displays another user's profile and allows editing of your own profile")]
		public async Task ExecuteAsync([Remainder][Summary("profile")] string phrase)
		{
			string name = phrase;
			ulong id = Context.User.Id;
			string cap = name[0].ToString();
			string space = "᲼᲼᲼᲼";
			string cutName = name.Substring(1);

			if (phrase.StartsWith("add"))
			{
				Players.AddPlayer(id, name);
				await ReplyAsync("You have been added to the database");
			}
			else if (phrase.StartsWith("remove"))
			{
				Players.RemovePlayer(id);
				await ReplyAsync("You have been removed from the database");
			}
			else if (phrase.StartsWith("change"))
			{
				if (Players.PlayerExistsID(id))
				{
					Players.ChangePlayer(id, phrase);
					await ReplyAsync("Your name has been changed");
				}
				else
				{
					await ReplyAsync("You don't exist in the database");
				}
			}
			else
			{
				if (Players.PlayerExistsName(name))
				{
					id = Players.GetDiscordIDName(name);

					var embed = new EmbedBuilder
					{
						Title = cap.ToUpper() + cutName + "'s Profile",
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
				else
				{
					await ReplyAsync("This person isn't in the database");
				}
			}
		}
	}
}
