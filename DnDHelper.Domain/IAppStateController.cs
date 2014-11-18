namespace DnDHelper.Domain
{
    public interface IAppStateController
    {
        bool IsRunning { get; set; }
        bool IsOffline { get; set; }
    }
}