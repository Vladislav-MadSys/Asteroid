using UnityEngine;
using Zenject;

public class UfoSpawner : ObstaclesSpawner
{
    private PlayerShip _playerShip;
    
    [Inject]
    void Inject(PlayerShip playerShip, GameEvents gameEvents)
    {
        _playerShip = playerShip;
        GameEvents = gameEvents;
    }

    protected override void Spawn()
    {
        Vector3 spawnPosition = GetPositionOutsideScreen();
        GameObject obstacle = Instantiate(Prefab, spawnPosition, Quaternion.identity);
        if (obstacle.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Initialize(GameEvents);
        }
        if (obstacle.TryGetComponent(out UfoMovment ufoMovment))
        {
            ufoMovment.Initialize(_playerShip);
        }
    }
}
