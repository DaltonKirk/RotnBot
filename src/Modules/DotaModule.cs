using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using RotnBot.Models;
using RotnBot.Services;

namespace RotnBot.Modules
{
    public class DotaModule : ModuleBase<SocketCommandContext>
    {
        private readonly IRotnBotUserService _rotnBotUserService;

        public DotaModule(IRotnBotUserService rotnBotUserService)
        {
            _rotnBotUserService = rotnBotUserService;
        }

        [Command("lastmatch")]
        [Summary("RotnBot finds your last Dota match result")]
        public async Task LastMatchAsync()
        {
            var userInfo = Context.Message.Author;
            string discordUserId = $"{userInfo.Username}#{userInfo.Discriminator}";
            string steamId32 = _rotnBotUserService.GetUserByDiscordId(discordUserId).SteamUserId;

            try
            {
                string responseBody = await Program.httpClient.GetStringAsync($"https://api.opendota.com/api/players/{steamId32}/matches?limit=1");

                EmbedBuilder embedBuilder = new EmbedBuilder();

                DotaMatch matchData = JsonConvert.DeserializeObject<DotaMatch[]>(responseBody).FirstOrDefault();

                if (matchData != null)
                {
                    bool won = (matchData.radiant_win && matchData.player_slot <= 4) || (!matchData.radiant_win && matchData.player_slot > 4);
                    embedBuilder.Title = "Last match " + UnixTimeStampToDateTime(matchData.start_time).ToString("dd/MM/yyyy HH:mm");
                    embedBuilder.AddField("Result", won ? "Won" : "Lost");
                    embedBuilder.AddField("K/D/A", $"{matchData.kills}/{matchData.deaths}/{matchData.assists}");
                    embedBuilder.AddField("Played", GetHeroes().Where(hero => hero.SteamId == matchData.hero_id).FirstOrDefault().Name);

                    await ReplyAsync("", false, embedBuilder.Build());
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private Hero[] GetHeroes()
        {
            return new Hero[]
            {
                new Hero
                {
                    Name = "Anti-Mage",
                    SteamId = 1
                },
                new Hero
                {
                    Name = "Axe",
                    SteamId = 2
                },
                new Hero
                {
                    Name = "Bane",
                    SteamId = 3
                },
                new Hero
                {
                    Name = "Bloodseeker",
                    SteamId = 4
                },
                new Hero
                {
                    Name = "Crystal Maiden",
                    SteamId = 5
                },
                new Hero
                {
                    Name = "Drow Ranger",
                    SteamId = 6
                },
                new Hero
                {
                    Name = "Earthshaker",
                    SteamId = 7
                },
                new Hero
                {
                    Name = "Juggernaut",
                    SteamId = 8
                },
                new Hero
                {
                    Name = "Mirana",
                    SteamId = 9
                },
                new Hero
                {
                    Name = "Shadow Fiend",
                    SteamId = 11
                },
                new Hero
                {
                    Name = "Morphling",
                    SteamId = 10
                },
                new Hero
                {
                    Name = "Phantom Lancer",
                    SteamId = 12
                },
                new Hero
                {
                    Name = "Puck",
                    SteamId = 13
                },
                new Hero
                {
                    Name = "Pudge",
                    SteamId = 14
                },
                new Hero
                {
                    Name = "Razor",
                    SteamId = 15
                },
                new Hero
                {
                    Name = "Sand King",
                    SteamId = 16
                },
                new Hero
                {
                    Name = "Storm Spirit",
                    SteamId = 17
                },
                new Hero
                {
                    Name = "Sven",
                    SteamId = 18
                },
                new Hero
                {
                    Name = "Tiny",
                    SteamId = 19
                },
                new Hero
                {
                    Name = "Vengeful Spirit",
                    SteamId = 20
                },
                new Hero
                {
                    Name = "Windranger",
                    SteamId = 21
                },
                new Hero
                {
                    Name = "Zeus",
                    SteamId = 22
                },
                new Hero
                {
                    Name = "Kunkka",
                    SteamId = 23
                },
                new Hero
                {
                    Name = "Lina",
                    SteamId = 25
                },
                new Hero
                {
                    Name = "Lich",
                    SteamId = 31
                },
                new Hero
                {
                    Name = "Lion",
                    SteamId = 26
                },
                new Hero
                {
                    Name = "Shadow Shaman",
                    SteamId = 27
                },
                new Hero
                {
                    Name = "Slardar",
                    SteamId = 28
                },
                new Hero
                {
                    Name = "Tidehunter",
                    SteamId = 29
                },
                new Hero
                {
                    Name = "Witch Doctor",
                    SteamId = 30
                },
                new Hero
                {
                    Name = "Riki",
                    SteamId = 32
                },
                new Hero
                {
                    Name = "Enigma",
                    SteamId = 33
                },
                new Hero
                {
                    Name = "Tinker",
                    SteamId = 34
                },
                new Hero
                {
                    Name = "Sniper",
                    SteamId = 35
                },
                new Hero
                {
                    Name = "Necrophos",
                    SteamId = 36
                },
                new Hero
                {
                    Name = "Warlock",
                    SteamId = 37
                },
                new Hero
                {
                    Name = "Beastmaster",
                    SteamId = 38
                },
                new Hero
                {
                    Name = "Queen of Pain",
                    SteamId = 39
                },
                new Hero
                {
                    Name = "Venomancer",
                    SteamId = 40
                },
                new Hero
                {
                    Name = "Faceless Void",
                    SteamId = 41
                },
                new Hero
                {
                    Name = "Wraith King",
                    SteamId = 42
                },
                new Hero
                {
                    Name = "Death Prophet",
                    SteamId = 43
                },
                new Hero
                {
                    Name = "Phantom Assassin",
                    SteamId = 44
                },
                new Hero
                {
                    Name = "Pugna",
                    SteamId = 45
                },
                new Hero
                {
                    Name = "Templar Assassin",
                    SteamId = 46
                },
                new Hero
                {
                    Name = "Viper",
                    SteamId = 47
                },
                new Hero
                {
                    Name = "Luna",
                    SteamId = 48
                },
                new Hero
                {
                    Name = "Dragon Knight",
                    SteamId = 49
                },
                new Hero
                {
                    Name = "Dazzle",
                    SteamId = 50
                },
                new Hero
                {
                    Name = "Clockwerk",
                    SteamId = 51
                },
                new Hero
                {
                    Name = "Leshrac",
                    SteamId = 52
                },
                new Hero
                {
                    Name = "Nature's Prophet",
                    SteamId = 53
                },
                new Hero
                {
                    Name = "Lifestealer",
                    SteamId = 54
                },
                new Hero
                {
                    Name = "Dark Seer",
                    SteamId = 55
                },
                new Hero
                {
                    Name = "Clinkz",
                    SteamId = 56
                },
                new Hero
                {
                    Name = "Omniknight",
                    SteamId = 57
                },
                new Hero
                {
                    Name = "Enchantress",
                    SteamId = 58
                },
                new Hero
                {
                    Name = "Huskar",
                    SteamId = 59
                },
                new Hero
                {
                    Name = "Night Stalker",
                    SteamId = 60
                },
                new Hero
                {
                    Name = "Broodmother",
                    SteamId = 61
                },
                new Hero
                {
                    Name = "Bounty Hunter",
                    SteamId = 62
                },
                new Hero
                {
                    Name = "Weaver",
                    SteamId = 63
                },
                new Hero
                {
                    Name = "Jakiro",
                    SteamId = 64
                },
                new Hero
                {
                    Name = "Batrider",
                    SteamId = 65
                },
                new Hero
                {
                    Name = "Chen",
                    SteamId = 66
                },
                new Hero
                {
                    Name = "Spectre",
                    SteamId = 67
                },
                new Hero
                {
                    Name = "Doom",
                    SteamId = 69
                },
                new Hero
                {
                    Name = "Ancient Apparition",
                    SteamId = 68
                },
                new Hero
                {
                    Name = "Ursa",
                    SteamId = 70
                },
                new Hero
                {
                    Name = "Spirit Breaker",
                    SteamId = 71
                },
                new Hero
                {
                    Name = "Gyrocopter",
                    SteamId = 72
                },
                new Hero
                {
                    Name = "Alchemist",
                    SteamId = 73
                },
                new Hero
                {
                    Name = "Invoker",
                    SteamId = 74
                },
                new Hero
                {
                    Name = "Silencer",
                    SteamId = 75
                },
                new Hero
                {
                    Name = "Outworld Devourer",
                    SteamId = 76
                },
                new Hero
                {
                    Name = "Lycan",
                    SteamId = 77
                },
                new Hero
                {
                    Name = "Brewmaster",
                    SteamId = 78
                },
                new Hero
                {
                    Name = "Shadow Demon",
                    SteamId = 79
                },
                new Hero
                {
                    Name = "Lone Druid",
                    SteamId = 80
                },
                new Hero
                {
                    Name = "Chaos Knight",
                    SteamId = 81
                },
                new Hero
                {
                    Name = "Meepo",
                    SteamId = 82
                },
                new Hero
                {
                    Name = "Treant Protector",
                    SteamId = 83
                },
                new Hero
                {
                    Name = "Ogre Magi",
                    SteamId = 84
                },
                new Hero
                {
                    Name = "Undying",
                    SteamId = 85
                },
                new Hero
                {
                    Name = "Rubick",
                    SteamId = 86
                },
                new Hero
                {
                    Name = "Disruptor",
                    SteamId = 87
                },
                new Hero
                {
                    Name = "Nyx Assassin",
                    SteamId = 88
                },
                new Hero
                {
                    Name = "Naga Siren",
                    SteamId = 89
                },
                new Hero
                {
                    Name = "Keeper of the Light",
                    SteamId = 90
                },
                new Hero
                {
                    Name = "Io",
                    SteamId = 91
                },
                new Hero
                {
                    Name = "Visage",
                    SteamId = 92
                },
                new Hero
                {
                    Name = "Slark",
                    SteamId = 93
                },
                new Hero
                {
                    Name = "Medusa",
                    SteamId = 94
                },
                new Hero
                {
                    Name = "Troll Warlord",
                    SteamId = 95
                },
                new Hero
                {
                    Name = "Centaur Warrunner",
                    SteamId = 96
                },
                new Hero
                {
                    Name = "Magnus",
                    SteamId = 97
                },
                new Hero
                {
                    Name = "Timbersaw",
                    SteamId = 98
                },
                new Hero
                {
                    Name = "Bristleback",
                    SteamId = 99
                },
                new Hero
                {
                    Name = "Tusk",
                    SteamId = 100
                },
                new Hero
                {
                    Name = "Skywrath Mage",
                    SteamId = 101
                },
                new Hero
                {
                    Name = "Abaddon",
                    SteamId = 102
                },
                new Hero
                {
                    Name = "Elder Titan",
                    SteamId = 103
                },
                new Hero
                {
                    Name = "Legion Commander",
                    SteamId = 104
                },
                new Hero
                {
                    Name = "Ember Spirit",
                    SteamId = 106
                },
                new Hero
                {
                    Name = "Earth Spirit",
                    SteamId = 107
                },
                new Hero
                {
                    Name = "Terrorblade",
                    SteamId = 109
                },
                new Hero
                {
                    Name = "Phoenix",
                    SteamId = 110
                },
                new Hero
                {
                    Name = "Oracle",
                    SteamId = 111
                },
                new Hero
                {
                    Name = "Techies",
                    SteamId = 105
                },
                new Hero
                {
                    Name = "Winter Wyvern",
                    SteamId = 112
                },
                new Hero
                {
                    Name = "Arc Warden",
                    SteamId = 113
                },
                new Hero
                {
                    Name = "Underlord",
                    SteamId = 108
                },
                new Hero
                {
                    Name = "Monkey King",
                    SteamId = 114
                },
                new Hero
                {
                    Name = "Pangolier",
                    SteamId = 120
                },
                new Hero
                {
                    Name = "Dark Willow",
                    SteamId = 119
                },
                new Hero
                {
                    Name = "Grimstroke",
                    SteamId = 121
                },
                new Hero
                {
                    Name = "Void Spirit",
                    SteamId = 126
                },
                new Hero
                {
                    Name = "Snapfire",
                    SteamId = 128
                },
                new Hero
                {
                    Name = "Mars",
                    SteamId = 129
                }
            };
        }

        private class Hero
        {
            public string Name;
            public int SteamId;
        }
    }
}