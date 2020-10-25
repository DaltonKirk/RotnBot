using System.Threading.Tasks;
using Discord.Commands;
using RotnBot.Models;
using RotnBot.Services;

namespace RotnBot.Modules
{
    public class SteamModule : ModuleBase<SocketCommandContext>
    {
        private readonly IRotnBotUserService _rotnBotUserService;

        public SteamModule(IRotnBotUserService rotnBotUserService)
        {
            _rotnBotUserService = rotnBotUserService;
        }

        [Command("setmysteamid")]
        [Summary("Saves and updates your Steam ID")]
        [Alias("setsteamid")]
        public async Task SetMySteamId([Summary("The user's steam Id")] string steamId)
        {
            if (steamId.Length > 10)
            {
                long steamId64;
                if (long.TryParse(steamId, out steamId64))
                {
                    steamId = Convert64BitSteamID(steamId64);
                }
            }

            string discordUserId = $"{Context.Message.Author.Username}#{Context.Message.Author.Discriminator}";

            _rotnBotUserService.AddOrUpdateUser(new RotnBotUser(Context.Message.Author, steamId));

            await ReplyAsync("Steam ID set. For " + discordUserId);
        }

        [Command("mysteamid")]
        [Summary("Show's your steam ID Rotn Bot has saved.")]
        [Alias("steamid")]
        public async Task MySteamId()
        {
            RotnBotUser user = _rotnBotUserService.GetUserByDiscordId(Context.Message.Author.Id);
            if (user != null)
            {
                await ReplyAsync($"{Context.Message.Author}, your steam ID is {user.SteamUserId}");
            }
            else
            {
                await ReplyAsync($"No Steam ID set for {Context.Message.Author}");
            }
        }

        private string Convert64BitSteamID(long steamId64)
        {
            return (steamId64 - 76561197960265728).ToString();
        }
    }
}