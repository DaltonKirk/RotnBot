using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Discord.WebSocket;
using RotnBot.Helpers;

namespace RotnBot.Services
{
    public class ServerStatusService : IServerStatusService
    {
        public ServerStatusService(DiscordSocketClient client, ulong serverId, ulong channelId)
        {
            _client = client;
            _serverId = serverId;
            _channelId = channelId;
        }

        private DiscordSocketClient _client;

        private ulong _serverId;

        private ulong _channelId;

        private Timer _timer;

        private bool _serverIsUp;

        private ISocketMessageChannel _channel;

        public Task Start()
        {
            SocketGuild server = DiscordHelpers.GetServer(_serverId, _client);
            if (server != null)
            {
                _channel = DiscordHelpers.GetTextChannel(server, _channelId);
                if (_channel != null)
                {
                    _timer = new System.Threading.Timer(Check, null, 0, 1000 * 10);
                }
            }
            return Task.CompletedTask;
        }

        private void Check(object state)
        {
            if (ServerUp("localhost", 25565))
            {
                if (!_serverIsUp)
                {
                    _serverIsUp = true;
                    _channel.SendMessageAsync("Minecraft server is up!");
                    _timer = new Timer(Check, null, 0, 1000 * 10);
                }
            }
            else
            {
                if (_serverIsUp)
                {
                    _serverIsUp = false;
                    _channel.SendMessageAsync("Mincraft server has gone down :(");
                    _timer = new System.Threading.Timer(Check, null, 0, 1000 * 10);
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