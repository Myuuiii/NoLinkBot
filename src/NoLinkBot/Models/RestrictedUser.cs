using System;
using Discord.WebSocket;

namespace NoLinkBot.Models
{
	public class RestrictedUser
	{
		public RestrictedUser() { }
		public RestrictedUser(SocketUser user)
		{
			this.Id = user.Id;
			this.Username = user.Username + "#" + user.Discriminator;
			this.RestrictedAt = DateTime.Now;
		}

		public ulong Id { get; set; }
		public string Username { get; set; }
		public DateTime RestrictedAt { get; set; }
	}
}