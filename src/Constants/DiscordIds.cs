namespace RotnBot.Constants
{
    public class DiscordIds
    {
        // Can't have the conditional statement with ulong so using strings and converting in the public getter.
        public static ulong ServerId
        {
            get
            {
                return ulong.Parse(_ServerId);
            }
        }

        private static string _ServerId
        {
            get
            {
                return Program._devMode ? "451086723217096725" : "231399120269475840";
            }
        }

        public static ulong ChannelId
        {
            get
            {
                return ulong.Parse(_ChannelId);
            }
        }

        private static string _ChannelId
        {
            get
            {
                return Program._devMode ? "451086723217096725" : "231399120269475840";
            }
        }
    }
}