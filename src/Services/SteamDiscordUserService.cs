using System.Linq;
using System.Collections.Generic;
using RotnBot.Models;

namespace RotnBot.Services
{
    public class SteamDiscordUserService : JsonFileLoader, ISteamDiscordUserService
    {
        private const string filename = "steamids.json";
        private readonly List<SteamDiscordUser> SteamDiscordUserCollection = new List<SteamDiscordUser>();

        public SteamDiscordUserService()
        {
            SteamDiscordUserCollection = LoadJsonFromDisk<SteamDiscordUser>(filename);
        }

        public SteamDiscordUser GetSteamDiscordUserBySteamId(string steamId32)
        {
            return SteamDiscordUserCollection.Where(e => e.SteamUserId == steamId32).FirstOrDefault();
        }

        public SteamDiscordUser GetSteamDiscordUserByDiscordId(string discordUserId)
        {
            return SteamDiscordUserCollection.Where(e => e.DiscordUserId == discordUserId).FirstOrDefault();
        }

        public void AddOrUpdateSteamDiscordUser(SteamDiscordUser user)
        {
            SteamDiscordUser existingUser = GetSteamDiscordUserByDiscordId(user.DiscordUserId);
            if (existingUser == null)
            {
                AddSteamDiscordUser(user);
            }
            else
            {
                UpdateSteamDiscordUser(user);
            }
        }

        private void AddSteamDiscordUser(SteamDiscordUser user)
        {
            SteamDiscordUserCollection.Add(user);
            SaveJsonToDisk(filename, SteamDiscordUserCollection);
        }

        private void UpdateSteamDiscordUser(SteamDiscordUser user)
        {
            foreach(var existingUser in SteamDiscordUserCollection)
            {
                if (existingUser.DiscordUserId == user.DiscordUserId)
                {
                    existingUser.SteamUserId = user.SteamUserId;
                    break;
                }
            }
            SaveJsonToDisk(filename, SteamDiscordUserCollection);
        }
    }
}