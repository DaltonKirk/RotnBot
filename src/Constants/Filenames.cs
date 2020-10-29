namespace RotnBot.Constants
{
    public class Filenames
    {
        public const string dataDirectory = "data/";

        public static string Token
        {
            get
            {
                return Program._devMode ? $"{dataDirectory}/dev-token.txt" : $"{dataDirectory}/token.txt";
            }
        }

        public static string SteamIds
        {
            get
            {
                return Program._devMode ? $"{dataDirectory}/dev-steamids.json" : $"{dataDirectory}/steamids.json";
            }
        }
    }
}