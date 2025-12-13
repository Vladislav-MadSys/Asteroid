using _Project.Scripts.GameEntities.Enemies;
using Zenject;

namespace _Project.Scripts.Services
{
    public class EnemyDeathListener
    {
        private GameSessionData _gameSessionData;

        [Inject]
        private void Inject(GameSessionData gameSessionData)
        {
            _gameSessionData = gameSessionData;
        }
        public void OnEnemyDeath(Enemy enemy)
        {
            _gameSessionData.ChangePoints(enemy.PointsForKill);
        }
    }
}
