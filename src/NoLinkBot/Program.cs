using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using NoLinkBot.Handlers;
using NoLinkBot.Models;

namespace NoLinkBot
{
	class Program
	{
		public static DiscordSocketClient _client;
		public static NoLinkBotConfiguration _config;
		public static CommandHandler _commandHandler;

		static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
		static async Task MainAsync()
		{
			_client = new DiscordSocketClient();

			_config = NoLinkBotConfiguration.LoadConfig();
			_config.Save();

			await _client.LoginAsync(TokenType.Bot, _config.Token);
			await _client.StartAsync();
			await _client.SetGameAsync($"all active channels", null, ActivityType.Listening);

			_commandHandler = new CommandHandler(_client, _config);

			_client.MessageReceived += HandleMessage;
			await Task.Delay(-1);

		}

		private static async Task HandleMessage(SocketMessage arg)
		{
			if (_config.RestrictedUsers.Any(u => u.Id == arg.Author.Id))
			{
				if (Regex.IsMatch(arg.Content, "([a-zA-Z0-9]+://)?([a-zA-Z0-9_]+:[a-zA-Z0-9_]+@)?([a-zA-Z0-9.-]+\\.[A-Za-z]{2,4})(:[0-9]+)?(/.*)?"))
				{
					await arg.Author.SendMessageAsync("You are not allowed to send links");
					await arg.DeleteAsync();
				}
				else
				{
					// Message did not contain a link
				}
			}
		}
	}
}
