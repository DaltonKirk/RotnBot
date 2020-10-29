using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using RotnBot.Constants;
using RotnBot.Helpers;

namespace RotnBot.Services
{
    public class ServerStatusService : IServerStatusService
    {
        public ServerStatusService(DiscordSocketClient client, IAppSettingsService appSettingsService)
        {
            _client = client;
            _appSettingsService = appSettingsService;
        }

        private IAppSettingsService _appSettingsService;

        private DiscordSocketClient _client;

        private Timer _timer;

        private bool _serverIsUp;

        private ISocketMessageChannel _channel;

        const int PingDelaySeconds = 240;

        public Task Start()
        {
            SocketGuild server = DiscordHelpers.GetServer(DiscordIds.ServerId, _client);
            if (server != null)
            {
                _channel = DiscordHelpers.GetTextChannel(server, DiscordIds.ChannelId);
                if (_channel != null)
                {
                    _timer = new System.Threading.Timer(Check, null, 0, 1000 * PingDelaySeconds);
                }
            }
            return Task.CompletedTask;
        }

        private void Check(object state)
        {
            if (ServerUp(_appSettingsService.GetServerStatusServiceIP(), _appSettingsService.GetServerStatusServicePort()))
            {
                if (!_serverIsUp)
                {
                    _serverIsUp = true;
                    _channel.SendMessageAsync("Minecraft server is up!");
                    _timer.Change(1000 * PingDelaySeconds, 1000 * PingDelaySeconds);
                }
            }
            else
            {
                if (_serverIsUp)
                {
                    _serverIsUp = false;
                    _channel.SendMessageAsync("Minecraft server has gone down :(");
                    _timer.Change(1000 * PingDelaySeconds, 1000 * PingDelaySeconds);
                }
            }
        }

        private bool ServerUp(string address, int port)
        {
            try
            {
                using (var client = new TcpClient(address, port))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}