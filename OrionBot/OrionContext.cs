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
		public DbSet<Players> Players { get; set; }
		public string DbPath { get; set; }

		public OrionContext()
		{
			DbPath = "C:\\Projects\\OrionBotDatabaseTest\\OrionBot.db";
			//DbPath = "/home/oliverhoward/OrionBot/OrionBotDatabase/OrionBot.db";
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
	}

	public class Players
	{
		[Key]
		public int userID { get; set; }

		public ulong discordID { get; set; }
		public string? name { get; set; }
		public int wins { get; set; }
		public int loses { get; set; }
		public int kills { get; set; }
		public string? timeZone { get; set; }

		//Player
		public static bool PlayerExistsID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.discordID == id)
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
				.Where(x => x.name == name.ToLower())
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
				using var db = new OrionContext();
				db.Players.Add(new Players { userID = GetUserIDNewest(), discordID = id, name = name.ToLower() });
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public static void RemovePlayer(ulong id)
		{

		}

		//UserID
		public static int GetUserIDID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.discordID == id)
				.Select(x => x.userID)
				.FirstOrDefault();
				
			return player;
		}

		public static int GetUserIDNewest()
		{
			using var db = new OrionContext();
			var player = db.Players
				.Select(x => x.userID)
				.OrderDescending()
				.LastOrDefault();

			return player;
		}

		//DiscordID
		public static ulong GetDiscordIDName(string name)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.name == name)
				.Select(x => x.discordID)
				.FirstOrDefault();

			return player;
		}

		//Name
		public static string GetNameID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.discordID == id)
				.Select(x => x.name)
				.FirstOrDefault();

			return player ?? "Discord ID not there";
		}

		//Wins
		public static int GetWinsID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.discordID == id)
				.Select(x => x.wins)
				.FirstOrDefault();

			return player;
		}

		//Loses
		public static int GetLosesID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.discordID == id)
				.Select(x => x.loses)
				.FirstOrDefault();

			return player;
		}

		//Kills
		public static int GetKillsID(ulong id)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.discordID == id)
				.Select(x => x.kills)
				.FirstOrDefault();

			return player;
		}

		//Timezone
		public static string GetZoneID(ulong id)
		{
			var db = new OrionContext();
			var player = db.Players
				.Where(x => x.discordID == id)
				.Select(x => x.timeZone)
				.FirstOrDefault();

			return player ?? "Zone Not There";
		}

		public static string GetZoneName(string name)
		{
			var db = new OrionContext();
			var player = db.Players
				.Where(x => x.name == name.ToLower())
				.Select(x => x.timeZone)
				.FirstOrDefault();

			return player ?? "Zone Not There";
		}

		public static void AddZone(ulong id, string zone)
		{
			try
			{
				using var db = new OrionContext();
				var player = db.Players
					.Where(x => x.discordID == id)
					.FirstOrDefault();

				player.timeZone = zone;
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}