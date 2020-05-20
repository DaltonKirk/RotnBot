using System;
using Discord.WebSocket;

namespace RotnBot.Models
{
    public class RotnBotUser
    {
        public RotnBotUser() {}
        public RotnBotUser(SocketUser user, string steamId32 = "")
        {
            if (user != null)
            {
                DiscordUserId = user.ToString();
            }
        }

        public string DiscordUserId { get; set; }
        public string SteamUserId { get; set; }
        public int Points { get; set; }
        public DateTime LastChestOpened { get; set; }
    }
}