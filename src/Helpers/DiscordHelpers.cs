using Discord.WebSocket;

namespace RotnBot.Helpers
{
    public class DiscordHelpers
    {
        /// <summary>
        /// Gets a Discord server that exists in the client with the given ID.
        /// </summary>
        /// <param name="id">The ID of the server</param>
        /// <param name="client">The Discord client</param>
        /// <returns></returns>
        public static SocketGuild GetServer(ulong id, DiscordSocketClient client)
        {
            foreach (SocketGuild server in client.Guilds)
            {
                if (server.Id == id)
                    return server;
            }
            return null;
        }

        /// <summary>
        /// Gets a Discord text channel that belongs to the given server and ID.
        /// </summary>
        /// <param name="server">The server the text channel belongs to.</param>
        /// <param name="id">The ID of the text channel</param>
        /// <returns></returns>
        public static ISocketMessageChannel GetTextChannel(SocketGuild server, ulong id)
        {
            foreach (ISocketMessageChannel channel in server.TextChannels)
            {
                if (channel.Id == id)
                    return channel;
            }
            return null;
        }
    }
}