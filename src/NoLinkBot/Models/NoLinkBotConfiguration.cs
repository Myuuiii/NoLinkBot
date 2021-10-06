using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NoLinkBot.Models
{
	public class NoLinkBotConfiguration
	{
		public string Token { get; set; }
		public string Prefix { get; set; }
		public List<RestrictedUser> RestrictedUsers { get; set; } = new List<RestrictedUser> { };

		public static NoLinkBotConfiguration LoadConfig()
		{
			if (File.Exists("./config.json"))
			{
				return JsonConvert.DeserializeObject<NoLinkBotConfiguration>(File.ReadAllText("./config.json"));
			}
			else
			{
				File.WriteAllText("./config.json", JsonConvert.SerializeObject(new NoLinkBotConfiguration(), Formatting.Indented));
				throw new Exception("Configuration file was created as it did not exist");
			}
		}

		public void Save()
		{
			File.WriteAllText("./config.json", JsonConvert.SerializeObject(this, Formatting.Indented));
		}
	}
}