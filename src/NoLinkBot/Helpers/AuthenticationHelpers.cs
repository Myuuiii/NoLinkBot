using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using NoLinkBot.Models;

namespace NoLinkBot
{
	public partial class Helpers
	{
		/// <summary>
		/// Check if a user has one of the moderation roles that are saved in the configuration
		/// </summary>
		/// <param name="context">The command context used to do the check and send a "no permission" message if applicable</param>
		/// <returns><see cref="bool"/></returns>
		public static async Task<bool> CheckUserPermission(ICommandContext context)
		{
			foreach (AllowedRole ar in Program._config.AllowedRoles)
				if ((context.User as SocketGuildUser).Roles.Any(r => r.Id == ar.Id))
					return true;
			await context.Channel.SendMessageAsync($"{context.User.Mention} you do not have permission to use this command");
			return false;
		}
	}
}