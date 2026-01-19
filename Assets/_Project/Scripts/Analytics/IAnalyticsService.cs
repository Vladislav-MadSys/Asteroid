namespace _Project.Scripts.Analytics
{
    public interface IAnalyticsService
    {
        public void LogGameStart();
        public void LogGameEnd(int shotsCount, int laserUsesCount, int destroyedAsteroids, int destroyedUfo);
        public void LogLaserUse();
    }
}
