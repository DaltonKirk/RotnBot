using System.Threading.Tasks;
using Discord.Commands;

namespace RotnBot.Modules
{
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        [Command("shutdown")]
        [Summary("Shutsdown the bot.")]
        [Remarks("hidden")]
        public Task ShutdownBotAsync()
        {
            if (Context.Message.Author.Id == 231044105638117376)
                System.Environment.Exit(1);
            return Task.CompletedTask;
        }
    }
}