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
		public DbSet<Server_Users> Server_Users { get; set; }
		public DbSet<Players> Players { get; set; }
		public DbSet<Qotd> Qotd { get; set; }
		public string DbPath { get; set; }

		public OrionContext()
		{
			DbPath = "/home/oliverhoward/OrionBot/OrionBotDatabase/OrionBot.db";
			//DbPath = "C:\\Projects\\OrionBotDatabase\\OrionBot.db";
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
		public ulong QotdRole { get; set; }
		public int QotdCommand { get; set; }
		public int TimezoneCommand { get; set; }
		public int HungerGamesCommand { get; set; }

		//Server
		public static bool ServerExists(ulong serverID)
		{
			using var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == serverID)
				.FirstOrDefault();

			if (server == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public static int GetLatestServerID()
		{
			using var db = new OrionContext();
			var server = db.Servers
				.OrderDescending()
				.FirstOrDefault();

			return server.ServerID;
		}

		public static void AddServer(ulong id, string name)
		{
			using var db = new OrionContext();
			var server = db.Servers
				.Add(new Servers
				{
					ServerID = 0,
					DiscordID = id,
					Name = name,
					CommandPrefix = '!',
					QotdID = 1,
					QotdChannel = 0,
					QotdRole = 0,
					QotdCommand = 1,
					TimezoneCommand = 1,
					HungerGamesCommand = 1,
				});
			db.SaveChanges();
		}

		//CommandPrefix
		public static char GetPrefix(ulong discordID)
		{
			using var db = new OrionContext();
			var test = db.Servers;
			var server = db.Servers
				.Where(x => x.DiscordID == discordID)
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

		public static ulong GetQotdChannel(int serverID)
		{
			try
			{
				var db = new OrionContext();
				var channel = db.Servers
					.Where(x => x.ServerID == serverID)
					.Select(x => x.QotdChannel)
					.FirstOrDefault();

				return channel;
			}
			catch
			{
				return 0;
			}
		}

		public static int GetQotdID(int serverID)
		{
			var db = new OrionContext();
			var qotdID = db.Servers
				.Where(x => x.ServerID == serverID)
				.Select(x => x.QotdID)
				.FirstOrDefault();

			return qotdID;
		}

		public static void IncrementQotdID(int serverID)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.ServerID == serverID)
				.FirstOrDefault();
			server.QotdID++;

			db.SaveChanges();
		}

		public static ulong GetQotdRole(int serverID)
		{
			var db = new OrionContext();
			var role = db.Servers
				.Where(x => x.ServerID == serverID)
				.Select(x => x.QotdRole)
				.FirstOrDefault();
			
			return role;
		}

		public static void SetQotdRole(ulong serverID, ulong role)
		{
			var db = new OrionContext();
			var server = db.Servers
				.Where(x => x.DiscordID == serverID)
				.FirstOrDefault();
			server.QotdRole = role;

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

	public class Server_Users
	{
		[Key]
		public int Server_UserID { get; set; }

		public ulong UserID { get; set; }
		public ulong ServerID { get; set; }

		public static void AddServerUser(ulong userID, ulong serverID)
		{
			using var db = new OrionContext();
			var server = db.Server_Users
				.Add(new Server_Users 
				{ 
					Server_UserID = 0, 
					UserID = userID, 
					ServerID = serverID 
				});
			db.SaveChanges();
		}

		public static void RemoveServerUser(ulong userID, ulong serverID)
		{
			using var db = new OrionContext();
			var server = db.Server_Users
				.Where(x => x.UserID == userID && x.ServerID == serverID)
				.FirstOrDefault();
			db.Server_Users.Remove(server);
			db.SaveChanges();
		}

		public static bool ServerUserExists(ulong userID, ulong serverID)
		{
			using var db = new OrionContext();
			var server = db.Server_Users
				.Where(x => x.UserID == userID && x.ServerID == serverID)
				.FirstOrDefault();

			if (server == null)
			{
				return false;
			}
			else
			{
				return true;
			}
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

		public static void AddPlayer(ulong userID, string name, ulong serverID)
		{
			try
			{
				using var db = new OrionContext();
				db.Players.Add(new Players 
				{ 
					UserID = GetUserIDNewest() + 1, 
					DiscordID = userID, 
					Name = name.ToLower() 
				});
				db.SaveChanges();
				Server_Users.AddServerUser(userID, serverID);
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

		public static void RemovePlayer(ulong userID, ulong serverID)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.DiscordID == userID)
				.FirstOrDefault();
			Server_Users.RemoveServerUser(userID, serverID);
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
				.Order()
				.LastOrDefault();

			return player;
		}

		//DiscordID
		public static ulong GetDiscordIDID(int userID)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.UserID == userID)
				.Select (x => x.DiscordID)
				.FirstOrDefault();

			return player;
		}

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

		public static string GetQuestion(int qotdID)
		{
			var db = new OrionContext();
			var qotd = db.Qotd
				.Where(x => x.QotdID == qotdID)
				.Select(x => x.Question)
				.FirstOrDefault();

			return qotd;
		}
	}
}