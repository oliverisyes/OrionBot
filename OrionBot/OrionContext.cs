using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

#nullable enable
namespace OrionBot
{
	public class OrionContext : DbContext
	{
		public DbSet<Servers> Servers { get; set; }
		public DbSet<Players> Players { get; set; }
		public DbSet<Qotd> Qotd { get; set; }
		public string DbPath { get; set; }

		public OrionContext()
		{
			//DbPath = "C:\\Projects\\OrionBotDatabase\\OrionBot.db";
			DbPath = "/home/oliverhoward/OrionBot/OrionBotDatabase/OrionBot.db";
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
	}

	public class Servers
	{
		[Key]
		public int ServerID { get; set; }

		public ulong DiscordID { get; set; }
		public string Name { get; set; }
		public char CommandPrefix { get; set; }
		public int QotdID { get; set; }
		public ulong QotdChannel {  get; set; }
		public int QotdCommand { get; set; }
		public int TimezoneCommand { get; set; }
		public int HungerGamesCommand { get; set; }

		//Server
		public static void AddServer(ulong id, string name)
		{
			using var db = new OrionContext();
			var server = db.Servers
				.Add(new Servers { ServerID = 0, DiscordID = id, Name = name, QotdID = 1 });
			db.SaveChanges();
		}

		//CommandPrefix
		public static char GetPrefix(ulong id)
		{
			using var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.Select(x => x.CommandPrefix)
				.FirstOrDefault();

			return server;
		}

		public static void ChangePrefix(ulong id, char prefix)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.CommandPrefix = prefix;

			db.SaveChanges();
		}

		//Qotd
		public static bool QotdEnabled(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.Select(x => x.QotdCommand)
				.FirstOrDefault();

			if (server == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static void EnableQotd(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.QotdCommand = 1;

			db.SaveChanges();
		}

		public static void DisableQotd(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.QotdCommand = 0;

			db.SaveChanges();
		}

		public static void SetQotdChannel(ulong id, ulong channelID)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.QotdChannel = channelID;

			db.SaveChanges();
		}

		//Timezone
		public static bool TimeEnabled(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.Select(x => x.TimezoneCommand)
				.FirstOrDefault();

			if (server == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static void EnableTime(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.TimezoneCommand = 1;

			db.SaveChanges();
		}

		public static void DisableTime(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.QotdCommand = 0;

			db.SaveChanges();
		}

		//Hunger Games
		public static bool HungerGamesEnabled(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.Select(x => x.HungerGamesCommand)
				.FirstOrDefault();

			if (server == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static void EnableHungerGames(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.HungerGamesCommand = 1;

			db.SaveChanges();
		}

		public static void DisableHungerGames(ulong id)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			server.HungerGamesCommand = 0;

			db.SaveChanges();
		}
	}

	public class Players
	{
		[Key]
		public int UserID { get; set; }

		public ulong DiscordID { get; set; }
		public string? Name { get; set; }
		public int Wins { get; set; }
		public int Loses { get; set; }
		public int Kills { get; set; }
		public string? TimeZone { get; set; }

		//Player
		public static bool PlayerExistsID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			if (player == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public static bool PlayerExistsName(string name)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.Name == name.ToLower())
				.FirstOrDefault();
			if (player == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public static void AddPlayer(ulong id, string name)
		{
			try
			{
				Console.WriteLine(GetUserIDNewest() + 1);
				using var db = new OrionContext();
				db.Players.Add(new Players { UserID = GetUserIDNewest() + 1, DiscordID = id, Name = name.ToLower() });
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public static void ChangePlayer(ulong id, string name)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			
			player.Name = name;
			db.SaveChanges();
		}

		public static void RemovePlayer(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.FirstOrDefault();
			db.Players.Remove(player);
			db.SaveChanges();
		}

		//UserID
		public static int GetUserIDID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.Select(x => x.UserID)
				.FirstOrDefault();
				
			return player;
		}

		public static int GetUserIDNewest()
		{
			using var db = new OrionContext();
			var player = db.Players
				.Select(x => x.UserID)
				.OrderDescending()
				.LastOrDefault();

			return player;
		}

		//DiscordID
		public static ulong GetDiscordIDName(string name)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.Name == name)
				.Select(x => x.DiscordID)
				.FirstOrDefault();

			return player;
		}

		//Name
		public static string GetNameID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.Select(x => x.Name)
				.FirstOrDefault();

			return player ?? "0";
		}

		//Wins
		public static int GetWinsID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.Select(x => x.Wins)
				.FirstOrDefault();

			return player;
		}

		//Loses
		public static int GetLosesID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.Select(x => x.Loses)
				.FirstOrDefault();

			return player;
		}

		//Kills
		public static int GetKillsID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.Select(x => x.Kills)
				.FirstOrDefault();

			return player;
		}

		//TimeZone
		public static string GetZoneID(ulong id)
		{
			var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == id)
				.Select(x => x.TimeZone)
				.FirstOrDefault();

			return player ?? "N/A";
		}

		public static string GetZoneName(string name)
		{
			var db = new OrionContext();
			var player = db.Players
				.Where(x => x.Name == name.ToLower())
				.Select(x => x.TimeZone)
				.FirstOrDefault();

			return player ?? "N/A";
		}

		public static void AddZone(ulong id, string zone)
		{
			try
			{
				using var db = new OrionContext();
				var player = db.Players
					.Where(x => x.DiscordID == id)
					.FirstOrDefault();

				player.TimeZone = zone;
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}

	public class Qotd
	{
		[Key]
		public int QotdID { get; set; }

		public string? Question { get; set; }

		//QotdID
		public static int GetQotdIDNewest()
		{
			using var db = new OrionContext();
			var qotd = db.Qotd
				.Select(x => x.QotdID)
				.OrderDescending()
				.LastOrDefault();

			return qotd;
		}

		//Question
		public static void AddQuestion(string question)
		{
			using var db = new OrionContext();
			var qotd = db.Qotd
				.Add(new Qotd { QotdID = GetQotdIDNewest(), Question = question });
			db.SaveChanges();
		}
	}
}