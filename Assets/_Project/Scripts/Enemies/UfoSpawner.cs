using UnityEngine;
using Zenject;

public class UfoSpawner : ObstaclesSpawner
{
    private PlayerShip _playerShip;
    
    [Inject]
    void Inject(PlayerShip playerShip)
    {
        _playerShip = playerShip;
    }

    protected override void Spawn()
    {
        Vector3 spawnPosition = GetPositionOutsideScreen();
        GameObject obstacle = Instantiate(_prefab, spawnPosition, Quaternion.identity);
        if (obstacle.TryGetComponent(out UfoMovment ufoMovment))
        {
            ufoMovment.Initialize(_playerShip);
        }
        
    }
    
}
