using UnityEngine;
using Zenject;

public class PlayerShip : MonoBehaviour
{
    private GameEvents _gameEvents;
    
    [Inject]
    private void Inject(GameEvents gameEvents)
    {
        _gameEvents = gameEvents;     
    }
    
    public void KillPlayer()
    {
        _gameEvents.KillPlayer();
        Destroy(gameObject);
    }
}
