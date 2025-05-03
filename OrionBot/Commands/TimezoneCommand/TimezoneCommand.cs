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
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OrionBot.Commands.TimezoneCommand
{
	public class TimezoneCommand : ModuleBase<SocketCommandContext> 
	{
		[Command("time")]
		[Summary("Displays your time")]
		public async Task ExecuteAsync()
		{
			ulong serverID = Context.Guild.Id;

			if (Servers.TimeEnabled(serverID))
			{
				ulong id = Context.User.Id;

				DateTimeZone zone = DateTimeZoneProviders.Tzdb[Players.GetZoneID(id)];

				//Convert utc into target's time
				Instant instant = Instant.FromDateTimeUtc(DateTime.UtcNow);
				ZonedDateTime time = instant.InZone(zone);

				await ReplyAsync("The time for you is " + FixTime(time));
			}
		}

		[Command("time")]
		[Summary("Displays the time for someone else")]
		public async Task ExecuteAsync([Remainder][Summary("timezone")] string phrase)
		{
			ulong serverID = Context.Guild.Id;

			if (Servers.TimeEnabled(serverID))
			{
				ulong id = Context.User.Id;
				string command = phrase.Substring(0, 3);

				//Add a timezone
				if (command.StartsWith("add") && Servers.TimeEnabled(serverID))
				{
					string zone = phrase.Replace("add ", "");

					try
					{
						DateTimeZone convertedzone = DateTimeZoneProviders.Tzdb[zone];
						if (Players.PlayerExistsID(id))
						{
							Players.AddZone(id, zone);

							await ReplyAsync("Your timezone has been added");
						}
						else
						{
							await ReplyAsync("You are not in the database, please add youself via the profile add command");
						}
					}
					catch
					{
						await ReplyAsync(zone + " isn't in our database, please try another.\n" +
							"Refer to " + Servers.GetPrefix(serverID) + "help time, if needed");
					}
				}
				else if (command.StartsWith("for") && Servers.TimeEnabled(serverID))
				{
					string target = phrase.Replace("for ", "");

					if (Players.PlayerExistsName(target))
					{
						string timezone = Players.GetZoneName(target);
						if (timezone == null || timezone == "N/A")
						{
							await ReplyAsync("This person doesn't have a timezone saved");
						}
						else
						{
							DateTimeZone convertedzone = DateTimeZoneProviders.Tzdb[timezone];
							ZonedDateTime time = Instant.FromDateTimeUtc(DateTime.UtcNow).InZone(convertedzone);

							await ReplyAsync("The time for " + target + " is " + FixTime(time));
						}
					}
					else
					{
						await ReplyAsync("This person isn't in the database");
					}
				}
				else if (phrase.StartsWith("remove") && Servers.TimeEnabled(serverID))
				{
					phrase = phrase.ToLower();
					if (Players.PlayerExistsID(id))
					{
						Players.AddZone(id, "N/A");

						await ReplyAsync("Your timezone has been removed");
					}
					else
					{
						await ReplyAsync("You already aren't in the database");
					}
				}
				else
				{
					await ReplyAsync("This command doesn't exist");
				}
			}
		}

		public static string FixTime(ZonedDateTime brokeTime)
		{
			string time = brokeTime.ToString();
			string date = time[..10];
			time = time.Substring(11, 5);

			return time + " (" + date + ")";
		}
	}
}
