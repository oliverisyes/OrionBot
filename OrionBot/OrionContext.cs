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
			DbPath = "C:\\Projects\\OrionBotDatabase\\OrionBot.db";
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

		public static int GetIDNewest()
		{
			using var db = new OrionContext();
			var player = db.Players
				.Select(x => x.userID)
				.LastOrDefault();

			return player;
		}

		public static string GetZone(string name)
		{
			var db = new OrionContext();
			var player = db.Players
				.Where(x => x.name == name.ToLower())
				.Select(x => x.timeZone)
				.FirstOrDefault();

			return player ?? "Zone Not There";
		}

		public static void AddPlayer(ulong id, string name)
		{
			try
			{
				using var db = new OrionContext();
				db.Players.Add(new Players { userID = GetIDNewest(), discordID = id, name = name.ToLower() });
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