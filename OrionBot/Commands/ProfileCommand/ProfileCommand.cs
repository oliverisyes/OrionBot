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
			ulong userID = Context.User.Id;
			string name = Players.GetNameID(userID);
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
					"User ID: " + Players.GetUserIDID(userID) + space +
					//DiscordID
					"Discord ID: " + userID);

				embed.AddField("Hunger Games",
					//Wins
					"Wins: " + Players.GetWinsID(userID) + space +
					//Loses
					"Loses: " + Players.GetLosesID(userID) + space +
					//Kills
					"Kills: " + Players.GetKillsID(userID));

				embed.AddField("Timezone",
					//Timezone
					Players.GetZoneID(userID));

				await ReplyAsync(embed: embed.Build());
			}
		}

		[Command("profile")]
		[Summary("Displays another user's profile and allows editing of your own profile")]
		public async Task ExecuteAsync([Remainder][Summary("profile")] string phrase)
		{
			string name = phrase;
			ulong userID = Context.User.Id;
			string cap = name[0].ToString();
			string space = "᲼᲼᲼᲼";
			string cutName = name.Substring(1);

			if (phrase.StartsWith("add"))
			{
				Players.AddPlayer(userID, name);
				await ReplyAsync("You have been added to the database");
			}
			else if (phrase.StartsWith("remove"))
			{
				Players.RemovePlayer(userID);
				await ReplyAsync("You have been removed from the database");
			}
			else if (phrase.StartsWith("change"))
			{
				if (Players.PlayerExistsID(userID))
				{
					Players.ChangePlayer(userID, phrase);
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
					userID = Players.GetDiscordIDName(name);

					var embed = new EmbedBuilder
					{
						Title = cap.ToUpper() + cutName + "'s Profile",
						Color = Color.DarkPurple
					};
					embed.AddField("Profile",
						//UserID
						"User ID: " + Players.GetUserIDID(userID) + space +
						//DiscordID
						"Discord ID: " + userID);

					embed.AddField("Hunger Games",
						//Wins
						"Wins: " + Players.GetWinsID(userID) + space +
						//Loses
						"Loses: " + Players.GetLosesID(userID) + space +
						//Kills
						"Kills: " + Players.GetKillsID(userID));

					embed.AddField("Timezone",
						//Timezone
						Players.GetZoneID(userID));

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
