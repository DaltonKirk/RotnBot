using Discord.WebSocket;
using RotnBot.Models;

namespace RotnBot.Services
{
    public interface IRotnBotUserService
    {
        RotnBotUser[] GetAll();
        RotnBotUser GetUser(SocketUser user);
        RotnBotUser GetUserByDiscordId(string discordId);
        RotnBotUser GetUserBySteamId(string steamId32);
        void AddOrUpdateUser(RotnBotUser user);
    }
}