using Discord.WebSocket;

namespace NoLinkBot.Models
{
	public class AllowedRole
	{
		public AllowedRole(SocketRole role)
		{
			this.Id = role.Id;
			this.Name = role.Name;
		}
		public ulong Id { get; set; }
		public string Name { get; set; }
	}
}