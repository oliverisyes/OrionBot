using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot
{
	public interface IBot
	{
		Task StartAsync(IServiceProvider services);

		Task StopAsync();
	}
}
