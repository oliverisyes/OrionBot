using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionBot
{
	internal class EnvReader
	{
		public static void Load(string path)
		{
			if (!File.Exists(path))
				throw new Exception($"The file {path} does not exist");

			foreach(var line in File.ReadAllLines(path))
			{
				if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
					continue; 
			
				var parts = line.Split('=', 2);
				if(parts.Length != 2)
					continue;

				var key = parts[0].Trim();
				var value = parts[1].Trim();
				Environment.SetEnvironmentVariable(key, value);
			}
		}
	}
}
