using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace RotnBot.Modules
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService;

        // Retrieve client and CommandService instance via ctor
        public HelpModule(CommandService commands)
        {
            _commandService = commands;
        }

        [Command("help")]
        [Summary
        ("Returns info about the available commands.")]
        [Alias("cmds", "commands")]
        public Task HelpAsync()
        {
            var commands = _commandService.Commands;
            EmbedBuilder embedBuilder = new EmbedBuilder();
            
            embedBuilder.WithTitle("List of commands");

            foreach (CommandInfo command in commands)
            {
                // Get the command Summary attribute information
                string embedFieldText = command.Summary ?? "No description available\n";
                
                embedBuilder.AddField("/" + command.Name, embedFieldText);
            }

            return ReplyAsync("Here's a list of commands and their description:  \nAll commands need to be prefixed with /", false, embedBuilder.Build());
        }
    }
}