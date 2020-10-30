using Discord.WebSocket;
using RotnBot.Models;

namespace RotnBot.Services
{
    public interface IRotnBotUserService
    {
        /// <summary>
        /// Returns an array of all RotnBotUsers
        /// </summary>
        /// <returns></returns>
        RotnBotUser[] GetAll();

        /// <summary>
        /// Uses the user id to check for existing RotnBotUsers and creates and returns a new user if none are found.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        RotnBotUser GetUser(SocketUser user);
        RotnBotUser GetUserByDiscordId(ulong discordId);
        RotnBotUser GetUserBySteamId(string steamId32);
        void AddOrUpdateUser(RotnBotUser user);
    }
}