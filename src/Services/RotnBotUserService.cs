using System.Linq;
using System.Collections.Generic;
using RotnBot.Models;
using Discord.WebSocket;
using RotnBot.Constants;

namespace RotnBot.Services
{
    public class RotnBotUserService : JsonFileLoader, IRotnBotUserService
    {
        private readonly List<RotnBotUser> RotnBotUserCollection = new List<RotnBotUser>();

        public RotnBotUserService()
        {
            RotnBotUserCollection = LoadJsonFromDisk<RotnBotUser>(Filenames.SteamIds);
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
            var rotnBotUser = RotnBotUserCollection.Where(e => e.DiscordUserId == user.Id).FirstOrDefault();
            if (rotnBotUser != null)
            {
                return rotnBotUser;
            }
            else
            {
                return AddRotnBotUser(user);
            }
        }

        public RotnBotUser GetUserByDiscordId(ulong discordUserId)
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
            SaveJsonToDisk(Filenames.SteamIds, RotnBotUserCollection);
        }

        private RotnBotUser AddRotnBotUser(SocketUser user)
        {
            var rotnBotUser = new RotnBotUser(user);
            AddRotnBotUser(rotnBotUser);
            return rotnBotUser;
        }

        private void UpdateRotnBotUser(RotnBotUser user)
        {
            RotnBotUser existingUser = RotnBotUserCollection.Where(e => e.DiscordUserId == user.DiscordUserId).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser = user;
            }
            SaveJsonToDisk(Filenames.SteamIds, RotnBotUserCollection);
        }
    }
}