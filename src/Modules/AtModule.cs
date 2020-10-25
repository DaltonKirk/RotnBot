using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using RotnBot.Services;

namespace RotnBot.Modules
{
    public class AtModule : ModuleBase<SocketCommandContext>
    {
        private readonly IRotnBotUserService _rotnBotUserService;

        public AtModule(IRotnBotUserService rotnBotUserService)
        {
            _rotnBotUserService = rotnBotUserService;
        }

        [Command("subdota")]
        [Summary
        ("Subcribe to Dota invites.")]
        public async Task SubDotaAsync()
        {
            UpdateDotaSub(true, Context.Message.Author);
            await ReplyAsync($"{Context.Message.Author} subscribed to Dota invites");
        }

        [Command("unsubdota")]
        [Summary
        ("Unsubscibe from Dota invites.")]
        public async Task UnSubDotaAsync()
        {
            UpdateDotaSub(false, Context.Message.Author);
            await ReplyAsync($"{Context.Message.Author} unsubscribed to Dota invites");
        }


        [Command("dota")]
        [Summary
        ("Asks poeple to play Dota.")]
        [Alias("dota?")]
        public async Task InviteDotaAsync()
        {
            var users = _rotnBotUserService.GetAll();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var user in users)
            {
                if (user.DotaNotificationsOn)
                {
                    stringBuilder.Append(MentionUtils.MentionUser(user.DiscordUserId) + " ");
                }
            }
            stringBuilder.Append("DOTA?");
            await ReplyAsync(stringBuilder.ToString());
        }

        private void UpdateDotaSub(bool onOff, SocketUser user)
        {
            var rotnBotUser = _rotnBotUserService.GetUser(user);
            rotnBotUser.DotaNotificationsOn = onOff;
            _rotnBotUserService.AddOrUpdateUser(rotnBotUser);
        }
    }
}