using System;

namespace RotnBot.Models
{
    public class RotnBotUser
    {
        public string DiscordUserId { get; set; }
        public string SteamUserId { get; set; }
        public int Points { get; set; }
        public DateTime LastChestOpened { get; set; }
    }
}