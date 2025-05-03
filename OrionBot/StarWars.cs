using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace OrionBot
{
	internal class StarWars : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			Bot.SetswCooldown();
		}

		public async static void SWActivate(SocketCommandContext command)
		{
			var channel = command.Channel as IMessageChannel;
			await channel.SendMessageAsync($"<@503907258640367616>\nSTAR WARS MENTION\nSTAR WARS MENTION\nSTAR WARS MENTION");
		}
	}
}
