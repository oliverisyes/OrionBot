using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Calendars;
using NodaTime.Extensions;
using OrionBot.Commands.HungerGamesCommand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrionBot.Commands.TimezoneCommand
{
	public class TimezoneCommand : ModuleBase<SocketCommandContext> 
	{
		[Command("time")]
		[Summary("Displays the time for someone else")]
		public async Task ExecuteAsync([Remainder][Summary("timezone")] string phrase)
		{
			ulong id = Context.User.Id;

			if (phrase.StartsWith("for"))
			{
				string target = GetTarget(phrase);
				if (Players.PlayerExistsName(target))
				{
					string userZone = Players.GetZoneName(target);

					if (userZone == "Zone Not There" || userZone == "0")
					{
						await ReplyAsync("This person doesn't have a timezone saved");
					}
					else
					{
						//Gets the name of the target and their timezone
						DateTimeZone zone = DateTimeZoneProviders.Tzdb[userZone];

						//Convert utc into target's time
						DateTime utcNow = DateTime.UtcNow;
						Instant instant = Instant.FromDateTimeUtc(utcNow);
						ZonedDateTime time = instant.InZone(zone);

						await ReplyAsync("The time for " + target + " is " + FixTime(time));
					}
				}
				else
				{
					await ReplyAsync("This person doesn't exist");
				}
			}
			else if (phrase.StartsWith("add"))
			{
				string target = GetTarget(phrase);
				if (Players.PlayerExistsID(id) && Players.GetZoneID(id) != "0")
				{
					await ReplyAsync("Only one timezone can be added for each user");
				}
				else
				{
					if (!Players.PlayerExistsName(target))
					{
						Players.AddPlayer(id, target);

					}

					string userzone = GetWord(phrase);
					
					try
					{
						DateTimeZone zone = DateTimeZoneProviders.Tzdb[userzone];
						Players.AddZone(id, userzone);
						await ReplyAsync("Your timezone has been added");
					}
					catch
					{
						await ReplyAsync("This city isn't in our database, please try another");
					}
				}
			}
			else if (phrase.StartsWith("remove"))
			{
				Players.AddZone(id, "0");
				await ReplyAsync("Your timezone has been removed");
			}
			else
			{
				await ReplyAsync("This isn't a command");
			}
		}

		//Timezone methods
		public string GetTarget(string phrase)
		{
			string[] splitted = phrase.Split(' ');
			return splitted[1];
		}

		public string FixTime(ZonedDateTime brokeTime)
		{
			string time = brokeTime.ToString();
			string date = time.Substring(0, 10);
			time = time.Substring(11, 5);

			return time + " (" + date + ")";
		}

		public string GetWord(string phrase)
		{
			string[] splited = phrase.Split(' ');

			return splited[splited.Length - 1];
		}
	}
}
