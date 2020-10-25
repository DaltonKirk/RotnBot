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
                DiscordUserId = user.Id;
            }
        }

        public ulong DiscordUserId { get; set; }
        public string SteamUserId { get; set; }
        public int Points { get; set; }
        public DateTime LastChestOpened { get; set; }
        public int ChestsPurchased { get; set; }
        public bool DotaNotificationsOn { get; set; }
    }
}