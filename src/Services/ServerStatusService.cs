using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using RotnBot.Helpers;

namespace RotnBot.Services
{
    public class ServerStatusService : IServerStatusService
    {
        public ServerStatusService(DiscordSocketClient client, ulong serverId, ulong channelId, IAppSettingsService appSettingsService)
        {
            _client = client;
            _serverId = serverId;
            _channelId = channelId;
            _appSettingsService = appSettingsService;
        }

        private IAppSettingsService _appSettingsService;

        private DiscordSocketClient _client;

        private ulong _serverId;

        private ulong _channelId;

        private Timer _timer;

        private bool _serverIsUp;

        private ISocketMessageChannel _channel;

        const int PingDelaySeconds = 240;

        public Task Start()
        {
            SocketGuild server = DiscordHelpers.GetServer(_serverId, _client);
            if (server != null)
            {
                _channel = DiscordHelpers.GetTextChannel(server, _channelId);
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