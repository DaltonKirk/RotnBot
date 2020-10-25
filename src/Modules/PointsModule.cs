using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using RotnBot.Models;
using RotnBot.Services;

namespace RotnBot.Modules
{
    public class PointsModule : ModuleBase<SocketCommandContext>
    {
        private readonly IRotnBotUserService _rotnBotUserService;

        int[] PoinstChests = { 1, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 100 };

        public PointsModule(IRotnBotUserService rotnBotUserService)
        {
            _rotnBotUserService = rotnBotUserService;
        }

        [Command("mypoints")]
        [Summary("See how many points you have")]
        [Alias("points")]
        public async Task MyPointsAsync()
        {
            RotnBotUser user = _rotnBotUserService.GetUser(Context.Message.Author);
            await ReplyAsync($"{Context.Message.Author}, you have {user.Points} points");
        }

        [Command("openchest")]
        [Summary("Opens a points chest")]
        [Alias("pointschest")]
        public async Task OpenChestAsync()
        {
            RotnBotUser user = _rotnBotUserService.GetUser(Context.Message.Author);

            if (user.LastChestOpened.Date == DateTime.Now.Date)
            {
                await ReplyAsync($"{Context.Message.Author} you have already opened your free points chest today.");
                return;
            }
            
            
            Random rnd = new Random();
            int newPoints  = PoinstChests[rnd.Next(0, PoinstChests.Length)];

            user.LastChestOpened = DateTime.Now;
            user.Points += newPoints;
            _rotnBotUserService.AddOrUpdateUser(user);

            await ReplyAsync($"{Context.Message.Author}, you have recieved {newPoints} {(newPoints > 1 ? "points" : "point")}\nYou now have {user.Points} {(user.Points > 1 ? "points" : "point")}");
        }

        [Command("buychest")]
        [Summary("Opens a points chest for 30 points")]
        [Alias("pointschest")]
        public async Task BuyChestAsync()
        {
            RotnBotUser user = _rotnBotUserService.GetUser(Context.Message.Author);
            int cost  = 30;
            if (user.Points < cost)
            {
                await ReplyAsync($"{Context.Message.Author}, you need { (cost - user.Points) } more points to afford a chest");
                return;
            }
            
            Random rnd = new Random();
            int newPoints  = PoinstChests[rnd.Next(0, PoinstChests.Length)];

            user.Points -= cost;
            user.Points += newPoints;
            user.ChestsPurchased++;

            _rotnBotUserService.AddOrUpdateUser(user);

            StringBuilder sb = new StringBuilder();


            sb.AppendLine($"{Context.Message.Author} spent {cost} on a points chest");
            if (newPoints == 100)
            {
                sb.AppendLine(":moneybag::moneybag::moneybag: Jackpot! :moneybag::moneybag::moneybag:");
            }
            sb.AppendLine($"The chest contained {newPoints} points");
            sb.AppendLine($"{(newPoints >= cost ? "You gain" : "You lose")} {( newPoints - cost )} points");
            sb.AppendLine($"You have a new total of {user.Points} points");

            await ReplyAsync(sb.ToString());
        }

        [Command("leaderboard")]
        [Summary("Shows the points leaderboard")]
        public async Task ShowLeaderBoardAsync()
        {
            RotnBotUser[] users = _rotnBotUserService.GetAll();

            users = users.OrderByDescending(e => e.Points).ToArray();

            EmbedBuilder embedBuilder = new EmbedBuilder();
            embedBuilder.Title = "Points Leaderboard";

            foreach (var user in users)
            {
                embedBuilder.AddField(Context.Client.GetUser(user.DiscordUserId).ToString(), user.Points);
            }
            
            await ReplyAsync("", false, embedBuilder.Build());
        }

        [Command("chestsbought")]
        [Summary("Shows the number of chests you have bought")]
        public async Task ShowChestsBoughtAsync()
        {
            var user = _rotnBotUserService.GetUser(Context.Message.Author);
            await ReplyAsync($"You have bought {user.ChestsPurchased} chests");
        }
    }
}