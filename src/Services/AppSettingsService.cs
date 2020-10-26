using Newtonsoft.Json;

namespace RotnBot.Services 
{
    public class AppSettingsService : JsonFileLoader, IAppSettingsService
    {
        public AppSettingsService()
        {
            string json = System.IO.File.ReadAllText(filename);
            appSettings = JsonConvert.DeserializeObject<AppSettings>(json);
        }

        private const string filename = "data/appsettings.json";

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

        public string GetVersion()
        {
            return appSettings.Version;
        }
    }
}