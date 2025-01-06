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
		public DbSet<Qotd> Qotd { get; set; }
		public string DbPath { get; set; }

		public OrionContext()
		{
			DbPath = "C:\\Projects\\OrionBotDatabase\\OrionBot.db";
			//DbPath = "/home/oliverhoward/OrionBot/OrionBotDatabase/OrionBot.db";
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		=> options.UseSqlite($"Data Source={DbPath}");
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

		public static bool PlayerExistsName(string Name)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.Name == Name.ToLower())
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

		public static void AddPlayer(ulong id, string Name)
		{
			try
			{
				using var db = new OrionContext();
				db.Players.Add(new Players { UserID = GetUserIDNewest(), DiscordID = id, Name = Name.ToLower() });
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		public static void RemovePlayer()
		{

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
		public static ulong GetDiscordIDName(string Name)
		{
			using var db = new OrionContext();
			var player = db.Players
				.Where(x => x.Name == Name)
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

			return player ?? "Discord ID not there";
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

			return player ?? "Zone Not There";
		}

		public static string GetZoneName(string Name)
		{
			var db = new OrionContext();
			var player = db.Players
				.Where(x => x.Name == Name.ToLower())
				.Select(x => x.TimeZone)
				.FirstOrDefault();

			return player ?? "Zone Not There";
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
	}
}