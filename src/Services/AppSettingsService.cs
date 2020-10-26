namespace RotnBot.Services 
{
    public class AppSettingsService : JsonFileLoader, IAppSettingsService
    {
        public AppSettingsService()
        {
            appSettings = LoadJsonFromDisk<AppSettings>(filename)?[0];
            if (appSettings == null) appSettings = new AppSettings();
        }

        private const string filename = "appsettings.json";

        private AppSettings appSettings;

        public string GetServerStatusServiceIP()
        {
            return appSettings.ServerStatusServiceIP;
        }

        public void SetServerStatusServiceIP(string newIP)
        {
            appSettings.ServerStatusServiceIP = newIP;
            SaveJsonToDisk(filename, appSettings);
        }

        public int GetServerStatusServicePort()
        {
            return appSettings.ServerStatusServicePort;
        }

        public void SetServerStatusServicePort(int newPort)
        {
            appSettings.ServerStatusServicePort = newPort;
            SaveJsonToDisk(filename, appSettings);
        }
    }
}