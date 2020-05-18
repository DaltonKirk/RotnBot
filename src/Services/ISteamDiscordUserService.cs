using RotnBot.Models;

namespace RotnBot.Services
{
    public interface ISteamDiscordUserService
    {
        SteamDiscordUser GetSteamDiscordUserByDiscordId(string discordId);
        SteamDiscordUser GetSteamDiscordUserBySteamId(string steamId32);
        void AddOrUpdateSteamDiscordUser(SteamDiscordUser user);
    }
}