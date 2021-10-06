using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NoLinkBot.Models;

namespace NoLinkBot.Modules
{
	public class GeneralModule : ModuleBase
	{
		[Command("disallow")]
		[RequireUserPermission(Discord.GuildPermission.Administrator)]
		public async Task Disallow(SocketUser user)
		{
			if (Program._config.RestrictedUsers.Any(u => u.Id == user.Id))
			{
				await Context.Channel.SendMessageAsync("This has already been disallowed to send links");
			}
			else
			{
				Program._config.RestrictedUsers.Add(new RestrictedUser(user));
				await Context.Channel.SendMessageAsync($"{user.Mention} has been disallowed to send links");
				Program._config.Save();
			}
		}

		[Command("allow")]
		[RequireUserPermission(Discord.GuildPermission.Administrator)]
		public async Task Allow(SocketUser user)
		{
			if (!Program._config.RestrictedUsers.Any(u => u.Id == user.Id))
			{
				await Context.Channel.SendMessageAsync("This user is not disallowed from sending links");
			}
			else
			{
				Program._config.RestrictedUsers.Remove(Program._config.RestrictedUsers.Single(u => u.Id == user.Id));
				await Context.Channel.SendMessageAsync($"{user.Mention} has been allowed to send links");
				Program._config.Save();
			}
		}

		[Command("list")]
		[RequireUserPermission(Discord.GuildPermission.Administrator)]
		public async Task List()
		{
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithTitle("Users prohibited from sending links");

			StringBuilder sb = new StringBuilder();

			foreach (RestrictedUser ru in Program._config.RestrictedUsers)
			{
				sb.AppendLine($"- ({ru.RestrictedAt}) -> [**{ru.Id}**] -> **{ru.Username}**");
			}

			embed.WithDescription(sb.ToString());
			embed.WithColor(new Color(245, 135, 80));
			await Context.Channel.SendMessageAsync(null, false, embed.Build());
		}

		[Command("status")]
		[RequireUserPermission(GuildPermission.Administrator)]
		public async Task Status(SocketUser user)
		{
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithTitle($"Status for {user.Username}");

			if (Program._config.RestrictedUsers.Any(u => u.Id == user.Id))
			{
				embed.WithDescription("This user is **not allowed** to send links");
				embed.AddField("Prohibited to send links since", Program._config.RestrictedUsers.Single(u => u.Id == user.Id).RestrictedAt);
				embed.WithColor(new Color(255, 0, 0));
			}
			else
			{
				embed.WithDescription("This user is **allowed** to send links");
				embed.WithColor(new Color(0, 255, 0));
			}
			await Context.Channel.SendMessageAsync(null, false, embed.Build());
		}

		[Command("help")]
		[RequireUserPermission(GuildPermission.Administrator)]
		public async Task Help()
		{
			EmbedBuilder embed = new EmbedBuilder();
			embed.WithTitle("No Link Bot - Help");

			StringBuilder sb = new StringBuilder();
			embed.AddField($"{Program._config.Prefix}help", "shows this message");
			embed.AddField($"{Program._config.Prefix}disallow <user>", "disallow a user to send links");
			embed.AddField($"{Program._config.Prefix}allow <user>", "allow a user to send links again");
			embed.AddField($"{Program._config.Prefix}status <user>", "get the allowed/disallowed status from the target user");
			embed.AddField($"{Program._config.Prefix}list", "list all the users that are currently not allowed to send links");
			embed.WithColor(new Color(0, 125, 255));

			await Context.Channel.SendMessageAsync(null, false, embed.Build());
		}
	}
}