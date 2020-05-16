using System.Threading.Tasks;
using Discord.Commands;

namespace RotnBot.Modules
{
    public class SayModule : ModuleBase<SocketCommandContext>
    {
        [Command("type")]
        [Summary("RotnBot types a message.")]
        public Task SayAsync([Remainder][Summary("The text to type")] string echo)
            => ReplyAsync(echo);


        [Command("say")]
        [Summary("RotnBot speaks a message.")]
        public Task Say([Remainder][Summary("The text to speak")] string echo)
            => base.ReplyAsync(echo, true);
    }
}