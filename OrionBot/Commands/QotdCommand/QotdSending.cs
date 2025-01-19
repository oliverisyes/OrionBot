using Discord;
using Discord.WebSocket;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot.Commands.QotdCommand
{
	public class QotdSending : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			var _client = (DiscordSocketClient)context
				.JobDetail
				.JobDataMap
				.Get("_client");

			for (int i = 0; i < Servers.GetLatestServerID(); i++)
			{
				Console.WriteLine("Latest serverID: " + Servers.GetLatestServerID());
				Console.WriteLine("Current serverID: " + i);

				ulong channelID = Servers.GetQotdChannel(i);

				if (channelID != 0)
				{
					Console.WriteLine("ChannelID: " + channelID);

					var channel = _client.GetChannel(channelID) as IMessageChannel;
					Console.WriteLine(channel);
					if (channel != null)
					{
						string message = Qotd.GetQuestion(Servers.GetQotdID(i));
						ulong roleID = Servers.GetQotdRole(i);
						if (roleID != 0)
						{
							message = $"<@&{roleID}> " + message;
						}

						await channel.SendMessageAsync("QOTD TIME!\n" + message);
						Servers.IncrementQotdID(i);
					}
					else
					{
						Console.WriteLine("Channel not found");
					}
				}
			}
		}
	}
}
