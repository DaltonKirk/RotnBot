using System.Linq;
using System.Collections.Generic;
using RotnBot.Models;
using Discord.WebSocket;

namespace RotnBot.Services
{
    public class RotnBotUserService : JsonFileLoader, IRotnBotUserService
    {
        private const string filename = "steamids.json";
        private readonly List<RotnBotUser> RotnBotUserCollection = new List<RotnBotUser>();

        public RotnBotUserService()
        {
            RotnBotUserCollection = LoadJsonFromDisk<RotnBotUser>(filename);
        }

        public RotnBotUser[] GetAll()
        {
            return RotnBotUserCollection.ToArray();
        }

        public RotnBotUser GetUserBySteamId(string steamId32)
        {
            return RotnBotUserCollection.Where(e => e.SteamUserId == steamId32).FirstOrDefault();
        }

        public RotnBotUser GetUser(SocketUser user)
        {
            var rotnBotUser = RotnBotUserCollection.Where(e => e.DiscordUserId == user.ToString()).FirstOrDefault();
            if (rotnBotUser != null)
            {
                return rotnBotUser;
            }
            else
            {
                return AddRotnBotUser(user);
            }
        }

        public RotnBotUser GetUserByDiscordId(string discordUserId)
        {
            return RotnBotUserCollection.Where(e => e.DiscordUserId == discordUserId).FirstOrDefault();
        }

        public void AddOrUpdateUser(RotnBotUser user)
        {
            RotnBotUser existingUser = GetUserByDiscordId(user.DiscordUserId);
            if (existingUser == null)
            {
                AddRotnBotUser(user);
            }
            else
            {
                UpdateRotnBotUser(user);
            }
        }

        private void AddRotnBotUser(RotnBotUser user)
        {
            RotnBotUserCollection.Add(user);
            SaveJsonToDisk(filename, RotnBotUserCollection);
        }

        private RotnBotUser AddRotnBotUser(SocketUser user)
        {
            var rotnBotUser = new RotnBotUser(user);
            AddRotnBotUser(rotnBotUser);
            return rotnBotUser;
        }

        private void UpdateRotnBotUser(RotnBotUser user)
        {
            foreach(var existingUser in RotnBotUserCollection)
            {
                if (existingUser.DiscordUserId == user.DiscordUserId)
                {
                    existingUser.SteamUserId = user.SteamUserId;
                    existingUser.Points = user.Points;
                    existingUser.LastChestOpened = user.LastChestOpened;
                    break;
                }
            }
            SaveJsonToDisk(filename, RotnBotUserCollection);
        }
    }
}