using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using RotnBot.Services;

namespace RotnBot.Modules
{
    public class ServerCheckModule : ModuleBase<SocketCommandContext>
    {
        IAppSettingsService _appSettingsService;

        public ServerCheckModule(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        [Command("checkserver")]
        [Summary("pings the given server")]
        public Task CheckAsync([Remainder][Summary("The server address IPAddress:PortNumber")] string serverAddress = "")
        {
            if (string.IsNullOrEmpty(serverAddress))
            {
                serverAddress = $"{_appSettingsService.GetServerStatusServiceIP()}:{_appSettingsService.GetServerStatusServicePort()}";
            }

            var serverDetails = serverAddress.Split(':');
            string address = serverDetails[0];
            int port = int.Parse(serverDetails[1]);

            if (ServerUp(address, port))
            {
                return ReplyAsync("Server up!");
            }
            else
            {
                return ReplyAsync("Server not found :(");
            }
        }

        private bool ServerUp(string address, int port)
        {
            try
            {
                using (var client = new TcpClient(address, port))
                    return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }
    }
}