using System;
using Discord.WebSocket;

namespace RotnBot.Models
{
    public class RotnBotUser
    {
        public RotnBotUser() {}

        /// <summary>
        /// Use to create new users only.
        /// </summary>
        /// <param name="user">Discord.Net socket user to create a RotnBotUser from.</param>
        public RotnBotUser(SocketUser user) 
        {
            if (user != null)
            {
                DiscordUserId = user.Id;
                Username = user.ToString();
                Points = 30;
            }
        }

        public ulong DiscordUserId { get; set; }
        public string SteamUserId { get; set; }
        public int Points { get; set; }
        public DateTime LastChestOpened { get; set; }
        public int ChestsPurchased { get; set; }
        public bool DotaNotificationsOn { get; set; }
        public string Username { get; set; }
    }
}