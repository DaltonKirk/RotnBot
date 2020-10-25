using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using RotnBot.Services;

namespace RotnBot.Modules
{
    public class UserInfoModule : ModuleBase<SocketCommandContext>
    {
        private readonly IRotnBotUserService _rotnBotUserService;

        public UserInfoModule(IRotnBotUserService rotnBotUserService)
        {
            _rotnBotUserService = rotnBotUserService;
        }

        [Command("userinfo")]
        [Summary
        ("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;            
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }

        // [Command("update")]
        // public async Task UpdateAsync()
        // {
        //     var users = _rotnBotUserService.GetAll();
        //     foreach (var user in users)
        //     {
        //         string[] userData = user.DiscordUserId.Split('#');
        //         var DiscordUser = Context.Client.GetUser(userData[0], userData[1]);
        //         if (DiscordUser != null)
        //         {
        //             user.DiscordUserId = DiscordUser.Id.ToString();
        //             _rotnBotUserService.AddOrUpdateUser(user);
        //         }
        //     }
        //     await Task.CompletedTask;
        // }
    }
}