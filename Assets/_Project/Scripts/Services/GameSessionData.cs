using UnityEngine;
using Zenject;

public class GameSessionData : MonoBehaviour
{
    [field: SerializeField] public int Points { get; private set; } = 0;
    
    private GameEvents _gameEvents;   

    [Inject]
    void Inject(GameEvents gameEvents)
    {
        _gameEvents = gameEvents;
    }
    
    private void Awake()
    {
        _gameEvents.OnEnemyKilled += ChangePoints;
    }

    private void OnDestroy()
    {
        _gameEvents.OnEnemyKilled -= ChangePoints;
    }

    void ChangePoints(int deltaPoints)
    {
        Points += deltaPoints;
        _gameEvents.ChangePoints(Points);
    }
}
