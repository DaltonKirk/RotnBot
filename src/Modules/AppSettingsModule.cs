using System.Threading.Tasks;
using Discord.Commands;
using RotnBot.Services;

namespace RotnBot.Modules
{
    public class AppSettingsModule : ModuleBase<SocketCommandContext>
    {
        private readonly IAppSettingsService _appSettingsService;

        public AppSettingsModule(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        [Command("setserverstatusip")]
        [Summary("Sets the default server IP to check")]
        [Alias("setip")]
        [Remarks("hidden")]
        public async Task SetServerStatusServiceIP(string ip)
        {
            _appSettingsService.SetServerStatusServiceIP(ip);
            await ReplyAsync("New IP set.");
        }

        [Command("setserverstatusport")]
        [Summary("Sets the default server Port to check")]
        [Alias("setport")]
        [Remarks("hidden")]
        public async Task SetServerStatusServicePort(int port)
        {
            _appSettingsService.SetServerStatusServicePort(port);
            await ReplyAsync("New port set.");
        }
    }
}