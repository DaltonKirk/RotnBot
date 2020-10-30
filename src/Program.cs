using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using RotnBot.Services;
using Discord.Addons.Interactive;
using RotnBot.Constants;

namespace RotnBot
{
    public class Program
    {
        public Program()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
            });

            _commands = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                CaseSensitiveCommands = false,
            });

            _serverStatusService = new ServerStatusService(_client, new AppSettingsService());
            _client.Log += Log;
            _commands.Log += Log;
            _services = ConfigureServices();
        }

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private static DiscordSocketClient _client;

        private readonly CommandService _commands;

        private readonly IServiceProvider _services;

        private readonly IServerStatusService _serverStatusService;

        public static HttpClient httpClient = new HttpClient();

        public static bool _devMode = true;

        private static IServiceProvider ConfigureServices()
        {
            var map = new ServiceCollection()
            .AddSingleton(typeof(IRotnBotUserService), typeof(RotnBotUserService))
            .AddSingleton(typeof(IAppSettingsService), typeof(AppSettingsService))
            .AddSingleton(new InteractiveService(_client));
            return map.BuildServiceProvider();
        }

        private static Task Log(LogMessage message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
            }

            Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
            Console.ResetColor();

            return Task.CompletedTask;
        }

        private async Task MainAsync()
        {
            // Centralize the logic for commands into a separate method.
            await InitCommands();

            //Load token
            var token = File.ReadAllText(Filenames.Token);

            // Login and connect.
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += _serverStatusService.Start;

            // Wait infinitely so your bot actually stays connected.
            await Task.Delay(Timeout.Infinite);
        }

        private async Task InitCommands()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

            if (msg.Author.ToString() == "Taylor#7371")
            {
                Random rand = new Random();
                if (rand.Next(1, 100) <= 10)
                {
                    await msg.Channel.SendMessageAsync("Shut up Taylor");
                }
            }

            int prefixEndIndex = Constants.RotnBot.CommandPrefix.Length - 1;
            if (msg.HasStringPrefix(Constants.RotnBot.CommandPrefix, ref prefixEndIndex))
            {
                var context = new SocketCommandContext(_client, msg);
                var result = await _commands.ExecuteAsync(context, prefixEndIndex, _services);
            }
        }
    }
}