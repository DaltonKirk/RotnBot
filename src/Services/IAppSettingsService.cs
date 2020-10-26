namespace RotnBot.Services
{
    public interface IAppSettingsService
    {
        string GetServerStatusServiceIP();
        void SetServerStatusServiceIP(string newIP);
        int GetServerStatusServicePort();
        void SetServerStatusServicePort(int newPort);
        string GetVersion();
    }
}