using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot
{
	public class TestClass : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			await Console.Out.WriteLineAsync("aaaaaaaaaa");
		}
	}
}
