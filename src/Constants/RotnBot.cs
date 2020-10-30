namespace RotnBot.Constants
{
    public class RotnBot
    {
        public static string CommandPrefix
        {
            get => Program._devMode ? "/dev" : "/";
        }
    }
}